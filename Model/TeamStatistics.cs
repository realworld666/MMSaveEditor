using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class TeamStatistics
{
    private Team mTeam;


    public enum Stat
    {
        [LocalisationID("PSG_10004390")] Car,
        [LocalisationID("PSG_10000002")] Drivers,
        [LocalisationID("PSG_10002250")] Headquarters,
        [LocalisationID("PSG_10003781")] Staff,
        [LocalisationID("PSG_10001460")] Finances,
        [LocalisationID("PSG_10003782")] Sponsors,
    }
}
