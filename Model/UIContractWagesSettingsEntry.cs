// Decompiled with JetBrains decompiler
// Type: UIContractWagesSettingsEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIContractWagesSettingsEntry : UIContractSettingsEntry
{
  private float mMaxValue = 2500000f;
  private float mMinValue = 2500000f;
  public int steps = 1;
  private float mScalar = 10000f;
  public Button leftButton;
  public Button rightButton;
  public TextMeshProUGUI wageAmountLabel;
  private int mCurrentStep;
  private int mPreviousSliderValue;

  public override void Reset(ContractPerson inContract)
  {
    this.mCurrentStep = 0;
    this.UpdateWageLabel();
    this.UpdateButtonsState();
  }

  public override void Setup(Contract inContract, Contract inDraftContract)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    UIContractWagesSettingsEntry.\u003CSetup\u003Ec__AnonStorey7F setupCAnonStorey7F = new UIContractWagesSettingsEntry.\u003CSetup\u003Ec__AnonStorey7F();
    // ISSUE: reference to a compiler-generated field
    setupCAnonStorey7F.inDraftContract = inDraftContract;
    // ISSUE: reference to a compiler-generated field
    setupCAnonStorey7F.\u003C\u003Ef__this = this;
    ContractPerson contractPerson = (ContractPerson) inContract;
    this.mMinValue = contractPerson.person.contractManager.contractEvaluation.desiredContractValues.minimumOfferableWage;
    this.mMaxValue = contractPerson.person.contractManager.contractEvaluation.desiredContractValues.maximumOfferableWage;
    this.mScalar = (this.mMaxValue - this.mMinValue) / (float) this.steps;
    this.leftButton.onClick.RemoveAllListeners();
    // ISSUE: reference to a compiler-generated method
    this.leftButton.onClick.AddListener(new UnityAction(setupCAnonStorey7F.\u003C\u003Em__15F));
    this.rightButton.onClick.RemoveAllListeners();
    // ISSUE: reference to a compiler-generated method
    this.rightButton.onClick.AddListener(new UnityAction(setupCAnonStorey7F.\u003C\u003Em__160));
    this.UpdateWageLabel();
    this.UpdateButtonsState();
  }

  public override void SetupImportanceLabel(ContractPerson inContract, TextMeshProUGUI inLabel)
  {
    if (this.TrySetImportanceLabelUnknown(inContract, inLabel))
      return;
    UIContractSettingsEntry.ImportanceLabelType inImportanceType = UIContractSettingsEntry.ImportanceLabelType.Low;
    ContractDesiredValuesHelper desiredContractValues = inContract.person.contractManager.contractEvaluation.desiredContractValues;
    float minimumOfferableWage = desiredContractValues.minimumOfferableWage;
    float num1 = desiredContractValues.maximumOfferableWage - minimumOfferableWage;
    float num2 = (desiredContractValues.desiredWages - minimumOfferableWage) / num1;
    if ((double) num2 >= 0.699999988079071)
    {
      inImportanceType = UIContractSettingsEntry.ImportanceLabelType.High;
      inLabel.text = Localisation.LocaliseID("PSG_10009266", (GameObject) null);
    }
    else if ((double) num2 >= 0.400000005960464)
    {
      inImportanceType = UIContractSettingsEntry.ImportanceLabelType.Medium;
      inLabel.text = Localisation.LocaliseID("PSG_10009267", (GameObject) null);
    }
    else
      inLabel.text = Localisation.LocaliseID("PSG_10009268", (GameObject) null);
    this.SetupImportanceLabelForValue(inImportanceType, inLabel);
  }

  public override void PopulateWithPreviousProposal(Contract inDraftProposal)
  {
    this.mCurrentStep = Mathf.RoundToInt(((float) ((ContractPerson) inDraftProposal).yearlyWages - this.mMinValue) / this.mScalar);
    this.mPreviousSliderValue = this.mCurrentStep;
    this.UpdateWageLabel();
    this.UpdateButtonsState();
  }

  private void UpdateWageLabel()
  {
    this.wageAmountLabel.text = GameUtility.GetCurrencyString((long) (((int) this.mMinValue + Mathf.RoundToInt((float) this.mCurrentStep * this.mScalar)) / Game.instance.player.team.championship.eventCount), 0);
  }

  public override void Revert()
  {
    this.mCurrentStep = this.mPreviousSliderValue;
    this.UpdateButtonsState();
  }

  private void OnLeftButton(Contract inContract)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mCurrentStep = Mathf.Max(0, this.mCurrentStep - 1);
    this.UpdateWageLabel();
    this.UpdateContractInfo(inContract);
    this.UpdateButtonsState();
  }

  private void OnRightButton(Contract inContract)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mCurrentStep = Mathf.Min(this.mCurrentStep + 1, this.steps);
    this.UpdateWageLabel();
    this.UpdateContractInfo(inContract);
    this.UpdateButtonsState();
  }

  private void UpdateButtonsState()
  {
    this.rightButton.interactable = this.mCurrentStep < this.steps;
    this.leftButton.interactable = this.mCurrentStep > 0;
  }

  public override bool HaveTheSettingsChanged()
  {
    return this.mPreviousSliderValue != this.mCurrentStep;
  }

  public override ContractEvaluationPerson.ReactionType GetScore(Contract inContract)
  {
    return ((ContractPerson) inContract).person.contractManager.contractEvaluation.GetContractWageReaction();
  }

  public override void SetPrevious()
  {
    this.mPreviousSliderValue = this.mCurrentStep;
  }

  public override void UpdateContractInfo(Contract inContract)
  {
    ((ContractPerson) inContract).yearlyWages = (int) this.mMinValue + Mathf.RoundToInt((float) this.mCurrentStep * this.mScalar);
  }

  public override bool AreSettingsChosen()
  {
    return true;
  }
}
