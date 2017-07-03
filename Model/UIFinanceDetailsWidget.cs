// Decompiled with JetBrains decompiler
// Type: UIFinanceDetailsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIFinanceDetailsWidget : MonoBehaviour
{
  private TeamFinanceController.NextYearCarInvestement investmentTypeSelected = TeamFinanceController.NextYearCarInvestement.Medium;
  public UIGridList balanceSheetList;
  public Button requestFundsButton;
  public Toggle[] carDevToggle;
  public TextMeshProUGUI[] carDevToggleLabels;
  public TextMeshProUGUI costPerRaceHeader;
  public TextMeshProUGUI overallCostPerRace;
  public TextMeshProUGUI[] overallBalance;
  public TextMeshProUGUI initialSeasonBudget;
  public GameObject panelContainer;
  public Button openCarPanelButton;
  public Button closeCarPanelButton;
  public Button closeAndConfirmButton;

  private void Awake()
  {
    for (int index = 0; index < this.carDevToggle.Length; ++index)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: reference to a compiler-generated method
      this.carDevToggle[index].onValueChanged.AddListener(new UnityAction<bool>(new UIFinanceDetailsWidget.\u003CAwake\u003Ec__AnonStorey81()
      {
        \u003C\u003Ef__this = this,
        carInvestement = (TeamFinanceController.NextYearCarInvestement) index
      }.\u003C\u003Em__173));
    }
    this.openCarPanelButton.onClick.AddListener(new UnityAction(this.OpenCarPanel));
    this.closeCarPanelButton.onClick.AddListener(new UnityAction(this.CloseCarPanel));
    this.closeAndConfirmButton.onClick.AddListener(new UnityAction(this.CloseCarPanel));
    this.closeAndConfirmButton.onClick.AddListener(new UnityAction(this.ConfirmInvestment));
    this.requestFundsButton.onClick.AddListener(new UnityAction(this.OnRequestFundsButton));
  }

  private void OpenCarPanel()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    GameUtility.SetActive(this.panelContainer, true);
    this.SetToggles();
  }

  public void SetToggles()
  {
    for (int index = 0; index < this.carDevToggle.Length; ++index)
    {
      TeamFinanceController.NextYearCarInvestement yearCarInvestement = (TeamFinanceController.NextYearCarInvestement) index;
      this.carDevToggle[index].isOn = Game.instance.player.team.financeController.nextYearCarInvestement == yearCarInvestement;
    }
  }

  private void CloseCarPanel()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    GameUtility.SetActive(this.panelContainer, false);
  }

  private void ConfirmInvestment()
  {
    Game.instance.player.team.financeController.SetCarInvestement(this.investmentTypeSelected);
    this.OnEnter();
  }

  private void SetToggleValue(bool inValue, TeamFinanceController.NextYearCarInvestement inInvestment)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (!inValue)
      return;
    this.investmentTypeSelected = inInvestment;
  }

  public void OnEnter()
  {
    Finance finance = Game.instance.player.team.financeController.finance;
    for (Transaction.Group inGroup = Transaction.Group.CarParts; inGroup < Transaction.Group.Count; ++inGroup)
    {
      if (inGroup != Transaction.Group.BackstoryFinancial || Game.instance.player.playerBackStoryType == PlayerBackStory.PlayerBackStoryType.Financial)
      {
        UIBreakdownBudgetEntry breakdownBudgetEntry = this.balanceSheetList.GetOrCreateItem<UIBreakdownBudgetEntry>((int) inGroup);
        if (inGroup == Transaction.Group.Designer)
          StringVariableParser.subject = Game.instance.player.team.contractManager.GetPersonOnJob(Contract.Job.EngineerLead);
        breakdownBudgetEntry.SetLabels(Localisation.LocaliseEnum((Enum) inGroup), finance.transactionHistory.GetBalance(inGroup, TransactionHistory.TimeOption.ThisYear), inGroup);
        StringVariableParser.subject = (Person) null;
      }
    }
    StringVariableParser.ordinalNumberString = GameUtility.GetCurrencyString(Game.instance.player.team.financeController.GetCarDevCost(TeamFinanceController.NextYearCarInvestement.Low), 0);
    this.carDevToggleLabels[0].text = Localisation.LocaliseID("PSG_10010982", (GameObject) null);
    StringVariableParser.ordinalNumberString = GameUtility.GetCurrencyString(Game.instance.player.team.financeController.GetCarDevCost(TeamFinanceController.NextYearCarInvestement.Medium), 0);
    this.carDevToggleLabels[1].text = Localisation.LocaliseID("PSG_10010981", (GameObject) null);
    StringVariableParser.ordinalNumberString = GameUtility.GetCurrencyString(Game.instance.player.team.financeController.GetCarDevCost(TeamFinanceController.NextYearCarInvestement.High), 0);
    this.carDevToggleLabels[2].text = Localisation.LocaliseID("PSG_10010980", (GameObject) null);
    for (int index = 0; index < this.overallBalance.Length; ++index)
    {
      this.overallBalance[index].text = GameUtility.GetCurrencyString(finance.currentBudget, 0);
      this.overallBalance[index].color = GameUtility.GetCurrencyColor(finance.currentBudget);
    }
    this.initialSeasonBudget.text = GameUtility.GetCurrencyString(finance.initialBudget, 0);
    List<Transaction> eventTransactions = Game.instance.player.team.financeController.GetAllEventTransactions();
    long inValue = 0;
    for (int index = 0; index < eventTransactions.Count; ++index)
      inValue += eventTransactions[index].amountWithSign;
    this.overallCostPerRace.text = GameUtility.GetCurrencyStringWithSign(inValue, 0);
    this.overallCostPerRace.color = GameUtility.GetCurrencyColor(inValue);
    if (inValue > 0L)
      this.costPerRaceHeader.text = Localisation.LocaliseID("PSG_10010863", (GameObject) null);
    else
      this.costPerRaceHeader.text = Localisation.LocaliseID("PSG_10006844", (GameObject) null);
  }

  private void OnRequestFundsButton()
  {
    RequestFundsPopup.OpenPopup();
  }
}
