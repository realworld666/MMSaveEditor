using FullSerializer;
using System.Collections.Generic;
using UnityEngine;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SteeringManager
{
    private static float maxTurnAngle = 180f;
    private static float maxTurnAnglePitlane = 280f;
    private static float maxPathSpaceTurnAngle = 180f;
    private static float pathSpaceLateralVelocity = 45f;
    private RacingLineSteeringBehaviour mRacingLineSteeringBehaviour = new RacingLineSteeringBehaviour();
    private AvoidanceSteeringBehaviour mAvoidanceSteeringBehaviour = new AvoidanceSteeringBehaviour();
    private TargetPointSteeringBehaviour mTargetPointSteeringBehaviour = new TargetPointSteeringBehaviour();
    private TyreLockUpSteeringBehaviour mTyreLockUpSteeringBehaviour = new TyreLockUpSteeringBehaviour();
    private SpinOutSteeringBehaviour mSpinOutSteeringBehaviour = new SpinOutSteeringBehaviour();
    private BlockingSteeringBehaviour mBlockingSteeringBehaviour = new BlockingSteeringBehaviour();
    private OvertakeSteeringBehaviour mOvertakeSteeringBehaviour = new OvertakeSteeringBehaviour();
    private SlowSpeedSteeringBehaviour mSlowSpeedSteeringBehaviour = new SlowSpeedSteeringBehaviour();
    private RaceStartSteeringBehaviour mRaceStartSteeringBehaviour = new RaceStartSteeringBehaviour();
    private SteeringContextMap mInterestMap = new SteeringContextMap();
    private SteeringContextMap mPreviousInterestMap = new SteeringContextMap();
    private SteeringContextMap mDangerMap = new SteeringContextMap();
    private SteeringContextMap mPreviousDangerMap = new SteeringContextMap();
    private SteeringContextMap mSteeringMap = new SteeringContextMap();
    private List<SteeringBehaviour> mBehaviours = new List<SteeringBehaviour>();
    private PIDController mPIDController = new PIDController();
    private Vector3 mSteeringDirection;
    private bool[] mMaskedDangerSlots = new bool[SteeringContextMap.slotCount];
    private float mDangerMaskLevel = 0.75f;
    private Vehicle mVehicle;

    public enum Behaviour
    {
        RacingLine,
        Avoidance,
        TargetPoint,
        TyreLockUp,
        SpinOut,
        Blocking,
        Overtake,
        SlowSpeed,
        RaceStart,
    }
}
