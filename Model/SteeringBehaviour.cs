
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class SteeringBehaviour
{
	private SteeringManager mSteeringManager;
	private Vehicle mVehicle;
	private RacingVehicle mRacingVehicle;
	private bool mIsActive;

}
