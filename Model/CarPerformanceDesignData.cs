using FullSerializer;
using System.Xml.Serialization;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class CarPerformanceDesignData : PerformanceDesignData
{
    [XmlElement("Bonuses")]
    public BonusesDesignData bonuses = new BonusesDesignData();
    [XmlElement("CarCondition")]
    public CarConditionDesignData carCondition = new CarConditionDesignData();
    [XmlElement("CriticalCarPart")]
    public CriticalCarPartDesignData criticalCarPart = new CriticalCarPartDesignData();
    [XmlElement("DriverForm")]
    public DriverFormDesignData driverForm = new DriverFormDesignData();
    [XmlElement("DriverPerformance")]
    public DriverPerformanceDesignData driverPerformance = new DriverPerformanceDesignData();
    [XmlElement("DrivingStyle")]
    public DrivingStyleDesignData drivingStyle = new DrivingStyleDesignData();
    [XmlElement("Fuel")]
    public FuelDesignData fuel = new FuelDesignData();
    [XmlElement("Setup")]
    public SetupDesignData setup = new SetupDesignData();
    [XmlElement("TemperatureOptimisation")]
    public TemperatureOptimisationDesignData temperatureOptimisation = new TemperatureOptimisationDesignData();
    [XmlElement("TrackExpert")]
    public TrackExpertDesignData trackExpert = new TrackExpertDesignData();
    [XmlElement("TrackWater")]
    public TrackWaterDesignData trackWater = new TrackWaterDesignData();
    [XmlElement("BehaviourType")]
    public BehaviourTypeDesignData behaviourType = new BehaviourTypeDesignData();
}
