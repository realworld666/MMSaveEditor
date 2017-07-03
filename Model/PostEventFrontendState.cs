// Decompiled with JetBrains decompiler
// Type: PostEventFrontendState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class PostEventFrontendState : GameState
{
  private PostEventFrontendState.Stage mStage;
  private EventSummaryScreen eventSummaryScreen;

  public override GameState.Type type
  {
    get
    {
      return GameState.Type.PostEventFrontendState;
    }
  }

  public override UIManager.ScreenSet screenSet
  {
    get
    {
      return UIManager.ScreenSet.Frontend;
    }
  }

  public override GameState.Group group
  {
    get
    {
      return GameState.Group.Frontend;
    }
  }

  public PostEventFrontendState.Stage stage
  {
    get
    {
      return this.mStage;
    }
  }

  public override void OnEnter(bool fromSave = false)
  {
    base.OnEnter(fromSave);
    this.SetAsSaveEntryPoint();
    UIManager.instance.currentScreen.showNavigationBars = true;
    Game.instance.time.Pause(GameTimer.PauseType.Game);
    Championship[] championshipsRacingToday = Game.instance.championshipManager.GetChampionshipsRacingToday(true, Championship.Series.Count);
    for (int index = 0; index < championshipsRacingToday.Length; ++index)
    {
      if (!championshipsRacingToday[index].isPlayerChampionship)
      {
        SessionSimulation.SimulateEvent(championshipsRacingToday[index]);
        Debug.Log((object) ("Event Simulated -> " + championshipsRacingToday[index].GetAcronym(false) + " - " + Localisation.LocaliseID(championshipsRacingToday[index].GetCurrentEventDetails().circuit.locationNameID, (GameObject) null)), (UnityEngine.Object) null);
      }
    }
    UIManager.instance.navigationBars.topBar.SetMode(UITopBar.Mode.Frontend);
    UIManager.instance.navigationBars.bottomBar.SetMode(UIBottomBar.Mode.Core);
    UIManager.instance.navigationBars.bottomBar.MarkContinueButtonForUpdate();
    this.SetStage(PostEventFrontendState.Stage.TeamReport);
  }

  public override void OnExit(GameState inNextState)
  {
    base.OnExit(inNextState);
    Game.instance.queuedAutosave = true;
  }

  public override void GetFirstScreenForState(out string screenName, out UIManager.ScreenTransition screenTransition, out float transitionDuration, bool fromSave = false)
  {
    screenName = "TeamReportScreen";
    screenTransition = UIManager.ScreenTransition.Fade;
    transitionDuration = 0.5f;
  }

  public override void Update()
  {
    base.Update();
    this.CheckForEscapeButton();
    UIManager.instance.navigationBars.SetContinueInteractable(this.IsContinueInteractable());
  }

  private bool IsContinueInteractable()
  {
    switch (this.mStage)
    {
      case PostEventFrontendState.Stage.TeamReport:
      case PostEventFrontendState.Stage.WeekendSummary:
      case PostEventFrontendState.Stage.PlayerSeriesDriverChampion:
      case PostEventFrontendState.Stage.PlayerSeriesTeamChampion:
      case PostEventFrontendState.Stage.ChallengeAwardScreen:
      case PostEventFrontendState.Stage.UltimatumCheck:
      case PostEventFrontendState.Stage.Complete:
        return true;
      case PostEventFrontendState.Stage.AllocateFunds:
        if (!UIManager.instance.dialogBoxManager.GetDialog<FinancePopupWidget>().isActiveAndEnabled)
          return Game.instance.player.team.financeController.unnallocatedTransactions.Count == 0;
        return false;
      default:
        return false;
    }
  }

  private void NextStage()
  {
    this.SetStage(this.GetNextStage());
    UIManager.instance.navigationBars.bottomBar.MarkContinueButtonForUpdate();
    UIManager.instance.ClearNavigationStacks();
  }

  private bool SkipStage(PostEventFrontendState.Stage inStage)
  {
    if (Game.instance.player.IsUnemployed() && inStage != PostEventFrontendState.Stage.UltimatumResult && inStage != PostEventFrontendState.Stage.Complete)
      return true;
    switch (inStage)
    {
      case PostEventFrontendState.Stage.UltimatumResult:
        if (!Game.instance.player.IsUnemployed() && !Game.instance.player.team.chairman.hasMadeUltimatum)
          return true;
        break;
      case PostEventFrontendState.Stage.AllocateFunds:
        if (Game.instance.player.IsUnemployed() || Game.instance.player.team.financeController.unnallocatedTransactions.Count == 0)
          return true;
        break;
      case PostEventFrontendState.Stage.WeekendSummary:
        return Game.instance.championshipManager.GetChampionshipsRacingToday(false, Game.instance.player.team.championship.series).Length == 0;
      case PostEventFrontendState.Stage.PlayerSeriesDriverChampion:
      case PostEventFrontendState.Stage.PlayerSeriesTeamChampion:
      case PostEventFrontendState.Stage.ChallengeAwardScreen:
        Championship championship = Game.instance.player.team.championship;
        if (!championship.HasSeasonEnded())
          return true;
        if (inStage == PostEventFrontendState.Stage.ChallengeAwardScreen)
          return !championship.IsChallengeCompleteOnChampionshipsAwarded();
        break;
      case PostEventFrontendState.Stage.UltimatumCheck:
        if (!Game.instance.player.IsUnemployed() && !Game.instance.player.team.chairman.CanMakeUltimatum())
          return true;
        break;
    }
    return false;
  }

  private void SetStage(PostEventFrontendState.Stage inStage)
  {
    this.mStage = inStage;
    switch (this.mStage)
    {
      case PostEventFrontendState.Stage.UltimatumResult:
        UIManager.instance.ChangeScreen("FinanceScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
        if (Game.instance.isCareer && !Game.instance.player.IsUnemployed() && Game.instance.player.team.chairman.hasMadeUltimatum)
          Game.instance.player.team.chairman.CompleteUltimatum();
        if (!Game.instance.player.IsUnemployed())
        {
          this.SetStage(PostEventFrontendState.Stage.AllocateFunds);
          break;
        }
        this.NextStage();
        break;
      case PostEventFrontendState.Stage.AllocateFunds:
        TeamFinanceController.ShowPopupToPlayer((Action) (() => this.NextStage()));
        break;
      case PostEventFrontendState.Stage.WeekendSummary:
        UIManager.instance.ChangeScreen("EventSummaryScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
        break;
      case PostEventFrontendState.Stage.PlayerSeriesDriverChampion:
      case PostEventFrontendState.Stage.PlayerSeriesTeamChampion:
      case PostEventFrontendState.Stage.ChallengeAwardScreen:
        Championship championship = Game.instance.player.team.championship;
        if (!championship.HasSeasonEnded())
          break;
        int year = Game.instance.time.now.Year;
        ChampionshipWinnersEntry inEntry = new ChampionshipWinnersEntry(championship.standings.GetDriverEntry(0), championship.standings.GetTeamEntry(0), year);
        if (this.mStage == PostEventFrontendState.Stage.PlayerSeriesDriverChampion)
        {
          championship.OnDriverChampionshipAwardedAchievements();
          UIManager.instance.ChangeScreen("SeriesResultsDriverScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
          UIManager.instance.GetScreen<SeriesResultsDriverScreen>().Setup(inEntry, championship);
          break;
        }
        if (this.mStage == PostEventFrontendState.Stage.PlayerSeriesTeamChampion)
        {
          championship.OnTeamChampionshipAwardedAchievements();
          UIManager.instance.ChangeScreen("SeriesResultsTeamScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
          UIManager.instance.GetScreen<SeriesResultsTeamScreen>().Setup(inEntry, championship);
          break;
        }
        if (this.mStage != PostEventFrontendState.Stage.ChallengeAwardScreen)
          break;
        UIManager.instance.ChangeScreen("ChallengeRewardScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
        UIManager.instance.GetScreen<ChallengeRewardScreen>().Setup(Game.instance.challengeManager.currentChallenge.challengeName);
        break;
      case PostEventFrontendState.Stage.UltimatumCheck:
        if (Game.instance.player.IsUnemployed() || !Game.instance.isCareer)
          break;
        Game.instance.player.team.chairman.CheckUltimatum();
        break;
      case PostEventFrontendState.Stage.Complete:
        App.instance.gameStateManager.SetState(GameState.Type.FrontendState, GameStateManager.StateChangeType.CheckForFadedScreenChange, false);
        break;
    }
  }

  public PostEventFrontendState.Stage GetNextStage()
  {
    PostEventFrontendState.Stage inStage = this.mStage + 1;
    while (this.SkipStage(inStage))
      ++inStage;
    return inStage;
  }

  public override GameState.Type GetNextState()
  {
    return GameState.Type.FrontendState;
  }

  public override void OnContinueButton()
  {
    base.OnContinueButton();
    this.eventSummaryScreen = UIManager.instance.currentScreen as EventSummaryScreen;
    if ((UnityEngine.Object) this.eventSummaryScreen != (UnityEngine.Object) null && !this.eventSummaryScreen.isComplete)
    {
      int num = (int) this.eventSummaryScreen.OnContinueButton();
    }
    else
      this.NextStage();
  }

  public enum Stage
  {
    [LocalisationID("needsID")] TeamReport,
    [LocalisationID("needsID")] UltimatumResult,
    [LocalisationID("PSG_10004851")] AllocateFunds,
    [LocalisationID("PSG_10004846")] WeekendSummary,
    [LocalisationID("PSG_10004849")] PlayerSeriesDriverChampion,
    [LocalisationID("PSG_10004850")] PlayerSeriesTeamChampion,
    [LocalisationID("needsID")] ChallengeAwardScreen,
    [LocalisationID("needsID")] UltimatumCheck,
    [LocalisationID("PSG_10004852")] Complete,
  }
}
