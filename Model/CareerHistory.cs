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
}
