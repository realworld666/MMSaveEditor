// Decompiled with JetBrains decompiler
// Type: UIFinanceScreenTransactionInfoEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UIFinanceScreenTransactionInfoEntry : MonoBehaviour
{
  public TextMeshProUGUI title;
  public TextMeshProUGUI number;
  public TextMeshProUGUI amount;

  public void Setup(Transaction inTransaction, int inIndex)
  {
    this.number.text = inIndex.ToString() + ".";
    this.title.text = inTransaction.group.ToString();
    this.amount.text = inTransaction.transactionType != Transaction.Type.Credit ? "- " + GameUtility.GetCurrencyString(inTransaction.amount, 0) : "+ " + GameUtility.GetCurrencyString(inTransaction.amount, 0);
    this.amount.color = inTransaction.transactionType != Transaction.Type.Credit ? UIConstants.negativeColor : UIConstants.positiveColor;
  }
}
