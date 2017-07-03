// Decompiled with JetBrains decompiler
// Type: CarScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class CarScreen : UIScreen
{
  public UICarScreenOptionsWidget optionsWidget;
  public UICarScreenCarStatsWidget statsWidget;
  public UICarSupplierRolloverTrigger supplierRollover;
  private StudioScene mStudioScene;

  public override void OnStart()
  {
    base.OnStart();
    this.optionsWidget.OnStart();
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.supplierRollover.Setup(Game.instance.player.team, false);
    this.showNavigationBars = true;
    this.LoadScene();
    this.statsWidget.Setup();
    this.optionsWidget.OnEnter();
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionHome, 0.0f);
  }

  public override void OnExit()
  {
    base.OnExit();
    if ((Object) this.mStudioScene != (Object) null)
    {
      this.mStudioScene.TuneSpotlight(true);
      this.mStudioScene.SetCameraTargetToTrackAlongCar(false);
    }
    SceneManager.instance.LeaveCurrentScene();
    this.optionsWidget.OnExit();
  }

  private void LoadScene()
  {
    SceneManager.instance.SwitchScene("TrackFrontEnd");
    GameObject sceneGameObject = SceneManager.instance.GetSceneGameObject("TrackFrontEnd");
    if (!((Object) sceneGameObject != (Object) null) || this.screenMode != UIScreen.ScreenMode.Mode3D)
      return;
    this.mStudioScene = sceneGameObject.GetComponent<StudioScene>();
    this.mStudioScene.SetSeries(Game.instance.player.team.championship.series);
    Transform transform;
    if (App.instance.gameStateManager.currentState is FrontendState)
    {
      this.mStudioScene.SetCarType(StudioScene.Car.CurrentCar);
      transform = Game.instance.player.team.carManager.frontendCar.gameObject.transform;
    }
    else
    {
      PreSeasonState currentState = (PreSeasonState) App.instance.gameStateManager.currentState;
      if (currentState.stage > PreSeasonState.PreSeasonStage.DesigningCar && currentState.stage < PreSeasonState.PreSeasonStage.ChooseLivery)
      {
        this.mStudioScene.SetCarType(StudioScene.Car.NextYearCar);
        transform = Game.instance.player.team.carManager.nextFrontendCar.gameObject.transform;
      }
      else
      {
        this.mStudioScene.SetCarType(StudioScene.Car.CurrentCar);
        transform = Game.instance.player.team.carManager.frontendCar.gameObject.transform;
      }
    }
    this.mStudioScene.SetCarVisualsToCurrentGame();
    this.mStudioScene.EnableCamera(this.mStudioScene.GetTeamSelectCameraString(Game.instance.player.team.championship.series));
    GameCamera component = this.mStudioScene.GetCamera(this.mStudioScene.GetTeamSelectCameraString(Game.instance.player.team.championship.series)).GetComponent<GameCamera>();
    component.freeRoamCamera.SetTarget(this.mStudioScene.GetCameraTArget(), CameraManager.Transition.Instant, 0.5f);
    component.freeRoamCamera.enabled = true;
    component.SetTiltShiftActive(false);
    component.depthOfField.focalTransform = transform;
    this.mStudioScene.TuneSpotlight(false);
    component.transform.localEulerAngles = new Vector3(30f, transform.eulerAngles.y + 135f, 0.0f);
    this.mStudioScene.SetCameraTargetToTrackAlongCar(true);
  }
}
