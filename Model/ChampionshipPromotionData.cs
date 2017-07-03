using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ChampionshipPromotionData
{
  public Team team;
  public ChampionshipPromotionData.TeamStatus teamStatus;
  public Championship previousChampionship;
  public CarStats teamPreviousChampPartStatRankings;
  public bool carStatsDataUsed;

  public ChampionshipPromotionData(Team inTeam, Championship inPreviousChampionship, ChampionshipPromotionData.TeamStatus inStatus, CarStats inStats)
  {
    this.team = inTeam;
    this.teamStatus = inStatus;
    this.previousChampionship = inPreviousChampionship;
    this.teamPreviousChampPartStatRankings = inStats;
  }

  public enum TeamStatus
  {
    Promoted,
    Relegated,
  }
}
