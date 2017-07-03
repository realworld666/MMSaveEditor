
using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class PlayerJobApplication
{
	public DateTime dateSent = new DateTime();
	public DateTime dateTeamReplied = new DateTime();
	private readonly float mDecisionThreshold = 1f;
	private readonly float mPlayerLoyaltyWeighting = 2f;
	private readonly float mPlayerFinancesWeighting = 6f;
	private readonly float mTeamMarketabilityWeighting = 0.1f;
	private readonly float mChampionshipDifferenceWeighting = 3f;
	private readonly Dictionary<int, int> mChairmanHappinessResults = new Dictionary<int, int>() { { 10, -8 }, { 26, -6 }, { 46, -2 }, { 55, 0 }, { 60, 2 }, { 70, 4 }, { 80, 6 }, { 81, 20 } };
	public Team team;
	public Message message;
	private PlayerJobApplication.Status mStatus;
	private bool mPreSeason;

	public enum Status
	{
		[LocalisationID( "PSG_10007133" )] WaitingTeamReply,
		[LocalisationID( "PSG_10007134" )] WaitingPlayerReply,
		[LocalisationID( "PSG_10007136" )] Declined,
		[LocalisationID( "PSG_10007135" )] Accepted,
	}
}
