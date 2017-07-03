using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class TeamManager : GenericManager<Team>
{
  public NullTeam nullTeam = new NullTeam();
  public TeamRumourManager teamRumourManager = new TeamRumourManager();
    
}
