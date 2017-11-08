using System.Xml.Serialization;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SetupDesignData : PerformanceDesignData
{
    [XmlElement("MaxSetupTimeCost")]
    public float maxSetupTimeCost = 2f;
    [XmlElement("SafePitstopStrategyTimeCost")]
    public float safePitstopStrategyTimeCost = 4f;
    [XmlElement("BalancedPitstopStrategyTimeCost")]
    public float balancedPitstopStrategyTimeCost = 2f;
    [XmlElement("PitstopTimeCostPerLapOfFuel")]
    public float pitstopTimeCostPerLapOfFuel = 0.7f;
    [XmlElement("PitstopTyreChangeTimeCost")]
    public float pitstopTyreChangeTimeCost = 3f;
    [XmlElement("SetupChangeTimeCost")]
    public float setupChangeTimeCost = 5f;
    [XmlElement("TrimChangeTimeCost")]
    public float trimChangeTimeCost = 10f;
    [XmlElement("DriverChangeTimeCost")]
    public float driverChangeTimeCost = 40f;
    [XmlElement("FastPitstopStrategyTimeCost")]
    public float fastPitstopStrategyTimeCost;
}
