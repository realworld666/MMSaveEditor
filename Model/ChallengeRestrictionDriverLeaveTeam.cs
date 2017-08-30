
using FullSerializer;
using System;
using System.Collections.Generic;
using UnityEngine;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ChallengeRestrictionDriverLeaveTeam : ChallengeRestriction
{
    private string mDriverName = string.Empty;
    [NonSerialized]
    private Driver mTargetDriver;
    private int mDriverIndex;

}
