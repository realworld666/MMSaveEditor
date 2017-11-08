using FullSerializer;
using System.Xml.Serialization;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class DriverFormDesignData : PerformanceDesignData
{
    [XmlElement("MaxDriverFormTimeCost")]
    public float maxDriverFormTimeCost;
    [XmlElement("TotalUpdateChunks")]
    public int totalUpdateChunks;
    [XmlElement("TotalUpdateChunksQualifying")]
    public int totalUpdateChunksQualifying;
    [XmlElement("ConsistencyMaxFormLoss")]
    public float consistencyMaxFormLoss;
    [XmlElement("ConsistencyMinFormLoss")]
    public float consistencyMinFormLoss;
}
