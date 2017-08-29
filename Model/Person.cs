using System;
using System.Linq;
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
    private bool mIsShortlisted;
    public Nationality nationality = new Nationality();
    public Portrait portrait = new Portrait();
    public Popularity popularity = new Popularity();
    public Relationships relationships = new Relationships();
    public DialogQueryCreator dialogQuery = new DialogQueryCreator();
    public ContractManagerPerson contractManager = new ContractManagerPerson();
    private ContractPerson contract = new ContractPerson();
    public ContractPerson nextYearContract = new ContractPerson();

    public Person.Gender gender;
    public int rewardID;
    public DateTime dateOfBirth;
    public int weight;
    public int retirementAge;
    public float obedience;
    public DateTime peakAge;
    public int peakDuration;
    public CareerHistory careerHistory = new CareerHistory();

    public float mMorale;
    private StatModificationHistory mMoraleStatModificationHistory = new StatModificationHistory();
    private float mImprovementRateDecay;

    public string firstName
    {
        get
        {
            return mFirstName;
        }
    }

    public string lastName
    {
        get
        {
            return mLastName;
        }
    }

    public string twitterHandle
    {
        get
        {
            return shortName.Replace(" ", string.Empty);
        }
    }

    public string shortName
    {
        get
        {
            return mShortName;
        }
    }

    public string threeLetterLastName
    {
        get
        {
            return mThreeLetterName;
        }
    }

    public ContractPerson Contract
    {
        get
        {
            return contract;
        }

        set
        {
            contract = value;
        }
    }

    public bool IsFreeAgent()
    {
        return Contract.Job1 == global::Contract.Job.Unemployed;
    }

    public int GetAge()
    {
        DateTime now = Game.instance.time.now;
        int num = now.Year - dateOfBirth.Year;
        if (now.Month < dateOfBirth.Month || now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)
            --num;
        return num;
    }

    public void SetName(string inFirstName, string inLastName)
    {
        string oldName = name;

        mFirstName = inFirstName;
        if (inLastName.Length > 0)
            mLastName = GameUtility.ChangeFirstCharToUpperCase(inLastName);
        mShortName = mFirstName.Length <= 0 ? "." : firstName[0] + ". " + lastName;
        mThreeLetterName = "";

        for (int index = 0; index < lastName.Length; ++index)
        {
            if (mThreeLetterName.Length < 3 && lastName[index].ToString() != " ")
                mThreeLetterName += lastName[index];
        }
        name = inFirstName + " " + inLastName;

        Game.instance.mechanicManager.GetEntityList().ForEach(mechs => mechs.DriverRenamed(oldName, name));
    }

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

    public virtual bool IsReplacementPerson()
    {
        return false;
    }
}
