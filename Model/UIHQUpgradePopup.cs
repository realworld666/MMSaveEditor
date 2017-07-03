// Decompiled with JetBrains decompiler
// Type: UIHQUpgradePopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHQUpgradePopup : MonoBehaviour
{
  public Button confirmButton;
  public Button cancelButton;
  public Button closeButton;
  public UIGridList grid;
  public GameObject knowledgePrefab;
  public GameObject unlockPrefab;
  public GameObject knowledgeParent;
  public GameObject confirmBuild;
  public GameObject confirmUpgrade;
  public GameObject arrow;
  public TextMeshProUGUI headerLabel;
  public TextMeshProUGUI upgradeTimeLabel;
  public TextMeshProUGUI costLabel;
  public TextMeshProUGUI upgradeTimeBuild;
  public TextMeshProUGUI upgradeTimeUpgrade;
  public UIHQUpgradeBuildingEntry currentBuilding;
  public UIHQUpgradeBuildingEntry upgradeBuilding;
  private HQsBuilding_v1 mBuilding;

  private void Awake()
  {
    this.confirmButton.onClick.AddListener(new UnityAction(this.OnConfirm));
    this.cancelButton.onClick.AddListener(new UnityAction(this.OnClose));
    this.closeButton.onClick.AddListener(new UnityAction(this.OnClose));
  }

  public void Setup(HQsBuilding_v1 inBuilding)
  {
    this.mBuilding = inBuilding;
    StringVariableParser.building = this.mBuilding;
    this.headerLabel.text = this.mBuilding.isBuilt ? Localisation.LocaliseID("PSG_10010033", (GameObject) null) : Localisation.LocaliseID("PSG_10010032", (GameObject) null);
    this.upgradeTimeLabel.text = this.mBuilding.isBuilt ? Localisation.LocaliseID("PSG_10010036", (GameObject) null) : Localisation.LocaliseID("PSG_10010035", (GameObject) null);
    this.costLabel.text = GameUtility.GetCurrencyString(this.mBuilding.isBuilt ? (long) this.mBuilding.costs.GetUpgradeCost() : (long) this.mBuilding.costs.GetBuildTotalCost(), 0);
    GameUtility.SetActive(this.upgradeTimeBuild.gameObject, !this.mBuilding.isBuilt);
    GameUtility.SetActive(this.upgradeTimeUpgrade.gameObject, this.mBuilding.isBuilt);
    GameUtility.SetActive(this.upgradeBuilding.gameObject, this.mBuilding.isBuilt);
    GameUtility.SetActive(this.arrow, this.mBuilding.isBuilt);
    this.currentBuilding.Setup(this.mBuilding, this.mBuilding.isBuilt);
    if (this.upgradeBuilding.gameObject.activeSelf)
      this.upgradeBuilding.Setup(this.mBuilding, false);
    GameUtility.SetActive(this.confirmBuild, !this.mBuilding.isBuilt);
    GameUtility.SetActive(this.confirmUpgrade, this.mBuilding.isBuilt);
    this.SetKnowledge();
  }

  private void SetKnowledge()
  {
    this.grid.DestroyListItems();
    List<PartTypeSlotSettings> typeSlotSettingsList = new List<PartTypeSlotSettings>();
    this.grid.itemPrefab = this.knowledgePrefab;
    GameUtility.SetActive(this.grid.itemPrefab, true);
    if (this.mBuilding.info.sets[Game.instance.player.team.championship.championshipID].ContainsKey(this.mBuilding.nextLevel))
      typeSlotSettingsList = this.mBuilding.info.sets[Game.instance.player.team.championship.championshipID][this.mBuilding.nextLevel];
    int num = 0;
    int count = typeSlotSettingsList.Count;
    for (int index = 0; index < count; ++index)
    {
      this.grid.CreateListItem<UIHQKnowledgeEntry>().SetupKnowledge(typeSlotSettingsList[index], true, this.mBuilding);
      ++num;
    }
    if (this.mBuilding.info.effects != null && !this.mBuilding.isMaxLevel)
    {
      for (int inEffectIndex = 0; inEffectIndex < this.mBuilding.info.effects.Length; ++inEffectIndex)
      {
        this.grid.CreateListItem<UIHQKnowledgeEntry>().SetupEffect(this.mBuilding, inEffectIndex, true);
        ++num;
      }
    }
    GameUtility.SetActive(this.grid.itemPrefab, false);
    this.grid.itemPrefab = this.unlockPrefab;
    GameUtility.SetActive(this.grid.itemPrefab, true);
    HQsBuilding_v1[] buildingsCanUnlock = this.mBuilding.team.headquarters.GetBuildingsCanUnlock(this.mBuilding.info.type, this.mBuilding.nextLevel);
    int length = buildingsCanUnlock.Length;
    for (int index = 0; index < length; ++index)
    {
      this.grid.CreateListItem<UIHQUnlockEntry>().Setup(buildingsCanUnlock[index], this.mBuilding.info.type, this.mBuilding.nextLevel);
      ++num;
    }
    GameUtility.SetActive(this.grid.itemPrefab, false);
    GameUtility.SetActive(this.knowledgeParent, num > 0);
  }

  private List<PartTypeSlotSettings> GetSettings(CarPart.PartType[] inTypes)
  {
    List<PartTypeSlotSettings> typeSlotSettingsList = new List<PartTypeSlotSettings>();
    if (inTypes != null)
    {
      for (int index = 0; index < inTypes.Length; ++index)
        typeSlotSettingsList.Add(Game.instance.partSettingsManager.championshipPartSettings[Game.instance.player.team.championship.championshipID][inTypes[index]]);
    }
    return typeSlotSettingsList;
  }

  public void Hide()
  {
    HQUpgradePopup.Close();
  }

  private void OnConfirm()
  {
    this.Hide();
    if (this.mBuilding == null || this.mBuilding.isLeveling)
      return;
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    UIHQUpgradePopup.\u003COnConfirm\u003Ec__AnonStorey82 confirmCAnonStorey82 = new UIHQUpgradePopup.\u003COnConfirm\u003Ec__AnonStorey82();
    // ISSUE: reference to a compiler-generated field
    confirmCAnonStorey82.\u003C\u003Ef__this = this;
    // ISSUE: reference to a compiler-generated field
    confirmCAnonStorey82.screen = UIManager.instance.GetScreen<HeadquartersScreen>();
    // ISSUE: reference to a compiler-generated method
    Action inActionSuccess = new Action(confirmCAnonStorey82.\u003C\u003Em__17C);
    // ISSUE: reference to a compiler-generated field
    confirmCAnonStorey82.screen.ProcessTransaction(this.mBuilding, inActionSuccess);
  }

  private void OnClose()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.Hide();
  }
}
