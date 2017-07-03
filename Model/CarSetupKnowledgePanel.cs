// Decompiled with JetBrains decompiler
// Type: CarSetupKnowledgePanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarSetupKnowledgePanel : MonoBehaviour
{
  private bool mFeedbackChanged = true;
  private string mFeedbackText = string.Empty;
  private Color feedbackColor = UIConstants.whiteColor;
  private PracticeKnowledge.SetupKnowledgeLevel mLastKnowledgeLevel = PracticeKnowledge.SetupKnowledgeLevel.Great;
  public Slider knowledgeSlider;
  public TextMeshProUGUI feedbackLabel;
  public Image feedbackImage;
  private RacingVehicle mVehicle;
  private bool mManuallyControl;
  private Image sliderFill;

  public void OnEnable()
  {
    this.sliderFill = ((IEnumerable<Image>) this.knowledgeSlider.GetComponentsInChildren<Image>()).FirstOrDefault<Image>((Func<Image, bool>) (t => t.name == "Fill"));
  }

  public void SetVehicle(RacingVehicle inVehicle)
  {
    this.mVehicle = inVehicle;
    this.mManuallyControl = false;
  }

  public void SetManualKnowledge(float inKnowledgeNormalised)
  {
    this.SetPanel(inKnowledgeNormalised);
    this.mManuallyControl = true;
  }

  private void Update()
  {
    if (this.mVehicle == null || this.mManuallyControl)
      return;
    this.SetPanel(this.mVehicle.practiceKnowledge.GetSetupKnowledgeNormalised());
  }

  private void SetPanel(float inKnowledgeNormalised)
  {
    if ((UnityEngine.Object) this.knowledgeSlider != (UnityEngine.Object) null)
      GameUtility.SetSliderAmountIfDifferent(this.knowledgeSlider, inKnowledgeNormalised, 1000f);
    PracticeKnowledge.SetupKnowledgeLevel fromNormalisedValue = PracticeKnowledge.GetSetupKnowledgeLevelFromNormalisedValue(inKnowledgeNormalised);
    this.mFeedbackChanged = fromNormalisedValue != this.mLastKnowledgeLevel;
    this.mLastKnowledgeLevel = fromNormalisedValue;
    if (!this.mFeedbackChanged)
      return;
    switch (fromNormalisedValue)
    {
      case PracticeKnowledge.SetupKnowledgeLevel.Low:
        this.mFeedbackText = Localisation.LocaliseID("PSG_10001437", (GameObject) null);
        this.feedbackColor = UIConstants.colorSetupOpinionVeryPoor;
        break;
      case PracticeKnowledge.SetupKnowledgeLevel.Medium:
        this.mFeedbackText = Localisation.LocaliseID("PSG_10001438", (GameObject) null);
        this.feedbackColor = UIConstants.colorSetupOpinionPoor;
        break;
      case PracticeKnowledge.SetupKnowledgeLevel.High:
        this.mFeedbackText = Localisation.LocaliseID("PSG_10001439", (GameObject) null);
        this.feedbackColor = UIConstants.colorSetupOpinionGood;
        break;
      case PracticeKnowledge.SetupKnowledgeLevel.Great:
        this.mFeedbackText = Localisation.LocaliseID("PSG_10010180", (GameObject) null);
        this.feedbackColor = UIConstants.colorSetupOpinionGreat;
        break;
    }
    this.feedbackLabel.text = this.mFeedbackText;
    this.feedbackImage.color = this.feedbackColor;
    if (!((UnityEngine.Object) this.sliderFill != (UnityEngine.Object) null))
      return;
    this.sliderFill.color = this.feedbackColor;
  }
}
