using FullSerializer;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class CareerHistory
{
    private List<CareerHistoryEntry> mCareer = new List<CareerHistoryEntry>();
    private Person mPerson;

    public int careerCount
    {
        get
        {
            return this.mCareer.Count;
        }
    }

    public Team currentTeam
    {
        get
        {
            if (this.careerCount > 0 && !this.mPerson.IsFreeAgent())
                return this.mCareer[this.mCareer.Count - 1].team;
            return (Team)null;
        }
    }

    public Team previousTeam
    {
        get
        {
            if (this.mPerson.IsFreeAgent() && this.careerCount > 0)
                return this.mCareer[this.mCareer.Count - 1].team;
            if (this.careerCount > 1)
                return this.mCareer[this.mCareer.Count - 2].team;
            return (Team)null;
        }
    }

    public CareerHistoryEntry GetLastEntryTeam(Team inTeam, bool inCheckForFinishedEntryOnly)
    {
        for (int index = this.careerCount - 1; index >= 0; --index)
        {
            CareerHistoryEntry careerHistoryEntry = this.mCareer[index];
            if (inCheckForFinishedEntryOnly)
            {
                if (careerHistoryEntry.team == inTeam && careerHistoryEntry.isFinished)
                    return careerHistoryEntry;
            }
            else if (careerHistoryEntry.team == inTeam)
                return careerHistoryEntry;
        }
        return (CareerHistoryEntry)null;
    }

    public void MarkLastEntryTeamAsFinished(Team inTeam)
    {
        CareerHistoryEntry lastEntryTeam = this.GetLastEntryTeam(inTeam, false);
        if (lastEntryTeam == null)
            return;
        lastEntryTeam.MarkEntryAsFinished(Game.instance.time.now);
    }
}
