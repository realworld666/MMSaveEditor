// Decompiled with JetBrains decompiler
// Type: CameraRain
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[ExecuteInEditMode]
public class CameraRain : MonoBehaviour
{
  public float dynamicDropsIntensity = 0.09f;
  public float condensation = 0.03f;
  public float speed = 0.3f;
  public Vector4 offsetVelocity = new Vector4(0.03f, 0.06f, 0.01f, 0.03f);
  private Vector4 mOffset = Vector4.zero;
  private Material mMaterial;
  private Camera mCamera;
  public Texture WaterTex;
  public float staticDropsIntensity;
  private static scSoundContainer mSoundAmbienceRainLow;
  private static scSoundContainer mSoundAmbienceRainHigh;
  private static scOneShotSounds mSoundDistantThunder;
  private static scOneShotSounds mSoundVeryDistantThunder;
  private static Weather.RainType mLastRainType;

  private void Start()
  {
    CameraRain.mSoundDistantThunder = new scOneShotSounds(SoundID.Sfx_ThunderDistant, 15f, 30f);
    CameraRain.mSoundVeryDistantThunder = new scOneShotSounds(SoundID.Sfx_ThunderVeryDistant, 15f, 30f);
    this.mCamera = this.GetComponent<Camera>();
    this.mCamera.depthTextureMode = DepthTextureMode.Depth;
    this.mMaterial = new Material(Shader.Find("Custom/Camera_Rain"));
  }

  private void OnEnable()
  {
  }

  private void OnDisable()
  {
    CameraRain.StopSound();
  }

  private void Update()
  {
    if (!Game.IsActive())
      return;
    SessionWeatherDetails currentSessionWeather = Game.instance.sessionManager.currentSessionWeather;
    if (currentSessionWeather != null && currentSessionWeather.GetCurrentWeather() != null && currentSessionWeather.GetCurrentWeather().rainType != Weather.RainType.None)
    {
      float normalizedRain = currentSessionWeather.GetNormalizedRain();
      this.dynamicDropsIntensity = Mathf.Lerp(0.09f, 0.5f, normalizedRain);
      this.staticDropsIntensity = Mathf.Lerp(0.01f, 0.5f, normalizedRain);
      this.condensation = Mathf.Lerp(0.03f, 0.1f, normalizedRain);
      this.speed = Mathf.Lerp(0.3f, 0.9f, normalizedRain);
      if (!scSessionAmbience.PlayingSound)
        CameraRain.StopSound();
      else if (!scSessionAmbience.IsPaused)
        this.UpdateSound(currentSessionWeather.GetCurrentWeather().rainType);
    }
    else
    {
      this.dynamicDropsIntensity = 0.0f;
      this.staticDropsIntensity = 0.0f;
      this.condensation = 0.0f;
      this.speed = 0.0f;
      CameraRain.StopSound();
    }
    this.mOffset += this.offsetVelocity * this.speed * GameTimer.deltaTime;
  }

  private void UpdateSound(Weather.RainType rainType)
  {
    if (CameraRain.mLastRainType != rainType)
      CameraRain.StopSound();
    CameraRain.mLastRainType = rainType;
    switch (rainType)
    {
      case Weather.RainType.None:
        CameraRain.StopSound();
        break;
      case Weather.RainType.Light:
        scSoundManager.CheckPlaySound(SoundID.Weather_RainLoop_01, ref CameraRain.mSoundAmbienceRainLow, 0.0f);
        scSoundManager.CheckPlaySound(SoundID.Weather_RainLoop_02, ref CameraRain.mSoundAmbienceRainHigh, 0.0f);
        break;
      case Weather.RainType.Medium:
        scSoundManager.CheckPlaySound(SoundID.Weather_RainLoop_03, ref CameraRain.mSoundAmbienceRainLow, 0.0f);
        scSoundManager.CheckPlaySound(SoundID.Weather_RainLoop_04, ref CameraRain.mSoundAmbienceRainHigh, 0.0f);
        break;
      case Weather.RainType.Heavy:
        scSoundManager.CheckPlaySound(SoundID.Weather_RainLoop_05, ref CameraRain.mSoundAmbienceRainLow, 0.0f);
        scSoundManager.CheckPlaySound(SoundID.Weather_RainLoop_06, ref CameraRain.mSoundAmbienceRainHigh, 0.0f);
        CameraRain.mSoundVeryDistantThunder.Update(GameTimer.deltaTime);
        break;
      case Weather.RainType.Monsooon:
        scSoundManager.CheckPlaySound(SoundID.Weather_RainLoop_07, ref CameraRain.mSoundAmbienceRainLow, 0.0f);
        scSoundManager.CheckPlaySound(SoundID.Weather_RainLoop_08, ref CameraRain.mSoundAmbienceRainHigh, 0.0f);
        CameraRain.mSoundVeryDistantThunder.Update(GameTimer.deltaTime);
        CameraRain.mSoundDistantThunder.Update(GameTimer.deltaTime);
        break;
    }
    float num = 1f;
    if (Game.IsActive() && (Object) App.instance.cameraManager.gameCamera != (Object) null)
      num = App.instance.cameraManager.gameCamera.freeRoamCamera.zoomNormalized;
    if ((Object) CameraRain.mSoundAmbienceRainLow != (Object) null)
      CameraRain.mSoundAmbienceRainLow.Volume = num;
    if (!((Object) CameraRain.mSoundAmbienceRainHigh != (Object) null))
      return;
    CameraRain.mSoundAmbienceRainHigh.Volume = (float) (1.0 - (double) num * 0.699999988079071);
  }

  public static void StopSound()
  {
    scSoundManager.CheckStopSound(ref CameraRain.mSoundAmbienceRainLow);
    scSoundManager.CheckStopSound(ref CameraRain.mSoundAmbienceRainHigh);
    if (CameraRain.mSoundDistantThunder != null)
      CameraRain.mSoundDistantThunder.Stop();
    if (CameraRain.mSoundVeryDistantThunder != null)
      CameraRain.mSoundVeryDistantThunder.Stop();
    CameraRain.mLastRainType = Weather.RainType.None;
  }

  private void OnRenderImage(RenderTexture inSource, RenderTexture inDestination)
  {
    this.mMaterial.SetTexture(MaterialPropertyHashes._WaterTex, this.WaterTex);
    this.mMaterial.SetFloat(MaterialPropertyHashes._DynamicDropsIntensity, this.dynamicDropsIntensity);
    this.mMaterial.SetFloat(MaterialPropertyHashes._StaticDropsIntensity, this.staticDropsIntensity);
    this.mMaterial.SetFloat(MaterialPropertyHashes._Condensation, this.condensation);
    this.mMaterial.SetVector(MaterialPropertyHashes._Offset, this.mOffset);
    Graphics.Blit((Texture) inSource, inDestination, this.mMaterial);
  }
}
