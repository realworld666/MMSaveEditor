
using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class PitCrewApplicationManager
{
    private List<PitCrewApplication> mPitCrewApplications = new List<PitCrewApplication>();
    private Team mTeam;


}
