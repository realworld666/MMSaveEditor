// Decompiled with JetBrains decompiler
// Type: NotificationWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class NotificationWidget : MonoBehaviour
{
  public UIGridList gridList;
  public GameObject container;
  private bool mSetup;

  private void Update()
  {
    GameUtility.SetActive(this.container, Game.IsActive() && (App.instance.gameStateManager.currentState is PreSeasonState || App.instance.gameStateManager.currentState is FrontendState) && this.gridList.itemCount != 0);
    if (this.mSetup)
      return;
    if (Game.IsActive())
    {
      Game.instance.notificationManager.OnNotificationsChanged -= new Action(this.CreateList);
      Game.instance.notificationManager.OnNotificationsChanged += new Action(this.CreateList);
      this.CreateList();
      Game.OnGameDataChanged -= new Action(this.SetupFlagToFalse);
      Game.OnGameDataChanged += new Action(this.SetupFlagToFalse);
      this.mSetup = true;
    }
    else
      GameUtility.SetActive(this.container, false);
  }

  private void SetupFlagToFalse()
  {
    this.mSetup = false;
  }

  public void CreateList()
  {
    this.gridList.DestroyListItems();
    NotificationManager notificationManager = Game.instance.notificationManager;
    for (int inIndex = 0; inIndex < notificationManager.count; ++inIndex)
    {
      Notification entity = notificationManager.GetEntity(inIndex);
      if (entity.count > 0 && entity.display)
        this.gridList.CreateListItem<CurrentNotificationEntry>().Setup(entity);
    }
  }

  private void OnDisable()
  {
    GameUtility.SetActive(this.gameObject, false);
  }

  private void OnDestroy()
  {
    Game.OnGameDataChanged -= new Action(this.SetupFlagToFalse);
    if (!Game.IsActive())
      return;
    Game.instance.notificationManager.OnNotificationsChanged -= new Action(this.CreateList);
  }
}
