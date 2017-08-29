
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class AIRacingBehaviour : AIBehaviour
{
    private static readonly SpeedManager.Controller[] mSpeedControllers = new SpeedManager.Controller[5] { SpeedManager.Controller.TrackLayout, SpeedManager.Controller.Avoidance, SpeedManager.Controller.PathType, SpeedManager.Controller.GroupSpeed, SpeedManager.Controller.SafetyCar };
    private static readonly SteeringManager.Behaviour[] mSteeringBehaviours = new SteeringManager.Behaviour[3] { SteeringManager.Behaviour.Avoidance, SteeringManager.Behaviour.RacingLine, SteeringManager.Behaviour.TargetPoint };
    private int mCornerCount;
    private int mCornersUntilNextOvertakeCheck;
    private float mConfortDistanceToVehicleAhead;
    private int gatesAheadToRunWide;
    private int gatesAheadToCutCorner;
    private bool mIsSetToCrash;
}
