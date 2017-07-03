// Decompiled with JetBrains decompiler
// Type: ChooseTeamStats
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class ChooseTeamStats : MonoBehaviour
{
  public UITeamStatsBarEntry carStat;
  public UITeamStatsBarEntry driversStat;
  public UITeamStatsBarEntry headquartersStat;
  public UITeamStatsBarEntry staffStat;
  public UITeamStatsBarEntry sponsorsStat;

  public void Setup(Team inTeam)
  {
    this.carStat.Setup(inTeam);
    this.driversStat.Setup(inTeam);
    this.headquartersStat.Setup(inTeam);
    this.staffStat.Setup(inTeam);
    this.sponsorsStat.Setup(inTeam);
  }
}
