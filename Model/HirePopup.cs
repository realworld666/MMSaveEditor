// Decompiled with JetBrains decompiler
// Type: HirePopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using MM2;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HirePopup : UIDialogBox
{
  private List<int[]> mPersonOrder = new List<int[]>()
  {
    new int[3]{ 1, 0, 2 },
    new int[3]{ 1, 0, 2 },
    new int[3]{ 1, 0, 2 }
  };
  public UIHirePopupStaffInfo info;
  public UIHirePopupStaffOption[] options;
  public TextMeshProUGUI contractSignedByLabel;
  public TextMeshProUGUI selectFireReplaceLabel;
  public ToggleGroup toggleGroup;
  private Person mPersonToHire;
  private Person mPersonToFire;
  private Person[] mPersonOptions;
  private ContractPerson mDraftContract;
  private ContractNegotiationScreen.NegotatiationType mNegotiationType;

  public void Setup(ContractPerson inDraftContract, Person inPersonToHire, ContractNegotiationScreen.NegotatiationType inNegotiationType)
  {
    this.mNegotiationType = inNegotiationType;
    this.mDraftContract = inDraftContract;
    this.mPersonToHire = inPersonToHire;
    this.mPersonToFire = (Person) null;
    this.contractSignedByLabel.text = Localisation.LocaliseID("PSG_10010960", (GameObject) null);
    this.okButton.onClick.RemoveAllListeners();
    this.okButton.onClick.AddListener(new UnityAction(this.OnOkButtonClick));
    this.info.Setup(this.mPersonToHire, this.mDraftContract);
    if (this.mPersonToHire is Engineer)
      this.SetupForRole(this.mPersonToHire as Engineer);
    else if (this.mPersonToHire is Mechanic)
      this.SetupForRole(this.mPersonToHire as Mechanic);
    else if (this.mPersonToHire is Driver)
      this.SetupForRole(this.mPersonToHire as Driver);
    this.SetupOptions();
  }

  public void ShowTooltip()
  {
    UIManager.instance.dialogBoxManager.GetDialog<ContractNegotiationRollover>().ShowRollover(this.mPersonToHire);
  }

  public void HideTooltip()
  {
    UIManager.instance.dialogBoxManager.GetDialog<ContractNegotiationRollover>().HideRollover();
  }

  private void SetupForRole(Engineer inEngineer)
  {
    this.mPersonOptions = new Person[1]
    {
      Game.instance.player.team.contractManager.GetPersonOnJob(Contract.Job.EngineerLead)
    };
    this.selectFireReplaceLabel.text = Localisation.LocaliseID("PSG_10010961", (GameObject) null);
  }

  private void SetupForRole(Mechanic inMechanic)
  {
    this.mPersonOptions = Game.instance.player.team.contractManager.GetAllPeopleOnJob(Contract.Job.Mechanic).ToArray();
    this.selectFireReplaceLabel.text = Localisation.LocaliseID("PSG_10010962", (GameObject) null);
  }

  private void SetupForRole(Driver inDriver)
  {
    this.SetupDriversOptions();
    this.selectFireReplaceLabel.text = Localisation.LocaliseID("PSG_10010963", (GameObject) null);
  }

  private void SetupDriversOptions()
  {
    List<Person> allPeopleOnJob = Game.instance.player.team.contractManager.GetAllPeopleOnJob(Contract.Job.Driver);
    if (allPeopleOnJob.Count == 2)
      allPeopleOnJob.Add((Person) null);
    this.mPersonOptions = allPeopleOnJob.ToArray();
  }

  private void SetupOptions()
  {
    this.toggleGroup.SetAllTogglesOff();
    bool flag = false;
    for (int index = 0; index < this.options.Length; ++index)
    {
      UIHirePopupStaffOption option = this.options[this.mPersonOrder[this.mPersonOptions.Length - 1][index]];
      GameUtility.SetActive(option.gameObject, index < this.mPersonOptions.Length);
      if (option.gameObject.activeSelf)
      {
        option.Setup(this.mPersonOptions[index]);
        if (!flag)
        {
          this.mPersonToFire = option.person;
          option.toggle.isOn = true;
          flag = true;
        }
      }
    }
    GameUtility.SetActive(this.selectFireReplaceLabel.gameObject, this.options.Length > 1);
  }

  public void SelectPersonToReplace(Person inPerson)
  {
    this.mPersonToFire = inPerson;
  }

  private void OnOkButtonClick()
  {
    this.Hide();
    Action inOnTransactionSucess = (Action) (() =>
    {
      this.ConfirmNewHire();
      this.RefreshScreen();
      this.Hide();
    });
    Action inOnTransactionFail = (Action) (() => UIManager.instance.dialogBoxManager.Show((UIDialogBox) this));
    List<Transaction> inList = new List<Transaction>();
    StringVariableParser.stringValue1 = this.mPersonToHire.shortName;
    Transaction transaction1 = new Transaction(this.mPersonToHire.GetTransactionType(), Transaction.Type.Debit, this.mDraftContract.amountForContractorToPay, Localisation.LocaliseID("PSG_10010574", (GameObject) null));
    StringVariableParser.stringValue1 = this.mPersonToHire.shortName;
    Transaction transaction2 = new Transaction(this.mPersonToHire.GetTransactionType(), Transaction.Type.Debit, this.mDraftContract.signOnFee, Localisation.LocaliseID("PSG_10010575", (GameObject) null));
    Finance.AddTransactionsIfNotZero(inList, transaction1, transaction2);
    if (this.mPersonToFire != null)
    {
      StringVariableParser.stringValue1 = this.mPersonToFire.shortName;
      Transaction transaction3 = new Transaction(this.mPersonToFire.GetTransactionType(), Transaction.Type.Debit, this.mPersonToFire.contract.GetContractTerminationCost(), Localisation.LocaliseID("PSG_10010574", (GameObject) null));
      Finance.AddTransactionsIfNotZero(inList, transaction3);
      this.ShowCostComparison();
      Game.instance.player.team.financeController.finance.ProcessTransactions(inOnTransactionSucess, inOnTransactionFail, true, inList.ToArray());
    }
    else
      Game.instance.player.team.financeController.finance.ProcessTransactions(inOnTransactionSucess, inOnTransactionFail, true, inList.ToArray());
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
    {
      Driver mPersonToFire = this.mPersonToFire as Driver;
      num += (long) mPersonToFire.personalityTraitController.GetTieredPayDriverAmount();
    }
    if (this.mPersonToHire is Driver)
    {
      Driver mPersonToHire = this.mPersonToHire as Driver;
      if (this.mDraftContract.currentStatus == ContractPerson.Status.Reserve)
        num -= (long) ((double) mPersonToHire.personalityTraitController.GetTieredPayDriverAmountForPlayer() * 0.5);
      else
        num -= (long) mPersonToHire.personalityTraitController.GetTieredPayDriverAmountForPlayer();
    }
    long inNew = inOld + GameUtility.RoundCurrency((long) (this.mPersonToFire.contract.yearlyWages / eventCount)) - GameUtility.RoundCurrency((long) (this.mDraftContract.yearlyWages / eventCount)) - num;
    UIManager.instance.dialogBoxManager.GetDialog<FinancePopupWidget>().ShowCostPerRace(inOld, inNew);
  }

  private void ConfirmNewHire()
  {
    switch (this.mNegotiationType)
    {
      case ContractNegotiationScreen.NegotatiationType.NewDriver:
      case ContractNegotiationScreen.NegotatiationType.NewDriverUnemployed:
        this.ConfirmNewHireDriver();
        this.UpdateDriverHiredAchievements();
        break;
      case ContractNegotiationScreen.NegotatiationType.NewStaffEmployed:
      case ContractNegotiationScreen.NegotatiationType.NewStaffUnemployed:
        this.UpdateStaffHiredAchievements();
        Game.instance.player.team.contractManager.ReplacePersonWithNewOne(this.mDraftContract, this.mPersonToHire, this.mPersonToFire);
        this.mPersonToFire.contractManager.SetCooldownPeriodForBeingFiredByPlayer();
        StringVariableParser.contractJob = this.mPersonToHire.contract.job;
        StringVariableParser.subject = this.mPersonToHire;
        FeedbackPopup.Open(Localisation.LocaliseID("PSG_10010866", (GameObject) null), string.Format("{0} has been hired!", (object) this.mPersonToHire.name));
        break;
    }
    Game.instance.player.team.CheckIfDriversPromisedAreFulfilled();
  }

  private void ConfirmNewHireDriver()
  {
    ContractManagerTeam contractManager = Game.instance.player.team.contractManager;
    if (this.mPersonToFire != null)
    {
      contractManager.ReplaceCurrentDriverWithNewOne(this.mDraftContract, this.mPersonToHire, this.mPersonToFire);
      this.mPersonToFire.contractManager.SetCooldownPeriodForBeingFiredByPlayer();
    }
    else
    {
      StringVariableParser.personReplaced = (Person) null;
      this.mDraftContract.SetCurrentStatus(ContractPerson.Status.Reserve);
      contractManager.HireNewDriver(this.mDraftContract, this.mPersonToHire);
    }
    StringVariableParser.subject = this.mPersonToHire;
    FeedbackPopup.Open(Localisation.LocaliseID("PSG_10010867", (GameObject) null), string.Format("{0} has been hired!", (object) this.mPersonToHire.name));
  }

  private void RefreshScreen()
  {
    switch (this.mNegotiationType)
    {
      case ContractNegotiationScreen.NegotatiationType.NewDriver:
      case ContractNegotiationScreen.NegotatiationType.NewDriverUnemployed:
        if (!UIManager.instance.IsScreenOpen("AllDriversScreen"))
        {
          UIManager.instance.ChangeScreen("AllDriversScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
          break;
        }
        UIManager.instance.RefreshCurrentPage();
        break;
      case ContractNegotiationScreen.NegotatiationType.NewStaffEmployed:
      case ContractNegotiationScreen.NegotatiationType.NewStaffUnemployed:
        if (!UIManager.instance.IsScreenOpen("StaffScreen"))
        {
          UIManager.instance.ChangeScreen("StaffScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
          break;
        }
        UIManager.instance.RefreshCurrentPage();
        break;
    }
  }

  private void UpdateDriverHiredAchievements()
  {
    App.instance.steamAchievementsManager.SetFloatStat(Achievements.FloatStatsEnum.Most_Spent_On_A_Driver, (float) this.mDraftContract.GetMonthlyWageCost() * 12f);
    App.instance.steamAchievementsManager.UnlockAchievement(Achievements.AchievementEnum.Hire_A_Driver);
    List<Mechanic> mechanics = new List<Mechanic>();
    Game.instance.player.team.contractManager.GetAllMechanics(ref mechanics);
    for (int index = 0; index < mechanics.Count; ++index)
    {
      if (mechanics[index].GetRelationshipWithDriver(this.mPersonToHire as Driver) != null)
      {
        App.instance.steamAchievementsManager.UnlockAchievement(Achievements.AchievementEnum.Hire_Driver_Mech_Combo);
        break;
      }
    }
  }

  private void UpdateStaffHiredAchievements()
  {
    if (this.mPersonToHire.contract.job == Contract.Job.EngineerLead && Game.instance.player.team.name == "Zampelli Engineering" && (this.mPersonToHire is Engineer && (double) (this.mPersonToHire as Engineer).stats.GetAbility() >= 4.5))
      App.instance.steamAchievementsManager.UnlockAchievement(Achievements.AchievementEnum.Hire_Five_Star_Designer_Zamp);
    if (this.mPersonToHire.contract.job != Contract.Job.Mechanic)
      return;
    List<Driver> drivers = new List<Driver>();
    Game.instance.player.team.contractManager.GetAllDrivers(ref drivers);
    for (int index = 0; index < drivers.Count; ++index)
    {
      if ((this.mPersonToHire as Mechanic).GetRelationshipWithDriver(drivers[index]) != null)
      {
        App.instance.steamAchievementsManager.UnlockAchievement(Achievements.AchievementEnum.Hire_Driver_Mech_Combo);
        break;
      }
    }
  }
}
