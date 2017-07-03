// Decompiled with JetBrains decompiler
// Type: DriverStatsSimulationScenarioEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class DriverStatsSimulationScenarioEntry
{
  public string mType;
  public int mFrequencyDays;
  public float mAmount;
  public int mNumPerYear;
  public bool mAllowDecline;
  public int mLastDayProcessed;
  public int mCountPerYear;

  public DriverStatsSimulationScenarioEntry(string inType, int inFrequencyDays, float inAmount, int inNumPerYear, bool inAllowDecline = false)
  {
    this.mType = inType;
    this.mFrequencyDays = inFrequencyDays;
    this.mAmount = inAmount;
    this.mNumPerYear = inNumPerYear;
    this.mAllowDecline = inAllowDecline;
  }
}
