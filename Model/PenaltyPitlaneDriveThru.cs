// Decompiled with JetBrains decompiler
// Type: PenaltyPitlaneDriveThru
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PenaltyPitlaneDriveThru : Penalty
{
  public override Penalty.PenaltyType penaltyType
  {
    get
    {
      return Penalty.PenaltyType.PitlaneDriveThru;
    }
  }

  public PenaltyPitlaneDriveThru(Penalty.PenaltyCause inCause)
  {
    this.cause = inCause;
  }

  public override void Apply(RacingVehicle inVehicle)
  {
    inVehicle.strategy.PitLaneDriveTrough();
    CommentaryManager.SendComment(inVehicle, Comment.CommentType.PenaltyDriveThrough, new object[2]
    {
      (object) inVehicle.driver,
      (object) this.cause
    });
  }

  public override string GetDescription()
  {
    return Localisation.LocaliseID("PSG_10008860", (GameObject) null);
  }
}
