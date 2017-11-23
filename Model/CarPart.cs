using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class CarPart : Entity
{
    private static readonly CarPart.PartType[] singleSeaterSeries = new CarPart.PartType[6] { CarPart.PartType.Brakes, CarPart.PartType.Engine, CarPart.PartType.FrontWing, CarPart.PartType.Gearbox, CarPart.PartType.RearWing, CarPart.PartType.Suspension };
    private static readonly CarPart.PartType[] singleSeaterSeriesStatOrdered = new CarPart.PartType[6] { CarPart.PartType.Engine, CarPart.PartType.Gearbox, CarPart.PartType.Brakes, CarPart.PartType.FrontWing, CarPart.PartType.Suspension, CarPart.PartType.RearWing };
    private static readonly CarPart.PartType[] gtSeries = new CarPart.PartType[5] { CarPart.PartType.BrakesGT, CarPart.PartType.EngineGT, CarPart.PartType.GearboxGT, CarPart.PartType.RearWingGT, CarPart.PartType.SuspensionGT };
    private static readonly CarPart.PartType[] gtSeriesStatOrdered = new CarPart.PartType[5] { CarPart.PartType.EngineGT, CarPart.PartType.GearboxGT, CarPart.PartType.BrakesGT, CarPart.PartType.SuspensionGT, CarPart.PartType.RearWingGT };
    private static readonly CarPart.PartType[] enduranceSeries = new CarPart.PartType[6] { CarPart.PartType.BrakesGET, CarPart.PartType.EngineGET, CarPart.PartType.FrontWingGET, CarPart.PartType.GearboxGET, CarPart.PartType.RearWingGET, CarPart.PartType.SuspensionGET };
    private static readonly CarPart.PartType[] enduranceSeriesStatOrdered = new CarPart.PartType[6] { CarPart.PartType.EngineGET, CarPart.PartType.GearboxGET, CarPart.PartType.BrakesGET, CarPart.PartType.FrontWingGET, CarPart.PartType.SuspensionGET, CarPart.PartType.RearWingGET };
    public static string[] PartNames = new string[12] { "Brakes", "Engine", "FrontWing", "Gearbox", "RearWing", "Suspension", "RearWingGT", "BrakesGT", "EngineGT", "GearboxGT", "SuspensionGT", "Count" };
    public Action OnStageChange;
    public DateTime buildDate;
    public Car fittedCar;
    public bool isFitted;
    public bool isBanned;
    public List<CarPartComponent> components = new List<CarPartComponent>();
    private CarPartStats mStats;
    private int mModelId;

    public CarPartStats stats
    {
        get
        {
            return this.mStats;
        }
        set
        {
            this.mStats = value;
        }
    }

    public float reliability
    {
        get
        {
            return this.mStats.reliability;
        }
    }

    public bool IsBanned
    {
        get
        {
            return isBanned;
        }

        set
        {
            isBanned = value;
        }
    }

    public void AddAllComponentStats()
    {
        for (int index = 0; index < this.components.Count; ++index)
        {
            CarPartComponent component = this.components[index];
            if (component != null)
                this.AddComponentStats(component);
        }
    }

    public void AddComponentStats(CarPartComponent inComponent)
    {
        inComponent.ApplyStats(this);
    }

    public virtual CarPart.PartType GetPartType()
    {
        return CarPart.PartType.Brakes;
    }

    public static CarPart.PartType[] GetPartType(Championship.Series inSeries, bool statOrdered = false)
    {
        if (inSeries == Championship.Series.GTSeries)
        {
            if (statOrdered)
                return CarPart.gtSeriesStatOrdered;
            return CarPart.gtSeries;
        }
        if (statOrdered)
            return CarPart.singleSeaterSeriesStatOrdered;
        return CarPart.singleSeaterSeries;
    }


    public enum PartType
    {
        None = -1,
        [LocalisationID("PSG_10001657")] Brakes = 0,
        [LocalisationID("PSG_10001653")] Engine = 1,
        [LocalisationID("PSG_10001651")] FrontWing = 2,
        [LocalisationID("PSG_10001654")] Gearbox = 3,
        [LocalisationID("PSG_10001652")] RearWing = 4,
        [LocalisationID("PSG_10001655")] Suspension = 5,
        [LocalisationID("PSG_10011533")] RearWingGT = 6,
        [LocalisationID("PSG_10001657")] BrakesGT = 7,
        [LocalisationID("PSG_10001653")] EngineGT = 8,
        [LocalisationID("PSG_10001654")] GearboxGT = 9,
        [LocalisationID("PSG_10001655")] SuspensionGT = 10,
        [LocalisationID("PSG_10001657")] BrakesGET = 11,
        [LocalisationID("PSG_10001653")] EngineGET = 12,
        [LocalisationID("PSG_10012673")] FrontWingGET = 13,
        [LocalisationID("PSG_10001654")] GearboxGET = 14,
        [LocalisationID("PSG_10001652")] RearWingGET = 15,
        [LocalisationID("PSG_10001655")] SuspensionGET = 16,
        Last = 17,
    }

    public enum PartTypePlural
    {
        [LocalisationID("PSG_10008535")] Brakes,
        [LocalisationID("PSG_10008532")] Engine,
        [LocalisationID("PSG_10008530")] FrontWing,
        [LocalisationID("PSG_10008533")] Gearbox,
        [LocalisationID("PSG_10008531")] RearWing,
        [LocalisationID("PSG_10008534")] Suspension,
        [LocalisationID("PSG_10012157")] RearWingGT,
        [LocalisationID("PSG_10008535")] BrakesGT,
        [LocalisationID("PSG_10008532")] EngineGT,
        [LocalisationID("PSG_10008533")] GearboxGT,
        [LocalisationID("PSG_10008534")] SuspensionGT,
        [LocalisationID("PSG_10008535")] BrakesGET,
        [LocalisationID("PSG_10008532")] EngineGET,
        [LocalisationID("PSG_10012673")] FrontWingGET,
        [LocalisationID("PSG_10008533")] GearboxGET,
        [LocalisationID("PSG_10008531")] RearWingGET,
        [LocalisationID("PSG_10008534")] SuspensionGET,
        Count,
    }

    public static CarStats.StatType GetStatForPartType(CarPart.PartType inPartType)
    {
        switch (inPartType)
        {
            case CarPart.PartType.Brakes:
            case CarPart.PartType.BrakesGT:
                return CarStats.StatType.Braking;
            case CarPart.PartType.Engine:
            case CarPart.PartType.EngineGT:
                return CarStats.StatType.TopSpeed;
            case CarPart.PartType.FrontWing:
                return CarStats.StatType.LowSpeedCorners;
            case CarPart.PartType.Gearbox:
            case CarPart.PartType.GearboxGT:
                return CarStats.StatType.Acceleration;
            case CarPart.PartType.RearWing:
            case CarPart.PartType.RearWingGT:
                return CarStats.StatType.HighSpeedCorners;
            case CarPart.PartType.Suspension:
            case CarPart.PartType.SuspensionGT:
                return CarStats.StatType.MediumSpeedCorners;
            default:
                return CarStats.StatType.Count;
        }
    }

    public static CarPart.PartType GetPartForStatType(CarStats.StatType inStat, Championship.Series inSeries)
    {
        switch (inStat)
        {
            case CarStats.StatType.TopSpeed:
                return inSeries == Championship.Series.SingleSeaterSeries ? CarPart.PartType.Engine : CarPart.PartType.EngineGT;
            case CarStats.StatType.Acceleration:
                return inSeries == Championship.Series.SingleSeaterSeries ? CarPart.PartType.Gearbox : CarPart.PartType.GearboxGT;
            case CarStats.StatType.Braking:
                return inSeries == Championship.Series.SingleSeaterSeries ? CarPart.PartType.Brakes : CarPart.PartType.BrakesGT;
            case CarStats.StatType.LowSpeedCorners:
                return inSeries == Championship.Series.SingleSeaterSeries ? CarPart.PartType.FrontWing : CarPart.PartType.None;
            case CarStats.StatType.MediumSpeedCorners:
                return inSeries == Championship.Series.SingleSeaterSeries ? CarPart.PartType.Suspension : CarPart.PartType.SuspensionGT;
            case CarStats.StatType.HighSpeedCorners:
                return inSeries == Championship.Series.SingleSeaterSeries ? CarPart.PartType.RearWing : CarPart.PartType.RearWingGT;
            default:
                return CarPart.PartType.None;
        }
    }

    public void StatsUpdated()
    {
        //fittedCar.carManager.carPartDesign.ApplyComponents(this);
        //OnPartBuilt(fittedCar?.carManager?.carPartDesign);
        //fittedCar.carManager.carPartDesign.DesignModified();
    }

    public void OnPartBuilt(CarPartDesign inDesign)
    {
        this.components.ForEach(delegate (CarPartComponent component)
        {
            if (component != null)
            {
                component.OnPartBuilt(inDesign, this);
            }
        });
    }

    public static CarPart CreatePartEntity(CarPart.PartType inType, Championship inChampionship)
    {
        CarPart carPart = (CarPart)null;
        switch (inType)
        {
            case CarPart.PartType.Brakes:
                carPart = (CarPart)Game.instance.entityManager.CreateEntity<BrakesPart>();
                break;
            case CarPart.PartType.Engine:
                carPart = (CarPart)Game.instance.entityManager.CreateEntity<EnginePart>();
                break;
            case CarPart.PartType.FrontWing:
                carPart = (CarPart)Game.instance.entityManager.CreateEntity<FrontWingPart>();
                break;
            case CarPart.PartType.Gearbox:
                carPart = (CarPart)Game.instance.entityManager.CreateEntity<GearboxPart>();
                break;
            case CarPart.PartType.RearWing:
                carPart = (CarPart)Game.instance.entityManager.CreateEntity<RearWingPart>();
                break;
            case CarPart.PartType.Suspension:
                carPart = (CarPart)Game.instance.entityManager.CreateEntity<SuspensionPart>();
                break;
            case CarPart.PartType.RearWingGT:
                carPart = (CarPart)Game.instance.entityManager.CreateEntity<RearWingGTPart>();
                break;
            case CarPart.PartType.BrakesGT:
                carPart = (CarPart)Game.instance.entityManager.CreateEntity<BrakesGTPart>();
                break;
            case CarPart.PartType.EngineGT:
                carPart = (CarPart)Game.instance.entityManager.CreateEntity<EngineGTPart>();
                break;
            case CarPart.PartType.GearboxGT:
                carPart = (CarPart)Game.instance.entityManager.CreateEntity<GearBoxGTPart>();
                break;
            case CarPart.PartType.SuspensionGT:
                carPart = (CarPart)Game.instance.entityManager.CreateEntity<SuspensionGTPart>();
                break;
            case CarPart.PartType.BrakesGET:
                carPart = (CarPart)Game.instance.entityManager.CreateEntity<BrakesGETPart>();
                break;
            case CarPart.PartType.EngineGET:
                carPart = (CarPart)Game.instance.entityManager.CreateEntity<EngineGETPart>();
                break;
            case CarPart.PartType.FrontWingGET:
                carPart = (CarPart)Game.instance.entityManager.CreateEntity<FrontWingGETPart>();
                break;
            case CarPart.PartType.GearboxGET:
                carPart = (CarPart)Game.instance.entityManager.CreateEntity<GearboxGETPart>();
                break;
            case CarPart.PartType.RearWingGET:
                carPart = (CarPart)Game.instance.entityManager.CreateEntity<RearWingGETPart>();
                break;
            case CarPart.PartType.SuspensionGET:
                carPart = (CarPart)Game.instance.entityManager.CreateEntity<SuspensionGETPart>();
                break;
        }
        carPart.Setup(inChampionship);
        return carPart;
    }

    public void Setup(Championship inChampionship)
    {
        this.stats = new CarPartStats(this);
        this.name = ((int)this.GetPartName()[0]).ToString() + "-" + CarPart.GenerateName();
    }

    private static string GenerateName()
    {
        string str = string.Empty + (object)(char)(65 + RandomUtility.GetRandom(0, 26));
        int num = 3;
        while (num > 0)
        {
            --num;
            str += ((char)(65 + RandomUtility.GetRandom(0, 26))).ToString();
        }
        return str;
    }

    public virtual string GetPartName()
    {
        return string.Empty;
    }

    public void PostStatsSetup(Championship inChampionship)
    {
        this.PickModel(inChampionship);
    }

    private void PickModel(Championship inChampionship)
    {
        //throw new NotImplementedException();
    }

    public void DestroyCarPart()
    {
        // Notification notification = Game.instance.notificationManager.GetNotification(this.name);
        // if (notification != null)
        //     notification.ResetCount();
        Game.instance.entityManager.DestroyEntity((Entity)this);
    }
}
