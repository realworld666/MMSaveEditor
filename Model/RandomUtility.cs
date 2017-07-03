// Decompiled with JetBrains decompiler
// Type: RandomUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public static class RandomUtility
{
  private static System.Random globalRandomInstance = new System.Random();

  public static float GetRandomDistribution(float inMinValue, float inMaxValue, int inNumberOfRolls, System.Random inRandom)
  {
    Debug.Assert(inNumberOfRolls > 0, "Jason - Cannot roll 0 times for GetRandomDistribution");
    float num = 0.0f;
    for (int index = 0; index < inNumberOfRolls; ++index)
      num = RandomUtility.GetRandom(inMinValue, inMaxValue, inRandom);
    return num / (float) inNumberOfRolls;
  }

  public static float GetRandomDistribution(float inMinValue, float inMaxValue, int inNumberOfRolls)
  {
    return RandomUtility.GetRandomDistribution(inMinValue, inMaxValue, inNumberOfRolls, RandomUtility.globalRandomInstance);
  }

  public static float GetRandomNormallyDistributed(float inMean, float inStdDev)
  {
    return inMean + RandomUtility.GetRandomNormallyDistributed01() * inStdDev;
  }

  public static float GetRandomNormallyDistributed01()
  {
    return Mathf.Sqrt(-2f * Mathf.Log(RandomUtility.GetRandom01())) * Mathf.Sin(6.283185f * RandomUtility.GetRandom01());
  }

  public static float GetRandom01(System.Random inRandom)
  {
    return (float) inRandom.NextDouble();
  }

  public static float GetRandom01()
  {
    return RandomUtility.GetRandom01(RandomUtility.globalRandomInstance);
  }

  public static int GetRandom(int inMin, int inMax, System.Random inRandom)
  {
    if (inMin > inMax)
    {
      inMax = inMax;
      inMin = inMax;
    }
    return inRandom.Next(inMin, inMax);
  }

  public static int GetRandom(int inMin, int inMax)
  {
    return RandomUtility.GetRandom(inMin, inMax, RandomUtility.globalRandomInstance);
  }

  public static int GetRandomInc(int inMin, int inMax, System.Random inRandom)
  {
    if (inMin > inMax)
    {
      inMax = inMax;
      inMin = inMax;
    }
    return inRandom.Next(inMin, inMax + 1);
  }

  public static int GetRandomInc(int inMin, int inMax)
  {
    return RandomUtility.GetRandomInc(inMin, inMax, RandomUtility.globalRandomInstance);
  }

  public static float GetRandom(float inMin, float inMax, System.Random inRandom)
  {
    if ((double) inMin > (double) inMax)
    {
      inMax = inMax;
      inMin = inMax;
    }
    return inMin + (inMax - inMin) * RandomUtility.GetRandom01(inRandom);
  }

  public static float GetRandom(float inMin, float inMax)
  {
    return RandomUtility.GetRandom(inMin, inMax, RandomUtility.globalRandomInstance);
  }

  public static float GetRandomRoundedToStep(float inMin, float inMax, int nSteps)
  {
    Debug.AssertFormat((nSteps > 1 ? 1 : 0) != 0, "Jason - Number of steps {0} is too few for GetRandomRoundedToStep method.", (object) nSteps);
    if ((double) inMin > (double) inMax)
    {
      float num = inMin;
      inMin = inMax;
      inMax = num;
    }
    float num1 = inMax - inMin;
    float num2 = inMin + num1 * Mathf.Round(RandomUtility.GetRandom01() * (float) nSteps) / (float) nSteps;
    Debug.AssertFormat(((double) num2 > (double) inMax ? 0 : ((double) num2 >= (double) inMin ? 1 : 0)) != 0, "Jason - Error in GetRandomRoundedToStep. Output {0} is outside requested range {1} - {2}", (object) num2, (object) inMin, (object) inMax);
    return num2;
  }

  public static Color GetRandomColor(System.Random inRandom)
  {
    return new Color(RandomUtility.GetRandom01(inRandom), RandomUtility.GetRandom01(inRandom), RandomUtility.GetRandom01(inRandom), 1f);
  }

  public static Color GetRandomColor()
  {
    return RandomUtility.GetRandomColor(RandomUtility.globalRandomInstance);
  }

  public static void Shuffle<T>(ref List<T> inList)
  {
    int count = inList.Count;
    for (int index = 0; index < count; ++index)
    {
      int random = RandomUtility.GetRandom(0, count);
      T obj = inList[random];
      inList[random] = inList[index];
      inList[index] = obj;
    }
  }
}
