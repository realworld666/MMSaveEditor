
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class RaceDirector
{
    private CarStats mHighestCarStats = new CarStats();
    private CarStats mLowestCarStats = new CarStats();
    private CarStats mAverageCarStats = new CarStats();
    private bool mOvertakingEnabled = true;
    private SprinklersDirector mSprinklersDirector = new SprinklersDirector();
    private PenaltyDirector mPenaltyDirector = new PenaltyDirector();
    private CrashDirector mCrashDirector = new CrashDirector();
    private RetireDirector mRetireDirector = new RetireDirector();
    private SpinOutDirector mSpinOutDirector = new SpinOutDirector();
    private TyreLockUpDirector mTyreLockUpDirector = new TyreLockUpDirector();
    private RetirementDirector mRetirementDirector = new RetirementDirector();
    private TyreConfiscationDirector mTyreConfiscationDirector = new TyreConfiscationDirector();
    private DriverStats mHighestDriverStats;
    private DriverStats mLowestDriverStats;
    private float mYellowFlagDuration;
    private SessionManager mSessionManager;
    private RunningWideDirector mRunningWideDirector;
    private CuttingCornersDirector mCutCornerDirector;

}
