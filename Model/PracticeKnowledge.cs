
using FullSerializer;
using System;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class PracticeKnowledge
{
	private static float mMaxBonusIncrease = 10f;
	private static float mMinBonusIncrease = 2f;
	public PracticeReportSessionData.KnowledgeType knowledgeType = PracticeReportSessionData.KnowledgeType.QualifyingTrim;
	private int mCarIndex = -1;
	private RacingVehicle mVehicle;
	private PracticeReportSessionData mPracticeReport;
	private DriverStats mDriverStats;
	private Driver mDriver;

	public enum SetupKnowledgeLevel
	{
		Low,
		Medium,
		High,
		Great,
	}
}
