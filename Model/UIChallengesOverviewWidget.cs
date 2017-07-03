// Decompiled with JetBrains decompiler
// Type: UIChallengesOverviewWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;

public class UIChallengesOverviewWidget : MonoBehaviour
{
  public TextMeshProUGUI titleLabel;
  public TextMeshProUGUI difficultyLabel;
  public TextMeshProUGUI objectiveLabel;
  public TextMeshProUGUI restrictionsLabel;
  public TextMeshProUGUI rewardsLabel;
  private Challenge mChallenge;

  public void Setup(Challenge inChallenge)
  {
    this.mChallenge = inChallenge;
    this.titleLabel.text = Localisation.LocaliseEnum((Enum) this.mChallenge.challengeName);
    this.difficultyLabel.text = Localisation.LocaliseEnum((Enum) this.mChallenge.difficulty);
    this.difficultyLabel.color = this.GetDifficultyColor();
    this.objectiveLabel.text = Localisation.LocaliseID(this.mChallenge.objectivesID, (GameObject) null);
    this.restrictionsLabel.text = Localisation.LocaliseID(this.mChallenge.restrictionsID, (GameObject) null);
    this.rewardsLabel.text = Localisation.LocaliseID(this.mChallenge.rewardsID, (GameObject) null);
  }

  private Color GetDifficultyColor()
  {
    switch (this.mChallenge.difficulty)
    {
      case Challenge.Difficulty.Easy:
        return UIConstants.colorBandGreen;
      case Challenge.Difficulty.Medium:
        return UIConstants.colorBandYellow;
      case Challenge.Difficulty.Hard:
        return UIConstants.colorBandRed;
      default:
        return UIConstants.colorBandGreen;
    }
  }
}
