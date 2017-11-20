
using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class MechanicStats : PersonStats
{
    public const int mechanicStatsMax = 20;
    public const int mechanicStatsNum = 6;
    public const int mechanicStatsTotalMax = 120;
    public float reliability;
    public float performance;
    public float concentration;
    public float speed;
    public float pitStops;
    public float leadership;
    public int totalStatsMax = 120;

    public float Reliability
    {
        get
        {
            return reliability;
        }

        set
        {
            reliability = value;
        }
    }

    public float Performance
    {
        get
        {
            return performance;
        }

        set
        {
            performance = value;
        }
    }

    public float Concentration
    {
        get
        {
            return concentration;
        }

        set
        {
            concentration = value;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    public float PitStops
    {
        get
        {
            return pitStops;
        }

        set
        {
            pitStops = value;
        }
    }

    public float Leadership
    {
        get
        {
            return leadership;
        }

        set
        {
            leadership = value;
        }
    }

    public int TotalStatsMax
    {
        get
        {
            return totalStatsMax;
        }

        set
        {
            totalStatsMax = value;
        }
    }
}
