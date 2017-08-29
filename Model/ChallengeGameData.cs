using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ChallengeGameData
{
    public bool isSabotageDriverOptionActive;
    public int sabotageDriverCount;
    private bool mHasSeenChallengeRecapPopup;

}
