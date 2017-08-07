using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class CarStats
{
    private float topSpeed;
    private float acceleration;
    private float braking;
    private float lowSpeedCorners;
    private float mediumSpeedCorners;
    private float highSpeedCorners;

    public float TopSpeed
    {
        get
        {
            return topSpeed;
        }

        set
        {
            topSpeed = value;
        }
    }

    public float Acceleration
    {
        get
        {
            return acceleration;
        }

        set
        {
            acceleration = value;
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

    public float LowSpeedCorners
    {
        get
        {
            return lowSpeedCorners;
        }

        set
        {
            lowSpeedCorners = value;
        }
    }

    public float MediumSpeedCorners
    {
        get
        {
            return mediumSpeedCorners;
        }

        set
        {
            mediumSpeedCorners = value;
        }
    }

    public float HighSpeedCorners
    {
        get
        {
            return highSpeedCorners;
        }

        set
        {
            highSpeedCorners = value;
        }
    }

    public enum RelevantToCircuit
    {
        [LocalisationID("PSG_10003785")] No,
        [LocalisationID("PSG_10003786")] Useful,
        [LocalisationID("PSG_10003787")] VeryUseful,
        [LocalisationID("PSG_10003788")] VeryImportant,
    }

    public enum StatType
    {
        [LocalisationID("PSG_10004158")] TopSpeed,
        [LocalisationID("PSG_10004159")] Acceleration,
        [LocalisationID("PSG_10004160")] Braking,
        [LocalisationID("PSG_10004161")] LowSpeedCorners,
        [LocalisationID("PSG_10004162")] MediumSpeedCorners,
        [LocalisationID("PSG_10004163")] HighSpeedCorners,
        Count,
    }

    public enum CarStatShortName
    {
        [LocalisationID("PSG_10004164")] TS,
        [LocalisationID("PSG_10004165")] ACC,
        [LocalisationID("PSG_10004166")] DEC,
        [LocalisationID("PSG_10004167")] LSC,
        [LocalisationID("PSG_10004168")] MSC,
        [LocalisationID("PSG_10004169")] HSC,
    }

    public float GetStat(CarStats.StatType inStat)
    {
        float num = 0.0f;
        switch (inStat)
        {
            case CarStats.StatType.TopSpeed:
                num = this.topSpeed;
                break;
            case CarStats.StatType.Acceleration:
                num = this.acceleration;
                break;
            case CarStats.StatType.Braking:
                num = this.braking;
                break;
            case CarStats.StatType.LowSpeedCorners:
                num = this.lowSpeedCorners;
                break;
            case CarStats.StatType.MediumSpeedCorners:
                num = this.mediumSpeedCorners;
                break;
            case CarStats.StatType.HighSpeedCorners:
                num = this.highSpeedCorners;
                break;
        }
        return num;
    }
}
