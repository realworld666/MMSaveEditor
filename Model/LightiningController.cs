// Decompiled with JetBrains decompiler
// Type: LightiningController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using uSky;

public class LightiningController
{
  private float mLightiningSpeed = 2f;
  private LightiningController.State mState = LightiningController.State.Done;
  public Gradient mLightiningColor = new Gradient() { colorKeys = new GradientColorKey[2]{ new GradientColorKey((Color) new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue), 0.23f), new GradientColorKey((Color) new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue), 0.77f) }, alphaKeys = new GradientAlphaKey[2]{ new GradientAlphaKey(1f, 0.0f), new GradientAlphaKey(1f, 1f) } };
  private float mLightiningBrightNess;
  private float mInitialBrigtness;
  private float mLightiningTimer;
  private float mHugeThunderSoundCount;
  public Gradient mOriginalLightColor;
  private uSkyLight mSkyLight;

  public LightiningController.State state
  {
    get
    {
      return this.mState;
    }
  }

  public LightiningController(uSkyLight inSkyLight)
  {
    this.mSkyLight = inSkyLight;
  }

  public void InitializeLightining()
  {
    this.mLightiningTimer = 0.0f;
    this.mInitialBrigtness = this.mSkyLight.SunIntensity;
    this.mOriginalLightColor = this.mSkyLight.LightColor;
    this.mSkyLight.LightColor = (Gradient) null;
    this.mSkyLight.LightColor = this.mLightiningColor;
    this.mLightiningSpeed = RandomUtility.GetRandom(1.3f, 2f);
    this.mState = LightiningController.State.Start;
    bool flag = false;
    SessionWeatherDetails currentSessionWeather = Game.instance.sessionManager.currentSessionWeather;
    if (currentSessionWeather != null && currentSessionWeather.GetCurrentWeather() != null && currentSessionWeather.GetCurrentWeather().rainType == Weather.RainType.Monsooon)
    {
      --this.mHugeThunderSoundCount;
      if ((double) this.mHugeThunderSoundCount < 0.0)
      {
        this.mHugeThunderSoundCount = Random.Range(1f, 3f);
        flag = true;
      }
    }
    if (flag)
      scSoundManager.Instance.PlaySound(SoundID.Sfx_ThunderHugeCrack, 0.0f);
    else
      scSoundManager.Instance.PlaySound(SoundID.Ambience_Thunder, 0.0f);
  }

  public void UpdateLightining()
  {
    if (this.mState == LightiningController.State.Done)
      return;
    this.mLightiningTimer += GameTimer.simulationDeltaTime * this.mLightiningSpeed;
    float num = 5f;
    switch (this.mState)
    {
      case LightiningController.State.Start:
        this.mLightiningBrightNess = EasingUtility.EaseByType(EasingUtility.Easing.OutElastic, this.mInitialBrigtness, num, this.mLightiningTimer);
        break;
      case LightiningController.State.Ending:
        this.mLightiningBrightNess = EasingUtility.EaseByType(EasingUtility.Easing.OutElastic, num, this.mInitialBrigtness, this.mLightiningTimer);
        break;
    }
    this.mSkyLight.SunIntensity = this.mLightiningBrightNess;
    if ((double) this.mLightiningTimer <= 1.0)
      return;
    this.mState = this.mState + 1;
    this.mLightiningTimer = 0.0f;
    this.mLightiningSpeed = RandomUtility.GetRandom(1.3f, 2f);
    if (this.mState != LightiningController.State.Done)
      return;
    this.mSkyLight.LightColor = this.mOriginalLightColor;
  }

  public enum State
  {
    Start,
    Ending,
    Done,
  }
}
