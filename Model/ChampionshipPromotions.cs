using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ChampionshipPromotions
{
  public Team champion;
  public Team lastPlace;
  public CarStats championPartRankings;
  public CarStats lastPlacePartRankings;
  public ChampionshipPromotions.Status championStatus;
  public ChampionshipPromotions.Status lastPlaceStatus;

  public enum Status
  {
    Waiting,
    Promoted,
    Relegated,
    RefusedPromotion,
    SavedFromRelegation,
  }
}
