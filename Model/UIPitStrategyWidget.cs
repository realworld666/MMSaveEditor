// Decompiled with JetBrains decompiler
// Type: UIPitStrategyWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPitStrategyWidget : UIBaseTimerWidget
{
  private SessionStrategy.PitStrategy mPitStrategy = SessionStrategy.PitStrategy.Balanced;
  private SessionStrategy.PitStrategy mStartPitStrategy = SessionStrategy.PitStrategy.Balanced;
  private RacingVehicle mVehicle;
  public Toggle safeToggle;
  public Toggle balancedToggle;
  public Toggle fastToggle;
  public TextMeshProUGUI safeRisk;
  public TextMeshProUGUI balancedRisk;
  public TextMeshProUGUI fastRisk;
  public RectTransform[] tooltips;

  private void OnEnable()
  {
    this.SetTimeEstimate(this.mVehicle.setup.GetPitStrategyTimeImpact(), true);
  }

  public void SetVehicle(RacingVehicle inVehicle)
  {
    this.mVehicle = inVehicle;
    this.mPitStrategy = this.mVehicle.strategy.pitStrategy;
    this.mStartPitStrategy = this.mPitStrategy;
    UIManager.instance.GetScreen<PitScreen>().OnStrategyChanged(false);
    this.safeRisk.text = this.toPercentage(this.mVehicle.setup.changes.GetStrategyTotalErrorPercent(SessionStrategy.PitStrategy.Safe));
    this.balancedRisk.text = this.toPercentage(this.mVehicle.setup.changes.GetStrategyTotalErrorPercent(SessionStrategy.PitStrategy.Balanced));
    this.fastRisk.text = this.toPercentage(this.mVehicle.setup.changes.GetStrategyTotalErrorPercent(SessionStrategy.PitStrategy.Fast));
    this.ReadToggles();
  }

  private string toPercentage(float inValue)
  {
    return Mathf.RoundToInt(Mathf.Clamp01(inValue) * 100f).ToString() + "%";
  }

  public void SetPitStrategySafe()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.SetPitStrategy(SessionStrategy.PitStrategy.Safe);
  }

  public void SetPitStrategyBalanced()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.SetPitStrategy(SessionStrategy.PitStrategy.Balanced);
  }

  public void SetPitStrategyFast()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.SetPitStrategy(SessionStrategy.PitStrategy.Fast);
  }

  private void SetPitStrategy(SessionStrategy.PitStrategy inPitStrategy)
  {
    if (this.mPitStrategy == inPitStrategy)
      return;
    UIManager.instance.GetScreen<PitScreen>().OnStrategyChanged(this.mStartPitStrategy != inPitStrategy);
    this.mPitStrategy = inPitStrategy;
    this.mVehicle.strategy.SetPitStrategy(this.mPitStrategy);
    this.SetTimeEstimate(this.mVehicle.setup.GetPitStrategyTimeImpact(), true);
  }

  public SessionStrategy.PitStrategy GetPitStrategy()
  {
    return this.mPitStrategy;
  }

  private void ReadToggles()
  {
    if (this.safeToggle.isOn)
      this.SetPitStrategySafe();
    else if (this.balancedToggle.isOn)
      this.SetPitStrategyBalanced();
    else if (this.fastToggle.isOn)
      this.SetPitStrategyFast();
    UIManager.instance.GetScreen<PitScreen>().OnStrategyChanged(false);
  }

  public void ShowRiskBreakdown(int inValue)
  {
    UIManager.instance.dialogBoxManager.GetDialog<TimeBreakdownRollover>().ShowErrorChance(this.tooltips[inValue], this.mVehicle, inValue);
  }

  public void HideRiskBreakdown()
  {
    UIManager.instance.dialogBoxManager.GetDialog<TimeBreakdownRollover>().Hide();
  }
}
