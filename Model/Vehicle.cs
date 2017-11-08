using System;
using FullSerializer;
using UnityEngine;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class Vehicle
{
    public int id;
    public string name = string.Empty;
    public float speed;
    public bool enabled;
    public bool movementEnabled = true;
    public float currentAcceleration;
    public float currentBraking;
    protected PathStateManager mPathStateManager = new PathStateManager();
    protected AIBehaviourStateManager mBehaviourManager = new AIBehaviourStateManager();
    protected SpeedManager mSpeedManager = new SpeedManager();
    protected SteeringManager mSteeringManager = new SteeringManager();
    protected PathController mPathController = new PathController();
    protected SessionPerformance mPerformance = new SessionPerformance();
    protected PathTransform mTransform = new PathTransform();
    protected CollisionBounds mCollisionBounds = new CollisionBounds();
    protected Vector3 mVelocity;
    protected Vehicle.PedalState mPedalState;
    protected AnimatedFloat mThrottle = new AnimatedFloat();
    protected float mPedalStateTimer;
    protected float mBraking;
    protected float mAcceleration = float.MaxValue;
    protected float mMaxSpeed = float.MaxValue;
    protected float mCollisionCooldown;
    protected Vector3 mPreviousPosition;
    private Championship mChampionship;

    public Action OnLapEnd;

    public enum PedalState
    {
        Braking,
        Accelerating,
        Cruising,
    }
}
