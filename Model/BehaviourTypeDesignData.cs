using FullSerializer;
using System.Xml.Serialization;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class BehaviourTypeDesignData : PerformanceDesignData
{
    [XmlElement("MaxRunWideTimeCost")]
    public float maxRunWideTimeCost = 5f;
    [XmlElement("MaxCutCornerTimeCost")]
    public float maxCutCornerTimeCost = 1f;
}
