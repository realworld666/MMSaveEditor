// Decompiled with JetBrains decompiler
// Type: PitStopRepairWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PitStopRepairWidget : MonoBehaviour
{
  public Color defaultColor = Color.white;
  public Color completeColor = Color.white;
  private List<SessionSetupRepairPart> parts = new List<SessionSetupRepairPart>();
  private bool mHasLeftPitlaneArea = true;
  private int mRepairedParts = -1;
  public TextMeshProUGUI partsRepaired;
  public TextMeshProUGUI partName;
  public GameObject mistake;
  public GameObject outcome;
  public UIPitOutcome changeOutcome;
  public GameObject mistakeIcon;
  public GameObject complete;
  public Slider repairSlider;
  public Transform iconsParent;
  private Image[] repairIcons;
  public CanvasGroup canvasGroup;
  public GameObject inProgressContainer;
  private SessionSetupRepairPart lastPart;
  private SessionSetupRepairPart currentPart;
  private SessionSetupChangeEntry currentChange;
  private RacingVehicle mVehicle;
  private Car mCar;
  private Image mCurrentPartIcon;
  private bool mActivated;
  private bool mOutcomeError;
  private bool mComplete;
  private int mCount;

  private void Awake()
  {
    this.repairIcons = this.iconsParent.GetComponentsInChildren<Image>();
  }

  public void Setup(RacingVehicle inVehicle)
  {
    if (inVehicle == null)
      return;
    this.mVehicle = inVehicle;
    this.UpdatePitlaneStatus();
    this.mCar = this.mVehicle.car;
    this.mActivated = this.mCar.GetNumPartsToBeRepaired() > 0;
    this.mComplete = this.mVehicle.setup.state == SessionSetup.State.Setup;
    if (this.mActivated)
      this.mHasLeftPitlaneArea = false;
    if (!this.mActivated && !this.mHasLeftPitlaneArea)
      this.mActivated = true;
    GameUtility.SetActive(this.complete, false);
    GameUtility.SetActive(this.mistakeIcon, false);
    GameUtility.SetActive(this.inProgressContainer, false);
    GameUtility.SetSliderAmountIfDifferent(this.repairSlider, 0.0f, 1000f);
    this.ShowMistake(false);
    this.ShowOutcome(false);
    GameUtility.SetActive(this.gameObject, this.mActivated);
    if (!this.mActivated)
      return;
    this.mRepairedParts = -1;
    this.SetLabel();
    this.LoadParts();
    this.ResetPartColors();
    this.SetCanvasGroup();
    this.SetNextPart();
  }

  private void LoadParts()
  {
    this.parts.Clear();
    if (!this.mActivated)
      return;
    this.parts = new List<SessionSetupRepairPart>((IEnumerable<SessionSetupRepairPart>) this.mVehicle.setup.repairParts);
    this.mCount = this.parts.Count;
    this.partsRepaired.text = "0/" + this.mCount.ToString();
  }

  private void UpdatePitlaneStatus()
  {
    if (this.mHasLeftPitlaneArea || this.mVehicle == null || this.mVehicle.pathController.IsOnPitlanePath())
      return;
    this.mHasLeftPitlaneArea = true;
  }

  private void Update()
  {
    float stopTime = this.mVehicle.timer.currentPitstop.stopTime;
    this.UpdatePitlaneStatus();
    if (!this.mActivated || (double) stopTime <= 0.0)
      return;
    this.mOutcomeError = false;
    float num1 = 0.0f;
    int num2 = 0;
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    for (int index = 0; index < this.mCount; ++index)
    {
      SessionSetupRepairPart part = this.parts[index];
      SessionSetupChangeEntry changeCarPart = this.mVehicle.setup.changes.GetChangeCarPart(part.carPart.GetPartType());
      bool flag4 = (double) stopTime >= (double) changeCarPart.pitStopStart;
      flag1 = (double) stopTime >= (double) changeCarPart.mistakeEnd;
      flag2 = (double) stopTime >= (double) changeCarPart.mistakeStart && (double) stopTime < (double) changeCarPart.mistakeEnd;
      if (changeCarPart.isSet && flag2 && !this.mOutcomeError)
        this.mOutcomeError = true;
      if (flag1)
        ++num2;
      if (flag4 && !flag1)
      {
        this.currentPart = part;
        this.currentChange = changeCarPart;
      }
      float num3 = Mathf.Max(stopTime - changeCarPart.pitStopStart, 0.0f);
      num1 += (double) num3 <= 0.0 ? 0.0f : Mathf.Clamp01(num3 / (changeCarPart.mistakeStart - changeCarPart.pitStopStart));
      if (flag4)
        flag3 = true;
    }
    GameUtility.SetSliderAmountIfDifferent(this.repairSlider, num1 / (float) this.mCount, 1000f);
    this.mComplete = num2 == this.mCount;
    GameUtility.SetActive(this.complete, this.mComplete);
    GameUtility.SetActive(this.mistakeIcon, this.currentPart != null && this.currentChange.isSet && flag2);
    if (this.mRepairedParts != num2)
    {
      this.mRepairedParts = num2;
      this.partsRepaired.text = num2.ToString() + "/" + this.mCount.ToString();
    }
    this.SetCurrentPart();
    this.SetPartColor();
    this.ShowMistake(this.mOutcomeError);
    GameUtility.SetActive(this.inProgressContainer, flag3 && !flag1);
  }

  private void SetNextPart()
  {
    if (this.parts.Count < 1)
      return;
    this.SetPartIcon(this.parts[0].carPart.GetPartType());
  }

  private void SetCurrentPart()
  {
    if (this.currentPart == null || this.lastPart == this.currentPart)
      return;
    this.SetPartIcon(this.currentPart.carPart.GetPartType());
    this.lastPart = this.currentPart;
  }

  private void SetPartIcon(CarPart.PartType inCarPartType)
  {
    if (this.repairIcons == null)
      this.Awake();
    CarPart.SetIcon(this.iconsParent, inCarPartType);
    for (int index = 0; index < this.repairIcons.Length; ++index)
    {
      if (this.repairIcons[index].gameObject.activeSelf)
        this.mCurrentPartIcon = this.repairIcons[index];
    }
    this.partName.text = Localisation.LocaliseEnum((Enum) inCarPartType);
  }

  private void SetPartColor()
  {
    if (!((UnityEngine.Object) this.mCurrentPartIcon != (UnityEngine.Object) null))
      return;
    this.mCurrentPartIcon.color = !this.mComplete ? this.defaultColor : this.completeColor;
  }

  private void ResetPartColors()
  {
    if (this.repairIcons == null)
      this.Awake();
    for (int index = 0; index < this.repairIcons.Length; ++index)
      this.repairIcons[index].color = this.defaultColor;
  }

  private void SetLabel()
  {
    if (this.mActivated)
      return;
    this.partsRepaired.text = "-";
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
    this.changeOutcome.SetOutcome(this.mVehicle.setup.changes.GetOutcome(SessionSetupChangeEntry.Target.PartsRepair));
  }

  private void SetCanvasGroup()
  {
    this.canvasGroup.alpha = !this.mActivated ? 0.35f : 1f;
    this.canvasGroup.interactable = this.mActivated;
  }
}
