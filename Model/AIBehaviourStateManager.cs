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
    private AISpinBehaviour mSpinBehaviour = new AISpinBehaviour();
    private AIInOutLapBehaviour mInOutlapBehaviour = new AIInOutLapBehaviour();
    private AIBlueFlagBehaviour mBlueFlagBehaviour = new AIBlueFlagBehaviour();
    private AINoOvertakingBehaviour mNoOvertakingBehaviour = new AINoOvertakingBehaviour();
    private AIRaceStartBehaviour mRaceStartBehaviour = new AIRaceStartBehaviour();
    private AISafetyFlagBehaviour mSafetyFlagBehaviour = new AISafetyFlagBehaviour();
    private AICriticalIssueBehaviour mCriticalIssueBehaviour = new AICriticalIssueBehaviour();
    private AITeamOrderBehaviour mTeamOrderBehaviour = new AITeamOrderBehaviour();
    private AISafetyCarBehaviour mSafetyCarBehaviour = new AISafetyCarBehaviour();
    private AISafetyCarIdleBehaviour mSafetyCarIdleBehaviour = new AISafetyCarIdleBehaviour();
    private AIRunningWideBehaviour mRunningWideBehaviour;
    private AICuttingCornersBehaviour mCuttingCornersBehaviour;
    private List<AIBehaviour> mBehaviours = new List<AIBehaviour>();
    private bool mCanAttackVehicle = true;
    private bool mCanDefendVehicle = true;
    public Action OnBehaviourChange;
    private RacingVehicle mRacingVehicle;
    private SafetyVehicle mSafetyVehicle;
    private Vehicle mVehicle;
    private AIBehaviour mCurrentBehaviour;
    private AIBehaviour mPreviousBehaviour;


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
