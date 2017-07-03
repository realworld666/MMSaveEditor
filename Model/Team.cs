
using System;
using System.Collections.Generic;
using FullSerializer;

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
    public TeamFinanceController financeController = new TeamFinanceController();
    public YoungDriverProgramme youngDriverProgramme = new YoungDriverProgramme();
    public History history = new History();
    public CarManager carManager = new CarManager();
    public TeamPerkManager perksManager = new TeamPerkManager();
    public SponsorController sponsorController = new SponsorController();
    public ContractManagerTeam contractManager = new ContractManagerTeam();
    public TeamStatistics teamStatistics = new TeamStatistics();
    public TeamAIController teamAIController = new TeamAIController();
    public TeamLogo customLogo = new TeamLogo();
    public string locationID = string.Empty;
    public int startOfSeasonExpectedChampionshipResult;
    public List<PoliticalVote.TeamCharacteristics> votingCharacteristics = new List<PoliticalVote.TeamCharacteristics>();
    public bool canRequestFunds = true;
    private string startDescription = string.Empty;
    private string mCustomStartDescription = string.Empty;
    private Driver[] mSelectedDriver = new Driver[Team.mainDriverCount];
    private int mCurrentExpectedChampionshipResult;
    private List<Mechanic> mMechanics = new List<Mechanic>();
    [NonSerialized]
    private List<Driver> mDriversCache = new List<Driver>();
    private List<EmployeeSlot> mEmployeeSlots = new List<EmployeeSlot>();
    public const int invalidTeamID = -1;
    public static readonly int mMinRacesBeforeStaffChangeAllowed;
    public Championship championship;
    public Headquarters headquarters;
    public TeamAIWeightings aiWeightings;
    public Investor investor;
    public int teamID;
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
    public int rulesBrokenThisSeason;
    public bool isBlockedByChallenge;
    public bool isCreatedByPlayer;
    public int votingPower;
    public Team rivalTeam;
    public float marketabilityBeforeEvent;
    public int expectedRacePosition;
    public bool canReceiveFullChairmanPayments;
    private ChampionshipEntry_v1 mChampionshipEntry;
    private List<Driver> mDrivers;

}
