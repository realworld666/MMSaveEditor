using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class DriverStats : PersonStats
{
    public int totalStatsMax = 180;
    public int fame = -1;
    public float mBrakingImprovementRate = 1f;
    public float mCorneringImprovementRate = 1f;
    public float mSmoothnessImprovementRate = 1f;
    public float mOvertakingImprovementRate = 1f;
    public float mConsistencyImprovementRate = 1f;
    public float mAdaptabilityImprovementRate = 1f;
    public float mFitnessImprovementRate = 1f;
    public float mFeedbackImprovementRate = 1f;
    public float mFocusImprovementRate = 1f;
    public const int driverStatsMax = 20;
    public const int driverStatsNum = 9;
    public const int driverStatsTotalMax = 180;
    private const float driverStatImprovementRateMin = 0.75f;
    private const float driverStatImprovementRateMax = 1.15f;
    public float braking;
    public float cornering;
    public float smoothness;
    public float overtaking;
    public float consistency;
    public float adaptability;
    public float fitness;
    public float feedback;
    public float focus;
    public float balance;
    public float experience;
    public float marketability;
    public int favouriteBrakesSupplier;
    public int scoutingLevelRequired;
    private int mLowRangeBraking;
    private int mHighRangeBraking;
    private int mLowRangeCornering;
    private int mHighRangeCornering;
    private int mLowRangeSmoothness;
    private int mHighRangeSmoothness;
    private int mLowRangeOvertaking;
    private int mHighRangeOvertaking;
    private int mLowRangeConsistency;
    private int mHighRangeConsistency;
    private int mLowRangeAdaptability;
    private int mHighRangeAdaptability;
    private int mLowRangeFitness;
    private int mHighRangeFitness;
    private int mLowRangeFeedback;
    private int mHighRangeFeedback;
    private int mLowRangeFocus;
    private int mHighRangeFocus;
}
