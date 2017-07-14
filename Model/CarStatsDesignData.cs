using System.Xml.Serialization;

public class CarStatsDesignData
{
    [XmlAttribute( "isActive" )]
    public bool isActive = true;
    [XmlElement( "TopSpeedMph" )]
    public float topSpeedMph = 250f;
    [XmlElement( "Acceleration" )]
    public float acceleration = 15f;
    [XmlElement( "Braking" )]
    public float braking = 40f;
    [XmlElement( "LowSpeedCorners" )]
    public float lowSpeedCorners = 250f;
    [XmlElement( "MediumSpeedCorners" )]
    public float mediumSpeedCorners = 15f;
    [XmlElement( "HighSpeedCorners" )]
    public float highSpeedCorners = 40f;
    [XmlElement( "TopSpeedPerformanceScalar" )]
    public float topSpeedPerformanceScalar = 1f;
    [XmlElement( "AccelerationPerformanceScalar" )]
    public float accelerationPerformanceScalar = 1f;
    [XmlElement( "BrakingPerformanceScalar" )]
    public float brakingPerformanceScalar = 1f;
    [XmlElement( "CorneringPerformanceScalar" )]
    public float corneringPerformanceScalar = 1f;
    [XmlElement( "MinPerformanceTimeCost" )]
    public float minPerformanceTimeCost;
    [XmlElement( "Tier2TimeCost" )]
    public float tier2TimeCost;
    [XmlElement( "Tier3TimeCost" )]
    public float tier3TimeCost;
}
