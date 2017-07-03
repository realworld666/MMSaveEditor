// Decompiled with JetBrains decompiler
// Type: HomeScreenDriversInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;

public class HomeScreenDriversInfo : HomeScreenInfoPanel
{
  private HomeScreenDriversInfo.Stage mStage = HomeScreenDriversInfo.Stage.General;
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
      case HomeScreenDriversInfo.Stage.ReplaceMainDriver:
        ScoutingScreen.ChangeScreen(UIScoutingFilterJobRole.Filter.Drivers);
        break;
      case HomeScreenDriversInfo.Stage.MissingReserveDriver:
        ScoutingScreen.ChangeScreen(UIScoutingFilterJobRole.Filter.Drivers);
        break;
      case HomeScreenDriversInfo.Stage.ReplaceReserveDriver:
        ScoutingScreen.ChangeScreen(UIScoutingFilterJobRole.Filter.Drivers);
        break;
      case HomeScreenDriversInfo.Stage.General:
        UIManager.instance.ChangeScreen("AllDriversScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
        break;
    }
  }

  private void SelectStage()
  {
    Driver driver1 = this.mTeam.GetDriver(0);
    Driver driver2 = this.mTeam.GetDriver(1);
    Driver driver3 = this.mTeam.GetDriver(2);
    bool flag1 = driver1.IsReplacementPerson() || driver2.IsReplacementPerson();
    bool flag2 = driver3 == null || driver3 is NullDriver;
    bool flag3 = driver3 != null && driver3.IsReplacementPerson();
    if (flag1)
      this.mStage = HomeScreenDriversInfo.Stage.ReplaceMainDriver;
    else if (flag2)
      this.mStage = HomeScreenDriversInfo.Stage.MissingReserveDriver;
    else if (flag3)
      this.mStage = HomeScreenDriversInfo.Stage.ReplaceReserveDriver;
    else
      this.mStage = HomeScreenDriversInfo.Stage.General;
  }

  private void SetStage()
  {
    switch (this.mStage)
    {
      case HomeScreenDriversInfo.Stage.ReplaceMainDriver:
        this.titleLabel.text = Localisation.LocaliseID("PSG_10010272", (GameObject) null);
        this.descriptionLabel.text = Localisation.LocaliseID("PSG_10010273", (GameObject) null);
        this.buttonLabel.text = Localisation.LocaliseID("PSG_10006858", (GameObject) null);
        this.statusLabel.text = Localisation.LocaliseID("PSG_10010274", (GameObject) null);
        break;
      case HomeScreenDriversInfo.Stage.MissingReserveDriver:
        this.titleLabel.text = Localisation.LocaliseID("PSG_10001585", (GameObject) null);
        this.descriptionLabel.text = Localisation.LocaliseID("PSG_10010275", (GameObject) null);
        this.buttonLabel.text = Localisation.LocaliseID("PSG_10006858", (GameObject) null);
        this.statusLabel.text = Localisation.LocaliseID("PSG_10010277", (GameObject) null);
        break;
      case HomeScreenDriversInfo.Stage.ReplaceReserveDriver:
        this.titleLabel.text = Localisation.LocaliseID("PSG_10001585", (GameObject) null);
        this.descriptionLabel.text = Localisation.LocaliseID("PSG_10010275", (GameObject) null);
        this.buttonLabel.text = Localisation.LocaliseID("PSG_10006858", (GameObject) null);
        this.statusLabel.text = Localisation.LocaliseID("PSG_10010278", (GameObject) null);
        break;
      case HomeScreenDriversInfo.Stage.General:
        this.titleLabel.text = Localisation.LocaliseID("PSG_10000002", (GameObject) null);
        this.descriptionLabel.text = Localisation.LocaliseID("PSG_10010388", (GameObject) null);
        this.buttonLabel.text = Localisation.LocaliseID("PSG_10010389", (GameObject) null);
        this.statusLabel.text = Localisation.LocaliseID("PSG_10000002", (GameObject) null);
        break;
    }
  }

  public override bool isDefaultState()
  {
    return this.mStage == HomeScreenDriversInfo.Stage.General;
  }

  public enum Stage
  {
    ReplaceMainDriver,
    MissingReserveDriver,
    ReplaceReserveDriver,
    General,
  }
}
