
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


}
