using FullSerializer;
using System.Xml.Serialization;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class TrackExpertDesignData : PerformanceDesignData
{
    [XmlElement( "MaxTrackExpertTimeCost" )]
    public float maxTrackExpertTimeCost = 0.4f;
}
