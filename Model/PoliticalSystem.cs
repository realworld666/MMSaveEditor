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

    public List<PoliticalVote> votesForSeason
    {
        get
        {
            return this.mVotesForSeason;
        }
    }

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

    public bool HasVote(PoliticalVote inVote)
    {
        int count = this.mChampionship.Rules.ActiveRules.Count;
        for (int index = 0; index < count; ++index)
        {
            if (this.mChampionship.Rules.ActiveRules[index].ID == inVote.ID)
                return true;
        }
        return false;
    }

    public bool CanVoteBeUsed(PoliticalVote inVote, List<PoliticalVote> inList)
    {
        for (int index = 0; index < inList.Count; ++index)
        {
            if (inList[index].group == inVote.group)
                return false;
        }
        return true;
    }

    public void OnStart(Championship inChampionship)
    {
        this.mChampionship = inChampionship;
    }
}
