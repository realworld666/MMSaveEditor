using FullSerializer;
using System;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ChallengeObjectiveRaceBase : ChallengeObjective
{
    [NonSerialized]
    protected float requiredTimer = 1f;
    [NonSerialized]
    protected int cachedRequiredPosition;

}
