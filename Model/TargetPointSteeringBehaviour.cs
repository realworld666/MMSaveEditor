﻿
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class TargetPointSteeringBehaviour : SteeringBehaviour
{
	private PathController.PathType mTargetPath = PathController.PathType.Count;
	private PathController.Path mTargetPointPath;
	private PathSpline.SplinePosition mTagetRacingLinePosition;
	private TargetPointSteeringBehaviour.State mState;
	private TargetPointSteeringBehaviour.TargetResult mTargetResult;
	private float mDistance;
	private float mBlendDistance;
	private float mHalfBlendLength;
	private float mPathSpace;
	private float mPathSpaceOnBlendStart;
	private bool mWaitToGetCloseToPath;

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