
using FullSerializer;

[fsObject("v1", new System.Type[] { typeof(ChampionshipEntry) }, MemberSerialization = fsMemberSerialization.OptOut)]
public class ChampionshipEntry_v1
{
    public int races;
    public int podiums;
    public int wins;
    public int DNFs;
    private int mCurrentPosition;
    private int[] mQualifyingPositions;
    private int[] mRacePositions;
    private int[] mChampionshipPositions;
    private int[] mPoints;
    private int[] mExpectedRacePositions;
    private int[] mEventPositions;
    private Entity mEntity;
    private Championship mChampionship;
    public ChampionshipEntry_v1()
    {
    }

    public ChampionshipEntry_v1(ChampionshipEntry v0)
    {
        this.races = v0.races;
        this.podiums = v0.podiums;
        this.wins = v0.podiums;
        this.DNFs = v0.podiums;
        this.mCurrentPosition = v0.mCurrentPosition;
        this.mQualifyingPositions = v0.mQualifyingPositions;
        this.mRacePositions = v0.mRacePositions;
        this.mChampionshipPositions = v0.mChampionshipPositions;
        this.mPoints = v0.mPoints;
        this.mExpectedRacePositions = v0.mExpectedRacePositions;
        this.mEventPositions = new int[v0.mExpectedRacePositions.Length];
        this.mEntity = v0.mEntity;
        this.mChampionship = v0.mChampionship;
    }
}
