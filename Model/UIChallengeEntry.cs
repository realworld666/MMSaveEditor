// Decompiled with JetBrains decompiler
// Type: UIChallengeEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIChallengeEntry : MonoBehaviour
{
  public Toggle toggle;
  public TextMeshProUGUI titleLabel;
  public TextMeshProUGUI descriptionLabel;
  public TextMeshProUGUI difficultyLabel;
  public GameObject challengeCompleted;
  public ChallengesScreen screen;
  private Challenge mChallenge;

  public void Setup(Challenge inChallenge)
  {
    this.mChallenge = inChallenge;
    this.toggle.onValueChanged.RemoveAllListeners();
    this.toggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggle));
    this.titleLabel.text = Localisation.LocaliseEnum((Enum) this.mChallenge.challengeName);
    this.descriptionLabel.text = Localisation.LocaliseID(this.mChallenge.descriptionID, (GameObject) null);
    this.difficultyLabel.text = Localisation.LocaliseEnum((Enum) this.mChallenge.difficulty);
    this.difficultyLabel.color = this.GetDifficultyColor();
    GameUtility.SetActive(this.challengeCompleted, this.mChallenge.HasBeenCompleted());
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

  public void OnToggle(bool inValue)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (!inValue || this.mChallenge == null)
      return;
    this.screen.SelectChallenge(this.mChallenge);
  }
}
