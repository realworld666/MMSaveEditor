// Decompiled with JetBrains decompiler
// Type: FinanceCostsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FinanceCostsWidget : MonoBehaviour
{
  public Button cancel;
  public Button confirm;
  public UIGridList spendList;
  public TextMeshProUGUI[] totalSpend;
  public TextMeshProUGUI availableFunds;
  public TextMeshProUGUI budgetHeader;
  public Image budgetBacking;
  private long mTotalBudgetChange;
  private FinancePopupWidget mFinanceWidget;

  public long totalBudgetChange
  {
    get
    {
      return this.mTotalBudgetChange;
    }
  }

  private void Awake()
  {
    this.mFinanceWidget = UIManager.instance.dialogBoxManager.GetDialog<FinancePopupWidget>();
    this.confirm.onClick.AddListener((UnityAction) (() => this.mFinanceWidget.Confirm()));
    this.cancel.onClick.AddListener((UnityAction) (() => this.mFinanceWidget.Close()));
  }

  private void Update()
  {
    GameUtility.SetInteractable(this.cancel, this.mFinanceWidget.state == FinancePopupWidget.AnimationState.Open);
    GameUtility.SetInteractable(this.confirm, this.mFinanceWidget.state == FinancePopupWidget.AnimationState.Open);
  }

  public void Open(Finance inFinance, params Transaction[] inTransaction)
  {
    this.spendList.DestroyListItems();
    long num1 = 0;
    long num2 = 0;
    for (int index = 0; index < inTransaction.Length; ++index)
    {
      TransactionEntry listItem = this.spendList.CreateListItem<TransactionEntry>();
      Transaction transaction = inTransaction[index];
      listItem.SetData(inTransaction[index].name, inTransaction[index].amountWithSign, transaction);
      if (transaction.transactionType == Transaction.Type.Debit)
        num1 -= transaction.amount;
      else
        num2 += transaction.amount;
    }
    this.mTotalBudgetChange = num1 + num2;
    this.availableFunds.text = GameUtility.GetCurrencyString(inFinance.currentBudget, 0);
    string currencyStringWithSign = GameUtility.GetCurrencyStringWithSign(this.mTotalBudgetChange, 0);
    Color currencyColor = GameUtility.GetCurrencyColor(this.mTotalBudgetChange);
    for (int index = 0; index < this.totalSpend.Length; ++index)
    {
      this.totalSpend[index].text = currencyStringWithSign;
      this.totalSpend[index].color = currencyColor;
    }
    if (this.mTotalBudgetChange >= 0L)
      this.budgetHeader.text = "Total Income";
    else
      this.budgetHeader.text = "Total Expenses";
    this.budgetBacking.color = GameUtility.GetCurrencyBackingColor(inFinance.currentBudget);
  }
}
