using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class CarPartDesign
{
    private CarPartDesign.Stage mStage;
    private CarPart mCarPart;
    private CarPart mLastCarPart;
    private bool mBuildTwoCopies;
    public List<CarPartComponent> componentSlots = new List<CarPartComponent>();
    public List<CarPartComponent> componentBonusSlots = new List<CarPartComponent>();
    public List<int> componentBonusSlotsLevel = new List<int>();
    public DateTime startDate;
    public DateTime endDate;
    private bool mAllPartsUnlocked;
    private float mComponentTimeDaysBonus;
    private Notification mNotification;
    private CalendarEvent_v1 mCalendarEvent;
    private int mExtraCopies;
    private Team mTeam;
    private CarPartComponent mRandomComponent;

    private Dictionary<CarPart.PartType, int> seasonPartStartingStat = new Dictionary<CarPart.PartType, int>();
    private Dictionary<int, List<CarPartComponent>> brakeComponents = new Dictionary<int, List<CarPartComponent>>();
    private Dictionary<int, List<CarPartComponent>> engineComponents = new Dictionary<int, List<CarPartComponent>>();
    private Dictionary<int, List<CarPartComponent>> frontWingComponents = new Dictionary<int, List<CarPartComponent>>();
    private Dictionary<int, List<CarPartComponent>> rearWingComponents = new Dictionary<int, List<CarPartComponent>>();
    private Dictionary<int, List<CarPartComponent>> suspensionComponents = new Dictionary<int, List<CarPartComponent>>();
    private Dictionary<int, List<CarPartComponent>> gearboxComponents = new Dictionary<int, List<CarPartComponent>>();
    private Dictionary<int, List<CarPartComponent>> engineGTComponents = new Dictionary<int, List<CarPartComponent>>();
    private Dictionary<int, List<CarPartComponent>> rearWingGTComponents = new Dictionary<int, List<CarPartComponent>>();
    private Dictionary<int, List<CarPartComponent>> brakeGTComponents = new Dictionary<int, List<CarPartComponent>>();
    private Dictionary<int, List<CarPartComponent>> suspensionGTComponents = new Dictionary<int, List<CarPartComponent>>();
    private Dictionary<int, List<CarPartComponent>> gearboxGTComponents = new Dictionary<int, List<CarPartComponent>>();
    private Dictionary<int, List<CarPartComponent>> brakeComponentsGET = new Dictionary<int, List<CarPartComponent>>();
    private Dictionary<int, List<CarPartComponent>> engineComponentsGET = new Dictionary<int, List<CarPartComponent>>();
    private Dictionary<int, List<CarPartComponent>> frontWingComponentsGET = new Dictionary<int, List<CarPartComponent>>();
    private Dictionary<int, List<CarPartComponent>> rearWingComponentsGET = new Dictionary<int, List<CarPartComponent>>();
    private Dictionary<int, List<CarPartComponent>> suspensionComponentsGET = new Dictionary<int, List<CarPartComponent>>();
    private Dictionary<int, List<CarPartComponent>> gearboxComponentsGET = new Dictionary<int, List<CarPartComponent>>();
    public Action OnDesignModified;
    public Action OnPartBuilt;

    private bool mImidiateFinish;
    public Team team
    {
        get
        {
            return this.mTeam;
        }
    }

    private void SetBaseStats(CarPart inPart)
    {
        Engineer personOnJob = (Engineer)this.mTeam.contractManager.GetPersonOnJob(Contract.Job.EngineerLead);
        inPart.stats = new CarPartStats(inPart);
        float inValue = (float)(this.seasonPartStartingStat[inPart.GetPartType()] + (int)Math.Floor(personOnJob.Stats.PartContributionStats.GetStat(inPart.stats.statType)));
        inPart.stats.level = this.GetLevelFromComponents(inPart);
        inPart.stats.maxPerformance = this.mTeam.carManager.GetCar(0).chassisStats.improvability * 2f;
        inPart.stats.SetStat(CarPartStats.CarPartStat.Reliability, GameStatsConstants.initialReliabilityValue);
        inPart.stats.maxReliability = GameStatsConstants.initialMaxReliabilityValue;
        inPart.stats.partCondition.redZone = GameStatsConstants.initialRedZone;
        inPart.stats.SetStat(CarPartStats.CarPartStat.MainStat, inValue);
    }

    private int GetLevelFromComponents(CarPart inPart)
    {
        int num = 0;
        for (int index = 0; index < inPart.components.Count; ++index)
        {
            CarPartComponent component = inPart.components[index];
            if (component != null)
                num += component.level;
        }
        if (num >= 15)
            return 5;
        if (num >= 10)
            return 4;
        if (num >= 6)
            return 3;
        if (num >= 3)
            return 2;
        return num >= 1 ? 1 : 0;
    }

    public void ApplyComponents(CarPart inPart)
    {
        this.mComponentTimeDaysBonus = 0f;
        this.SetBaseStats(inPart);
        inPart.AddAllComponentStats();
        inPart.components.ForEach(delegate (CarPartComponent component)
        {
            if (component != null)
            {
                component.ApplyBonus(this, inPart);
            }
        });
        if (inPart.stats.GetStat(CarPartStats.CarPartStat.MainStat) < 1f)
        {
            inPart.stats.SetStat(CarPartStats.CarPartStat.MainStat, 1f);
        }
    }

    public void DesignModified()
    {
        if (this.OnDesignModified == null)
            return;
        this.OnDesignModified();
    }

    public enum Stage
    {
        Idle,
        Designing,
    }
}
