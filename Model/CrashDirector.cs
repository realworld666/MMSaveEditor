
using System.Collections.Generic;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class CrashDirector
{
    private int mRealSafetyCarCount = 1;
    private float mVirtualSafetyFlagDuration;
    private SafetyVehicle mSafetyCar;
    private SessionManager mSessionManager;
    private List<RacingVehicle> mVehiclesCantCrash = new List<RacingVehicle>();
    private CrashDirector.CrashRaceChunk[] mCrashChunks = new CrashDirector.CrashRaceChunk[0];
    private CrashDirector.CrashRaceChunk mActiveChunk;


    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    private class CrashRaceChunk
    {
        public float normalizedChunkSize;
        public float normalizedChunkStart;
        public int crashCount;
    }
}
