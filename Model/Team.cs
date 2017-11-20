
using System;
using System.Collections.Generic;
using FullSerializer;
using System.Linq;
using System.Diagnostics;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class Team : Entity
{
    public static int mainDriverCount = 2;
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
    public PitCrewController pitCrewController = new PitCrewController();
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
    public int startOfSeasonExpectedChampionshipResult = RandomUtility.GetRandom(1, 18);
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
    private string mShortName = string.Empty;
    private ChampionshipEntry_v1 mChampionshipEntry;
    private Driver[] mSelectedDriver;
    private Dictionary<int, List<Driver>> mSelectedSessionDrivers = new Dictionary<int, List<Driver>>();
    private Dictionary<int, Driver> mVehicleSessionDrivers = new Dictionary<int, Driver>();
    private int mCurrentExpectedChampionshipResult = RandomUtility.GetRandom(1, 18);
    private List<Mechanic> mMechanics = new List<Mechanic>();
    private List<Driver> mDrivers;
    private List<EmployeeSlot> mEmployeeSlots = new List<EmployeeSlot>();

    public static int maxDriverCount;


    public List<Driver> Drivers
    {
        get
        {
            this.mDriversCache.Clear();
            this.contractManager.GetAllDrivers(ref this.mDriversCache);
            return mDriversCache;
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

    public string ShortName
    {
        get { return mShortName; }
        set { mShortName = value; }
    }

    public bool HasAIPitcrew
    {
        get
        {
            return this.pitCrewController.AIPitCrew != null;
        }
    }

    public Driver GetDriverForCar(int inCarIndex)
    {
        Driver[] driversForCar = this.GetDriversForCar(inCarIndex);
        if (driversForCar.Length > 0)
            return driversForCar[0];
        return (Driver)null;
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
        bool flag = Game.instance.sessionManager.sessionType == SessionDetails.SessionType.Qualifying && this.championship.Rules.gridSetup == ChampionshipRules.GridSetup.AverageLap;
        for (int inCarIndex = 0; inCarIndex < CarManager.carCount; ++inCarIndex)
        {
            Driver[] driversForCar = this.GetDriversForCar(inCarIndex);
            this.mSelectedSessionDrivers[inCarIndex].Clear();
            for (int index = 0; index < driversForCar.Length; ++index)
            {
                Driver inDriver = driversForCar[index];
                if (this.contractManager.IsSittingOutEvent(inDriver))
                    this.mSelectedSessionDrivers[inCarIndex].Add(this.GetReserveDriverToReplaceSitOut());
                else if (!flag)
                    this.mSelectedSessionDrivers[inCarIndex].Add(inDriver);
                else if (this.mSelectedSessionDrivers[inCarIndex].Count < 2 && !this.IsWorstDriver(inDriver, driversForCar))
                    this.mSelectedSessionDrivers[inCarIndex].Add(inDriver);
            }
        }
        for (int index1 = 0; index1 < CarManager.carCount; ++index1)
        {
            int index2 = 0;
            if (!this.IsPlayersTeam())
                index2 = RandomUtility.GetRandom(0, this.mSelectedSessionDrivers[index1].Count);
            this.mVehicleSessionDrivers[index1] = this.mSelectedSessionDrivers[index1][index2];
        }
    }

    private bool IsWorstDriver(Driver inDriver, params Driver[] inDrivers)
    {
        float total = inDriver.GetDriverStats().GetTotal();
        for (int index = 0; index < inDrivers.Length; ++index)
        {
            if ((double)inDrivers[index].GetDriverStats().GetTotal() <= (double)total)
                return false;
        }
        return true;
    }

    public Driver GetReserveDriverToReplaceSitOut()
    {
        List<Driver> drivers = new List<Driver>();
        Driver inDriver = (Driver)null;
        this.contractManager.GetAllDrivers(ref drivers);
        for (int index = 0; index < drivers.Count; ++index)
        {
            Driver driver = drivers[index];
            if (driver.contract.currentStatus == ContractPerson.Status.Reserve)
                inDriver = driver;
        }
        Game.instance.driverManager.AddDriverToChampionship(inDriver, true);
        return inDriver;
    }

    public Driver[] GetDriversForCar(int inCarIndex)
    {
        this.mDriversCache.Clear();
        if (this.championship.series == Championship.Series.EnduranceSeries)
        {
            this.contractManager.GetAllDriversForCar(ref this.mDriversCache, inCarIndex);
        }
        else
        {
            List<EmployeeSlot> employeeSlotsForJob = this.contractManager.GetAllEmployeeSlotsForJob(Contract.Job.Driver);
            if (!employeeSlotsForJob[inCarIndex].IsAvailable())
            {
                Driver personHired = employeeSlotsForJob[inCarIndex].personHired as Driver;
                if (this.contractManager.IsSittingOutEvent(personHired))
                    this.mDriversCache.Add(this.GetReserveDriverToReplaceSitOut());
                else
                    this.mDriversCache.Add(personHired);
            }
        }
        return this.mDriversCache.ToArray();
    }

    internal void RefreshMechanics()
    {
        this.mMechanics.Clear();
        this.contractManager.GetAllMechanics(ref this.mMechanics);
    }

    public Mechanic GetMechanicOfDriver(Driver inDriver)
    {
        if (inDriver.IsReserveDriver())
            inDriver = this.contractManager.GetDriverSittingOut();
        this.mMechanics.Clear();
        this.contractManager.GetAllMechanics(ref this.mMechanics);
        int count = this.mMechanics.Count;
        for (int index = 0; index < count; ++index)
        {
            Mechanic mMechanic = this.mMechanics[index];
            foreach (Driver driver in mMechanic.GetDrivers())
            {
                if (driver == inDriver)
                    return mMechanic;
            }
        }
        return (Mechanic)null;
    }

    public int GetDriverIndex(Driver inDriver)
    {
        this.mEmployeeSlots.Clear();
        this.contractManager.GetAllEmployeeSlotsForJob(Contract.Job.Driver, ref this.mEmployeeSlots);
        if (inDriver != null && inDriver.IsReserveDriver())
            return -1;
        for (int index = 0; index < Team.maxDriverCount; ++index)
        {
            if (this.mEmployeeSlots[index].personHired != null && this.mEmployeeSlots[index].personHired == inDriver)
                return index;
        }
        return -1;
    }

    public void AssignDriverToCar(Driver inDriver)
    {
        if (this.championship.series != Championship.Series.EnduranceSeries)
            return;
        inDriver.SetCarID(this.GetDriversForCar(0).Length >= 3 ? 1 : 0);
    }
}
