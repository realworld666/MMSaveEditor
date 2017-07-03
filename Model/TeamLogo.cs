
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class TeamLogo
{
    public string teamFirstName = string.Empty;
    public string teamLasttName = string.Empty;
    public int styleID;
}
