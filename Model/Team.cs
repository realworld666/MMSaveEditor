
using System;
using System.Collections.Generic;
using FullSerializer;
using System.Linq;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class Team : Entity
{
    public static int mainDriverCount = 2;
    public static int driverCount = 3;
    public static readonly float mMinMarketabilityChangePerEvent = -0.05f;
    public static readonly float mMaxMarketabilityChangePerEvent = 0.05f;
    public static readonly float mMarketabilityModifier = 1f;
    public static readonly float mMarketabilityModifierKeptSameCSPosition = 0.1f;
    public static readonly float mMarketabilityMaxPositionChange = 5f;
    public Nationality nationality = new Nationality();
    private TeamFinanceController financeController = new TeamFinanceController();
    public YoungDriverProgramme youngDriverProgramme = new YoungDriverProgramme();
    public History history = new History();
    public CarManager carManager = new CarManager();
    public Championship championship;
    public TeamPerkManager perksManager = new TeamPerkManager();
    public SponsorController sponsorController = new SponsorController();
    public ContractManagerTeam contractManager = new ContractManagerTeam();
    public TeamStatistics teamStatistics = new TeamStatistics();
    public TeamAIController teamAIController = new TeamAIController();

    [NonSerialized]
    private List<Driver> mDriversCache = new List<Driver>();
    public const int invalidTeamID = -1;
    public static readonly int mMinRacesBeforeStaffChangeAllowed;
    public Headquarters headquarters;
    public TeamAIWeightings aiWeightings;
    public TeamLogo customLogo = new TeamLogo();
    public Investor investor;
    public int teamID;
    public string locationID = string.Empty;
    public int reputation;
    public float marketability;
    public int pressure;
    public float fanBase;
    public float aggression;
    public float initialTotalFanBase;
    public int colorID;
    public int liveryID;
    public int driversHatStyle;
    public int driversBodyStyle;
    public int startOfSeasonExpectedChampionshipResult;
    public int rulesBrokenThisSeason;
    public bool isBlockedByChallenge;
    public bool isCreatedByPlayer;
    public List<PoliticalVote.TeamCharacteristics> votingCharacteristics = new List<PoliticalVote.TeamCharacteristics>();
    public int votingPower;
    public Team rivalTeam;
    public float marketabilityBeforeEvent;
    public int expectedRacePosition;
    public bool canRequestFunds = true;
    public bool canReceiveFullChairmanPayments;
    private string startDescription = string.Empty;
    private string mCustomStartDescription = string.Empty;
    private ChampionshipEntry_v1 mChampionshipEntry;
    private Driver[] mSelectedDriver = new Driver[Team.mainDriverCount];
    private int mCurrentExpectedChampionshipResult;
    private List<Mechanic> mMechanics = new List<Mechanic>();
    private List<Driver> mDrivers;
    private List<EmployeeSlot> mEmployeeSlots = new List<EmployeeSlot>();

    public List<Driver> Drivers
    {
        get
        {
            var result = mSelectedDriver.ToList();
            result.Add(GetReserveDriver());
            return result;
        }
    }

    public TeamFinanceController FinanceController
    {
        get
        {
            return financeController;
        }

        set
        {
            financeController = value;
        }
    }

    public List<Mechanic> Mechanics
    {
        get
        {
            return mMechanics;
        }

        set
        {
            mMechanics = value;
        }
    }

    public Driver GetReserveDriver()
    {
        this.mDriversCache.Clear();
        this.contractManager.GetAllDrivers(ref this.mDriversCache);
        for (int index = 0; index < this.mDriversCache.Count; ++index)
        {
            Driver driver = this.mDriversCache[index];
            if (driver.Contract.currentStatus == ContractPerson.Status.Reserve)
                return driver;
        }
        return (Driver)null;
    }
}
