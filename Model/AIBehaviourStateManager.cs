using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class AIBehaviourStateManager
{
    private AIRacingBehaviour mRacingBehaviour = new AIRacingBehaviour();
    private AIOvertakingBehaviour mOvertakingBehaviour = new AIOvertakingBehaviour();
    private AIDefendingBehaviour mDefendingBehaviour = new AIDefendingBehaviour();
    private AIRetiredBehaviour mRetiredBehaviour = new AIRetiredBehaviour();
    private AICrashingBehaviour mCrashingBehaviour = new AICrashingBehaviour();
    private AICrashedBehaviour mCrashedBehaviour = new AICrashedBehaviour();
    private AILockUpBehaviour mLockUpBehaviour = new AILockUpBehaviour();
    private AIInOutLapBehaviour mInOutlapBehaviour = new AIInOutLapBehaviour();
    private AIBlueFlagBehaviour mBlueFlagBehaviour = new AIBlueFlagBehaviour();
    private AINoOvertakingBehaviour mNoOvertakingBehaviour = new AINoOvertakingBehaviour();
    private AIRaceStartBehaviour mRaceStartBehaviour = new AIRaceStartBehaviour();
    private AISafetyFlagBehaviour mSafetyFlagBehaviour = new AISafetyFlagBehaviour();
    private AICriticalIssueBehaviour mCriticalIssueBehaviour = new AICriticalIssueBehaviour();
    private AITeamOrderBehaviour mTeamOrderBehaviour = new AITeamOrderBehaviour();
    private AIRunningWideBehaviour mRunningWideBehaviour = new AIRunningWideBehaviour();
    private AICuttingCornersBehaviour mCuttingCornersBehaviour = new AICuttingCornersBehaviour();
    private AISpinBehaviour mSpinBehaviour = new AISpinBehaviour();
    private AIRollingStartBehaviour mRollingStartBehaviour = new AIRollingStartBehaviour();
    private AISafetyCarBehaviour mSafetyCarBehaviour = new AISafetyCarBehaviour();
    private AISafetyCarIdleBehaviour mSafetyCarIdleBehaviour = new AISafetyCarIdleBehaviour();
    private AISafetyCarRolloutBehaviour mSafetyCarRolloutBehaviour = new AISafetyCarRolloutBehaviour();
    private RacingVehicle mRacingVehicle;
    private SafetyVehicle mSafetyVehicle;
    private Vehicle mVehicle;
    private List<AIBehaviour> mBehaviours = new List<AIBehaviour>();
    private AIBehaviour mCurrentBehaviour;
    private AIBehaviour mPreviousBehaviour;
    private bool mCanAttackVehicle = true;
    private bool mCanDefendVehicle = true;
    public Action OnBehaviourChange;


    public enum Behaviour
    {
        Racing,
        Overtaking,
        Defending,
        Retired,
        Crashing,
        Crashed,
        TyreLockUp,
        Spin,
        InOutLap,
        BlueFlag,
        NoOverTaking,
        SafetyFlag,
        RaceStart,
        CriticalIssue,
        TeamOrder,
        SafetyCar,
        SafetyCarIdle,
    }
}
