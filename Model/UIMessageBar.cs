// Decompiled with JetBrains decompiler
// Type: UIMessageBar
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class UIMessageBar : MonoBehaviour
{
  public Vector3 offScreenPosition = Vector3.zero;
  public Vector3 onScreenPosition = Vector3.zero;
  public float transitionDuration = 0.2f;
  public EasingUtility.Easing easeIn = EasingUtility.Easing.OutSin;
  public EasingUtility.Easing easeOut = EasingUtility.Easing.InSin;
  private List<Message> mMessages = new List<Message>();
  public AnimatedGrid grid;
  private GameObject mMessagePrefab;
  private UIMessageBar.State mState;
  private float mStateTimer;

  public UIMessageBar.State state
  {
    get
    {
      return this.mState;
    }
  }

  public void OnStart()
  {
    UIManager.OnScreenChange += new Action(this.OnScreenChange);
    Game.OnGameDataChanged += new Action(this.OnGameDataChanged);
  }

  public void OnGameDataChanged()
  {
    for (int index = 0; index < this.grid.transform.childCount; ++index)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.grid.transform.GetChild(index).gameObject);
    this.mMessages.Clear();
    if (!Game.IsActive())
      return;
    MessageManager messageManager = Game.instance.messageManager;
    int count = messageManager.count;
    for (int inIndex = 0; inIndex < count; ++inIndex)
      this.PostMessageInstantly(messageManager.GetEntity(inIndex));
  }

  public void OnDestroy()
  {
    UIManager.OnScreenChange -= new Action(this.OnScreenChange);
    Game.OnGameDataChanged -= new Action(this.OnGameDataChanged);
  }

  public void PostMessage(Message inMessage)
  {
    if (this.gameObject.activeSelf)
      this.grid.Insert(this.AddMessage(inMessage).gameObject);
    else
      this.PostMessageInstantly(inMessage);
  }

  private void PostMessageInstantly(Message inMessage)
  {
    this.grid.InsertInstantly(this.AddMessage(inMessage).gameObject);
  }

  private UIMessage AddMessage(Message inMessage)
  {
    this.LoadMessagePrefabIfRequired();
    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mMessagePrefab);
    UIMessage component = gameObject.GetComponent<UIMessage>();
    component.SetMessage(inMessage);
    gameObject.SetActive(false);
    this.mMessages.Add(inMessage);
    return component;
  }

  private void LoadMessagePrefabIfRequired()
  {
    if (!((UnityEngine.Object) this.mMessagePrefab == (UnityEngine.Object) null))
      return;
    this.mMessagePrefab = Resources.Load("Prefabs/UI/Widgets/Shared/UIMessage") as GameObject;
  }

  public void Show()
  {
    this.SetState(UIMessageBar.State.Intro);
  }

  public void Hide()
  {
    this.SetState(UIMessageBar.State.Outro);
  }

  public void QuickHide()
  {
    this.SetState(UIMessageBar.State.InActive);
  }

  public void OnScreenChange()
  {
    this.Hide();
  }

  private void Update()
  {
    this.mStateTimer += GameTimer.deltaTime;
    switch (this.mState)
    {
      case UIMessageBar.State.InActive:
        this.UpdateInActive();
        break;
      case UIMessageBar.State.Intro:
        this.UpdateIntro();
        break;
      case UIMessageBar.State.Active:
        this.UpdateActive();
        break;
      case UIMessageBar.State.Outro:
        this.UpdateOutro();
        break;
    }
  }

  private void SetState(UIMessageBar.State inState)
  {
    this.mState = inState;
    this.mStateTimer = 0.0f;
    switch (this.mState)
    {
      case UIMessageBar.State.InActive:
        this.OnEnterInActiveState();
        break;
      case UIMessageBar.State.Intro:
        this.OnEnterIntroState();
        break;
      case UIMessageBar.State.Active:
        this.OnEnterActiveState();
        break;
      case UIMessageBar.State.Outro:
        this.OnEnterOutroState();
        break;
    }
  }

  private void OnEnterIntroState()
  {
    this.transform.localPosition = this.offScreenPosition;
    this.gameObject.SetActive(true);
  }

  private void UpdateIntro()
  {
    this.transform.localPosition = EasingUtility.EaseByType(this.easeIn, this.offScreenPosition, this.onScreenPosition, Mathf.Clamp01(this.mStateTimer / this.transitionDuration));
    if ((double) this.mStateTimer <= (double) this.transitionDuration)
      return;
    this.SetState(UIMessageBar.State.Active);
  }

  private void OnEnterActiveState()
  {
  }

  private void UpdateActive()
  {
  }

  private void OnEnterOutroState()
  {
  }

  private void UpdateOutro()
  {
    this.transform.localPosition = EasingUtility.EaseByType(this.easeOut, this.onScreenPosition, this.offScreenPosition, Mathf.Clamp01(this.mStateTimer / this.transitionDuration));
    if ((double) this.mStateTimer <= (double) this.transitionDuration)
      return;
    this.SetState(UIMessageBar.State.InActive);
  }

  private void OnEnterInActiveState()
  {
    this.transform.localPosition = this.offScreenPosition;
    this.gameObject.SetActive(false);
    Game.instance.messageManager.MarkAllMessagesAsRead();
  }

  private void UpdateInActive()
  {
  }

  public enum State
  {
    InActive,
    Intro,
    Active,
    Outro,
  }
}
