// Decompiled with JetBrains decompiler
// Type: ChooseTeamScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseTeamScreen : UIScreen
{
  private List<Team> mTeams = new List<Team>();
  private List<ChooseTeamEntry> mTeamsEntry = new List<ChooseTeamEntry>();
  public ChooseTeamOverview teamOverviewWidget;
  public ToggleGroup toggleGroup;
  public UIGridList teamGrid;
  public UICarBackground carBackground;
  private Team mSelectedTeam;
  private Championship mChampionship;
  private StudioScene mStudioScene;
  private FrontendCar mStudioCar;
  private bool mAllowMenuSounds;

  public Championship championship
  {
    get
    {
      return this.mChampionship;
    }
  }

  public override void OnStart()
  {
    base.OnStart();
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.mAllowMenuSounds = false;
    this.showNavigationBars = true;
    this.continueButtonLabel = Localisation.LocaliseID("PSG_10002716", (GameObject) null);
    this.mChampionship = (Championship) this.data;
    if (this.mChampionship == null)
      this.mChampionship = Game.instance.championshipManager.GetMainChampionship(Championship.Series.SingleSeaterSeries);
    this.SetTopBarMode(UITopBar.Mode.Core);
    this.SetBottomBarMode(UIBottomBar.Mode.Core);
    this.SetupTeamsEntry();
    this.LoadScene();
    this.SelectTeam(this.mSelectedTeam, true);
    this.mAllowMenuSounds = true;
  }

  public override void OnExit()
  {
    base.OnExit();
    this.DestroyTeamsEntry();
    if (this.screenMode != UIScreen.ScreenMode.Mode3D)
      return;
    this.mStudioScene.TuneSpotlight(true);
    this.mStudioScene.SetCameraTargetToTrackAlongCar(false);
    SceneManager.instance.LeaveCurrentScene();
  }

  private void LoadScene()
  {
    GameUtility.SetActive(this.carBackground.gameObject, this.screenMode == UIScreen.ScreenMode.Mode2D);
    if (this.screenMode != UIScreen.ScreenMode.Mode3D)
      return;
    SceneManager.instance.SwitchScene("TrackFrontEnd");
    this.mStudioScene = SceneManager.instance.GetSceneGameObject("TrackFrontEnd").GetComponent<StudioScene>();
    this.mStudioScene.SetSeries(this.mChampionship.series);
    this.mStudioScene.SetDefaultCarVisuals(this.mSelectedTeam.teamID, this.mChampionship.championshipID);
    this.mStudioCar = this.mStudioScene.GetGeneratedCar(0);
    this.mStudioScene.EnableCamera(this.mStudioScene.GetTeamSelectCameraString(this.mChampionship.series));
    GameCamera component = this.mStudioScene.GetCamera(this.mStudioScene.GetTeamSelectCameraString(this.mChampionship.series)).GetComponent<GameCamera>();
    component.freeRoamCamera.SetTarget(this.mStudioScene.GetCameraTArget(), CameraManager.Transition.Instant, 0.0f);
    component.freeRoamCamera.enabled = true;
    Transform transform = this.mStudioCar.gameObject.transform;
    component.SetTiltShiftActive(false);
    component.depthOfField.focalTransform = this.mStudioCar.gameObject.transform;
    this.mStudioScene.TuneSpotlight(false);
    component.transform.localEulerAngles = new Vector3(30f, transform.eulerAngles.y + 125f, 0.0f);
    this.mStudioScene.SetCameraTargetToTrackAlongCar(true);
  }

  private void SetupTeamsEntry()
  {
    this.mTeams.Clear();
    int teamEntryCount = this.mChampionship.standings.teamEntryCount;
    for (int inIndex = 0; inIndex < teamEntryCount; ++inIndex)
      this.mTeams.Add(this.mChampionship.standings.GetTeamEntry(inIndex).GetEntity<Team>());
    this.mTeams.Sort((Comparison<Team>) ((x, y) => x.GetStarsStat().CompareTo(y.GetStarsStat())));
    this.mTeams.Reverse();
    this.mSelectedTeam = this.mTeams[0];
    for (int index = 0; index < teamEntryCount; ++index)
    {
      Team mTeam = this.mTeams[index];
      if (Game.instance.gameType != Game.GameType.Career || !mTeam.isBlockedByChallenge)
      {
        this.mSelectedTeam = mTeam;
        break;
      }
    }
    GameUtility.SetActive(this.teamGrid.itemPrefab, true);
    for (int index = teamEntryCount - 1; index >= 0; --index)
    {
      Team mTeam = this.mTeams[index];
      bool inValue = Game.instance.gameType == Game.GameType.Career && mTeam.isBlockedByChallenge;
      ChooseTeamEntry listItem = this.teamGrid.CreateListItem<ChooseTeamEntry>();
      listItem.Setup(mTeam);
      this.mTeamsEntry.Add(listItem);
      listItem.SetLocked(inValue);
      listItem.OnStart();
    }
    this.toggleGroup.SetAllTogglesOff();
    GameUtility.SetActive(this.teamGrid.itemPrefab, false);
  }

  public void DestroyTeamsEntry()
  {
    for (int index = 0; index < this.mTeamsEntry.Count; ++index)
      this.mTeamsEntry[index].toggle.onValueChanged.RemoveAllListeners();
    this.mTeamsEntry.Clear();
    this.teamGrid.DestroyListItems();
  }

  public void HighlightTeamEntry()
  {
    int count = this.mTeamsEntry.Count;
    for (int index = 0; index < count; ++index)
    {
      ChooseTeamEntry chooseTeamEntry = this.mTeamsEntry[index];
      chooseTeamEntry.toggle.isOn = chooseTeamEntry.team == this.mSelectedTeam;
      chooseTeamEntry.Highlight(chooseTeamEntry.team == this.mSelectedTeam);
    }
  }

  public void SelectTeam(Team inTeam, bool ForceSelect)
  {
    if (this.mSelectedTeam == inTeam && !ForceSelect)
      return;
    if (this.mSelectedTeam != inTeam && this.mAllowMenuSounds)
      scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionTeam, 0.0f);
    this.mSelectedTeam = inTeam;
    this.teamOverviewWidget.Setup(this.mSelectedTeam);
    App.instance.uiTeamColourManager.SetCurrentColour(App.instance.teamColorManager.GetColor(this.mSelectedTeam.colorID), true, (GameObject) null);
    this.HighlightTeamEntry();
    if (this.screenMode == UIScreen.ScreenMode.Mode3D && (UnityEngine.Object) this.mStudioScene != (UnityEngine.Object) null && this.mStudioCar != null)
    {
      this.mStudioScene.ResetMode();
      this.mStudioScene.SetDefaultCarVisuals(inTeam.teamID, this.mChampionship.championshipID);
      this.mStudioCar = this.mStudioScene.GetGeneratedCar(0);
    }
    this.SetCarColors();
    if (this.screenMode != UIScreen.ScreenMode.Mode3D)
      return;
    this.mStudioCar.SetWheelModel(Car.GetWheelID(this.mChampionship.championshipID), this.mChampionship.championshipID, App.instance.carPartModelDatabase);
  }

  private void SetCarColors()
  {
    if (this.screenMode == UIScreen.ScreenMode.Mode3D && this.mStudioCar != null)
    {
      LiveryData livery = Game.instance.liveryManager.GetLivery(this.mSelectedTeam.liveryID);
      this.mStudioCar.SetColours(this.mSelectedTeam.GetTeamColor().livery);
      this.mStudioCar.SetLiveryData(livery);
      SponsorSlot[] slots = this.mSelectedTeam.sponsorController.slots;
      for (int sponsorSlotIndex = 0; sponsorSlotIndex < 6; ++sponsorSlotIndex)
      {
        SponsorSlot sponsorSlot = slots[sponsorSlotIndex];
        int sponsorLogoId = sponsorSlot == null || sponsorSlot.sponsorshipDeal == null ? 0 : sponsorSlot.sponsorshipDeal.sponsor.logoIndex;
        this.mStudioCar.SetSponsorTexture(sponsorSlotIndex, sponsorLogoId);
      }
    }
    else
    {
      if (this.screenMode != UIScreen.ScreenMode.Mode2D)
        return;
      this.carBackground.SetCar(this.mSelectedTeam);
    }
  }

  public override UIScreen.NavigationButtonEvent OnContinueButton()
  {
    if (Game.instance.tutorialSystem.isTutorialActive && (!this.mChampionship.IsBaseGameChampionship || App.instance.modManager.GetNewGameModsCount(true) > 0))
      Game.instance.tutorialSystem.SetTutorialActive(false);
    Game.instance.time.Pause(GameTimer.PauseType.Game);
    Game.instance.ManageTeam(this.mSelectedTeam);
    MovieScreen screen = UIManager.instance.GetScreen<MovieScreen>();
    screen.PlayTeamMovie(SoundID.Video_TeamLogo, this.mSelectedTeam.teamID);
    if (App.instance.gameStateManager.currentState is QuickRaceSetupState)
    {
      QuickRaceSetupState state = (QuickRaceSetupState) App.instance.gameStateManager.GetState(GameState.Type.QuickRaceSetup);
      state.PrepareCircuit();
      if (state.raceWeekend == QuickRaceSetupState.RaceWeekend.RaceOnly)
        Game.instance.stateInfo.SetStateToGoToAfterPlayerConfirms(GameState.Type.RacePreSession);
      else
        Game.instance.stateInfo.SetStateToGoToAfterPlayerConfirms(GameState.Type.PracticePreSession);
      screen.SetNextState(GameState.Type.EventLoading);
    }
    else if (Game.instance.tutorialSystem.isTutorialActive)
    {
      Game.instance.time.SetTime(Game.instance.player.team.championship.GetCurrentEventDetails().currentSession.sessionDateTime);
      this.TutorialRemoveOldNotTriggeredEvents();
      Team team = Game.instance.player.team;
      team.StoreTeamDataBeforeEvent();
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
      Game.instance.stateInfo.SetStateToGoToAfterPlayerConfirms(GameState.Type.RacePreSession);
      screen.SetNextState(GameState.Type.EventLoading);
      team.financeController.SetRacePayment(TeamFinanceController.RacePaymentType.Medium);
      team.chairman.playerChosenExpectedTeamChampionshipPosition = team.chairman.GetEstimatedPosition(Chairman.EstimatedPosition.Medium, team);
      UITutorialMarkAllMessagesRead.MarkAllMessagesReadForTutorial();
    }
    else
    {
      screen.SetNextState(GameState.Type.FrontendLoading);
      Game.instance.stateInfo.SetStateToGoToAfterPlayerConfirms(GameState.Type.FrontendState);
    }
    UIManager.instance.ChangeScreen("MovieScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
    UIManager.instance.ClearNavigationStacks();
    return UIScreen.NavigationButtonEvent.HandledByScreen;
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
}
