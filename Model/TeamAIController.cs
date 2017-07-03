using System;
using System.Collections.Generic;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class TeamAIController
{
    private static readonly int minScoutDriverAge = 21;
    private static readonly int maxScoutDriverAge = 31;
    private static readonly int minScoutEngineerAge = 22;
    private static readonly int maxScoutEngineerAge = 45;
    private static readonly int minScoutMechanicAge = 18;
    private static readonly int maxScoutMechanicAge = 31;
    private static readonly int maxActiveHQBuilding = 2;
    private static readonly int hireStaffCooldownDays = 40;
    private static readonly int fireStaffDelayDays = 14;
    private static readonly float firingChanceScalar = 0.25f;
    private static readonly float carImprovementReliabilityMinAggression = 1f;
    private static readonly float carImprovementReliabilityMaxAggression = 0.5f;

    private DateTime mLastCheckedTime = new DateTime();
    private DateTime mRequestFundsCooldown = new DateTime();

    private AIScoutingManager mScoutingManager = new AIScoutingManager();
    private List<TeamAIController.NegotiationEntry> mNegotiations = new List<TeamAIController.NegotiationEntry>();
    private DateTime mLastDriverScoutTime = new DateTime();
    private DateTime mLastEngineerScoutTime = new DateTime();
    private DateTime mLastMechanicScoutTime = new DateTime();
    private DateTime mLastHQUpdateTime = new DateTime();
    private DateTime mLastCarUpdateTime = new DateTime();
    private DateTime mLastFiringUpdateTime = new DateTime();
    private List<Driver> mDrivers = new List<Driver>();
    private List<Driver> mDriversTeamHasAttemptedToRenewContractWith = new List<Driver>();
    private List<Person> mPeopleApproachedAndRejectedBy = new List<Person>();
    private List<int> mImproveCarPartsList = new List<int>();
    private List<int> mImproveCarPartsListOther = new List<int>();
    private List<CarPart> mImproveCarPartsMostRecentParts = new List<CarPart>();
    public List<HQsBuildingInfo.Type> mHQTargetsList = new List<HQsBuildingInfo.Type>();
    public List<HQsBuildingInfo.Type> mHQHistoryList = new List<HQsBuildingInfo.Type>();
    private const float STARS_FOR_BETTER = 0.75f;
    private Team mTeam;
    private float mChanceOfFiring;
    private float mSeasonWeight;
    private int expectedEndOfSeasonPosition;
    private int endOfSeasonPosition;
    public class HQBuildingValue
    {
        public float mValue;
        public HQsBuilding_v1 mBuilding;
        public bool mbIsUpgrade;
    }

    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public class NegotiationEntry
    {
        public ContractNegotiationScreen.NegotatiationType mNegotiationType;
        public Person mPerson;
        public EmployeeSlot mPersonToFire;
        public ContractPerson mDraftContractPerson;
        public DateTime mLastNegotiatedWith;
        public Team mTeamAtTimeOfNegotiation;
        public bool mIsReplacementPerson;
    }

    private class DriverEval
    {
        public int mIndex;
        public float mStats;
        public bool mIsPayDriver;
        public bool mIsNegotiating;

        public DriverEval(int index, float stats, bool is_pay, bool is_negotiating)
        {
            this.mIndex = index;
            this.mStats = stats;
            this.mIsPayDriver = is_pay;
            this.mIsNegotiating = is_negotiating;
        }
    }

    public class CarUpgradeStatValues
    {
        public CarPart.PartType partType = CarPart.PartType.None;
        public float weighting;
        public float highestStatOnGrid;
        public float difference;
        public bool gotTwoPartsOfBestPossibleLevel;
        public int nPartsOfbestPossibleLevel;
        public bool has5ComponentSlots;
        public bool isSpecPart;
    }
}
