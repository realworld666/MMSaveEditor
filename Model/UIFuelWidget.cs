// Decompiled with JetBrains decompiler
// Type: UIFuelWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIFuelWidget : UIBaseTimerWidget
{
  public Button subtractFuelButton;
  public Button addFuelButton;
  public TextMeshProUGUI currentFuelLapsLabel;
  public Slider currentFuelLevelSlider;
  public CanvasGroup fuelPreviewCanvasGroup;
  public TextMeshProUGUI fuelSubHeader;
  public TextMeshProUGUI targetFuelLapsLabel;
  public Slider targetFuelLevelSlider;
  public TextMeshProUGUI lapsRemainingText;
  public TextMeshProUGUI addedFuelLabel;
  public Slider addedFuelSlider;
  public TextMeshProUGUI secondsPerLapPerFuelText;
  private RacingVehicle mVehicle;
  private int mFuelLapsToAdd;
  private int mMaxAdditionalFuelLaps;
  private int mTankCapacity;
  private bool mIsChanged;

  public bool isChanged
  {
    get
    {
      return this.mIsChanged;
    }
  }

  private void Awake()
  {
    this.subtractFuelButton.onClick.AddListener(new UnityAction(this.OnSubtractFuelButton));
    this.addFuelButton.onClick.AddListener(new UnityAction(this.OnAddFuelButton));
  }

  public void SetVehicle(RacingVehicle inVehicle, PitScreen.Mode inMode)
  {
    this.mVehicle = inVehicle;
    Fuel fuel = this.mVehicle.performance.fuel;
    GameUtility.SetSliderAmountIfDifferent(this.currentFuelLevelSlider, fuel.GetNormalisedFuelLevel(), 1000f);
    GameUtility.SetSliderAmountIfDifferent(this.targetFuelLevelSlider, fuel.GetNormalisedFuelLevel(), 1000f);
    UIFuelWidget.UpdateTextFieldNLaps(this.currentFuelLapsLabel, fuel.GetFuelLapsRemainingDecimal(), true);
    this.mTankCapacity = fuel.fuelTankLapCountCapacity;
    this.mMaxAdditionalFuelLaps = Mathf.RoundToInt((float) this.mTankCapacity - Mathf.Max(0.0f, Game.instance.sessionManager.isSessionActive ? fuel.GetFuelLapsRemainingAtPit() : (float) fuel.GetFuelLapsRemaining()));
    this.mFuelLapsToAdd = 0;
    StringVariableParser.intValue1 = Game.instance.sessionManager.GetLapsRemaining();
    using (GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe())
    {
      StringBuilder stringBuilder = builderSafe.stringBuilder;
      switch (inMode)
      {
        case PitScreen.Mode.Pitting:
          if (Game.instance.sessionManager.GetLapsRemaining() != 1)
          {
            stringBuilder.Append(Localisation.LocaliseID("PSG_10011012", (GameObject) null));
            break;
          }
          stringBuilder.Append(Localisation.LocaliseID("PSG_10011012", (GameObject) null));
          break;
        default:
          stringBuilder.Append(Localisation.LocaliseID("PSG_10010182", (GameObject) null));
          stringBuilder.Append(" ");
          if (Game.instance.sessionManager.GetLapsRemaining() != 1)
          {
            stringBuilder.Append(Localisation.LocaliseID("PSG_10010646", (GameObject) null));
            break;
          }
          stringBuilder.Append(Localisation.LocaliseID("PSG_10010645", (GameObject) null));
          break;
      }
      this.lapsRemainingText.text = stringBuilder.ToString();
    }
    this.secondsPerLapPerFuelText.text = GameUtility.FormatSecondsToString(fuel.scaledTimeCostPerLap, 3);
    this.UpdateData();
  }

  public void OnSubtractFuelButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mFuelLapsToAdd = this.mFuelLapsToAdd - 1;
    this.UpdateData();
  }

  public void OnAddFuelButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mFuelLapsToAdd = Mathf.Min(this.mFuelLapsToAdd + 1, this.mMaxAdditionalFuelLaps);
    this.UpdateData();
  }

  private void UpdateData()
  {
    bool flag = !Game.instance.sessionManager.isSessionActive;
    Fuel fuel = this.mVehicle.performance.fuel;
    float num1 = Mathf.Max(0.0f, !flag ? fuel.GetFuelLapsRemainingAtPit() : (float) fuel.GetFuelLapsRemaining());
    this.subtractFuelButton.interactable = (double) num1 + (double) this.mFuelLapsToAdd > 1.0;
    this.addFuelButton.interactable = this.mFuelLapsToAdd < this.mMaxAdditionalFuelLaps;
    this.mVehicle.setup.SetTargetFuelLevel(Mathf.CeilToInt(num1 + (float) this.mFuelLapsToAdd));
    this.RefreshTime();
    float num2 = Mathf.Clamp(num1 + (float) this.mFuelLapsToAdd, 0.0f, (float) this.mVehicle.performance.fuel.fuelTankLapCountCapacity);
    UIFuelWidget.UpdateTextFieldNLaps(this.targetFuelLapsLabel, num2, true);
    UIFuelWidget.UpdateTextFieldNLaps(this.addedFuelLabel, (float) this.mFuelLapsToAdd, false);
    this.mIsChanged = this.mFuelLapsToAdd != 0;
    if (this.mIsChanged || flag)
    {
      this.fuelPreviewCanvasGroup.alpha = 1f;
      this.fuelSubHeader.text = Localisation.LocaliseID("PSG_10010650", (GameObject) null);
    }
    else
    {
      this.fuelPreviewCanvasGroup.alpha = 0.8f;
      this.fuelSubHeader.text = Localisation.LocaliseID("PSG_10011135", (GameObject) null);
    }
    GameUtility.SetSliderAmountIfDifferent(this.addedFuelSlider, Mathf.Clamp01(num2 / (float) fuel.fuelTankLapCountCapacity), 1000f);
    GameUtility.SetSliderAmountIfDifferent(this.targetFuelLevelSlider, Mathf.Clamp01(Mathf.Min(fuel.GetNormalisedFuelLevel(), Mathf.Clamp01((num1 + (float) this.mFuelLapsToAdd) / (float) fuel.fuelTankLapCountCapacity))), 1000f);
    UIManager.instance.GetScreen<PitScreen>().OnFuelSelectionChanged(num2, this.mIsChanged);
  }

  public static void UpdateTextFieldNLaps(TextMeshProUGUI inTextField, float inLaps, bool inDecimals = true)
  {
    if (inDecimals)
      inTextField.text = GameUtility.FormatForLaps(inLaps);
    else
      inTextField.text = GameUtility.FormatForLaps(Mathf.RoundToInt(inLaps));
  }

  public override void RefreshTime()
  {
    this.SetTimeEstimate(this.mVehicle.setup.GetFuelTimeImpact(), this.mVehicle.setup.IsOnTheCriticalPath(SessionSetup.PitCrewSizeDependentSteps.Fuel));
  }
}
