
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class AIBlueFlagBehaviour : AIBehaviour
{
	private static readonly SpeedManager.Controller[] mSpeedControllers = new SpeedManager.Controller[6] { SpeedManager.Controller.TrackLayout, SpeedManager.Controller.Avoidance, SpeedManager.Controller.PathType, SpeedManager.Controller.SafetyCar, SpeedManager.Controller.GroupSpeed, SpeedManager.Controller.BlueFlag };
	private static readonly SteeringManager.Behaviour[] mSteeringBehaviours = new SteeringManager.Behaviour[4] { SteeringManager.Behaviour.Avoidance, SteeringManager.Behaviour.RacingLine, SteeringManager.Behaviour.TargetPoint, SteeringManager.Behaviour.SlowSpeed };
	private float mSessionTimeOfLastMessage;
	private float mBlueFlagStateTimer;


}
