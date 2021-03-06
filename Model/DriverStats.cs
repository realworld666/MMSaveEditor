﻿using FullSerializer;
using System;
using MMSaveEditor.Utils;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class DriverStats : PersonStats
{
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
    private float focus;
    public int totalStatsMax = 180;
    private float balance;
    private float experience;
    public float marketability;
    public int favouriteBrakesSupplier;
    public int fame = -1;
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
    public float mBrakingImprovementRate = 1f;
    public float mCorneringImprovementRate = 1f;
    public float mSmoothnessImprovementRate = 1f;
    public float mOvertakingImprovementRate = 1f;
    public float mConsistencyImprovementRate = 1f;
    public float mAdaptabilityImprovementRate = 1f;
    public float mFitnessImprovementRate = 1f;
    public float mFeedbackImprovementRate = 1f;
    public float mFocusImprovementRate = 1f;

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

    public float BrakingImprovementRate
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

    public float CorneringImprovementRate
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

    public float SmoothnessImprovementRate
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

    public float OvertakingImprovementRate
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

    public float ConsistencyImprovementRate
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

    public float AdaptabilityImprovementRate
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

    public float FitnessImprovementRate
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

    public float FeedbackImprovementRate
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

    public float FocusImprovementRate
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

    public int LowRangeBraking
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

    public int HighRangeBraking
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

    public int LowRangeCornering
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

    public int HighRangeCornering
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

    public int LowRangeSmoothness
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

    public int HighRangeSmoothness
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

    public int LowRangeOvertaking
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

    public int HighRangeOvertaking
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

    public int LowRangeConsistency
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

    public int HighRangeConsistency
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

    public int LowRangeAdaptability
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

    public int HighRangeAdaptability
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

    public int LowRangeFitness
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

    public int HighRangeFitness
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

    public int LowRangeFeedback
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

    public int HighRangeFeedback
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

    public int LowRangeFocus
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

    public int HighRangeFocus
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

    public void Clear()
    {
        this.braking = 0.0f;
        this.cornering = 0.0f;
        this.smoothness = 0.0f;
        this.overtaking = 0.0f;
        this.consistency = 0.0f;
        this.adaptability = 0.0f;
        this.fitness = 0.0f;
        this.feedback = 0.0f;
        this.focus = 0.0f;
        this.balance = 0.0f;
        this.experience = 0.0f;
        this.marketability = 0.0f;
    }

    public float GetTotal()
    {
        return this.braking + this.cornering + this.smoothness + this.overtaking + this.consistency + this.adaptability + this.fitness + this.feedback + this.focus;
    }

    public int GetMaxPotential()
    {
        return 180 - (int)this.GetTotal();
    }

    public void SetMaxFromPotential(int inPotential)
    {
        this.totalStatsMax = (int)this.GetTotal() + inPotential;
        this.totalStatsMax = Math.Min(this.totalStatsMax, 180);
    }

    public void Add(DriverStats inAdd)
    {
        this.braking += inAdd.braking;
        this.cornering += inAdd.cornering;
        this.smoothness += inAdd.smoothness;
        this.overtaking += inAdd.overtaking;
        this.consistency += inAdd.consistency;
        this.adaptability += inAdd.adaptability;
        this.fitness += inAdd.fitness;
        this.feedback += inAdd.feedback;
        this.focus += inAdd.focus;
        this.balance += inAdd.balance;
    }

    public void ClampStats()
    {
        this.braking = this.braking.Clamp(0.0f, 20f);
        this.cornering = this.cornering.Clamp(0.0f, 20f);
        this.smoothness = this.smoothness.Clamp(0.0f, 20f);
        this.overtaking = this.overtaking.Clamp(0.0f, 20f);
        this.consistency = this.consistency.Clamp(0.0f, 20f);
        this.adaptability = this.adaptability.Clamp(0.0f, 20f);
        this.fitness = this.fitness.Clamp(0.0f, 20f);
        this.feedback = this.feedback.Clamp(0.0f, 20f);
        this.focus = this.focus.Clamp(0.0f, 20f);
        this.marketability = this.marketability.Clamp(0f, 1f);
        this.balance = this.balance.Clamp(0f, 1f);
    }
}
