// Decompiled with JetBrains decompiler
// Type: Range
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;
using UnityEngine;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class Range
{
  public float min;
  public float max;

  public Range()
  {
  }

  public Range(float inMin, float inMax)
  {
    this.min = inMin;
    this.max = inMax;
  }

  public float GetValue(float inTime)
  {
    return Mathf.Lerp(this.min, this.max, inTime);
  }
}
