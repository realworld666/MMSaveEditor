// Decompiled with JetBrains decompiler
// Type: PitStopFuelWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PitStopFuelWidget : MonoBehaviour
{
  private bool mHasLeftPitlaneArea = true;
  public TextMeshProUGUI lapsLabel;
  public Slider fuelProjected;
  public Slider fuel;
  public GameObject mistakeFuelIcon;
  public GameObject complete;
  public GameObject mistake;
  public GameObject outcome;
  public UIPitOutcome changeOutcome;
  public CanvasGroup canvasGroup;
  public GameObject inProgressContainer;
  private RacingVehicle mVehicle;
  private SessionSetup mSetup;
  private SessionSetupChangeEntry mChange;
  private bool mActivated;

  public void Setup(RacingVehicle inVehicle)
  {
    if (inVehicle == null)
      return;
    this.mVehicle = inVehicle;
    this.UpdatePitlaneStatus();
    this.mSetup = this.mVehicle.setup;
    this.mChange = this.mSetup.changes.GetChangeFuel();
    this.mActivated = this.mSetup.IsRefueling() && this.mVehicle.driver.contract.GetTeam().championship.rules.isRefuelingOn;
    if (this.mActivated)
      this.mHasLeftPitlaneArea = false;
    if (!this.mActivated && !this.mHasLeftPitlaneArea)
      this.mActivated = true;
    GameUtility.SetActive(this.mistakeFuelIcon, false);
    GameUtility.SetActive(this.complete, false);
    GameUtility.SetActive(this.inProgressContainer, false);
    GameUtility.SetSliderAmountIfDifferent(this.fuel, 0.0f, 1000f);
    GameUtility.SetSliderAmountIfDifferent(this.fuelProjected, 0.0f, 1000f);
    this.ShowMistake(false);
    this.ShowOutcome(false);
    GameUtility.SetActive(this.gameObject, this.mActivated);
    if (!this.mActivated)
      return;
    this.SetDetails();
    this.SetFuelSliders();
    this.SetCanvasGroup();
  }

  private void SetDetails()
  {
    if (!this.mActivated)
      return;
    float num1 = Mathf.Max(this.mVehicle.timer.currentPitstop.stopTime - this.mChange.pitStopStart, 0.0f);
    float num2 = this.mChange.mistakeStart - this.mChange.pitStopStart;
    float num3 = (double) num1 <= 0.0 || (double) num2 <= 0.0 ? 0.0f : Mathf.Clamp01(num1 / num2);
    float num4 = (float) (this.mSetup.targetFuelLaps - this.mSetup.startFuelLaps);
    float num5 = (float) Mathf.RoundToInt(Mathf.Clamp((float) this.mVehicle.performance.fuel.GetFuelLapsRemaining() + num3 * num4, 0.0f, (float) this.mVehicle.performance.fuel.fuelTankLapCountCapacity));
    StringVariableParser.intValue1 = (double) num3 < 1.0 ? (int) num5 : this.mSetup.targetFuelLaps;
    StringVariableParser.intValue2 = this.mSetup.targetFuelLaps;
    this.lapsLabel.text = Localisation.LocaliseID("PSG_10010842", (GameObject) null);
  }

  private void UpdatePitlaneStatus()
  {
    if (this.mHasLeftPitlaneArea || this.mVehicle == null || this.mVehicle.pathController.IsOnPitlanePath())
      return;
    this.mHasLeftPitlaneArea = true;
  }

  public void Update()
  {
    float stopTime = this.mVehicle.timer.currentPitstop.stopTime;
    this.UpdatePitlaneStatus();
    if (!this.mActivated || (double) stopTime <= 0.0)
      return;
    this.SetFuelSliders();
    bool inIsActive = (double) stopTime >= (double) this.mChange.mistakeEnd;
    bool flag1 = (double) stopTime >= (double) this.mChange.pitStopStart;
    bool flag2 = (double) stopTime >= (double) this.mChange.mistakeStart && (double) stopTime < (double) this.mChange.mistakeEnd;
    GameUtility.SetActive(this.inProgressContainer, flag1 && !inIsActive);
    GameUtility.SetActive(this.mistakeFuelIcon, flag2);
    GameUtility.SetActive(this.complete, inIsActive);
    this.SetDetails();
    this.ShowMistake(flag2);
  }

  private void SetFuelSliders()
  {
    float stopTime = this.mVehicle.timer.currentPitstop.stopTime;
    float lapCountCapacity = (float) this.mVehicle.performance.fuel.fuelTankLapCountCapacity;
    float inValue1;
    float inValue2;
    if (this.mActivated && (double) stopTime > 0.0)
    {
      float num1 = Mathf.Max(stopTime - this.mChange.pitStopStart, 0.0f);
      float num2 = this.mChange.mistakeStart - this.mChange.pitStopStart;
      float num3 = Mathf.Clamp((float) this.mVehicle.performance.fuel.GetFuelLapsRemaining() + ((double) num1 <= 0.0 || (double) num2 <= 0.0 ? 0.0f : Mathf.Clamp01(num1 / num2)) * (float) (this.mSetup.targetFuelLaps - this.mSetup.startFuelLaps), 0.0f, lapCountCapacity);
      inValue1 = (double) lapCountCapacity <= 0.0 ? 0.0f : Mathf.Clamp01(num3 / lapCountCapacity);
      inValue2 = Mathf.Clamp01((float) this.mSetup.targetFuelLaps / lapCountCapacity);
    }
    else
    {
      inValue1 = (double) lapCountCapacity <= 0.0 ? 0.0f : Mathf.Clamp01((float) this.mVehicle.performance.fuel.GetFuelLapsRemaining() / lapCountCapacity);
      inValue2 = Mathf.Clamp01((float) this.mSetup.targetFuelLaps / lapCountCapacity);
    }
    if ((double) this.fuelProjected.value != (double) inValue2)
      GameUtility.SetSliderAmountIfDifferent(this.fuelProjected, inValue2, 1000f);
    if ((double) this.fuel.value == (double) inValue1)
      return;
    GameUtility.SetSliderAmountIfDifferent(this.fuel, inValue1, 1000f);
  }

  private void SetCanvasGroup()
  {
    this.canvasGroup.alpha = !this.mActivated ? 0.35f : 1f;
    this.canvasGroup.interactable = this.mActivated;
  }

  private void ShowMistake(bool inValue)
  {
    GameUtility.SetActive(this.mistake, inValue);
  }

  public void ShowOutcome(bool inValue)
  {
    GameUtility.SetActive(this.outcome, this.mActivated && inValue);
    if (!this.outcome.activeSelf)
      return;
    this.changeOutcome.SetOutcome(this.mVehicle.setup.changes.GetOutcome(SessionSetupChangeEntry.Target.Fuel));
  }
}
