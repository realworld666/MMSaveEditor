// Decompiled with JetBrains decompiler
// Type: TransactionEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TransactionEntry : MonoBehaviour
{
  private Transaction.Group mGroup = Transaction.Group.BackstoryFinancial;
  private List<Transaction> mTransactions = new List<Transaction>();
  public TextMeshProUGUI title;
  public TextMeshProUGUI amount;
  public Button tooltip;

  public void SetData(string inTitle, long inAmount, params Transaction[] inTransactions)
  {
    if ((Object) this.tooltip != (Object) null)
    {
      if (inTransactions.Length > 0)
      {
        this.mGroup = inTransactions[0].group;
        this.tooltip.interactable = true;
      }
      else
        this.tooltip.interactable = false;
      this.mTransactions = new List<Transaction>((IEnumerable<Transaction>) inTransactions);
    }
    this.title.text = inTitle;
    this.amount.text = GameUtility.GetCurrencyStringWithSign(inAmount, 0);
    this.amount.color = GameUtility.GetCurrencyColor(inAmount);
  }

  public void ShowToolTipTransactions()
  {
    if (this.mTransactions.Count <= 0)
      return;
    UIManager.instance.dialogBoxManager.GetDialog<FinanceBreakdownDialogBox>().ShowRollover(this.mTransactions, this.mGroup, false);
  }

  public void HideTooltip()
  {
    UIManager.instance.dialogBoxManager.GetDialog<FinanceBreakdownDialogBox>().Hide();
  }

  private void OnDisable()
  {
    this.HideTooltip();
  }
}
