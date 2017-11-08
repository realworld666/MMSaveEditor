
using FullSerializer;
using System.Xml.Serialization;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class FixingPartsTaskTimes
{
    [XmlElement("BrakesMinTime")]
    public float brakesMinTime;
    [XmlElement("BrakesMedTime")]
    public float brakesMedTime;
    [XmlElement("BrakesMaxTime")]
    public float brakesMaxTime;
    [XmlElement("EngineMinTime")]
    public float engineMinTime;
    [XmlElement("EngineMedTime")]
    public float engineMedTime;
    [XmlElement("EngineMaxTime")]
    public float engineMaxTime;
    [XmlElement("FrontWingMinTime")]
    public float frontWingMinTime;
    [XmlElement("FrontWingMedTime")]
    public float frontWingMedTime;
    [XmlElement("FrontWingMaxTime")]
    public float frontWingMaxTime;
    [XmlElement("GearboxMinTime")]
    public float gearboxMinTime;
    [XmlElement("GearboxMedTime")]
    public float gearboxMedTime;
    [XmlElement("GearboxMaxTime")]
    public float gearboxMaxTime;
    [XmlElement("RearWingMinTime")]
    public float rearWingMinTime;
    [XmlElement("RearWingMedTime")]
    public float rearWingMedTime;
    [XmlElement("RearWingMaxTime")]
    public float rearWingMaxTime;
    [XmlElement("SuspensionMinTime")]
    public float suspensionMinTime;
    [XmlElement("SuspensionMedTime")]
    public float suspensionMedTime;
    [XmlElement("SuspensionMaxTime")]
    public float suspensionMaxTime;
}
