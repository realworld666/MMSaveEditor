
using FullSerializer;

[fsObject("v0", new System.Type[] { }, MemberSerialization = fsMemberSerialization.OptOut)]
public class ChampionshipEntry
{
    public int races;
    public int podiums;
    public int wins;
    public int DNFs;
    public int mCurrentPosition;
    public int[] mQualifyingPositions;
    public int[] mRacePositions;
    public int[] mChampionshipPositions;
    public int[] mPoints;
    public int[] mExpectedRacePositions;
    public int[] mEventPositions;
    public Entity mEntity;
    public Championship mChampionship;
}
