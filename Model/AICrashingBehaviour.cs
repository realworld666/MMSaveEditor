
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class AICrashingBehaviour : AIBehaviour
{
	private static readonly SpeedManager.Controller[] mSpeedControllers = new SpeedManager.Controller[1] { SpeedManager.Controller.SpiningOut };
	private static readonly SteeringManager.Behaviour[] mSteeringBehaviours = new SteeringManager.Behaviour[3] { SteeringManager.Behaviour.Avoidance, SteeringManager.Behaviour.RacingLine, SteeringManager.Behaviour.TargetPoint };
	private bool mLoadFlag = true;
	private int mCrashPathID = -1;
	private float mOffTrackBrackingValue = 35f;
	private Vector3 mRotationTarget = new Vector3();
	private float mCurrentAngle;
	private float mTargetAngle;
	private bool mIsOutOfTrack;


}
