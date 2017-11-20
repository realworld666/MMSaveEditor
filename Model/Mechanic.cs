
using FullSerializer;
using System;
using System.Collections.Generic;
using MMSaveEditor.Utils;
using GalaSoft.MvvmLight.Command;
using MMSaveEditor.ViewModel;
using GalaSoft.MvvmLight.Ioc;
using MMSaveEditor.View;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class Mechanic : Person
{
    public MechanicStats stats = new MechanicStats();
    public MechanicStats lastAccumulatedStats = new MechanicStats();
    public int driver;
    public float improvementRate = RandomUtility.GetRandom(0.1f, 1f);
    public MechanicBonus bonusOne;
    public MechanicBonus bonusTwo;
    private Map<string, Mechanic.DriverRelationship> mDriversRelationships;
    private Map<string, StatModificationHistory> mRelationshipModificationHistory;
    private Dictionary<string, Mechanic.DriverRelationship> mDictDriversRelationships = new Dictionary<string, Mechanic.DriverRelationship>();
    private Dictionary<string, StatModificationHistory> mDictRelationshipModificationHistory = new Dictionary<string, StatModificationHistory>();
    private readonly float weeklyRelationshipIncreaseRate = 2f;
    private readonly float weeklyRelationshipDecreaseRate = 2f;
    private readonly float endRaceRelationshipIncreaseRate = 15f;
    private readonly float endRaceRelationshipDecreaseRate = 15f;
    private readonly float positionRange = 5f;
    private readonly float negativeImprovementHQScalar = 0.9f;
    private readonly float negativeImprovementHQOverallScalar = 0.03f;
    private readonly float negativeMaxImprovementHQ = 0.75f;
    private readonly float maxMechanicRelationshipDecayPercent = 0.5f;
    private readonly float mechanicRelationshipInvalidDecay = -1f;
    public const float minPitStopAddedError = 0.0f;
    public const float maxPitStopAddedError = 0.1f;
    public float driverRelationshipAmountBeforeEvent;
    private List<Driver> mRelationshipDriversCache = new List<Driver>();

    public RelayCommand<Mechanic> ViewDriver { get; private set; }

    public MechanicStats Stats
    {
        get
        {
            return stats;
        }

        set
        {
            stats = value;
        }
    }

    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public class DriverRelationship
    {
        public float relationshipAmount;
        public float relationshipAmountAfterDecay = -1f;
        public int numberOfWeeks;

        public float RelationshipAmount
        {
            get
            {
                return relationshipAmount;
            }

            set
            {
                relationshipAmount = value;
            }
        }

        public float RelationshipAmountAfterDecay
        {
            get
            {
                return relationshipAmountAfterDecay;
            }

            set
            {
                relationshipAmountAfterDecay = value;
            }
        }

        public int NumberOfWeeks
        {
            get
            {
                return numberOfWeeks;
            }

            set
            {
                numberOfWeeks = value;
            }
        }
    }

    public Dictionary<string, Mechanic.DriverRelationship> allDriverRelationships
    {
        get
        {
            return this.mDictDriversRelationships;
        }
    }

    public Mechanic()
    {
        ViewDriver = new RelayCommand<Mechanic>(_viewDriver);
    }
    private void _viewDriver(Mechanic d)
    {
        var driverVM = SimpleIoc.Default.GetInstance<MechanicViewModel>();
        driverVM.SetModel(this);
        MainWindow.Instance.SwitchToTab(MainWindow.TabPage.Mechanic);
    }

    public void DriverRenamed(string oldname, string newName)
    {
        mDictDriversRelationships.RenameKey(oldname, newName);
        mDictRelationshipModificationHistory.RenameKey(oldname, newName);
    }

    public override bool IsReplacementPerson()
    {
        return Game.instance.mechanicManager.IsReplacementPerson(this);
    }

    public void SetDriverRelationship(float inRelationshipAmount, int inWeeksTogether)
    {
        Driver driver = this.Contract.GetTeam().GetDriver(this.driver);
        if (this.mDictDriversRelationships.ContainsKey(driver.name))
            return;
        this.mDictDriversRelationships.Add(driver.name, new Mechanic.DriverRelationship()
        {
            numberOfWeeks = inWeeksTogether,
            relationshipAmount = inRelationshipAmount
        });
        this.mDictRelationshipModificationHistory.Add(driver.name, new StatModificationHistory());
    }

    public void SetDriverRelationship(float inRelationshipAmount, int inWeeksTogether, string inDriverName = null)
    {
        string str = !string.IsNullOrEmpty(inDriverName) ? inDriverName : this.Contract.GetTeam().GetDriver(this.driver).name;
        if (!this.mDictDriversRelationships.ContainsKey(str))
        {
            this.mDictDriversRelationships.Add(str, new Mechanic.DriverRelationship()
            {
                numberOfWeeks = inWeeksTogether,
                relationshipAmount = inRelationshipAmount
            });
            if (this.mDictRelationshipModificationHistory.ContainsKey(str))
                return;
            this.mDictRelationshipModificationHistory.Add(str, new StatModificationHistory());
        }
        else
        {
            if (this.mDriversRelationships == null)
                return;
            Mechanic.DriverRelationship map = this.mDriversRelationships.GetMap(str);
            map.numberOfWeeks = inWeeksTogether;
            map.relationshipAmount = inRelationshipAmount;
        }
    }

    public Mechanic.DriverRelationship GetRelationshipWithDriver(Driver inDriver)
    {
        if (!this.mDictDriversRelationships.ContainsKey(inDriver.name))
            return (Mechanic.DriverRelationship)null;
        return this.mDictDriversRelationships[inDriver.name];
    }

    public Driver[] GetDrivers()
    {
        if (this.IsFreeAgent())
            return (Driver[])null;
        this.mRelationshipDriversCache.Clear();
        Team team = this.contract.GetTeam();
        if (team.championship.series == Championship.Series.EnduranceSeries)
            return team.GetDriversForCar(this.driver);
        this.mRelationshipDriversCache.Add(team.GetDriver(this.driver));
        return this.mRelationshipDriversCache.ToArray();
    }

    internal void SetDefaultDriverRelationship()
    {
        foreach (Entity driver in this.GetDrivers())
            this.GenerateDriverRelationship(driver.name, 0, 0.0f);
    }

    private Mechanic.DriverRelationship GenerateDriverRelationship(string inDriverName, int inWeeksTogether, float inRelationshipAmount)
    {
        if (string.IsNullOrEmpty(inDriverName) || this.mDictDriversRelationships.ContainsKey(inDriverName))
            return (Mechanic.DriverRelationship)null;
        Mechanic.DriverRelationship driverRelationship = new Mechanic.DriverRelationship();
        driverRelationship.numberOfWeeks = inWeeksTogether;
        driverRelationship.relationshipAmount = inRelationshipAmount;
        this.mDictDriversRelationships.Add(inDriverName, driverRelationship);
        if (!this.mDictRelationshipModificationHistory.ContainsKey(inDriverName))
            this.mDictRelationshipModificationHistory.Add(inDriverName, new StatModificationHistory());
        return driverRelationship;
    }
}
