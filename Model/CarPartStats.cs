
using FullSerializer;
using System;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class CarPartStats
{
    public CarStats.StatType statType = CarStats.StatType.Acceleration;
    private float maxReliability = 0.8f;
    private float maxPerformance = 20f;
    public CarPartCondition partCondition = new CarPartCondition();
    public const int maxLevelMultiplier = 3;
    public const int maxPerformanceConstant = 21;
    public int level;
    private float rulesRisk1;
    private float mPerformance;
    private float mReliability;
    private float mStat;

    public float statWithPerformance
    {
        get
        {
            return this.mStat + this.mPerformance;
        }
    }

    public float reliability
    {
        get
        {
            return this.mReliability;
        }
        set
        {
            mReliability = value;
        }
    }

    public float performance
    {
        get
        {
            return this.mPerformance;
        }
        set
        {
            this.mPerformance = value;
        }
    }

    public float stat
    {
        get
        {
            return this.mStat;
        }
        set
        {
            this.mStat = value;
        }
    }

    public float MaxReliability
    {
        get
        {
            return maxReliability;
        }

        set
        {
            maxReliability = value;
        }
    }

    public float MaxPerformance
    {
        get
        {
            return maxPerformance;
        }

        set
        {
            maxPerformance = value;
        }
    }

    public float RulesRisk1
    {
        get
        {
            return rulesRisk1;
        }

        set
        {
            rulesRisk1 = value;
        }
    }

    public static CarPartStats.RulesRisk GetRisk(float inValue)
    {
        if ((double)inValue <= 0.0)
            return CarPartStats.RulesRisk.None;
        if ((double)inValue <= 1.0)
            return CarPartStats.RulesRisk.Low;
        return (double)inValue <= 2.0 ? CarPartStats.RulesRisk.Medium : CarPartStats.RulesRisk.High;
    }

    public enum CarPartStat
    {
        MainStat,
        [LocalisationID("PSG_10002078")] Reliability,
        [LocalisationID("PSG_10004387")] Condition,
        [LocalisationID("PSG_10004388")] Performance,
        Count,
    }

    public enum RulesRisk
    {
        [LocalisationID("PSG_10005815")] None,
        [LocalisationID("PSG_10001437")] Low,
        [LocalisationID("PSG_10001438")] Medium,
        [LocalisationID("PSG_10001439")] High,
    }
}
