// Decompiled with JetBrains decompiler
// Type: TeamScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using MM2;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeamScreen : UIScreen
{
  public UITeamScreenKeyStaffEntry[] drivers = new UITeamScreenKeyStaffEntry[0];
  public UITeamScreenKeyStaffEntry[] mechanics = new UITeamScreenKeyStaffEntry[0];
  public UITeamScreenKeyStaffEntry teamPrinciple;
  public UITeamScreenKeyStaffEntry chiefEngineer;
  public UITeamScreenHistoryWidget historyWidget;
  public UITeamScreenHeadquartersWidget HQwidget;
  public UITeamScreenChairmanWidget chairmanWidget;
  public UITeamScreenTeamInfoWidget teamWidget;
  public UICarSupplierRolloverTrigger supplierRollover;
  public UIGenericCarBackground carBackground;
  public GameObject carBackgroundParent;
  public GameObject carCamera;
  public TextMeshProUGUI marketabilityText;
  public Slider marketabilitySlider;
  private Team mTeam;
  private StudioScene mStudioScene;

  public Team team
  {
    get
    {
      return this.mTeam;
    }
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.mTeam = (Team) this.data;
    if (this.mTeam == null)
      this.mTeam = Game.instance.player.team;
    this.supplierRollover.Setup(this.mTeam, false);
    this.SetupStaff(this.mTeam);
    this.chairmanWidget.Setup(this.mTeam);
    this.teamWidget.Setup(this.mTeam);
    this.HQwidget.Setup(this.mTeam.headquarters);
    this.historyWidget.Setup(this.mTeam);
    float inMarketability = MathsUtility.RoundToDecimal(this.mTeam.GetTeamTotalMarketability() * 100f);
    this.marketabilityText.text = inMarketability.ToString((IFormatProvider) Localisation.numberFormatter) + "%";
    this.marketabilitySlider.value = this.mTeam.GetTeamTotalMarketability();
    this.UpdateMarketabilityAchievements(inMarketability);
    this.showNavigationBars = true;
    if (App.instance.gameStateManager.currentState is FrontendState)
      this.LoadScene();
    GameUtility.SetActive(this.carBackgroundParent, this.screenMode == UIScreen.ScreenMode.Mode2D);
    if (this.screenMode == UIScreen.ScreenMode.Mode2D)
      this.carBackground.SetCar(this.mTeam);
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionHome, 0.0f);
  }

  private void LoadScene()
  {
    GameUtility.SetActive(this.carCamera, this.screenMode == UIScreen.ScreenMode.Mode3D);
    if (this.screenMode != UIScreen.ScreenMode.Mode3D)
      return;
    if (!SceneManager.instance.GetSceneGameObject("TrackFrontEnd").activeSelf)
    {
      SceneManager.instance.SwitchScene("TrackFrontEnd");
      this.mStudioScene = SceneManager.instance.GetSceneGameObject("TrackFrontEnd").GetComponent<StudioScene>();
      this.mStudioScene.SetCarType(StudioScene.Car.CurrentCar);
      this.mStudioScene.EnableCamera("ToTextureProfileCamera");
    }
    this.mStudioScene.ResetMode();
    this.mStudioScene.SetSeries(this.mTeam.championship.series);
    this.mStudioScene.SetSavedCarVisuals(this.mTeam.teamID, this.mTeam.championship.championshipID, this.mTeam.carManager.frontendCar.data, this.mTeam.carManager.nextFrontendCar.data);
  }

  private void SetupStaff(Team inTeam)
  {
    for (int inIndex = 0; inIndex < Team.driverCount; ++inIndex)
      this.drivers[inIndex].Setup((Person) inTeam.GetDriver(inIndex));
    List<Person> allPeopleOnJob = inTeam.contractManager.GetAllPeopleOnJob(Contract.Job.Mechanic);
    for (int index = 0; index < allPeopleOnJob.Count; ++index)
      this.mechanics[index].Setup(allPeopleOnJob[index]);
    this.teamPrinciple.Setup(inTeam.contractManager.GetPersonOnJob(Contract.Job.TeamPrincipal));
    this.chiefEngineer.Setup(inTeam.contractManager.GetPersonOnJob(Contract.Job.EngineerLead));
  }

  public void UpdateAbility()
  {
    for (int index = 0; index < Team.driverCount; ++index)
      this.drivers[index].UpdateAbility();
  }

  public override void OnExit()
  {
    base.OnExit();
    if (!(App.instance.gameStateManager.currentState is FrontendState))
      return;
    this.UnloadScene();
  }

  private void UnloadScene()
  {
    if (this.screenMode != UIScreen.ScreenMode.Mode3D || !((UnityEngine.Object) this.mStudioScene != (UnityEngine.Object) null) || Game.instance.player.IsUnemployed())
      return;
    this.mStudioScene.TuneSpotlight(true);
    this.mStudioScene.SetCameraTargetToTrackAlongCar(false);
    this.mStudioScene.SetCarVisualsToCurrentGame();
    SceneManager.instance.LeaveCurrentScene();
  }

  private void UpdateMarketabilityAchievements(float inMarketability)
  {
    if (!MathsUtility.Approximately(inMarketability, 100f, 0.05f))
      return;
    App.instance.steamAchievementsManager.UnlockAchievement(Achievements.AchievementEnum.Reach_Max_Marketability);
  }
}
