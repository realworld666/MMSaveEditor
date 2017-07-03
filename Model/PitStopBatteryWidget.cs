// Decompiled with JetBrains decompiler
// Type: PitStopBatteryWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PitStopBatteryWidget : MonoBehaviour
{
  private bool mHasLeftPitlaneArea = true;
  public TextMeshProUGUI chargeLabel;
  public GameObject complete;
  public GameObject mistake;
  public GameObject outcome;
  public UIPitOutcome changeOutcome;
  public CanvasGroup canvasGroup;
  public Image fillAmount;
  public GameObject inProgressContainer;
  private RacingVehicle mVehicle;
  private SessionSetup mSetup;
  private SessionSetupChangeEntry mChange;
  private float mNormalizedTime;
  private bool mActivated;
  private bool mIsDone;

  public float resultingCharge
  {
    get
    {
      return Mathf.Clamp01(this.mVehicle.ERSController.normalizedCharge + this.mSetup.targetBatteryCharge);
    }
  }

  public void Setup(RacingVehicle inVehicle)
  {
    if (inVehicle == null)
      return;
    this.mVehicle = inVehicle;
    this.UpdatePitlaneStatus();
    this.mSetup = this.mVehicle.setup;
    this.mChange = this.mSetup.changes.GetChangeBatteryRecharge();
    GameUtility.SetActive(this.complete, false);
    this.mActivated = this.mSetup.IsRecharging() && this.mVehicle.driver.contract.GetTeam().championship.rules.isEnergySystemActive;
    if (this.mActivated)
      this.mHasLeftPitlaneArea = false;
    if (!this.mActivated && !this.mHasLeftPitlaneArea)
      this.mActivated = true;
    this.ShowMistake(false);
    this.ShowOutcome(false, SessionSetupChange.Outcome.None);
    GameUtility.SetActive(this.gameObject, this.mActivated);
    GameUtility.SetActive(this.inProgressContainer, false);
    if (!this.mActivated)
      return;
    float stopTime = this.mVehicle.timer.currentPitstop.stopTime;
    if (!this.mIsDone && (double) stopTime >= (double) this.mChange.pitStopStart || (double) stopTime <= 0.0)
    {
      this.fillAmount.fillAmount = this.mVehicle.ERSController.normalizedCharge;
      this.chargeLabel.text = Mathf.RoundToInt(this.mVehicle.ERSController.normalizedCharge * 100f).ToString() + "%";
    }
    this.SetCanvasGroup();
  }

  private void SetDetails()
  {
    if (!this.mActivated)
      return;
    this.chargeLabel.text = Mathf.RoundToInt(Mathf.Lerp(this.mVehicle.ERSController.normalizedCharge, this.resultingCharge, this.mNormalizedTime) * 100f).ToString() + "%";
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
    bool flag = (double) stopTime >= (double) this.mChange.pitStopStart;
    GameUtility.SetActive(this.inProgressContainer, flag && !this.mIsDone);
    if (flag && !this.mIsDone)
    {
      this.mNormalizedTime = (float) (((double) stopTime - (double) this.mChange.pitStopStart) / ((double) this.mChange.mistakeEnd - (double) this.mChange.pitStopStart));
      this.fillAmount.fillAmount = Mathf.Lerp(this.mVehicle.ERSController.normalizedCharge, this.resultingCharge, this.mNormalizedTime);
      this.SetDetails();
    }
    if (this.mIsDone && flag)
    {
      GameUtility.SetActive(this.complete, this.mIsDone);
      if (this.mVehicle.setup.isBatteryGoingToBreak || this.mVehicle.ERSController.state == ERSController.ERSState.Broken)
      {
        this.ShowMistake(true);
      }
      else
      {
        this.ShowMistake(false);
        this.ShowOutcome(true, SessionSetupChange.Outcome.Good);
      }
    }
    this.mIsDone = (double) stopTime >= (double) this.mChange.mistakeEnd;
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

  public void ShowOutcome(bool inValue, SessionSetupChange.Outcome inOutcome)
  {
    GameUtility.SetActive(this.outcome, this.mActivated && inValue);
    if (!this.outcome.activeSelf)
      return;
    this.changeOutcome.SetOutcome(inOutcome);
  }
}
