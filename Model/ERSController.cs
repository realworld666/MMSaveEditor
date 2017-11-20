
using FullSerializer;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ERSController
{
    private ERSController.ERSState mState;
    private ERSController.Mode mMode;
    private float[] mModeChangingCooldown = new float[3];
    private float[] mModeChangingCooldownDefaultValue = new float[3];
    private float mCurrentCharge;
    private float mMaxCharge;
    private float mChargeRate;
    private float mDesignDataChargetRate;
    private float mHybridDrainRate;
    private float mPowerDrainRate;
    private RacingVehicle mVehicle;
    private bool mHybridRuleActive = true;
    private bool mIsActive;
    private bool mIsAutoControl = true;
    private List<int> mERSGates = new List<int>();
    private int mNumberOfTrackGates;
    private int mFinishLineGateId;
    private bool[] mActiveERSInfluences = new bool[10];
    private bool isChargeBasedOnStandings;
    private ERSController.Mode mQueuedMode;
    private bool mIsAdvancedERSActive;
    private float mInfluenceTargetCharge;

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
