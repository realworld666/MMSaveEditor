
using System.Xml.Serialization;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class DriverPerformanceDesignData : PerformanceDesignData
{
    [XmlElement("MaxDriverTimeCostWithDriverAidsOn")]
    public float maxDriverTimeCostWithDriverAidsOn;
    [XmlElement("MaxDriverTimeCostWithDriverAidsOff")]
    public float maxDriverTimeCostWithDriverAidsOff;
    [XmlElement("MaxDriverStaminaTimeCost")]
    public float maxDriverStaminaTimeCost;
    [XmlElement("InTheZoneTimeCost")]
    public float inTheZoneTimeCost;
    [XmlElement("StaminaDecreaseRate")]
    public float staminaDecreaseRate;
    [XmlElement("StaminaRestingRate")]
    public float staminaRestingRate;
    [XmlElement("DriverSetupPerformanceTimeCost")]
    public float driverSetupPerformanceTimeCost;
}
