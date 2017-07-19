using FullSerializer;
using System.Xml.Serialization;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class TyrePerformanceDesignData
{
    [XmlElement("DriverSmoothnessLapCountIncrease")]
    public int driverSmoothnessLapCountIncrease = 1;
    [XmlElement("DriverSmoothnessLapCountDecrease")]
    public int driverSmoothnessLapCountDecrease = 1;
    [XmlElement("CircuitHighTyreWearLapCountDecrease")]
    public int circuitHighTyreWearLapCountDecrease = 1;
    [XmlElement("CircuitLowTyreWearLapCountIncrease")]
    public int circuitLowTyreWearLapCountIncrease = 1;
    [XmlElement("HighPerformanceLapCount")]
    public int highPerformanceLapCount;
    [XmlElement("HighPerformanceTimeCost")]
    public float highPerformanceTimeCost;
    [XmlElement("MediumPerformanceLapCount")]
    public int mediumPerformanceLapCount;
    [XmlElement("MediumPerformanceTimeCost")]
    public float mediumPerformanceTimeCost;
    [XmlElement("LowPerformanceLapCount")]
    public int lowPerformanceLapCount;
    [XmlElement("LowPerformanceTimeCost")]
    public float lowPerformanceTimeCost;
}
