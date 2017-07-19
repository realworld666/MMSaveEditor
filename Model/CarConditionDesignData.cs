using FullSerializer;
using System.Xml.Serialization;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class CarConditionDesignData : PerformanceDesignData
{
    [XmlElement("BrakeFailureTimeCost")]
    public float brakeFailureTimeCost;
    [XmlElement("EngineFailureTimeCost")]
    public float engineFailureTimeCost;
    [XmlElement("FrontWingFailureTimeCost")]
    public float frontWingFailureTimeCost;
    [XmlElement("GearboxFailureTimeCost")]
    public float gearboxFailureTimeCost;
    [XmlElement("RearWingFailureTimeCost")]
    public float rearWingFailureTimeCost;
    [XmlElement("SuspensionFailureTimeCost")]
    public float suspensionFailureTimeCost;
}
