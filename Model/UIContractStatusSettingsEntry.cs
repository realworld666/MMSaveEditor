// Decompiled with JetBrains decompiler
// Type: UIContractStatusSettingsEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIContractStatusSettingsEntry : UIContractSettingsEntry
{
  public Toggle numberOneDriver;
  public Toggle numberTwoDriver;
  public Toggle equalStatusDriver;
  public Toggle reserveDriver;
  private Toggle mPreviousToggle;

  public override void Reset(ContractPerson inContract)
  {
    this.numberOneDriver.group.allowSwitchOff = true;
    this.numberOneDriver.isOn = false;
    this.numberTwoDriver.isOn = false;
    this.equalStatusDriver.isOn = false;
    this.reserveDriver.isOn = false;
    this.mPreviousToggle = (Toggle) null;
    this.numberOneDriver.group.allowSwitchOff = false;
  }

  public override void Setup(Contract inContract, Contract inDraftContract)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    UIContractStatusSettingsEntry.\u003CSetup\u003Ec__AnonStorey7E setupCAnonStorey7E = new UIContractStatusSettingsEntry.\u003CSetup\u003Ec__AnonStorey7E();
    // ISSUE: reference to a compiler-generated field
    setupCAnonStorey7E.inDraftContract = inDraftContract;
    // ISSUE: reference to a compiler-generated field
    setupCAnonStorey7E.\u003C\u003Ef__this = this;
    this.SetPrevious();
    this.numberOneDriver.onValueChanged.RemoveAllListeners();
    this.numberTwoDriver.onValueChanged.RemoveAllListeners();
    this.equalStatusDriver.onValueChanged.RemoveAllListeners();
    this.reserveDriver.onValueChanged.RemoveAllListeners();
    // ISSUE: reference to a compiler-generated method
    this.numberOneDriver.onValueChanged.AddListener(new UnityAction<bool>(setupCAnonStorey7E.\u003C\u003Em__15B));
    // ISSUE: reference to a compiler-generated method
    this.numberTwoDriver.onValueChanged.AddListener(new UnityAction<bool>(setupCAnonStorey7E.\u003C\u003Em__15C));
    // ISSUE: reference to a compiler-generated method
    this.equalStatusDriver.onValueChanged.AddListener(new UnityAction<bool>(setupCAnonStorey7E.\u003C\u003Em__15D));
    // ISSUE: reference to a compiler-generated method
    this.reserveDriver.onValueChanged.AddListener(new UnityAction<bool>(setupCAnonStorey7E.\u003C\u003Em__15E));
  }

  public override void SetupImportanceLabel(ContractPerson inContract, TextMeshProUGUI inLabel)
  {
    if (this.TrySetImportanceLabelUnknown(inContract, inLabel))
      return;
    ContractDesiredValuesHelper desiredContractValues = inContract.person.contractManager.contractEvaluation.desiredContractValues;
    UIContractSettingsEntry.ImportanceLabelType inImportanceType = UIContractSettingsEntry.ImportanceLabelType.Low;
    if (desiredContractValues.desiredStatus == ContractPerson.Status.One)
    {
      inImportanceType = UIContractSettingsEntry.ImportanceLabelType.High;
      inLabel.text = Localisation.LocaliseID("PSG_10009269", (GameObject) null);
    }
    else if (desiredContractValues.desiredStatus == ContractPerson.Status.Reserve)
    {
      inImportanceType = UIContractSettingsEntry.ImportanceLabelType.Medium;
      inLabel.text = Localisation.LocaliseID("PSG_10009270", (GameObject) null);
    }
    else
      inLabel.text = Localisation.LocaliseID("PSG_10009271", (GameObject) null);
    this.SetupImportanceLabelForValue(inImportanceType, inLabel);
  }

  public override void PopulateWithPreviousProposal(Contract inDraftProposal)
  {
    switch (((ContractPerson) inDraftProposal).proposedStatus)
    {
      case ContractPerson.Status.Equal:
        this.equalStatusDriver.isOn = true;
        break;
      case ContractPerson.Status.One:
        this.numberOneDriver.isOn = true;
        break;
      case ContractPerson.Status.Two:
        this.numberTwoDriver.isOn = true;
        break;
      case ContractPerson.Status.Reserve:
        this.reserveDriver.isOn = true;
        break;
    }
    this.SetPrevious();
  }

  public override void SetPrevious()
  {
    if (this.numberOneDriver.isOn)
      this.mPreviousToggle = this.numberOneDriver;
    else if (this.numberTwoDriver.isOn)
      this.mPreviousToggle = this.numberTwoDriver;
    else if (this.equalStatusDriver.isOn)
    {
      this.mPreviousToggle = this.equalStatusDriver;
    }
    else
    {
      if (!this.reserveDriver.isOn)
        return;
      this.mPreviousToggle = this.reserveDriver;
    }
  }

  public override void Revert()
  {
    this.numberOneDriver.isOn = false;
    this.numberTwoDriver.isOn = false;
    this.equalStatusDriver.isOn = false;
    this.reserveDriver.isOn = false;
    this.mPreviousToggle.isOn = true;
  }

  public override void UpdateContractInfo(Contract inContract)
  {
    ContractPerson contractPerson = (ContractPerson) inContract;
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.numberOneDriver.isOn)
      contractPerson.SetProposedStatus(ContractPerson.Status.One);
    else if (this.numberTwoDriver.isOn)
      contractPerson.SetProposedStatus(ContractPerson.Status.Two);
    else if (this.equalStatusDriver.isOn)
    {
      contractPerson.SetProposedStatus(ContractPerson.Status.Equal);
    }
    else
    {
      if (!this.reserveDriver.isOn)
        return;
      contractPerson.SetProposedStatus(ContractPerson.Status.Reserve);
    }
  }

  public override bool HaveTheSettingsChanged()
  {
    return this.numberOneDriver.isOn && (Object) this.mPreviousToggle != (Object) this.numberOneDriver || this.numberTwoDriver.isOn && (Object) this.mPreviousToggle != (Object) this.numberTwoDriver || (this.equalStatusDriver.isOn && (Object) this.mPreviousToggle != (Object) this.equalStatusDriver || this.reserveDriver.isOn && (Object) this.mPreviousToggle != (Object) this.reserveDriver);
  }

  public override ContractEvaluationPerson.ReactionType GetScore(Contract inContract)
  {
    return ((ContractPerson) inContract).person.contractManager.contractEvaluation.GetContractStatusReaction();
  }

  public override bool AreSettingsChosen()
  {
    return this.numberOneDriver.isOn || this.numberTwoDriver.isOn || (this.equalStatusDriver.isOn || this.reserveDriver.isOn);
  }
}
