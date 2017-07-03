
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class PreSeasonTestingData
{
	public string notes = string.Empty;
	private readonly float mUpdateProblemsRate = 0.12f;
	public Driver driver;
	public int laps;
	public float fastestLap;
	public float topSpeed;
	public float averageSpeed;
	public float timeOnTrack;
	public int expectedPosition;
	private RaceEventResults.ResultData mResultData;
	private Circuit mCircuit;
	private bool mIsOut;
	private float mNormalizedTimeIsOut;
	private bool mHadProblemDuringTesting;
	private float mLastProblemUpdateTime;

}
