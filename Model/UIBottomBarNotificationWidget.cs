// Decompiled with JetBrains decompiler
// Type: UIBottomBarNotificationWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using MM2.UI;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIBottomBarNotificationWidget : MonoBehaviour
{
  public Animator animator;
  public TextMeshProUGUI notificationText;
  public Button button;
  public CalendarEventTypeIconContainer iconType;
  public CanvasGroup teamLogoCanvasGroup;
  private Notification mNotification;
  private NotificationManager mNotificationManager;

  private void Awake()
  {
    this.button.onClick.AddListener(new UnityAction(this.OnButtonPressed));
    GameStateManager.OnStateChange += new Action(this.OnStateChange);
  }

  private void OnDestroy()
  {
    GameStateManager.OnStateChange -= new Action(this.OnStateChange);
  }

  private void OnEnable()
  {
    if (!Game.IsActive())
      return;
    this.mNotificationManager = Game.instance.notificationManager;
  }

  private void OnButtonPressed()
  {
    if (!(App.instance.gameStateManager.currentState is FrontendState))
      return;
    UIManager.instance.ChangeScreen(this.mNotification.screenReference, UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }

  private void Update()
  {
    if (!(App.instance.gameStateManager.currentState is FrontendState))
      return;
    this.DispatchNotification();
  }

  private void DispatchNotification()
  {
    if (this.mNotificationManager == null || this.mNotificationManager.notificationsDispachtList.Count == 0 || this.animator.GetCurrentAnimatorStateInfo(0).shortNameHash != AnimationHashes.None)
      return;
    Notification notificationsDispacht = this.mNotificationManager.notificationsDispachtList[0];
    this.mNotification = notificationsDispacht;
    if (notificationsDispacht.localisationID != string.Empty)
      this.notificationText.text = Localisation.LocaliseID(notificationsDispacht.localisationID, (GameObject) null);
    else
      this.notificationText.text = notificationsDispacht.name;
    this.iconType.SetIcon(notificationsDispacht.iconType);
    this.animator.SetTrigger(AnimationHashes.Play);
    this.mNotificationManager.notificationsDispachtList.Remove(notificationsDispacht);
  }

  private void OnDisable()
  {
    this.animator.Play(AnimationHashes.None, 0, 1f);
    if (!((UnityEngine.Object) this.teamLogoCanvasGroup != (UnityEngine.Object) null))
      return;
    this.teamLogoCanvasGroup.alpha = 1f;
  }

  private void OnStateChange()
  {
    if (App.instance.gameStateManager.currentState is FrontendState)
      return;
    this.animator.Play(AnimationHashes.None, 0, 1f);
    if (!((UnityEngine.Object) this.teamLogoCanvasGroup != (UnityEngine.Object) null))
      return;
    this.teamLogoCanvasGroup.alpha = 1f;
  }
}
