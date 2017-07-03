// Decompiled with JetBrains decompiler
// Type: UIHQBuildingsBarWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHQBuildingsBarWidget : MonoBehaviour
{
  public GameObject headerBuild;
  public GameObject headerUpgrade;
  public UIGridList grid;
  public Button closeButton;
  public HeadquartersScreen screen;
  private UIHQBuildingsBarWidget.Mode mMode;
  private Headquarters mHeadquarters;
  private Notification mNotificationBuild;
  private Notification mNotificationUpgrade;
  private HQsBuilding_v1 mBuilding;

  public UIHQBuildingsBarWidget.Mode mode
  {
    get
    {
      return this.mMode;
    }
  }

  public void OnStart()
  {
    this.closeButton.onClick.RemoveAllListeners();
    this.closeButton.onClick.AddListener(new UnityAction(this.OnCloseButton));
  }

  public void Setup(UIHQBuildingsBarWidget.Mode inMode)
  {
    this.mMode = inMode;
    this.mHeadquarters = this.screen.headquarters;
    HQsBuilding_v1.OnBuildingNotification -= new Action<HQsBuilding_v1.NotificationState, HQsBuilding_v1>(this.OnNotification);
    HQsBuilding_v1.OnBuildingNotification += new Action<HQsBuilding_v1.NotificationState, HQsBuilding_v1>(this.OnNotification);
    GameUtility.SetActive(this.closeButton.gameObject, this.screen.screenMode == UIScreen.ScreenMode.Mode3D);
    this.SetGrid();
    this.SetNotifications();
  }

  public void SetGrid()
  {
    GameUtility.SetActive(this.grid.itemPrefab, true);
    List<HQsBuilding_v1> hqsBuildingV1List = new List<HQsBuilding_v1>((IEnumerable<HQsBuilding_v1>) this.mHeadquarters.hqBuildings);
    hqsBuildingV1List.Sort((Comparison<HQsBuilding_v1>) ((x, y) =>
    {
      if (x.info.category == y.info.category)
        return x.buildingName.CompareTo(y.buildingName);
      return x.buildingCategory.CompareTo(y.buildingCategory);
    }));
    bool flag = false;
    int itemCount1 = this.grid.itemCount;
    int count = hqsBuildingV1List.Count;
    int num = count - itemCount1;
    for (int index = 0; index < num; ++index)
      this.grid.CreateListItem<UIHQBuildingEntry>().OnStart();
    int itemCount2 = this.grid.itemCount;
    int index1 = 0;
    for (int index2 = 0; index2 < itemCount2; ++index2)
    {
      HQsBuilding_v1 inBuilding = hqsBuildingV1List[index2];
      if (this.mMode == UIHQBuildingsBarWidget.Mode.Build)
        flag = !inBuilding.isBuilt;
      else if (this.mMode == UIHQBuildingsBarWidget.Mode.Upgrade)
        flag = inBuilding.isBuilt;
      UIHQBuildingEntry uihqBuildingEntry = this.grid.GetItem<UIHQBuildingEntry>(index2);
      GameUtility.SetActive(uihqBuildingEntry.gameObject, flag && index2 < count);
      if (uihqBuildingEntry.gameObject.activeSelf)
      {
        uihqBuildingEntry.Setup(inBuilding);
        if (this.mMode == UIHQBuildingsBarWidget.Mode.Build && !inBuilding.isLocked)
        {
          if (uihqBuildingEntry.transform.GetSiblingIndex() != index1)
            uihqBuildingEntry.transform.SetSiblingIndex(index1);
          ++index1;
        }
        else if (this.mMode == UIHQBuildingsBarWidget.Mode.Upgrade && uihqBuildingEntry.transform.GetSiblingIndex() != index2)
          uihqBuildingEntry.transform.SetSiblingIndex(index2);
      }
    }
    GameUtility.SetActive(this.grid.itemPrefab, false);
    GameUtility.SetActive(this.headerBuild, this.mMode == UIHQBuildingsBarWidget.Mode.Build);
    GameUtility.SetActive(this.headerUpgrade, this.mMode == UIHQBuildingsBarWidget.Mode.Upgrade);
  }

  public void SetNotifications()
  {
    this.mHeadquarters = this.screen.headquarters;
    this.mNotificationBuild = Game.instance.notificationManager.GetNotification("HQBuildingsAvailable");
    this.mNotificationBuild.ResetCount();
    this.mNotificationUpgrade = Game.instance.notificationManager.GetNotification("HQUpgradesAvailable");
    this.mNotificationUpgrade.ResetCount();
    int count = this.mHeadquarters.hqBuildings.Count;
    for (int index = 0; index < count; ++index)
    {
      this.mBuilding = this.mHeadquarters.hqBuildings[index];
      if (this.mBuilding.state == HQsBuilding_v1.BuildingState.NotBuilt && !this.mBuilding.isLocked)
        this.mNotificationBuild.IncrementCount();
      else if (this.mBuilding.isBuilt && this.mBuilding.CanUpgrade())
        this.mNotificationUpgrade.IncrementCount();
    }
  }

  private void OnNotification(HQsBuilding_v1.NotificationState inState, HQsBuilding_v1 inBuilding)
  {
    if (this.gameObject.activeSelf)
      this.SetGrid();
    this.SetNotifications();
  }

  public void ClearNotifications()
  {
    HQsBuilding_v1.OnBuildingNotification -= new Action<HQsBuilding_v1.NotificationState, HQsBuilding_v1>(this.OnNotification);
  }

  private void OnCloseButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.screen.HideBuildingList(true);
  }

  public enum Mode
  {
    Build,
    Upgrade,
  }
}
