using FullSerializer;
using System.Xml.Serialization;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class DriverFormDesignData : PerformanceDesignData
{
    [XmlElement( "MaxDriverFormTimeCost" )]
    public float maxDriverFormTimeCost;
}
