// Decompiled with JetBrains decompiler
// Type: UICompareStaffContractWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICompareStaffContractWidget : MonoBehaviour
{
  public TextMeshProUGUI wagePerMonth;
  public TextMeshProUGUI expires;
  public TextMeshProUGUI breakContract;
  public TextMeshProUGUI marketabilityLabel;
  public TextMeshProUGUI contractStatus;
  public TextMeshProUGUI qualifyingTarget;
  public TextMeshProUGUI qualifyingBonus;
  public TextMeshProUGUI raceTarget;
  public TextMeshProUGUI raceBonus;
  public Slider marketability;
  public GameObject marketabilityContainer;
  public GameObject contractStatusContainer;
  private Person mPerson;

  public void Setup(Person inPerson)
  {
    this.mPerson = inPerson;
    this.SetDetails();
    this.SetMarketability();
    this.SetContractStatus();
  }

  private void SetDetails()
  {
    if (!this.mPerson.IsFreeAgent())
    {
      this.wagePerMonth.text = GameUtility.GetCurrencyString(this.mPerson.contract.perRaceCost, 0);
      int monthsRemaining = this.mPerson.contract.GetMonthsRemaining();
      StringVariableParser.intValue1 = monthsRemaining;
      this.expires.text = Localisation.LocaliseID(monthsRemaining != 1 ? "PSG_10010608" : "PSG_10010609", (GameObject) null);
      this.breakContract.text = GameUtility.GetCurrencyString((long) this.mPerson.contract.GetContractTerminationCost(), 0);
    }
    else
    {
      this.wagePerMonth.text = "-";
      this.expires.text = "-";
      this.breakContract.text = "-";
    }
    if (!this.mPerson.IsFreeAgent() && this.mPerson.contract.hasQualifyingBonus)
    {
      this.qualifyingTarget.text = GameUtility.FormatForPositionOrAbove(this.mPerson.contract.qualifyingBonusTargetPosition, (string) null);
      this.qualifyingBonus.text = GameUtility.GetCurrencyString((long) -this.mPerson.contract.qualifyingBonus, 0);
      this.qualifyingBonus.color = GameUtility.GetCurrencyColor(-this.mPerson.contract.qualifyingBonus);
    }
    else
    {
      this.qualifyingTarget.text = "-";
      this.qualifyingBonus.text = "-";
      this.qualifyingBonus.color = Color.white;
    }
    if (!this.mPerson.IsFreeAgent() && this.mPerson.contract.hasRaceBonus)
    {
      this.raceTarget.text = GameUtility.FormatForPositionOrAbove(this.mPerson.contract.raceBonusTargetPosition, (string) null);
      this.raceBonus.text = GameUtility.GetCurrencyString((long) -this.mPerson.contract.raceBonus, 0);
      this.raceBonus.color = GameUtility.GetCurrencyColor(-this.mPerson.contract.raceBonus);
    }
    else
    {
      this.raceTarget.text = "-";
      this.raceBonus.text = "-";
      this.raceBonus.color = Color.white;
    }
  }

  private void SetMarketability()
  {
    GameUtility.SetActive(this.marketabilityContainer, this.mPerson is Driver);
    if (!this.marketabilityContainer.activeSelf)
      return;
    Driver mPerson = this.mPerson as Driver;
    this.marketability.value = mPerson.GetDriverStats().marketability;
    this.marketabilityLabel.text = GameUtility.GetPercentageText(mPerson.GetDriverStats().marketability, 1f, false, false);
  }

  private void SetContractStatus()
  {
    GameUtility.SetActive(this.contractStatusContainer, this.mPerson is Driver);
    if (!this.contractStatusContainer.activeSelf)
      return;
    if (this.mPerson.IsFreeAgent())
      this.contractStatus.text = "-";
    else
      this.contractStatus.text = Localisation.LocaliseEnum((Enum) this.mPerson.contract.proposedStatus);
  }
}
