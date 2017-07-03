// Decompiled with JetBrains decompiler
// Type: UIPartStatsBarsEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPartStatsBarsEntry : MonoBehaviour
{
  private CarStats.StatType mStat = CarStats.StatType.Count;
  public TextMeshProUGUI statName;
  public TextMeshProUGUI relevantForCircuit;
  public TextMeshProUGUI bestStatValue;
  public Slider minStatValue;
  public Slider maxStatValue;
  public Slider highestLevelPartValue;
  public UICarStatIcon icon;

  public CarStats.StatType stat
  {
    get
    {
      return this.mStat;
    }
  }

  public void Setup(CarStats.StatType inStat, float inMinNormalizedValue, float inMaxNormalizedValue)
  {
    PartDesignScreen screen = UIManager.instance.GetScreen<PartDesignScreen>();
    float highestStatOfType = Game.instance.player.team.carManager.partInventory.GetHighestStatOfType(screen.partType, CarPartStats.CarPartStat.MainStat);
    this.highestLevelPartValue.value = CarPartStats.GetNormalizedStatValue(highestStatOfType, screen.partType);
    this.bestStatValue.text = highestStatOfType.ToString("0", (IFormatProvider) Localisation.numberFormatter);
    this.minStatValue.value = inMinNormalizedValue;
    this.maxStatValue.value = inMaxNormalizedValue;
    if (this.mStat == inStat)
      return;
    this.mStat = inStat;
    this.icon.SetIcon(this.mStat, Game.instance.player.team.championship.series);
    this.statName.text = Localisation.LocaliseEnum((Enum) (CarStats.CarStatShortName) this.mStat);
  }

  public void SetCircuitOptions(Circuit inCircuit)
  {
    if (inCircuit == null)
      return;
    CarStats.RelevantToCircuit inRelevancy = (CarStats.RelevantToCircuit) Mathf.RoundToInt(inCircuit.trackStatsCharacteristics.GetStat(this.mStat) - 1f);
    this.relevantForCircuit.text = Localisation.LocaliseEnum((Enum) inRelevancy);
    this.relevantForCircuit.color = CarStats.GetColorForCircuitRelevancy(inRelevancy);
  }
}
