
using FullSerializer;
using System;
using System.Collections.Generic;
using System.Text;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class PitCrewRegen
{
    private readonly int mMinAge = 20;
    private readonly int mMaxAge = 30;
    private PitCrewMemberStats.PitCrewStatType mLastPitCrewStat = PitCrewMemberStats.PitCrewStatType.Count;

}
