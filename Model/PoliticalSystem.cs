using System;
using System.Collections.Generic;
using FullSerializer;


[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class PoliticalSystem
{
    public Person president;

    private PoliticalSystem.VoteResult mLatestVoteResult = PoliticalSystem.VoteResult.Tie;
    private PoliticalVote mActiveVote;
    private Championship mChampionship;

    private List<VoteChoice> mVoteChoices = new List<VoteChoice>();
    private List<PoliticalSystem.VoteResults> mVoteResultsForSeason = new List<PoliticalSystem.VoteResults>();
    private List<PoliticalVote> mVotesForSeason = new List<PoliticalVote>();
    private List<CalendarEvent_v1> mCalendarEvents = new List<CalendarEvent_v1>();
    private const DayOfWeek dayOfTheWeek = DayOfWeek.Wednesday;
    private const int everyXMonths = 2;
    private int mNextVoteIndex;
    private int mNewRuleAproved;
    private bool mEndOfSeasonMessage;

    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public class VoteResults
    {
        public PoliticalSystem.VoteResult voteResult = PoliticalSystem.VoteResult.Tie;
        public int yesVotesCount;
        public int noVotesCount;
        public int abstainedVotesCount;
        public PoliticalVote votedSubject;


    }

    public enum VoteResult
    {
        [LocalisationID("PSG_10008527")] Accepted,
        [LocalisationID("PSG_10008528")] Rejected,
        [LocalisationID("PSG_10008529")] Tie,
    }
}
