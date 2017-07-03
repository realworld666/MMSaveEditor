
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class AICriticalIssueBehaviour : AIRacingBehaviour
{
	private static readonly SpeedManager.Controller[] mSpeedControllers = new SpeedManager.Controller[5] { SpeedManager.Controller.TrackLayout, SpeedManager.Controller.Avoidance, SpeedManager.Controller.PathType, SpeedManager.Controller.GroupSpeed, SpeedManager.Controller.SafetyCar };
	private static readonly SteeringManager.Behaviour[] mSteeringBehaviours = new SteeringManager.Behaviour[4] { SteeringManager.Behaviour.Avoidance, SteeringManager.Behaviour.RacingLine, SteeringManager.Behaviour.TargetPoint, SteeringManager.Behaviour.SlowSpeed };


}
