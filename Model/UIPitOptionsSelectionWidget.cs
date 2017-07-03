// Decompiled with JetBrains decompiler
// Type: UIPitOptionsSelectionWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class UIPitOptionsSelectionWidget : MonoBehaviour
{
  private PitScreen.Mode mMode = PitScreen.Mode.Pitting;
  private List<UIPitStopStep> mCurrentSteps = new List<UIPitStopStep>();
  private List<UIPitStopStep> mAllSteps = new List<UIPitStopStep>();
  public CanvasGroup canvasGroup;
  public UIPitStopStep stintType;
  public UIPitStopStep pitStrategy;
  public UIPitStopStep fuelOptions;
  public UIPitStopStep tyreChoice;
  public UIPitStopStep partCondition;
  public UIPitStopStep carSetup;
  public UIPitStopStep batteryCharge;
  public UIPracticeStintTypeWidget practiceStintTypeWidget;
  public UIPitStrategyWidget pitStrategyWidget;
  public UIFuelWidget fuelWidget;
  public UITyreSelectionWidget tyreSelectionWidget;
  public UIPitConditionWidget pitConditionWidget;
  public UISetupInputWidget setupInputWidget;
  public UIPitBatteryChargeWidget batteryChargeWidget;
  private RacingVehicle mVehicle;
  private bool mIsRefuelingEnabled;
  private bool mIsERSActive;

  public void OnStart()
  {
    this.mIsRefuelingEnabled = Game.instance.player.team.championship.rules.isRefuelingOn;
    this.mIsERSActive = Game.instance.player.team.championship.rules.isEnergySystemActive;
    this.mAllSteps.Clear();
    this.carSetup.OnStart(this.setupInputWidget.gameObject);
    this.mAllSteps.Add(this.carSetup);
    this.stintType.OnStart(this.practiceStintTypeWidget.gameObject);
    this.mAllSteps.Add(this.stintType);
    this.tyreChoice.OnStart(this.tyreSelectionWidget.gameObject);
    this.mAllSteps.Add(this.tyreChoice);
    this.fuelOptions.OnStart(this.fuelWidget.gameObject);
    this.mAllSteps.Add(this.fuelOptions);
    this.partCondition.OnStart(this.pitConditionWidget.gameObject);
    this.mAllSteps.Add(this.partCondition);
    this.batteryCharge.OnStart(this.batteryChargeWidget.gameObject);
    this.mAllSteps.Add(this.batteryCharge);
    this.pitStrategy.OnStart(this.pitStrategyWidget.gameObject);
    this.mAllSteps.Add(this.pitStrategy);
  }

  public void Setup(RacingVehicle inVehicle, PitScreen.Mode inMode)
  {
    this.mVehicle = inVehicle;
    this.mMode = inMode;
    this.SetActiveOptionsObjects(inMode);
    this.SetupActiveWidgets();
    this.canvasGroup.blocksRaycasts = true;
  }

  public void GoToNextStep()
  {
    int count = this.mCurrentSteps.Count;
    for (int index = 0; index < count; ++index)
    {
      UIPitStopStep mCurrentStep = this.mCurrentSteps[index];
      if (!mCurrentStep.isComplete)
      {
        this.SetActiveStep(mCurrentStep);
        return;
      }
    }
    this.SetActiveStep(this.mCurrentSteps[this.mCurrentSteps.Count - 1]);
  }

  private void ResetAllSteps(UIPitStopStep inDefaultStep)
  {
    inDefaultStep.toggle.isOn = true;
    for (int index = 0; index < this.mCurrentSteps.Count; ++index)
      this.mCurrentSteps[index].SetComplete(false);
    this.SetActiveStep(inDefaultStep);
  }

  private void SetActiveStep(UIPitStopStep inStep)
  {
    inStep.toggle.isOn = true;
    inStep.OnToggle();
  }

  public UIPitStopStep GetIncompleteStep()
  {
    int count = this.mCurrentSteps.Count;
    for (int index = 0; index < count; ++index)
    {
      UIPitStopStep mCurrentStep = this.mCurrentSteps[index];
      if (!mCurrentStep.isComplete)
        return mCurrentStep;
    }
    return (UIPitStopStep) null;
  }

  public bool IsComplete()
  {
    bool flag = true;
    int count = this.mCurrentSteps.Count;
    for (int index = 0; index < count; ++index)
    {
      if (!this.mCurrentSteps[index].isComplete)
        return false;
    }
    return flag;
  }

  public void NotifyStepComplete()
  {
    UIManager.instance.GetScreen<PitScreen>().UpdateContinuebuttonText();
  }

  private void SetActiveOptionsObjects(PitScreen.Mode inMode)
  {
    switch (inMode)
    {
      case PitScreen.Mode.PreSession:
        GameUtility.SetActive(this.carSetup.gameObject, true);
        GameUtility.SetActive(this.fuelOptions.gameObject, this.mIsRefuelingEnabled && Game.instance.sessionManager.sessionType == SessionDetails.SessionType.Race);
        GameUtility.SetActive(this.partCondition.gameObject, false);
        GameUtility.SetActive(this.pitStrategy.gameObject, false);
        GameUtility.SetActive(this.stintType.gameObject, false);
        GameUtility.SetActive(this.tyreChoice.gameObject, true);
        GameUtility.SetActive(this.batteryCharge.gameObject, false);
        break;
      case PitScreen.Mode.SendOut:
        GameUtility.SetActive(this.carSetup.gameObject, true);
        GameUtility.SetActive(this.fuelOptions.gameObject, false);
        GameUtility.SetActive(this.partCondition.gameObject, false);
        GameUtility.SetActive(this.pitStrategy.gameObject, false);
        GameUtility.SetActive(this.stintType.gameObject, Game.instance.sessionManager.sessionType == SessionDetails.SessionType.Practice);
        GameUtility.SetActive(this.tyreChoice.gameObject, true);
        GameUtility.SetActive(this.batteryCharge.gameObject, false);
        break;
      case PitScreen.Mode.Pitting:
        GameUtility.SetActive(this.carSetup.gameObject, false);
        GameUtility.SetActive(this.fuelOptions.gameObject, this.mIsRefuelingEnabled);
        GameUtility.SetActive(this.partCondition.gameObject, true);
        GameUtility.SetActive(this.pitStrategy.gameObject, true);
        GameUtility.SetActive(this.stintType.gameObject, false);
        GameUtility.SetActive(this.tyreChoice.gameObject, true);
        GameUtility.SetActive(this.batteryCharge.gameObject, this.mIsERSActive && this.mVehicle.ERSController.state != ERSController.ERSState.Broken);
        break;
    }
    bool inShow = inMode != PitScreen.Mode.PreSession;
    this.practiceStintTypeWidget.ShowTimeWidget(inShow);
    this.fuelWidget.ShowTimeWidget(inShow);
    this.setupInputWidget.ShowTimeWidget(inShow);
    this.tyreSelectionWidget.ShowTimeWidget(inShow);
    this.pitStrategyWidget.ShowTimeWidget(inShow);
    this.pitConditionWidget.ShowTimeWidget(inShow);
    this.batteryChargeWidget.ShowTimeWidget(inShow);
    this.RefreshActiveSteps();
  }

  private void SetupActiveWidgets()
  {
    if (this.fuelOptions.gameObject.activeSelf)
      this.fuelWidget.SetVehicle(this.mVehicle, this.mMode);
    if (this.carSetup.gameObject.activeSelf)
      this.setupInputWidget.SetVehicle(this.mVehicle);
    if (this.tyreChoice.gameObject.activeSelf)
      this.tyreSelectionWidget.SetVehicle(this.mVehicle, this.mMode);
    if (this.partCondition.gameObject.activeSelf)
      this.pitConditionWidget.SetVehicle(this.mVehicle);
    if (this.pitStrategy.gameObject.activeSelf)
      this.pitStrategyWidget.SetVehicle(this.mVehicle);
    if (this.stintType.gameObject.activeSelf)
      this.practiceStintTypeWidget.SetVehicle(this.mVehicle);
    if (!this.batteryCharge.gameObject.activeSelf)
      return;
    this.batteryChargeWidget.SetVehicle(this.mVehicle);
  }

  private void RefreshActiveSteps()
  {
    this.mCurrentSteps.Clear();
    for (int index = 0; index < this.mAllSteps.Count; ++index)
      this.TryAddStep(this.mAllSteps[index]);
    this.ResetAllSteps(this.mCurrentSteps[0]);
  }

  private void TryAddStep(UIPitStopStep inStep)
  {
    if (!inStep.gameObject.activeSelf)
      return;
    this.mCurrentSteps.Add(inStep);
    inStep.SetComplete(false);
  }
}
