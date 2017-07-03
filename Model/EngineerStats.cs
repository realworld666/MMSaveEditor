using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class EngineerStats : PersonStats
{
    public CarStats partContributionStats = new CarStats();
    public int totalStatsMax = 120;
    public const int engineerStatsMax = 20;
    public const int engineerStatsNum = 6;
    public const int engineerStatsTotalMax = 120;


}
