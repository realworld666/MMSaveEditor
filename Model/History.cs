
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class History
{
    public int races;
    public int wins;
    public int podiums;
    public int poles;
    public int DNFs;
    public int DNFsViaError;
    public int DNS;
    public int careerPoints;
    public int championships;
    public int previousSeasonTeamResult;

    public void IncreaseStat(History.HistoryStat inHistoryStat, int inIncrease = 1)
    {
        switch (inHistoryStat)
        {
            case History.HistoryStat.Races:
                this.races += inIncrease;
                break;
            case History.HistoryStat.Wins:
                this.wins += inIncrease;
                break;
            case History.HistoryStat.Podiums:
                this.podiums += inIncrease;
                break;
            case History.HistoryStat.Poles:
                this.poles += inIncrease;
                break;
            case History.HistoryStat.Dnfs:
                this.DNFs += inIncrease;
                break;
            case History.HistoryStat.DnfsViaError:
                this.DNFsViaError += inIncrease;
                break;
            case History.HistoryStat.Dns:
                this.DNS += inIncrease;
                break;
            case History.HistoryStat.CareerPoints:
                this.careerPoints += inIncrease;
                break;
            case History.HistoryStat.Championships:
                this.championships += inIncrease;
                break;
        }
    }

    public bool HasPreviousSeasonHistory()
    {
        return this.previousSeasonTeamResult != 0;
    }

    public enum HistoryStat
    {
        Races,
        Wins,
        Podiums,
        Poles,
        Dnfs,
        DnfsViaError,
        Dns,
        CareerPoints,
        Championships,
        OverallPreviousSeasonTeamResult,
    }
}
