
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SimulationSettings
{
    public SessionDetails.SessionType sessionType;
    public ChampionshipRules.SessionLength sessionLength = ChampionshipRules.SessionLength.Medium;
    public float tyreWearRate;
    public int duration;
    public int weatherScale;

}
