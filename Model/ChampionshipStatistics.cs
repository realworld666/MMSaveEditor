// Decompiled with JetBrains decompiler
// Type: ChampionshipStatistics
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class ChampionshipStatistics
{
  private static List<Team> mTeams = new List<Team>();

  public static ChampionshipStatistics.StatisticsReport GenerateReport(Team inTeam, TeamStatistics.Stat inStat)
  {
    ChampionshipStatistics.StatisticsReport statisticsReport = new ChampionshipStatistics.StatisticsReport();
    Championship championship = inTeam.championship;
    ChampionshipStatistics.mTeams.Clear();
    int teamEntryCount = championship.standings.teamEntryCount;
    for (int inIndex = 0; inIndex < teamEntryCount; ++inIndex)
      ChampionshipStatistics.mTeams.Add(championship.standings.GetTeamEntry(inIndex).GetEntity<Team>());
    TeamStatistics.SortListByStat(ref ChampionshipStatistics.mTeams, inStat);
    statisticsReport.stat = inStat;
    statisticsReport.championship = inTeam.championship;
    statisticsReport.team = inTeam;
    statisticsReport.bestTeam = ChampionshipStatistics.mTeams[0];
    statisticsReport.worstTeam = ChampionshipStatistics.mTeams[ChampionshipStatistics.mTeams.Count - 1];
    float stat = statisticsReport.worstTeam.teamStatistics.GetStat(inStat);
    float a = statisticsReport.bestTeam.teamStatistics.GetStat(inStat) - stat;
    float num1 = 0.0f;
    for (int index = 0; index < teamEntryCount; ++index)
    {
      Team mTeam = ChampionshipStatistics.mTeams[index];
      float num2 = mTeam.teamStatistics.GetStat(inStat) - stat;
      num1 += num2;
      if (mTeam == inTeam)
      {
        statisticsReport.teamPosition = index + 1;
        statisticsReport.statNormalized = !MathsUtility.ApproximatelyZero(a) ? Mathf.Clamp01(num2 / a) : 0.0f;
      }
      statisticsReport.teamsOrdered.Add(mTeam);
    }
    statisticsReport.averageNormalized = !MathsUtility.ApproximatelyZero(a) ? Mathf.Clamp01(num1 / (float) teamEntryCount / a) : 0.0f;
    ChampionshipStatistics.mTeams.Clear();
    return statisticsReport;
  }

  public static ChampionshipStatistics.StatisticsReport GenerateReport(Team inTeam, HQsBuildingInfo.Category inCategory)
  {
    ChampionshipStatistics.StatisticsReport statisticsReport = new ChampionshipStatistics.StatisticsReport();
    Championship championship = inTeam.championship;
    ChampionshipStatistics.mTeams.Clear();
    int teamEntryCount = championship.standings.teamEntryCount;
    for (int inIndex = 0; inIndex < teamEntryCount; ++inIndex)
      ChampionshipStatistics.mTeams.Add(championship.standings.GetTeamEntry(inIndex).GetEntity<Team>());
    TeamStatistics.SortListByStat(ref ChampionshipStatistics.mTeams, inCategory);
    statisticsReport.category = inCategory;
    statisticsReport.championship = inTeam.championship;
    statisticsReport.team = inTeam;
    statisticsReport.bestTeam = ChampionshipStatistics.mTeams[0];
    statisticsReport.worstTeam = ChampionshipStatistics.mTeams[ChampionshipStatistics.mTeams.Count - 1];
    float normalizedRating = statisticsReport.worstTeam.headquarters.GetNormalizedRating(inCategory);
    float a = statisticsReport.bestTeam.headquarters.GetNormalizedRating(inCategory) - normalizedRating;
    float num1 = 0.0f;
    for (int index = 0; index < teamEntryCount; ++index)
    {
      Team mTeam = ChampionshipStatistics.mTeams[index];
      float num2 = mTeam.headquarters.GetNormalizedRating(inCategory) - normalizedRating;
      num1 += num2;
      if (mTeam == inTeam)
      {
        statisticsReport.teamPosition = index + 1;
        statisticsReport.statNormalized = !MathsUtility.ApproximatelyZero(a) ? Mathf.Clamp01(num2 / a) : 0.0f;
      }
      statisticsReport.teamsOrdered.Add(mTeam);
    }
    statisticsReport.averageNormalized = !MathsUtility.ApproximatelyZero(a) ? Mathf.Clamp01(num1 / (float) teamEntryCount / a) : 0.0f;
    ChampionshipStatistics.mTeams.Clear();
    return statisticsReport;
  }

  public class StatisticsReport
  {
    public HQsBuildingInfo.Category category = HQsBuildingInfo.Category.Brand;
    public CarStats.StatType carStat = CarStats.StatType.Acceleration;
    public List<Team> teamsOrdered = new List<Team>();
    public TeamStatistics.Stat stat;
    public Championship championship;
    public Team team;
    public Team bestTeam;
    public Team worstTeam;
    public Driver bestDriver;
    public float statNormalized;
    public float averageNormalized;
    public int teamPosition;
  }
}
