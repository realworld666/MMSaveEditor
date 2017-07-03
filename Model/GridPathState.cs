
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class GridPathState : PathState
{
	private float mRaceStartReactionTimer;
	private bool mLightAreOut;
	private bool mUsesAIForStrategy;


}
