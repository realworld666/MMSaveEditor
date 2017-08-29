using FullSerializer;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class CutCornerSeasonDirector
{
    public Dictionary<RaceEventDetails, List<Team>> cutCornersForEvents = new Dictionary<RaceEventDetails, List<Team>>();

}
