// Decompiled with JetBrains decompiler
// Type: UIHirePopupStaffOption
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHirePopupStaffOption : MonoBehaviour
{
  public Toggle toggle;
  public UICharacterPortrait personPortrait;
  public UICharacterPortrait driverPortrait;
  public Flag personFlag;
  public TextMeshProUGUI ageLabel;
  public TextMeshProUGUI roleLabel;
  public TextMeshProUGUI statusLabel;
  public TextMeshProUGUI personNameLabel;
  public TextMeshProUGUI contractStatusLabel;
  public TextMeshProUGUI costPerRaceLabel;
  public TextMeshProUGUI breakClauseLabel;
  public TextMeshProUGUI contractLengthLabel;
  public TextMeshProUGUI qualifyingBonusTarget;
  public TextMeshProUGUI qualifyingAmmount;
  public TextMeshProUGUI raceBonusTarget;
  public TextMeshProUGUI raceBonusAmmount;
  public GameObject driverHeader;
  public GameObject statusContainer;
  public GameObject costContainer;
  public GameObject personContainer;
  public GameObject emptyContainer;
  public UIAbilityStars abilityStars;
  public HirePopup widget;
  private Person mPerson;

  public Person person
  {
    get
    {
      return this.mPerson;
    }
  }

  public void Setup(Person inPerson)
  {
    this.toggle.onValueChanged.RemoveAllListeners();
    this.toggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SetReplacementPerson()));
    this.mPerson = inPerson;
    StringVariableParser.subject = this.mPerson;
    if (inPerson != null)
    {
      GameUtility.SetActive(this.personContainer, true);
      GameUtility.SetActive(this.emptyContainer, false);
      this.personFlag.SetNationality(this.mPerson.nationality);
      this.personNameLabel.text = this.mPerson.name;
      Engineer mPerson1 = this.mPerson as Engineer;
      Mechanic mPerson2 = this.mPerson as Mechanic;
      Driver mPerson3 = this.mPerson as Driver;
      GameUtility.SetActive(this.driverHeader, mPerson3 != null);
      GameUtility.SetActive(this.roleLabel.gameObject, mPerson3 == null);
      GameUtility.SetActive(this.statusContainer, mPerson3 != null);
      if (mPerson3 != null)
      {
        this.statusLabel.text = this.mPerson.contract.GetCurrentStatusText();
        this.contractStatusLabel.text = this.mPerson.contract.GetProposedStatusText();
      }
      else
        this.roleLabel.text = Localisation.LocaliseEnum((Enum) this.mPerson.contract.job);
      this.ageLabel.text = this.mPerson.GetAge().ToString();
      if (mPerson1 != null)
      {
        GameUtility.SetActive(this.personPortrait.gameObject, true);
        GameUtility.SetActive(this.driverPortrait.gameObject, false);
        this.personPortrait.SetPortrait(this.mPerson);
        this.abilityStars.SetAbilityStarsData((Person) mPerson1);
      }
      else if (mPerson2 != null)
      {
        GameUtility.SetActive(this.personPortrait.gameObject, true);
        GameUtility.SetActive(this.driverPortrait.gameObject, false);
        this.personPortrait.SetPortrait(this.mPerson);
        this.abilityStars.SetAbilityStarsData((Person) mPerson2);
      }
      else if (mPerson3 != null)
      {
        GameUtility.SetActive(this.personPortrait.gameObject, false);
        GameUtility.SetActive(this.driverPortrait.gameObject, true);
        this.driverPortrait.SetPortrait((Person) mPerson3);
        this.abilityStars.SetAbilityStarsData(mPerson3);
      }
      GameUtility.SetActive(this.costContainer, true);
      this.costPerRaceLabel.text = GameUtility.GetCurrencyString(-this.mPerson.contract.perRaceCost, 0);
      this.costPerRaceLabel.color = GameUtility.GetCurrencyColor(-this.mPerson.contract.perRaceCost);
      this.breakClauseLabel.text = GameUtility.GetCurrencyString((long) -this.mPerson.contract.GetContractTerminationCost(), 0);
      this.breakClauseLabel.color = GameUtility.GetCurrencyColor(-this.mPerson.contract.GetContractTerminationCost());
      if (this.mPerson.IsReplacementPerson())
      {
        this.contractLengthLabel.text = Localisation.LocaliseID("PSG_10010607", (GameObject) null);
      }
      else
      {
        int monthsRemaining = this.mPerson.contract.GetMonthsRemaining();
        StringVariableParser.intValue1 = monthsRemaining;
        this.contractLengthLabel.text = Localisation.LocaliseID(monthsRemaining != 1 ? "PSG_10010608" : "PSG_10010609", (GameObject) null);
      }
      if (!this.mPerson.contract.hasQualifyingBonus)
      {
        this.qualifyingBonusTarget.text = "-";
        this.qualifyingAmmount.text = "-";
        this.qualifyingAmmount.color = Color.white;
      }
      else
      {
        this.qualifyingBonusTarget.text = GameUtility.FormatForPositionOrAbove(this.mPerson.contract.qualifyingBonusTargetPosition, (string) null);
        this.qualifyingAmmount.text = GameUtility.GetCurrencyString((long) -this.mPerson.contract.qualifyingBonus, 0);
        this.qualifyingAmmount.color = GameUtility.GetCurrencyColor(-this.mPerson.contract.qualifyingBonus);
      }
      if (!this.mPerson.contract.hasRaceBonus)
      {
        this.raceBonusTarget.text = "-";
        this.raceBonusAmmount.text = "-";
        this.raceBonusAmmount.color = Color.white;
      }
      else
      {
        this.raceBonusTarget.text = GameUtility.FormatForPositionOrAbove(this.mPerson.contract.raceBonusTargetPosition, (string) null);
        this.raceBonusAmmount.text = GameUtility.GetCurrencyString((long) -this.mPerson.contract.raceBonus, 0);
        this.raceBonusAmmount.color = GameUtility.GetCurrencyColor(-this.mPerson.contract.raceBonus);
      }
    }
    else
    {
      GameUtility.SetActive(this.personContainer, false);
      GameUtility.SetActive(this.emptyContainer, true);
    }
    StringVariableParser.subject = (Person) null;
  }

  public void ShowTooltip()
  {
    UIManager.instance.dialogBoxManager.GetDialog<ContractNegotiationRollover>().ShowRollover(this.mPerson);
  }

  public void HideTooltip()
  {
    UIManager.instance.dialogBoxManager.GetDialog<ContractNegotiationRollover>().HideRollover();
  }

  private void SetReplacementPerson()
  {
    if (!this.toggle.isOn)
      return;
    this.widget.SelectPersonToReplace(this.mPerson);
  }
}
