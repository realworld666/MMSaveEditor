// Decompiled with JetBrains decompiler
// Type: HomeScreenStaffInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HomeScreenStaffInfo : HomeScreenInfoPanel
{
  private HomeScreenStaffInfo.Stage mStage = HomeScreenStaffInfo.Stage.General;
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
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    switch (this.mStage)
    {
      case HomeScreenStaffInfo.Stage.FindNewEngineer:
        ScoutingScreen.ChangeScreen(UIScoutingFilterJobRole.Filter.Designers);
        break;
      case HomeScreenStaffInfo.Stage.FindNewMechanic:
        ScoutingScreen.ChangeScreen(UIScoutingFilterJobRole.Filter.Mechanics);
        break;
      case HomeScreenStaffInfo.Stage.General:
        UIManager.instance.ChangeScreen("StaffScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
        break;
    }
  }

  private void SelectStage()
  {
    Engineer personOnJob = this.mTeam.contractManager.GetPersonOnJob<Engineer>(Contract.Job.EngineerLead);
    List<Mechanic> allPeopleOnJob = this.mTeam.contractManager.GetAllPeopleOnJob<Mechanic>(Contract.Job.Mechanic);
    bool flag1 = personOnJob.IsReplacementPerson();
    bool flag2 = allPeopleOnJob[0].IsReplacementPerson() || allPeopleOnJob[1].IsReplacementPerson();
    if (flag1)
      this.mStage = HomeScreenStaffInfo.Stage.FindNewEngineer;
    else if (flag2)
      this.mStage = HomeScreenStaffInfo.Stage.FindNewMechanic;
    else
      this.mStage = HomeScreenStaffInfo.Stage.General;
  }

  private void SetStage()
  {
    switch (this.mStage)
    {
      case HomeScreenStaffInfo.Stage.FindNewEngineer:
        this.titleLabel.text = Localisation.LocaliseID("PSG_10010342", (GameObject) null);
        this.descriptionLabel.text = Localisation.LocaliseID("PSG_10010343", (GameObject) null);
        this.buttonLabel.text = Localisation.LocaliseID("PSG_10010344", (GameObject) null);
        this.statusLabel.text = Localisation.LocaliseID("PSG_10010345", (GameObject) null);
        break;
      case HomeScreenStaffInfo.Stage.FindNewMechanic:
        this.titleLabel.text = Localisation.LocaliseID("PSG_10010346", (GameObject) null);
        this.descriptionLabel.text = Localisation.LocaliseID("PSG_10010347", (GameObject) null);
        this.buttonLabel.text = Localisation.LocaliseID("PSG_10010348", (GameObject) null);
        this.statusLabel.text = Localisation.LocaliseID("PSG_10010349", (GameObject) null);
        break;
      case HomeScreenStaffInfo.Stage.General:
        this.titleLabel.text = Localisation.LocaliseID("PSG_10003781", (GameObject) null);
        this.descriptionLabel.text = Localisation.LocaliseID("PSG_10010350", (GameObject) null);
        this.buttonLabel.text = Localisation.LocaliseID("PSG_10009335", (GameObject) null);
        this.statusLabel.text = Localisation.LocaliseID("PSG_10005796", (GameObject) null);
        break;
    }
  }

  public override bool isDefaultState()
  {
    return this.mStage == HomeScreenStaffInfo.Stage.General;
  }

  public enum Stage
  {
    FindNewEngineer,
    FindNewMechanic,
    General,
  }
}
