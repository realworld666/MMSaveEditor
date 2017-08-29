using FullSerializer;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class CuttingCornersDirector
{
    private CuttingCornersDirector.CutCornerRaceChunk[] mCutCornerChunks = new CuttingCornersDirector.CutCornerRaceChunk[0];
    private int[] mPathUseCount;
    private float mCooldown;
    private CuttingCornersDirector.CutCornerRaceChunk mActiveChunk;
    private List<Team> mTeamsToCutCorner;

    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    private class CutCornerRaceChunk
    {
        public float normalizedChunkSize;
        public float normalizedChunkStart;
        public int cutCornerCount;


    }
}
