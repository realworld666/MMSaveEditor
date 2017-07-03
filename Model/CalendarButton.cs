// Decompiled with JetBrains decompiler
// Type: CalendarButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CalendarButton : MonoBehaviour
{
  private float delayBeforeClosingCalendarAfterSkip = 1.5f;
  public MM2.UI.Calendar calendar;
  public GameObject calendarObject;
  public GameObject[] disableOnShow;
  public Animator animator;
  public Button calendarButton;
  private IEnumerator closeCalendarSoonCoroutine;
  private bool mCalendarVisible;

  public bool isCalendarOpen
  {
    get
    {
      if (this.animator.isActiveAndEnabled)
        return this.animator.GetBool("Open");
      return true;
    }
  }

  private void Awake()
  {
    this.calendarButton.onClick.AddListener(new UnityAction(this.OnCalendarButton));
  }

  private void Start()
  {
    if (!Game.IsActive())
      return;
    Game.instance.time.OnSkipTargetReached += new Action(this.CalendarHandleTimeSkipFinished);
    Game.instance.time.OnPause += new Action(this.CheckCloseCalendar);
    UIManager.OnScreenChange += new Action(this.OnScreenChange);
    Game.instance.time.OnChangeTimeState += new Action<GameTimer.TimeState>(this.OnTimeStateChange);
    Game.OnGameDataChanged += new Action(this.RefreshCalendarButtonActions);
  }

  private void OnDestroy()
  {
    if (Game.instance != null)
    {
      Game.instance.time.OnSkipTargetReached -= new Action(this.CalendarHandleTimeSkipFinished);
      Game.instance.time.OnPause -= new Action(this.CheckCloseCalendar);
      UIManager.OnScreenChange -= new Action(this.OnScreenChange);
      Game.instance.time.OnChangeTimeState -= new Action<GameTimer.TimeState>(this.OnTimeStateChange);
    }
    Game.OnGameDataChanged -= new Action(this.RefreshCalendarButtonActions);
  }

  private void OnCalendarButton()
  {
    float normalizedTime = this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    if ((double) normalizedTime > 0.0 && (double) normalizedTime < 0.75)
      return;
    if (!this.animator.isActiveAndEnabled || !this.animator.GetBool("Open"))
    {
      scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionHome, 0.0f);
      this.OpenCalendar();
    }
    else
      this.Pause();
  }

  public void SetCalendarVisibility(bool inValue)
  {
    this.mCalendarVisible = inValue;
    if (this.mCalendarVisible)
      this.OpenCalendar();
    else
      this.CloseCalendar();
  }

  private void Update()
  {
    bool flag = Game.IsActive() && (App.instance.gameStateManager.IsInState(GameState.Type.FrontendState) || App.instance.gameStateManager.IsInState(GameState.Type.PreSeasonState));
    if (!flag)
      this.mCalendarVisible = false;
    if (Game.instance.time.timeState == GameTimer.TimeState.Skip && !this.mCalendarVisible)
      this.SetCalendarVisibility(true);
    if (this.mCalendarVisible || flag)
      return;
    this.CloseCalendar();
  }

  public void RefreshCalendarButtonActions()
  {
    if (Game.instance != null)
    {
      Game.instance.time.OnSkipTargetReached -= new Action(this.CalendarHandleTimeSkipFinished);
      Game.instance.time.OnSkipTargetReached += new Action(this.CalendarHandleTimeSkipFinished);
      Game.instance.time.OnPause -= new Action(this.CheckCloseCalendar);
      Game.instance.time.OnPause += new Action(this.CheckCloseCalendar);
      Game.instance.time.OnChangeTimeState -= new Action<GameTimer.TimeState>(this.OnTimeStateChange);
      Game.instance.time.OnChangeTimeState += new Action<GameTimer.TimeState>(this.OnTimeStateChange);
    }
    UIManager.OnScreenChange -= new Action(this.OnScreenChange);
    UIManager.OnScreenChange += new Action(this.OnScreenChange);
  }

  public void Pause()
  {
    Game.instance.time.Pause(GameTimer.PauseType.Game);
    this.CloseCalendar();
  }

  private void SetCalendarOpen(bool inValue)
  {
    if (inValue && !this.isCalendarOpen)
      this.OpenCalendar();
    else
      this.CloseCalendar();
  }

  public void OpenCalendar()
  {
    this.calendar.ActivateBlur();
    GameUtility.SetActive(this.calendarObject, true);
    this.animator.SetBool(AnimationHashes.Open, true);
    UIManager.instance.navigationBars.bottomBar.MarkContinueButtonForUpdate();
    this.CancelCalendarClosingSoon();
  }

  private void CheckCloseCalendar()
  {
    if (this.closeCalendarSoonCoroutine != null)
      return;
    this.CloseCalendar();
  }

  private void CloseCalendar()
  {
    if (this.animator.isActiveAndEnabled)
      this.animator.SetBool("Open", false);
    UIManager.instance.navigationBars.bottomBar.MarkContinueButtonForUpdate();
    this.CancelCalendarClosingSoon();
  }

  private void CalendarHandleTimeSkipFinished()
  {
    this.CloseCalendarSoon();
  }

  private void CloseCalendarSoon()
  {
    this.CancelCalendarClosingSoon();
    this.closeCalendarSoonCoroutine = this.CloseCalendarSoonCoroutine();
    App.instance.StartCoroutine(this.closeCalendarSoonCoroutine);
  }

  private void CancelCalendarClosingSoon()
  {
    if (this.closeCalendarSoonCoroutine == null)
      return;
    App.instance.StopCoroutine(this.closeCalendarSoonCoroutine);
    this.closeCalendarSoonCoroutine = (IEnumerator) null;
  }

  [DebuggerHidden]
  private IEnumerator CloseCalendarSoonCoroutine()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new CalendarButton.\u003CCloseCalendarSoonCoroutine\u003Ec__Iterator14()
    {
      \u003C\u003Ef__this = this
    };
  }

  private void OnScreenChange()
  {
    if (!this.isCalendarOpen)
      return;
    this.CheckCloseCalendar();
  }

  private void EnableOtherButtons(bool inValue)
  {
    for (int index = 0; index < this.disableOnShow.Length; ++index)
      GameUtility.SetActive(this.disableOnShow[index].gameObject, inValue);
  }

  private void OnTimeStateChange(GameTimer.TimeState inTimeState)
  {
    if (!this.gameObject.activeSelf)
      return;
    this.calendar.UpdateButtonsState(inTimeState == GameTimer.TimeState.Standard);
    this.EnableOtherButtons(inTimeState == GameTimer.TimeState.Standard);
  }
}
