
using System.Collections.Generic;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class CrashDirector
{
    private int mRealSafetyCarCount = 1;
    private float mVirtualSafetyFlagDuration;
    private SafetyVehicle mSafetyCar;
    private SessionManager mSessionManager;
    private List<RacingVehicle> mVehiclesCantCrash;
    private CrashDirector.CrashRaceChunk[] mCrashChunks;
    private CrashDirector.CrashRaceChunk mActiveChunk;
    public float normalizedChunkSize;
    public float normalizedChunkStart;
    public int crashCount;

    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    private class CrashRaceChunk
    {
        public float normalizedChunkSize;
        public float normalizedChunkStart;
        public int crashCount;
    }
}
