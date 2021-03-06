﻿
using FullSerializer;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class GateInfo
{
    private List<GateInfo.LapData> mLapData = new List<GateInfo.LapData>();
    private bool[] mVehicleTimeRecorded = new bool[0];
    private int mHighestLap;
    private float[] mVehicleSessionTimeArray = new float[25];
    private int mPreviouslyTrimmedLaps;

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
