using FullSerializer;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class RunningWideDirector
{
    private RunningWideDirector.RunWidePerRaceChunk[] mRunWideCount = new RunningWideDirector.RunWidePerRaceChunk[0];
    private List<RacingVehicle> mVehiclesToRunWideOnFirstCorner = new List<RacingVehicle>();
    private int[] mRunWidePathUseCount;
    private float mCooldown;
    private RunningWideDirector.RunWidePerRaceChunk mActiveRunWideChunk;

    public enum Behaviour
    {
        CuttingCorner,
        RunningWide,
        Count,
    }

    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    private class RunWidePerRaceChunk
    {
        public float normalizedChunkSize;
        public float normalizedChunkStart;
        public int runWideCount;


    }
}
