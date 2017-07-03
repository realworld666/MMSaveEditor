// Decompiled with JetBrains decompiler
// Type: TyreSetPerformanceRange
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class TyreSetPerformanceRange
{
  private int mMaxLaps;
  private float mTimeCost;
  private float mMaxDistance;

  public float maxDistance
  {
    get
    {
      return this.mMaxDistance;
    }
  }

  public int maxLaps
  {
    get
    {
      return this.mMaxLaps;
    }
  }

  public float timeCost
  {
    get
    {
      return this.mTimeCost;
    }
  }

  public void SetMaxLaps(int inMaxLaps)
  {
    float meters = GameUtility.MilesToMeters(Game.instance.sessionManager.eventDetails.circuit.trackLengthMiles);
    this.mMaxLaps = Mathf.Max(inMaxLaps, 0);
    this.mMaxDistance = (float) inMaxLaps * meters;
  }

  public void SetTimeCost(float inTimeCost)
  {
    this.mTimeCost = inTimeCost;
  }
}
