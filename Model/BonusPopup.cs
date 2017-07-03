// Decompiled with JetBrains decompiler
// Type: BonusPopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BonusPopup : UIDialogBox
{
  public TextMeshProUGUI[] overallCost;
  public TextMeshProUGUI availableBudget;
  public UICharacterPortrait chairmanPortrait;
  public Flag chairmanFlag;
  public TextMeshProUGUI chairmanName;
  public TextMeshProUGUI chairmanComment;
  public UIGridList list;
  public Button continueButton;
  public TextMeshProUGUI budgetHeader;
  public Image budgetBacking;
  private scSoundContainer mSoundContainer;
  private float mClosingTimer;
  private bool isClosing;
  private Action mClosingAction;

  protected override void Awake()
  {
    base.Awake();
    this.continueButton.onClick.AddListener(new UnityAction(this.OnContinueButton));
  }

  private void OnContinueButton()
  {
    if (this.isClosing)
      return;
    this.isClosing = true;
  }

  protected override void OnEnable()
  {
    base.OnEnable();
    this.CreateList();
    this.SetAssistantInfo();
    this.SetMoneyLabels();
    this.mClosingTimer = 0.0f;
    this.isClosing = false;
  }

  public void SetActionForOnClose(Action inAction)
  {
    this.mClosingAction = inAction;
  }

  private void Update()
  {
    if (!this.isClosing)
      return;
    scSoundManager.CheckPlaySound(SoundID.Button_Money_Get_Loop, ref this.mSoundContainer, 0.0f);
    this.mClosingTimer += GameTimer.deltaTime;
    long currentBudget = Game.instance.player.team.financeController.finance.currentBudget;
    long total = this.GetTotal();
    long inValue = (long) Mathf.Lerp((float) total, 0.0f, this.mClosingTimer);
    long num = (long) Mathf.Lerp((float) currentBudget, (float) (currentBudget + total), this.mClosingTimer);
    this.availableBudget.text = GameUtility.GetCurrencyString(num, 0);
    for (int index = 0; index < this.overallCost.Length; ++index)
      this.overallCost[index].text = GameUtility.GetCurrencyString(inValue, 0);
    if ((double) this.mClosingTimer > 1.0)
      scSoundManager.CheckStopSound(ref this.mSoundContainer);
    if ((double) this.mClosingTimer > 1.5)
    {
      this.isClosing = false;
      this.mClosingTimer = 0.0f;
      List<Transaction> unnallocatedTransactions = Game.instance.player.team.financeController.unnallocatedTransactions;
      for (int index = 0; index < unnallocatedTransactions.Count; ++index)
        Game.instance.player.team.financeController.finance.ProcessTransactions((Action) null, (Action) null, false, unnallocatedTransactions[index]);
      if (UIManager.instance.currentScreen is FinanceScreen)
        UIManager.instance.currentScreen.OnEnter();
      this.Hide();
      Game.instance.player.team.financeController.unnallocatedTransactions.Clear();
      if (this.mClosingAction != null)
      {
        this.mClosingAction();
        this.mClosingAction = (Action) null;
      }
    }
    this.SetBudgetColors(num);
  }

  private void CreateList()
  {
    List<Transaction> unnallocatedTransactions = Game.instance.player.team.financeController.unnallocatedTransactions;
    if (Game.instance.player.playerBackStoryType == PlayerBackStory.PlayerBackStoryType.Financial)
    {
      long num = 0;
      for (int index = 0; index < unnallocatedTransactions.Count; ++index)
      {
        Transaction transaction = unnallocatedTransactions[index];
        if (transaction.transactionType == Transaction.Type.Debit)
          num += transaction.amount;
      }
      long inAmount = num / 100L * (long) Game.instance.player.paymentsModifier;
      if (inAmount != 0L)
      {
        Transaction transaction = new Transaction(Transaction.Group.BackstoryFinancial, Transaction.Type.Credit, inAmount, Localisation.LocaliseID("PSG_10010559", (GameObject) null));
        unnallocatedTransactions.Add(transaction);
      }
    }
    for (Transaction.Group inGroup = Transaction.Group.CarParts; inGroup < Transaction.Group.Count; ++inGroup)
    {
      if (inGroup != Transaction.Group.BackstoryFinancial || Game.instance.player.playerBackStoryType == PlayerBackStory.PlayerBackStoryType.Financial)
      {
        long inAmount = 0;
        List<Transaction> transactionList = new List<Transaction>();
        for (int index = 0; index < unnallocatedTransactions.Count; ++index)
        {
          Transaction transaction = unnallocatedTransactions[index];
          if (transaction.group == inGroup)
          {
            transactionList.Add(transaction);
            inAmount += transaction.amountWithSign;
          }
        }
        TransactionEntry transactionEntry = this.list.GetOrCreateItem<TransactionEntry>((int) inGroup);
        Transaction.SetupStringVariableParserForGroup(inGroup);
        transactionEntry.SetData(Localisation.LocaliseEnum((Enum) inGroup), inAmount, transactionList.ToArray());
        StringVariableParser.subject = (Person) null;
      }
    }
  }

  private void SetMoneyLabels()
  {
    long total = this.GetTotal();
    if (UIManager.instance.currentScreen is CarDesignScreen)
      this.chairmanComment.text = Localisation.LocaliseID("PSG_10005731", (GameObject) null);
    else if (Game.instance.dilemmaSystem.gotFineTransaction)
    {
      this.chairmanComment.text = Localisation.LocaliseID("PSG_10010476", (GameObject) null);
      Game.instance.dilemmaSystem.gotFineTransaction = false;
    }
    else if (total > 0L)
      this.chairmanComment.text = Localisation.LocaliseID("PSG_10005732", (GameObject) null);
    else if (Game.instance.player.team.financeController.availableFunds + total > 0L)
      this.chairmanComment.text = Localisation.LocaliseID("PSG_10005733", (GameObject) null);
    else
      this.chairmanComment.text = Localisation.LocaliseID("PSG_10005734", (GameObject) null);
    this.availableBudget.text = GameUtility.GetCurrencyString(Game.instance.player.team.financeController.finance.currentBudget, 0);
    string currencyStringWithSign = GameUtility.GetCurrencyStringWithSign(total, 0);
    Color currencyColor = GameUtility.GetCurrencyColor(total);
    for (int index = 0; index < this.overallCost.Length; ++index)
    {
      this.overallCost[index].text = currencyStringWithSign;
      this.overallCost[index].color = currencyColor;
    }
    if (total >= 0L)
      this.budgetHeader.text = "Total Income";
    else
      this.budgetHeader.text = "Total Expenses";
    this.SetBudgetColors(Game.instance.player.team.financeController.finance.currentBudget);
  }

  private void SetBudgetColors(long inBudget)
  {
    this.budgetBacking.color = GameUtility.GetCurrencyBackingColor(inBudget);
  }

  private void SetAssistantInfo()
  {
    Person personOnJob = Game.instance.player.team.contractManager.GetPersonOnJob(Contract.Job.Chairman);
    this.chairmanPortrait.SetPortrait(personOnJob);
    this.chairmanFlag.SetNationality(personOnJob.nationality);
    this.chairmanName.text = personOnJob.name;
  }

  public long GetTotal()
  {
    long num1 = 0;
    long num2 = 0;
    List<Transaction> unnallocatedTransactions = Game.instance.player.team.financeController.unnallocatedTransactions;
    for (int index = 0; index < unnallocatedTransactions.Count; ++index)
    {
      if (unnallocatedTransactions[index].transactionType == Transaction.Type.Debit)
        num1 += unnallocatedTransactions[index].amount;
      else
        num2 += unnallocatedTransactions[index].amount;
    }
    return num2 - num1;
  }
}
