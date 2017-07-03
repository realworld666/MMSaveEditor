// Decompiled with JetBrains decompiler
// Type: UIGauge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGauge : MonoBehaviour
{
  private static int driverPinCount = 2;
  public float animationDuration = 1f;
  private Image[] mPinDriver = new Image[UIGauge.driverPinCount];
  private Vector2 mPinRotationRange = new Vector2(90f, -90f);
  private Vector2 mValueRange = new Vector2(0.0f, 1f);
  public UIGauge.GaugeType gaugeType;
  public EasingUtility.Easing animationCurve;
  private UIGauge.State mState;
  private float mTimer;
  private GameObject mRange;
  private GameObject mRangeWithDelta;
  private GameObject mPositiveNegative;
  private GameObject mBalance;
  private Transform mTypeTransform;
  private TextMeshProUGUI mLeftLabel;
  private TextMeshProUGUI mRightLabel;
  private TextMeshProUGUI mMainLabel;
  private TextMeshProUGUI mTitleLabel;
  private TextMeshProUGUI mSubLabel;
  private Image mPin;
  private Image mFill;
  private Image mFillDelta;
  private Image mFillNegative;
  private Image mFillPositive;
  private float mValue;
  private float mPreviousValue;

  public float maxRange
  {
    get
    {
      return this.mValueRange.y;
    }
  }

  public float value
  {
    get
    {
      return this.mValue;
    }
  }

  public float previousValue
  {
    get
    {
      return this.mPreviousValue;
    }
  }

  private void Awake()
  {
    this.FindArtwork();
    this.UpdateType();
  }

  private void FindArtwork()
  {
    this.mRange = this.FindChild("Dial/Types/Range", this.transform);
    this.mRangeWithDelta = this.FindChild("Dial/Types/RangeWithDelta", this.transform);
    this.mPositiveNegative = this.FindChild("Dial/Types/PositiveNegative", this.transform);
    this.mBalance = this.FindChild("Dial/Types/Balance", this.transform);
    this.mLeftLabel = this.FindChild<TextMeshProUGUI>("Labels/LeftLabel", this.transform);
    this.mRightLabel = this.FindChild<TextMeshProUGUI>("Labels/RightLabel", this.transform);
    this.mMainLabel = this.FindChild<TextMeshProUGUI>("Labels/MainLabel", this.transform);
    this.mSubLabel = this.FindChild<TextMeshProUGUI>("Labels/JobTitle", this.transform);
    this.mTitleLabel = this.FindChild<TextMeshProUGUI>("Header/Header", this.transform);
    this.mPin = this.FindChild<Image>("Dial/Pins/Pin", this.transform);
    this.mPinDriver[0] = this.FindChild<Image>("Dial/Pins/PinDriver1", this.transform);
    this.mPinDriver[1] = this.FindChild<Image>("Dial/Pins/PinDriver2", this.transform);
    for (int inDriverIndex = 0; inDriverIndex < UIGauge.driverPinCount; ++inDriverIndex)
      this.HideDriverPin(inDriverIndex);
  }

  private void CheckArtworkFound()
  {
    if (!((Object) this.mRange == (Object) null) || !((Object) this.mRangeWithDelta == (Object) null) || (!((Object) this.mPositiveNegative == (Object) null) || !((Object) this.mBalance == (Object) null)))
      return;
    this.FindArtwork();
    this.UpdateType();
  }

  public void UpdateType()
  {
    if ((Object) this.mRange == (Object) null && (Object) this.mRangeWithDelta == (Object) null && ((Object) this.mPositiveNegative == (Object) null && (Object) this.mBalance == (Object) null))
      this.FindArtwork();
    if ((Object) this.mRange != (Object) null)
      this.mRange.SetActive(false);
    if ((Object) this.mRangeWithDelta != (Object) null)
      this.mRangeWithDelta.SetActive(false);
    if ((Object) this.mPositiveNegative != (Object) null)
      this.mPositiveNegative.SetActive(false);
    if ((Object) this.mBalance != (Object) null)
      this.mBalance.SetActive(false);
    switch (this.gaugeType)
    {
      case UIGauge.GaugeType.Range:
        this.UpdateRangeType();
        break;
      case UIGauge.GaugeType.RangeWithDelta:
        this.UpdateRangeWithDeltaType();
        break;
      case UIGauge.GaugeType.PositiveNegative:
        this.UpdatePositiveNegativeType();
        break;
      case UIGauge.GaugeType.Balance:
        this.UpdateBalanceType();
        break;
    }
  }

  private void UpdateRangeType()
  {
    if (!((Object) this.mRange != (Object) null))
      return;
    this.mRange.SetActive(true);
    this.mTypeTransform = this.mRange.transform;
    this.mFill = this.FindChild<Image>("Fill", this.mTypeTransform);
    this.UpdateVisuals(this.mPreviousValue, this.mValue);
  }

  private void UpdateRangeWithDeltaType()
  {
    if (!((Object) this.mRangeWithDelta != (Object) null))
      return;
    this.mRangeWithDelta.SetActive(true);
    this.mTypeTransform = this.mRangeWithDelta.transform;
    this.mFill = this.FindChild<Image>("Fill", this.mTypeTransform);
    this.mFillDelta = this.FindChild<Image>("FillDelta", this.mTypeTransform);
    this.UpdateVisuals(this.mPreviousValue, this.mValue);
  }

  private void UpdatePositiveNegativeType()
  {
    if (!((Object) this.mPositiveNegative != (Object) null))
      return;
    this.mPositiveNegative.SetActive(true);
    this.mTypeTransform = this.mPositiveNegative.transform;
    this.mFill = (Image) null;
    this.mFillNegative = this.FindChild<Image>("FillNegative", this.mTypeTransform);
    this.mFillPositive = this.FindChild<Image>("FillPositive", this.mTypeTransform);
    this.UpdateVisuals(this.mPreviousValue, this.mValue);
  }

  private void UpdateBalanceType()
  {
    if (!((Object) this.mBalance != (Object) null))
      return;
    this.mBalance.SetActive(true);
    this.mTypeTransform = this.mBalance.transform;
    this.mFill = this.FindChild<Image>("Fill", this.mTypeTransform);
    this.UpdateVisuals(this.mPreviousValue, this.mValue);
  }

  private void Update()
  {
    if (this.mState != UIGauge.State.Animating)
      return;
    this.mTimer += GameTimer.deltaTime;
    this.UpdateVisuals(this.mPreviousValue, Mathf.Lerp(this.mPreviousValue, this.mValue, EasingUtility.EaseByType(this.animationCurve, 0.0f, 1f, Mathf.Clamp01(this.mTimer / this.animationDuration))));
    if ((double) this.mTimer < (double) this.animationDuration)
      return;
    this.StopAnimation();
  }

  private void StartAnimation()
  {
    this.mState = UIGauge.State.Animating;
    this.mTimer = 0.0f;
  }

  private void StopAnimation()
  {
    this.mState = UIGauge.State.Idle;
  }

  private GameObject FindChild(string inName, Transform inTransform)
  {
    GameObject gameObject = (GameObject) null;
    Transform child = inTransform.FindChild(inName);
    if ((Object) child != (Object) null)
      gameObject = child.gameObject;
    return gameObject;
  }

  private T FindChild<T>(string inName, Transform inTransform) where T : Component
  {
    T obj = (T) null;
    Transform child = inTransform.FindChild(inName);
    if ((Object) child != (Object) null)
      obj = child.GetComponent<T>();
    return obj;
  }

  public void SetLeftLabel(string inText)
  {
    this.CheckArtworkFound();
    if (!((Object) this.mLeftLabel != (Object) null))
      return;
    this.mLeftLabel.text = inText;
  }

  public void SetRightLabel(string inText)
  {
    this.CheckArtworkFound();
    if (!((Object) this.mRightLabel != (Object) null))
      return;
    this.mRightLabel.text = inText;
  }

  public void SetMainLabel(string inText)
  {
    this.CheckArtworkFound();
    if (!((Object) this.mMainLabel != (Object) null))
      return;
    this.mMainLabel.text = inText;
  }

  public void SetJobLabel(string inText)
  {
    this.CheckArtworkFound();
    if (!((Object) this.mSubLabel != (Object) null))
      return;
    this.mSubLabel.text = inText;
  }

  public void SetTitleLabel(string inText)
  {
    this.CheckArtworkFound();
    if (!((Object) this.mTitleLabel != (Object) null))
      return;
    this.mTitleLabel.text = inText;
  }

  public void ShowDriverPin(int inDriverIndex, float inValue)
  {
    this.CheckArtworkFound();
    this.mPinDriver[inDriverIndex].gameObject.SetActive(true);
    float z = Mathf.Lerp(this.mPinRotationRange.x, this.mPinRotationRange.y, Mathf.Clamp01((float) (((double) inValue - (double) this.mValueRange.x) / ((double) this.mValueRange.y - (double) this.mValueRange.x))));
    this.mPinDriver[inDriverIndex].transform.rotation = Quaternion.Euler(0.0f, 0.0f, z);
  }

  public void SetDriverPinAlpha(int inDriverIndex, float inValue)
  {
    if (!((Object) this.mPinDriver[inDriverIndex] != (Object) null))
      return;
    Image component = this.mPinDriver[inDriverIndex].gameObject.GetComponent<Image>();
    if (!((Object) component != (Object) null))
      return;
    Color color = component.color;
    color.a = inValue;
    component.color = color;
  }

  public void HideDriverPin(int inDriverIndex)
  {
    if (!((Object) this.mPinDriver[inDriverIndex] != (Object) null))
      return;
    this.mPinDriver[inDriverIndex].gameObject.SetActive(false);
  }

  public void SetValueRange(float inMin, float inMax)
  {
    this.CheckArtworkFound();
    this.mValueRange.x = inMin;
    this.mValueRange.y = inMax;
  }

  public void SetValue(float inValue, UIGauge.AnimationSetting inAnimation)
  {
    if (Mathf.Approximately(this.mValue, inValue))
      return;
    this.mPreviousValue = this.mValue;
    this.mValue = inValue;
    if (inAnimation == UIGauge.AnimationSetting.Animate)
      this.StartAnimation();
    else
      this.UpdateVisuals(this.mPreviousValue, this.mValue);
  }

  private void UpdateVisuals(float inPreviousValue, float inValue)
  {
    this.CheckArtworkFound();
    float t = Mathf.Clamp01((float) (((double) inValue - (double) this.mValueRange.x) / ((double) this.mValueRange.y - (double) this.mValueRange.x)));
    if (this.gaugeType == UIGauge.GaugeType.PositiveNegative)
    {
      if ((double) t < 0.5)
      {
        this.mFillNegative.fillAmount = (float) (1.0 - (double) t / 0.5);
        this.mFillPositive.fillAmount = 0.0f;
      }
      else
      {
        this.mFillNegative.fillAmount = 0.0f;
        this.mFillPositive.fillAmount = (float) (((double) t - 0.5) / 0.5);
      }
    }
    else if (this.gaugeType == UIGauge.GaugeType.RangeWithDelta)
    {
      if ((double) inPreviousValue > (double) inValue)
      {
        this.mFillDelta.fillAmount = Mathf.Clamp01((float) (((double) inPreviousValue - (double) this.mValueRange.x) / ((double) this.mValueRange.y - (double) this.mValueRange.x)));
        t = Mathf.Clamp01((float) (((double) inValue - (double) this.mValueRange.x) / ((double) this.mValueRange.y - (double) this.mValueRange.x)));
        this.mFill.fillAmount = t;
        this.mFillDelta.color = UIConstants.negativeColor;
      }
      else
      {
        this.mFill.fillAmount = Mathf.Clamp01((float) (((double) inPreviousValue - (double) this.mValueRange.x) / ((double) this.mValueRange.y - (double) this.mValueRange.x)));
        t = Mathf.Clamp01((float) (((double) inValue - (double) this.mValueRange.x) / ((double) this.mValueRange.y - (double) this.mValueRange.x)));
        this.mFillDelta.fillAmount = t;
        this.mFillDelta.color = UIConstants.positiveColor;
      }
    }
    else if (this.gaugeType != UIGauge.GaugeType.Balance)
      this.mFill.fillAmount = t;
    this.mPin.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Lerp(this.mPinRotationRange.x, this.mPinRotationRange.y, t));
  }

  public enum GaugeType
  {
    Range,
    RangeWithDelta,
    PositiveNegative,
    Balance,
  }

  public enum State
  {
    Idle,
    Animating,
  }

  public enum AnimationSetting
  {
    Animate,
    DontAnimate,
  }
}
