
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class TrackLayoutSpeedController : SpeedController
{
	private TrackLayoutSpeedController.State mState;
	private float mStateTimer;
	private float mTargetCorneringSpeed;


	public enum State
	{
		Accelerating,
		Braking,
		Cornering,
	}
}
