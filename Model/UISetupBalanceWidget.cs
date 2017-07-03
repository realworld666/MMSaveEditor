// Decompiled with JetBrains decompiler
// Type: UISetupBalanceWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISetupBalanceWidget : MonoBehaviour
{
  private PitScreen.Mode mMode = PitScreen.Mode.Pitting;
  private SessionSetup.SetupOutput mCurrentSetupOutput = new SessionSetup.SetupOutput();
  private SessionSetup.SetupOutput mTargetSetupOutput = new SessionSetup.SetupOutput();
  private SetupPerformance.OptimalSetup mOptimalSetup = new SetupPerformance.OptimalSetup();
  private UISetupBalanceWidget.animatorState mAnimatorState = UISetupBalanceWidget.animatorState.Idle;
  private float[] mAnimatorPreviousMinKnowledge = new float[3];
  private float[] mAnimatorPreviousMaxKnowledge = new float[3];
  private float mKnowledgeBarAnimatorTimeSeconds = 2.5f;
  private float mAnimationTimeLeft = 1f;
  public UISetupBalanceSliderEntry aeroSlider;
  public UISetupBalanceSliderEntry handlingSlider;
  public UISetupBalanceSliderEntry speedSlider;
  public Animator knowledgeBarAnimator;
  public Animator percentageFeedbackAnimator;
  public CarSetupKnowledgePanel animatedSetupKnowledgePanel;
  public GameObject knowledgAnimBackgroundBlock;
  public TextMeshProUGUI percentageTotalText;
  public TextMeshProUGUI percentageDeltaText;
  public GameObject percentageDeltaUpArrow;
  public GameObject percentageDeltaDownArrow;
  public GameObject percentageDivider;
  public GameObject percentageFeedbackContainer;
  private RacingVehicle mVehicle;
  private float mCurrentKnowledge;
  private float mPreviousKnowledge;
  private SetupStintData mAnimatorPreviousStint;
  private bool mAnimatePercentageFeedback;
  private bool mAnimateKnowledge;

  public SessionSetup.SetupOutput targetSetupOutput
  {
    get
    {
      return this.mTargetSetupOutput;
    }
  }

  public void OnStart()
  {
    this.aeroSlider.OnStart();
    this.speedSlider.OnStart();
    this.handlingSlider.OnStart();
  }

  public void SetupVehicle(RacingVehicle inVehicle, bool inAnimateWidget, PitScreen.Mode inMode)
  {
    this.mVehicle = inVehicle;
    this.mMode = inMode;
    this.mOptimalSetup = this.mVehicle.performance.setupPerformance.GetOptimalSetup();
    SetupStintData stintFromStack = this.GetStintFromStack(this.mVehicle, 0);
    SessionSetup.SetupOpinion[] opinionOnPreviousSetup = Game.instance.persistentEventData.GetOpinionOnPreviousSetup(this.mVehicle);
    this.aeroSlider.SetupSlider(this.mVehicle, this.mOptimalSetup.setupOutput.aerodynamics, this.mOptimalSetup.minVisualRangeOffset[0], this.mOptimalSetup.maxVisualRangeOffset[0], opinionOnPreviousSetup[0], stintFromStack.deltaFromOptimumAerodynamics, true);
    this.speedSlider.SetupSlider(this.mVehicle, this.mOptimalSetup.setupOutput.speedBalance, this.mOptimalSetup.minVisualRangeOffset[1], this.mOptimalSetup.maxVisualRangeOffset[1], opinionOnPreviousSetup[1], stintFromStack.deltaFromOptimumSpeedBalance, true);
    this.handlingSlider.SetupSlider(this.mVehicle, this.mOptimalSetup.setupOutput.handling, this.mOptimalSetup.minVisualRangeOffset[2], this.mOptimalSetup.maxVisualRangeOffset[2], opinionOnPreviousSetup[2], stintFromStack.deltaFromOptimumHandling, true);
    inVehicle.setup.GetSetupOutput(ref this.mCurrentSetupOutput);
    this.SetSetupOutput(this.mCurrentSetupOutput, this.mCurrentSetupOutput);
    this.ResetAnimator();
    if (inAnimateWidget && !Game.instance.persistentEventData.playerHasSeenStintFeedback[this.mVehicle.carID])
    {
      this.PrepareToAnimate();
    }
    else
    {
      this.animatedSetupKnowledgePanel.SetVehicle(this.mVehicle);
      this.aeroSlider.SetupEntryOnCurrentKnowledge();
      this.speedSlider.SetupEntryOnCurrentKnowledge();
      this.handlingSlider.SetupEntryOnCurrentKnowledge();
    }
    this.RefreshPercentagesText((SetupStintData) null);
  }

  public void Update()
  {
    if (this.mAnimatorState == UISetupBalanceWidget.animatorState.Idle)
      return;
    if (this.mAnimatorState == UISetupBalanceWidget.animatorState.KnowledgeBar || this.mAnimatorState == UISetupBalanceWidget.animatorState.OverallOpinion)
    {
      if ((double) this.mAnimationTimeLeft <= 0.0 && (this.mAnimatorState != UISetupBalanceWidget.animatorState.KnowledgeBar || this.knowledgeBarAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash != AnimationHashes.StintCompleteIntro))
      {
        this.GoToNextAnimatorState();
      }
      else
      {
        if (this.mAnimatorState == UISetupBalanceWidget.animatorState.KnowledgeBar)
          this.animatedSetupKnowledgePanel.SetManualKnowledge(this.mCurrentKnowledge - (this.mCurrentKnowledge - this.mPreviousKnowledge) * Mathf.Clamp01(this.mAnimationTimeLeft / this.mKnowledgeBarAnimatorTimeSeconds));
        this.mAnimationTimeLeft -= GameTimer.deltaTime;
      }
    }
    else
    {
      if (!this.IsSliderFinishedAnimating())
        return;
      this.GoToNextAnimatorState();
    }
  }

  public void OnExit()
  {
    this.mAnimatorState = UISetupBalanceWidget.animatorState.Idle;
    this.ResetAnimator();
  }

  public void SetSetupOutput(SessionSetup.SetupOutput inCurrentOutput, SessionSetup.SetupOutput inTargetOutput)
  {
    this.mTargetSetupOutput = inTargetOutput;
    this.aeroSlider.SetSetupOutput(inCurrentOutput.aerodynamics, this.mTargetSetupOutput.aerodynamics);
    this.speedSlider.SetSetupOutput(inCurrentOutput.speedBalance, this.mTargetSetupOutput.speedBalance);
    this.handlingSlider.SetSetupOutput(inCurrentOutput.handling, this.mTargetSetupOutput.handling);
  }

  private void ResetAnimator()
  {
    GameUtility.SetActive(this.knowledgAnimBackgroundBlock, false);
    GameUtility.SetActive(this.percentageFeedbackContainer, false);
  }

  private void PrepareToAnimate()
  {
    this.mAnimatorState = UISetupBalanceWidget.animatorState.Idle;
    this.mAnimatePercentageFeedback = false;
    this.mAnimatorPreviousStint = this.GetStintFromStack(this.mVehicle, 1);
    this.mPreviousKnowledge = this.mAnimatorPreviousStint.setupKnowledgeNormalised;
    this.mCurrentKnowledge = this.mVehicle.practiceKnowledge.GetSetupKnowledgeNormalised();
    this.mAnimateKnowledge = (double) this.mPreviousKnowledge != (double) this.mCurrentKnowledge;
    this.mVehicle.performance.setupPerformance.GetVisualKnowledgeRangeFromNormalisedValue(this.mAnimatorPreviousStint.setupKnowledgeNormalised, out this.mAnimatorPreviousMinKnowledge, out this.mAnimatorPreviousMaxKnowledge);
    this.mAnimatePercentageFeedback |= this.aeroSlider.PrepareToAnimate(this.mAnimatorPreviousMinKnowledge[0], this.mAnimatorPreviousMaxKnowledge[0], this.mAnimatorPreviousStint.deltaFromOptimumAerodynamics);
    this.mAnimatePercentageFeedback |= this.speedSlider.PrepareToAnimate(this.mAnimatorPreviousMinKnowledge[1], this.mAnimatorPreviousMaxKnowledge[1], this.mAnimatorPreviousStint.deltaFromOptimumSpeedBalance);
    this.mAnimatePercentageFeedback |= this.handlingSlider.PrepareToAnimate(this.mAnimatorPreviousMinKnowledge[2], this.mAnimatorPreviousMaxKnowledge[2], this.mAnimatorPreviousStint.deltaFromOptimumHandling);
    this.GoToNextAnimatorState();
    if (this.mAnimatePercentageFeedback && !Game.instance.persistentEventData.playerHasSeenStintFeedback[this.mVehicle.carID])
      GameUtility.SetActive(this.percentageFeedbackContainer, false);
    Game.instance.persistentEventData.playerHasSeenStintFeedback[this.mVehicle.carID] = true;
  }

  private void GoToNextAnimatorState()
  {
    switch (this.mAnimatorState)
    {
      case UISetupBalanceWidget.animatorState.KnowledgeBar:
        GameUtility.SetActive(this.knowledgAnimBackgroundBlock, false);
        this.mAnimatorState = UISetupBalanceWidget.animatorState.Aero;
        this.aeroSlider.StartAnimating();
        break;
      case UISetupBalanceWidget.animatorState.Aero:
        this.mAnimatorState = UISetupBalanceWidget.animatorState.Handling;
        this.handlingSlider.StartAnimating();
        break;
      case UISetupBalanceWidget.animatorState.Speed:
        if (this.mAnimatePercentageFeedback)
        {
          this.AnimateOverallDriverFeedbackPercent();
          break;
        }
        this.mAnimatorState = UISetupBalanceWidget.animatorState.Idle;
        break;
      case UISetupBalanceWidget.animatorState.Handling:
        this.mAnimatorState = UISetupBalanceWidget.animatorState.Speed;
        this.speedSlider.StartAnimating();
        break;
      case UISetupBalanceWidget.animatorState.OverallOpinion:
        this.mAnimatorState = UISetupBalanceWidget.animatorState.Idle;
        break;
      case UISetupBalanceWidget.animatorState.Idle:
        this.mAnimatorState = UISetupBalanceWidget.animatorState.KnowledgeBar;
        if (this.mAnimateKnowledge)
        {
          GameUtility.SetActive(this.knowledgAnimBackgroundBlock, true);
          this.knowledgeBarAnimator.SetTrigger("AnimateKnowledgeBar");
          this.mAnimationTimeLeft = this.mKnowledgeBarAnimatorTimeSeconds;
          break;
        }
        this.GoToNextAnimatorState();
        break;
    }
  }

  private void AnimateOverallDriverFeedbackPercent()
  {
    this.mAnimatorState = UISetupBalanceWidget.animatorState.OverallOpinion;
    GameUtility.SetActive(this.percentageFeedbackContainer, true);
    this.percentageFeedbackAnimator.SetTrigger("PopIn");
  }

  private bool IsSliderFinishedAnimating()
  {
    switch (this.mAnimatorState)
    {
      case UISetupBalanceWidget.animatorState.Aero:
        return this.aeroSlider.isAnimatorFinished;
      case UISetupBalanceWidget.animatorState.Speed:
        return this.speedSlider.isAnimatorFinished;
      case UISetupBalanceWidget.animatorState.Handling:
        return this.handlingSlider.isAnimatorFinished;
      default:
        return false;
    }
  }

  public void SetFeedbackFromStint(SetupStintData inStint)
  {
    if (inStint == null)
      return;
    this.aeroSlider.SetOpinion(inStint.aerodynamicsOpinion);
    this.speedSlider.SetOpinion(inStint.speedBalanceOpinion);
    this.handlingSlider.SetOpinion(inStint.handlingOpinion);
    this.RefreshPercentagesText(inStint);
  }

  private void RefreshPercentagesText(SetupStintData inStint = null)
  {
    bool inIsActive1;
    if (inStint == null)
    {
      inStint = this.GetStintWithCurrentSetup(this.mVehicle);
      inIsActive1 = inStint != null;
    }
    else
      inIsActive1 = inStint.aerodynamicsOpinion != SessionSetup.SetupOpinion.None && inStint.handlingOpinion != SessionSetup.SetupOpinion.None && inStint.speedBalanceOpinion != SessionSetup.SetupOpinion.None;
    GameUtility.SetActive(this.percentageFeedbackContainer, inIsActive1);
    if (!inIsActive1)
      return;
    this.percentageTotalText.text = GameUtility.GetPercentageText(inStint.GetOverallSetupPercentage(), 0.0f, false, false);
    float num = this.mAnimatorPreviousStint == null ? 0.0f : inStint.GetOverallSetupPercentage() - this.mAnimatorPreviousStint.GetOverallSetupPercentage();
    bool inIsActive2 = this.mMode == PitScreen.Mode.SendOut && !Mathf.Approximately(num, 0.0f) && (this.mAnimatorPreviousStint.aerodynamicsOpinion != SessionSetup.SetupOpinion.None && this.mAnimatorPreviousStint.handlingOpinion != SessionSetup.SetupOpinion.None) && this.mAnimatorPreviousStint.speedBalanceOpinion != SessionSetup.SetupOpinion.None;
    GameUtility.SetActive(this.percentageDeltaText.gameObject, inIsActive2);
    GameUtility.SetActive(this.percentageDivider, inIsActive2);
    GameUtility.SetActive(this.percentageDeltaUpArrow, inIsActive2 && (double) num > 0.0);
    GameUtility.SetActive(this.percentageDeltaDownArrow, inIsActive2 && (double) num < 0.0);
    if (!inIsActive2)
      return;
    this.percentageDeltaText.text = GameUtility.GetPercentageText(num, 0.0f, false, false);
    this.percentageDeltaText.color = GameUtility.GetCurrencyColor(num);
  }

  public void PreviewSetupFromRollover(bool inSetActive, SessionSetup.SetupOutput inSetupOutput = null)
  {
    if (inSetActive)
    {
      this.aeroSlider.PreviewSetupFromRollover(true, inSetupOutput.aerodynamics);
      this.speedSlider.PreviewSetupFromRollover(true, inSetupOutput.speedBalance);
      this.handlingSlider.PreviewSetupFromRollover(true, inSetupOutput.handling);
    }
    else
    {
      this.aeroSlider.PreviewSetupFromRollover(false, 0.0f);
      this.speedSlider.PreviewSetupFromRollover(false, 0.0f);
      this.handlingSlider.PreviewSetupFromRollover(false, 0.0f);
    }
  }

  private SetupStintData GetStintFromStack(RacingVehicle inVehicle, int inStintsToSkip = 0)
  {
    if (inVehicle != null)
    {
      List<SetupStintData> setupStintData = Game.instance.persistentEventData.GetSetupStintData(inVehicle);
      if (setupStintData.Count > 0 + inStintsToSkip)
        return setupStintData[setupStintData.Count - (1 + inStintsToSkip)];
    }
    return new SetupStintData();
  }

  private SetupStintData GetStintWithCurrentSetup(RacingVehicle inVehicle)
  {
    SetupInput_v1 input = inVehicle.setup.currentSetup.input;
    if (inVehicle != null)
    {
      List<SetupStintData> setupStintData = Game.instance.persistentEventData.GetSetupStintData(inVehicle);
      for (int index = 0; index < setupStintData.Count; ++index)
      {
        if (setupStintData[index].setupInput == input)
          return setupStintData[index];
      }
    }
    return (SetupStintData) null;
  }

  private enum animatorState
  {
    KnowledgeBar,
    Aero,
    Speed,
    Handling,
    OverallOpinion,
    Idle,
  }
}
