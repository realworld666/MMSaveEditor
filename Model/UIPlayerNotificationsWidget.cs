// Decompiled with JetBrains decompiler
// Type: UIPlayerNotificationsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class UIPlayerNotificationsWidget : MonoBehaviour
{
  public UIGridList gridList;
  public GameObject noNotificationsGFX;

  public void Setup()
  {
    if (!Game.IsActive())
      return;
    Game.instance.notificationManager.OnNotificationsChanged -= new Action(this.CreateList);
    Game.instance.notificationManager.OnNotificationsChanged += new Action(this.CreateList);
    this.CreateList();
  }

  public void CreateList()
  {
    if (!(UIManager.instance.currentScreen is PlayerScreen))
      return;
    this.gridList.DestroyListItems();
    NotificationManager notificationManager = Game.instance.notificationManager;
    for (int inIndex = 0; inIndex < notificationManager.count; ++inIndex)
    {
      Notification entity = notificationManager.GetEntity(inIndex);
      if (entity.count != 0 && entity.display)
        this.gridList.CreateListItem<CurrentNotificationEntry>().Setup(entity);
    }
    this.noNotificationsGFX.SetActive(this.gridList.itemCount == 0);
  }

  public void OnExit()
  {
    if (!Game.IsActive())
      return;
    Game.instance.notificationManager.OnNotificationsChanged -= new Action(this.CreateList);
  }
}
