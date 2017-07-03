using System;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class Person : Entity
{
    public static readonly int worldClassReputationValue = 18;
    public static readonly int greatReputationValue = 15;
    public static readonly int goodReputationValue = 12;
    public static readonly int averageReputationValue = 8;
    public static readonly int minRacesBeforeThinkingAboutJobChange = 3;
    public static readonly float bonusImprovementAmount = 2f;
    public static readonly int bonusImprovementAge = 23;
    public static readonly float improvementRateDecayMin = -3f / 1000f;
    public static readonly float improvementRateDecayMax = -1f / 1000f;
    private string mFirstName = string.Empty;
    private string mLastName = string.Empty;
    private string mShortName = string.Empty;
    private string mThreeLetterName = string.Empty;
    public Nationality nationality = new Nationality();
    public Portrait portrait = new Portrait();
    public Popularity popularity = new Popularity();
    public Relationships relationships = new Relationships();
    public DialogQueryCreator dialogQuery = new DialogQueryCreator();
    public ContractManagerPerson contractManager = new ContractManagerPerson();
    public ContractPerson contract = new ContractPerson();
    public ContractPerson nextYearContract = new ContractPerson();
    public int peakDuration;
    public CareerHistory careerHistory = new CareerHistory();
    private StatModificationHistory mMoraleStatModificationHistory = new StatModificationHistory();
    private float mImprovementRateDecay;
    private bool mIsShortlisted;
    public Person.Gender gender;
    public DateTime dateOfBirth;
    public int weight;
    public int retirementAge;
    public float obedience;
    public DateTime peakAge;
    private float mMorale;


    public enum Gender
    {
        Male,
        Female,
    }

    public enum Reputation
    {
        Bad,
        Average,
        Good,
        Great,
        WorldClass,
    }

    public enum InterestedToTalkResponseType
    {
        InterestedToTalk,
        NotJoiningLowerChampionship,
        WantToJoinHigherChampionship,
        JustBeenFiredByPlayer,
        InsultedByLastProposal,
        WantsToRetire,
        WontRenewContract,
        WontJoinRival,
        JustStartedANewContract,
        TooEarlyToRenew,
        MoraleTooLow,
        LetNegotiationExpire,
        CanceledNegotiation,
        NotInterestedToTalkGeneric,
        WontDriveForThatSeries,
        OffendedByInterview,
    }
}
