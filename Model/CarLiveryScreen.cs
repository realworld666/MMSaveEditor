// Decompiled with JetBrains decompiler
// Type: CarLiveryScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class CarLiveryScreen : UIScreen
{
  public LiveryOptionsWidget liveryWidget;
  public GameObject car2DMode;
  private StudioScene mStudioScene;

  public override void OnStart()
  {
    base.OnStart();
    this.dontAddToBackStack = false;
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.canUseScreenHotkeys = false;
    this.canEnterPreferencesScreen = false;
    this.needsPlayerConfirmation = true;
    this.showNavigationBars = true;
    GameUtility.SetActive(this.car2DMode, this.screenMode == UIScreen.ScreenMode.Mode2D);
    this.liveryWidget.Setup();
    if (App.instance.gameStateManager.currentState is FrontendState)
    {
      this.SetBottomBarMode(UIBottomBar.Mode.PlayerAction);
    }
    else
    {
      if (Game.IsActive())
        UIManager.instance.ClearNavigationStacks();
      this.SetBottomBarMode(UIBottomBar.Mode.Core);
    }
    UIManager.instance.navigationBars.bottomBar.MarkContinueButtonForUpdate();
    this.LoadScene();
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionFactory, 0.0f);
  }

  public override UIScreen.NavigationButtonEvent OnContinueButton()
  {
    this.OpenConfirmDialogBox((Action) null);
    return UIScreen.NavigationButtonEvent.HandledByScreen;
  }

  public override UIScreen.NavigationButtonEvent OnCancelButton()
  {
    UIManager.instance.OnBackButton();
    return UIScreen.NavigationButtonEvent.HandledByScreen;
  }

  public override UIScreen.NavigationButtonEvent OnBackButton()
  {
    this.needsPlayerConfirmation = false;
    this.liveryWidget.RevertChanges();
    return UIScreen.NavigationButtonEvent.LetGameStateHandle;
  }

  public override void OnExit()
  {
    ColorPickerDialogBox.Close();
    this.liveryWidget.OnExit();
    base.OnExit();
    if ((UnityEngine.Object) this.mStudioScene != (UnityEngine.Object) null)
    {
      this.mStudioScene.TuneSpotlight(true);
      this.mStudioScene.SetCameraTargetToTrackAlongCar(false);
    }
    SceneManager.instance.LeaveCurrentScene();
  }

  private void LoadScene()
  {
    SceneManager.instance.SwitchScene("TrackFrontEnd");
    GameObject sceneGameObject = SceneManager.instance.GetSceneGameObject("TrackFrontEnd");
    if (!((UnityEngine.Object) sceneGameObject != (UnityEngine.Object) null) || this.screenMode != UIScreen.ScreenMode.Mode3D)
      return;
    this.mStudioScene = sceneGameObject.GetComponent<StudioScene>();
    this.mStudioScene.SetSeries(Game.instance.player.team.championship.series);
    Transform transform;
    if (App.instance.gameStateManager.currentState.type == GameState.Type.PreSeasonState)
    {
      this.mStudioScene.SetCarType(StudioScene.Car.NextYearCar);
      transform = Game.instance.player.team.carManager.nextFrontendCar.gameObject.transform;
    }
    else
    {
      this.mStudioScene.SetCarType(StudioScene.Car.CurrentCar);
      transform = Game.IsActive() ? Game.instance.player.team.carManager.frontendCar.gameObject.transform : CreateTeamManager.newTeam.carManager.frontendCar.gameObject.transform;
    }
    this.mStudioScene.SetCarVisualsToCurrentGame();
    this.mStudioScene.EnableCamera(this.mStudioScene.GetTeamSelectCameraString(Game.instance.player.team.championship.series));
    GameCamera component = this.mStudioScene.GetCamera(this.mStudioScene.GetTeamSelectCameraString(Game.instance.player.team.championship.series)).GetComponent<GameCamera>();
    component.freeRoamCamera.SetTarget(this.mStudioScene.GetCameraTArget(), CameraManager.Transition.Instant, -1f);
    component.freeRoamCamera.enabled = true;
    component.SetTiltShiftActive(false);
    component.depthOfField.focalTransform = transform;
    this.mStudioScene.TuneSpotlight(false);
    component.transform.localEulerAngles = new Vector3(30f, transform.eulerAngles.y + 135f, 0.0f);
    this.mStudioScene.SetCameraTargetToTrackAlongCar(true);
  }

  public void OpenFinances(Action inAction)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CarLiveryScreen.\u003COpenFinances\u003Ec__AnonStorey69 financesCAnonStorey69 = new CarLiveryScreen.\u003COpenFinances\u003Ec__AnonStorey69();
    // ISSUE: reference to a compiler-generated field
    financesCAnonStorey69.inAction = inAction;
    // ISSUE: reference to a compiler-generated field
    financesCAnonStorey69.\u003C\u003Ef__this = this;
    if (App.instance.gameStateManager.currentState is PreSeasonState)
    {
      CarManager carManager = Game.instance.player.team.carManager;
      if (carManager != null)
        carManager.UpdateThisYearFrontendCarWithNextYears();
      ((PreSeasonState) App.instance.gameStateManager.currentState).SetStage(PreSeasonState.PreSeasonStage.PreseasonTest);
      MovieScreen screen = UIManager.instance.GetScreen<MovieScreen>();
      screen.PlayTeamMovie(SoundID.Video_TeamLogo, Game.instance.player.team.teamID);
      screen.SetNextScreen("CarScreen", 1.5f, (Entity) null);
      Game.instance.time.Pause(GameTimer.PauseType.Game);
      UIManager.instance.ChangeScreen("MovieScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
    }
    else if (this.liveryWidget.hasLiveryChanged())
    {
      // ISSUE: reference to a compiler-generated method
      Action inOnTransactionSucess = new Action(financesCAnonStorey69.\u003C\u003Em__CB);
      // ISSUE: reference to a compiler-generated method
      Action inOnTransactionFail = new Action(financesCAnonStorey69.\u003C\u003Em__CC);
      Transaction transaction = new Transaction(Transaction.Group.Designer, Transaction.Type.Debit, GameStatsConstants.liveryEditCost, Localisation.LocaliseID("PSG_10010391", (GameObject) null));
      Game.instance.player.team.financeController.finance.ProcessTransactions(inOnTransactionSucess, inOnTransactionFail, true, transaction);
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      if (financesCAnonStorey69.inAction == null)
      {
        UIManager.instance.ChangeScreen("CarScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        financesCAnonStorey69.inAction();
      }
    }
  }

  public override void OpenConfirmDialogBox(Action inAction)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CarLiveryScreen.\u003COpenConfirmDialogBox\u003Ec__AnonStorey6A boxCAnonStorey6A = new CarLiveryScreen.\u003COpenConfirmDialogBox\u003Ec__AnonStorey6A();
    // ISSUE: reference to a compiler-generated field
    boxCAnonStorey6A.inAction = inAction;
    // ISSUE: reference to a compiler-generated field
    boxCAnonStorey6A.\u003C\u003Ef__this = this;
    GenericConfirmation dialog = UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>();
    Action inCancelAction = (Action) (() => {});
    // ISSUE: reference to a compiler-generated method
    Action inConfirmAction = new Action(boxCAnonStorey6A.\u003C\u003Em__CE);
    string inTitle = Localisation.LocaliseID("PSG_10009104", (GameObject) null);
    string inText = Localisation.LocaliseID("PSG_10009105", (GameObject) null);
    string inCancelString = Localisation.LocaliseID("PSG_10009077", (GameObject) null);
    string inConfirmString = Localisation.LocaliseID("PSG_10009078", (GameObject) null);
    UIManager.instance.dialogBoxManager.Show("GenericConfirmation");
    dialog.Show(inCancelAction, inCancelString, inConfirmAction, inConfirmString, inText, inTitle);
  }
}
