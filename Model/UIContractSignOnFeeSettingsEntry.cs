// Decompiled with JetBrains decompiler
// Type: UIContractSignOnFeeSettingsEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIContractSignOnFeeSettingsEntry : UIContractSettingsEntry
{
  private float mMaxBonus = 2500000f;
  public int steps = 1;
  private float mScalar = 1000f;
  public Button leftButton;
  public Button rightButton;
  public TextMeshProUGUI wageAmountLabel;
  private int mCurrentStep;
  private int mPreviousStepValue;

  public override void Reset(ContractPerson inContract)
  {
    this.mCurrentStep = 0;
    this.wageAmountLabel.text = GameUtility.GetCurrencyString((long) Mathf.RoundToInt((float) this.mCurrentStep * this.mScalar), 0);
    this.UpdateButtonsState();
  }

  public override void SetDefaultValue(ContractPerson inContract)
  {
    this.mCurrentStep = 1;
    this.wageAmountLabel.text = GameUtility.GetCurrencyString((long) Mathf.RoundToInt((float) this.mCurrentStep * this.mScalar), 0);
    this.UpdateButtonsState();
  }

  public override void Setup(Contract inContract, Contract inDraftContract)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    UIContractSignOnFeeSettingsEntry.\u003CSetup\u003Ec__AnonStorey7D setupCAnonStorey7D = new UIContractSignOnFeeSettingsEntry.\u003CSetup\u003Ec__AnonStorey7D();
    // ISSUE: reference to a compiler-generated field
    setupCAnonStorey7D.inDraftContract = inDraftContract;
    // ISSUE: reference to a compiler-generated field
    setupCAnonStorey7D.\u003C\u003Ef__this = this;
    this.leftButton.onClick.RemoveAllListeners();
    // ISSUE: reference to a compiler-generated method
    this.leftButton.onClick.AddListener(new UnityAction(setupCAnonStorey7D.\u003C\u003Em__159));
    this.rightButton.onClick.RemoveAllListeners();
    // ISSUE: reference to a compiler-generated method
    this.rightButton.onClick.AddListener(new UnityAction(setupCAnonStorey7D.\u003C\u003Em__15A));
    this.mMaxBonus = ((ContractPerson) inContract).person.contractManager.contractEvaluation.desiredContractValues.maximumOfferableSignOnFee;
    this.mScalar = this.mMaxBonus / (float) this.steps;
    this.wageAmountLabel.text = GameUtility.GetCurrencyString((long) Mathf.RoundToInt((float) this.mCurrentStep * this.mScalar), 0);
    this.UpdateButtonsState();
  }

  public override void SetupImportanceLabel(ContractPerson inContract, TextMeshProUGUI inLabel)
  {
    if (this.TrySetImportanceLabelUnknown(inContract, inLabel))
      return;
    UIContractSettingsEntry.ImportanceLabelType inImportanceType = UIContractSettingsEntry.ImportanceLabelType.Low;
    ContractDesiredValuesHelper desiredContractValues = inContract.person.contractManager.contractEvaluation.desiredContractValues;
    if (desiredContractValues.wantSignOnFee)
    {
      if (desiredContractValues.wageRangeType == ContractVariablesData.RangeType.RangeB || desiredContractValues.wageRangeType == ContractVariablesData.RangeType.RangeA)
      {
        inImportanceType = UIContractSettingsEntry.ImportanceLabelType.High;
        inLabel.text = Localisation.LocaliseID("PSG_10009278", (GameObject) null);
      }
      else
      {
        inImportanceType = UIContractSettingsEntry.ImportanceLabelType.Medium;
        inLabel.text = Localisation.LocaliseID("PSG_10009279", (GameObject) null);
      }
    }
    else
      inLabel.text = Localisation.LocaliseID("PSG_10009280", (GameObject) null);
    this.SetupImportanceLabelForValue(inImportanceType, inLabel);
  }

  public override void PopulateWithPreviousProposal(Contract inDraftProposal)
  {
    ContractPerson contractPerson = (ContractPerson) inDraftProposal;
    this.mMaxBonus = contractPerson.person.contractManager.contractEvaluation.desiredContractValues.maximumOfferableSignOnFee;
    this.mScalar = this.mMaxBonus / (float) this.steps;
    this.mCurrentStep = Mathf.RoundToInt((float) contractPerson.signOnFee / this.mScalar);
    this.mPreviousStepValue = this.mCurrentStep;
    this.wageAmountLabel.text = GameUtility.GetCurrencyString((long) Mathf.RoundToInt((float) this.mCurrentStep * this.mScalar), 0);
    this.UpdateButtonsState();
  }

  public override void Revert()
  {
    this.mCurrentStep = this.mPreviousStepValue;
    this.wageAmountLabel.text = GameUtility.GetCurrencyString((long) Mathf.RoundToInt((float) this.mCurrentStep * this.mScalar), 0);
    this.UpdateButtonsState();
  }

  private void OnLeftButton(Contract inContract)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mCurrentStep = Mathf.Max(1, this.mCurrentStep - 1);
    this.wageAmountLabel.text = GameUtility.GetCurrencyString((long) Mathf.RoundToInt((float) this.mCurrentStep * this.mScalar), 0);
    this.UpdateContractInfo(inContract);
    this.UpdateButtonsState();
  }

  private void OnRightButton(Contract inContract)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mCurrentStep = Mathf.Min(this.steps, this.mCurrentStep + 1);
    this.wageAmountLabel.text = GameUtility.GetCurrencyString((long) Mathf.RoundToInt((float) this.mCurrentStep * this.mScalar), 0);
    this.UpdateContractInfo(inContract);
    this.UpdateButtonsState();
  }

  private void UpdateButtonsState()
  {
    this.rightButton.interactable = this.mCurrentStep < this.steps;
    this.leftButton.interactable = this.mCurrentStep > 1;
  }

  public override bool HaveTheSettingsChanged()
  {
    return this.mPreviousStepValue != this.mCurrentStep;
  }

  public override ContractEvaluationPerson.ReactionType GetScore(Contract inContract)
  {
    return ((ContractPerson) inContract).person.contractManager.contractEvaluation.GetContractSignOnFeeReaction();
  }

  public override void SetPrevious()
  {
    this.mPreviousStepValue = this.mCurrentStep;
  }

  public override void UpdateContractInfo(Contract inContract)
  {
    ContractPerson contractPerson = (ContractPerson) inContract;
    contractPerson.signOnFee = Mathf.RoundToInt((float) this.mCurrentStep * this.mScalar);
    contractPerson.hasSignOnFee = contractPerson.signOnFee > 0;
  }

  public override bool AreSettingsChosen()
  {
    return true;
  }
}
