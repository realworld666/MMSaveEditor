using System.Xml.Serialization;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class BonusesDesignData : PerformanceDesignData
{
    [XmlElement("LowFuelBurnRateBonus")]
    public float lowFuelBurnRateBonus = 0.95f;
    [XmlElement("HighFuelBurnRateBonus")]
    public float highFuelBurnRateBonus = 1.05f;
    [XmlElement("SessionBonusTotalTimeCost")]
    public float totalTimeCost = 1f;
    [XmlElement("SessionBonusTimeCostReduction")]
    public float timeCostBonusReduction = 0.1f;
    [XmlElement("MechanicBonusTyreWearRate")]
    public float mechanicBonusTyreWearRate = 0.9f;
}
