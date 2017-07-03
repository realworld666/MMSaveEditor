// Decompiled with JetBrains decompiler
// Type: UIPlaybackSpeedWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPlaybackSpeedWidget : MonoBehaviour
{
  public GameObject[] speedGFX = new GameObject[3];
  private int mHour = -1;
  private int mMinute = -1;
  public Button playButton;
  public Button speedButton;
  public TextMeshProUGUI dateLabel;
  public TextMeshProUGUI timeLabel;
  private DateTime dateLabelLastSetDate;
  private string dateStringFormatLastSetDate;
  public Image dayCycleBar;
  public Image nextEventBar;
  public GameObject pauseButtonGFX;
  public GameObject playButtonGFX;
  public GameObject pauseIconGreyState;
  public Animator animationControllerFullScreen;
  public Animator animationController;

  private void Start()
  {
    this.playButton.onClick.AddListener(new UnityAction(this.OnPlayButton));
    this.speedButton.onClick.AddListener(new UnityAction(this.OnSpeedUp));
  }

  private void OnEnable()
  {
    if (Game.instance == null)
      return;
    if (Game.instance.time.isPaused)
      this.OnGamePaused();
    else
      this.OnGameUnPaused();
    Game.instance.time.OnPause += new Action(this.OnGamePaused);
    Game.instance.time.OnPlay += new Action(this.OnGameUnPaused);
  }

  private void OnDisable()
  {
    if (Game.instance == null)
      return;
    Game.instance.time.OnPause -= new Action(this.OnGamePaused);
    Game.instance.time.OnPlay -= new Action(this.OnGameUnPaused);
  }

  public void OnUnload()
  {
    if (Game.instance == null)
      return;
    Game.instance.time.OnPause -= new Action(this.OnGamePaused);
    Game.instance.time.OnPlay -= new Action(this.OnGameUnPaused);
  }

  private void OnGamePaused()
  {
    if (this.animationControllerFullScreen.enabled)
      this.animationControllerFullScreen.SetTrigger(AnimationHashes.Pause);
    if (!this.animationController.enabled)
      return;
    this.animationController.SetTrigger(AnimationHashes.Pause);
  }

  private void OnGameUnPaused()
  {
    if (this.animationControllerFullScreen.enabled)
      this.animationControllerFullScreen.SetTrigger(AnimationHashes.Play);
    if (!this.animationController.enabled)
      return;
    this.animationController.SetTrigger(AnimationHashes.Play);
  }

  public void OnPlayButton()
  {
    if (Game.instance.time.isPaused)
      Game.instance.time.UnPause(GameTimer.PauseType.Game);
    else
      Game.instance.time.Pause(GameTimer.PauseType.Game);
  }

  public void OnSpeedUp()
  {
    int num = (int) (Game.instance.time.speed + 1);
    if (num > 2)
      num = 0;
    Game.instance.time.SetSpeed((GameTimer.Speed) num);
    scSoundManager.Instance.PlaySound(SoundID.Button_FastForward, 0.0f);
  }

  private void Update()
  {
    this.UpdatePlaybackSpeedButtons();
    this.UpdateLabels();
  }

  private void UpdateLabels()
  {
    if (!Game.IsActive())
      return;
    DateTime now = Game.instance.time.now;
    if (now.Date != this.dateLabelLastSetDate || App.instance.preferencesManager.gamePreferences.GetCurrentDateFormat() != this.dateStringFormatLastSetDate)
    {
      this.dateLabelLastSetDate = now.Date;
      this.dateStringFormatLastSetDate = App.instance.preferencesManager.gamePreferences.GetCurrentDateFormat();
      using (GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe())
      {
        StringBuilder stringBuilder = builderSafe.stringBuilder;
        stringBuilder.Append(GameUtility.GetLocalisedDay(this.dateLabelLastSetDate));
        stringBuilder.Append(" ");
        stringBuilder.Append(GameUtility.FormatDateTimeToShortDateString(this.dateLabelLastSetDate));
        this.dateLabel.text = stringBuilder.ToString();
      }
    }
    if (now.Minute != this.mMinute || now.Hour != this.mHour)
    {
      this.mMinute = now.Minute;
      this.mHour = now.Hour;
      this.timeLabel.text = GameUtility.GetLocalised12HourTime(now, false);
    }
    GameUtility.SetImageFillAmountIfDifferent(this.nextEventBar, Game.instance.calendar.GetNormalisedTimeToNextEvent(), 1f / 512f);
    GameUtility.SetImageFillAmountIfDifferent(this.dayCycleBar, (float) now.Hour / 24f, 1f / 512f);
  }

  private void UpdatePlaybackSpeedButtons()
  {
    if (App.instance.gameStateManager.isChangingState)
      return;
    Game instance = Game.instance;
    if (!Game.IsActive())
      return;
    bool flag = !instance.stateInfo.isReadyToGoToRace;
    GameUtility.SetInteractable(this.playButton, flag);
    GameUtility.SetInteractable(this.speedButton, flag);
    if (App.instance.gameStateManager.currentState.group == GameState.Group.Frontend)
    {
      this.pauseButtonGFX.SetActive(instance.time.isPaused && this.playButton.interactable);
      this.playButtonGFX.SetActive(!instance.time.isPaused && this.speedButton.interactable);
      this.pauseIconGreyState.SetActive(instance.time.isPaused && !this.playButton.interactable);
    }
    this.SetSpeedGFX((int) instance.time.speed, flag);
  }

  private void SetSpeedGFX(int inSpeed, bool inValue)
  {
    for (int index = 0; index < this.speedGFX.Length; ++index)
      this.speedGFX[index].SetActive(index <= inSpeed && inValue);
  }
}
