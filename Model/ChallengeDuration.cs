using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ChallengeDuration
{
    protected bool mHasExpired;

    public enum DurationType
    {
        Infinite,
        Race,
        Season,
        Count,
    }
}
