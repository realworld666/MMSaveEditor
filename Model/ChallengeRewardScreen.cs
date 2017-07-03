// Decompiled with JetBrains decompiler
// Type: ChallengeRewardScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class ChallengeRewardScreen : UIScreen
{
  private Challenge.ChallengeName mChallengeName = Challenge.ChallengeName.TopManager;
  public UIRacePodiumTrophyWidget sceneWidget;
  public UIPlayerWidget playerWidget;
  public TextMeshProUGUI completeHeading;
  public GameObject challengeUnderdogBackground;
  public GameObject challengeTopManagerBackground;
  public GameObject underdogAward;
  public GameObject topmanagerAward;
  public Portrait playerPortait;

  public override void OnStart()
  {
    base.OnStart();
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.showNavigationBars = true;
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionResults, 0.0f);
  }

  public void Setup(Challenge.ChallengeName inChallengeName)
  {
    this.mChallengeName = inChallengeName;
    StringVariableParser.challengeName = this.mChallengeName;
    this.completeHeading.text = Localisation.LocaliseID("PSG_10010178", (GameObject) null);
    this.playerWidget.Setup(Game.instance.player);
    switch (this.mChallengeName)
    {
      case Challenge.ChallengeName.Underdog:
        this.sceneWidget.SetConfettiColor(UIConstants.colorBandRed, UIConstants.whiteColor);
        GameUtility.SetActive(this.challengeUnderdogBackground, true);
        GameUtility.SetActive(this.challengeTopManagerBackground, false);
        GameUtility.SetActive(this.topmanagerAward, false);
        GameUtility.SetActive(this.underdogAward, true);
        break;
      case Challenge.ChallengeName.TopManager:
        this.sceneWidget.SetConfettiColor(UIConstants.colorBandYellow, UIConstants.colorBandRed);
        GameUtility.SetActive(this.challengeUnderdogBackground, false);
        GameUtility.SetActive(this.challengeTopManagerBackground, true);
        GameUtility.SetActive(this.topmanagerAward, true);
        GameUtility.SetActive(this.underdogAward, false);
        break;
    }
    this.sceneWidget.SetupNoTrophy();
  }

  public override void OnExit()
  {
    base.OnExit();
  }
}
