
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class DriverSessionForm : PerformanceImpact
{
	private float mFormPerformance;
	private float mConsistencyInfluence;
	private float mFocusInfluence;
	private float mFitnessInfluence;
	private float mRaceDistance;
	private float mCurrentDistance;
	private bool mHasAppliedFitnessInfluence;
	private SessionDetails.SessionType mSessionType;
	private float mDistance;
	private float mNextUpdateDistance;
	private float mSegmentDistance;

}
