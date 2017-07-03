
using FullSerializer;
using System;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class PlayerBackStory
{
	private readonly float mDriverFeedBackStatModifier = 3f;
	private readonly float mDriverImprovementRateModifier = 0.1f;
	private readonly TimeSpan mPartDesignTimeModifier = new TimeSpan( 1, 0, 0, 0 );
	private readonly int mVotePowerModifier = 4;
	private readonly int mPaymentsModifier = 5;
	private PlayerBackStory.PlayerBackStoryType mBackStory = PlayerBackStory.PlayerBackStoryType.Outsider;

	public enum PlayerBackStoryType
	{
		ExDriver,
		ExEngineer,
		Financial,
		Politico,
		Outsider,
		Count,
	}
}
