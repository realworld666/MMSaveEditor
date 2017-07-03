using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class SessionCarBonuses
{
	private MechanicBonus.Trait[] mActiveBonuses = new MechanicBonus.Trait[2];
	private PracticeReportSessionData.KnowledgeType[] mActiveKnowledge = new PracticeReportSessionData.KnowledgeType[2];
	private List<MechanicBonus> mMechanicBonus = new List<MechanicBonus>();
	private List<PracticeReportKnowledgeData> mKnowledgeBonus = new List<PracticeReportKnowledgeData>();
	private List<SessionCarBonuses.DisplayBonusInfo> mDisplayBonusesAndKnowledge = new List<SessionCarBonuses.DisplayBonusInfo>();
	private List<object> mAvailable = new List<object>();
	public const int maxBonuses = 2;
	private RacingVehicle mVehicle;



	public class DisplayBonusInfo
	{
		private string mNameID;
		private string mDescrptionID;
		private int mBonusLevel;
		private float mBonusAmount;
		private List<TyreSet.Compound> mApplicableTyres;
		private PracticeReportSessionData.KnowledgeType mPracticeKnowledgeType;
		private int mMechanicKnowledgeInt;
	}
}
