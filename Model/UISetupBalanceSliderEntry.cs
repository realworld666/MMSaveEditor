// Decompiled with JetBrains decompiler
// Type: UISetupBalanceSliderEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISetupBalanceSliderEntry : MonoBehaviour
{
  public string reccomendationAnimatorTrigger = "AnimateBalanceEntry";
  public float reccomendationAnimatorTimeSeconds = 2.2f;
  public string smileyAnimatorTrigger = "PlayIntro";
  private UISetupBalanceSliderEntry.AnimationState mLastAnimatorState = UISetupBalanceSliderEntry.AnimationState.Idle;
  public UISetupBalanceSliderEntry.balanceEntryType setupBalanceEntry;
  public TextMeshProUGUI titleText;
  public TextMeshProUGUI rightText;
  public TextMeshProUGUI leftText;
  public Slider targetSliderValue;
  public Slider previousSliderValue;
  public Slider stintHoverSliderValue;
  public GameObject warningSymbol;
  public GameObject tooltipContainer;
  public SetupKnowledgeFeedbackEntry feedbackEntry;
  public Image mechanicKnowledge;
  public Image oldMechanicKnowledge;
  public Animator reccomendationAnimator;
  public Animator smileyAnimator;
  public GameObject feedbackSmileyContainer;
  public GameObject sliderContainer;
  private bool mIsInTargetRange;
  private float mOptimalSetupOutput;
  private float mMinRecommendedValueOffset;
  private float mMaxRecommendedValueOffset;
  private SessionSetup.SetupOpinion mSetupOpinion;
  private float mDeltaFromOptimalSetup;
  private float mOldMinOffsetFromOptimal;
  private float mOldMaxOffsetFromOptimal;
  private float mDeltaToMin;
  private float mDeltaToMax;
  private float mAnimReccomendationTimeLeft;
  private float mNormalisedTimeLeft;
  private bool mAnimateDriverFeedbackSmiley;
  private bool mAnimateMechanicReccomendation;
  private bool mAnimatorFinished;
  private float mCurrentMinNormalised;
  private float mCurrentMaxNormalised;

  public UISetupBalanceSliderEntry.AnimationState animationState
  {
    get
    {
      return this.mLastAnimatorState;
    }
  }

  public bool isAnimatorFinished
  {
    get
    {
      return this.mAnimatorFinished;
    }
  }

  public void OnStart()
  {
    this.targetSliderValue.minValue = -1f;
    this.targetSliderValue.maxValue = 1f;
    this.previousSliderValue.minValue = -1f;
    this.previousSliderValue.maxValue = 1f;
    this.stintHoverSliderValue.minValue = -1f;
    this.stintHoverSliderValue.maxValue = 1f;
  }

  public void Update()
  {
    if ((double) this.mAnimReccomendationTimeLeft <= 0.0)
      return;
    this.mNormalisedTimeLeft = Mathf.Clamp01(this.mAnimReccomendationTimeLeft / this.reccomendationAnimatorTimeSeconds);
    this.SetFillBarKnowledge(UISetupBalanceSliderEntry.FillBarType.currentReccomendation, this.mMinRecommendedValueOffset + this.mNormalisedTimeLeft * this.mDeltaToMin, this.mMaxRecommendedValueOffset + this.mNormalisedTimeLeft * this.mDeltaToMax);
    this.mAnimReccomendationTimeLeft -= GameTimer.deltaTime;
  }

  public void SetupSlider(RacingVehicle inVehicle, float inHoverStintOutput, SessionSetup.SetupOpinion inHoverSetupOpinion)
  {
    this.SetLabels();
    this.SetDefaultActiveGameObjects(false);
    this.mLastAnimatorState = UISetupBalanceSliderEntry.AnimationState.Idle;
    this.mSetupOpinion = inHoverSetupOpinion;
    this.SetOpinion(this.mSetupOpinion);
    this.PreviewSetupFromRollover(true, inHoverStintOutput);
  }

  public void SetupSlider(RacingVehicle inVehicle, float inOptimalOutput, float inMinVisualRangeOffset, float inMaxVisualRangeOffset, SessionSetup.SetupOpinion inOpinion, float inSetupDelta, bool inTooltipsAndWarnings = true)
  {
    this.SetLabels();
    this.SetDefaultActiveGameObjects(inTooltipsAndWarnings);
    this.mOptimalSetupOutput = inOptimalOutput;
    this.mMinRecommendedValueOffset = inMinVisualRangeOffset;
    this.mMaxRecommendedValueOffset = inMaxVisualRangeOffset;
    this.mSetupOpinion = inOpinion;
    this.mDeltaFromOptimalSetup = inSetupDelta;
    this.mLastAnimatorState = UISetupBalanceSliderEntry.AnimationState.Idle;
  }

  private void SetDefaultActiveGameObjects(bool inTooltipsAndWarnings)
  {
    GameUtility.SetActive(this.targetSliderValue.gameObject, false);
    GameUtility.SetActive(this.previousSliderValue.gameObject, false);
    GameUtility.SetActive(this.stintHoverSliderValue.gameObject, false);
    GameUtility.SetActive(this.mechanicKnowledge.gameObject, false);
    GameUtility.SetActive(this.oldMechanicKnowledge.gameObject, false);
    GameUtility.SetActive(this.feedbackSmileyContainer, true);
    GameUtility.SetActive(this.sliderContainer, true);
    GameUtility.SetActive(this.warningSymbol, inTooltipsAndWarnings);
    GameUtility.SetActive(this.tooltipContainer, inTooltipsAndWarnings);
  }

  public void SetSetupOutput(float inPreviousOutput, float inTargetOutput)
  {
    GameUtility.SetSliderAmountIfDifferent(this.previousSliderValue, Mathf.Clamp(inPreviousOutput, -1f, 1f), 1000f);
    GameUtility.SetActive(this.previousSliderValue.gameObject, true);
    GameUtility.SetSliderAmountIfDifferent(this.targetSliderValue, Mathf.Clamp(inTargetOutput, -1f, 1f), 1000f);
    GameUtility.SetActive(this.targetSliderValue.gameObject, true);
    this.RefreshWarningIndicator();
  }

  public void SetSetupOutput(float inPreviousOutput, float inTargetOutput, float inStintHoverOutput)
  {
    GameUtility.SetSliderAmountIfDifferent(this.previousSliderValue, Mathf.Clamp(inPreviousOutput, -1f, 1f), 1000f);
    GameUtility.SetActive(this.previousSliderValue.gameObject, true);
    GameUtility.SetSliderAmountIfDifferent(this.targetSliderValue, Mathf.Clamp(inTargetOutput, -1f, 1f), 1000f);
    GameUtility.SetActive(this.targetSliderValue.gameObject, true);
    GameUtility.SetSliderAmountIfDifferent(this.stintHoverSliderValue, Mathf.Clamp(inStintHoverOutput, -1f, 1f), 1000f);
    GameUtility.SetActive(this.stintHoverSliderValue.gameObject, true);
    this.RefreshWarningIndicator();
  }

  private void RefreshWarningIndicator()
  {
    this.mIsInTargetRange = (double) this.targetSliderValue.normalizedValue > (double) this.mCurrentMinNormalised && (double) this.targetSliderValue.normalizedValue < (double) this.mCurrentMaxNormalised;
    GameUtility.SetActive(this.warningSymbol, !this.mIsInTargetRange && this.tooltipContainer.gameObject.activeInHierarchy);
  }

  private void SetLabels()
  {
    switch (this.setupBalanceEntry)
    {
      case UISetupBalanceSliderEntry.balanceEntryType.Aero:
        if (Game.instance.sessionManager.championship.series == Championship.Series.GTSeries)
        {
          this.titleText.text = Localisation.LocaliseID("PSG_10001287", (GameObject) null);
          this.leftText.text = Localisation.LocaliseID("PSG_10011995", (GameObject) null);
          this.rightText.text = Localisation.LocaliseID("PSG_10011994", (GameObject) null);
          break;
        }
        this.titleText.text = Localisation.LocaliseID("PSG_10009964", (GameObject) null);
        this.leftText.text = Localisation.LocaliseID("PSG_10001439", (GameObject) null);
        this.rightText.text = Localisation.LocaliseID("PSG_10001437", (GameObject) null);
        break;
      case UISetupBalanceSliderEntry.balanceEntryType.Speed:
        this.titleText.text = Localisation.LocaliseID("PSG_10005771", (GameObject) null);
        this.leftText.text = Localisation.LocaliseID("PSG_10002085", (GameObject) null);
        this.rightText.text = Localisation.LocaliseID("PSG_10002084", (GameObject) null);
        break;
      case UISetupBalanceSliderEntry.balanceEntryType.Handling:
        this.titleText.text = Localisation.LocaliseID("PSG_10005772", (GameObject) null);
        this.leftText.text = Localisation.LocaliseID("PSG_10001642", (GameObject) null);
        this.rightText.text = Localisation.LocaliseID("PSG_10001643", (GameObject) null);
        break;
    }
  }

  public void SetupEntryOnCurrentKnowledge()
  {
    this.SetFillBarKnowledge(UISetupBalanceSliderEntry.FillBarType.currentReccomendation, this.mMinRecommendedValueOffset, this.mMaxRecommendedValueOffset);
    GameUtility.SetActive(this.oldMechanicKnowledge.gameObject, false);
    this.SetOpinion(this.mSetupOpinion);
  }

  public void SetOpinion(SessionSetup.SetupOpinion inOpinion)
  {
    this.feedbackEntry.SetOpinion(inOpinion);
  }

  private void SetFillBarKnowledge(UISetupBalanceSliderEntry.FillBarType inWhichFillBar, float inMinReccomended, float inMaxReccomended)
  {
    float num1 = (float) (((double) this.mOptimalSetupOutput + 1.0) / 2.0);
    float num2 = Mathf.Clamp01(num1 - inMinReccomended);
    float num3 = Mathf.Clamp01(num1 + inMaxReccomended);
    Image mechanicKnowledge;
    switch (inWhichFillBar)
    {
      case UISetupBalanceSliderEntry.FillBarType.oldReccomendation:
        mechanicKnowledge = this.oldMechanicKnowledge;
        break;
      default:
        GameUtility.SetActive(this.mechanicKnowledge.gameObject, true);
        mechanicKnowledge = this.mechanicKnowledge;
        this.mCurrentMinNormalised = num2;
        this.mCurrentMaxNormalised = num3;
        this.RefreshWarningIndicator();
        break;
    }
    float num4 = num3 - num2;
    if ((double) num4 < 0.0)
      Debug.LogErrorFormat("Calculated a backwards fill bar for mechanic reccomendation! Max value: {0}, Min Value: {1}", new object[2]
      {
        (object) num2,
        (object) num3
      });
    float num5 = num2 + num4 / 2f;
    float width = mechanicKnowledge.rectTransform.rect.width;
    GameUtility.SetImageFillAmountIfDifferent(mechanicKnowledge, Mathf.Clamp01(num4), 1f / 512f);
    float num6 = (float) (0.5 - (double) num4 / 2.0) * width;
    mechanicKnowledge.rectTransform.localPosition = new Vector3(num6 + (num5 - 0.5f) * width, 0.0f, 0.0f);
  }

  public void StartAnimating()
  {
    if (this.mAnimateMechanicReccomendation)
      this.AnimateReccomendation();
    else if (this.mAnimateDriverFeedbackSmiley)
      this.AnimateSmiley();
    else
      this.SetAnimatorFinished();
  }

  public void GoToNextAnimationState()
  {
    switch (this.mLastAnimatorState)
    {
      case UISetupBalanceSliderEntry.AnimationState.BarChange:
        this.mAnimateMechanicReccomendation = false;
        if (this.mAnimateDriverFeedbackSmiley)
        {
          this.AnimateSmiley();
          break;
        }
        this.mLastAnimatorState = UISetupBalanceSliderEntry.AnimationState.Idle;
        this.SetAnimatorFinished();
        break;
      case UISetupBalanceSliderEntry.AnimationState.Smiley:
        this.mAnimateDriverFeedbackSmiley = false;
        this.mLastAnimatorState = UISetupBalanceSliderEntry.AnimationState.Idle;
        this.SetAnimatorFinished();
        break;
    }
  }

  private void AnimateReccomendation()
  {
    GameUtility.SetActive(this.feedbackSmileyContainer, !this.mAnimateDriverFeedbackSmiley);
    GameUtility.SetActive(this.sliderContainer, true);
    this.reccomendationAnimator.SetTrigger(this.reccomendationAnimatorTrigger);
    this.mAnimReccomendationTimeLeft = this.reccomendationAnimatorTimeSeconds;
    this.mLastAnimatorState = UISetupBalanceSliderEntry.AnimationState.BarChange;
  }

  private void AnimateSmiley()
  {
    GameUtility.SetActive(this.feedbackSmileyContainer, true);
    this.smileyAnimator.SetTrigger(this.smileyAnimatorTrigger);
    this.mLastAnimatorState = UISetupBalanceSliderEntry.AnimationState.Smiley;
  }

  private void SetAnimatorFinished()
  {
    this.mAnimatorFinished = true;
  }

  public bool PrepareToAnimate(float inOldMinOffsetFromOptimal, float inOldMaxOffsetFromOptimal, float inOldDeltaFromOptimalSetup)
  {
    this.mAnimatorFinished = false;
    this.mOldMinOffsetFromOptimal = inOldMinOffsetFromOptimal;
    this.mOldMaxOffsetFromOptimal = inOldMaxOffsetFromOptimal;
    this.mDeltaToMin = this.mOldMinOffsetFromOptimal - this.mMinRecommendedValueOffset;
    this.mDeltaToMax = this.mOldMaxOffsetFromOptimal - this.mMaxRecommendedValueOffset;
    this.mAnimateDriverFeedbackSmiley = !Mathf.Approximately(inOldDeltaFromOptimalSetup, this.mDeltaFromOptimalSetup);
    this.mAnimateMechanicReccomendation = !Mathf.Approximately(this.mOldMaxOffsetFromOptimal, this.mMaxRecommendedValueOffset) || !Mathf.Approximately(this.mOldMinOffsetFromOptimal, this.mMinRecommendedValueOffset);
    if (this.mAnimateMechanicReccomendation)
    {
      this.SetFillBarKnowledge(UISetupBalanceSliderEntry.FillBarType.currentReccomendation, inOldMinOffsetFromOptimal, inOldMaxOffsetFromOptimal);
      this.SetFillBarKnowledge(UISetupBalanceSliderEntry.FillBarType.oldReccomendation, inOldMinOffsetFromOptimal, inOldMaxOffsetFromOptimal);
    }
    else
    {
      this.SetFillBarKnowledge(UISetupBalanceSliderEntry.FillBarType.currentReccomendation, this.mMinRecommendedValueOffset, this.mMaxRecommendedValueOffset);
      GameUtility.SetActive(this.oldMechanicKnowledge.gameObject, false);
    }
    GameUtility.SetActive(this.oldMechanicKnowledge.gameObject, this.mAnimateMechanicReccomendation);
    GameUtility.SetActive(this.sliderContainer, !this.mAnimateMechanicReccomendation);
    this.SetOpinion(this.mSetupOpinion);
    GameUtility.SetActive(this.feedbackSmileyContainer, !this.mAnimateDriverFeedbackSmiley && !this.mAnimateMechanicReccomendation);
    return this.mAnimateDriverFeedbackSmiley;
  }

  public void PreviewSetupFromRollover(bool inIsActive, float inSliderValue = 0.0f)
  {
    GameUtility.SetActive(this.stintHoverSliderValue.gameObject, inIsActive);
    GameUtility.SetSliderAmountIfDifferent(this.stintHoverSliderValue, inSliderValue, 1000f);
  }

  public enum balanceEntryType
  {
    Aero,
    Speed,
    Handling,
    Count,
  }

  public enum AnimationState
  {
    BarChange,
    Smiley,
    Idle,
  }

  private enum FillBarType
  {
    oldReccomendation,
    currentReccomendation,
  }
}
