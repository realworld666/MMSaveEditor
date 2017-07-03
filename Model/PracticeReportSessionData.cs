
using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class PracticeReportSessionData
{
	private float[] mBalanceSetupKnowledge = new float[CarManager.carCount];
	private PracticeReportKnowledgeData[] mKnowledgeData;


	public enum KnowledgeType
	{
		FirstOptionTyres,
		SecondOptionTyres,
		ThirdOptionTyres,
		[LocalisationID( "PSG_10007143" )] IntermediateTyres,
		[LocalisationID( "PSG_10007144" )] WetTyres,
		[LocalisationID( "PSG_10007146" )] QualifyingTrim,
		[LocalisationID( "PSG_10007147" )] RaceTrim,
		Count,
	}
}
