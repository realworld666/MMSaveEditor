// Decompiled with JetBrains decompiler
// Type: RubberOnTrackDropdown
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class RubberOnTrackDropdown : UIBaseSessionHudDropdown
{
  public float refreshRate = 1f;
  private float mTimeCounter = 1f;
  public UISessionHUDTrackBar[] sessionHudTrackBars;
  private SessionWeatherDetails mSessionWeatherDetails;

  protected override void OnEnable()
  {
    base.OnEnable();
    this.mSessionWeatherDetails = Game.instance.sessionManager.currentSessionWeather;
    this.UpdateRubberLevels();
  }

  protected override void Update()
  {
    base.Update();
    this.mTimeCounter -= GameTimer.deltaTime;
    if ((double) this.mTimeCounter >= 0.0)
      return;
    this.UpdateRubberLevels();
    this.mTimeCounter = this.refreshRate;
  }

  private void UpdateRubberLevels()
  {
    int num1 = -15;
    int num2 = 5;
    for (int index = 0; index < this.sessionHudTrackBars.Length; ++index)
    {
      float normalizedTrackRubber = this.mSessionWeatherDetails.GetNormalizedTrackRubber();
      int num3 = num1 + num2 * index;
      this.sessionHudTrackBars[index].time.text = num3 != 0 ? num3.ToString() + "mins" : "Now";
      GameUtility.SetImageFillAmountIfDifferent(this.sessionHudTrackBars[index].trackBar, normalizedTrackRubber, 1f / 512f);
    }
  }
}
