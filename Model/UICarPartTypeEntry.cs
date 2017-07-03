// Decompiled with JetBrains decompiler
// Type: UICarPartTypeEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UICarPartTypeEntry : MonoBehaviour
{
  public CarPart.PartType partType = CarPart.PartType.None;
  private string mTimeTooltipText = string.Empty;
  public TextMeshProUGUI partTypeNameText;
  public TextMeshProUGUI partStatNameText;
  public TextMeshProUGUI nextRaceRelevancy;
  public Transform partIconParent;
  public Image currentPartPerformance;
  public Image currentPartMaxPerformance;
  public UIPartConditionBar currentPartConditonBar;
  public Image currentPartBacking;
  public TextMeshProUGUI currentPartLevel;
  public Transform currentSlotsParent;
  public Image availablePartBacking;
  public TextMeshProUGUI availablePartLevel;
  public Toggle toggle;
  public TextMeshProUGUI baseCost;
  public TextMeshProUGUI baseTime;
  public TextMeshProUGUI componentsVailable;
  public GameObject timeTooltip;
  private PartDesignScreen mScreen;

  private void Awake()
  {
    this.toggle.onValueChanged.AddListener((UnityAction<bool>) (value => this.OnValueChanged(value)));
  }

  private void OnValueChanged(bool inValue)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (!inValue)
      return;
    this.mScreen.SetPartType(this.partType);
  }

  private void OnEnable()
  {
    this.toggle.isOn = false;
  }

  public void Setup()
  {
    if ((UnityEngine.Object) this.mScreen == (UnityEngine.Object) null)
      this.mScreen = UIManager.instance.GetScreen<PartDesignScreen>();
    PartTypeSlotSettings typeSlotSettings = Game.instance.partSettingsManager.championshipPartSettings[Game.instance.player.team.championship.championshipID][this.partType];
    this.partTypeNameText.text = Localisation.LocaliseEnum((Enum) this.partType);
    this.partStatNameText.text = Localisation.LocaliseEnum((Enum) CarPart.GetStatForPartType(this.partType));
    float num = typeSlotSettings.buildTimeDays - (float) Game.instance.player.designPartTimeModifier.Days;
    HQsBuilding_v1 building = Game.instance.player.team.headquarters.GetBuilding(HQsBuildingInfo.Type.DesignCentre);
    if (building != null && building.isBuilt)
      num += HQsBuilding_v1.designCentrePartDaysPerLevel[building.currentLevel];
    StringVariableParser.ordinalNumberString = num.ToString("0.#");
    this.baseTime.text = Localisation.LocaliseID("PSG_10010377", (GameObject) null);
    int days = (Game.instance.player.team.championship.GetCurrentEventDetails().eventDate - Game.instance.time.now).Days;
    StringVariableParser.projectedPartDesignDays = (int) ((double) typeSlotSettings.buildTimeDays - (double) days);
    StringVariableParser.partFrontendUI = this.partType;
    this.mTimeTooltipText = StringVariableParser.projectedPartDesignDays != 1 ? Localisation.LocaliseID("PSG_10005744", (GameObject) null) : Localisation.LocaliseID("PSG_10005743", (GameObject) null);
    if (days < (int) typeSlotSettings.buildTimeDays)
    {
      this.baseTime.color = UIConstants.negativeColor;
      this.timeTooltip.SetActive(true);
    }
    else
    {
      this.baseTime.color = Color.white;
      this.timeTooltip.SetActive(false);
    }
    this.baseCost.text = GameUtility.GetCurrencyString((long) (int) typeSlotSettings.materialsCost, 0);
    this.SetIcon(this.partType);
    this.SetBestPartData();
  }

  private void SetBestPartData()
  {
    CarPartDesign carPartDesign = Game.instance.player.team.carManager.carPartDesign;
    CarPart highestLevel = Game.instance.player.team.carManager.partInventory.GetHighestLevel(this.partType, true);
    Color partLevelColor = UIConstants.GetPartLevelColor(highestLevel.stats.level);
    this.currentPartPerformance.fillAmount = CarPartStats.GetNormalizedStatValue(highestLevel.stats.statWithPerformance, highestLevel.GetPartType());
    this.currentPartPerformance.color = partLevelColor;
    this.currentPartMaxPerformance.fillAmount = CarPartStats.GetNormalizedStatValue(highestLevel.stats.stat + highestLevel.stats.maxPerformance, highestLevel.GetPartType());
    this.currentPartMaxPerformance.color = partLevelColor;
    this.currentPartConditonBar.Setup(highestLevel);
    int level = highestLevel.stats.level;
    int inLevel = Mathf.Clamp(level + 1, 1, GameStatsConstants.slotCount);
    this.currentPartLevel.text = CarPart.GetLevelUIString(level);
    this.currentPartBacking.color = UIConstants.GetPartLevelColor(level);
    this.availablePartLevel.text = CarPart.GetLevelUIString(inLevel);
    this.availablePartBacking.color = UIConstants.GetPartLevelColor(inLevel);
    this.componentsVailable.text = carPartDesign.GetComponentsAvailableCount(this.partType, inLevel - 1).ToString() + "/" + (object) carPartDesign.GetComponentsCountForPartType(this.partType);
    Championship championship = Game.instance.player.team.championship;
    CarStats.RelevantToCircuit relevancy = CarStats.GetRelevancy(Mathf.RoundToInt(championship.calendar[championship.eventNumber].circuit.trackStatsCharacteristics.GetStat(CarPart.GetStatForPartType(this.partType))));
    this.nextRaceRelevancy.transform.parent.gameObject.SetActive(relevancy != CarStats.RelevantToCircuit.No);
    this.nextRaceRelevancy.text = Localisation.LocaliseEnum((Enum) relevancy);
    this.nextRaceRelevancy.color = CarStats.GetColorForCircuitRelevancy(relevancy);
  }

  public void ShowTimeTooltip()
  {
    UIManager.instance.dialogBoxManager.GetDialog<GenericInfoRollover>().Open(Localisation.LocaliseID("PSG_10005742", (GameObject) null), this.mTimeTooltipText);
  }

  public void HideTimeTooltip()
  {
    UIManager.instance.dialogBoxManager.GetDialog<GenericInfoRollover>().Hide();
  }

  public void SetIcon(CarPart.PartType inType)
  {
    for (int index = 0; index < this.partIconParent.childCount; ++index)
    {
      if ((CarPart.PartType) index == inType)
        this.partIconParent.GetChild(index).gameObject.SetActive(true);
      else
        this.partIconParent.GetChild(index).gameObject.SetActive(false);
    }
  }
}
