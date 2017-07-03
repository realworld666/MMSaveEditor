// Decompiled with JetBrains decompiler
// Type: MathsUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class MathsUtility
{
  private const double squareMagnitudeThresholdForSafeNormalize = 9.99999949475751E-11;

  public static bool LinePlaneIntersection(out Vector3 inIntersection, Vector3 inLinePosition, Vector3 inLineDirection, Vector3 inPlaneNormal, Vector3 inPlanePoint, bool inNormalize = true)
  {
    Vector3 zero = Vector3.zero;
    inIntersection = Vector3.zero;
    float num1 = Vector3.Dot(inPlanePoint - inLinePosition, inPlaneNormal);
    float num2 = Vector3.Dot(inLineDirection, inPlaneNormal);
    if ((double) num2 == 0.0)
      return false;
    float num3 = num1 / num2;
    if (inNormalize && !Mathf.Approximately(inLineDirection.sqrMagnitude, 1f))
      inLineDirection.Normalize();
    Vector3 vector3 = inLineDirection * num3;
    inIntersection = inLinePosition + vector3;
    return true;
  }

  public static int Wrap(int inValue, int inMin, int inMax)
  {
    if (inValue < inMin)
      inValue = inMax;
    else if (inValue > inMax)
      inValue = inMin;
    return inValue;
  }

  public static long Lerp(long inValueA, long inValueB, float inTime)
  {
    inTime = Mathf.Clamp01(inTime);
    return (long) ((1.0 - (double) inTime) * (double) inValueA + (double) inTime * (double) inValueB);
  }

  public static float DistanceToRange(float value, float rangeMin, float rangeMax)
  {
    if ((double) value < (double) rangeMin)
      return rangeMin - value;
    if ((double) value > (double) rangeMax)
      return value - rangeMax;
    return 0.0f;
  }

  public static void CoalesceRanges(List<KeyValuePair<float, float>> ranges)
  {
    ranges.Sort((Comparison<KeyValuePair<float, float>>) ((x, y) => x.Key.CompareTo(y.Key)));
    for (int index = 0; index < ranges.Count - 1; ++index)
    {
      if ((double) ranges[index].Value > (double) ranges[index + 1].Key)
      {
        ranges[index] = new KeyValuePair<float, float>(ranges[index].Key, Mathf.Max(ranges[index].Value, ranges[index + 1].Value));
        ranges.RemoveAt(index + 1);
        --index;
      }
    }
  }

  public static float RoundToScale(float inValue, float inScale)
  {
    return Mathf.Round(inValue * inScale) / inScale;
  }

  public static float RoundToDecimal(float inValue)
  {
    return Mathf.Round(inValue * 10f) / 10f;
  }

  public static float RoundToDecimal(float inValue, float inDecimal)
  {
    float num = Mathf.Pow(10f, inDecimal);
    return Mathf.Round(inValue * num) / num;
  }

  public static int RoundToNearestThousand(int inValue)
  {
    return (inValue + 500) / 1000 * 1000;
  }

  public static int RoundToNearestHundredThousands(int inValue)
  {
    if (inValue % 100000 >= 50000)
      return inValue + 100000 - inValue % 100000;
    return inValue - inValue % 100000;
  }

  public static float NextMultipleOf(float inValue, float inMultiple)
  {
    inMultiple = !Mathf.Approximately(inMultiple, 0.0f) ? inMultiple : 1f;
    if ((double) inValue % (double) inMultiple == 0.0)
      return inValue + inMultiple;
    return Mathf.Ceil(inValue / inMultiple) * inMultiple;
  }

  public static int NextMultipleOf(int inValue, int inMultiple)
  {
    inMultiple = inMultiple != 0 ? inMultiple : 1;
    if (inValue % inMultiple == 0)
      return inValue + inMultiple;
    return Mathf.CeilToInt((float) inValue / (float) inMultiple) * inMultiple;
  }

  public static float NearestMultipleOf(float inValue, float inMultiple)
  {
    inMultiple = !Mathf.Approximately(inMultiple, 0.0f) ? inMultiple : 1f;
    return Mathf.Round(inValue / inMultiple) * inMultiple;
  }

  public static int NearestMultipleOf(int inValue, int inMultiple)
  {
    inMultiple = inMultiple != 0 ? inMultiple : 1;
    return Mathf.RoundToInt((float) inValue / (float) inMultiple) * inMultiple;
  }

  public static float PreviousMultipleOf(float inValue, float inMultiple)
  {
    inMultiple = !Mathf.Approximately(inMultiple, 0.0f) ? inMultiple : 1f;
    if ((double) inValue % (double) inMultiple == 0.0)
      return inValue - inMultiple;
    return Mathf.Floor(inValue / inMultiple) * inMultiple;
  }

  public static int PreviousMultipleOf(int inValue, int inMultiple)
  {
    inMultiple = inMultiple != 0 ? inMultiple : 1;
    if (inValue % inMultiple == 0)
      return inValue - inMultiple;
    return Mathf.FloorToInt((float) inValue / (float) inMultiple) * inMultiple;
  }

  public static bool CanBeSafelyNormalized(Vector3 vec)
  {
    return (double) vec.sqrMagnitude >= 9.99999949475751E-11;
  }

  public static bool ApproximatelyZero(float a)
  {
    return (double) a < 1E-05;
  }

  public static bool Approximately(float a, float b, float tolerance)
  {
    return (double) Mathf.Abs(a - b) <= (double) tolerance;
  }

  public static float Mean(List<int> inVals)
  {
    float num = 0.0f;
    for (int index = 0; index < inVals.Count; ++index)
      num += (float) inVals[index];
    if (inVals.Count > 0)
      return num / (float) inVals.Count;
    return 0.0f;
  }

  public static float Mean(List<float> inVals)
  {
    float num = 0.0f;
    for (int index = 0; index < inVals.Count; ++index)
      num += inVals[index];
    if (inVals.Count > 0)
      return num / (float) inVals.Count;
    return 0.0f;
  }

  public static float Variance(List<int> inVals)
  {
    List<float> inVals1 = new List<float>();
    float num = MathsUtility.Mean(inVals);
    for (int index = 0; index < inVals.Count; ++index)
      inVals1.Add(Mathf.Pow((float) inVals[index] - num, 2f));
    return MathsUtility.Mean(inVals1);
  }

  public static float StdDev(List<int> inVals)
  {
    return Mathf.Sqrt(MathsUtility.Variance(inVals));
  }
}
