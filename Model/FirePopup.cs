// Decompiled with JetBrains decompiler
// Type: FirePopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirePopup : UIDialogBox
{
  public Action OnFireSuccessfull;
  public Action OnFireFailure;
  public TextMeshProUGUI okButtonLabel;
  public UICharacterPortrait staffPortrait;
  public UICharacterPortrait driverPortrait;
  public TextMeshProUGUI replaceLabel;
  public TextMeshProUGUI replaceJobLabel;
  public TextMeshProUGUI ageLabel;
  public TextMeshProUGUI nameLabel;
  public TextMeshProUGUI positionLabel;
  public TextMeshProUGUI budgetLabel;
  public TextMeshProUGUI budgetHeaderLabel;
  public TextMeshProUGUI statusLabel;
  public TextMeshProUGUI costPerRace;
  public TextMeshProUGUI costToBreakContract;
  public TextMeshProUGUI contractRemaining;
  public TextMeshProUGUI qualifyingTarget;
  public TextMeshProUGUI qualifyingBonus;
  public TextMeshProUGUI raceTarget;
  public TextMeshProUGUI raceBonus;
  public Flag personFlag;
  public UIAbilityStars staffAbilityStars;
  public ToggleGroup toggleGroup;
  public UIFirePopupStaffOption optionOne;
  public UIFirePopupStaffOption optionTwo;
  private Person mPersonToFire;
  private Person mPersonToHire;
  private Person mReplacementPerson;

  public void Setup(Driver inDriver)
  {
    this.OnOKButton -= new Action(this.OnConfirm);
    this.OnOKButton += new Action(this.OnConfirm);
    this.mPersonToFire = (Person) inDriver;
    this.mPersonToHire = (Person) null;
    this.mReplacementPerson = (Person) null;
    GameUtility.SetActive(this.staffPortrait.gameObject, false);
    GameUtility.SetActive(this.driverPortrait.gameObject, true);
    this.driverPortrait.SetPortrait((Person) inDriver);
    this.positionLabel.text = Localisation.LocaliseEnum((Enum) inDriver.contract.proposedStatus);
    this.SetupPersonDetails();
    this.staffAbilityStars.SetAbilityStarsData(inDriver);
    this.replaceJobLabel.text = Localisation.LocaliseID("PSG_10010851", (GameObject) null);
    StringVariableParser.contractJob = inDriver.contract.job;
    this.okButtonLabel.text = Localisation.LocaliseID("PSG_10010849", (GameObject) null);
    this.mReplacementPerson = (Person) Game.instance.driverManager.GetReplacementDriver(false);
    this.mReplacementPerson.contract = Game.instance.player.team.contractManager.CreateDefaultReplacementContract();
    this.SetupReplacementDetails(false);
  }

  public override void OnCancelButtonClicked()
  {
    base.OnCancelButtonClicked();
    Game.instance.dilemmaSystem.isFiringBecauseOfDilemma = false;
    this.OnFireFailure = (Action) null;
    this.OnFireSuccessfull = (Action) null;
  }

  public void Setup(Person inStaff)
  {
    this.OnOKButton -= new Action(this.OnConfirm);
    this.OnOKButton += new Action(this.OnConfirm);
    this.mPersonToFire = inStaff;
    this.mPersonToHire = (Person) null;
    this.mReplacementPerson = (Person) null;
    GameUtility.SetActive(this.staffPortrait.gameObject, true);
    GameUtility.SetActive(this.driverPortrait.gameObject, false);
    this.staffPortrait.SetPortrait(inStaff);
    this.positionLabel.text = Localisation.LocaliseEnum((Enum) inStaff.contract.job);
    this.SetupPersonDetails();
    this.staffAbilityStars.SetAbilityStarsData(inStaff);
    if (inStaff is Mechanic)
    {
      this.mReplacementPerson = (Person) Game.instance.mechanicManager.GetReplacementMechanic(false);
      this.mReplacementPerson.contract = Game.instance.player.team.contractManager.CreateDefaultReplacementContract();
      StringVariableParser.contractJob = inStaff.contract.job;
      this.okButtonLabel.text = Localisation.LocaliseID("PSG_10010849", (GameObject) null);
    }
    else if (inStaff is Engineer)
    {
      this.mReplacementPerson = (Person) Game.instance.engineerManager.GetReplacementEngineer(false);
      this.mReplacementPerson.contract = Game.instance.player.team.contractManager.CreateDefaultReplacementContract();
      StringVariableParser.contractJob = inStaff.contract.job;
      this.okButtonLabel.text = Localisation.LocaliseID("PSG_10010849", (GameObject) null);
    }
    this.SetupReplacementDetails(true);
  }

  private void SetupPersonDetails()
  {
    StringVariableParser.subject = this.mPersonToFire;
    this.replaceLabel.text = Localisation.LocaliseID("PSG_10010848", (GameObject) null);
    StringVariableParser.subject = (Person) null;
    this.nameLabel.text = this.mPersonToFire.shortName;
    this.ageLabel.text = this.mPersonToFire.GetAge().ToString();
    this.personFlag.SetNationality(this.mPersonToFire.nationality);
    this.SetCosts();
  }

  private void SetCosts()
  {
    ContractPerson contract = this.mPersonToFire.contract;
    long num1 = (long) contract.GetContractTerminationCost();
    if (Game.instance.dilemmaSystem.isFiringBecauseOfDilemma)
      num1 = 0L;
    this.statusLabel.text = Localisation.LocaliseEnum((Enum) contract.proposedStatus);
    long num2 = GameUtility.RoundCurrency((long) (contract.yearlyWages / Game.instance.player.team.championship.eventCount));
    this.costPerRace.text = GameUtility.GetCurrencyString(-num2, 0);
    this.costPerRace.color = GameUtility.GetCurrencyColor(-num2);
    this.costToBreakContract.text = GameUtility.GetCurrencyString(-num1, 0);
    this.costToBreakContract.color = GameUtility.GetCurrencyColor(-num1);
    int monthsRemaining = this.mPersonToFire.contract.GetMonthsRemaining();
    StringVariableParser.intValue1 = monthsRemaining;
    this.contractRemaining.text = Localisation.LocaliseID(monthsRemaining != 1 ? "PSG_10010608" : "PSG_10010609", (GameObject) null);
    if (!this.mPersonToFire.IsFreeAgent() && this.mPersonToFire.contract.hasQualifyingBonus)
    {
      this.qualifyingTarget.text = GameUtility.FormatForPositionOrAbove(this.mPersonToFire.contract.qualifyingBonusTargetPosition, (string) null);
      this.qualifyingBonus.text = GameUtility.GetCurrencyString((long) -this.mPersonToFire.contract.qualifyingBonus, 0);
      this.qualifyingBonus.color = GameUtility.GetCurrencyColor(-this.mPersonToFire.contract.qualifyingBonus);
    }
    else
    {
      this.qualifyingTarget.text = "-";
      this.qualifyingBonus.text = "-";
      this.qualifyingBonus.color = Color.white;
    }
    if (!this.mPersonToFire.IsFreeAgent() && this.mPersonToFire.contract.hasRaceBonus)
    {
      this.raceTarget.text = GameUtility.FormatForPositionOrAbove(this.mPersonToFire.contract.raceBonusTargetPosition, (string) null);
      this.raceBonus.text = GameUtility.GetCurrencyString((long) -this.mPersonToFire.contract.raceBonus, 0);
      this.raceBonus.color = GameUtility.GetCurrencyColor(-this.mPersonToFire.contract.raceBonus);
    }
    else
    {
      this.raceTarget.text = "-";
      this.raceBonus.text = "-";
      this.raceBonus.color = Color.white;
    }
  }

  public void SetReplacementPerson(Person inPerson)
  {
    if (inPerson == null)
      return;
    this.mPersonToHire = inPerson;
    this.UpdateCost();
  }

  private void UpdateCost()
  {
    if (this.mPersonToFire == null)
      return;
    long inValue = Game.instance.player.team.financeController.finance.currentBudget - (long) this.mPersonToFire.contract.GetContractTerminationCost();
    this.budgetLabel.text = GameUtility.GetCurrencyString(inValue, 0);
    this.budgetLabel.color = GameUtility.GetCurrencyColor(inValue);
    this.budgetHeaderLabel.text = Localisation.LocaliseID("PSG_10010940", (GameObject) null);
  }

  private void SetupReplacementDetails(bool isStaff)
  {
    this.mPersonToHire = this.mReplacementPerson;
    this.toggleGroup.SetAllTogglesOff();
    if (isStaff)
    {
      GameUtility.SetActive(this.optionOne.gameObject, true);
      GameUtility.SetActive(this.optionTwo.gameObject, false);
      this.optionOne.Setup(this.mReplacementPerson);
      this.optionOne.toggle.isOn = true;
      GameUtility.SetActive(this.replaceJobLabel.gameObject, false);
    }
    else
    {
      Driver driver = this.mPersonToFire.contract.GetTeam().GetDriver(2);
      GameUtility.SetActive(this.optionTwo.gameObject, false);
      this.optionOne.Setup(this.mReplacementPerson);
      this.optionOne.toggle.isOn = true;
      if (this.optionTwo.gameObject.activeSelf)
      {
        this.optionTwo.Setup((Person) driver);
        this.optionTwo.toggle.isOn = false;
      }
      GameUtility.SetActive(this.replaceJobLabel.gameObject, this.optionTwo.gameObject.activeSelf);
    }
    this.UpdateCost();
  }

  private void OnConfirm()
  {
    Action inOnTransactionSucess = (Action) null;
    Contract.Job job = this.mPersonToFire.contract.job;
    switch (job)
    {
      case Contract.Job.Driver:
        inOnTransactionSucess = (Action) (() =>
        {
          Team team = this.mPersonToFire.contract.GetTeam();
          if (this.mPersonToFire.contract.proposedStatus == ContractPerson.Status.Reserve)
          {
            team.contractManager.FireDriver(this.mPersonToFire, Contract.ContractTerminationType.FiredByPlayer);
            team.contractManager.HireReplacementDriver();
            StringVariableParser.subject = this.mPersonToFire;
            UIManager.instance.dialogBoxManager.GetDialog<FeedbackPopup>().Show(Localisation.LocaliseID("PSG_10007187", (GameObject) null), Localisation.LocaliseID("PSG_10010850", (GameObject) null));
            StringVariableParser.subject = (Person) null;
          }
          else if (this.mPersonToHire.IsReplacementPerson())
          {
            team.contractManager.FireDriver(this.mPersonToFire, Contract.ContractTerminationType.FiredByPlayer);
            team.contractManager.HireReplacementDriver();
            StringVariableParser.subject = this.mPersonToFire;
            UIManager.instance.dialogBoxManager.GetDialog<FeedbackPopup>().Show(Localisation.LocaliseID("PSG_10007187", (GameObject) null), Localisation.LocaliseID("PSG_10010850", (GameObject) null));
            StringVariableParser.subject = (Person) null;
          }
          else
          {
            team.contractManager.PromoteDriver(this.mPersonToFire, this.mPersonToHire);
            team.contractManager.FireDriver(this.mPersonToFire, Contract.ContractTerminationType.FiredByPlayer);
            StringVariableParser.subject = this.mPersonToFire;
            UIManager.instance.dialogBoxManager.GetDialog<FeedbackPopup>().Show(Localisation.LocaliseID("PSG_10007187", (GameObject) null), Localisation.LocaliseID("PSG_10010850", (GameObject) null));
            StringVariableParser.subject = (Person) null;
          }
          this.mPersonToFire.contractManager.SetCooldownPeriodForBeingFiredByPlayer();
          Game.instance.player.team.CheckIfDriversPromisedAreFulfilled();
          this.Hide();
          if (!UIManager.instance.IsScreenOpen("AllDriversScreen"))
            UIManager.instance.ChangeScreen("AllDriversScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
          else
            UIManager.instance.RefreshCurrentPage();
          if (this.OnFireSuccessfull == null)
            return;
          this.OnFireSuccessfull();
          this.OnFireSuccessfull = (Action) null;
        });
        break;
      case Contract.Job.EngineerLead:
        inOnTransactionSucess = (Action) (() =>
        {
          Team team = this.mPersonToFire.contract.GetTeam();
          team.contractManager.FirePerson(this.mPersonToFire, Contract.ContractTerminationType.FiredByPlayer);
          team.contractManager.HireReplacementEngineer();
          this.mPersonToFire.contractManager.SetCooldownPeriodForBeingFiredByPlayer();
          StringVariableParser.subject = this.mPersonToFire;
          UIManager.instance.dialogBoxManager.GetDialog<FeedbackPopup>().Show(Localisation.LocaliseID("PSG_10010941", (GameObject) null), Localisation.LocaliseID("PSG_10010850", (GameObject) null));
          StringVariableParser.subject = (Person) null;
          Game.instance.player.team.CheckIfDriversPromisedAreFulfilled();
          this.Hide();
          if (!UIManager.instance.IsScreenOpen("StaffScreen"))
            UIManager.instance.ChangeScreen("StaffScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
          else
            UIManager.instance.RefreshCurrentPage();
          if (this.OnFireSuccessfull == null)
            return;
          this.OnFireSuccessfull();
          this.OnFireSuccessfull = (Action) null;
        });
        break;
      default:
        if (job == Contract.Job.Mechanic)
        {
          inOnTransactionSucess = (Action) (() =>
          {
            Team team = this.mPersonToFire.contract.GetTeam();
            team.contractManager.FirePerson(this.mPersonToFire, Contract.ContractTerminationType.FiredByPlayer);
            team.contractManager.HireReplacementMechanic();
            this.mPersonToFire.contractManager.SetCooldownPeriodForBeingFiredByPlayer();
            StringVariableParser.subject = this.mPersonToFire;
            UIManager.instance.dialogBoxManager.GetDialog<FeedbackPopup>().Show(Localisation.LocaliseID("PSG_10007188", (GameObject) null), Localisation.LocaliseID("PSG_10010850", (GameObject) null));
            StringVariableParser.subject = (Person) null;
            Game.instance.player.team.CheckIfDriversPromisedAreFulfilled();
            this.Hide();
            if (!UIManager.instance.IsScreenOpen("StaffScreen"))
              UIManager.instance.ChangeScreen("StaffScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
            else
              UIManager.instance.RefreshCurrentPage();
            if (this.OnFireSuccessfull == null)
              return;
            this.OnFireSuccessfull();
            this.OnFireSuccessfull = (Action) null;
          });
          break;
        }
        break;
    }
    Action inOnTransactionFail = (Action) (() =>
    {
      UIManager.instance.dialogBoxManager.Show((UIDialogBox) this);
      if (this.mPersonToFire.contract.job == Contract.Job.Driver)
        this.Setup(this.mPersonToFire as Driver);
      else
        this.Setup(this.mPersonToFire);
      if (this.OnFireFailure == null)
        return;
      this.OnFireFailure();
      this.OnFireFailure = (Action) null;
    });
    int inAmount = this.mPersonToFire.contract.GetContractTerminationCost();
    if (Game.instance.dilemmaSystem.isFiringBecauseOfDilemma)
      inAmount = 0;
    StringVariableParser.stringValue1 = this.mPersonToFire.shortName;
    Transaction transaction = new Transaction(this.mPersonToFire.GetTransactionType(), Transaction.Type.Debit, inAmount, Localisation.LocaliseID("PSG_10010574", (GameObject) null));
    this.ShowCostComparison();
    Game.instance.player.team.financeController.finance.ProcessTransactions(inOnTransactionSucess, inOnTransactionFail, true, transaction);
  }

  private void ShowCostComparison()
  {
    long inOld = 0;
    int eventCount = Game.instance.player.team.championship.eventCount;
    switch (this.mPersonToFire.contract.job)
    {
      case Contract.Job.Driver:
        inOld = Game.instance.player.team.financeController.GetCostPerRace(Transaction.Group.Drivers);
        break;
      case Contract.Job.Engineer:
      case Contract.Job.EngineerLead:
        inOld = Game.instance.player.team.financeController.GetCostPerRace(Transaction.Group.Designer);
        break;
      case Contract.Job.Mechanic:
        inOld = Game.instance.player.team.financeController.GetCostPerRace(Transaction.Group.Mechanics);
        break;
    }
    long num = 0;
    if (this.mPersonToFire is Driver)
      num = (long) (this.mPersonToFire as Driver).personalityTraitController.GetTieredPayDriverAmount();
    long inNew = inOld + GameUtility.RoundCurrency((long) (this.mPersonToFire.contract.yearlyWages / eventCount)) - GameUtility.RoundCurrency((long) (Game.instance.player.team.contractManager.CreateDefaultReplacementContract().yearlyWages / eventCount)) - num;
    UIManager.instance.dialogBoxManager.GetDialog<FinancePopupWidget>().ShowCostPerRace(inOld, inNew);
  }
}
