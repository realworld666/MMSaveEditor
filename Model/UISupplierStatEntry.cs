// Decompiled with JetBrains decompiler
// Type: UISupplierStatEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;

public class UISupplierStatEntry : MonoBehaviour
{
  private CarChassisStats.Stats mStat = CarChassisStats.Stats.Count;
  public TextMeshProUGUI statName;
  public TextMeshProUGUI statValue;
  public Transform icon;

  public void Setup(CarChassisStats.Stats inStat, float inValue)
  {
    this.mStat = inStat;
    this.statName.text = Localisation.LocaliseEnum((Enum) this.mStat);
    this.SetStatString((float) ((double) inValue / (double) GameStatsConstants.chassisSupplierStatMax * 5.0));
    this.SetIcon(this.mStat);
  }

  private void SetIcon(CarChassisStats.Stats inStat)
  {
    for (int index = 0; index < this.icon.childCount; ++index)
    {
      if ((CarChassisStats.Stats) index == inStat)
        this.icon.GetChild(index).gameObject.SetActive(true);
      else
        this.icon.GetChild(index).gameObject.SetActive(false);
    }
  }

  private void SetStatString(float inValue)
  {
    if ((double) inValue > 4.0)
    {
      this.statValue.color = UIConstants.positiveColor;
      this.statValue.text = "VERY STRONG";
    }
    else if ((double) inValue > 3.0)
    {
      this.statValue.color = UIConstants.positiveColor;
      this.statValue.text = "STRONG";
    }
    else if ((double) inValue > 2.0)
    {
      this.statValue.color = UIConstants.mailMedia;
      this.statValue.text = "MEDIUM";
    }
    else if ((double) inValue > 1.0)
    {
      this.statValue.color = UIConstants.negativeColor;
      this.statValue.text = "WEAK";
    }
    else
    {
      this.statValue.color = UIConstants.negativeColor;
      this.statValue.text = "VERY WEAK";
    }
  }
}
