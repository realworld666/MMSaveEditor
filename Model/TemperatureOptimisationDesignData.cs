using FullSerializer;
using System.Xml.Serialization;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class TemperatureOptimisationDesignData : PerformanceDesignData
{
    [XmlElement( "MaxTemperatureOptimisationTimeCost" )]
    public float maxTemperatureOptimisationTimeCost = 0.5f;
}
