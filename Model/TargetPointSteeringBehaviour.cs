
using FullSerializer;
using UnityEngine;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class TargetPointSteeringBehaviour : SteeringBehaviour
{
    private PathController.PathType mTargetPath = PathController.PathType.Count;
    private PathController.Path mTargetPointPath = new PathController.Path();
    private TargetPointSteeringBehaviour.State mState;
    private TargetPointSteeringBehaviour.TargetResult mTargetResult;
    private float mHalfBlendLength;
    private float mPathSpace;
    private float mPathSpaceOnBlendStart;
    private bool mWaitToGetCloseToPath;
    private Vector3 mTargetPosition = new Vector3();
    private PathSpline.SplinePosition mTargetSplinePosition = new PathSpline.SplinePosition();
    private bool mUseTargetPosition;
    private float mBlendLength;

    public enum State
    {
        None,
        WaitingToBlend,
        Blending,
        Finishing,
        Finished,
    }

    public enum TargetResult
    {
        None,
        ZeroSpeed,
        PathChange,
    }
}
