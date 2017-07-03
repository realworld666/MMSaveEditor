// Decompiled with JetBrains decompiler
// Type: DriverStatsSimulationScenario
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

public class DriverStatsSimulationScenario
{
  public List<DriverStatsSimulationScenarioEntry> mScenarioEntries;
  public int mDurationMonths;

  public DriverStatsSimulationScenario(int inDurationMonths, params DriverStatsSimulationScenarioEntry[] inScenarioEntries)
  {
    this.mDurationMonths = inDurationMonths;
    this.mScenarioEntries = new List<DriverStatsSimulationScenarioEntry>();
    this.mScenarioEntries.AddRange((IEnumerable<DriverStatsSimulationScenarioEntry>) inScenarioEntries);
  }
}
