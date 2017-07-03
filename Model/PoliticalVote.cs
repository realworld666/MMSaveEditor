
using FullSerializer;
using System.Collections.Generic;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class PoliticalVote
{
	private string mName = string.Empty;
	private string mDescription = string.Empty;
	public string group = string.Empty;
	public string effectType = string.Empty;
	public List<PoliticalImpact> impacts = new List<PoliticalImpact>();
	public List<PoliticalVote.TeamCharacteristics> benificialCharacteristics = new List<PoliticalVote.TeamCharacteristics>();
	public List<PoliticalVote.TeamCharacteristics> detrimentalCharacteristics = new List<PoliticalVote.TeamCharacteristics>();
	public CarPart.PartType currentPartType = CarPart.PartType.None;
	public DialogQuery messageCriteria = new DialogQuery();
	private bool mDisplayRule = true;
	public int ID;
	public DilemmaSystem.BribedOption playerBribe;
	private Championship mChampionship;

	public enum TeamImpact
	{
		Beneficial,
		Neutral,
		Detrimental,
	}

	public enum TeamCharacteristics
	{
		[LocalisationID( "PSG_10008463" )] Traditionalist,
		[LocalisationID( "PSG_10008464" )] Progressive,
		[LocalisationID( "PSG_10008465" )] Egalitarian,
		[LocalisationID( "PSG_10008466" )] Gimmicky,
		[LocalisationID( "PSG_10008467" )] TrackStatsFavourable,
		[LocalisationID( "PSG_10008468" )] TrackStatsUnfavourable,
		[LocalisationID( "PSG_10008467" )] NewTrackStatsFavourable,
		[LocalisationID( "PSG_10008526" )] OldTrackStatsUnfavourable,
		[LocalisationID( "PSG_10008522" )] NewLayoutStatsFavourable,
		[LocalisationID( "PSG_10008523" )] NewLayoutStatsUnfavourable,
		[LocalisationID( "PSG_10008525" )] OldTrackStatsFavourable,
		[LocalisationID( "PSG_10008469" )] TeamQualityLow,
		[LocalisationID( "PSG_10008470" )] TeamQualityAverage,
		[LocalisationID( "PSG_10008471" )] TeamQualityHigh,
		[LocalisationID( "PSG_10008472" )] TeamPositionTop,
		[LocalisationID( "PSG_10008473" )] TeamPositionHigh,
		[LocalisationID( "PSG_10008474" )] TeamPositionAverage,
		[LocalisationID( "PSG_10008475" )] TeamPositionLow,
		[LocalisationID( "PSG_10008476" )] TeamBudgetLow,
		[LocalisationID( "PSG_10008477" )] TeamBudgetAverage,
		[LocalisationID( "PSG_10008478" )] TeamBudgetHigh,
		[LocalisationID( "PSG_10008482" )] CorneringSpeedVeryHigh,
		[LocalisationID( "PSG_10008483" )] CorneringSpeedHigh,
		[LocalisationID( "PSG_10008484" )] CorneringSpeedAverage,
		[LocalisationID( "PSG_10008485" )] CorneringSpeedLow,
		[LocalisationID( "PSG_10008486" )] CorneringSpeedVeryLow,
		[LocalisationID( "PSG_10008487" )] TyreWearRateBad,
		[LocalisationID( "PSG_10008488" )] TyreWearRateGood,
		[LocalisationID( "PSG_10008489" )] FuelBurnRateBad,
		[LocalisationID( "PSG_10008490" )] FuelBurnRateGood,
		[LocalisationID( "PSG_10008492" )] TeamDriverQualityLow,
		[LocalisationID( "PSG_10008491" )] TeamDriverQualityHigh,
	}
}
