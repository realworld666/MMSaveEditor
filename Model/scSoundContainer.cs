// Decompiled with JetBrains decompiler
// Type: scSoundContainer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class scSoundContainer : MonoBehaviour
{
  public int m_MaxInstances = 1;
  public scSoundMix.SubmixID m_SubmixID = scSoundMix.SubmixID.Menu;
  public float m_Volume = 0.5f;
  public float m_Pitch = 1f;
  [HideInInspector]
  public float m_InitialVolume = 0.5f;
  private scSoundBlendValue m_DampenedVolume = new scSoundBlendValue(0.0f, 10f, -10f);
  private SoundID mSoundID;
  public List<scSoundContainer.Sound> m_SamplePool;
  public scSoundContainer.CullingBehaviour m_CullingBehaviour;
  public float m_FadeUpTime;
  public float m_FadeDownTime;
  public bool m_Looping;
  public bool m_Mute;
  public bool m_Solo;
  private bool m_IsPlaying;
  private int m_RandomIndex;
  private int m_UpdateCount;
  private Transform m_Parent;
  private scSoundBlendValue m_FadeVolume;
  private bool m_StopRequested;

  public SoundID SoundID
  {
    get
    {
      return this.mSoundID;
    }
  }

  public float Volume
  {
    set
    {
      this.m_DampenedVolume.Value = value;
    }
  }

  public float Pitch { get; set; }

  public bool IsPlaying
  {
    get
    {
      return this.m_IsPlaying;
    }
  }

  public int UpdateCount
  {
    get
    {
      return this.m_UpdateCount;
    }
  }

  public void InitialiseSamples()
  {
    using (List<scSoundContainer.Sound>.Enumerator enumerator = this.m_SamplePool.GetEnumerator())
    {
      while (enumerator.MoveNext())
        enumerator.Current.Initialise(this);
    }
  }

  private void Start()
  {
    try
    {
      this.mSoundID = (SoundID) Enum.Parse(typeof (SoundID), this.gameObject.name);
      this.m_InitialVolume = this.m_Volume;
      scSoundManager.Instance.SoundContainer.AddSoundContainer(this, this.mSoundID);
      this.InitialiseSamples();
      float num1 = float.MaxValue;
      float num2 = float.MinValue;
      if ((double) this.m_FadeUpTime > 0.0)
        num1 = 1f / this.m_FadeUpTime;
      if ((double) this.m_FadeDownTime > 0.0)
        num2 = -1f / this.m_FadeDownTime;
      this.m_FadeVolume = new scSoundBlendValue(0.0f, num1 * this.m_Volume, num2 * this.m_Volume);
      this.gameObject.SetActive(false);
    }
    catch
    {
      this.mSoundID = SoundID.Unset;
      Debug.LogError((object) ("Sound Container " + this.gameObject.name + " does not have a matching entry in the SoundID enum"), (UnityEngine.Object) null);
    }
  }

  private float CalcVolume()
  {
    return this.m_FadeVolume.Value * this.m_DampenedVolume.Value;
  }

  public void PlaySound(float volume, Transform parent, float delay)
  {
    this.Pitch = 1f;
    this.m_StopRequested = false;
    volume = Mathf.Clamp01(volume);
    this.m_Parent = parent;
    if ((bool) ((UnityEngine.Object) this.m_Parent))
      this.transform.position = this.m_Parent.position;
    this.gameObject.SetActive(true);
    this.m_RandomIndex = UnityEngine.Random.Range(0, this.m_SamplePool.Count);
    float volume1 = this.m_Volume;
    if ((double) this.m_FadeUpTime == 0.0)
      this.m_FadeVolume.ResetValue(volume1);
    else
      this.m_FadeVolume.ResetValue(0.0f);
    this.m_FadeVolume.Value = volume1;
    this.m_DampenedVolume.ResetValue(volume);
    this.m_SamplePool[this.m_RandomIndex].Pitch = this.m_Pitch;
    this.m_SamplePool[this.m_RandomIndex].Play(this.CalcVolume(), delay);
    this.m_UpdateCount = 0;
    this.m_IsPlaying = true;
  }

  public void StopSound(bool instantStop = false)
  {
    this.m_StopRequested = true;
    if (instantStop)
    {
      this.m_SamplePool[this.m_RandomIndex].StopRelease();
      this.m_IsPlaying = false;
      this.gameObject.SetActive(false);
    }
    else if ((double) this.m_FadeDownTime == 0.0)
    {
      this.m_SamplePool[this.m_RandomIndex].StopFade();
      this.m_IsPlaying = false;
      this.gameObject.SetActive(false);
    }
    else
      this.m_FadeVolume.Value = 0.0f;
  }

  public void UpdateSound(float dt, bool soloSounds)
  {
    if ((bool) ((UnityEngine.Object) this.m_Parent))
      this.transform.position = this.m_Parent.position;
    if (!this.m_StopRequested && this.m_IsPlaying)
      this.m_FadeVolume.Value = this.m_Volume;
    this.m_FadeVolume.Update(dt);
    this.m_DampenedVolume.Update(dt);
    bool flag = false;
    if ((double) this.m_FadeVolume.Value == 0.0 && this.m_StopRequested)
      flag = true;
    if (!this.m_SamplePool[this.m_RandomIndex].IsPlaying)
    {
      if (this.m_StopRequested)
        flag = true;
      else if (this.m_Looping)
      {
        this.m_SamplePool[this.m_RandomIndex].Play(this.CalcVolume(), 0.0f);
        Debug.LogWarning((object) ("Looping sound stopped unexpectedly and restarted: " + this.gameObject.name + " - headphones unplugged?"), (UnityEngine.Object) null);
        AudioCarWidget.HeadPhonesUnplugged = true;
      }
      else
        flag = true;
    }
    if (flag)
    {
      this.m_SamplePool[this.m_RandomIndex].StopFade();
      this.m_IsPlaying = false;
      this.gameObject.SetActive(false);
    }
    else
    {
      if (this.m_Mute || soloSounds && !this.m_Solo)
        this.m_SamplePool[this.m_RandomIndex].Volume = 0.0f;
      else
        this.m_SamplePool[this.m_RandomIndex].Volume = this.CalcVolume();
      this.m_SamplePool[this.m_RandomIndex].Pitch = this.Pitch;
      ++this.m_UpdateCount;
    }
  }

  public void Pause()
  {
    if (!this.m_IsPlaying)
      return;
    this.m_SamplePool[this.m_RandomIndex].Pause(true, false);
  }

  public void UnPause()
  {
    if (!this.m_IsPlaying)
      return;
    this.m_SamplePool[this.m_RandomIndex].UnPause();
  }

  public void BlockPause()
  {
    if (!this.m_IsPlaying)
      return;
    this.m_SamplePool[this.m_RandomIndex].BlockPause = true;
  }

  public void UnBlockPause()
  {
    if (!this.m_IsPlaying)
      return;
    this.m_SamplePool[this.m_RandomIndex].BlockPause = false;
  }

  public enum CullingBehaviour
  {
    Steal,
    Reject,
  }

  [Serializable]
  public class Sound : scSoundBase
  {
    public AudioClip m_AudioClip;

    public void Initialise(scSoundContainer soundContainer)
    {
      AudioMixerGroup mixGroup = scSoundMix.Instance.GetMixGroup(soundContainer.m_SubmixID);
      this.InitialiseClip(soundContainer.gameObject, this.m_AudioClip, mixGroup, soundContainer.m_Looping, soundContainer.m_SubmixID);
    }
  }
}
