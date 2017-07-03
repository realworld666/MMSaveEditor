// Decompiled with JetBrains decompiler
// Type: HomeScreenSponsorsInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;

public class HomeScreenSponsorsInfo : HomeScreenInfoPanel
{
  private HomeScreenSponsorsInfo.Stage mStage = HomeScreenSponsorsInfo.Stage.General;
  private Team mTeam;

  public override void OnStart()
  {
    this.button.onClick.AddListener(new UnityAction(this.OnButton));
  }

  public override void Setup()
  {
    this.mTeam = Game.instance.player.team;
    this.SelectStage();
    this.SetStage();
  }

  private void OnButton()
  {
    switch (this.mStage)
    {
      case HomeScreenSponsorsInfo.Stage.CheckOffers:
        UIManager.instance.ChangeScreen("SponsorsScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
        break;
      case HomeScreenSponsorsInfo.Stage.General:
        UIManager.instance.ChangeScreen("SponsorsScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
        break;
    }
  }

  private void SelectStage()
  {
    if (this.mTeam.sponsorController.GetTotalOffersCount() > 0 && this.mTeam.sponsorController.GetFreeSponsorSlots() > 0)
      this.mStage = HomeScreenSponsorsInfo.Stage.CheckOffers;
    else
      this.mStage = HomeScreenSponsorsInfo.Stage.General;
  }

  private void SetStage()
  {
    switch (this.mStage)
    {
      case HomeScreenSponsorsInfo.Stage.CheckOffers:
        this.titleLabel.text = Localisation.LocaliseID("PSG_10003545", (GameObject) null);
        this.descriptionLabel.text = Localisation.LocaliseID("PSG_10010255", (GameObject) null);
        this.buttonLabel.text = Localisation.LocaliseID("PSG_10010256", (GameObject) null);
        this.statusLabel.text = Localisation.LocaliseID("PSG_10010257", (GameObject) null);
        break;
      case HomeScreenSponsorsInfo.Stage.General:
        this.titleLabel.text = Localisation.LocaliseID("PSG_10003782", (GameObject) null);
        this.descriptionLabel.text = Localisation.LocaliseID("PSG_10010258", (GameObject) null);
        this.buttonLabel.text = Localisation.LocaliseID("PSG_10009337", (GameObject) null);
        this.statusLabel.text = Localisation.LocaliseID("PSG_10005796", (GameObject) null);
        break;
    }
  }

  public override bool isDefaultState()
  {
    return this.mStage == HomeScreenSponsorsInfo.Stage.General;
  }

  public enum Stage
  {
    CheckOffers,
    General,
  }
}
