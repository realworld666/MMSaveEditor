
using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class PitstopTimesHelper
{
    [NonSerialized]
    private RacingVehicle mVehicle;
    [NonSerialized]
    private ChampionshipRules.PitStopCrewSize mPitCrewSize;


}
