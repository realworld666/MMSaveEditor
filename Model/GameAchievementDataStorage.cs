
using FullSerializer;

namespace MM2
{
  [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
  public class GameAchievementDataStorage
  {
    public int racesWonInARow;
    public int qualifiedPoleInARow;
    public int qualifiedPolesThisSeason;
    public int championships_won;
    public bool hasGotSponsorBonusesThisSeason;
    public bool hasUpgradedCarPartThisSeason;
    public bool hasUpgradedHQThisSeason;
    public bool startedAtBottomOfT3;

   
  }
}
