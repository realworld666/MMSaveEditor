using FullSerializer;
using System.Xml.Serialization;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class DesignData
{
    [XmlElement("EnergyRecoverySystem")]
    public ERSDesignData ERSDesignData = new ERSDesignData();
    [XmlElement("CarPerformance")]
    public CarPerformanceDesignData carPerformance = new CarPerformanceDesignData();
    [XmlElement("CarChassis")]
    public CarChassisDesignData carChassis = new CarChassisDesignData();
    [XmlElement("FirstOptionTyres")]
    public TyrePerformanceDesignData firstOptionTyres = new TyrePerformanceDesignData();
    [XmlElement("SecondOptionTyres")]
    public TyrePerformanceDesignData secondOptionTyres = new TyrePerformanceDesignData();
    [XmlElement("ThirdOptionTyres")]
    public TyrePerformanceDesignData thirdOptionTyres = new TyrePerformanceDesignData();
    [XmlElement("IntermediatesTyres")]
    public TyrePerformanceDesignData intermediatesTyres = new TyrePerformanceDesignData();
    [XmlElement("WetTyres")]
    public TyrePerformanceDesignData wetTyres = new TyrePerformanceDesignData();
    [XmlElement("PitCrew")]
    public PitCrewDesignData pitCrew = new PitCrewDesignData();
    [XmlElement("WeatherBarsForecastCount")]
    public int weatherBarsForecastCount = 20;
}
