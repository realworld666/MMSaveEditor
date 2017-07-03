// Decompiled with JetBrains decompiler
// Type: scSoundBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Audio;

public class scSoundBase
{
  private scSoundMix.SubmixID m_SubmixID = scSoundMix.SubmixID.Unset;
  private scSoundBlendValue m_FadeVolume = new scSoundBlendValue(1f, 10f, -10f);
  private scSoundBlendValue m_MuteVolume = new scSoundBlendValue(1f, 10f, -10f);
  private scSoundBlendValue m_PauseVolume = new scSoundBlendValue(1f, 2f, -2f);
  private float m_TargetPitch = 1f;
  private float m_TargetVolume = 1f;
  private AudioSource m_AudioSource;
  private bool m_UserPaused;
  private bool m_IsPlaying;
  private bool m_IsPaused;
  private float m_PauseVolumeDelay;

  public scSoundMix.SubmixID SubmixID
  {
    get
    {
      return this.m_SubmixID;
    }
  }

  public bool BlockPause { get; set; }

  public bool IsPlaying
  {
    get
    {
      return this.m_IsPlaying;
    }
  }

  public bool IsPaused
  {
    get
    {
      return this.m_IsPaused;
    }
  }

  public float Pitch
  {
    get
    {
      return this.m_TargetPitch;
    }
    set
    {
      this.m_TargetPitch = value;
      if (this.IsPlaying)
        return;
      this.m_AudioSource.pitch = this.m_TargetPitch;
    }
  }

  public float Volume
  {
    get
    {
      return this.m_TargetVolume;
    }
    set
    {
      this.m_TargetVolume = value;
      if (this.IsPlaying)
        return;
      this.m_AudioSource.volume = this.CalcVolume();
    }
  }

  public void InitialiseClip(GameObject gameObject, AudioClip clip, AudioMixerGroup mixerGroup, bool isLooping, scSoundMix.SubmixID submixID)
  {
    this.m_SubmixID = submixID;
    this.m_AudioSource = gameObject.AddComponent<AudioSource>();
    this.m_AudioSource.bypassEffects = false;
    this.m_AudioSource.bypassListenerEffects = false;
    this.m_AudioSource.bypassReverbZones = false;
    this.m_AudioSource.dopplerLevel = 0.0f;
    this.m_AudioSource.ignoreListenerPause = false;
    this.m_AudioSource.ignoreListenerVolume = false;
    this.m_AudioSource.maxDistance = 100f;
    this.m_AudioSource.minDistance = 100f;
    this.m_AudioSource.mute = false;
    this.m_AudioSource.panStereo = 0.0f;
    this.m_AudioSource.playOnAwake = false;
    this.m_AudioSource.priority = 0;
    this.m_AudioSource.reverbZoneMix = 0.0f;
    this.m_AudioSource.rolloffMode = AudioRolloffMode.Linear;
    this.m_AudioSource.spatialBlend = 0.0f;
    this.m_AudioSource.spread = 0.0f;
    this.m_AudioSource.velocityUpdateMode = AudioVelocityUpdateMode.Fixed;
    this.m_AudioSource.outputAudioMixerGroup = mixerGroup;
    this.m_AudioSource.loop = isLooping;
    this.m_TargetVolume = 0.0f;
    this.m_AudioSource.pitch = this.m_TargetPitch;
    this.m_AudioSource.volume = this.m_TargetVolume;
    this.m_AudioSource.clip = clip;
    scSoundManager.Instance.AddSound(this);
  }

  public void Play(float volume = 1f, float delay = 0)
  {
    this.BlockPause = false;
    this.StopRelease();
    this.m_FadeVolume.ResetValue(1f);
    this.m_MuteVolume.ResetValue(1f);
    this.m_PauseVolume.ResetValue(1f);
    this.m_IsPlaying = true;
    this.m_IsPaused = false;
    this.m_UserPaused = false;
    this.m_TargetVolume = volume;
    this.m_AudioSource.volume = this.CalcVolume();
    if ((double) delay == 0.0)
      this.m_AudioSource.Play();
    else
      this.m_AudioSource.PlayDelayed(delay);
  }

  public void StopFade()
  {
    this.m_FadeVolume.Value = 0.0f;
  }

  public void StopRelease()
  {
    if (!this.m_IsPlaying)
      return;
    if (this.m_IsPaused)
      this.UnPause();
    if (this.m_AudioSource.isPlaying)
      this.m_AudioSource.Stop();
    this.m_IsPlaying = false;
  }

  public void Update(float dt)
  {
    if (!this.m_AudioSource.isPlaying && !this.m_IsPaused)
    {
      this.m_IsPlaying = false;
    }
    else
    {
      this.m_FadeVolume.Update(dt);
      if ((double) this.m_FadeVolume.Value == 0.0 && (double) this.m_FadeVolume.TargetValue == 0.0)
      {
        this.StopRelease();
      }
      else
      {
        this.m_MuteVolume.Update(dt);
        this.m_PauseVolumeDelay -= dt;
        if ((double) this.m_PauseVolumeDelay <= 0.0)
        {
          this.m_PauseVolumeDelay = 0.0f;
          this.m_PauseVolume.Update(dt);
        }
        if ((double) this.m_PauseVolume.Value == 0.0 && (double) this.m_PauseVolume.TargetValue == 0.0)
          this.Pause(true, false);
        this.m_AudioSource.pitch = this.m_TargetPitch;
        this.m_AudioSource.volume = this.CalcVolume();
        if (!this.m_AudioSource.loop || this.m_UserPaused)
          return;
        if ((double) this.m_AudioSource.volume == 0.0 && !this.m_IsPaused)
        {
          this.Pause(false, false);
        }
        else
        {
          if ((double) this.m_AudioSource.volume <= 0.0 || !this.m_IsPaused)
            return;
          this.UnPause();
        }
      }
    }
  }

  public void Pause(bool userPaused, bool fadeThenPause = false)
  {
    if (this.BlockPause)
      return;
    this.m_UserPaused = userPaused;
    if (fadeThenPause)
    {
      this.m_PauseVolume.Value = 0.0f;
      this.m_PauseVolumeDelay = 0.25f;
    }
    else
    {
      if (this.m_IsPaused)
        return;
      this.m_AudioSource.Pause();
      this.m_IsPaused = true;
    }
  }

  public void UnPause()
  {
    if (this.BlockPause)
      return;
    this.m_UserPaused = false;
    if ((double) this.m_PauseVolume.Value == 0.0 && (double) this.m_PauseVolume.TargetValue == 0.0)
      this.m_PauseVolumeDelay = 0.25f;
    this.m_PauseVolume.Value = 1f;
    if (!this.m_IsPaused)
      return;
    this.m_AudioSource.UnPause();
    this.m_IsPaused = false;
  }

  private float CalcVolume()
  {
    return this.m_TargetVolume * this.m_FadeVolume.Value * this.m_MuteVolume.Value * this.m_PauseVolume.Value;
  }

  public void Mute()
  {
    this.m_MuteVolume.Value = 0.0f;
  }

  public void UnMute()
  {
    this.m_MuteVolume.Value = 1f;
  }
}
