// Decompiled with JetBrains decompiler
// Type: UIMessageNotification
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class UIMessageNotification : MonoBehaviour
{
  public Vector3 offScreenPosition = Vector3.zero;
  public Vector3 onScreenPosition = Vector3.zero;
  public float transitionDuration = 0.2f;
  public float displayDuration = 4f;
  public EasingUtility.Easing easeIn = EasingUtility.Easing.OutSin;
  public EasingUtility.Easing easeOut = EasingUtility.Easing.InSin;
  private Queue<Message> mMessageQueue = new Queue<Message>();
  public UIMessage messageEntry;
  private RectTransform mRectTransform;
  private UIMessageNotification.State mState;
  private float mStateTimer;

  public UIMessageNotification.State state
  {
    get
    {
      return this.mState;
    }
  }

  public void OnStart()
  {
    Game.OnGameDataChanged += new Action(this.OnGameDataChanged);
    this.mRectTransform = this.GetComponent<RectTransform>();
    this.messageEntry.transform.SetParent(this.transform, false);
    this.messageEntry.isNotificationMessage = true;
    this.messageEntry.gameObject.SetActive(false);
  }

  private void OnDestroy()
  {
    Game.OnGameDataChanged -= new Action(this.OnGameDataChanged);
  }

  public void OnGameDataChanged()
  {
    this.mMessageQueue.Clear();
  }

  public void QueueMessage(Message inMessage)
  {
    this.mMessageQueue.Enqueue(inMessage);
  }

  private void ShowNextMessageNotification()
  {
    this.messageEntry.SetMessage(this.mMessageQueue.Dequeue());
    this.SetState(UIMessageNotification.State.Intro);
  }

  public void QuickHide()
  {
    this.SetState(UIMessageNotification.State.InActive);
    this.mMessageQueue.Clear();
  }

  private void Update()
  {
    this.mStateTimer += GameTimer.deltaTime;
    switch (this.mState)
    {
      case UIMessageNotification.State.InActive:
        this.UpdateInActive();
        break;
      case UIMessageNotification.State.Intro:
        this.UpdateIntro();
        break;
      case UIMessageNotification.State.Active:
        this.UpdateActive();
        break;
      case UIMessageNotification.State.Outro:
        this.UpdateOutro();
        break;
    }
  }

  private void SetState(UIMessageNotification.State inState)
  {
    this.mState = inState;
    this.mStateTimer = 0.0f;
    switch (this.mState)
    {
      case UIMessageNotification.State.InActive:
        this.OnEnterInActiveState();
        break;
      case UIMessageNotification.State.Intro:
        this.OnEnterIntroState();
        break;
      case UIMessageNotification.State.Active:
        this.OnEnterActiveState();
        break;
      case UIMessageNotification.State.Outro:
        this.OnEnterOutroState();
        break;
    }
  }

  private void OnEnterIntroState()
  {
    this.mRectTransform.anchoredPosition3D = this.offScreenPosition;
    this.messageEntry.gameObject.SetActive(true);
  }

  private void UpdateIntro()
  {
    this.mRectTransform.anchoredPosition3D = EasingUtility.EaseByType(this.easeIn, this.offScreenPosition, this.onScreenPosition, Mathf.Clamp01(this.mStateTimer / this.transitionDuration));
    if ((double) this.mStateTimer <= (double) this.transitionDuration)
      return;
    this.SetState(UIMessageNotification.State.Active);
  }

  private void OnEnterActiveState()
  {
  }

  private void UpdateActive()
  {
    if ((double) this.mStateTimer <= (double) this.displayDuration)
      return;
    this.SetState(UIMessageNotification.State.Outro);
  }

  private void OnEnterOutroState()
  {
  }

  private void UpdateOutro()
  {
    this.mRectTransform.anchoredPosition3D = EasingUtility.EaseByType(this.easeOut, this.onScreenPosition, this.offScreenPosition, Mathf.Clamp01(this.mStateTimer / this.transitionDuration));
    if ((double) this.mStateTimer <= (double) this.transitionDuration)
      return;
    this.SetState(UIMessageNotification.State.InActive);
  }

  private void OnEnterInActiveState()
  {
    this.mRectTransform.anchoredPosition3D = this.offScreenPosition;
    this.messageEntry.gameObject.SetActive(false);
  }

  private void UpdateInActive()
  {
    if ((double) this.mStateTimer <= 1.0 || this.mMessageQueue.Count <= 0)
      return;
    this.ShowNextMessageNotification();
  }

  public enum State
  {
    InActive,
    Intro,
    Active,
    Outro,
  }
}
