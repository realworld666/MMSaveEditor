// Decompiled with JetBrains decompiler
// Type: WithdrawPotEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WithdrawPotEntry : MonoBehaviour
{
  [SerializeField]
  private TextMeshProUGUI fundsAllocatedLabel;
  [SerializeField]
  private Button selectPot;
  [SerializeField]
  private Image outline;
  private BonusWithdrawWidget mParentWidget;
  private Finance mFinance;

  private void Awake()
  {
    this.selectPot.onClick.AddListener(new UnityAction(this.OnSelect));
  }

  private void OnSelect()
  {
    this.mParentWidget.Select(this);
    Color color = this.outline.color;
    color.a = 1f;
    this.outline.color = color;
    this.SetCurrencyString(this.mFinance.currentBudget + UIManager.instance.dialogBoxManager.GetDialog<BonusPopup>().GetTotal());
  }

  public void Deselect()
  {
    Color color = this.outline.color;
    color.a = 0.3f;
    this.outline.color = color;
    if (this.mFinance == null)
      return;
    this.SetCurrencyString(this.mFinance.currentBudget);
  }

  public void Setup(BonusWithdrawWidget inWidget, Finance inFinance)
  {
    this.mParentWidget = inWidget;
    this.mFinance = inFinance;
    this.SetCurrencyString(this.mFinance.currentBudget);
  }

  private void SetCurrencyString(long inCurrency)
  {
    if (inCurrency > 0L)
      this.fundsAllocatedLabel.color = UIConstants.positiveColor;
    else
      this.fundsAllocatedLabel.color = UIConstants.negativeColor;
    this.fundsAllocatedLabel.text = GameUtility.GetCurrencyString(inCurrency, 0);
  }

  public void SendTransaction()
  {
    this.mFinance.ProcessTransactions((Action) null, (Action) null, false, new Transaction(Transaction.Group.ChairmanPayments, Transaction.Type.Debit, UIManager.instance.dialogBoxManager.GetDialog<BonusPopup>().GetTotal(), Localisation.LocaliseID("PSG_10010577", (GameObject) null)));
  }
}
