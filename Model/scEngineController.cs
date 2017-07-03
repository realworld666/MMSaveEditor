// Decompiled with JetBrains decompiler
// Type: scEngineController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class scEngineController
{
  private static float m_GearChangeUpFreqStep = 0.05f;
  private static float m_GearChangeDownFreqStep = 0.05f;
  private static float m_GearChangeUpFreqStepState = 0.005f;
  private static float m_GearChangeDownFreqStepState = 3f / 1000f;
  private static float m_GearChangeUpFreqTimeOut = 15f;
  private static float m_GearChangeDownFreqTimeOut = 12f;
  private static float m_RpmDeltaScale = 1f;
  private static float m_DeltaGearChangeScale = 1f;
  private scSoundBlendValue m_RpmDelta = new scSoundBlendValue(0.0f, 20000f);
  private scSoundBlendValue m_LoadDelta = new scSoundBlendValue(0.0f, 10f);
  private scSoundBlendValue m_RpmDeltaGearChange = new scSoundBlendValue(0.0f, 100000f);
  private scSoundBlendValue m_LoadDeltaGearChange = new scSoundBlendValue(0.0f, 50f);
  private float m_RpmGearChangeDecay = 1f;
  private float m_LoadDeltaTarget = -1f;
  private scEngineController.GearTarget m_RpmGearAccelTarget = new scEngineController.GearTarget(7500f, 7500f, 8500f, 150f);
  private scEngineController.GearTarget m_RpmGearBrakeTarget = new scEngineController.GearTarget(9300f, 8600f, 9300f, -50f);
  private float m_RpmGearChangeLow = 5000f;
  private float m_RpmGearChangeHigh = 10000f;
  private float m_GearWobbleDelta = 12.56637f;
  private float m_Rpm;
  private float m_Load;
  private bool m_ChangingGear;
  private scEngineController.State m_State;
  private bool m_Accelerating;
  private static float m_GearChangeUpFreq;
  private static float m_GearChangeDownFreq;
  private float m_SpeedDelta;
  private int m_StateChangeBlockedCounter;
  private float m_GearWobble;
  private float m_GearWobbleRpm;
  private bool m_AcceleratingGearChange;

  public static float GearChangeUpFreq
  {
    get
    {
      return scEngineController.m_GearChangeUpFreq;
    }
  }

  public static float GearChangeDownFreq
  {
    get
    {
      return scEngineController.m_GearChangeDownFreq;
    }
  }

  public float Load
  {
    get
    {
      return this.m_Load;
    }
  }

  public float Rpm
  {
    get
    {
      return this.m_Rpm;
    }
  }

  public static float RpmDeltaScale
  {
    get
    {
      return scEngineController.m_RpmDeltaScale;
    }
    set
    {
      scEngineController.m_RpmDeltaScale = value;
    }
  }

  public static float DeltaGearChangeScale
  {
    get
    {
      return scEngineController.m_DeltaGearChangeScale;
    }
    set
    {
      scEngineController.m_DeltaGearChangeScale = value;
    }
  }

  public static void SetTweaks(float gearChangeUpFreqStep, float gearChangeDownFreqStep, float gearChangeUpFreqTimeOut, float gearChangeDownFreqTimeOut, float gearChangeUpFreqStepState, float gearChangeDownFreqStepState)
  {
    scEngineController.m_GearChangeUpFreqStep = gearChangeUpFreqStep;
    scEngineController.m_GearChangeDownFreqStep = gearChangeDownFreqStep;
    scEngineController.m_GearChangeUpFreqTimeOut = gearChangeUpFreqTimeOut;
    scEngineController.m_GearChangeDownFreqTimeOut = gearChangeDownFreqTimeOut;
    scEngineController.m_GearChangeUpFreqStepState = gearChangeUpFreqStepState;
    scEngineController.m_GearChangeDownFreqStepState = gearChangeDownFreqStepState;
  }

  public void SetState(scEngineController.State state, float speedDelta)
  {
    this.m_SpeedDelta = this.m_State != scEngineController.State.Idle ? Mathf.Clamp01(Mathf.Abs(speedDelta) + 0.5f) : 1f;
    --this.m_StateChangeBlockedCounter;
    if (state == this.m_State || this.m_StateChangeBlockedCounter > 0)
      return;
    this.m_State = state;
    this.m_StateChangeBlockedCounter = 8;
    scEngineController.m_GearChangeUpFreq = Mathf.Clamp01(scEngineController.m_GearChangeUpFreq + scEngineController.m_GearChangeUpFreqStep * 0.5f);
    scEngineController.m_GearChangeDownFreq = Mathf.Clamp01(scEngineController.m_GearChangeDownFreq + scEngineController.m_GearChangeDownFreqStep * 0.5f);
    switch (this.m_State)
    {
      case scEngineController.State.Idle:
        this.m_RpmDelta.Value = -3000f;
        this.m_LoadDelta.Value = -1f;
        this.m_Accelerating = false;
        this.m_RpmGearChangeDecay = 0.95f;
        break;
      case scEngineController.State.AccelerateSteady:
        float b1 = (float) (1500.0 - 1300.0 * (double) scEngineController.m_GearChangeUpFreq);
        this.m_RpmDelta.Value = !this.m_Accelerating ? b1 : Mathf.Min(this.m_RpmDelta.TargetValue, b1);
        this.m_LoadDelta.Value = 4f;
        this.m_Accelerating = true;
        this.m_RpmGearChangeDecay = 0.6f;
        break;
      case scEngineController.State.AccelerateFast:
        this.m_RpmDelta.Value = (float) (4000.0 - 3400.0 * (double) scEngineController.m_GearChangeUpFreq);
        this.m_LoadDelta.Value = 5f;
        this.m_Accelerating = true;
        this.m_RpmGearChangeDecay = 0.7f;
        break;
      case scEngineController.State.BrakeSteady:
        float b2 = (float) (2000.0 * (double) scEngineController.m_GearChangeDownFreq - 2500.0);
        this.m_RpmDelta.Value = this.m_Accelerating ? b2 : Mathf.Max(this.m_RpmDelta.TargetValue, b2);
        this.m_LoadDelta.Value = -3f;
        this.m_Accelerating = false;
        this.m_RpmGearChangeDecay = 0.75f;
        break;
      case scEngineController.State.BrakeFast:
        this.m_RpmDelta.Value = (float) (4300.0 * (double) scEngineController.m_GearChangeDownFreq - 5000.0);
        this.m_LoadDelta.Value = -5f;
        this.m_Accelerating = false;
        this.m_RpmGearChangeDecay = 0.65f;
        break;
    }
    if (this.m_Accelerating)
      scEngineController.m_GearChangeUpFreq = Mathf.Clamp01(scEngineController.m_GearChangeUpFreq + scEngineController.m_GearChangeUpFreqStepState);
    else
      scEngineController.m_GearChangeDownFreq = Mathf.Clamp01(scEngineController.m_GearChangeDownFreq + scEngineController.m_GearChangeDownFreqStepState);
    this.m_LoadDeltaTarget = this.m_LoadDelta.TargetValue;
  }

  private void SetGearChangeThresholds()
  {
    this.m_RpmGearChangeLow = 6700f + Random.Range(0.0f, 100f);
    this.m_RpmGearChangeLow -= scEngineController.m_GearChangeDownFreq * 1500f;
    this.m_RpmGearChangeHigh = 10000f - Random.Range(0.0f, 50f);
  }

  public void Update()
  {
    float dt = 0.01666667f;
    if (this.m_ChangingGear)
    {
      if (this.m_AcceleratingGearChange)
      {
        if ((double) this.m_Rpm <= (double) this.m_RpmGearAccelTarget.Value)
        {
          this.m_GearWobble = (float) (3.14159274101257 * (double) Random.value * 2.0);
          this.m_GearWobbleRpm = (float) (100.0 - (double) scEngineController.m_GearChangeUpFreq * 80.0);
          this.m_GearWobbleDelta = 37.69911f;
          this.m_ChangingGear = false;
          this.m_RpmGearAccelTarget.Step();
          scEngineController.m_GearChangeUpFreq = Mathf.Clamp01(scEngineController.m_GearChangeUpFreq + scEngineController.m_GearChangeUpFreqStep);
        }
      }
      else if ((double) this.m_Rpm >= (double) this.m_RpmGearBrakeTarget.Value)
      {
        this.m_GearWobble = (float) (3.14159274101257 * (double) Random.value * 0.5);
        this.m_GearWobbleRpm = (float) (50.0 - (double) scEngineController.m_GearChangeDownFreq * 25.0);
        this.m_GearWobbleDelta = 18.84956f;
        this.m_ChangingGear = false;
        this.m_RpmGearBrakeTarget.Step();
        scEngineController.m_GearChangeDownFreq = Mathf.Clamp01(scEngineController.m_GearChangeDownFreq + scEngineController.m_GearChangeDownFreqStep);
      }
      if (!this.m_ChangingGear)
      {
        this.SetGearChangeThresholds();
        this.m_RpmDeltaGearChange.Value = 0.0f;
        this.m_LoadDeltaGearChange.Value = 0.0f;
        this.m_RpmDelta.Value = this.m_RpmDelta.TargetValue * this.m_RpmGearChangeDecay;
      }
    }
    else
    {
      this.m_RpmGearBrakeTarget.SlowReset(dt);
      this.m_RpmGearAccelTarget.SlowReset(dt);
      scEngineController.m_GearChangeUpFreq = Mathf.Clamp01(scEngineController.m_GearChangeUpFreq - dt / scEngineController.m_GearChangeUpFreqTimeOut);
      scEngineController.m_GearChangeDownFreq = Mathf.Clamp01(scEngineController.m_GearChangeDownFreq - dt / scEngineController.m_GearChangeDownFreqTimeOut);
      if (this.m_Accelerating)
      {
        this.m_LoadDelta.Value = (double) this.m_Rpm < (double) this.m_RpmGearChangeHigh * 0.949999988079071 ? this.m_LoadDeltaTarget : -1f;
        if ((double) this.m_Rpm >= (double) this.m_RpmGearChangeHigh)
        {
          this.m_RpmDeltaGearChange.Value = -50000f;
          this.m_LoadDeltaGearChange.Value = -20f;
          this.m_ChangingGear = true;
          this.m_AcceleratingGearChange = this.m_Accelerating;
        }
      }
      else if (this.m_State != scEngineController.State.Idle && (double) this.m_Rpm <= (double) this.m_RpmGearChangeLow)
      {
        this.m_RpmDeltaGearChange.Value = 40000f;
        this.m_LoadDeltaGearChange.Value = 10f;
        this.m_ChangingGear = true;
        this.m_AcceleratingGearChange = this.m_Accelerating;
      }
    }
    this.m_RpmDelta.Update(dt);
    this.m_LoadDelta.Update(dt);
    this.m_RpmDeltaGearChange.Update(dt);
    this.m_LoadDeltaGearChange.Update(dt);
    float num1 = (float) (((double) this.m_Rpm - 1000.0) / 9000.0);
    float num2;
    if (this.m_Accelerating)
    {
      float num3 = num1 * num1;
      num2 = (float) (((double) num3 + (double) num3 * (double) num3) / 2.0);
      if ((double) num3 < 0.5)
      {
        float num4 = 1f - num3 * 2f;
        float num5 = num4 * num4;
        num2 += num5 * 2f;
      }
    }
    else
    {
      float num3 = 1f - num1;
      num2 = 1f - num3 * num3 * num3;
    }
    if (this.m_ChangingGear)
    {
      this.m_Rpm += this.m_RpmDeltaGearChange.Value * dt * scEngineController.DeltaGearChangeScale;
      this.m_Load += this.m_LoadDeltaGearChange.Value * dt * scEngineController.DeltaGearChangeScale;
    }
    else
    {
      this.m_Rpm += this.m_RpmDelta.Value * dt * num2 * this.m_SpeedDelta * scEngineController.RpmDeltaScale;
      this.m_Load += this.m_LoadDelta.Value * dt * scEngineController.RpmDeltaScale;
    }
    this.m_Rpm += Mathf.Cos(this.m_GearWobble) * this.m_GearWobbleRpm;
    this.m_GearWobble += dt * this.m_GearWobbleDelta;
    this.m_GearWobbleDelta *= 0.97f;
    this.m_GearWobbleRpm *= 0.93f;
    this.m_Load = Mathf.Clamp01(this.m_Load);
    this.m_Rpm = Mathf.Clamp(this.m_Rpm, 1000f, 10000f);
  }

  public enum State
  {
    Idle,
    AccelerateSteady,
    AccelerateFast,
    BrakeSteady,
    BrakeFast,
  }

  private class GearTarget
  {
    private float m_Current;
    private float m_Min;
    private float m_Max;
    private float m_Step;

    public float Value
    {
      get
      {
        return this.m_Current;
      }
    }

    public GearTarget(float current, float min, float max, float step)
    {
      this.m_Current = current;
      this.m_Min = min;
      this.m_Max = max;
      this.m_Step = step;
    }

    public void Reset()
    {
      this.m_Current = this.m_Min;
    }

    public void Step()
    {
      this.m_Current += this.m_Step;
      this.m_Current = Mathf.Clamp(this.m_Current, this.m_Min, this.m_Max);
    }

    public void SlowReset(float dt)
    {
      this.m_Current -= this.m_Step * (dt / 10f);
      this.m_Current = Mathf.Clamp(this.m_Current, this.m_Min, this.m_Max);
    }
  }
}
