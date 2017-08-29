using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ChallengeObjectiveRacePosition : ChallengeObjectiveRaceBase
{
    private string mDriverName = string.Empty;
    private ChallengeObjectiveRacePosition.Target mTarget;
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
