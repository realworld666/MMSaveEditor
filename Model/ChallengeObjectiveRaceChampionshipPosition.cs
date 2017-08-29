using FullSerializer;
using System;
using System.Collections.Generic;
using UnityEngine;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ChallengeObjectiveRaceChampionshipPosition : ChallengeObjectiveRaceBase
{
    private string mDriverName = string.Empty;
    private ChallengeObjectiveRaceChampionshipPosition.Target mTarget;
    private int mPosition;
    [NonSerialized]
    private Driver mTargetDriver;
    private int mDriverIndex;
    public RacingVehicle vehicle;
    public Driver driver;
    public int predictedPoints;
    public int predictedTotalPoints;
    public ChampionshipEntry_v1 entry;

    public enum Target
    {
        TargetDriver,
    }
}
