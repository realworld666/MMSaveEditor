
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class SessionBonuses : PerformanceImpact
{
	private int[] mPracticeKnowledgeLevel = new int[7];
	private bool[] mActivePartBonuses = new bool[0];
	private float mTotalTimeCost = 1f;
	private float mTimeCostBonusReduction = 0.2f;
	private SessionDetails.SessionType mSessionType;
	private Car mCar;

}
