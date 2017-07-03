
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class PathState
{
	private Vehicle mVehicle;
	private RacingVehicle mRacingVehicle;
	private PathStateManager mStateMachine;
	private SpeedManager mSpeedManager;
	private SteeringManager mSteeringManager;

}
