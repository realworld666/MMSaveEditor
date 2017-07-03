// Decompiled with JetBrains decompiler
// Type: UITeamReportScreenStatEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITeamReportScreenStatEntry : MonoBehaviour
{
  public float animationDuration = 1f;
  public bool isDriverStat = true;
  private AnimatedFloat mAnimatedFloat = new AnimatedFloat();
  private int mLastValue = -1;
  public Image statBar;
  public TextMeshProUGUI ammount;
  public TextMeshProUGUI change;
  public float animationDelay;
  public EasingUtility.Easing animationCurve;
  public bool usePercentageAmmount;
  private UITeamReportScreenDriverWidget.UIDriverStatChangeEntry mEntry;
  private int mCurrentValue;
  private float mTimer;
  private float mTotalTimer;
  private float mChangeValue;
  private float mBarValue;
  private float mBarTotal;
  private bool mAnimating;

  public void Setup(UITeamReportScreenDriverWidget.UIDriverStatChangeEntry inEntry)
  {
    this.mAnimating = false;
    this.mEntry = inEntry;
    if ((UnityEngine.Object) this.statBar != (UnityEngine.Object) null)
    {
      this.mAnimatedFloat.SetValue(this.mEntry.oldValue, AnimatedFloat.Animation.DontAnimate);
      GameUtility.SetImageFillAmountIfDifferent(this.statBar, this.mEntry.barOldValueNormalized, 1f / 512f);
      this.SetBarColor();
    }
    this.SetAmmountValues();
    this.mChangeValue = !this.isDriverStat ? MathsUtility.RoundToDecimal(Mathf.Clamp(this.mEntry.valueChange / this.mEntry.statMax, -1f, 1f) * 100f, 2f) : MathsUtility.RoundToDecimal(Mathf.Clamp(this.mEntry.valueChange, -this.mEntry.statMax, this.mEntry.statMax) * 100f, 2f);
    GameUtility.SetActive(this.change.gameObject, false);
    this.change.color = this.GetColor(this.mChangeValue);
    this.change.text = this.FormatChange(this.mChangeValue);
  }

  public void Animate()
  {
    if ((UnityEngine.Object) this.statBar != (UnityEngine.Object) null)
      this.mAnimatedFloat.SetValue(this.mEntry.newValue, AnimatedFloat.Animation.Animate, 0.0f, this.animationDuration, this.animationCurve);
    this.mTotalTimer = 0.0f;
    this.mAnimating = true;
  }

  public void Update()
  {
    if (!this.mAnimating)
      return;
    this.mTotalTimer += GameTimer.deltaTime;
    this.mTimer = this.mTotalTimer - this.animationDelay;
    if ((double) this.mTimer < 0.0)
      return;
    if ((UnityEngine.Object) this.statBar != (UnityEngine.Object) null)
    {
      this.mAnimatedFloat.Update();
      this.mBarValue = !this.isDriverStat ? Mathf.Clamp01(this.mAnimatedFloat.value / this.mEntry.statMax) : Mathf.Clamp01(this.mAnimatedFloat.value - (float) (int) this.mAnimatedFloat.value);
      GameUtility.SetImageFillAmountIfDifferent(this.statBar, this.mBarValue, 1f / 512f);
      this.SetAmmountValues();
      this.SetBarColor();
    }
    if ((double) this.mTimer < (double) this.animationDuration)
      return;
    GameUtility.SetActive(this.change.gameObject, true);
    this.mAnimating = false;
  }

  private string FormatChange(float inValue)
  {
    if ((double) inValue == 0.0)
      return "-";
    return ((double) inValue <= 0.0 ? string.Empty : "+") + string.Format((IFormatProvider) Localisation.numberFormatter, "{0:f2}%", new object[1]
    {
      (object) this.mChangeValue
    });
  }

  private Color GetColor(float inValue)
  {
    if ((double) inValue > 0.0)
      return UIConstants.positiveColor;
    if ((double) inValue == 0.0)
      return UIConstants.whiteColor;
    return UIConstants.negativeColor;
  }

  private void SetAmmountValues()
  {
    this.mCurrentValue = !((UnityEngine.Object) this.statBar != (UnityEngine.Object) null) ? Mathf.RoundToInt(this.mEntry.newValue) : Mathf.Clamp((int) this.mAnimatedFloat.value, 0, (int) this.mEntry.statMax);
    if (this.mLastValue == this.mCurrentValue)
      return;
    this.ammount.text = this.mCurrentValue.ToString() + (!this.usePercentageAmmount ? string.Empty : "%");
    this.mLastValue = this.mCurrentValue;
  }

  private void SetBarColor()
  {
    if (!this.isDriverStat)
      return;
    this.mBarTotal = Mathf.Clamp01(this.mAnimatedFloat.value / this.mEntry.statMax);
    Color color = UIConstants.colorBandGreen;
    if ((double) this.mBarTotal <= 0.25)
      color = UIConstants.colorBandRed;
    else if ((double) this.mBarTotal <= 0.5)
      color = UIConstants.colorBandYellow;
    else if ((double) this.mBarTotal <= 0.75)
      color = UIConstants.colorBandBlue;
    color.a = this.statBar.color.a;
    if (!(this.statBar.color != color))
      return;
    this.statBar.color = color;
  }
}
