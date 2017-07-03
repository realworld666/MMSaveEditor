// Decompiled with JetBrains decompiler
// Type: PhysicsUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class PhysicsUtility
{
  public static float GetTimeToTravelDistance(float inDistance, float inSpeed)
  {
    if (MathsUtility.Approximately(inDistance, 0.0f, 1f))
      return 0.0f;
    if ((double) inSpeed == 0.0 || (double) Mathf.Sign(inDistance) != (double) Mathf.Sign(inSpeed))
      Debug.LogWarningFormat("Error in input paramters. Please check the code! distanceToTravel: {0}, currentSpeed : {1}", new object[2]
      {
        (object) inDistance,
        (object) inSpeed
      });
    return inDistance / inSpeed;
  }

  public static TimeSpan GetTimeToTravelDistance(float inDistance, float inSpeed, float inAcceleration)
  {
    if (MathsUtility.Approximately(inDistance, 0.0f, 1f))
      return TimeSpan.FromSeconds(0.0);
    if ((double) inAcceleration == 0.0)
      return TimeSpan.FromSeconds((double) PhysicsUtility.GetTimeToTravelDistance(inDistance, inSpeed));
    if ((double) Mathf.Sign(inSpeed) != (double) Mathf.Sign(inAcceleration))
    {
      float num = (float) ((-(double) inSpeed - (double) Mathf.Sqrt(Mathf.Pow(inSpeed, 2f) + 2f * inAcceleration * inDistance)) / (2.0 * (double) inAcceleration));
      if ((double) num > 0.0)
        return TimeSpan.FromSeconds((double) num);
    }
    float num1 = (float) ((-(double) inSpeed + (double) Mathf.Sqrt(Mathf.Pow(inSpeed, 2f) + 2f * inAcceleration * inDistance)) / (2.0 * (double) inAcceleration));
    if ((double) num1 > 0.0)
      return TimeSpan.FromSeconds((double) num1);
    Debug.LogError((object) "Error in input paramters. Please check the code! distanceToTravel: {0}, acceleration : {1}", (UnityEngine.Object) null);
    return TimeSpan.FromSeconds(0.0);
  }

  public static float GetAccelerationNeededToReachTargetSpeed(float inStartingSpeed, float inTargetSpeed, float inDistance)
  {
    if (Mathf.Approximately(inStartingSpeed, inTargetSpeed))
      return 0.0f;
    Debug.Assert((double) inDistance != 0.0, "Cannot calculate acceleration over zero distance!");
    return (float) (((double) Mathf.Pow(inTargetSpeed, 2f) - (double) Mathf.Pow(inStartingSpeed, 2f)) / (2.0 * (double) inDistance));
  }

  public static float GetDistanceWhilstChangingSpeed(float inStartingSpeed, float inEndSpeed, float inAccelleration)
  {
    if ((double) Mathf.Sign(inEndSpeed - inStartingSpeed) != (double) Mathf.Sign(inAccelleration) || (double) inAccelleration == 0.0)
    {
      if (MathsUtility.Approximately(inEndSpeed, inStartingSpeed, 0.1f))
        return 0.0f;
      Debug.LogWarningFormat("Error in SUVAT eqn, inSpeed: {0}, inTargetSpeed: {1}, inAcceleration: {2}", (object) inStartingSpeed, (object) inEndSpeed, (object) inAccelleration);
    }
    return (float) (((double) Mathf.Pow(inEndSpeed, 2f) - (double) Mathf.Pow(inStartingSpeed, 2f)) / (2.0 * (double) inAccelleration));
  }

  public static float GetCappedSpeedReachedOverDistance(float inDistance, float inAcceleration, float inStartSpeed, float maxSpeed = 1000000f, float minSpeed = 0.0f)
  {
    float f = Mathf.Pow(inStartSpeed, 2f) + 2f * inAcceleration * inDistance;
    if ((double) f < 0.0)
      return 0.0f;
    return Mathf.Clamp(Mathf.Sqrt(f), minSpeed, maxSpeed);
  }

  public static float GetPeakSpeedOverDistanceGivenBoundryConditions(float inDistance, float inAcceleration, float inBraking, float inEntrySpeed, float inExitSpeed)
  {
    if ((double) inEntrySpeed < (double) inExitSpeed)
    {
      float whilstChangingSpeed = PhysicsUtility.GetDistanceWhilstChangingSpeed(inEntrySpeed, inExitSpeed, inAcceleration);
      if (MathsUtility.Approximately(whilstChangingSpeed, inDistance, 0.1f))
        return inExitSpeed;
      if ((double) whilstChangingSpeed > (double) inDistance)
        Debug.LogError((object) "Was asked to calculate the maximum speed reached subject to inconsistant boundry conditions", (UnityEngine.Object) null);
    }
    else
    {
      float whilstChangingSpeed = PhysicsUtility.GetDistanceWhilstChangingSpeed(inEntrySpeed, inExitSpeed, inBraking);
      if (MathsUtility.Approximately(whilstChangingSpeed, inDistance, 0.1f))
        return inEntrySpeed;
      if ((double) whilstChangingSpeed > (double) inDistance)
        Debug.LogError((object) "Was asked to calculate the maximum speed reached subject to inconsistant boundry conditions", (UnityEngine.Object) null);
    }
    Debug.Assert((double) inAcceleration > 0.0, "Cannot have zero or negative acceleration when asking to change speed");
    Debug.Assert((double) inBraking < 0.0, "Cannot have zero or positive braking when asking to change speed");
    float num1 = Mathf.Sqrt((float) ((double) inDistance + (double) Mathf.Pow(inEntrySpeed, 2f) / (2.0 * (double) inAcceleration) - (double) Mathf.Pow(inExitSpeed, 2f) / (2.0 * (double) inBraking)) / (float) (1.0 / (2.0 * (double) inAcceleration) - 1.0 / (2.0 * (double) inBraking)));
    float whilstChangingSpeed1 = PhysicsUtility.GetDistanceWhilstChangingSpeed(inEntrySpeed, num1, inAcceleration);
    float whilstChangingSpeed2 = PhysicsUtility.GetDistanceWhilstChangingSpeed(num1, inExitSpeed, inBraking);
    if (!MathsUtility.Approximately(whilstChangingSpeed1 + whilstChangingSpeed2, inDistance, 2f))
    {
      float num2 = whilstChangingSpeed1 + whilstChangingSpeed2;
      Debug.LogWarningFormat("Error in peak speed calculation. Distance to accelerate={0}, Distance to braking={1}, Total{2}. inDistance={3}", (object) whilstChangingSpeed1, (object) whilstChangingSpeed2, (object) num2, (object) inDistance);
    }
    if ((double) num1 < (double) inEntrySpeed || (double) num1 < (double) inExitSpeed)
      Debug.Log((object) "Something fishy is going on!", (UnityEngine.Object) null);
    return num1;
  }
}
