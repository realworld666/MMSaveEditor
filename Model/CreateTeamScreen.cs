// Decompiled with JetBrains decompiler
// Type: CreateTeamScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreateTeamScreen : UIScreen
{
  public List<GameObject> tabSteps = new List<GameObject>();
  public CreateTeamOverviewWidget overviewWidget;
  public CreateTeamOptionsWidget optionsWidget;
  public GameObject errorMessage;
  public GameObject car2DMode;
  private StudioScene mStudioScene;
  private FreeRoamCamera mFreeRoamCamera;

  public StudioScene studioScene
  {
    get
    {
      return this.mStudioScene;
    }
  }

  public FreeRoamCamera freeRoamCamera
  {
    get
    {
      return this.mFreeRoamCamera;
    }
  }

  public override void OnStart()
  {
    base.OnStart();
    this.optionsWidget.OnStart();
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.showNavigationBars = true;
    GameUtility.SetActive(this.car2DMode, this.screenMode == UIScreen.ScreenMode.Mode2D);
    this.SetTopBarMode(UITopBar.Mode.Core);
    this.SetBottomBarMode(UIBottomBar.Mode.Core);
    this.overviewWidget.Setup();
    this.optionsWidget.Setup();
    this.LoadScene();
  }

  public override void OnExit()
  {
    base.OnExit();
    this.optionsWidget.OnExit();
    if ((UnityEngine.Object) this.mStudioScene != (UnityEngine.Object) null)
    {
      this.mStudioScene.TuneSpotlight(true);
      this.mStudioScene.SetCameraTargetToTrackAlongCar(false);
    }
    SceneManager.instance.LeaveCurrentScene();
    ColorPickerDialogBox.Close();
  }

  private void Update()
  {
    if (!Input.GetKeyDown(KeyCode.Tab))
      return;
    this.GoToNextTabStep();
  }

  public void DisplayErrorMessage(bool inValue)
  {
    GameUtility.SetActive(this.errorMessage, !inValue);
  }

  public override UIScreen.NavigationButtonEvent OnContinueButton()
  {
    if (!CreateTeamManager.championship.IsBaseGameChampionship && Game.instance.tutorialSystem.isTutorialActive)
      Game.instance.tutorialSystem.SetTutorialActive(false);
    if (this.optionsWidget.StepsComplete())
    {
      ColorPickerDialogBox.Close();
      this.OpenConfirmDialogBox((Action) null);
    }
    else
      this.optionsWidget.GoToNextStep();
    return UIScreen.NavigationButtonEvent.HandledByScreen;
  }

  private void LoadScene()
  {
    SceneManager.instance.SwitchScene("TrackFrontEnd");
    GameObject sceneGameObject = SceneManager.instance.GetSceneGameObject("TrackFrontEnd");
    if (!((UnityEngine.Object) sceneGameObject != (UnityEngine.Object) null) || this.screenMode != UIScreen.ScreenMode.Mode3D)
      return;
    this.mStudioScene = sceneGameObject.GetComponent<StudioScene>();
    Championship.Series series = CreateTeamManager.newTeam.championship.series;
    this.mStudioScene.SetSeries(series);
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
    this.mStudioScene.EnableCamera(this.mStudioScene.GetTeamSelectCameraString(series));
    GameCamera component = this.mStudioScene.GetCamera(this.mStudioScene.GetTeamSelectCameraString(series)).GetComponent<GameCamera>();
    this.mFreeRoamCamera = component.freeRoamCamera;
    this.mFreeRoamCamera.SetTarget(this.mStudioScene.GetCameraTArget(), CameraManager.Transition.Instant, -1.25f);
    this.mFreeRoamCamera.enabled = true;
    component.SetTiltShiftActive(false);
    component.depthOfField.focalTransform = transform;
    this.mStudioScene.TuneSpotlight(false);
    component.transform.localEulerAngles = new Vector3(30f, transform.eulerAngles.y + 135f, 0.0f);
    this.mStudioScene.SetCameraTargetToTrackAlongCar(true);
    CreateTeamManager.newTeam.carManager.frontendCar.PreviewSponsors(CreateTeamManager.defaultSettings.defaultCarSponsors);
  }

  public override void OpenConfirmDialogBox(Action inAction)
  {
    GenericConfirmation dialog = UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>();
    Action inCancelAction = (Action) (() => {});
    Action inConfirmAction = (Action) (() => UITeamInvestorOffers.Open());
    string inTitle = Localisation.LocaliseID("PSG_10011899", (GameObject) null);
    string inText = Localisation.LocaliseID("PSG_10011900", (GameObject) null);
    string inCancelString = Localisation.LocaliseID("PSG_10009077", (GameObject) null);
    string inConfirmString = Localisation.LocaliseID("PSG_10009078", (GameObject) null);
    UIManager.instance.dialogBoxManager.Show("GenericConfirmation");
    dialog.Show(inCancelAction, inCancelString, inConfirmAction, inConfirmString, inText, inTitle);
  }

  public void StartGameInCustomTeam(Investor inInvestor)
  {
    Team newTeam = CreateTeamManager.CompleteCreateNewTeam(inInvestor);
    Game.instance.time.Pause(GameTimer.PauseType.Game);
    Game.instance.ManageTeam(newTeam);
    MovieScreen screen = UIManager.instance.GetScreen<MovieScreen>();
    screen.PlayTeamMovie(SoundID.Video_TeamLogo, newTeam.teamID);
    if (Game.instance.tutorialSystem.isTutorialActive)
    {
      Game.instance.time.SetTime(Game.instance.player.team.championship.GetCurrentEventDetails().currentSession.sessionDateTime);
      this.TutorialRemoveOldNotTriggeredEvents();
      newTeam.StoreTeamDataBeforeEvent();
      Game.instance.teamManager.UpdateSponsorObjectives(Game.instance.player.team.championship);
      List<SponsorshipDeal> sponsorshipDeals = Game.instance.player.team.sponsorController.sponsorshipDeals;
      if (sponsorshipDeals.Count > 0 && sponsorshipDeals[0] != null)
      {
        for (int index = 0; index < sponsorshipDeals.Count; ++index)
        {
          if (sponsorshipDeals[index].hasRaceBonusReward)
          {
            Game.instance.player.team.sponsorController.SetWeekendSponsor(sponsorshipDeals[index]);
            break;
          }
        }
      }
      else
        Game.instance.player.team.sponsorController.SetWeekendSponsor((SponsorshipDeal) null);
      screen.SetNextState(GameState.Type.EventLoading);
      Game.instance.stateInfo.SetStateToGoToAfterPlayerConfirms(GameState.Type.RacePreSession);
      newTeam.financeController.SetRacePayment(TeamFinanceController.RacePaymentType.Medium);
      newTeam.ReceiveAllChairmanPayments(Chairman.EstimatedPosition.Medium, (Action) null, (Action) null, false);
      newTeam.chairman.playerChosenExpectedTeamChampionshipPosition = newTeam.chairman.GetEstimatedPosition(Chairman.EstimatedPosition.Medium, newTeam);
      newTeam.canReceiveFullChairmanPayments = false;
      UITutorialMarkAllMessagesRead.MarkAllMessagesReadForTutorial();
    }
    else
    {
      screen.SetNextState(GameState.Type.FrontendLoading);
      Game.instance.stateInfo.SetStateToGoToAfterPlayerConfirms(GameState.Type.FrontendState);
    }
    UIManager.instance.ChangeScreen("MovieScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
    UIManager.instance.ClearNavigationStacks();
  }

  public void TutorialRemoveOldNotTriggeredEvents()
  {
    List<CalendarEvent_v1> calendarEvents = Game.instance.calendar.calendarEvents;
    for (int index = 0; index < calendarEvents.Count; ++index)
    {
      if (Game.instance.time.now >= calendarEvents[index].triggerDate && calendarEvents[index].showOnCalendar)
        calendarEvents.RemoveAt(index);
    }
  }

  private void GoToNextTabStep()
  {
    for (int index = 0; index < this.tabSteps.Count; ++index)
    {
      if ((UnityEngine.Object) EventSystem.current.currentSelectedGameObject == (UnityEngine.Object) this.tabSteps[index])
      {
        if (index < this.tabSteps.Count - 1)
        {
          int num;
          EventSystem.current.SetSelectedGameObject(this.tabSteps[num = index + 1]);
          break;
        }
        EventSystem.current.SetSelectedGameObject(this.tabSteps[0]);
        break;
      }
    }
  }
}
