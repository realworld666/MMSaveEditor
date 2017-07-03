using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class CarConditionPerformance : PerformanceImpact
{
	private float[] mPartTimeCost = new float[0];
	private Car mCar;
	private float mDistance;
	private float mSegmentDistance;
	private float mNextUpdateDistance;
	private SessionDetails.SessionType mSessionType;

}
