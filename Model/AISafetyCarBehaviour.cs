
using FullSerializer;
using System;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class AISafetyCarBehaviour : AIBehaviour
{
	private static readonly SpeedManager.Controller[] mSpeedControllers = new SpeedManager.Controller[4] { SpeedManager.Controller.TrackLayout, SpeedManager.Controller.Avoidance, SpeedManager.Controller.PathType, SpeedManager.Controller.SafetyCar };
	private static readonly SteeringManager.Behaviour[] mSteeringBehaviours = new SteeringManager.Behaviour[3] { SteeringManager.Behaviour.Avoidance, SteeringManager.Behaviour.RacingLine, SteeringManager.Behaviour.TargetPoint };
	private AISafetyCarBehaviour.SafetyCarState mState = AISafetyCarBehaviour.SafetyCarState.ExitingGarage;
	private int mLaps;
	private int mLapsLength;
	private bool mHeadingToGarage;
	private bool mOnActiveLap;
	private bool mGreenLight;
	private float mGreenLightTimer;
	public enum SafetyCarState
	{
		Ending,
		ExitingGarage,
		ExitingPitlane,
		OnGoing,
		InGarage,
	}
}
