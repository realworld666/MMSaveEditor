// Decompiled with JetBrains decompiler
// Type: scSoundCarEngine
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class scSoundCarEngine : MonoBehaviour
{
  private float mVolume = 1f;
  public const float MinRpm = 0.0f;
  public const float MaxRpm = 10000f;
  public float m_MasterVolume;
  public AudioMixerGroup m_AudioMixerGroup;
  public List<scSoundCarEngine.EngineSound> m_OnLoad;
  public List<scSoundCarEngine.EngineSound> m_OffLoad;
  public scSoundCarEngine.EngineSound m_Idle;
  private List<scSoundCarEngine.EngineSound> m_AllSounds;
  private bool m_IsPlaying;
  private bool m_IsInitialized;
  private scSoundCarEngine.State m_State;

  public float Volume
  {
    get
    {
      return this.mVolume;
    }
    set
    {
      this.mVolume = value;
    }
  }

  public scSoundCarEngine.State StateRef
  {
    get
    {
      return this.m_State;
    }
    set
    {
      this.m_State = value;
    }
  }

  private float FindRmpEndBelow(scSoundCarEngine.EngineSound engineSound)
  {
    float num = float.MinValue;
    scSoundCarEngine.EngineSound engineSound1 = (scSoundCarEngine.EngineSound) null;
    int count = this.m_AllSounds.Count;
    for (int index = 0; index < count; ++index)
    {
      scSoundCarEngine.EngineSound allSound = this.m_AllSounds[index];
      if (engineSound != allSound && allSound.MatchingLoadVolumes(engineSound) && ((double) allSound.m_RpmReference < (double) engineSound.m_RpmReference && (double) allSound.m_RpmReference > (double) num))
      {
        num = allSound.m_RpmReference;
        engineSound1 = allSound;
      }
    }
    if (engineSound1 != null)
      return engineSound1.m_RpmStart + engineSound1.m_RpmLength;
    return 0.0f;
  }

  private float FindRmpStartAbove(scSoundCarEngine.EngineSound engineSound)
  {
    float num = float.MaxValue;
    scSoundCarEngine.EngineSound engineSound1 = (scSoundCarEngine.EngineSound) null;
    int count = this.m_AllSounds.Count;
    for (int index = 0; index < count; ++index)
    {
      scSoundCarEngine.EngineSound allSound = this.m_AllSounds[index];
      if (engineSound != allSound && allSound.MatchingLoadVolumes(engineSound) && ((double) allSound.m_RpmReference > (double) engineSound.m_RpmReference && (double) allSound.m_RpmReference < (double) num))
      {
        num = allSound.m_RpmReference;
        engineSound1 = allSound;
      }
    }
    if (engineSound1 != null)
      return engineSound1.m_RpmStart;
    return 10000f;
  }

  public void InitialiseEngine()
  {
    this.m_AllSounds = new List<scSoundCarEngine.EngineSound>();
    this.m_AllSounds.Add(this.m_Idle);
    this.m_AllSounds.AddRange((IEnumerable<scSoundCarEngine.EngineSound>) this.m_OnLoad);
    this.m_AllSounds.AddRange((IEnumerable<scSoundCarEngine.EngineSound>) this.m_OffLoad);
    this.m_State = new scSoundCarEngine.State();
    int count = this.m_AllSounds.Count;
    for (int index = 0; index < count; ++index)
    {
      scSoundCarEngine.EngineSound allSound = this.m_AllSounds[index];
      float onLoadVolume = 0.0f;
      float offLoadVolume = 0.0f;
      if (this.m_OnLoad.Contains(allSound) || allSound == this.m_Idle)
        onLoadVolume = 1f;
      if (this.m_OffLoad.Contains(allSound) || allSound == this.m_Idle)
        offLoadVolume = 1f;
      allSound.SetLoadVolumes(onLoadVolume, offLoadVolume);
    }
    for (int index = 0; index < count; ++index)
      this.m_AllSounds[index].Initialise(this.gameObject, this, this.m_AudioMixerGroup);
    this.m_IsInitialized = true;
  }

  public void SetState()
  {
    this.m_State.Set();
    this.UpdateState(0.01666667f);
  }

  public void UpdateState(float dt)
  {
    if (!this.m_IsInitialized)
      this.InitialiseEngine();
    this.m_State.Update(dt);
    int count = this.m_AllSounds.Count;
    for (int index = 0; index < count; ++index)
      this.m_AllSounds[index].Update(this.m_State);
    if (this.m_IsPlaying)
      return;
    this.Play();
  }

  public void Play()
  {
    if (this.m_IsPlaying)
      return;
    int count = this.m_AllSounds.Count;
    for (int index = 0; index < count; ++index)
      this.m_AllSounds[index].Play(0.0f, 0.0f);
    this.m_IsPlaying = true;
  }

  public void Stop()
  {
    if (!this.m_IsPlaying)
      return;
    int count = this.m_AllSounds.Count;
    for (int index = 0; index < count; ++index)
      this.m_AllSounds[index].StopFade();
    this.m_IsPlaying = false;
  }

  [Serializable]
  public class EngineSound : scSoundBase
  {
    public AudioClip m_AudioClip;
    public float m_RpmReference;
    public float m_RpmStart;
    public float m_RpmLength;
    public float m_RpmLowFadeStart;
    public float m_RpmLowFadeEnd;
    public float m_RpmLowFadeSpan;
    public float m_RpmHighFadeStart;
    public float m_RpmHighFadeEnd;
    public float m_RpmHighFadeSpan;
    private float m_OnLoadVolume;
    private float m_OffLoadVolume;
    private scSoundCarEngine m_AudioCarEngine;

    public void SetLoadVolumes(float onLoadVolume, float offLoadVolume)
    {
      this.m_OnLoadVolume = onLoadVolume;
      this.m_OffLoadVolume = offLoadVolume;
    }

    public bool MatchingLoadVolumes(scSoundCarEngine.EngineSound otherEngineSound)
    {
      return (double) otherEngineSound.m_OnLoadVolume == (double) this.m_OnLoadVolume || (double) otherEngineSound.m_OffLoadVolume == (double) this.m_OffLoadVolume;
    }

    public void Initialise(GameObject gameObject, scSoundCarEngine audioCarEngine, AudioMixerGroup audioMixerGroup)
    {
      this.m_AudioCarEngine = audioCarEngine;
      this.InitialiseClip(gameObject, this.m_AudioClip, audioMixerGroup, true, scSoundMix.SubmixID.PlayerCar);
      this.m_RpmLowFadeEnd = audioCarEngine.FindRmpEndBelow(this);
      if ((double) this.m_RpmLowFadeEnd == 0.0)
      {
        this.m_RpmLowFadeStart = 0.0f;
        this.m_RpmLowFadeSpan = 0.0f;
      }
      else
      {
        this.m_RpmLowFadeStart = this.m_RpmStart;
        this.m_RpmLowFadeSpan = this.m_RpmLowFadeEnd - this.m_RpmLowFadeStart;
      }
      this.m_RpmHighFadeStart = audioCarEngine.FindRmpStartAbove(this);
      if ((double) this.m_RpmHighFadeStart == 10000.0)
      {
        this.m_RpmHighFadeEnd = 10000f;
        this.m_RpmHighFadeSpan = 0.0f;
      }
      else
      {
        this.m_RpmHighFadeEnd = this.m_RpmStart + this.m_RpmLength;
        this.m_RpmHighFadeSpan = this.m_RpmHighFadeEnd - this.m_RpmHighFadeStart;
      }
    }

    private float FadeExponential3dB(float linearVolume)
    {
      linearVolume = 1f - linearVolume;
      linearVolume = (float) ((double) linearVolume * (double) linearVolume * 0.800000011920929 + (double) linearVolume * 0.200000002980232);
      linearVolume = 1f - linearVolume;
      return linearVolume;
    }

    public void Update(scSoundCarEngine.State state)
    {
      this.Pitch = state.m_RPM.Value / this.m_RpmReference;
      if ((double) state.m_RPM.Value > (double) this.m_RpmHighFadeEnd || (double) state.m_RPM.Value < (double) this.m_RpmLowFadeStart)
        this.Volume = 0.0f;
      else if ((double) state.m_RPM.Value > (double) this.m_RpmHighFadeStart)
        this.Volume = this.FadeExponential3dB((float) (1.0 - ((double) state.m_RPM.Value - (double) this.m_RpmHighFadeStart) / (double) this.m_RpmHighFadeSpan));
      else if ((double) state.m_RPM.Value < (double) this.m_RpmLowFadeEnd)
        this.Volume = this.FadeExponential3dB((float) (1.0 - ((double) this.m_RpmLowFadeEnd - (double) state.m_RPM.Value) / (double) this.m_RpmLowFadeSpan));
      else
        this.Volume = 1f;
      this.Volume = this.Volume * 0.75f * Mathf.Clamp01(0.0f + this.FadeExponential3dB(this.m_OnLoadVolume * state.m_Load.Value) + this.FadeExponential3dB(this.m_OffLoadVolume * (1f - state.m_Load.Value))) * state.m_Volume.Value * this.m_AudioCarEngine.m_MasterVolume * this.m_AudioCarEngine.Volume;
    }
  }

  public class State
  {
    public scSoundBlendValue m_RPM = new scSoundBlendValue(1000f, 20000f);
    public scSoundBlendValue m_Load = new scSoundBlendValue(0.0f, 100f);
    public scSoundBlendValue m_Volume = new scSoundBlendValue(1f, 2f);

    public void Update(float dt)
    {
      this.m_RPM.Update(dt);
      this.m_Load.Update(dt);
      this.m_Volume.Update(dt);
    }

    public void Set()
    {
      this.m_RPM.Set();
      this.m_Load.Set();
      this.m_Volume.Set();
    }
  }
}
