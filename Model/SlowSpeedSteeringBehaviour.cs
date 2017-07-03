
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class SlowSpeedSteeringBehaviour : SteeringBehaviour
{
	private SlowSpeedSteeringBehaviour.MovementDirection mMovementDirection;

	public enum MovementDirection
	{
		Straight,
		Left,
		Right,
	}
}
