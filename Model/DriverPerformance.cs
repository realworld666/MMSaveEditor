
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class DriverPerformance : PerformanceImpact
{
    private EnduranceDriverSetupPreferences mDriverSetupPreferences;
    private DriverStats mStatsPartBonuses;
}
