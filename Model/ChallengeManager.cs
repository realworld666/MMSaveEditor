using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ChallengeManager
{
    private Challenge mCurrentChallenge;
    private List<Challenge> mChallenges = new List<Challenge>();
    public ChallengeGameData challengeGameData;

    public bool IsAttemptingChallenge()
    {
        return this.mCurrentChallenge != null;
    }

    public enum ChallengeManagerGameEvents
    {
        NewCareer,
        PlayerTryingToLeaveTeam,
        PlayerLeftTeam,
        WonTopTier,
    }
}
