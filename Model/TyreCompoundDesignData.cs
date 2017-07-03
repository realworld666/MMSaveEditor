using System.Xml.Serialization;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class TyreCompoundDesignData
{
    [XmlElement("CliffCondition")]
    public float cliffCondition = 0.1f;
    [XmlElement("CliffConditionTimeCost")]
    public float cliffConditionTimeCost = 30f;
    [XmlElement("WrongTreadForWaterLevelTimeCost")]
    public float wrongTreadForWaterLevelTimeCost = 30f;
    [XmlElement("TemperatureIncreaseTimeInMinutes")]
    public float temperatureIncreaseTime = 10f;
    [XmlElement("TemperatureDecreaseTimeInMinutes")]
    public float temperatureDecreaseTime = 10f;
    [XmlElement("WeatherTemperatureGainStart")]
    public int weatherTemperatureGainStart = 22;
    [XmlElement("WeatherTemperatureLossStart")]
    public int weatherTemperatureLossStart = 21;
    [XmlElement("WeatherMinTempDeltaPerMinute")]
    public float weatherMinTempDelta = 0.02f;
    [XmlElement("WeatherUnitChangePerDegreePerMinute")]
    public float weatherUnitChangePerDegree = 0.01f;
}
