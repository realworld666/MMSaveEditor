// Decompiled with JetBrains decompiler
// Type: UIStatBarEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatBarEntry : MonoBehaviour
{
  public Color highestStatColor = new Color();
  private Color mInitialStatBarColor = new Color();
  private CarStats.StatType mStat = CarStats.StatType.Acceleration;
  public TextMeshProUGUI statName;
  public Image statBar;
  private bool mSetInitialColors;

  private void SetInitialColors()
  {
    this.mInitialStatBarColor = this.statBar.color;
    this.mSetInitialColors = true;
  }

  public void Setup(CarStats.StatType inStat, float inStatValue)
  {
    if (!this.mSetInitialColors)
      this.SetInitialColors();
    this.mStat = inStat;
    this.statName.text = Localisation.LocaliseEnum((Enum) (CarStats.CarStatShortName) this.mStat);
    float num = inStatValue;
    float carStatValueOnGrid = Game.instance.player.team.carManager.GetCarStatValueOnGrid(this.mStat, CarManager.MedianTypes.Highest);
    if ((double) num == (double) carStatValueOnGrid)
      this.statBar.color = this.highestStatColor;
    else
      this.statBar.color = this.mInitialStatBarColor;
    this.statBar.fillAmount = num / carStatValueOnGrid;
  }
}
