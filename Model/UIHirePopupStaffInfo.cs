// Decompiled with JetBrains decompiler
// Type: UIHirePopupStaffInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UIHirePopupStaffInfo : MonoBehaviour
{
  public UICharacterPortrait personPortrait;
  public UICharacterPortrait driverPortrait;
  public Flag personFlag;
  public TextMeshProUGUI ageLabel;
  public TextMeshProUGUI personNameLabel;
  public TextMeshProUGUI contractStatusLabel;
  public TextMeshProUGUI costPerRace;
  public TextMeshProUGUI costToBreakContract;
  public TextMeshProUGUI contractRemaining;
  public TextMeshProUGUI qualifyingBonusTarget;
  public TextMeshProUGUI qualifyingAmmount;
  public TextMeshProUGUI raceBonusTarget;
  public TextMeshProUGUI raceBonusAmmount;
  public GameObject statusContainer;
  public UIAbilityStars abilityStars;
  private Person mPerson;
  private ContractPerson mDraftContract;

  public void Setup(Person inPerson, ContractPerson inDraftContract)
  {
    this.mPerson = inPerson;
    this.mDraftContract = inDraftContract;
    this.personFlag.SetNationality(this.mPerson.nationality);
    this.personNameLabel.text = this.mPerson.name;
    this.ageLabel.text = this.mPerson.GetAge().ToString();
    GameUtility.SetActive(this.statusContainer, this.mPerson is Driver);
    if (this.mPerson is Engineer)
    {
      this.abilityStars.SetAbilityStarsData(this.mPerson);
      GameUtility.SetActive(this.personPortrait.gameObject, true);
      GameUtility.SetActive(this.driverPortrait.gameObject, false);
      this.personPortrait.SetPortrait(this.mPerson);
    }
    else if (this.mPerson is Mechanic)
    {
      this.abilityStars.SetAbilityStarsData(this.mPerson);
      GameUtility.SetActive(this.personPortrait.gameObject, true);
      GameUtility.SetActive(this.driverPortrait.gameObject, false);
      this.personPortrait.SetPortrait(this.mPerson);
    }
    else if (this.mPerson is Driver)
    {
      Driver mPerson = this.mPerson as Driver;
      this.abilityStars.SetAbilityStarsData(mPerson);
      GameUtility.SetActive(this.personPortrait.gameObject, false);
      GameUtility.SetActive(this.driverPortrait.gameObject, true);
      this.driverPortrait.SetPortrait(this.mPerson);
      this.contractStatusLabel.text = mPerson.contract.GetProposedStatusText();
    }
    long contractTerminationCost = (long) this.mDraftContract.GetContractTerminationCost();
    long num = GameUtility.RoundCurrency((long) (this.mDraftContract.yearlyWages / Game.instance.player.team.championship.eventCount));
    this.costPerRace.text = GameUtility.GetCurrencyString(-num, 0);
    this.costPerRace.color = GameUtility.GetCurrencyColor(-num);
    this.costToBreakContract.text = GameUtility.GetCurrencyString(-contractTerminationCost, 0);
    this.costToBreakContract.color = GameUtility.GetCurrencyColor(-contractTerminationCost);
    int monthsRemaining = this.mDraftContract.GetMonthsRemaining();
    StringVariableParser.intValue1 = monthsRemaining;
    this.contractRemaining.text = Localisation.LocaliseID(monthsRemaining != 1 ? "PSG_10010608" : "PSG_10010609", (GameObject) null);
    if (!this.mDraftContract.hasQualifyingBonus)
    {
      this.qualifyingBonusTarget.text = "-";
      this.qualifyingAmmount.text = "-";
      this.qualifyingAmmount.color = Color.white;
    }
    else
    {
      this.qualifyingBonusTarget.text = GameUtility.FormatForPositionOrAbove(this.mDraftContract.qualifyingBonusTargetPosition, (string) null);
      this.qualifyingAmmount.text = GameUtility.GetCurrencyString((long) -this.mDraftContract.qualifyingBonus, 0);
      this.qualifyingAmmount.color = GameUtility.GetCurrencyColor(-this.mDraftContract.qualifyingBonus);
    }
    if (!this.mDraftContract.hasRaceBonus)
    {
      this.raceBonusTarget.text = "-";
      this.raceBonusAmmount.text = "-";
      this.raceBonusAmmount.color = Color.white;
    }
    else
    {
      this.raceBonusTarget.text = GameUtility.FormatForPositionOrAbove(this.mDraftContract.raceBonusTargetPosition, (string) null);
      this.raceBonusAmmount.text = GameUtility.GetCurrencyString((long) -this.mDraftContract.raceBonus, 0);
      this.raceBonusAmmount.color = GameUtility.GetCurrencyColor(-this.mDraftContract.raceBonus);
    }
  }
}
