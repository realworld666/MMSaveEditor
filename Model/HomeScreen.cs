// Decompiled with JetBrains decompiler
// Type: HomeScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class HomeScreen : UIScreen
{
  public HomeScreenTeamInfoWidget teamInfoWidget;
  public HomeScreenTeamStatsWidget teamStatsWidget;
  public HomeScreenStatsInfoWidget infoPanelWidget;
  public HomeScreenChairmanWidget chairmanWidget;
  public HomeScreenWipWidget wipWidget;
  public HomeScreenNextRaceWidget nextRaceWidget;
  public MiniStandingsWidget miniStandingsWidget;
  public UIGenericCarBackground carBackground;
  public GameObject carBackgroundParent;
  public GameObject carCamera;
  private Championship mChampionship;
  private StudioScene mStudioScene;

  public override void OnStart()
  {
    base.OnStart();
    this.nextRaceWidget.OnStart();
    this.infoPanelWidget.OnStart();
  }

  public override void OnEnter()
  {
    base.OnEnter();
    scSoundManager.BlockSoundEvents = true;
    this.SetBottomBarMode(UIBottomBar.Mode.Frontend);
    this.SetTopBarMode(UITopBar.Mode.Frontend);
    this.showNavigationBars = true;
    if (!Game.instance.player.IsUnemployed())
    {
      this.teamInfoWidget.Setup();
      this.infoPanelWidget.Setup();
      this.teamStatsWidget.Setup();
      this.chairmanWidget.Setup();
      this.wipWidget.Setup();
      this.nextRaceWidget.Setup();
      this.mChampionship = Game.instance.player.team.championship;
      this.miniStandingsWidget.OnEnter(this.mChampionship);
    }
    scSoundManager.BlockSoundEvents = false;
    this.LoadScene();
  }

  private void CheckDemoFinished()
  {
    if (Game.instance.championshipManager.GetChampionshipByID(0).eventNumberForUI <= 3)
      return;
    GenericConfirmation dialog = UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>();
    UIManager.instance.blur.Show(dialog.gameObject, UIManager.instance.dialogBoxManager.canvas);
    Action toMainMenuAction = MMDropdown.GetQuitToMainMenuAction();
    string inTitle = "Thank You For Playing!";
    string inText = "You have reached the end of the demo!\nThank you for playing!\nWe hope you had fun!";
    string inConfirmString = Localisation.LocaliseID("PSG_10009081", (GameObject) null);
    dialog.Show((Action) null, string.Empty, toMainMenuAction, inConfirmString, inText, inTitle);
  }

  private void LoadScene()
  {
    GameUtility.SetActive(this.carCamera, this.screenMode == UIScreen.ScreenMode.Mode3D);
    GameUtility.SetActive(this.carBackgroundParent, this.screenMode == UIScreen.ScreenMode.Mode2D);
    if (this.screenMode == UIScreen.ScreenMode.Mode3D)
    {
      SceneManager.instance.SwitchScene("TrackFrontEnd");
      this.mStudioScene = SceneManager.instance.GetSceneGameObject("TrackFrontEnd").GetComponent<StudioScene>();
      if (this.mChampionship != null)
        this.mStudioScene.SetSeries(this.mChampionship.series);
      this.mStudioScene.SetCarType(StudioScene.Car.CurrentCar);
      this.mStudioScene.SetCarVisualsToCurrentGame();
      this.mStudioScene.EnableCamera("ToTextureProfileCamera");
    }
    else
      this.carBackground.SetCar(Game.instance.player.team);
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionHome, 0.0f);
  }

  public override void OnExit()
  {
    base.OnExit();
    this.miniStandingsWidget.OnExit();
    if (!(App.instance.gameStateManager.currentState is FrontendState))
      return;
    this.UnloadScene();
  }

  private void UnloadScene()
  {
    if (this.screenMode != UIScreen.ScreenMode.Mode3D)
      return;
    this.mStudioScene.TuneSpotlight(true);
    this.mStudioScene.SetCameraTargetToTrackAlongCar(false);
    SceneManager.instance.LeaveCurrentScene();
  }
}
