// Decompiled with JetBrains decompiler
// Type: WaterOnTrackDropdown
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class WaterOnTrackDropdown : UIBaseSessionHudDropdown
{
  public float refreshRate = 1f;
  private float mTimeCounter = 1f;
  public UISessionHUDTrackBar[] sessionHudTrackBars;
  private SessionWeatherDetails mSessionWeatherDetails;

  protected override void OnEnable()
  {
    base.OnEnable();
    this.mSessionWeatherDetails = Game.instance.sessionManager.currentSessionWeather;
    this.UpdateWaterLevels();
  }

  protected override void Update()
  {
    base.Update();
    this.mTimeCounter -= GameTimer.deltaTime;
    if ((double) this.mTimeCounter >= 0.0)
      return;
    this.UpdateWaterLevels();
    this.mTimeCounter = this.refreshRate;
  }

  private void UpdateWaterLevels()
  {
    float num1 = 300f / Game.instance.sessionManager.GetSessionDuration();
    float num2 = this.mSessionWeatherDetails.normalizedTimeElapsed - num1 * 3f;
    int num3 = -15;
    int num4 = 5;
    for (int index = 0; index < this.sessionHudTrackBars.Length; ++index)
    {
      float num5 = Mathf.Clamp01(this.mSessionWeatherDetails.sessionWaterLevelCurve.curve.Evaluate(num2 + (float) index * num1));
      int num6 = num3 + num4 * index;
      this.sessionHudTrackBars[index].time.text = num6 != 0 ? num6.ToString() + "mins" : "Now";
      this.sessionHudTrackBars[index].trackBar.fillAmount = num5;
    }
  }
}
