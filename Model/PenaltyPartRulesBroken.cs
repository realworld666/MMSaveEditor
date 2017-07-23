public class PenaltyPartRulesBroken : Penalty
{
    public float revealAnimTime;
    private CarPart mPart;
    private long mPenaltyCashAmount;
    private int mPlacesLost;

    public override Penalty.PenaltyType penaltyType
    {
        get
        {
            return Penalty.PenaltyType.PartPenalty;
        }
    }

}
