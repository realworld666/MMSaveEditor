using FullSerializer;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ChallengeObjectiveStaffQuality : ChallengeObjective
{
    private ChallengeObjectiveStaffQuality.Target mTarget;
    private float mStars;


    public enum Target
    {
        Driver,
        Engineer,
        Mechanic,
    }
}
