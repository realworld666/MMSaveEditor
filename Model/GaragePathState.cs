
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class GaragePathState : PathState
{
	private float mWaitDuration;
	private float mTimer;
	private int mExpectedPosition;
	private bool isDriverOutOfQualifying;

}
