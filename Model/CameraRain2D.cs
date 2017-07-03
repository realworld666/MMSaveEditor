// Decompiled with JetBrains decompiler
// Type: CameraRain2D
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class CameraRain2D : MonoBehaviour
{
  public int[] m_ParticleRate = new int[5];
  private ParticleSystem m_ParticleSystem;
  public Weather.RainType m_OverrideRain;
  private Weather.RainType m_CurrentRain;
  private static scSoundContainer mSoundAmbienceRainLow;
  private static scSoundContainer mSoundAmbienceRainHigh;
  private static scOneShotSounds mSoundDistantThunder;
  private static scOneShotSounds mSoundVeryDistantThunder;
  private static Weather.RainType mLastRainType;

  private void Start()
  {
    CameraRain2D.mSoundDistantThunder = new scOneShotSounds(SoundID.Sfx_ThunderDistant, 15f, 30f);
    CameraRain2D.mSoundVeryDistantThunder = new scOneShotSounds(SoundID.Sfx_ThunderVeryDistant, 15f, 30f);
    this.m_ParticleSystem = this.gameObject.GetComponent<ParticleSystem>();
    if (!((Object) this.m_ParticleSystem != (Object) null))
      return;
    this.m_ParticleSystem.Stop();
  }

  private void OnEnable()
  {
  }

  private void OnDisable()
  {
    this.m_OverrideRain = Weather.RainType.None;
    this.m_CurrentRain = Weather.RainType.None;
    CameraRain2D.StopSound();
    if (!((Object) this.m_ParticleSystem != (Object) null))
      return;
    this.m_ParticleSystem.Stop();
  }

  private void Update()
  {
    if (!Game.IsActive())
      return;
    SessionWeatherDetails currentSessionWeather = Game.instance.sessionManager.currentSessionWeather;
    if (currentSessionWeather != null && currentSessionWeather.GetCurrentWeather() != null && currentSessionWeather.GetCurrentWeather().rainType != Weather.RainType.None || this.m_OverrideRain != Weather.RainType.None)
    {
      Weather.RainType rainType = currentSessionWeather.GetCurrentWeather().rainType;
      if (this.m_OverrideRain != Weather.RainType.None)
        rainType = this.m_OverrideRain;
      if (rainType != this.m_CurrentRain)
      {
        this.m_CurrentRain = rainType;
        this.m_ParticleSystem.emission.rate = new ParticleSystem.MinMaxCurve((float) this.m_ParticleRate[(int) this.m_CurrentRain]);
        this.m_ParticleSystem.Play();
      }
      if (!scSessionAmbience.PlayingSound)
      {
        CameraRain2D.StopSound();
      }
      else
      {
        if (scSessionAmbience.IsPaused)
          return;
        this.UpdateSound(this.m_CurrentRain);
      }
    }
    else
    {
      if (this.m_CurrentRain == Weather.RainType.None)
        return;
      this.m_CurrentRain = Weather.RainType.None;
      this.m_ParticleSystem.Stop();
      CameraRain2D.StopSound();
    }
  }

  private void UpdateSound(Weather.RainType rainType)
  {
    if (CameraRain2D.mLastRainType != rainType)
      CameraRain2D.StopSound();
    CameraRain2D.mLastRainType = rainType;
    switch (rainType)
    {
      case Weather.RainType.None:
        CameraRain2D.StopSound();
        break;
      case Weather.RainType.Light:
        scSoundManager.CheckPlaySound(SoundID.Weather_RainLoop_01, ref CameraRain2D.mSoundAmbienceRainLow, 0.0f);
        scSoundManager.CheckPlaySound(SoundID.Weather_RainLoop_02, ref CameraRain2D.mSoundAmbienceRainHigh, 0.0f);
        break;
      case Weather.RainType.Medium:
        scSoundManager.CheckPlaySound(SoundID.Weather_RainLoop_03, ref CameraRain2D.mSoundAmbienceRainLow, 0.0f);
        scSoundManager.CheckPlaySound(SoundID.Weather_RainLoop_04, ref CameraRain2D.mSoundAmbienceRainHigh, 0.0f);
        break;
      case Weather.RainType.Heavy:
        scSoundManager.CheckPlaySound(SoundID.Weather_RainLoop_05, ref CameraRain2D.mSoundAmbienceRainLow, 0.0f);
        scSoundManager.CheckPlaySound(SoundID.Weather_RainLoop_06, ref CameraRain2D.mSoundAmbienceRainHigh, 0.0f);
        CameraRain2D.mSoundVeryDistantThunder.Update(GameTimer.deltaTime);
        break;
      case Weather.RainType.Monsooon:
        scSoundManager.CheckPlaySound(SoundID.Weather_RainLoop_07, ref CameraRain2D.mSoundAmbienceRainLow, 0.0f);
        scSoundManager.CheckPlaySound(SoundID.Weather_RainLoop_08, ref CameraRain2D.mSoundAmbienceRainHigh, 0.0f);
        CameraRain2D.mSoundVeryDistantThunder.Update(GameTimer.deltaTime);
        CameraRain2D.mSoundDistantThunder.Update(GameTimer.deltaTime);
        break;
    }
    float num = 1f;
    if ((Object) CameraRain2D.mSoundAmbienceRainLow != (Object) null)
      CameraRain2D.mSoundAmbienceRainLow.Volume = num;
    if (!((Object) CameraRain2D.mSoundAmbienceRainHigh != (Object) null))
      return;
    CameraRain2D.mSoundAmbienceRainHigh.Volume = (float) (1.0 - (double) num * 0.699999988079071);
  }

  public static void StopSound()
  {
    scSoundManager.CheckStopSound(ref CameraRain2D.mSoundAmbienceRainLow);
    scSoundManager.CheckStopSound(ref CameraRain2D.mSoundAmbienceRainHigh);
    if (CameraRain2D.mSoundDistantThunder != null)
      CameraRain2D.mSoundDistantThunder.Stop();
    if (CameraRain2D.mSoundVeryDistantThunder != null)
      CameraRain2D.mSoundVeryDistantThunder.Stop();
    CameraRain2D.mLastRainType = Weather.RainType.None;
  }
}
