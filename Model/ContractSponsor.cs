
using FullSerializer;
using System;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ContractSponsor : Contract
{
    public Circuit latestRaceAttended;
    public DateTime lattestRaceAttendedDate = new DateTime();
    public SponsorSlot.SlotType slotSponsoringType = SponsorSlot.SlotType.AirIntake;
    public int upfrontValue;
    public int bonusValuePerRace;
    public int perRacePayment;
    public int contractRacesLeft;
    public int contractTotalRaces;
    public int historyTotalBonus;
    public DateTime offerDate = new DateTime();
    public DateTime offerExpireDate = new DateTime();
    public int offerRacesLeft;
    public int offerRacesTotal;
    public CalendarEvent_v1 calendarEvent;
    private Team mSponsoredTeam;
    private Sponsor mSponsor;


}
