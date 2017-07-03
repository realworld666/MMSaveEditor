// Decompiled with JetBrains decompiler
// Type: UIFirePopupStaffOption
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIFirePopupStaffOption : MonoBehaviour
{
  public Toggle toggle;
  public UICharacterPortrait personPortrait;
  public UICharacterPortrait driverPortrait;
  public Flag personFlag;
  public TextMeshProUGUI ageLabel;
  public TextMeshProUGUI headingLabel;
  public TextMeshProUGUI headingDescriptionLabel;
  public TextMeshProUGUI jobTitleLabel;
  public TextMeshProUGUI personNameLabel;
  public TextMeshProUGUI costPerRaceLabel;
  public TextMeshProUGUI breakCostLabel;
  public TextMeshProUGUI contractRemainingLabel;
  public GameObject personContainer;
  public GameObject emptyContainer;
  public UIAbilityStars abilityStars;
  public FirePopup widget;
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
    if (inPerson != null)
    {
      GameUtility.SetActive(this.personContainer, true);
      GameUtility.SetActive(this.emptyContainer, false);
      this.personFlag.SetNationality(this.mPerson.nationality);
      this.personNameLabel.text = this.mPerson.name;
      Engineer mPerson1 = this.mPerson as Engineer;
      Mechanic mPerson2 = this.mPerson as Mechanic;
      Driver mPerson3 = this.mPerson as Driver;
      this.headingLabel.text = !this.mPerson.IsReplacementPerson() ? Localisation.LocaliseID("PSG_10006856", (GameObject) null) : Localisation.LocaliseID("PSG_10008905", (GameObject) null);
      this.headingDescriptionLabel.text = !this.mPerson.IsReplacementPerson() ? Localisation.LocaliseID("PSG_10006856", (GameObject) null) : Localisation.LocaliseID("PSG_10010852", (GameObject) null);
      this.ageLabel.text = this.mPerson.GetAge().ToString();
      this.costPerRaceLabel.text = GameUtility.GetCurrencyString(-this.mPerson.contract.perRaceCost, 0);
      this.costPerRaceLabel.color = GameUtility.GetCurrencyColor(-this.mPerson.contract.perRaceCost);
      this.breakCostLabel.text = GameUtility.GetCurrencyString((long) -this.mPerson.contract.GetContractTerminationCost(), 0);
      this.breakCostLabel.color = GameUtility.GetCurrencyColor(-this.mPerson.contract.GetContractTerminationCost());
      int monthsRemaining = this.mPerson.contract.GetMonthsRemaining();
      StringVariableParser.intValue1 = monthsRemaining;
      this.contractRemainingLabel.text = Localisation.LocaliseID(monthsRemaining != 1 ? "PSG_10010608" : "PSG_10010609", (GameObject) null);
      StringVariableParser.subject = this.mPerson;
      StringVariableParser.contractJob = this.mPerson.contract.job;
      if (mPerson1 != null)
      {
        GameUtility.SetActive(this.personPortrait.gameObject, true);
        GameUtility.SetActive(this.driverPortrait.gameObject, false);
        this.personPortrait.SetPortrait(this.mPerson);
        this.abilityStars.SetAbilityStarsData((Person) mPerson1);
        this.jobTitleLabel.text = Localisation.LocaliseID("PSG_10010605", (GameObject) null);
      }
      else if (mPerson2 != null)
      {
        GameUtility.SetActive(this.personPortrait.gameObject, true);
        GameUtility.SetActive(this.driverPortrait.gameObject, false);
        this.personPortrait.SetPortrait(this.mPerson);
        this.abilityStars.SetAbilityStarsData((Person) mPerson2);
        this.jobTitleLabel.text = Localisation.LocaliseID("PSG_10010605", (GameObject) null);
      }
      else if (mPerson3 != null)
      {
        GameUtility.SetActive(this.personPortrait.gameObject, false);
        GameUtility.SetActive(this.driverPortrait.gameObject, true);
        this.driverPortrait.SetPortrait((Person) mPerson3);
        this.abilityStars.SetAbilityStarsData(mPerson3);
        this.jobTitleLabel.text = !mPerson3.IsReplacementPerson() ? Localisation.LocaliseID("PSG_10001585", (GameObject) null) : Localisation.LocaliseID("PSG_10010605", (GameObject) null);
      }
      StringVariableParser.subject = (Person) null;
    }
    else
    {
      GameUtility.SetActive(this.personContainer, false);
      GameUtility.SetActive(this.emptyContainer, true);
    }
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
    this.widget.SetReplacementPerson(this.mPerson);
  }
}
