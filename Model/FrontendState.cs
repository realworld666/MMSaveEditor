// Decompiled with JetBrains decompiler
// Type: FrontendState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class FrontendState : GameState
{
  public override GameState.Type type
  {
    get
    {
      return GameState.Type.FrontendState;
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

  public override void OnEnter(bool fromSave = false)
  {
    base.OnEnter(fromSave);
    this.SetAsSaveEntryPoint();
    Game.instance.time.Pause(GameTimer.PauseType.Game);
    if (!Game.instance.tutorialSystem.IsTutorialSectionComplete(TutorialSystem_v1.TutorialSection.SkipTeamReportScreen))
    {
      List<Transaction> unnallocatedTransactions = Game.instance.player.team.financeController.unnallocatedTransactions;
      for (int index = 0; index < unnallocatedTransactions.Count; ++index)
        Game.instance.player.team.financeController.finance.ProcessTransactions((Action) null, (Action) null, false, unnallocatedTransactions[index]);
      Championship[] championshipsRacingToday = Game.instance.championshipManager.GetChampionshipsRacingToday(true, Championship.Series.Count);
      for (int index = 0; index < championshipsRacingToday.Length; ++index)
      {
        if (!championshipsRacingToday[index].isPlayerChampionship)
        {
          SessionSimulation.SimulateEvent(championshipsRacingToday[index]);
          Debug.Log((object) ("Event Simulated -> " + championshipsRacingToday[index].GetAcronym(false) + " - " + Localisation.LocaliseID(championshipsRacingToday[index].GetCurrentEventDetails().circuit.locationNameID, (GameObject) null)), (UnityEngine.Object) null);
        }
      }
      Game.instance.tutorialSystem.SetTutorialSectionComplete(TutorialSystem_v1.TutorialSection.SkipTeamReportScreen);
    }
    if (fromSave)
      return;
    App.instance.saveSystem.TryDispatchDelayedAutoSave();
  }

  public override void GetFirstScreenForState(out string screenName, out UIManager.ScreenTransition screenTransition, out float transitionDuration, bool fromSave = false)
  {
    screenName = Game.instance.player.IsUnemployed() ? "PlayerScreen" : "HomeScreen";
    if (App.instance.gameStateManager.currentState is PreSeasonState || App.instance.gameStateManager.currentState is PostEventFrontendState)
    {
      screenTransition = UIManager.ScreenTransition.None;
      transitionDuration = 0.0f;
    }
    else
    {
      screenTransition = UIManager.ScreenTransition.Fade;
      transitionDuration = 1.5f;
    }
  }

  public override void Update()
  {
    base.Update();
    this.CheckForEscapeButton();
    Game instance = Game.instance;
    instance.time.UpdateInput();
    instance.time.Update();
    instance.calendar.Update();
    instance.messageManager.DeliverDelayedMessages();
    instance.entityManager.Update();
    UIManager.instance.navigationBars.SetContinueInteractable(Game.instance.stateInfo.isReadyToGoToRace || Game.instance.stateInfo.isReadyToSimulateRace);
  }

  public override void OnContinueButton()
  {
    base.OnContinueButton();
    if (Game.instance.stateInfo.isReadyToSimulateRace)
    {
      Game.instance.stateInfo.GoToNextState();
    }
    else
    {
      if (!Game.instance.stateInfo.isReadyToGoToRace)
        return;
      UIManager.instance.ChangeScreen("TravelArrangementsScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
    }
  }

  public static bool PlayerCanAccessScreen(string inScreen)
  {
    return FrontendState.PlayerCanAccessScreen(UIManager.instance.GetScreen(inScreen));
  }

  public static bool PlayerCanAccessScreen(UIScreen inScreen)
  {
    if (Game.IsActive() && Game.instance.player.IsUnemployed() && (!(inScreen is PlayerScreen) && !(inScreen is MailScreen) && (!(inScreen is StandingsScreen) && !(inScreen is EventCalendarScreen)) && (!(inScreen is TeamScreen) && !(inScreen is DriverScreen) && !(inScreen is StaffDetailsScreen))))
      return inScreen is EventSummaryScreen;
    return true;
  }
}
