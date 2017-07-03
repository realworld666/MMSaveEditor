// Decompiled with JetBrains decompiler
// Type: PreSeason
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;
using System;
using UnityEngine;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class PreSeason
{
  private PreSeason.Status mStatus;
  private DateTime mPreSeasonStart;
  private DateTime mPreSeasonEnd;
  private DateTime mPredictedCarCompleteDate;

  public PreSeason.Status status
  {
    get
    {
      return this.mStatus;
    }
  }

  public void StartPreSeason()
  {
    this.mStatus = PreSeason.Status.Active;
    DateTime now = Game.instance.time.now;
    this.mPreSeasonStart = new DateTime(now.Year, 11, 1);
    this.mPreSeasonEnd = new DateTime(now.Year + 1, 2, 5);
    this.mPredictedCarCompleteDate = new DateTime(now.Year + 1, 2, 1);
  }

  public void EndPreSeason()
  {
    this.mStatus = PreSeason.Status.InActive;
  }

  public float GetTimeIntoPreSeason()
  {
    TimeSpan timeSpan = this.mPreSeasonEnd - this.mPreSeasonStart;
    return Mathf.Clamp01((float) (Game.instance.time.now - this.mPreSeasonStart).TotalMinutes / (float) timeSpan.TotalSeconds);
  }

  public float GetPredictedCompletionTime()
  {
    return Mathf.Clamp01((float) (this.mPredictedCarCompleteDate - this.mPreSeasonStart).TotalMinutes / (float) (this.mPreSeasonEnd - this.mPreSeasonStart).TotalSeconds);
  }

  public enum Status
  {
    InActive,
    Active,
  }
}
