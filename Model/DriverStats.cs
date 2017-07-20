using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class DriverStats : PersonStats
{
    public int totalStatsMax = 180;
    private int fame = -1;
    private float mBrakingImprovementRate = 1f;
    private float mCorneringImprovementRate = 1f;
    private float mSmoothnessImprovementRate = 1f;
    private float mOvertakingImprovementRate = 1f;
    private float mConsistencyImprovementRate = 1f;
    private float mAdaptabilityImprovementRate = 1f;
    private float mFitnessImprovementRate = 1f;
    private float mFeedbackImprovementRate = 1f;
    private float mFocusImprovementRate = 1f;
    public const int driverStatsMax = 20;
    public const int driverStatsNum = 9;
    public const int driverStatsTotalMax = 180;
    private const float driverStatImprovementRateMin = 0.75f;
    private const float driverStatImprovementRateMax = 1.15f;
    private float braking;
    private float cornering;
    private float smoothness;
    private float overtaking;
    private float consistency;
    private float adaptability;
    private float fitness;
    private float feedback;
    private float focus;
    private float balance;
    private float experience;
    private float marketability;
    private int favouriteBrakesSupplier;
    private int scoutingLevelRequired;
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

    public int Fame
    {
        get
        {
            return fame;
        }

        set
        {
            fame = value;
        }
    }

    public float MBrakingImprovementRate
    {
        get
        {
            return mBrakingImprovementRate;
        }

        set
        {
            mBrakingImprovementRate = value;
        }
    }

    public float MCorneringImprovementRate
    {
        get
        {
            return mCorneringImprovementRate;
        }

        set
        {
            mCorneringImprovementRate = value;
        }
    }

    public float MSmoothnessImprovementRate
    {
        get
        {
            return mSmoothnessImprovementRate;
        }

        set
        {
            mSmoothnessImprovementRate = value;
        }
    }

    public float MOvertakingImprovementRate
    {
        get
        {
            return mOvertakingImprovementRate;
        }

        set
        {
            mOvertakingImprovementRate = value;
        }
    }

    public float MConsistencyImprovementRate
    {
        get
        {
            return mConsistencyImprovementRate;
        }

        set
        {
            mConsistencyImprovementRate = value;
        }
    }

    public float MAdaptabilityImprovementRate
    {
        get
        {
            return mAdaptabilityImprovementRate;
        }

        set
        {
            mAdaptabilityImprovementRate = value;
        }
    }

    public float MFitnessImprovementRate
    {
        get
        {
            return mFitnessImprovementRate;
        }

        set
        {
            mFitnessImprovementRate = value;
        }
    }

    public float MFeedbackImprovementRate
    {
        get
        {
            return mFeedbackImprovementRate;
        }

        set
        {
            mFeedbackImprovementRate = value;
        }
    }

    public float MFocusImprovementRate
    {
        get
        {
            return mFocusImprovementRate;
        }

        set
        {
            mFocusImprovementRate = value;
        }
    }

    public float Braking
    {
        get
        {
            return braking;
        }

        set
        {
            braking = value;
        }
    }

    public float Cornering
    {
        get
        {
            return cornering;
        }

        set
        {
            cornering = value;
        }
    }

    public float Smoothness
    {
        get
        {
            return smoothness;
        }

        set
        {
            smoothness = value;
        }
    }

    public float Overtaking
    {
        get
        {
            return overtaking;
        }

        set
        {
            overtaking = value;
        }
    }

    public float Consistency
    {
        get
        {
            return consistency;
        }

        set
        {
            consistency = value;
        }
    }

    public float Adaptability
    {
        get
        {
            return adaptability;
        }

        set
        {
            adaptability = value;
        }
    }

    public float Fitness
    {
        get
        {
            return fitness;
        }

        set
        {
            fitness = value;
        }
    }

    public float Feedback
    {
        get
        {
            return feedback;
        }

        set
        {
            feedback = value;
        }
    }

    public float Focus
    {
        get
        {
            return focus;
        }

        set
        {
            focus = value;
        }
    }

    public float Balance
    {
        get
        {
            return balance;
        }

        set
        {
            balance = value;
        }
    }

    public float Experience
    {
        get
        {
            return experience;
        }

        set
        {
            experience = value;
        }
    }

    public float Marketability
    {
        get
        {
            return marketability;
        }

        set
        {
            marketability = value;
        }
    }

    public int FavouriteBrakesSupplier
    {
        get
        {
            return favouriteBrakesSupplier;
        }

        set
        {
            favouriteBrakesSupplier = value;
        }
    }

    public int ScoutingLevelRequired
    {
        get
        {
            return scoutingLevelRequired;
        }

        set
        {
            scoutingLevelRequired = value;
        }
    }

    public int MLowRangeBraking
    {
        get
        {
            return mLowRangeBraking;
        }

        set
        {
            mLowRangeBraking = value;
        }
    }

    public int MHighRangeBraking
    {
        get
        {
            return mHighRangeBraking;
        }

        set
        {
            mHighRangeBraking = value;
        }
    }

    public int MLowRangeCornering
    {
        get
        {
            return mLowRangeCornering;
        }

        set
        {
            mLowRangeCornering = value;
        }
    }

    public int MHighRangeCornering
    {
        get
        {
            return mHighRangeCornering;
        }

        set
        {
            mHighRangeCornering = value;
        }
    }

    public int MLowRangeSmoothness
    {
        get
        {
            return mLowRangeSmoothness;
        }

        set
        {
            mLowRangeSmoothness = value;
        }
    }

    public int MHighRangeSmoothness
    {
        get
        {
            return mHighRangeSmoothness;
        }

        set
        {
            mHighRangeSmoothness = value;
        }
    }

    public int MLowRangeOvertaking
    {
        get
        {
            return mLowRangeOvertaking;
        }

        set
        {
            mLowRangeOvertaking = value;
        }
    }

    public int MHighRangeOvertaking
    {
        get
        {
            return mHighRangeOvertaking;
        }

        set
        {
            mHighRangeOvertaking = value;
        }
    }

    public int MLowRangeConsistency
    {
        get
        {
            return mLowRangeConsistency;
        }

        set
        {
            mLowRangeConsistency = value;
        }
    }

    public int MHighRangeConsistency
    {
        get
        {
            return mHighRangeConsistency;
        }

        set
        {
            mHighRangeConsistency = value;
        }
    }

    public int MLowRangeAdaptability
    {
        get
        {
            return mLowRangeAdaptability;
        }

        set
        {
            mLowRangeAdaptability = value;
        }
    }

    public int MHighRangeAdaptability
    {
        get
        {
            return mHighRangeAdaptability;
        }

        set
        {
            mHighRangeAdaptability = value;
        }
    }

    public int MLowRangeFitness
    {
        get
        {
            return mLowRangeFitness;
        }

        set
        {
            mLowRangeFitness = value;
        }
    }

    public int MHighRangeFitness
    {
        get
        {
            return mHighRangeFitness;
        }

        set
        {
            mHighRangeFitness = value;
        }
    }

    public int MLowRangeFeedback
    {
        get
        {
            return mLowRangeFeedback;
        }

        set
        {
            mLowRangeFeedback = value;
        }
    }

    public int MHighRangeFeedback
    {
        get
        {
            return mHighRangeFeedback;
        }

        set
        {
            mHighRangeFeedback = value;
        }
    }

    public int MLowRangeFocus
    {
        get
        {
            return mLowRangeFocus;
        }

        set
        {
            mLowRangeFocus = value;
        }
    }

    public int MHighRangeFocus
    {
        get
        {
            return mHighRangeFocus;
        }

        set
        {
            mHighRangeFocus = value;
        }
    }
}
