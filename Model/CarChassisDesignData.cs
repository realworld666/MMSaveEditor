using FullSerializer;
using System.Xml.Serialization;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class CarChassisDesignData
{
    [XmlElement( "TyreWearLapCountIncrease" )]
    public int tyreWearLapCountIncrease = 1;
    [XmlElement( "TyreWearLapCountDecrease" )]
    public int tyreWearLapCountDecrease = 1;
    [XmlElement( "TyreHeatingTimeBonusInMinutes" )]
    public int tyreHeatingTimeBonusInMinutes = 2;
    [XmlElement( "FuelEfficiencyChassisStatNegativeImpact" )]
    public float fuelEfficiencyChassisStatNegativeImpact = 1.05f;
    [XmlElement( "FuelEfficiencyChassisStatPositiveImpact" )]
    public float fuelEfficiencyChassisStatPositiveImpact = 0.95f;
}
