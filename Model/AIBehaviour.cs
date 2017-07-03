using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class AIBehaviour
{
	protected RacingVehicle mRacingVehicle;
	protected SafetyVehicle mSafetyVehicle;
	protected Vehicle mVehicle;
	protected SpeedManager mSpeedManager;
	protected SteeringManager mSteeringManager;
	protected AIBehaviour mQueuedBehaviour;
	protected float mStateTimer;
}
