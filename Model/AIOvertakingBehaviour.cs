
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class AIOvertakingBehaviour : AIRacingBehaviour
{
	private float mOvertakeAttemptDuration = 8f;
	private float mMaxOvertakeAttemptDuration = 20f;
	private RacingVehicle mTarget;

}
