// Decompiled with JetBrains decompiler
// Type: UITest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UITest : MonoBehaviour
{
  public UIGauge gaugeRange;
  public UIGauge gaugeRangeWithDelta;
  public UIGauge gaugePositiveNegative;
  public UIGauge gaugeBalance;
  private float mTimer;

  private void Start()
  {
    this.SetGuageValues();
  }

  private void Update()
  {
    this.mTimer -= GameTimer.deltaTime;
    if ((double) this.mTimer >= 0.0)
      return;
    this.SetGuageValues();
  }

  private void SetGuageValues()
  {
    this.mTimer = 5f;
    this.gaugeRange.SetValue(RandomUtility.GetRandom01(), UIGauge.AnimationSetting.Animate);
    this.gaugeRange.ShowDriverPin(0, RandomUtility.GetRandom01());
    this.gaugeRange.ShowDriverPin(1, RandomUtility.GetRandom01());
    this.gaugeRangeWithDelta.SetValueRange(0.0f, 100f);
    this.gaugeRangeWithDelta.SetLeftLabel("0%");
    this.gaugeRangeWithDelta.SetRightLabel("100%");
    this.gaugeRangeWithDelta.SetValue(RandomUtility.GetRandom01() * 100f, UIGauge.AnimationSetting.Animate);
    this.gaugeRangeWithDelta.SetMainLabel(this.gaugeRangeWithDelta.value.ToString("0") + "%");
    this.gaugePositiveNegative.SetValueRange(-1f, 1f);
    this.gaugePositiveNegative.SetLeftLabel("-1");
    this.gaugePositiveNegative.SetRightLabel("1");
    this.gaugePositiveNegative.SetValue(RandomUtility.GetRandom(-1f, 1f), UIGauge.AnimationSetting.Animate);
    this.gaugeBalance.SetValueRange(-1f, 1f);
    this.gaugeBalance.SetLeftLabel("-1");
    this.gaugeBalance.SetRightLabel("1");
    this.gaugeBalance.SetValue(RandomUtility.GetRandom(-1f, 1f), UIGauge.AnimationSetting.Animate);
  }
}
