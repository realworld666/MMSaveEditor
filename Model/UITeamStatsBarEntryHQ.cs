// Decompiled with JetBrains decompiler
// Type: UITeamStatsBarEntryHQ
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class UITeamStatsBarEntryHQ : UITeamStatsBarEntry
{
  public HQsBuildingInfo.Category category = HQsBuildingInfo.Category.Brand;

  public override void Setup(Team inTeam)
  {
    this.mReport = ChampionshipStatistics.GenerateReport(inTeam, this.category);
    this.SetStat(this.mReport.averageNormalized, this.mReport.statNormalized);
  }

  public override void ShowRollover()
  {
    if (this.mReport == null)
      return;
    TeamStatsRollover.ShowRolloverHQ(this.mReport, this.flipRollover);
  }

  public override void HideRollover()
  {
    TeamStatsRollover.HideRollover();
  }
}
