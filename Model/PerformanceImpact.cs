
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class PerformanceImpact
{
    private bool mIsActive = true;
    protected RacingVehicle mVehicle;
    protected DesignData mDesignData;
    protected CarPerformanceDesignData mCarPerformance;
    private float mTimeCost;
    private bool mIsExperiencingCriticalIssue;

}
