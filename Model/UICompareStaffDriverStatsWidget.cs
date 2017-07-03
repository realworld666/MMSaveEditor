// Decompiled with JetBrains decompiler
// Type: UICompareStaffDriverStatsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UICompareStaffDriverStatsWidget : MonoBehaviour
{
  public UIGauge moraleGauge;
  public GameObject noFormWidget;
  public DriverCareerFormWidget formWidget;
  public UIDriverScreenTraitsWidget traitsWidget;
  private Driver mDriver;

  public void Setup(Driver inDriver)
  {
    if (inDriver == null)
      return;
    this.mDriver = inDriver;
    this.SetGauges();
    this.SetDriverForm();
    this.SetDriverTraits();
  }

  public void SetGauges()
  {
    this.moraleGauge.SetValueRange(0.0f, 1f);
    this.moraleGauge.SetValue(this.mDriver.GetMorale(), UIGauge.AnimationSetting.Animate);
  }

  public void SetDriverForm()
  {
    GameUtility.SetActive(this.noFormWidget, !this.mDriver.IsInAChampionship());
    GameUtility.SetActive(this.formWidget.gameObject, this.mDriver.IsInAChampionship());
    if (!this.formWidget.gameObject.activeSelf)
      return;
    this.formWidget.SetDriver(this.mDriver);
  }

  public void SetDriverTraits()
  {
    this.traitsWidget.SetupDriverTraits(this.mDriver);
  }
}
