
using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class PitCrewAutomanaging
{
    [NonSerialized]
    private List<PitCrewMember> mPitCrewMembersCache = new List<PitCrewMember>();
    [NonSerialized]
    private List<PitCrewMember.PitCrewRole> mRolePriority = new List<PitCrewMember.PitCrewRole>();
    private Team mTeam;
    private bool mIsAutomanaged;
    private int mManagingMechanicIndex;

    public enum BestPitCrewFilter
    {
        HighestConfidence,
        BestStat,
    }
}
