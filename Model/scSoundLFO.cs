// Decompiled with JetBrains decompiler
// Type: scSoundLFO
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class scSoundLFO
{
  private const float mTwoPi = 6.283185f;
  private float mModulator;
  private float mTimeStep;
  private float mValue;

  public float Value
  {
    get
    {
      return this.mValue;
    }
  }

  public scSoundLFO(float cycleTime)
  {
    this.mModulator = Random.Range(0.0f, 6.283185f);
    this.mTimeStep = 6.283185f / cycleTime;
  }

  public float Update(float dt)
  {
    this.mModulator += this.mTimeStep * dt;
    if ((double) this.mModulator > 6.28318548202515)
      this.mModulator -= 6.283185f;
    this.mValue = (float) (((double) Mathf.Cos(this.mModulator) + 1.0) * 0.5);
    return this.mValue;
  }
}
