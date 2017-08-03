using System;
using FullSerializer;
using UnityEngine;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class Vehicle
{
    public string name = string.Empty;
    public bool movementEnabled = true;
    protected PathStateManager mPathStateManager = new PathStateManager();
    protected AIBehaviourStateManager mBehaviourManager = new AIBehaviourStateManager();
    protected SpeedManager mSpeedManager = new SpeedManager();
    protected SteeringManager mSteeringManager = new SteeringManager();
    protected PathController mPathController = new PathController();
    protected SessionPerformance mPerformance = new SessionPerformance();
    protected PathTransform mTransform = new PathTransform();
    protected CollisionBounds mCollisionBounds = new CollisionBounds();
    protected Vector3 mVelocity;
    protected AnimatedFloat mThrottle = new AnimatedFloat();
    protected float mAcceleration = float.MaxValue;
    protected float mMaxSpeed = float.MaxValue;
    protected Vector3 mPreviousPosition;
    public Action OnLapEnd;
    public int id;
    public float speed;
    public bool enabled;
    public float currentAcceleration;
    public float currentBraking;
    protected Vehicle.PedalState mPedalState;
    protected float mPedalStateTimer;
    protected float mBraking;
    protected float mCollisionCooldown;

    public enum PedalState
    {
        Braking,
        Accelerating,
        Cruising,
    }
}
