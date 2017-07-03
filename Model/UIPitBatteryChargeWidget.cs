// Decompiled with JetBrains decompiler
// Type: UIPitBatteryChargeWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPitBatteryChargeWidget : UIBaseTimerWidget
{
  public TextMeshProUGUI rechargeAmountLabel;
  public TextMeshProUGUI currentRiskLabel;
  public Button removeChargeButton;
  public Button addChargeButton;
  private RacingVehicle mVehicle;
  private int mNumberOfSteps;

  private void Awake()
  {
    this.removeChargeButton.onClick.AddListener(new UnityAction(this.RemoveCharge));
    this.addChargeButton.onClick.AddListener(new UnityAction(this.AddCharge));
  }

  private void OnEnable()
  {
    this.SetTimeEstimate(this.mVehicle.setup.GetBatteryTimeImpact(), true);
    UIManager.instance.GetScreen<PitScreen>().OnBatteryChargeChanged(this.mNumberOfSteps != 0);
    this.RefreshRechargeLabel();
    this.UpdateButtonsInteractability();
  }

  private void RemoveCharge()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    --this.mNumberOfSteps;
    this.RefreshRechargeLabel();
    this.UpdateData();
    this.UpdateButtonsInteractability();
  }

  private void AddCharge()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    ++this.mNumberOfSteps;
    this.RefreshRechargeLabel();
    this.UpdateData();
    this.UpdateButtonsInteractability();
  }

  private void UpdateButtonsInteractability()
  {
    this.removeChargeButton.interactable = this.mNumberOfSteps > 0;
    this.addChargeButton.interactable = this.mNumberOfSteps < 4;
  }

  private void UpdateData()
  {
    this.mVehicle.setup.SetTargetBatteryCharge(this.mNumberOfSteps);
    this.RefreshTime();
    UIManager.instance.GetScreen<PitScreen>().OnBatteryChargeChanged(this.mNumberOfSteps != 0);
  }

  public override void RefreshTime()
  {
    this.SetTimeEstimate(this.mVehicle.setup.GetBatteryTimeImpact(), true);
  }

  private void RefreshRechargeLabel()
  {
    this.rechargeAmountLabel.text = Mathf.RoundToInt((float) this.mNumberOfSteps * 0.25f * 100f).ToString() + "%";
    switch (this.mNumberOfSteps)
    {
      case 0:
        this.currentRiskLabel.text = Localisation.LocaliseID("PSG_10005815", (GameObject) null);
        this.currentRiskLabel.color = UIConstants.riskNone;
        break;
      case 1:
        this.currentRiskLabel.text = Localisation.LocaliseID("PSG_10010920", (GameObject) null);
        this.currentRiskLabel.color = UIConstants.riskLow;
        break;
      case 2:
        this.currentRiskLabel.text = Localisation.LocaliseID("PSG_10010921", (GameObject) null);
        this.currentRiskLabel.color = UIConstants.riskMedium;
        break;
      case 3:
        this.currentRiskLabel.text = Localisation.LocaliseID("PSG_10010922", (GameObject) null);
        this.currentRiskLabel.color = UIConstants.riskHigh;
        break;
      case 4:
        this.currentRiskLabel.text = Localisation.LocaliseID("PSG_10010923", (GameObject) null);
        this.currentRiskLabel.color = UIConstants.riskHigh;
        break;
    }
  }

  public void SetVehicle(RacingVehicle inVehicle)
  {
    this.mVehicle = inVehicle;
    this.mNumberOfSteps = 0;
    this.UpdateData();
  }
}
