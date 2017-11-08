
using FullSerializer;
using System.Xml.Serialization;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class PitCrewContractPerRaceCosts
{
    [XmlElement("PerRaceCost")]
    public long[] starsValuePerRaceCosts;
}
