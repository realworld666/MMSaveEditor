
using System;
using System.Collections.Generic;
using FullSerializer;
using System.Linq;
using System.Diagnostics;

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

    public Chairman chairman
    {
        get
        {
            return this.contractManager.GetPersonOnJob(Contract.Job.Chairman) as Chairman;
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

    public Driver GetDriver(int inIndex)
    {
        if (inIndex < 0)
        {
            Debug.Assert(false, "Trying to access and array with a negative index ");
            return (Driver)null;
        }
        this.mDriversCache.Clear();
        this.contractManager.GetAllDrivers(ref this.mDriversCache);
        if (inIndex < this.mDriversCache.Count)
            return this.mDriversCache[inIndex];
        return (Driver)null;
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

    internal bool IsPlayersTeam()
    {
        if (this == Game.instance.player.team)
        {
            return !(this is NullTeam);
        }

        return false;
    }

    public void SelectMainDriversForSession()
    {
        List<EmployeeSlot> employeeSlotsForJob = this.contractManager.GetAllEmployeeSlotsForJob(Contract.Job.Driver);
        for (int index = 0; index < Team.mainDriverCount; ++index)
            this.mSelectedDriver[index] = employeeSlotsForJob[index].personHired as Driver;
    }

    public Mechanic GetMechanicOfDriver(Driver inDriver)
    {
        int driverIndex = this.GetDriverIndex(inDriver);
        this.mMechanics.Clear();
        this.contractManager.GetAllMechanics(ref this.mMechanics);
        int count = this.mMechanics.Count;
        for (int index = 0; index < count; ++index)
        {
            if (this.mMechanics[index].driver == driverIndex)
                return this.mMechanics[index];
        }
        return (Mechanic)null;
    }

    public int GetDriverIndex(Driver inDriver)
    {
        for (int index = 0; index < Team.mainDriverCount; ++index)
        {
            if (this.mSelectedDriver[index] != null && this.mSelectedDriver[index] == inDriver)
                return index;
        }
        this.mEmployeeSlots.Clear();
        this.contractManager.GetAllEmployeeSlotsForJob(Contract.Job.Driver, ref this.mEmployeeSlots);
        for (int index = 0; index < Team.mainDriverCount; ++index)
        {
            if (this.mEmployeeSlots[index].personHired != null && this.mEmployeeSlots[index].personHired == inDriver)
                return index;
        }
        return -1;
    }
}
