// Decompiled with JetBrains decompiler
// Type: scSoundBlendValue
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class scSoundBlendValue
{
  private float m_Value;
  private float m_TargetValue;
  private float m_MaxDeltaPerSecond;
  private float m_MinDeltaPerSecond;

  public float TargetValue
  {
    get
    {
      return this.m_TargetValue;
    }
  }

  public float Value
  {
    get
    {
      return this.m_Value;
    }
    set
    {
      this.m_TargetValue = value;
    }
  }

  public scSoundBlendValue(float value, float maxDeltaPerSecond)
  {
    this.Initialize(value, maxDeltaPerSecond, -maxDeltaPerSecond);
  }

  public scSoundBlendValue(float value, float maxDeltaPerSecond, float minDeltaPerSecond)
  {
    this.Initialize(value, maxDeltaPerSecond, minDeltaPerSecond);
  }

  public void ResetValue(float value)
  {
    this.m_Value = value;
    this.m_TargetValue = value;
  }

  public void Set()
  {
    this.m_Value = this.m_TargetValue;
  }

  private void Initialize(float value, float maxDeltaPerSecond, float minDeltaPerSecond)
  {
    this.m_Value = value;
    this.m_TargetValue = value;
    this.m_MaxDeltaPerSecond = maxDeltaPerSecond;
    this.m_MinDeltaPerSecond = minDeltaPerSecond;
  }

  public void Update(float dt)
  {
    this.m_Value += Mathf.Clamp(this.m_TargetValue - this.m_Value, this.m_MinDeltaPerSecond * dt, this.m_MaxDeltaPerSecond * dt);
  }
}
