// Decompiled with JetBrains decompiler
// Type: ScrutineeringWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrutineeringWidget : MonoBehaviour
{
  private float mAnimationSpeed = 3f;
  private List<RacingVehicle> mVehicles = new List<RacingVehicle>();
  private List<PenaltyPartRulesBroken> mPartPenalties = new List<PenaltyPartRulesBroken>();
  public TextMeshProUGUI riskOfRulesLabel;
  public TextMeshProUGUI ruleBreaksFound;
  public TextMeshProUGUI noRuleBreaksFound;
  public GameObject checkingCarsContainer;
  public GameObject scrutineeringDoneContainer;
  public UIGridList partsList;
  public Slider slider;
  public Animator animator;
  private float mTimer;
  private float mTotalRulesRisk;
  private int mRulesBrokenFound;
  private CarPartStats.RulesRisk mRulesRisk;
  private bool mSetup;
  private ScrutineeringWidget.State mCurrentState;
  private ScrutineeringScreen mScreen;

  public ScrutineeringWidget.State currentState
  {
    get
    {
      return this.mCurrentState;
    }
  }

  public void Setup(ScrutineeringScreen inScreen)
  {
    this.mScreen = inScreen;
    this.partsList.DestroyListItems();
    this.mVehicles.Clear();
    this.mPartPenalties.Clear();
    this.mTimer = 0.0f;
    for (int index = 0; index < Game.instance.sessionManager.standings.Count; ++index)
    {
      RacingVehicle standing = Game.instance.sessionManager.standings[index];
      if (standing.driver.contract.GetTeam() == Game.instance.player.team)
        this.mVehicles.Add(standing);
    }
    this.mTotalRulesRisk = 0.0f;
    for (int index1 = 0; index1 < this.mVehicles.Count; ++index1)
    {
      RacingVehicle mVehicle = this.mVehicles[index1];
      this.mTotalRulesRisk += mVehicle.car.GetRulesRisk();
      for (int index2 = 0; index2 < mVehicle.penaltiesCount; ++index2)
      {
        PenaltyPartRulesBroken penalty = mVehicle.sessionPenalty.penalties[index2] as PenaltyPartRulesBroken;
        if (penalty != null)
          this.mPartPenalties.Add(penalty);
      }
    }
    for (int index = 0; index < this.mPartPenalties.Count; ++index)
    {
      PenaltyPartRulesBroken mPartPenalty = this.mPartPenalties[index];
      mPartPenalty.revealAnimTime = (float) (index + 1) * this.mAnimationSpeed / (float) this.mPartPenalties.Count - RandomUtility.GetRandom(0.1f, this.mAnimationSpeed / 10f);
      this.mTotalRulesRisk += mPartPenalty.part.stats.rulesRisk;
    }
    this.SetRiskVariables(this.mTotalRulesRisk);
    this.mRulesBrokenFound = 0;
    this.riskOfRulesLabel.text = Localisation.LocaliseEnum((Enum) this.mRulesRisk);
    this.slider.value = this.mTimer / this.mAnimationSpeed;
    this.ruleBreaksFound.text = string.Empty;
    if ((double) this.mTotalRulesRisk == 0.0)
      this.noRuleBreaksFound.text = Localisation.LocaliseID("PSG_10005050", (GameObject) null);
    else
      this.noRuleBreaksFound.text = Localisation.LocaliseID("PSG_10005051", (GameObject) null);
    this.mSetup = true;
  }

  private void Update()
  {
    if (!this.mSetup)
      return;
    bool inIsActive = false;
    if (this.mCurrentState == ScrutineeringWidget.State.FadeIn)
    {
      if ((double) this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0)
        this.mCurrentState = ScrutineeringWidget.State.SliderUpdate;
    }
    else if (this.mCurrentState == ScrutineeringWidget.State.SliderUpdate)
    {
      this.mTimer += GameTimer.deltaTime;
      this.slider.value = this.mTimer / this.mAnimationSpeed;
      for (int index = 0; index < this.mPartPenalties.Count; ++index)
      {
        PenaltyPartRulesBroken mPartPenalty = this.mPartPenalties[index];
        if ((double) mPartPenalty.revealAnimTime <= (double) this.mTimer)
        {
          this.partsList.CreateListItem<PenaltyEntry>().Setup(mPartPenalty);
          this.mPartPenalties.Remove(mPartPenalty);
          --index;
          ++this.mRulesBrokenFound;
          StringVariableParser.intValue1 = this.mRulesBrokenFound;
          this.ruleBreaksFound.text = Localisation.LocaliseID("PSG_10011156", (GameObject) null);
          this.animator.SetTrigger(AnimationHashes.Play);
        }
      }
      if ((double) this.slider.value == 1.0)
        this.mCurrentState = ScrutineeringWidget.State.Complete;
    }
    else if (this.mCurrentState == ScrutineeringWidget.State.Complete)
    {
      this.mScreen.continueButtonInteractable = true;
      inIsActive = true;
    }
    GameUtility.SetActive(this.checkingCarsContainer, !inIsActive);
    GameUtility.SetActive(this.scrutineeringDoneContainer, inIsActive);
    GameUtility.SetActive(this.noRuleBreaksFound.gameObject, inIsActive && this.mRulesBrokenFound == 0);
  }

  public void SetRiskVariables(float inRisk)
  {
    this.mRulesRisk = CarPartStats.GetRisk(inRisk);
    this.riskOfRulesLabel.color = CarPartStats.GetRiskColor(inRisk);
    switch (this.mRulesRisk)
    {
      case CarPartStats.RulesRisk.None:
        this.mAnimationSpeed = 3f;
        break;
      case CarPartStats.RulesRisk.Low:
        this.mAnimationSpeed = 4f;
        break;
      case CarPartStats.RulesRisk.Medium:
        this.mAnimationSpeed = 5f;
        break;
      case CarPartStats.RulesRisk.High:
        this.mAnimationSpeed = 6f;
        break;
    }
  }

  public enum State
  {
    FadeIn,
    SliderUpdate,
    Complete,
  }
}
