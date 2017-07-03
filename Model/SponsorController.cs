using System.Collections.Generic;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SponsorController
{
    public Map<SponsorSlot.SlotType, List<ContractSponsor>> sponsorOffers = new Map<SponsorSlot.SlotType, List<ContractSponsor>>();
    private SponsorSlot[] mSlots = new SponsorSlot[6];
    private List<Sponsor> mUniqueSponsors = new List<Sponsor>();
    private List<SponsorshipDeal> mSponsorshipDeals = new List<SponsorshipDeal>();
    public const int sponsorSlotCount = 6;
    private SponsorshipDeal mWeekendSponsorship;
    private Team mTeam;
}
