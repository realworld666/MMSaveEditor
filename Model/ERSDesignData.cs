using FullSerializer;
using System.Xml.Serialization;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class ERSDesignData
{
    [XmlElement( "SmallBatterySize" )]
    public float smallBatterySize;
    [XmlElement( "LargeBatterySize" )]
    public float largeBatterySize;
    [XmlElement( "HarvestModeRate" )]
    public float harvestModeRate;
    [XmlElement( "PowerModeRate" )]
    public float powerModeRate;
    [XmlElement( "PowerModeTimeCostGain" )]
    public float powerModeTimeCostGain;
    [XmlElement( "HybridModeFuelSave" )]
    public float hybridModeFuelSave;
    [XmlElement( "HybridModeRate" )]
    public float hybridModeRate;
    [XmlElement( "PowerModeChangeCooldown" )]
    public float powerModeChangeCooldown;
    [XmlElement( "HybridModeChangeCooldown" )]
    public float hybridModeChangeCooldown;
    [XmlElement( "HarvestModeChangeCooldown" )]
    public float harvestModeChangeCooldown;
}
