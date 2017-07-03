using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ChampionshipDisplayEffect : DisplayEffect
{
    public SessionDetails.SessionType sessionType = SessionDetails.SessionType.Race;
    public Championship championship;
    public bool changeDisplaySessionActive;
    public bool showWMCIfUnemployed;

}
