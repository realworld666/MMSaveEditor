using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class TeamDisplayEffect : DisplayEffect
{
    public Team team;
}
