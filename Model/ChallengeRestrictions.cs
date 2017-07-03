using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ChallengeRestrictions
{
  private List<Team> mAllowedTeams = new List<Team>();
  private bool mCanLeaveTeam;
}
