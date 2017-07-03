
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ChampionshipStandingsHistoryEntry
{
    public int year;
    public int prizeFund;
    public int tvAudience;
    public ChampionshipStandings standings;
}
