using FullSerializer;
using System.Collections.Generic;

[fsObject("v1", new System.Type[] { typeof(ChallengeRestrictions) }, MemberSerialization = fsMemberSerialization.OptOut)]
public class ChallengeRestrictions_v1
{
    private List<ChallengeRestriction> mRestrictions = new List<ChallengeRestriction>();

    public ChallengeRestrictions_v1()
    {
    }

    public ChallengeRestrictions_v1(ChallengeRestrictions v0)
    {
    }

    public ChallengeRestrictions_v1(Challenge.ChallengeName inChallengeName)
    {
        switch (inChallengeName)
        {
            case Challenge.ChallengeName.Underdog:
                this.mRestrictions.Add((ChallengeRestriction)new ChallengeRestrictionTeam(new int[1]
                {
          27
                }));
                this.mRestrictions.Add((ChallengeRestriction)new ChallengeRestrictionLeaveTeam());
                break;
            case Challenge.ChallengeName.TopManager:
                this.mRestrictions.Add((ChallengeRestriction)new ChallengeRestrictionTeam(new int[1]
                {
          29
                }));
                break;
        }
    }
}
