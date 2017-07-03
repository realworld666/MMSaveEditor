// Decompiled with JetBrains decompiler
// Type: UISetupInputWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISetupInputWidget : UIBaseTimerWidget
{
  private static float mInputValueMin = -1f;
  private static float mInputValueMax = 1f;
  private static readonly int mNumberOfButtonSteps = 10;
  private static readonly int mNumberOfDiscreteSliderStepsDefault = 16;
  private static int mNumberOfDiscreteBallastSliderSteps = 51;
  private SessionSetup.SetupOutput mTargetSetupOutput = new SessionSetup.SetupOutput();
  private SessionSetup.SetupOutput mCurrentSetupOutput = new SessionSetup.SetupOutput();
  private bool[] mSetupValueChanged = new bool[7];
  public UISetupBalanceWidget setupBalanceWidget;
  public TextMeshProUGUI ballastLabelFront;
  public TextMeshProUGUI ballastLabelMiddle;
  public TextMeshProUGUI ballastLabelRear;
  public Image frontFill;
  public Image rearFill;
  public Image driverFill;
  public Image overallMiddleFill;
  public TextMeshProUGUI ballastDriverWeightLabel;
  public Slider ballastSlider;
  public GameObject ballastGameObject;
  public TextMeshProUGUI frontWingAngleLabel;
  public Slider frontWingAngleSlider;
  public GameObject frontWingSliderObject;
  public TextMeshProUGUI rearWingAngleLabel;
  public Slider rearWingAngleSlider;
  public GameObject rearWingSliderObject;
  public TextMeshProUGUI gearRatioLabel;
  public Slider gearRatioSlider;
  public TextMeshProUGUI tyrePressureLabel;
  public Button addPressureButton;
  public Button minusPressureButton;
  public Image tyrePressureFill;
  public TextMeshProUGUI tyreCamberLabel;
  public Button addCamberButton;
  public Button minusCamberButton;
  public GameObject tyreToRotate;
  public TextMeshProUGUI suspensionStiffnessLabel;
  public Slider suspensionStiffnessSlider;
  private SetupInput_v1 mSetupInput;
  private RacingVehicle mVehicle;
  private float mCamberValue;
  private float mPressureValue;

  public static int numberCamberPressureSteps
  {
    get
    {
      return UISetupInputWidget.mNumberOfButtonSteps;
    }
  }

  public static int numberSuspensionGearingSteps
  {
    get
    {
      return UISetupInputWidget.mNumberOfDiscreteSliderStepsDefault;
    }
  }

  public static int numberOfBallastSteps
  {
    get
    {
      return UISetupInputWidget.mNumberOfDiscreteBallastSliderSteps;
    }
  }

  private void SetupSliders()
  {
    Championship.Series series = Game.instance.sessionManager.championship.series;
    GameUtility.SetActive(this.frontWingSliderObject, series == Championship.Series.SingleSeaterSeries);
    GameUtility.SetActive(this.rearWingSliderObject, series == Championship.Series.SingleSeaterSeries);
    GameUtility.SetActive(this.ballastGameObject, series == Championship.Series.GTSeries);
    switch (series)
    {
      default:
        this.ballastSlider.minValue = UISetupInputWidget.mInputValueMin;
        this.ballastSlider.maxValue = (float) UISetupInputWidget.mNumberOfDiscreteBallastSliderSteps;
        this.ballastDriverWeightLabel.text = this.mVehicle.driver.name + " - " + GameUtility.GetWeightText((float) this.mVehicle.driver.weight, 1);
        this.frontWingAngleSlider.minValue = UISetupInputWidget.mInputValueMin;
        this.frontWingAngleSlider.maxValue = UISetupInputWidget.mInputValueMax;
        this.rearWingAngleSlider.minValue = UISetupInputWidget.mInputValueMin;
        this.rearWingAngleSlider.maxValue = UISetupInputWidget.mInputValueMax;
        this.gearRatioSlider.minValue = 0.0f;
        this.gearRatioSlider.maxValue = (float) UISetupInputWidget.mNumberOfDiscreteSliderStepsDefault;
        this.suspensionStiffnessSlider.minValue = 0.0f;
        this.suspensionStiffnessSlider.maxValue = (float) UISetupInputWidget.mNumberOfDiscreteSliderStepsDefault;
        this.addCamberButton.onClick.RemoveAllListeners();
        this.minusCamberButton.onClick.RemoveAllListeners();
        this.addPressureButton.onClick.RemoveAllListeners();
        this.minusPressureButton.onClick.RemoveAllListeners();
        this.addCamberButton.onClick.AddListener((UnityAction) (() => this.HandleButtonPress(this.addCamberButton, this.minusCamberButton, this.mCamberValue, UISetupInputWidget.ButtonPressed.AddButton, UISetupInputWidget.SetupType.TyreCamber)));
        this.minusCamberButton.onClick.AddListener((UnityAction) (() => this.HandleButtonPress(this.addCamberButton, this.minusCamberButton, this.mCamberValue, UISetupInputWidget.ButtonPressed.MinusButton, UISetupInputWidget.SetupType.TyreCamber)));
        this.addPressureButton.onClick.AddListener((UnityAction) (() => this.HandleButtonPress(this.addPressureButton, this.minusPressureButton, this.mPressureValue, UISetupInputWidget.ButtonPressed.AddButton, UISetupInputWidget.SetupType.TyrePressure)));
        this.minusPressureButton.onClick.AddListener((UnityAction) (() => this.HandleButtonPress(this.addPressureButton, this.minusPressureButton, this.mPressureValue, UISetupInputWidget.ButtonPressed.MinusButton, UISetupInputWidget.SetupType.TyrePressure)));
        this.ballastSlider.onValueChanged.AddListener((UnityAction<float>) (param0 => this.HandleSliderChange(UISetupInputWidget.SetupType.Ballast)));
        this.frontWingAngleSlider.onValueChanged.AddListener((UnityAction<float>) (param0 => this.HandleSliderChange(UISetupInputWidget.SetupType.FrontWingAngle)));
        this.rearWingAngleSlider.onValueChanged.AddListener((UnityAction<float>) (param0 => this.HandleSliderChange(UISetupInputWidget.SetupType.RearWingAngle)));
        this.gearRatioSlider.onValueChanged.AddListener((UnityAction<float>) (param0 => this.HandleSliderChange(UISetupInputWidget.SetupType.GearRatio)));
        this.suspensionStiffnessSlider.onValueChanged.AddListener((UnityAction<float>) (param0 => this.HandleSliderChange(UISetupInputWidget.SetupType.SuspensionStiffness)));
        break;
    }
  }

  public void SetVehicle(RacingVehicle inVehicle)
  {
    this.mVehicle = inVehicle;
    this.mSetupInput = new SetupInput_v1();
    this.SetupSliders();
    this.SetToSetupInput(this.mVehicle.setup.currentSetup.input);
    this.mVehicle.setup.GetSetupOutput(ref this.mCurrentSetupOutput);
    this.OnSetupChanged();
  }

  private void OnSetupChanged()
  {
    for (int index = 0; index < this.mSetupValueChanged.Length; ++index)
    {
      if (this.mSetupValueChanged[index])
      {
        this.mSetupValueChanged[index] = false;
        switch (index)
        {
          case 0:
            StringVariableParser.ordinalNumberString = Mathf.Lerp(18f, 24f, this.GetNormalisedInputValue(this.mPressureValue)).ToString("0.0", (IFormatProvider) Localisation.numberFormatter);
            this.tyrePressureLabel.text = Localisation.LocaliseID("PSG_10010649", (GameObject) null);
            continue;
          case 1:
            this.tyreCamberLabel.text = Mathf.Lerp(-4f, 0.0f, this.GetNormalisedInputValue(this.mCamberValue)).ToString("0.0", (IFormatProvider) Localisation.numberFormatter) + "°";
            continue;
          case 2:
            this.frontWingAngleLabel.text = Mathf.Lerp(10f, 20f, this.GetNormalisedInputValue(this.frontWingAngleSlider.value)).ToString("0.0", (IFormatProvider) Localisation.numberFormatter) + "°";
            continue;
          case 3:
            this.rearWingAngleLabel.text = Mathf.Lerp(20f, 30f, this.GetNormalisedInputValue(this.rearWingAngleSlider.value)).ToString("0.0", (IFormatProvider) Localisation.numberFormatter) + "°";
            continue;
          case 4:
            this.gearRatioLabel.text = Mathf.Lerp(0.0f, 100f, this.GetNormalisedInputValue(this.GetInputValueFromIntSlider(this.gearRatioSlider, UISetupInputWidget.mNumberOfDiscreteSliderStepsDefault))).ToString("0") + "%";
            continue;
          case 5:
            this.suspensionStiffnessLabel.text = Mathf.Lerp(0.0f, 100f, this.GetNormalisedInputValue(this.GetInputValueFromIntSlider(this.suspensionStiffnessSlider, UISetupInputWidget.mNumberOfDiscreteSliderStepsDefault))).ToString("0") + "%";
            continue;
          case 6:
            this.ArrangeBallast(this.GetNormalisedInputValue(this.GetInputValueFromIntSlider(this.ballastSlider, UISetupInputWidget.mNumberOfDiscreteBallastSliderSteps)));
            continue;
          default:
            continue;
        }
      }
    }
    if (this.mVehicle == null)
      return;
    this.UpdateSetupInput();
    this.mSetupInput.GetSetupOutput(ref this.mTargetSetupOutput, (float) this.mVehicle.driver.weight);
    this.setupBalanceWidget.SetSetupOutput(this.mCurrentSetupOutput, this.mTargetSetupOutput);
    this.mVehicle.setup.SetTargetSetupInput(this.mSetupInput);
    UIManager.instance.GetScreen<PitScreen>().OnSetupChanged(this.mSetupInput != this.mVehicle.setup.currentSetup.input);
    this.RefreshTime();
  }

  public void ArrangeBallast(float inNormalisedSliderValue)
  {
    float num1 = (float) (this.mVehicle.driver.weight + GameStatsConstants.ballastWeightKg);
    float num2 = Mathf.Lerp(0.0f, 0.3333333f, ((double) inNormalisedSliderValue <= 0.5 ? inNormalisedSliderValue : 1f - inNormalisedSliderValue) / 0.5f);
    float num3 = inNormalisedSliderValue * (1f - num2);
    float num4 = (float) ((1.0 - (double) inNormalisedSliderValue) * (1.0 - (double) num2));
    int num5 = Mathf.RoundToInt((float) GameStatsConstants.ballastWeightKg * num4);
    int num6 = Mathf.RoundToInt((float) GameStatsConstants.ballastWeightKg * num3);
    int num7 = GameStatsConstants.ballastWeightKg - (num5 + num6) + this.mVehicle.driver.weight;
    this.ballastLabelMiddle.text = GameUtility.GetWeightText((float) num7, 1);
    this.ballastLabelRear.text = GameUtility.GetWeightText((float) num5, 1);
    this.ballastLabelFront.text = GameUtility.GetWeightText((float) num6, 1);
    this.overallMiddleFill.fillAmount = (float) num7 / num1;
    this.driverFill.fillAmount = (float) this.mVehicle.driver.weight / num1;
    this.frontFill.fillAmount = (float) num6 / num1;
    this.rearFill.fillAmount = (float) num5 / num1;
  }

  public void SetToSetupInput(SetupInput_v1 inSetupInput)
  {
    switch (inSetupInput.series)
    {
      case Championship.Series.SingleSeaterSeries:
        GameUtility.SetSliderAmountIfDifferent(this.frontWingAngleSlider, Mathf.Clamp(inSetupInput.GetSetupValue(SetupInput_v1.SetupInputOptions.frontWingAngleOption), UISetupInputWidget.mInputValueMin, UISetupInputWidget.mInputValueMax), 1000f);
        GameUtility.SetSliderAmountIfDifferent(this.rearWingAngleSlider, Mathf.Clamp(inSetupInput.GetSetupValue(SetupInput_v1.SetupInputOptions.rearWingAngleOption), UISetupInputWidget.mInputValueMin, UISetupInputWidget.mInputValueMax), 1000f);
        break;
      case Championship.Series.GTSeries:
        GameUtility.SetSliderAmountIfDifferent(this.ballastSlider, (float) Mathf.Clamp(Mathf.RoundToInt(this.GetNormalisedInputValue(inSetupInput.GetSetupValue(SetupInput_v1.SetupInputOptions.ballastDistributionOption)) * (float) UISetupInputWidget.mNumberOfDiscreteBallastSliderSteps), 0, UISetupInputWidget.mNumberOfDiscreteBallastSliderSteps), 1000f);
        break;
    }
    this.mPressureValue = Mathf.Clamp(inSetupInput.GetSetupValue(SetupInput_v1.SetupInputOptions.tyrePressureOption), UISetupInputWidget.mInputValueMin, UISetupInputWidget.mInputValueMax);
    this.mCamberValue = Mathf.Clamp(inSetupInput.GetSetupValue(SetupInput_v1.SetupInputOptions.tyreCamberOption), UISetupInputWidget.mInputValueMin, UISetupInputWidget.mInputValueMax);
    GameUtility.SetSliderAmountIfDifferent(this.suspensionStiffnessSlider, (float) Mathf.Clamp(Mathf.RoundToInt(this.GetNormalisedInputValue(inSetupInput.GetSetupValue(SetupInput_v1.SetupInputOptions.suspensionStiffnessOption)) * (float) UISetupInputWidget.mNumberOfDiscreteSliderStepsDefault), 0, UISetupInputWidget.mNumberOfDiscreteSliderStepsDefault), 1000f);
    GameUtility.SetSliderAmountIfDifferent(this.gearRatioSlider, (float) Mathf.Clamp(Mathf.RoundToInt(this.GetNormalisedInputValue(inSetupInput.GetSetupValue(SetupInput_v1.SetupInputOptions.gearRatioOption)) * (float) UISetupInputWidget.mNumberOfDiscreteSliderStepsDefault), 0, UISetupInputWidget.mNumberOfDiscreteSliderStepsDefault), 1000f);
    this.RefreshWidgetDisplays();
    this.SetAllSetupValuesChanged(true);
    this.OnSetupChanged();
  }

  private void UpdateSetupInput()
  {
    switch (this.mSetupInput.series)
    {
      case Championship.Series.SingleSeaterSeries:
        this.mSetupInput.SetSetupValue(SetupInput_v1.SetupInputOptions.frontWingAngleOption, this.frontWingAngleSlider.value);
        this.mSetupInput.SetSetupValue(SetupInput_v1.SetupInputOptions.rearWingAngleOption, this.rearWingAngleSlider.value);
        break;
      case Championship.Series.GTSeries:
        this.mSetupInput.SetSetupValue(SetupInput_v1.SetupInputOptions.ballastDistributionOption, (float) (((double) UISetupInputWidget.mInputValueMax - (double) UISetupInputWidget.mInputValueMin) * ((double) this.ballastSlider.value / (double) UISetupInputWidget.mNumberOfDiscreteBallastSliderSteps)) + UISetupInputWidget.mInputValueMin);
        break;
    }
    this.mSetupInput.SetSetupValue(SetupInput_v1.SetupInputOptions.tyrePressureOption, this.mPressureValue);
    this.mSetupInput.SetSetupValue(SetupInput_v1.SetupInputOptions.tyreCamberOption, this.mCamberValue);
    this.mSetupInput.SetSetupValue(SetupInput_v1.SetupInputOptions.suspensionStiffnessOption, (float) (((double) UISetupInputWidget.mInputValueMax - (double) UISetupInputWidget.mInputValueMin) * ((double) this.suspensionStiffnessSlider.value / (double) UISetupInputWidget.mNumberOfDiscreteSliderStepsDefault)) + UISetupInputWidget.mInputValueMin);
    this.mSetupInput.SetSetupValue(SetupInput_v1.SetupInputOptions.gearRatioOption, (float) (((double) UISetupInputWidget.mInputValueMax - (double) UISetupInputWidget.mInputValueMin) * ((double) this.gearRatioSlider.value / (double) UISetupInputWidget.mNumberOfDiscreteSliderStepsDefault)) + UISetupInputWidget.mInputValueMin);
  }

  public void OnResetToDefault()
  {
    this.CentreSliderValue(this.frontWingAngleSlider);
    this.CentreSliderValue(this.rearWingAngleSlider);
    GameUtility.SetSliderAmountIfDifferent(this.ballastSlider, (float) Mathf.RoundToInt((float) (UISetupInputWidget.mNumberOfDiscreteBallastSliderSteps / 2)), 1000f);
    this.mPressureValue = UISetupInputWidget.mInputValueMin + (float) (((double) UISetupInputWidget.mInputValueMax - (double) UISetupInputWidget.mInputValueMin) / 2.0);
    this.mCamberValue = UISetupInputWidget.mInputValueMin + (float) (((double) UISetupInputWidget.mInputValueMax - (double) UISetupInputWidget.mInputValueMin) / 2.0);
    GameUtility.SetSliderAmountIfDifferent(this.gearRatioSlider, (float) Mathf.RoundToInt((float) (UISetupInputWidget.mNumberOfDiscreteSliderStepsDefault / 2)), 1000f);
    GameUtility.SetSliderAmountIfDifferent(this.suspensionStiffnessSlider, (float) Mathf.RoundToInt((float) (UISetupInputWidget.mNumberOfDiscreteSliderStepsDefault / 2)), 1000f);
    this.RefreshWidgetDisplays();
    this.SetAllSetupValuesChanged(true);
    this.OnSetupChanged();
  }

  private void CentreSliderValue(Slider inSlider)
  {
    GameUtility.SetSliderAmountIfDifferent(inSlider, Mathf.Clamp(inSlider.minValue + (float) (((double) inSlider.maxValue - (double) inSlider.minValue) / 2.0), UISetupInputWidget.mInputValueMin, UISetupInputWidget.mInputValueMax), 1000f);
  }

  private void RefreshWidgetDisplays()
  {
    this.HandleButtonPress(this.addCamberButton, this.minusCamberButton, this.mCamberValue, UISetupInputWidget.ButtonPressed.None, UISetupInputWidget.SetupType.TyreCamber);
    this.HandleButtonPress(this.addPressureButton, this.minusPressureButton, this.mPressureValue, UISetupInputWidget.ButtonPressed.None, UISetupInputWidget.SetupType.TyrePressure);
  }

  private void HandleButtonPress(Button inIncButton, Button inDecButton, float inControlledValue, UISetupInputWidget.ButtonPressed inButtonPressed, UISetupInputWidget.SetupType inSetupType)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    int num = Mathf.RoundToInt((float) UISetupInputWidget.mNumberOfButtonSteps * this.GetNormalisedInputValue(inControlledValue));
    switch (inButtonPressed)
    {
      case UISetupInputWidget.ButtonPressed.AddButton:
        ++num;
        break;
      case UISetupInputWidget.ButtonPressed.MinusButton:
        --num;
        break;
    }
    inIncButton.interactable = num < UISetupInputWidget.mNumberOfButtonSteps;
    inDecButton.interactable = num > 0;
    switch (inSetupType)
    {
      case UISetupInputWidget.SetupType.TyrePressure:
        GameUtility.SetImageFillAmountIfDifferent(this.tyrePressureFill, (float) num / (float) UISetupInputWidget.mNumberOfButtonSteps, 1f / 512f);
        this.mPressureValue = (float) num * (UISetupInputWidget.mInputValueMax - UISetupInputWidget.mInputValueMin) / (float) UISetupInputWidget.mNumberOfButtonSteps + UISetupInputWidget.mInputValueMin;
        break;
      case UISetupInputWidget.SetupType.TyreCamber:
        this.tyreToRotate.transform.localEulerAngles = new Vector3(0.0f, 0.0f, Mathf.Lerp(-4f, 0.0f, (float) num / (float) UISetupInputWidget.mNumberOfButtonSteps));
        this.mCamberValue = (float) num * (UISetupInputWidget.mInputValueMax - UISetupInputWidget.mInputValueMin) / (float) UISetupInputWidget.mNumberOfButtonSteps + UISetupInputWidget.mInputValueMin;
        break;
    }
    this.mSetupValueChanged[(int) inSetupType] = true;
    this.OnSetupChanged();
  }

  private void HandleSliderChange(UISetupInputWidget.SetupType inSetupType)
  {
    if (inSetupType == UISetupInputWidget.SetupType.GearRatio || inSetupType == UISetupInputWidget.SetupType.SuspensionStiffness || inSetupType == UISetupInputWidget.SetupType.Ballast)
      scSoundManager.Instance.PlaySquareSlider();
    else
      scSoundManager.Instance.PlayRoundSlider();
    this.mSetupValueChanged[(int) inSetupType] = true;
    this.OnSetupChanged();
  }

  private void SetAllSetupValuesChanged(bool changed)
  {
    for (int index = 0; index < this.mSetupValueChanged.Length; ++index)
      this.mSetupValueChanged[index] = changed;
  }

  private float GetNormalisedInputValue(float inValue)
  {
    return Mathf.Clamp01((float) (((double) inValue - (double) UISetupInputWidget.mInputValueMin) / ((double) UISetupInputWidget.mInputValueMax - (double) UISetupInputWidget.mInputValueMin)));
  }

  private float GetInputValueFromIntSlider(Slider inSlider, int inNumberSteps)
  {
    return (float) ((double) inSlider.value / (double) inNumberSteps * ((double) UISetupInputWidget.mInputValueMax - (double) UISetupInputWidget.mInputValueMin)) + UISetupInputWidget.mInputValueMin;
  }

  public override void RefreshTime()
  {
    this.SetTimeEstimate(this.mVehicle.setup.GetSetupTimeImpact(), this.mVehicle.setup.IsOnTheCriticalPath(SessionSetup.PitCrewSizeDependentSteps.Setup));
  }

  private enum ButtonPressed
  {
    AddButton,
    MinusButton,
    None,
  }

  private enum SetupType
  {
    TyrePressure,
    TyreCamber,
    FrontWingAngle,
    RearWingAngle,
    GearRatio,
    SuspensionStiffness,
    Ballast,
    MAX,
  }
}
