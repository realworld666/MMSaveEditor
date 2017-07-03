using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class TeamAIWeightings
{
    public int mTeamIndex;
    public float mHQDesignWeight;
    public float mHQFactoryWeight;
    public float mHQPerformanceWeight;
    public float mHQStaffWeight;
    public float mHQBrandWeight;
    public float mCarAcceleration;
    public float mCarBraking;
    public float mCarTopSpeed;
    public float mCarLowSpeedCornering;
    public float mCarMediumSpeedCornering;
    public float mCarHighSpeedCornering;
    public float mFinanceCar;
    public float mFinanceHQ;
    public float mFinanceDrivers;
    public float mAggressiveness;
    public float mStyle;
    public float mStaffRetention;

}
