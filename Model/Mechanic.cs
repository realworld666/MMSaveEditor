
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
    private MechanicStats stats = new MechanicStats();
    public MechanicStats lastAccumulatedStats = new MechanicStats();
    public int driver;
    public float improvementRate;
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
    public const float minPitStopAddedTime = 0.0f;
    public const float maxPitStopAddedTime = 2f;
    public const float minPitStopAddedError = 0.0f;
    public const float maxPitStopAddedError = 0.1f;
    public float driverRelationshipAmountBeforeEvent;

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
}
