// Decompiled with JetBrains decompiler
// Type: UIContractLengthSettingsEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIContractLengthSettingsEntry : UIContractSettingsEntry
{
  public Toggle oneYear;
  public Toggle twoYear;
  public Toggle threeYear;
  public TextMeshProUGUI oneYearLabel;
  public TextMeshProUGUI twoYearsLabel;
  public TextMeshProUGUI threeYearsLabel;
  private Toggle mPreviousToggle;

  public override void Reset(ContractPerson inContract)
  {
    this.oneYear.group.allowSwitchOff = true;
    this.oneYear.isOn = false;
    this.twoYear.isOn = false;
    this.threeYear.isOn = false;
    this.mPreviousToggle = (Toggle) null;
    this.oneYear.group.allowSwitchOff = false;
  }

  public override void Setup(Contract inContract, Contract inDraftContract)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    UIContractLengthSettingsEntry.\u003CSetup\u003Ec__AnonStorey7A setupCAnonStorey7A = new UIContractLengthSettingsEntry.\u003CSetup\u003Ec__AnonStorey7A();
    // ISSUE: reference to a compiler-generated field
    setupCAnonStorey7A.inDraftContract = inDraftContract;
    // ISSUE: reference to a compiler-generated field
    setupCAnonStorey7A.\u003C\u003Ef__this = this;
    // ISSUE: reference to a compiler-generated field
    ContractPerson inDraftContract1 = setupCAnonStorey7A.inDraftContract as ContractPerson;
    int num1 = 0;
    int year = Game.instance.time.now.Year;
    if (inDraftContract1 != null && this.NeedToAddOneYear(inDraftContract1))
    {
      num1 = 12;
      ++year;
    }
    int num2 = num1 + (12 - Game.instance.time.now.Month);
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    StringVariableParser.contractNegotiationYear = year.ToString();
    StringVariableParser.contractNegotiationMonths = num2.ToString();
    this.oneYearLabel.text = num2 <= 1 ? Localisation.LocaliseID("PSG_10010471", (GameObject) null) : Localisation.LocaliseID("PSG_10010470", (GameObject) null);
    StringVariableParser.contractNegotiationYear = (year + 1).ToString();
    StringVariableParser.contractNegotiationMonths = (num2 + 12).ToString();
    this.twoYearsLabel.text = Localisation.LocaliseID("PSG_10010470", (GameObject) null);
    StringVariableParser.contractNegotiationYear = (year + 2).ToString();
    StringVariableParser.contractNegotiationMonths = (num2 + 24).ToString();
    this.threeYearsLabel.text = Localisation.LocaliseID("PSG_10010470", (GameObject) null);
    this.oneYear.onValueChanged.RemoveAllListeners();
    this.twoYear.onValueChanged.RemoveAllListeners();
    this.threeYear.onValueChanged.RemoveAllListeners();
    // ISSUE: reference to a compiler-generated method
    this.oneYear.onValueChanged.AddListener(new UnityAction<bool>(setupCAnonStorey7A.\u003C\u003Em__14E));
    // ISSUE: reference to a compiler-generated method
    this.twoYear.onValueChanged.AddListener(new UnityAction<bool>(setupCAnonStorey7A.\u003C\u003Em__14F));
    // ISSUE: reference to a compiler-generated method
    this.threeYear.onValueChanged.AddListener(new UnityAction<bool>(setupCAnonStorey7A.\u003C\u003Em__150));
    this.SetPrevious();
  }

  public override void SetupImportanceLabel(ContractPerson inContract, TextMeshProUGUI inLabel)
  {
    if (this.TrySetImportanceLabelUnknown(inContract, inLabel))
      return;
    ContractDesiredValuesHelper desiredContractValues = inContract.person.contractManager.contractEvaluation.desiredContractValues;
    UIContractSettingsEntry.ImportanceLabelType inImportanceType;
    if (desiredContractValues.desiredContractLength == ContractPerson.ContractLength.Long)
    {
      inLabel.text = Localisation.LocaliseID("PSG_10009275", (GameObject) null);
      inImportanceType = UIContractSettingsEntry.ImportanceLabelType.High;
    }
    else if (desiredContractValues.desiredContractLength == ContractPerson.ContractLength.Medium)
    {
      inLabel.text = Localisation.LocaliseID("PSG_10009276", (GameObject) null);
      inImportanceType = UIContractSettingsEntry.ImportanceLabelType.Medium;
    }
    else
    {
      inLabel.text = Localisation.LocaliseID("PSG_10009277", (GameObject) null);
      inImportanceType = UIContractSettingsEntry.ImportanceLabelType.Low;
    }
    this.SetupImportanceLabelForValue(inImportanceType, inLabel);
  }

  public override void SetupSubtitleImportanceLabel(ContractPerson inContract, TextMeshProUGUI inLabel)
  {
    if (inContract.person.CanShowStats())
    {
      GameUtility.SetActive(inLabel.gameObject, true);
      ContractDesiredValuesHelper desiredContractValues = inContract.person.contractManager.contractEvaluation.desiredContractValues;
      if (desiredContractValues.desiredContractLength == ContractPerson.ContractLength.Long)
        inLabel.text = "Long Contract";
      else if (desiredContractValues.desiredContractLength == ContractPerson.ContractLength.Medium)
        inLabel.text = "Medium Contract";
      else
        inLabel.text = "Short Contract";
    }
    else
      GameUtility.SetActive(inLabel.gameObject, false);
  }

  public override void PopulateWithPreviousProposal(Contract inDraftProposal)
  {
    ContractPerson contractPerson = (ContractPerson) inDraftProposal;
    if (contractPerson.length == ContractPerson.ContractLength.Short)
      this.oneYear.isOn = true;
    else if (contractPerson.length == ContractPerson.ContractLength.Medium)
      this.twoYear.isOn = true;
    else if (contractPerson.length == ContractPerson.ContractLength.Long)
      this.threeYear.isOn = true;
    this.SetPrevious();
  }

  public override void Revert()
  {
    this.oneYear.isOn = false;
    this.twoYear.isOn = false;
    this.threeYear.isOn = false;
    this.mPreviousToggle.isOn = true;
  }

  public override void SetPrevious()
  {
    if (this.oneYear.isOn)
      this.mPreviousToggle = this.oneYear;
    else if (this.twoYear.isOn)
    {
      this.mPreviousToggle = this.twoYear;
    }
    else
    {
      if (!this.threeYear.isOn)
        return;
      this.mPreviousToggle = this.threeYear;
    }
  }

  public override void UpdateContractInfo(Contract inContract)
  {
    ContractPerson inContract1 = (ContractPerson) inContract;
    ContractNegotiationScreen screen = UIManager.instance.GetScreen<ContractNegotiationScreen>();
    int year = Game.instance.time.now.Year;
    if (screen.employeeContractYear == ContractNegotiationScreen.ContractYear.Current)
      inContract.startDate = Game.instance.time.now;
    else if (screen.employeeContractYear == ContractNegotiationScreen.ContractYear.NextYear)
      inContract.startDate = new DateTime(year + 1, 1, 1);
    if (this.NeedToAddOneYear(inContract1))
      ++year;
    DateTime dateTime = new DateTime(year, 12, 31);
    if (this.oneYear.isOn)
    {
      inContract.endDate = dateTime;
      inContract1.length = ContractPerson.ContractLength.Short;
    }
    else if (this.twoYear.isOn)
    {
      inContract.endDate = dateTime.AddYears(1);
      inContract1.length = ContractPerson.ContractLength.Medium;
    }
    else
    {
      if (!this.threeYear.isOn)
        return;
      inContract.endDate = dateTime.AddYears(2);
      inContract1.length = ContractPerson.ContractLength.Long;
    }
  }

  public override bool HaveTheSettingsChanged()
  {
    return this.oneYear.isOn && (UnityEngine.Object) this.mPreviousToggle != (UnityEngine.Object) this.oneYear || this.twoYear.isOn && (UnityEngine.Object) this.mPreviousToggle != (UnityEngine.Object) this.twoYear || this.threeYear.isOn && (UnityEngine.Object) this.mPreviousToggle != (UnityEngine.Object) this.threeYear;
  }

  private bool NeedToAddOneYear(ContractPerson inContract)
  {
    return ((((inContract.GetTeam().championship.HasSeasonEnded() ? 1 : 0) | (App.instance.gameStateManager.currentState.type != GameState.Type.PreSeasonState ? 0 : (Game.instance.time.now.Month == 12 ? 1 : 0))) != 0 ? 1 : 0) | (inContract.GetTeam().championship.GetFirstEventDetails().hasEventEnded ? 0 : (Game.instance.time.now.Year < inContract.GetTeam().championship.GetFirstEventDetails().eventDate.Year ? 1 : 0))) != 0;
  }

  public override ContractEvaluationPerson.ReactionType GetScore(Contract inContract)
  {
    return ((ContractPerson) inContract).person.contractManager.contractEvaluation.GetContractLengthReaction();
  }

  public override bool AreSettingsChosen()
  {
    return this.oneYear.isOn || this.twoYear.isOn || this.threeYear.isOn;
  }
}
