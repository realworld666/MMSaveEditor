using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ChallengeMailEventEffect : EventEffect
{
    public string mailPSG;

}
