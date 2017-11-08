using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class DriverManager : PersonManager<Driver>
{
    public NullDriver nullDriver = new NullDriver();
    public DriverStatsProgression ageDriverStatProgression;
    public DriverStatsProgression maxDriverStatProgressionPerDay;
    public DriverStatsProgression raceDriverStatProgression;
    public DriverStatsProgression qualifyingDriverStatProgression;
    public DriverStatsProgression practiceDriverStatProgression;
    private static IEnumerable<Driver> mDriversCache;

    public void AddDriverToChampionship(Driver inDriver)
    {
        if (!inDriver.IsMainDriver())
            return;
        Championship championship = inDriver.Contract.GetTeam().championship;
        ChampionshipEntry_v1 championshipEntry = inDriver.GetChampionshipEntry();
        bool flag = championshipEntry != null && championship != null && championship.standings.isEntryInactive(championshipEntry);
        if (championshipEntry != null && championshipEntry.championship == championship && !flag)
            return;
        championship.standings.AddEntry((Entity)inDriver, championship);
    }

    public void AddDriverToChampionship(Driver inDriver, bool addRegardless = false)
    {
        Championship championship = inDriver.contract.GetTeam().championship;
        if (!inDriver.IsMainDriver() && !addRegardless && championship.series != Championship.Series.EnduranceSeries)
            return;
        ChampionshipEntry_v1 championshipEntry = inDriver.GetChampionshipEntry();
        bool flag = championshipEntry != null && championship != null && championship.standings.isEntryInactive(championshipEntry);
        if (championshipEntry != null && championshipEntry.championship == championship && !flag)
            return;
        championship.standings.AddEntry((Entity)inDriver, championship);
    }
}
