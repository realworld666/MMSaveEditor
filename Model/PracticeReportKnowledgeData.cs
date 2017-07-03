
using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class PracticeReportKnowledgeData
{
	public static readonly int mNumberOfBonuses = 3;
	public PracticeReportSessionData.KnowledgeType knowledgeType = PracticeReportSessionData.KnowledgeType.Count;
	private List<PracticeReportBonusData> mBonusTiers = new List<PracticeReportBonusData>();
	private const int mMaxKnowledge = 100;
	private const float mKnowledgeBonusGap = 33.3f;
	private float mCurrentKnowledge;

}
