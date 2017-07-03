
using FullSerializer;
using System.Collections.Generic;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class ERSController
{
	private float[] mModeChangingCooldown = new float[3];
	private float[] mModeChangingCooldownDefaultValue = new float[3];
	private bool mHybridRuleActive = true;
	private bool mIsAutoControl = true;
	private List<int> mERSGates = new List<int>();
	private bool[] mActiveERSInfluences = new bool[7];
	private ERSController.ERSState mState;
	private ERSController.Mode mMode;
	private float mCurrentCharge;
	private float mMaxCharge;
	private float mChargeRate;
	private float mHybridDrainRate;
	private float mPowerDrainRate;
	private RacingVehicle mVehicle;
	private bool mIsActive;
	private int mNumberOfTrackGates;
	private int mFinishLineGateId;

	public enum ERSState
	{
		None,
		Broken,
		NotVisible,
	}

	public enum Mode
	{
		Harvest,
		Hybrid,
		Power,
		Count,
	}

	private enum ERSInfluence
	{
		Straight,
		Attacking,
		Defending,
		FullCharge,
		SafetyFlag,
		PitStopQueued,
		RaceEnding,
		Count,
	}
}
