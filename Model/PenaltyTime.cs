// Decompiled with JetBrains decompiler
// Type: PenaltyTime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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

  public PenaltyTime(PenaltyTime.PenaltySize inSize, Penalty.PenaltyCause inCause)
  {
    this.cause = inCause;
    switch (inSize)
    {
      case PenaltyTime.PenaltySize.Small:
        this.seconds = 10f;
        break;
      case PenaltyTime.PenaltySize.Big:
        this.seconds = 20f;
        break;
    }
  }

  public override void Apply(RacingVehicle inVehicle)
  {
    inVehicle.timer.sessionTimePenalty += this.seconds;
    CommentaryManager.SendComment(inVehicle, Comment.CommentType.PenaltyTime, (object) inVehicle.driver, (object) this.cause, (object) this.seconds);
  }

  public override string GetDescription()
  {
    return Localisation.LocaliseID("PSG_10010042", (GameObject) null);
  }

  public enum PenaltySize
  {
    Small,
    Big,
  }
}
