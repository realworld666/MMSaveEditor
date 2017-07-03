// Decompiled with JetBrains decompiler
// Type: FinancePopupWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class FinancePopupWidget : UIDialogBox
{
  public FinancePopupWidget.AnimationState state;
  public GameObject chairmanCancelContainer;
  public GameObject chairmanInfoContainer;
  public Animator animator;
  public FinanceCostsWidget costsWidget;
  public GameObject costPerRaceChangeContainer;
  public TextMeshProUGUI oldCostPerRace;
  public TextMeshProUGUI newCostPerRace;
  public TextMeshProUGUI deltaCostPerRace;
  public CanvasGroup canvasGroup;
  private Action mProcessTransaction;
  private Action mCancelTransaction;
  private Finance mFinance;
  private float mTimer;
  private scSoundContainer mSoundContainer;

  public void Confirm()
  {
    this.state = FinancePopupWidget.AnimationState.AnimateAndContinue;
    this.mTimer = 0.0f;
  }

  public void Close()
  {
    this.canvasGroup.interactable = false;
    this.animator.SetTrigger(AnimationHashes.Close);
  }

  public void CloseAndContinue()
  {
    UIManager.instance.dialogBoxManager.HideAll();
    App.instance.StartCoroutine(this.ProcessAction());
  }

  public void Open(Finance inFinance, Action inProcessAction, Action inCancelTransaction, FinancePopupWidget.TransactionState inState, params Transaction[] inTransaction)
  {
    this.mFinance = inFinance;
    this.mProcessTransaction = inProcessAction;
    this.mCancelTransaction = inCancelTransaction;
    this.costsWidget.Open(this.mFinance, inTransaction);
    UIManager.instance.dialogBoxManager.Show((UIDialogBox) this);
    this.animator.SetTrigger(AnimationHashes.Open);
    this.state = FinancePopupWidget.AnimationState.Open;
    GameUtility.SetActive(this.costsWidget.confirm.gameObject, inState != FinancePopupWidget.TransactionState.NoFundsForTransactions);
    GameUtility.SetActive(this.chairmanCancelContainer, inState == FinancePopupWidget.TransactionState.NoFundsForTransactions);
    GameUtility.SetActive(this.chairmanInfoContainer, inState != FinancePopupWidget.TransactionState.NoFundsForTransactions && Game.instance.player.team.isCreatedAndManagedByPlayerFirstYear() && this.costsWidget.totalBudgetChange < 0L);
    if (this.chairmanCancelContainer.activeSelf)
      GameUtility.SetActive(this.costPerRaceChangeContainer, false);
    this.canvasGroup.interactable = true;
  }

  public void ShowCostPerRace(long inOld, long inNew)
  {
    this.oldCostPerRace.text = GameUtility.GetCurrencyString(inOld, 0);
    this.newCostPerRace.text = GameUtility.GetCurrencyString(inNew, 0);
    long inValue = inNew - inOld;
    this.deltaCostPerRace.text = string.Format("({0})", (object) GameUtility.GetCurrencyStringWithSign(inValue, 0));
    this.deltaCostPerRace.color = GameUtility.GetCurrencyColor(inValue);
    GameUtility.SetActive(this.costPerRaceChangeContainer, true);
  }

  private void Update()
  {
    if (this.state != FinancePopupWidget.AnimationState.AnimateAndContinue)
      return;
    this.ConfirmAnimation();
  }

  private void ConfirmAnimation()
  {
    this.mTimer += GameTimer.deltaTime;
    long inValue1 = MathsUtility.Lerp(this.costsWidget.totalBudgetChange, 0L, this.mTimer);
    long inValue2 = this.mFinance.currentBudget + (this.costsWidget.totalBudgetChange - inValue1);
    if (this.costsWidget.totalBudgetChange > 0L)
      scSoundManager.CheckPlaySound(SoundID.Button_Money_Get_Loop, ref this.mSoundContainer, 0.0f);
    else
      scSoundManager.CheckPlaySound(SoundID.Button_Money_Spend_Loop, ref this.mSoundContainer, 0.0f);
    this.costsWidget.availableFunds.text = GameUtility.GetCurrencyString(inValue2, 0);
    this.costsWidget.budgetBacking.color = GameUtility.GetCurrencyBackingColor(inValue2);
    for (int index = 0; index < this.costsWidget.totalSpend.Length; ++index)
      this.costsWidget.totalSpend[index].text = GameUtility.GetCurrencyString(inValue1, 0);
    if ((double) this.mTimer <= 1.0)
      return;
    this.Close();
    scSoundManager.CheckStopSound(ref this.mSoundContainer);
    this.state = FinancePopupWidget.AnimationState.CloseAndContinue;
    this.mTimer = 0.0f;
  }

  [DebuggerHidden]
  private IEnumerator ProcessAction()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new FinancePopupWidget.\u003CProcessAction\u003Ec__Iterator12()
    {
      \u003C\u003Ef__this = this
    };
  }

  public enum AnimationState
  {
    None,
    Open,
    AnimateAndContinue,
    Close,
    CloseAndContinue,
  }

  public enum TransactionState
  {
    TransactionAccepted,
    NoFundsForTransactions,
  }
}
