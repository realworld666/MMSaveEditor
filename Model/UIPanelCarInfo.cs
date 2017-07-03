// Decompiled with JetBrains decompiler
// Type: UIPanelCarInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UIPanelCarInfo : MonoBehaviour
{
  public UIPanelCarInfoStatEntry[] boxedStats = new UIPanelCarInfoStatEntry[0];
  private Car mCar;

  public void Setup(Car inCar)
  {
    this.mCar = inCar;
    CarStats stats = this.mCar.GetStats();
    for (CarStats.StatType inStat = CarStats.StatType.TopSpeed; inStat <= CarStats.StatType.HighSpeedCorners; ++inStat)
      this.boxedStats[(int) inStat].Setup(inStat, stats.GetStat(inStat), this.mCar);
  }

  public void HideComparison()
  {
    this.Setup(this.mCar);
  }

  public void ShowComparison(CarPart inPart)
  {
    CarStats deltaWithNewPart = this.mCar.GetStatsDeltaWithNewPart(inPart);
    deltaWithNewPart.Add(this.mCar.GetStats());
    for (CarStats.StatType inStat = CarStats.StatType.TopSpeed; inStat <= CarStats.StatType.HighSpeedCorners; ++inStat)
      this.boxedStats[(int) inStat].ShowComparison(deltaWithNewPart.GetStat(inStat));
  }

  public void Refresh()
  {
    this.Setup(this.mCar);
  }
}
