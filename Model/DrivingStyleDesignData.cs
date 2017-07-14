using FullSerializer;
using System.Xml.Serialization;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class DrivingStyleDesignData : PerformanceDesignData
{
    [XmlElement( "MaxDrivingStyleTimeCost" )]
    public float maxDrivingStyleTimeCost = 1f;
    [XmlElement( "AttackPerformance" )]
    public float attackPerformance = 1f;
    [XmlElement( "AttackTyreWearRate" )]
    public float attackTyreWearRate = 1.2f;
    [XmlElement( "PushPerformance" )]
    public float pushPerformance = 0.75f;
    [XmlElement( "PushTyreWearRate" )]
    public float pushTyreWearRate = 1.1f;
    [XmlElement( "NeutralPerformance" )]
    public float neutralPerformance = 0.5f;
    [XmlElement( "NeutralTyreWearRate" )]
    public float neutralTyreWearRate = 1f;
    [XmlElement( "ConservePerformance" )]
    public float conservePerformance = 0.25f;
    [XmlElement( "ConserveTyreWearRate" )]
    public float conserveTyreWearRate = 0.9f;
    [XmlElement( "BackUpTyreWearRate" )]
    public float backUpTyreWearRate = 0.8f;
    [XmlElement( "BackUpPerformance" )]
    public float backUpPerformance;
}
