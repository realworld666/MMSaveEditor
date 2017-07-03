
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class AIInOutLapBehaviour : AIBehaviour
{
	private static readonly SpeedManager.Controller[] mSpeedControllers = new SpeedManager.Controller[6] { SpeedManager.Controller.TrackLayout, SpeedManager.Controller.Avoidance, SpeedManager.Controller.InOutLap, SpeedManager.Controller.PathType, SpeedManager.Controller.GroupSpeed, SpeedManager.Controller.SafetyCar };
	private static readonly SteeringManager.Behaviour[] mAISteeringBehaviours = new SteeringManager.Behaviour[4] { SteeringManager.Behaviour.Avoidance, SteeringManager.Behaviour.RacingLine, SteeringManager.Behaviour.SlowSpeed, SteeringManager.Behaviour.TargetPoint };
	private static readonly SteeringManager.Behaviour[] mPlayerSteeringBehaviours = new SteeringManager.Behaviour[3] { SteeringManager.Behaviour.Avoidance, SteeringManager.Behaviour.RacingLine, SteeringManager.Behaviour.TargetPoint };

}
