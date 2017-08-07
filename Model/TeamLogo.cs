
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class TeamLogo
{
    public int styleID;
    public string teamFirstName = string.Empty;
    public string teamLasttName = string.Empty;
}
