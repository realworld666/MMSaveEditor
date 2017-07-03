
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class SpeedController
{
	private Vehicle mVehicle;
	private RacingVehicle mRacingVehicle;
	private SafetyVehicle mSafetyVehicle;
	private bool mIsActive;

}
