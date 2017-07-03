// Decompiled with JetBrains decompiler
// Type: UIContractBuyOutClauseSettingsEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIContractBuyOutClauseSettingsEntry : UIContractSettingsEntry
{
  public TextMeshProUGUI buyOutClause;
  public Toggle teamPaysAllToggle;
  public Toggle evenSplitToggle;
  public Toggle personPaysAllToggle;
  private Toggle mPreviousToggle;
  private int mContractBuyOutAmount;
  private int amountForPlayerToPay;
  private int amountForTargetToPay;

  public override void Reset(ContractPerson inContract)
  {
    this.amountForPlayerToPay = 0;
    this.amountForTargetToPay = 0;
  }

  public override void Setup(Contract inContract, Contract inDraftContract)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    UIContractBuyOutClauseSettingsEntry.\u003CSetup\u003Ec__AnonStorey79 setupCAnonStorey79 = new UIContractBuyOutClauseSettingsEntry.\u003CSetup\u003Ec__AnonStorey79();
    // ISSUE: reference to a compiler-generated field
    setupCAnonStorey79.inDraftContract = inDraftContract;
    // ISSUE: reference to a compiler-generated field
    setupCAnonStorey79.\u003C\u003Ef__this = this;
    this.mContractBuyOutAmount = inContract.GetContractTerminationCost();
    this.buyOutClause.text = GameUtility.GetCurrencyString((long) this.mContractBuyOutAmount, 0);
    this.teamPaysAllToggle.onValueChanged.RemoveAllListeners();
    this.evenSplitToggle.onValueChanged.RemoveAllListeners();
    this.personPaysAllToggle.onValueChanged.RemoveAllListeners();
    this.teamPaysAllToggle.group.allowSwitchOff = true;
    this.teamPaysAllToggle.isOn = false;
    this.evenSplitToggle.isOn = false;
    this.personPaysAllToggle.isOn = false;
    this.mPreviousToggle = (Toggle) null;
    this.teamPaysAllToggle.group.allowSwitchOff = false;
    // ISSUE: reference to a compiler-generated method
    this.teamPaysAllToggle.onValueChanged.AddListener(new UnityAction<bool>(setupCAnonStorey79.\u003C\u003Em__14B));
    // ISSUE: reference to a compiler-generated method
    this.evenSplitToggle.onValueChanged.AddListener(new UnityAction<bool>(setupCAnonStorey79.\u003C\u003Em__14C));
    // ISSUE: reference to a compiler-generated method
    this.personPaysAllToggle.onValueChanged.AddListener(new UnityAction<bool>(setupCAnonStorey79.\u003C\u003Em__14D));
  }

  public override void SetupImportanceLabel(ContractPerson inContract, TextMeshProUGUI inLabel)
  {
    if (this.TrySetImportanceLabelUnknown(inContract, inLabel))
      return;
    UIContractSettingsEntry.ImportanceLabelType inImportanceType = UIContractSettingsEntry.ImportanceLabelType.Low;
    ContractDesiredValuesHelper desiredContractValues = inContract.person.contractManager.contractEvaluation.desiredContractValues;
    if (desiredContractValues.desiredBuyoutSplit == ContractPerson.BuyoutClauseSplit.PersonPaysAll)
      inImportanceType = UIContractSettingsEntry.ImportanceLabelType.Low;
    else if (desiredContractValues.desiredBuyoutSplit == ContractPerson.BuyoutClauseSplit.EvenSplit)
      inImportanceType = UIContractSettingsEntry.ImportanceLabelType.Medium;
    else if (desiredContractValues.desiredBuyoutSplit == ContractPerson.BuyoutClauseSplit.TeamPaysAll)
      inImportanceType = UIContractSettingsEntry.ImportanceLabelType.High;
    inLabel.text = Localisation.LocaliseEnum((Enum) desiredContractValues.desiredBuyoutSplit);
    this.SetupImportanceLabelForValue(inImportanceType, inLabel);
  }

  public override void PopulateWithPreviousProposal(Contract inDraftProposal)
  {
    ContractPerson contractPerson = (ContractPerson) inDraftProposal;
    this.amountForPlayerToPay = contractPerson.amountForContractorToPay;
    this.amountForTargetToPay = contractPerson.amountForTargetToPay;
    if (contractPerson.buyoutSplit == ContractPerson.BuyoutClauseSplit.TeamPaysAll)
      this.teamPaysAllToggle.isOn = true;
    else if (contractPerson.buyoutSplit == ContractPerson.BuyoutClauseSplit.PersonPaysAll)
      this.personPaysAllToggle.isOn = true;
    else
      this.evenSplitToggle.isOn = true;
  }

  public override void Revert()
  {
    this.teamPaysAllToggle.isOn = false;
    this.evenSplitToggle.isOn = false;
    this.personPaysAllToggle.isOn = false;
    this.mPreviousToggle.isOn = true;
  }

  private void OnValueChanged(Contract inContract)
  {
    ContractPerson contractPerson = (ContractPerson) inContract;
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.teamPaysAllToggle.isOn)
    {
      contractPerson.buyoutSplit = ContractPerson.BuyoutClauseSplit.TeamPaysAll;
      this.amountForPlayerToPay = this.mContractBuyOutAmount;
      this.amountForTargetToPay = 0;
    }
    else if (this.evenSplitToggle.isOn)
    {
      contractPerson.buyoutSplit = ContractPerson.BuyoutClauseSplit.EvenSplit;
      this.amountForPlayerToPay = Mathf.RoundToInt((float) this.mContractBuyOutAmount * 0.5f);
      this.amountForTargetToPay = Mathf.RoundToInt((float) this.mContractBuyOutAmount * 0.5f);
    }
    else if ((bool) ((UnityEngine.Object) this.personPaysAllToggle))
    {
      contractPerson.buyoutSplit = ContractPerson.BuyoutClauseSplit.PersonPaysAll;
      this.amountForPlayerToPay = 0;
      this.amountForTargetToPay = this.mContractBuyOutAmount;
    }
    this.UpdateContractInfo(inContract);
  }

  public override bool HaveTheSettingsChanged()
  {
    return this.teamPaysAllToggle.isOn && (UnityEngine.Object) this.mPreviousToggle != (UnityEngine.Object) this.teamPaysAllToggle || this.evenSplitToggle.isOn && (UnityEngine.Object) this.mPreviousToggle != (UnityEngine.Object) this.evenSplitToggle || this.personPaysAllToggle.isOn && (UnityEngine.Object) this.mPreviousToggle != (UnityEngine.Object) this.personPaysAllToggle;
  }

  public override ContractEvaluationPerson.ReactionType GetScore(Contract inContract)
  {
    return ((ContractPerson) inContract).person.contractManager.contractEvaluation.GetContractBuyOutClauseReaction();
  }

  public override void SetPrevious()
  {
    if (this.teamPaysAllToggle.isOn)
      this.mPreviousToggle = this.teamPaysAllToggle;
    else if (this.evenSplitToggle.isOn)
    {
      this.mPreviousToggle = this.evenSplitToggle;
    }
    else
    {
      if (!this.personPaysAllToggle.isOn)
        return;
      this.mPreviousToggle = this.personPaysAllToggle;
    }
  }

  public override void UpdateContractInfo(Contract inContract)
  {
    ContractPerson contractPerson = (ContractPerson) inContract;
    contractPerson.amountForContractorToPay = this.amountForPlayerToPay;
    contractPerson.amountForTargetToPay = this.amountForTargetToPay;
  }

  public override bool AreSettingsChosen()
  {
    return this.teamPaysAllToggle.isOn || this.evenSplitToggle.isOn || this.personPaysAllToggle.isOn;
  }
}
