
using FullSerializer;
using System.Xml.Serialization;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class TierPitCrewFunding
{
    [XmlElement("FundingLevelCost")]
    public long[] fundingLevelCosts;
}
