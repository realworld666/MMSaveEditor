// Decompiled with JetBrains decompiler
// Type: UICarPartSpecEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICarPartSpecEntry : MonoBehaviour
{
  public CarPart.PartType partType = CarPart.PartType.None;
  public TextMeshProUGUI partTypeNameText;
  public TextMeshProUGUI partStatNameText;
  public TextMeshProUGUI nextRaceRelevancy;
  public Transform partIconParent;
  public Image currentPartPerformance;
  public Image currentPartMaxPerformance;
  public UIPartConditionBar currentPartConditonBar;
  public Image currentPartBacking;
  public TextMeshProUGUI currentPartLevel;

  public void Setup()
  {
    this.partTypeNameText.text = Localisation.LocaliseEnum((Enum) this.partType);
    this.partStatNameText.text = Localisation.LocaliseEnum((Enum) CarPart.GetStatForPartType(this.partType));
    this.SetIcon(this.partType);
    this.SetBestPartData();
  }

  private void SetBestPartData()
  {
    CarPart highestLevel = Game.instance.player.team.carManager.partInventory.GetHighestLevel(this.partType, true);
    Color partLevelColor = UIConstants.GetPartLevelColor(highestLevel.stats.level);
    this.currentPartPerformance.fillAmount = CarPartStats.GetNormalizedStatValue(highestLevel.stats.statWithPerformance, highestLevel.GetPartType());
    this.currentPartPerformance.color = partLevelColor;
    this.currentPartMaxPerformance.fillAmount = CarPartStats.GetNormalizedStatValue(highestLevel.stats.stat + highestLevel.stats.maxPerformance, highestLevel.GetPartType());
    this.currentPartMaxPerformance.color = partLevelColor;
    this.currentPartConditonBar.Setup(highestLevel);
    int level = highestLevel.stats.level;
    this.currentPartLevel.text = CarPart.GetLevelUIString(level);
    this.currentPartBacking.color = UIConstants.GetPartLevelColor(level);
    Championship championship = Game.instance.player.team.championship;
    CarStats.RelevantToCircuit relevancy = CarStats.GetRelevancy(Mathf.RoundToInt(championship.calendar[championship.eventNumber].circuit.trackStatsCharacteristics.GetStat(CarPart.GetStatForPartType(this.partType))));
    this.nextRaceRelevancy.transform.parent.gameObject.SetActive(relevancy != CarStats.RelevantToCircuit.No);
    this.nextRaceRelevancy.text = Localisation.LocaliseEnum((Enum) relevancy);
    this.nextRaceRelevancy.color = CarStats.GetColorForCircuitRelevancy(relevancy);
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
