
using FullSerializer;
using UnityEngine;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class RaceStartSteeringBehaviour : SteeringBehaviour
{
    private Vector3 mTargetGatePosition;
    private float mStartingPathSpace;
    private float mStartingDistanceSqr;
    private int mTargetGateID;
    private float mTargetPathSpace;
    private bool mHasFoundFirstCorner;
    private EasingUtility.Easing mCurve;

}
