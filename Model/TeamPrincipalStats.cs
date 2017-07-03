using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class TeamPrincipalStats : PersonStats
{
    public const int teamPrincipalStatsMax = 20;
    public const int teamPrincipalStatsNum = 3;
    public const int teamPrincipalStatsTotalMax = 60;
    public float raceManagement;
    public float financial;
    public float loyalty;
    public float jobSecurity;


}
