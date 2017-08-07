using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ChampionshipDisplayEffect : DisplayEffect
{
    public Championship championship;
    public SessionDetails.SessionType sessionType = SessionDetails.SessionType.Race;
    public bool changeDisplaySessionActive;
    public bool showWMCIfUnemployed;

}
