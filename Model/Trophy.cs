
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class Trophy
{
    public Championship championship;
    public Team team;
    public int yearWon;
    public int pointsTotal;
    public long prizeMoney;

    public void Setup(Team inTeam, Championship inChampionship, int inYearWon, int inPointsTotal, long inPrizeMoney)
    {
        this.team = inTeam;
        this.championship = inChampionship;
        this.yearWon = inYearWon;
        this.pointsTotal = inPointsTotal;
        this.prizeMoney = inPrizeMoney;
    }
}
