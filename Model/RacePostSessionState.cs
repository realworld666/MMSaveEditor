// Decompiled with JetBrains decompiler
// Type: RacePostSessionState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class RacePostSessionState : PostSessionState
{
  public override GameState.Type type
  {
    get
    {
      return GameState.Type.RacePostSession;
    }
  }

  public override void OnEnter(bool fromSave = false)
  {
    if (Game.instance.tutorialSystem.isTutorialActive)
    {
      Game.instance.dialogSystem.OnTutorialRaceFinished();
      Game.instance.tutorialSystem.SetTutorialSectionComplete(TutorialSystem_v1.TutorialSection.HasRunFirstRace);
    }
    Game.instance.sessionManager.RecordConditionAfterEvent();
    if (Game.instance.gameType != Game.GameType.SingleEvent)
      Game.instance.dialogSystem.OnEventEndMessages();
    base.OnEnter(fromSave);
  }

  public override void OnExit(GameState inNextState)
  {
    base.OnExit(inNextState);
    if (Game.instance.gameType != Game.GameType.SingleEvent)
      return;
    Game.instance.Destroy();
    Game.instance = (Game) null;
  }

  public override void GetFirstScreenForState(out string screenName, out UIManager.ScreenTransition screenTransition, out float transitionDuration, bool fromSave = false)
  {
    screenName = Game.instance.gameType != Game.GameType.SingleEvent ? "ScrutineeringScreen" : "RaceResultsScreen";
    screenTransition = UIManager.ScreenTransition.Fade;
    transitionDuration = 1.5f;
  }

  public override GameState.Type GetNextState()
  {
    RaceEventDetails eventDetails = Game.instance.sessionManager.eventDetails;
    GameState.Type type = GameState.Type.None;
    switch (eventDetails.currentSession.sessionType)
    {
      case SessionDetails.SessionType.Practice:
        type = GameState.Type.PracticePreSession;
        break;
      case SessionDetails.SessionType.Qualifying:
        type = GameState.Type.QualifyingPreSession;
        break;
      case SessionDetails.SessionType.Race:
        type = GameState.Type.None;
        break;
    }
    return type;
  }

  public override void OnContinueButton()
  {
    if (Game.instance.isCareer)
    {
      Game.instance.sessionManager.championship.GoToNextSession();
      if (Game.instance.sessionManager.eventDetails.hasEventEnded)
      {
        Game.instance.sessionManager.championship.EndEvent();
        App.instance.gameStateManager.LoadToFrontend(GameStateManager.StateChangeType.CheckForFadedScreenChange);
      }
      else
        Game.instance.stateInfo.GoToNextState();
    }
    else
      App.instance.gameStateManager.LoadToTitleScreen(GameStateManager.StateChangeType.CheckForFadedScreenChange);
  }
}
