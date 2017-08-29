using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ChallengeRewards
{
    [NonSerialized]
    public List<ChallengeReward> mRewards = new List<ChallengeReward>();


}
