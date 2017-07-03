// Decompiled with JetBrains decompiler
// Type: UIBreakdownBudgetEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBreakdownBudgetEntry : MonoBehaviour
{
  private List<Transaction> mTransactions = new List<Transaction>();
  public Transaction.Group group;
  public TextMeshProUGUI descriptionLabel;
  public TextMeshProUGUI valueLabel;
  public TextMeshProUGUI perRaceCost;
  public Button tooltipPerRace;
  public Button tooltipTransactions;

  public void SetLabels(string inDescription, long inCurrencyValue, Transaction.Group inGroup)
  {
    this.group = inGroup;
    this.descriptionLabel.text = inDescription;
    this.tooltipTransactions.interactable = inCurrencyValue != 0L;
    this.valueLabel.text = GameUtility.GetCurrencyString(inCurrencyValue, 0);
    this.valueLabel.color = GameUtility.GetCurrencyColor(inCurrencyValue);
    List<Transaction> eventTransactions = Game.instance.player.team.financeController.GetEventTransactions(inGroup);
    long inValue = 0;
    for (int index = 0; index < eventTransactions.Count; ++index)
      inValue += eventTransactions[index].amountWithSign;
    if (inValue == 0L)
    {
      this.perRaceCost.text = "-";
      this.tooltipPerRace.interactable = false;
    }
    else
    {
      this.tooltipPerRace.interactable = true;
      this.perRaceCost.text = GameUtility.GetCurrencyStringWithSign(inValue, 0);
    }
    this.perRaceCost.color = GameUtility.GetCurrencyColor(inValue);
  }

  public void ShowToolTipPerRace()
  {
    List<Transaction> eventTransactions = Game.instance.player.team.financeController.GetEventTransactions(this.group);
    if (eventTransactions.Count <= 0)
      return;
    UIManager.instance.dialogBoxManager.GetDialog<FinanceBreakdownDialogBox>().ShowRollover(eventTransactions, this.group, true);
  }

  public void ShowToolTipTransactions()
  {
    this.mTransactions = Game.instance.player.team.financeController.finance.transactionHistory.GetTransactionsOfGroup(this.group, TransactionHistory.TimeOption.AllTime);
    if (this.mTransactions.Count <= 0)
      return;
    UIManager.instance.dialogBoxManager.GetDialog<FinanceBreakdownDialogBox>().ShowRollover(this.mTransactions, this.group, false);
  }

  public void HideTooltip()
  {
    UIManager.instance.dialogBoxManager.GetDialog<FinanceBreakdownDialogBox>().Hide();
  }
}
