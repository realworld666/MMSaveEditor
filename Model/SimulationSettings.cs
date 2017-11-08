
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SimulationSettings
{
    public SessionDetails.SessionType sessionType;
    public ChampionshipRules.SessionLength sessionLength = ChampionshipRules.SessionLength.Medium;
    public float tyreWearRate;
    public int duration;
    public int weatherScale;
    public int enduranceDuration;

    public void Apply(ChampionshipRules inRules)
    {
        switch (this.sessionType)
        {
            case SessionDetails.SessionType.Practice:
                inRules.PracticeDuration.Clear();
                inRules.PracticeDuration.Add(GameUtility.MinutesToSeconds((float)this.duration));
                break;
            case SessionDetails.SessionType.Qualifying:
                inRules.QualifyingDuration.Clear();
                inRules.QualifyingDuration.Add(GameUtility.MinutesToSeconds((float)this.duration));
                break;
            case SessionDetails.SessionType.Race:
                inRules.raceLength.Clear();
                inRules.raceLength.Add(this.sessionLength);
                break;
        }
    }
}
