// Decompiled with JetBrains decompiler
// Type: UIEstimatedOutputWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class UIEstimatedOutputWidget : MonoBehaviour
{
  public UIOutputStatBarEntry[] outputStatBars = new UIOutputStatBarEntry[CarChassisStats.carDesignScreenStats.Length];

  public void OnEnter()
  {
    for (int index = 0; index < CarChassisStats.carDesignScreenStats.Length; ++index)
      this.outputStatBars[index].Setup(CarChassisStats.carDesignScreenStats[index], 0.0f);
  }

  public void ResetStats()
  {
    for (int index = 0; index < this.outputStatBars.Length; ++index)
      this.outputStatBars[index].ResetStat();
  }

  public void AddToStat(CarChassisStats.Stats inStat, float inValue)
  {
    if (inStat >= (CarChassisStats.Stats) this.outputStatBars.Length)
      return;
    this.outputStatBars[(int) inStat].AddToStatValue(inValue);
  }

  public void HideSupplierContribution()
  {
    for (int index = 0; index < this.outputStatBars.Length; ++index)
      this.outputStatBars[index].SetSupplierContribution(0.0f);
  }

  public void SetSupplierContribution(CarChassisStats.Stats inStat, float inValue)
  {
    if (inStat >= (CarChassisStats.Stats) this.outputStatBars.Length)
      return;
    this.outputStatBars[(int) inStat].SetSupplierContribution(inValue);
  }

  public void SetMaxStat(CarChassisStats.Stats inStat, float inValue)
  {
    if (inStat >= (CarChassisStats.Stats) this.outputStatBars.Length)
      return;
    this.outputStatBars[(int) inStat].SetMaxStatValue(inValue);
  }

  public void ResetHighlightState()
  {
    for (int index = 0; index < this.outputStatBars.Length; ++index)
      this.outputStatBars[index].Highlight(true);
  }

  public void HighlightBarsForStats(List<CarChassisStats.Stats> inStats)
  {
    for (int index = 0; index < this.outputStatBars.Length; ++index)
    {
      if (inStats.Contains(this.outputStatBars[index].stat))
        this.outputStatBars[index].Highlight(true);
      else
        this.outputStatBars[index].Highlight(false);
    }
  }
}
