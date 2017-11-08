
using FullSerializer;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SessionFastestLapData
{
    private List<SessionFastestLapData.FastestLapData> mFastestLapData = new List<SessionFastestLapData.FastestLapData>();



    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public struct FastestLapData
    {
        public Championship championship;
        public RacingVehicle vehicle;
        public SessionTimer.LapData lap;
    }
}
