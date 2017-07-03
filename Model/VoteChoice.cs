
using System.Collections.Generic;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class VoteChoice
{
    public VoteChoice.Vote vote = VoteChoice.Vote.Undecided;
    public int votePowerUsed = 1;
    public List<PoliticalVote.TeamCharacteristics> benificialCharacteristics = new List<PoliticalVote.TeamCharacteristics>();
    public List<PoliticalVote.TeamCharacteristics> detrimentalCharacteristics = new List<PoliticalVote.TeamCharacteristics>();
    public Team team;

    public VoteChoice(Team inTeam)
    {
        this.team = inTeam;
    }

    public void Voted()
    {
        if (this.votePowerUsed == 1)
            return;
        this.team.votingPower -= this.votePowerUsed - 1;
    }

    public void Abstained()
    {
        ++this.team.votingPower;
    }

    public bool IsPresident()
    {
        return this.team == null;
    }

    public enum Vote
    {
        [LocalisationID("PSG_10008579")] Yes,
        [LocalisationID("PSG_10008578")] No,
        [LocalisationID("PSG_10008580")] Undecided,
        [LocalisationID("PSG_10011008")] Abstain,
    }
}
