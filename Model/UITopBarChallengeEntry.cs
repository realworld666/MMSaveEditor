// Decompiled with JetBrains decompiler
// Type: UITopBarChallengeEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITopBarChallengeEntry : MonoBehaviour
{
  public TextMeshProUGUI titleText;
  public TextMeshProUGUI objectiveText;
  public TextMeshProUGUI restrictionsText;
  public GameObject restrictionsContainer;
  public TextMeshProUGUI rewardText;
  public GameObject rewardContainer;
  public Image statusBackground;
  public TextMeshProUGUI statusText;

  public void Setup(Challenge inChallenge)
  {
    this.titleText.text = Localisation.LocaliseEnum((Enum) inChallenge.challengeName);
    this.objectiveText.text = Localisation.LocaliseID(inChallenge.descriptionID, (GameObject) null);
    this.restrictionsText.text = Localisation.LocaliseID(inChallenge.restrictionsID, (GameObject) null);
    this.rewardText.text = Localisation.LocaliseID(inChallenge.rewardsID, (GameObject) null);
    this.statusText.text = Localisation.LocaliseEnum((Enum) inChallenge.status);
    if (inChallenge.status == Challenge.ChallengeStatus.Completed)
    {
      this.statusBackground.color = UIConstants.positiveColor;
      GameUtility.SetActive(this.rewardContainer, true);
      GameUtility.SetActive(this.restrictionsContainer, false);
    }
    else if (inChallenge.status == Challenge.ChallengeStatus.Failed)
    {
      this.statusBackground.color = UIConstants.negativeColor;
      GameUtility.SetActive(this.rewardContainer, false);
      GameUtility.SetActive(this.restrictionsContainer, true);
    }
    else
    {
      this.statusBackground.color = UIConstants.colorSetupOpinionGood;
      GameUtility.SetActive(this.rewardContainer, true);
      GameUtility.SetActive(this.restrictionsContainer, true);
    }
  }
}
