
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class AnimatedFloat
{
	private float mAnimationDuration = 1f;
	private AnimatedFloat.State mState;
	private float mStateTimer;
	private EasingUtility.Easing mCurve;
	private float mAnimationDelay;
	private float mValue;
	private float mPreviousValue;
	private float mTargetValue;
	private bool mHasTargetBeenSet;

	public enum State
	{
		Idle,
		Animating,
	}

	public enum Animation
	{
		Animate,
		DontAnimate,
	}
}
