
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class AIRaceStartBehaviour : AIBehaviour
{
    private static readonly SpeedManager.Controller[] mSpeedControllers = new SpeedManager.Controller[5]
  {
    SpeedManager.Controller.TrackLayout,
    SpeedManager.Controller.Avoidance,
    SpeedManager.Controller.PathType,
    SpeedManager.Controller.GroupSpeed,
    SpeedManager.Controller.SafetyCar
  };
    private static readonly SteeringManager.Behaviour[] mSteeringBehaviours = new SteeringManager.Behaviour[3]
  {
    SteeringManager.Behaviour.RacingLine,
    SteeringManager.Behaviour.RaceStart,
    SteeringManager.Behaviour.TargetPoint
  };
    private int mExitStateCornerID;
    private AvoidanceSteeringBehaviour mAvoidanceSteeringBehaviour;

}
