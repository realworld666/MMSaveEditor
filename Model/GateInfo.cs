
using FullSerializer;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class GateInfo
{
    private List<GateInfo.LapData> mLapData = new List<GateInfo.LapData>();
    private Dictionary<int, float> mVehicleSessionTime = new Dictionary<int, float>(20);
    private bool[] mVehicleTimeRecorded = new bool[20];
    private int mHighestLap;


    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public class GateStandingsData
    {
        public RacingVehicle vehicle;
        public float time;
    }

    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public class LapData
    {
        public List<GateInfo.GateStandingsData> entries = new List<GateInfo.GateStandingsData>();
    }
}
