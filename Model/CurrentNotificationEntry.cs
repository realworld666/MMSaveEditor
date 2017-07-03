// Decompiled with JetBrains decompiler
// Type: CurrentNotificationEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using MM2.UI;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CurrentNotificationEntry : MonoBehaviour
{
  public TextMeshProUGUI notificationName;
  public UINotification uiNotificationCounter;
  public CalendarEventTypeIconContainer icon;
  public Button button;
  private Notification mNotification;

  private void Awake()
  {
    this.button.onClick.AddListener(new UnityAction(this.OnButtonClick));
  }

  private void OnButtonClick()
  {
    if (UIManager.instance.IsScreenLoaded(this.mNotification.screenReference))
    {
      if (App.instance.gameStateManager.currentState is PreSeasonState && !PreSeasonState.IsAtCorrectPreSeasonScreenForMailLink(this.mNotification.screenReference))
      {
        scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
        this.mNotification.ResetCount();
      }
      else
        UIManager.instance.ChangeScreen(this.mNotification.screenReference, UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
    }
    else
    {
      scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
      this.mNotification.ResetCount();
    }
  }

  public void Setup(Notification inNotification)
  {
    this.mNotification = inNotification;
    this.notificationName.text = this.mNotification.name;
    if (this.mNotification.localisationID != string.Empty)
      this.notificationName.text = Localisation.LocaliseID(this.mNotification.localisationID, (GameObject) null);
    else
      this.notificationName.text = this.mNotification.name;
    this.uiNotificationCounter.notificationName = this.mNotification.name;
    this.uiNotificationCounter.FindNotification();
    this.icon.SetIcon(this.mNotification.iconType);
  }
}
