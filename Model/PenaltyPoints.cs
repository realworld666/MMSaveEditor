public class PenaltyPoints : Penalty
{
  public override Penalty.PenaltyType penaltyType
  {
    get
    {
      return Penalty.PenaltyType.PointsPenalty;
    }
  }


}
