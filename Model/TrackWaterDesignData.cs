using FullSerializer;
using System.Xml.Serialization;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class TrackWaterDesignData : PerformanceDesignData
{
    [XmlElement( "MaxWetTrackTimeCost" )]
    public float maxWetTrackTimeCost = 12f;
}
