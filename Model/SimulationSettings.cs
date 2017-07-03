
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class SimulationSettings
{
	public ChampionshipRules.SessionLength sessionLength = ChampionshipRules.SessionLength.Medium;
	public SessionDetails.SessionType sessionType;
	public float tyreWearRate;
	public int duration;
	public int weatherScale;

}
