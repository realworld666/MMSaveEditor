public class SessionEvents
{
    private float mCrashPointsLimit = 1000f;
    private float mSpinOutPointsLimit = 1000f;
    private float mLockUpPointsLimit = 1000f;
    private float[] mPointsPerType = null;//new float[3, 7];
    private float mCrashPoints;
    private float mSpinOutPoints;
    private float mLockUpPoints;
    private RacingVehicle mVehicle;
    private int mPreviousGate;


    public enum EventType
    {
        Crash,
        SpinOut,
        LockUp,
        Count,
    }

    public enum PointsType
    {
        Weather,
        Tyre,
        DrivingStyle,
        DriverStats,
        EngineMode,
        AIBehaviourType,
        ClosestVehiclesPressure,
        Count,
    }
}
