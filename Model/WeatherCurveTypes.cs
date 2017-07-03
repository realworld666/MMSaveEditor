// Decompiled with JetBrains decompiler
// Type: WeatherCurveTypes
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class WeatherCurveTypes : MonoBehaviour
{
  public List<AnimationCurve> clearWeatherCurves = new List<AnimationCurve>();
  public List<AnimationCurve> cloudyRainWeatherCurves = new List<AnimationCurve>();
  public List<AnimationCurve> overcastRainWeatherCurves = new List<AnimationCurve>();
  public List<AnimationCurve> moonsoonRainWeatherCurves = new List<AnimationCurve>();
}
