using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ChallengeRestrictions
{
    public List<Team> mAllowedTeams = new List<Team>();
    public bool mCanLeaveTeam;
}
