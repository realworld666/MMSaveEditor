
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SponsorshipDeal
{
    public SessionObjective qualifyingObjective = new SessionObjective();
    public SessionObjective raceObjective = new SessionObjective();
    public ContractSponsor contract;

}
