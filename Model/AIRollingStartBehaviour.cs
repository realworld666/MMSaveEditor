
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class AIRollingStartBehaviour : AIBehaviour
{
    private static readonly SpeedManager.Controller[] mSpeedControllers = new SpeedManager.Controller[4] { SpeedManager.Controller.TrackLayout, SpeedManager.Controller.Avoidance, SpeedManager.Controller.PathType, SpeedManager.Controller.SafetyCar };
    private static readonly SteeringManager.Behaviour[] mSteeringBehaviours = new SteeringManager.Behaviour[2] { SteeringManager.Behaviour.RacingLine, SteeringManager.Behaviour.TargetPoint };
    private float mTargetSpaceOffset;
    private bool mIsInSafetyTrain;
    private bool mIsRollingOut;


}
