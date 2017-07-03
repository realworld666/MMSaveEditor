// Decompiled with JetBrains decompiler
// Type: UITravelCircuitStatWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;

public class UITravelCircuitStatWidget : MonoBehaviour
{
  public Transform iconParent;
  public TextMeshProUGUI statType;
  public TextMeshProUGUI statRelevance;

  public void SetupCircuitStat(Circuit inCircuit, CarStats.StatType inStatType)
  {
    CarStats.RelevantToCircuit relevancy = CarStats.GetRelevancy(Mathf.RoundToInt(inCircuit.trackStatsCharacteristics.GetStat(inStatType)));
    CarPart.SetIcon(this.iconParent, CarPart.GetPartForStatType(inStatType, Game.instance.player.team.championship.series));
    this.statType.text = Localisation.LocaliseEnum((Enum) inStatType);
    this.statRelevance.text = Localisation.LocaliseEnum((Enum) relevancy);
    this.statRelevance.color = CarStats.GetColorForCircuitRelevancy(relevancy);
  }
}
