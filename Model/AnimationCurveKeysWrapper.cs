// Decompiled with JetBrains decompiler
// Type: AnimationCurveKeysWrapper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class AnimationCurveKeysWrapper
{
  private AnimationCurve _wrappedCurve;
  private Keyframe[] _cachedKeys;

  public AnimationCurve curve
  {
    get
    {
      return this._wrappedCurve;
    }
  }

  public Keyframe[] keys
  {
    get
    {
      return this._cachedKeys;
    }
  }

  public AnimationCurveKeysWrapper(AnimationCurve wrappedCurve)
  {
    this._wrappedCurve = wrappedCurve;
    this._cachedKeys = wrappedCurve.keys;
  }
}
