
using System.Xml.Serialization;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class DriverPerformanceDesignData : PerformanceDesignData
{
    [XmlElement("MaxDriverTimeCostWithDriverAidsOn")]
    public float maxDriverTimeCostWithDriverAidsOn;
    [XmlElement("MaxDriverTimeCostWithDriverAidsOff")]
    public float maxDriverTimeCostWithDriverAidsOff;
}
