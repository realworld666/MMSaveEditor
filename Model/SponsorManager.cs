
using FullSerializer;
using System;
using System.Collections.Generic;


[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SponsorManager : GenericManager<Sponsor>
{
  private List<ContractSponsor> mContractSponsors = new List<ContractSponsor>();
  private readonly float sponsorMoneyScalar = 1000000f;
    
}
