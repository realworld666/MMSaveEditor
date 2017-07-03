// Decompiled with JetBrains decompiler
// Type: UIHQUpgradeBuildingEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UIHQUpgradeBuildingEntry : MonoBehaviour
{
  public TextMeshProUGUI nameLabel;
  public TextMeshProUGUI levelLabel;
  public TextMeshProUGUI perRaceCostLabel;
  public GameObject perRaceCost;
  public GameObject perRaceIncome;
  public UIBuilding buildingIcon;
  public UIHQGroupIcon groupIcon;
  private HQsBuilding_v1 mBuilding;

  public void Setup(HQsBuilding_v1 inBuilding, bool inCurrentLevel)
  {
    this.mBuilding = inBuilding;
    this.nameLabel.text = this.mBuilding.buildingName;
    StringVariableParser.building = this.mBuilding;
    this.levelLabel.text = !inCurrentLevel ? Localisation.LocaliseID("PSG_10010034", (GameObject) null) : Localisation.LocaliseID("PSG_10010003", (GameObject) null);
    long inValue = !inCurrentLevel ? (long) this.mBuilding.costs.GetTotalCostLevel(this.mBuilding.nextLevel, HQsBuildingCosts.TimePeriod.PerRace, true) : (long) this.mBuilding.costs.GetTotalCost(HQsBuildingCosts.TimePeriod.PerRace, true);
    GameUtility.SetActive(this.perRaceCost, inValue < 0L);
    GameUtility.SetActive(this.perRaceIncome, inValue >= 0L);
    this.perRaceCostLabel.text = GameUtility.GetCurrencyString(inValue, 0);
    this.perRaceCostLabel.color = inValue < 0L ? UIConstants.negativeColor : UIConstants.positiveColor;
    this.buildingIcon.SetBuilding(this.mBuilding, !inCurrentLevel ? this.mBuilding.nextLevel : this.mBuilding.currentLevel);
    this.groupIcon.SetIcon(this.mBuilding);
  }
}
