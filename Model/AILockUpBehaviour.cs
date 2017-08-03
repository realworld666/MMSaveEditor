
using FullSerializer;
using System;
using UnityEngine;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class AILockUpBehaviour : AIBehaviour
{
    private static readonly SpeedManager.Controller[] mSpeedControllers = new SpeedManager.Controller[4] { SpeedManager.Controller.TrackLayout, SpeedManager.Controller.Avoidance, SpeedManager.Controller.PathType, SpeedManager.Controller.TyreLockUp };
    private static readonly SteeringManager.Behaviour[] mSteeringBehaviours = new SteeringManager.Behaviour[1] { SteeringManager.Behaviour.TyreLockUp };
    private float mLockUpDuration = 2f;
    public Action OnTyreLockUpMessage;
    private float mTimer;
    private float mTargetSpace;
    private float mCurrentSpace;
    private Vector3 mDirection;

}
