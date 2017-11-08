
using FullSerializer;
using System.Xml.Serialization;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class PitCrewFundings
{
    [XmlElement("TierPitCrewFunding")]
    public TierPitCrewFunding[] tierSpecificFundings;
}
