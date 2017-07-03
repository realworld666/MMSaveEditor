// Decompiled with JetBrains decompiler
// Type: PenaltyPartRulesBroken
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class PenaltyPartRulesBroken : Penalty
{
  public float revealAnimTime;
  private CarPart mPart;
  private long mPenaltyCashAmount;
  private int mPlacesLost;

  public CarPart part
  {
    get
    {
      return this.mPart;
    }
  }

  public long penaltyCashAmount
  {
    get
    {
      return this.mPenaltyCashAmount;
    }
  }

  public int placesLost
  {
    get
    {
      return this.mPlacesLost;
    }
    set
    {
      this.mPlacesLost = value;
    }
  }

  public override Penalty.PenaltyType penaltyType
  {
    get
    {
      return Penalty.PenaltyType.PartPenalty;
    }
  }

  public PenaltyPartRulesBroken(CarPart inPart, long inAmount, int inPlacesLost)
  {
    this.mPart = inPart;
    this.mPenaltyCashAmount = inAmount;
    this.mPlacesLost = inPlacesLost;
  }
}
