
using FullSerializer;
using System;
using MMSaveEditor.Utils;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class CarPartStats
{
    public CarStats.StatType statType = CarStats.StatType.Acceleration;
    public int level;

    public const float performanceGainedPerReliabilityPointWeightStripped = 3f;
    public const float weightStrippedReliabilityMin = 0.5f;
    public float maxReliability = 0.8f;
    public float maxPerformance = 20f;
    public const int maxLevelMultiplier = 3;
    public const int maxPerformanceConstant = 21;
    public float rulesRisk;
    private float mPerformance;
    private float mReliability;
    private float mStat;
    public CarPartCondition partCondition = new CarPartCondition();
    private float[] mWeightStrippingReliabilityLost;
    private float mWeightStrippingModifier;


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
        get { return rulesRisk; }
        set { rulesRisk = value; }
    }

    public CarPartStats(CarPart inPart)
    {
        this.partCondition.Setup(inPart);
        this.statType = CarPart.GetStatForPartType(inPart.GetPartType());
    }

    public CarPartStats()
    {
    }

    public static CarPartStats.RulesRisk GetRisk(float inValue)
    {
        if ((double)inValue <= 0.0)
            return CarPartStats.RulesRisk.None;
        if ((double)inValue <= 1.0)
            return CarPartStats.RulesRisk.Low;
        return (double)inValue <= 2.0 ? CarPartStats.RulesRisk.Medium : CarPartStats.RulesRisk.High;
    }

    public void SetStat(CarPartStats.CarPartStat inStat, float inValue)
    {
        switch (inStat)
        {
            case CarPartStats.CarPartStat.MainStat:
                this.mStat = inValue;
                break;
            case CarPartStats.CarPartStat.Reliability:
                this.mReliability = inValue;
                this.mReliability = this.mReliability.Clamp(0.0f, this.maxReliability);
                this.SetStat(CarPartStats.CarPartStat.Condition, this.mReliability);
                break;
            case CarPartStats.CarPartStat.Condition:
                this.partCondition.SetCondition(inValue);
                break;
            case CarPartStats.CarPartStat.Performance:
                this.mPerformance = inValue;
                this.mPerformance = Math.Min(this.mPerformance, this.maxPerformance);
                break;
        }
    }

    public float GetStat(CarPartStats.CarPartStat inStat)
    {
        float num = 0.0f;
        switch (inStat)
        {
            case CarPartStats.CarPartStat.MainStat:
                num = this.mStat;
                break;
            case CarPartStats.CarPartStat.Reliability:
                num = this.mReliability;
                break;
            case CarPartStats.CarPartStat.Condition:
                num = this.partCondition.condition;
                break;
            case CarPartStats.CarPartStat.Performance:
                num = this.mPerformance;
                break;
        }
        return num;
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
