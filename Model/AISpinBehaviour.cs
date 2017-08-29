
using FullSerializer;
using System;
using UnityEngine;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class AISpinBehaviour : AIBehaviour
{
    private static readonly SpeedManager.Controller[] mSpeedControllers = new SpeedManager.Controller[1] { SpeedManager.Controller.SpiningOut };
    private static readonly SteeringManager.Behaviour[] mSteeringBehaviours = new SteeringManager.Behaviour[1] { SteeringManager.Behaviour.SpinOut };
    public Action OnSpinMessage;
    private float mTimer;
    private float mRecoveryTime;
    private Vector3 mRotation;
    private float mInitialBraking;
    private AISpinBehaviour.SpinState mState;
    private float mTargetAngle;
    private float mCurrentAngle;


    public enum SpinState
    {
        SpiningOut,
        Refocus,
        GoBackToTrack,
    }
}
