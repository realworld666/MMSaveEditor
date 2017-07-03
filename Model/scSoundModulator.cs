// Decompiled with JetBrains decompiler
// Type: scSoundModulator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class scSoundModulator
{
  private scSoundLFO mLFO;
  private float mMin;
  private float mMax;
  private float mValue;

  public float Value
  {
    get
    {
      return this.mValue;
    }
  }

  public scSoundModulator(float cycleTime, float min, float max)
  {
    this.mLFO = new scSoundLFO(cycleTime);
    this.mMin = min;
    this.mMax = max;
  }

  public float Update(float dt)
  {
    this.mValue = Mathf.Lerp(this.mMin, this.mMax, this.mLFO.Update(dt));
    return this.mValue;
  }
}
