using FullSerializer;
using System;
using UnityEngine;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ChallengeObjectiveChampionshipPosition : ChallengeObjective
{
    private string mDriverName = string.Empty;
    private ChallengeObjectiveChampionshipPosition.Target mTarget;
    private int mPosition;
    [NonSerialized]
    private Driver mTargetDriver;
    private int mDriverIndex;


    public enum Target
    {
        AnyDriver,
        BothDrivers,
        Team,
        TargetDriver,
    }
}
