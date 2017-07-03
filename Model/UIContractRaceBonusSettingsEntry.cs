// Decompiled with JetBrains decompiler
// Type: UIContractRaceBonusSettingsEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIContractRaceBonusSettingsEntry : UIContractSettingsEntry
{
  private float mMaxBonus = 500000f;
  public int stepsPosition = 1;
  public int stepsValue = 1;
  private int mCurrentPositionStep = 1;
  private int mPreviousStepPositionValue = 1;
  private float mScalar = 1000f;
  public Button leftButton;
  public Button rightButton;
  public TextMeshProUGUI targetLabel;
  public Button leftButtonValue;
  public Button rightButtonValue;
  public TextMeshProUGUI wageAmountLabel;
  private int mCurrentValueStep;
  private int mPreviousStepValue;

  public override void Reset(ContractPerson inContract)
  {
    this.mCurrentPositionStep = this.stepsPosition;
    this.mCurrentValueStep = 0;
    this.mPreviousStepPositionValue = 1;
    this.mPreviousStepValue = 0;
    this.wageAmountLabel.text = GameUtility.GetCurrencyString((long) Mathf.RoundToInt((float) this.mCurrentValueStep * this.mScalar), 0);
    this.targetLabel.text = this.GetTargetPosition(this.mCurrentPositionStep);
    this.UpdateButtonsState();
  }

  public override void SetDefaultValue(ContractPerson inContract)
  {
    this.mCurrentPositionStep = this.stepsPosition;
    this.mCurrentValueStep = 1;
    this.mPreviousStepPositionValue = 1;
    this.mPreviousStepValue = 1;
    this.wageAmountLabel.text = GameUtility.GetCurrencyString((long) Mathf.RoundToInt((float) this.mCurrentValueStep * this.mScalar), 0);
    this.targetLabel.text = this.GetTargetPosition(this.mCurrentPositionStep);
    this.UpdateButtonsState();
  }

  public override void Setup(Contract inContract, Contract inDraftContract)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    UIContractRaceBonusSettingsEntry.\u003CSetup\u003Ec__AnonStorey7C setupCAnonStorey7C = new UIContractRaceBonusSettingsEntry.\u003CSetup\u003Ec__AnonStorey7C();
    // ISSUE: reference to a compiler-generated field
    setupCAnonStorey7C.inDraftContract = inDraftContract;
    // ISSUE: reference to a compiler-generated field
    setupCAnonStorey7C.\u003C\u003Ef__this = this;
    this.mMaxBonus = ((ContractPerson) inContract).person.contractManager.contractEvaluation.desiredContractValues.maximumOfferableRaceBonus;
    this.mScalar = this.mMaxBonus / (float) this.stepsValue;
    this.mCurrentPositionStep = this.stepsPosition;
    this.mCurrentValueStep = 1;
    this.leftButton.onClick.RemoveAllListeners();
    // ISSUE: reference to a compiler-generated method
    this.leftButton.onClick.AddListener(new UnityAction(setupCAnonStorey7C.\u003C\u003Em__155));
    this.rightButton.onClick.RemoveAllListeners();
    // ISSUE: reference to a compiler-generated method
    this.rightButton.onClick.AddListener(new UnityAction(setupCAnonStorey7C.\u003C\u003Em__156));
    this.mPreviousStepPositionValue = this.mCurrentPositionStep;
    this.mPreviousStepValue = this.mCurrentValueStep;
    this.leftButtonValue.onClick.RemoveAllListeners();
    // ISSUE: reference to a compiler-generated method
    this.leftButtonValue.onClick.AddListener(new UnityAction(setupCAnonStorey7C.\u003C\u003Em__157));
    this.rightButtonValue.onClick.RemoveAllListeners();
    // ISSUE: reference to a compiler-generated method
    this.rightButtonValue.onClick.AddListener(new UnityAction(setupCAnonStorey7C.\u003C\u003Em__158));
    this.wageAmountLabel.text = GameUtility.GetCurrencyString((long) Mathf.RoundToInt((float) this.mCurrentValueStep * this.mScalar), 0);
    this.targetLabel.text = this.GetTargetPosition(this.mCurrentPositionStep);
    this.UpdateButtonsState();
  }

  public override void SetupImportanceLabel(ContractPerson inContract, TextMeshProUGUI inLabel)
  {
    if (this.TrySetImportanceLabelUnknown(inContract, inLabel))
      return;
    UIContractSettingsEntry.ImportanceLabelType inImportanceType = UIContractSettingsEntry.ImportanceLabelType.Low;
    if (inContract.person.contractManager.contractEvaluation.desiredContractValues.wantsRaceBonus)
    {
      if ((double) inContract.person.GetStats().GetAbility() >= 3.0)
      {
        inLabel.text = Localisation.LocaliseID("PSG_10009272", (GameObject) null);
        inImportanceType = UIContractSettingsEntry.ImportanceLabelType.High;
      }
      else
      {
        inLabel.text = Localisation.LocaliseID("PSG_10009273", (GameObject) null);
        inImportanceType = UIContractSettingsEntry.ImportanceLabelType.Medium;
      }
    }
    else
      inLabel.text = Localisation.LocaliseID("PSG_10009274", (GameObject) null);
    this.SetupImportanceLabelForValue(inImportanceType, inLabel);
  }

  public override void PopulateWithPreviousProposal(Contract inDraftProposal)
  {
    ContractPerson contractPerson = (ContractPerson) inDraftProposal;
    this.mMaxBonus = contractPerson.person.contractManager.contractEvaluation.desiredContractValues.maximumOfferableRaceBonus;
    this.mScalar = this.mMaxBonus / (float) this.stepsValue;
    this.mCurrentValueStep = Mathf.RoundToInt((float) contractPerson.raceBonus / this.mScalar);
    this.mCurrentPositionStep = contractPerson.raceBonusTargetPosition;
    this.mPreviousStepPositionValue = this.mCurrentPositionStep;
    this.mPreviousStepValue = this.mCurrentValueStep;
    this.wageAmountLabel.text = GameUtility.GetCurrencyString((long) Mathf.RoundToInt((float) this.mCurrentValueStep * this.mScalar), 0);
    this.targetLabel.text = this.GetTargetPosition(this.mCurrentPositionStep);
    this.UpdateButtonsState();
  }

  public override void Revert()
  {
    this.mCurrentPositionStep = this.mPreviousStepPositionValue;
    this.mCurrentValueStep = this.mPreviousStepValue;
    this.UpdateButtonsState();
  }

  private void OnLeftButtonPosition(Contract inContract)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mCurrentPositionStep = Mathf.Max(1, this.mCurrentPositionStep - 1);
    this.targetLabel.text = this.GetTargetPosition(this.mCurrentPositionStep);
    this.UpdateContractInfo(inContract);
    this.UpdateButtonsState();
  }

  private void OnLeftButtonValue(Contract inContract)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mCurrentValueStep = Mathf.Max(1, this.mCurrentValueStep - 1);
    this.wageAmountLabel.text = GameUtility.GetCurrencyString((long) Mathf.RoundToInt((float) this.mCurrentValueStep * this.mScalar), 0);
    this.UpdateContractInfo(inContract);
    this.UpdateButtonsState();
  }

  private void OnRightButtonPosition(Contract inContract)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mCurrentPositionStep = Mathf.Min(this.stepsPosition, this.mCurrentPositionStep + 1);
    this.targetLabel.text = this.GetTargetPosition(this.mCurrentPositionStep);
    this.UpdateContractInfo(inContract);
    this.UpdateButtonsState();
  }

  private void OnRightButtonValue(Contract inContract)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mCurrentValueStep = Mathf.Min(this.stepsValue, this.mCurrentValueStep + 1);
    this.wageAmountLabel.text = GameUtility.GetCurrencyString((long) Mathf.RoundToInt((float) this.mCurrentValueStep * this.mScalar), 0);
    this.UpdateContractInfo(inContract);
    this.UpdateButtonsState();
  }

  private void UpdateButtonsState()
  {
    this.rightButton.interactable = this.mCurrentPositionStep < this.stepsPosition;
    this.leftButton.interactable = this.mCurrentPositionStep > 1;
    this.rightButtonValue.interactable = this.mCurrentValueStep < this.stepsValue;
    this.leftButtonValue.interactable = this.mCurrentValueStep > 1;
  }

  private string GetTargetPosition(int inValue)
  {
    return GameUtility.FormatForPositionOrAbove(inValue, (string) null);
  }

  public override bool HaveTheSettingsChanged()
  {
    return this.mPreviousStepPositionValue != this.mCurrentPositionStep || this.mPreviousStepValue != this.mCurrentValueStep;
  }

  public override ContractEvaluationPerson.ReactionType GetScore(Contract inContract)
  {
    return ((ContractPerson) inContract).person.contractManager.contractEvaluation.GetContractRaceBonusReaction();
  }

  public override void SetPrevious()
  {
    this.mPreviousStepPositionValue = this.mCurrentPositionStep;
    this.mPreviousStepValue = this.mCurrentValueStep;
  }

  public override void UpdateContractInfo(Contract inContract)
  {
    ContractPerson contractPerson = (ContractPerson) inContract;
    contractPerson.raceBonus = Mathf.RoundToInt((float) this.mCurrentValueStep * this.mScalar);
    contractPerson.raceBonusTargetPosition = this.mCurrentPositionStep;
    contractPerson.hasRaceBonus = contractPerson.raceBonus > 0;
  }

  public override bool AreSettingsChosen()
  {
    return true;
  }
}
