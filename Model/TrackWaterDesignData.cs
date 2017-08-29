using FullSerializer;
using System.Xml.Serialization;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class TrackWaterDesignData : PerformanceDesignData
{
    [XmlElement("MaxWetTrackTimeCost")]
    public float maxWetTrackTimeCost = 12f;
    [XmlElement("MaxWetSetupPerformanceTimeCost")]
    public float maxWetSetupPerformanceTimeCost = 2f;
    [XmlElement("NoRubberOnTrackTimeCost")]
    public float noRubberOnTrackTimeCost = 0.5f;
}
