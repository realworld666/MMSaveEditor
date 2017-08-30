using FullSerializer;
using System;
using System.Collections.Generic;
using UnityEngine;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ChallengeRestrictionTeam : ChallengeRestriction
{
    private int[] mAllowedTeamsID;
    public ChallengeRestrictionTeam(params int[] inAllowedTeamsID)
    {
        this.mAllowedTeamsID = inAllowedTeamsID;
    }

}
