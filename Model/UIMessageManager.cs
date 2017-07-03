// Decompiled with JetBrains decompiler
// Type: UIMessageManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UIMessageManager : MonoBehaviour
{
  public UIMessageBar messageBar;
  public UIMessageNotification notification;

  public bool isMessageBarActive
  {
    get
    {
      return this.messageBar.gameObject.activeSelf;
    }
  }

  public bool isNotificationActive
  {
    get
    {
      return this.notification.state != UIMessageNotification.State.InActive;
    }
  }

  public void OnStart()
  {
    this.messageBar.OnStart();
    this.notification.OnStart();
  }

  public void PostMessage(Message inMessage)
  {
    GameState currentState = App.instance.gameStateManager.currentState;
    if (inMessage.showNotification)
      this.notification.QueueMessage(inMessage);
    if (!currentState.IsFrontend())
      return;
    this.messageBar.PostMessage(inMessage);
  }

  public void SetActive(bool inActive)
  {
    this.gameObject.SetActive(inActive);
    this.messageBar.QuickHide();
    this.notification.QuickHide();
  }
}
