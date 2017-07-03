
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SponsorSlot
{
    private SponsorSlot.SlotType mSlotType = SponsorSlot.SlotType.AirIntake;
    public SponsorshipDeal sponsorshipDeal;

    public string slotName
    {
        get
        {
            return SponsorSlot.GetSlotNameString(this.mSlotType);
        }
    }

    public bool isFreeSlot
    {
        get
        {
            return this.sponsorshipDeal == null;
        }
    }

    public int slotNumber
    {
        get
        {
            return (int)(this.mSlotType + 1);
        }
    }

    public SponsorSlot(SponsorSlot.SlotType inSlotType)
    {
        this.mSlotType = inSlotType;
    }

    public static string GetSlotNameString(SponsorSlot.SlotType inSlotType)
    {
        switch (inSlotType)
        {
            case SponsorSlot.SlotType.RearWing:
                return "Rear Wing";
            case SponsorSlot.SlotType.FrontWing:
                return "Front Wing";
            case SponsorSlot.SlotType.Nose:
                return "Nose";
            case SponsorSlot.SlotType.SidePods:
                return "Side Pods";
            case SponsorSlot.SlotType.EndPlate:
                return "End Plate";
            case SponsorSlot.SlotType.AirIntake:
                return "Air Intake";
            default:
                return "Air Intake";
        }
    }

    public enum SlotType
    {
        RearWing,
        FrontWing,
        Nose,
        SidePods,
        EndPlate,
        AirIntake,
        Count,
    }
}
