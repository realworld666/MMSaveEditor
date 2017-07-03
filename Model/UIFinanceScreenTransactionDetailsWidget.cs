// Decompiled with JetBrains decompiler
// Type: UIFinanceScreenTransactionDetailsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIFinanceScreenTransactionDetailsWidget : MonoBehaviour
{
  public UIFinanceScreenTransactionDetailsWidget.State state = UIFinanceScreenTransactionDetailsWidget.State.Inactive;
  private List<Transaction> mTransactions = new List<Transaction>();
  public TextMeshProUGUI title;
  public TextMeshProUGUI totalFunds;
  public UIGridList list;
  public CanvasGroup group;

  public void Setup(Finance inFinance)
  {
    this.gameObject.SetActive(true);
    StringVariableParser.month = Game.instance.time.now.Month;
    this.title.text = Localisation.LocaliseID("PSG_10010408", (GameObject) null);
    this.totalFunds.text = GameUtility.GetCurrencyString(inFinance.currentBudget, 0);
    this.totalFunds.color = inFinance.currentBudget <= 0L ? UIConstants.negativeColor : UIConstants.positiveColor;
    this.list.DestroyListItems();
    this.mTransactions = inFinance.transactionHistory.GetTransactionsForMonth(Game.instance.time.now);
    int inIndex = 1;
    int count = this.mTransactions.Count;
    for (int index = 0; index < count; ++index)
    {
      if (this.mTransactions[index].group != Transaction.Group.ChairmanPayments)
      {
        this.list.CreateListItem<UIFinanceScreenTransactionInfoEntry>().Setup(this.mTransactions[index], inIndex);
        ++inIndex;
      }
    }
    this.group.alpha = 0.0f;
  }

  private void Update()
  {
    if (this.state == UIFinanceScreenTransactionDetailsWidget.State.Active)
    {
      this.group.alpha = Mathf.MoveTowards(this.group.alpha, 1f, GameTimer.deltaTime * 5f);
    }
    else
    {
      this.group.alpha = Mathf.MoveTowards(this.group.alpha, 0.0f, GameTimer.deltaTime * 5f);
      if ((double) this.group.alpha != 0.0)
        return;
      GameUtility.SetActive(this.gameObject, false);
    }
  }

  public enum State
  {
    Active,
    Inactive,
  }
}
