using FullSerializer;
using System.Xml.Serialization;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class CriticalCarPartDesignData : PerformanceDesignData
{
    [XmlArrayItem( "TimeCostForRank" )]
    [XmlArray( "CriticalPartTimeCosts" )]
    public float[] timeCostForRank = new float[20];
}
