// Decompiled with JetBrains decompiler
// Type: UITravelSelectionWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class UITravelSelectionWidget : MonoBehaviour
{
  private List<UITravelStep> mSteps = new List<UITravelStep>();
  private UITravelSelectionWidget.warningStep mCurrentWarningStep = UITravelSelectionWidget.warningStep.none;
  public CanvasGroup canvasGroup;
  public UITravelStep ultimatum;
  public UITravelStep sponsorSelect;
  public UITravelStep tyreSelect;
  public UITravelStep partFittingSelect;
  private TravelArrangementsScreen mScreen;
  private RaceEventDetails mEventDetails;
  private int mSponsorCount;

  public UITravelSelectionWidget.warningStep currentWarning
  {
    get
    {
      return this.mCurrentWarningStep;
    }
  }

  public void OnStart()
  {
    this.ultimatum.OnStart();
    this.sponsorSelect.OnStart();
    this.tyreSelect.OnStart();
    this.partFittingSelect.OnStart();
  }

  public void Setup(TravelArrangementsScreen inTravelArrangementScreen)
  {
    this.mScreen = inTravelArrangementScreen;
    RaceEventDetails eventDetails = Game.instance.sessionManager.eventDetails;
    bool flag;
    if (this.mEventDetails == null || eventDetails != this.mEventDetails)
    {
      this.mEventDetails = eventDetails;
      flag = true;
    }
    else
      flag = false;
    if (flag)
    {
      this.mSteps.Clear();
      this.canvasGroup.blocksRaycasts = true;
      GameUtility.SetActive(this.ultimatum.gameObject, Game.instance.player.team.chairman.hasMadeUltimatum);
      if (this.ultimatum.gameObject.activeSelf)
      {
        this.ultimatum.Setup();
        this.mSteps.Add(this.ultimatum);
      }
      this.UpdateSponsorStep();
      if (this.sponsorSelect.gameObject.activeSelf)
        this.mSteps.Add(this.sponsorSelect);
      ChampionshipRules rules = Game.instance.player.team.championship.rules;
      bool inIsActive = rules.compoundChoice == ChampionshipRules.CompoundChoice.Free;
      GameUtility.SetActive(this.tyreSelect.gameObject, inIsActive);
      if (inIsActive)
      {
        this.tyreSelect.Setup();
        this.mSteps.Add(this.tyreSelect);
      }
      else if (rules.compoundsAvailable > 2)
      {
        int[] tyresForAi = Game.instance.persistentEventData.GetTyresForAI(Game.instance.player.team);
        Game.instance.persistentEventData.SetSelectedTyreCount(0, tyresForAi[0], tyresForAi[1], tyresForAi[2]);
        Game.instance.persistentEventData.SetSelectedTyreCount(1, tyresForAi[0], tyresForAi[1], tyresForAi[2]);
      }
      else
      {
        int[] tyresForAi = Game.instance.persistentEventData.GetTyresForAI(Game.instance.player.team);
        Game.instance.persistentEventData.SetSelectedTyreCount(0, tyresForAi[0], tyresForAi[1], 0);
        Game.instance.persistentEventData.SetSelectedTyreCount(1, tyresForAi[0], tyresForAi[1], 0);
      }
      GameUtility.SetActive(this.partFittingSelect.gameObject, true);
      this.partFittingSelect.Setup();
      this.mSteps.Add(this.partFittingSelect);
    }
    else
    {
      this.tyreSelect.RefreshText();
      this.ultimatum.RefreshText();
      this.sponsorSelect.RefreshText();
      GameUtility.SetActive(this.partFittingSelect.gameObject, true);
      this.partFittingSelect.Setup();
    }
    List<SponsorshipDeal> sponsorshipDeals = Game.instance.player.team.sponsorController.sponsorshipDeals;
    if (!flag && this.mSponsorCount != sponsorshipDeals.Count)
    {
      this.mScreen.ResetSponsorshipDeal();
      this.sponsorSelect.SetComplete(false);
      this.UpdateSponsorStep();
    }
    this.GoToNextStep();
    this.RefreshIsCompleteAndGetCurrentWarning();
  }

  private void UpdateSponsorStep()
  {
    this.mSponsorCount = Game.instance.player.team.sponsorController.sponsorshipDeals.Count;
    bool inIsActive = this.mSponsorCount > 0;
    GameUtility.SetActive(this.sponsorSelect.gameObject, inIsActive);
    if (this.sponsorSelect.gameObject.activeSelf)
      this.sponsorSelect.Setup();
    if (!inIsActive)
      this.sponsorSelect.requiresAction = false;
    else
      this.sponsorSelect.requiresAction = !this.sponsorSelect.IsComplete();
  }

  public void GoToNextStep()
  {
    int count = this.mSteps.Count;
    for (int index = 0; index < count; ++index)
    {
      UITravelStep mStep = this.mSteps[index];
      if (!mStep.IsComplete())
      {
        this.ActivateStep(mStep);
        return;
      }
    }
    this.ActivateStep(this.mSteps[this.mSteps.Count - 1]);
  }

  private void ActivateStep(UITravelStep inStep)
  {
    if (inStep.toggle.isOn)
    {
      this.TriggerSponsorAnimation(inStep);
      this.TriggerTyreAnimation(inStep);
      this.TriggerPartFittingAnimation(inStep);
    }
    inStep.toggle.isOn = true;
    inStep.OnToggle();
  }

  private void TriggerSponsorAnimation(UITravelStep inStep)
  {
    if (inStep.step != UITravelStep.Step.Sponsor)
      return;
    UITravelSponsorSelect option = inStep.option as UITravelSponsorSelect;
    if (!((Object) option != (Object) null))
      return;
    for (int index = 0; index < option.highlightAnimators.Count; ++index)
      option.highlightAnimators[index].SetTrigger(AnimationHashes.Play);
  }

  private void TriggerTyreAnimation(UITravelStep inStep)
  {
    if (inStep.step != UITravelStep.Step.TyreSelection)
      return;
    UITravelTyreSelection option = inStep.option as UITravelTyreSelection;
    if (!((Object) option != (Object) null))
      return;
    for (int index = 0; index < option.highlightAnimators.Length; ++index)
      option.highlightAnimators[index].SetTrigger(AnimationHashes.Play);
  }

  private void TriggerPartFittingAnimation(UITravelStep inStep)
  {
    if (inStep.step != UITravelStep.Step.CarFitting)
      return;
    UITravelCarFittingSelect option = inStep.option as UITravelCarFittingSelect;
    if (!((Object) option != (Object) null))
      return;
    for (int index = 0; index < option.highlightAnimators.Length; ++index)
      option.highlightAnimators[index].SetTrigger(AnimationHashes.Play);
  }

  public UITravelStep GetIncompleteStep()
  {
    int count = this.mSteps.Count;
    for (int index = 0; index < count; ++index)
    {
      UITravelStep mStep = this.mSteps[index];
      if (!mStep.IsComplete())
        return mStep;
    }
    return (UITravelStep) null;
  }

  public void RefreshIsCompleteAndGetCurrentWarning()
  {
    int count = this.mSteps.Count;
    for (int index = 0; index < count; ++index)
    {
      if (this.mSteps[index].requiresAction)
      {
        if (this.mSteps[index].step == UITravelStep.Step.Sponsor)
        {
          if (this.mCurrentWarningStep == UITravelSelectionWidget.warningStep.sponsors)
            return;
          this.mCurrentWarningStep = UITravelSelectionWidget.warningStep.sponsors;
          this.mScreen.RefreshWarnings();
          return;
        }
        if (this.mSteps[index].step == UITravelStep.Step.TyreSelection)
        {
          if (this.mCurrentWarningStep == UITravelSelectionWidget.warningStep.tyres)
            return;
          this.mCurrentWarningStep = UITravelSelectionWidget.warningStep.tyres;
          this.mScreen.RefreshWarnings();
          return;
        }
        if (this.mSteps[index].step == UITravelStep.Step.CarFitting)
        {
          if (this.mCurrentWarningStep == UITravelSelectionWidget.warningStep.partFitting)
            return;
          this.mCurrentWarningStep = UITravelSelectionWidget.warningStep.partFitting;
          this.mScreen.RefreshWarnings();
          return;
        }
      }
    }
    if (this.mCurrentWarningStep != UITravelSelectionWidget.warningStep.none)
      this.mCurrentWarningStep = UITravelSelectionWidget.warningStep.none;
    this.mScreen.RefreshWarnings();
  }

  public void DisableButtons()
  {
    this.canvasGroup.blocksRaycasts = false;
  }

  public bool IsComplete()
  {
    for (int index = 0; index < this.mSteps.Count; ++index)
    {
      if (!this.mSteps[index].IsComplete())
        return false;
    }
    return true;
  }

  public enum warningStep
  {
    sponsors,
    tyres,
    partFitting,
    none,
  }
}
