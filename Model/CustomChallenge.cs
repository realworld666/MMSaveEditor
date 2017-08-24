using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class CustomChallenge : Challenge
{
    public TeamColor[] teamColors;
}
