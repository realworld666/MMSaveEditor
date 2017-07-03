// Decompiled with JetBrains decompiler
// Type: UIHQSelectedBuilding
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHQSelectedBuilding : MonoBehaviour
{
  public Button upgradeButton;
  public UIGridList requirementsGrid;
  public UIGridList currentGrid;
  public GameObject knownledgePrefab;
  public TextMeshProUGUI nameLabel;
  public TextMeshProUGUI levelLabel;
  public TextMeshProUGUI descriptionLabel;
  public TextMeshProUGUI statusLabel;
  public GameObject perRaceParent;
  public GameObject perRaceCost;
  public GameObject perRaceIncome;
  public TextMeshProUGUI perRaceCostLabel;
  public UIBuilding buildingIcon;
  public UIHQGroupIcon groupIcon;
  public GameObject upgradingIcon;
  public GameObject contentParent;
  public GameObject requirementsParent;
  public GameObject currentKnowledgeParent;
  public GameObject upgradeParent;
  public GameObject upgradingParent;
  public Slider upgradingProgressBar;
  public TextMeshProUGUI upgradingTimeLeft;
  public TextMeshProUGUI upgradingStatus;
  public GameObject unlockIcon;
  public GameObject upgradeButtonHeadingUpgrade;
  public GameObject upgradeButtonHeadingBuild;
  public TextMeshProUGUI upgradeWeeksLeft;
  public TextMeshProUGUI upgradeCost;
  public GameObject upgradeHeaderBuild;
  public GameObject upgradeHeaderUpgrade;
  public HeadquartersScreen screen;
  private HQsBuilding_v1 mBuilding;

  public void OnStart()
  {
    this.upgradeButton.onClick.AddListener(new UnityAction(this.OnUpgradeButton));
  }

  public void Setup(HQsBuilding_v1 inBuilding)
  {
    this.ClearNotification();
    this.mBuilding = inBuilding;
    this.mBuilding.OnNotification -= new Action<HQsBuilding_v1.NotificationState>(this.OnBuildingNotification);
    this.mBuilding.OnNotification += new Action<HQsBuilding_v1.NotificationState>(this.OnBuildingNotification);
    this.Refresh();
    GameUtility.SetActive(this.gameObject, true);
  }

  private void Update()
  {
    if (this.mBuilding == null || !this.upgradingParent.activeSelf)
      return;
    this.upgradingProgressBar.value = this.mBuilding.normalizedProgressUI;
  }

  public void Hide()
  {
    GameUtility.SetActive(this.gameObject, false);
  }

  public void Refresh()
  {
    this.SetDetails();
    this.SetRequirements();
    this.SetKnowledge();
    this.SetUpgrades();
  }

  private void SetDetails()
  {
    this.nameLabel.text = this.mBuilding.buildingName;
    this.descriptionLabel.text = this.mBuilding.info.GetDescription();
    GameUtility.SetActive(this.levelLabel.gameObject, !this.mBuilding.isLocked && !this.mBuilding.isLeveling && !this.mBuilding.isMaxLevel);
    GameUtility.SetActive(this.statusLabel.gameObject, this.mBuilding.isLocked || this.mBuilding.isLeveling || this.mBuilding.isMaxLevel);
    StringVariableParser.building = this.mBuilding;
    if (this.levelLabel.gameObject.activeSelf)
    {
      StringVariableParser.building = this.mBuilding;
      this.levelLabel.text = !this.mBuilding.isBuilt ? Localisation.LocaliseID("PSG_10009991", (GameObject) null) : Localisation.LocaliseID("PSG_10010003", (GameObject) null);
    }
    if (this.statusLabel.gameObject.activeSelf)
    {
      StringVariableParser.building = this.mBuilding;
      this.statusLabel.text = !this.mBuilding.isLocked ? (!this.mBuilding.isMaxLevel ? (this.mBuilding.isBuilt ? Localisation.LocaliseID("PSG_10009999", (GameObject) null) : Localisation.LocaliseID("PSG_10010000", (GameObject) null)) : Localisation.LocaliseID("PSG_10009993", (GameObject) null)) : Localisation.LocaliseID("PSG_10009992", (GameObject) null);
    }
    GameUtility.SetActive(this.perRaceParent, this.mBuilding.isBuilt);
    if (this.perRaceParent.activeSelf)
    {
      long totalCost = (long) this.mBuilding.costs.GetTotalCost(HQsBuildingCosts.TimePeriod.PerRace, true);
      GameUtility.SetActive(this.perRaceIncome, totalCost >= 0L);
      GameUtility.SetActive(this.perRaceCost, totalCost < 0L);
      this.perRaceCostLabel.text = GameUtility.GetCurrencyString((long) this.mBuilding.costs.GetTotalCost(HQsBuildingCosts.TimePeriod.PerRace, true), 0);
      this.perRaceCostLabel.color = totalCost < 0L ? UIConstants.negativeColor : UIConstants.positiveColor;
    }
    this.buildingIcon.SetBuilding(this.mBuilding);
    this.groupIcon.SetIcon(this.mBuilding);
    GameUtility.SetActive(this.upgradingIcon, this.mBuilding.isLeveling);
  }

  private void SetKnowledge()
  {
    int num1 = 0;
    int num2 = 0;
    this.currentGrid.DestroyListItems();
    this.currentGrid.itemPrefab = this.knownledgePrefab;
    GameUtility.SetActive(this.currentGrid.itemPrefab, true);
    List<PartTypeSlotSettings> settings = this.GetSettings(PartTypeSlotSettingsManager.GetPartsRelevantToBuilding(this.mBuilding.info.type));
    int count = settings.Count;
    for (int index = 0; index < count; ++index)
    {
      this.currentGrid.CreateListItem<UIHQKnowledgeEntry>().SetupKnowledge(settings[index], false, this.mBuilding);
      ++num1;
    }
    if (this.mBuilding.info.effects != null && this.mBuilding.isBuilt)
    {
      for (int inEffectIndex = 0; inEffectIndex < this.mBuilding.info.effects.Length; ++inEffectIndex)
      {
        this.currentGrid.CreateListItem<UIHQKnowledgeEntry>().SetupEffect(this.mBuilding, inEffectIndex, false);
        ++num1;
      }
    }
    GameUtility.SetActive(this.currentGrid.itemPrefab, false);
    HQsBuilding_v1[] buildingsCanUnlock = this.mBuilding.team.headquarters.GetBuildingsCanUnlock(this.mBuilding.info.type, this.mBuilding.nextLevel);
    int num3 = num2 + buildingsCanUnlock.Length;
    if (this.mBuilding.info.sets[Game.instance.player.team.championship.championshipID].ContainsKey(this.mBuilding.nextLevel))
    {
      List<PartTypeSlotSettings> typeSlotSettingsList = this.mBuilding.info.sets[Game.instance.player.team.championship.championshipID][this.mBuilding.nextLevel];
      num3 += typeSlotSettingsList.Count;
    }
    if (this.mBuilding.info.effectsNextLevel != null)
      num3 += this.mBuilding.info.effectsNextLevel.Length;
    GameUtility.SetActive(this.currentKnowledgeParent, num1 > 0);
    GameUtility.SetActive(this.unlockIcon, !this.mBuilding.isLeveling && !this.mBuilding.isMaxLevel && num3 > 0);
  }

  private void SetRequirements()
  {
    GameUtility.SetActive(this.requirementsParent, this.mBuilding.isLocked);
    if (!this.requirementsParent.gameObject.activeSelf)
      return;
    this.requirementsGrid.DestroyListItems();
    HQsDependency[] remainingDependencies = this.mBuilding.GetRemainingDependencies();
    GameUtility.SetActive(this.requirementsGrid.itemPrefab, true);
    for (int inIndex = 0; inIndex < remainingDependencies.Length; ++inIndex)
      this.requirementsGrid.GetOrCreateItem<UIHQRequirementEntry>(inIndex).Setup(remainingDependencies[inIndex]);
    GameUtility.SetActive(this.requirementsGrid.itemPrefab, false);
  }

  private void SetUpgrades()
  {
    GameUtility.SetActive(this.contentParent, this.requirementsParent.activeSelf || this.currentKnowledgeParent.activeSelf);
    GameUtility.SetActive(this.upgradeParent, this.mBuilding.CanBuild() || this.mBuilding.CanUpgrade());
    GameUtility.SetActive(this.upgradingParent, this.mBuilding.isLeveling);
    if (this.upgradeParent.gameObject.activeSelf)
    {
      GameUtility.SetActive(this.upgradeButtonHeadingBuild, !this.mBuilding.isBuilt);
      GameUtility.SetActive(this.upgradeButtonHeadingUpgrade, this.mBuilding.isBuilt);
      StringVariableParser.building = this.mBuilding;
      this.upgradeWeeksLeft.text = this.mBuilding.isBuilt ? Localisation.LocaliseID("PSG_10010036", (GameObject) null) : Localisation.LocaliseID("PSG_10010035", (GameObject) null);
      this.upgradeCost.text = GameUtility.GetCurrencyString(this.mBuilding.isBuilt ? (long) this.mBuilding.costs.GetUpgradeCost() : (long) this.mBuilding.costs.GetBuildTotalCost(), 0);
      GameUtility.SetActive(this.upgradeHeaderBuild, !this.mBuilding.isBuilt);
      GameUtility.SetActive(this.upgradeHeaderUpgrade, this.mBuilding.isBuilt);
    }
    if (!this.upgradingParent.gameObject.activeSelf)
      return;
    this.upgradingProgressBar.value = this.mBuilding.normalizedProgressUI;
    this.upgradingTimeLeft.text = GameUtility.FormatTimeSpanWeeks(this.mBuilding.timeRemaining);
    this.upgradingStatus.text = this.mBuilding.isBuilt ? Localisation.LocaliseID("PSG_10008933", (GameObject) null) : Localisation.LocaliseID("PSG_10010000", (GameObject) null);
  }

  private List<PartTypeSlotSettings> GetSettings(CarPart.PartType[] inTypes)
  {
    List<PartTypeSlotSettings> typeSlotSettingsList = new List<PartTypeSlotSettings>();
    if (inTypes != null)
    {
      for (int index = 0; index < inTypes.Length; ++index)
      {
        if (Game.instance.partSettingsManager.championshipPartSettings[Game.instance.player.team.championship.championshipID].ContainsKey(inTypes[index]))
          typeSlotSettingsList.Add(Game.instance.partSettingsManager.championshipPartSettings[Game.instance.player.team.championship.championshipID][inTypes[index]]);
      }
    }
    return typeSlotSettingsList;
  }

  public void OnBuildingNotification(HQsBuilding_v1.NotificationState inState)
  {
    this.Refresh();
  }

  public void ClearNotification()
  {
    if (this.mBuilding == null)
      return;
    this.mBuilding.OnNotification -= new Action<HQsBuilding_v1.NotificationState>(this.OnBuildingNotification);
  }

  private void OnUpgradeButton()
  {
    if (this.mBuilding.isLeveling)
      return;
    HQUpgradePopup.Open(this.mBuilding);
  }

  public void OnRolloverEnter()
  {
    if (this.mBuilding == null || !this.unlockIcon.activeSelf)
      return;
    UIHQRollover.Setup(this.screen.screenMode, this.mBuilding, UIHQRollover.Mode.AutoSelect, false);
  }

  public void OnRolloverHide()
  {
    UIHQRollover.Close();
  }
}
