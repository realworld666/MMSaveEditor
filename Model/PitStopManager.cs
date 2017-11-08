
using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class PitStopManager
{
    [NonSerialized]
    private List<PitCrewMember> mPitCrewListCache = new List<PitCrewMember>();
    private Team mTeam;


}
