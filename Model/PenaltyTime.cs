
public class PenaltyTime : Penalty
{
    public float seconds;

    public override Penalty.PenaltyType penaltyType
    {
        get
        {
            return Penalty.PenaltyType.TimePenalty;
        }
    }


    public enum PenaltySize
    {
        Small,
        Big,
    }
}
