using System;

public abstract class Challenge
{
    public string descriptionID = string.Empty;
    public string objectivesID = string.Empty;
    public string restrictionsID = string.Empty;
    public string rewardsID = string.Empty;
    public Challenge.ChallengeName challengeName;
    public Challenge.Difficulty difficulty;
    public Challenge.ChallengeStatus status;
    public ChallengeRestrictions_v1 restrictions;
    public string titleID;
    public int id;
    public int[] rulesDisplay;
    public int numberUI;
    public bool blockPolitics;
    public bool blockDilemmas;
    public float partBonusAITeams;
    public Challenge.ChallengeStartType startType;
    public Challenge.ChallengeGroup group;
    public Challenge.ChallengeTier tier;
    public ChallengeDuration duration;
    public ChallengeObjectives objectives;
    public ChallengeRewards rewards;
    public Challenge.ChallengeType type;

    public enum Difficulty
    {
        [LocalisationID("PSG_10010654")] Easy,
        [LocalisationID("PSG_10010655")] Medium,
        [LocalisationID("PSG_10010656")] Hard,
        Count,
    }

    public enum ChallengeName
    {
        [LocalisationID("PSG_10007208")] Underdog,
        [LocalisationID("PSG_10007214")] TopManager,
        Custom,
    }

    public enum ChallengeStatus
    {
        [LocalisationID("PSG_10009316")] InProgress,
        [LocalisationID("PSG_10008896")] Completed,
        [LocalisationID("PSG_10007186")] Failed,
    }

    public enum ChallengeStartType
    {
        NewCareer,
        LoadChallenge,
    }

    public enum ChallengeGroup
    {
        BaseGame,
        ChallengePack,
        Count,
    }

    public enum ChallengeTier
    {
        None,
        TierOne,
        TierTwo,
        TierThree,
        Count,
    }

    public enum ChallengeType
    {
        Career,
        SingleRace,
    }
}
