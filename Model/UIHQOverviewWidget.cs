// Decompiled with JetBrains decompiler
// Type: UIHQOverviewWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHQOverviewWidget : MonoBehaviour
{
  public ToggleGroup toggleGroup;
  public Toggle buildButton;
  public Toggle upgradeButton;
  public GameObject buildButtonNewBuildings;
  public GameObject buildButtonNoBuildings;
  public UIHQKnowledgeEntry[] knowledgeEntries;
  public UITeamStatsBarEntry hqStatBar;
  public UITeamStatsBarEntryHQ[] statBars;
  public HeadquartersScreen screen;

  public bool isBuildToggleInteractable
  {
    get
    {
      return this.buildButton.interactable;
    }
  }

  public void OnStart()
  {
    this.buildButton.onValueChanged.RemoveAllListeners();
    this.buildButton.onValueChanged.AddListener(new UnityAction<bool>(this.OnBuildToggle));
    this.upgradeButton.onValueChanged.RemoveAllListeners();
    this.upgradeButton.onValueChanged.AddListener(new UnityAction<bool>(this.OnUpgradeToggle));
  }

  public void Setup()
  {
    HQsBuilding_v1.OnBuildingNotification -= new Action<HQsBuilding_v1.NotificationState, HQsBuilding_v1>(this.OnNotification);
    HQsBuilding_v1.OnBuildingNotification += new Action<HQsBuilding_v1.NotificationState, HQsBuilding_v1>(this.OnNotification);
    this.Refresh();
  }

  private void Refresh()
  {
    CarPart.PartType[] partType = CarPart.GetPartType(Game.instance.player.team.championship.series, false);
    for (int index = 0; index < this.knowledgeEntries.Length; ++index)
    {
      if (index < partType.Length)
        this.knowledgeEntries[index].Setup(partType[index]);
      GameUtility.SetActive(this.knowledgeEntries[index].gameObject, index < partType.Length);
    }
    this.SetStats();
    this.SetToggles();
  }

  private void SetStats()
  {
    Team team = Game.instance.player.team;
    this.hqStatBar.Setup(team);
    for (int index = 0; index < this.statBars.Length; ++index)
      this.statBars[index].Setup(team);
  }

  private void SetToggles()
  {
    this.toggleGroup.allowSwitchOff = this.screen.screenMode == UIScreen.ScreenMode.Mode3D;
    bool inIsActive = false;
    int count = this.screen.headquarters.hqBuildings.Count;
    for (int index = 0; index < count; ++index)
    {
      if (!this.screen.headquarters.hqBuildings[index].isBuilt)
      {
        inIsActive = true;
        break;
      }
    }
    this.buildButton.interactable = inIsActive;
    GameUtility.SetActive(this.buildButtonNewBuildings, inIsActive);
    GameUtility.SetActive(this.buildButtonNoBuildings, !inIsActive);
  }

  public void SetTogglesOff()
  {
    this.toggleGroup.SetAllTogglesOff();
  }

  public void SelectToggle()
  {
    this.SetTogglesOff();
    this.buildButton.isOn = this.screen.barWidget.mode == UIHQBuildingsBarWidget.Mode.Build;
    this.upgradeButton.isOn = this.screen.barWidget.mode == UIHQBuildingsBarWidget.Mode.Upgrade;
  }

  private void OnBuildToggle(bool inValue)
  {
    if (inValue)
    {
      if (this.screen.barWidget.gameObject.activeSelf && this.screen.screenMode == UIScreen.ScreenMode.Mode3D)
        this.screen.HideBuildingList(false);
      this.screen.ShowBuildingList(UIHQBuildingsBarWidget.Mode.Build);
    }
    else
    {
      if (this.screen.screenMode != UIScreen.ScreenMode.Mode3D || this.upgradeButton.isOn)
        return;
      this.screen.HideBuildingList(true);
    }
  }

  private void OnUpgradeToggle(bool inValue)
  {
    if (inValue)
    {
      if (this.screen.barWidget.gameObject.activeSelf && this.screen.screenMode == UIScreen.ScreenMode.Mode3D)
        this.screen.HideBuildingList(false);
      this.screen.ShowBuildingList(UIHQBuildingsBarWidget.Mode.Upgrade);
    }
    else
    {
      if (this.screen.screenMode != UIScreen.ScreenMode.Mode3D || this.buildButton.isOn)
        return;
      this.screen.HideBuildingList(true);
    }
  }

  public void OnNotification(HQsBuilding_v1.NotificationState inState, HQsBuilding_v1 inBuilding)
  {
    if (!this.gameObject.activeSelf)
      return;
    this.Refresh();
  }

  public void ClearNotifications()
  {
    HQsBuilding_v1.OnBuildingNotification -= new Action<HQsBuilding_v1.NotificationState, HQsBuilding_v1>(this.OnNotification);
  }
}
