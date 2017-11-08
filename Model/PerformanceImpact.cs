
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class PerformanceImpact
{
    protected RacingVehicle mVehicle;
    protected DesignData mDesignData;
    protected CarPerformanceDesignData mCarPerformance;
    private bool mIsActive = true;
    private float mTimeCost;
    private bool mIsExperiencingCriticalIssue;

}
