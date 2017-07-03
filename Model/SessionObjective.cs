
using FullSerializer;
[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class SessionObjective
{
	private SessionObjective.ConditionType mConditionType;
	private int mFinancialReward;
	private int mTargetResult;
	private bool mObjectiveExpired;
	private bool mLastObjectiveCompleted;
	private SponsorshipDeal mSponsorshipDeal;

	public enum Type
	{
		Qualify,
		Race,
	}

	public enum ConditionType
	{
		Equal,
		Greater,
		GreaterOrEqual,
		Smaller,
		SmallerOrEqual,
	}
}
