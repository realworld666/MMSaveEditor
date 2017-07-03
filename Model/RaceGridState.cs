// Decompiled with JetBrains decompiler
// Type: RaceGridState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class RaceGridState : SessionState
{
  public override GameState.Type type
  {
    get
    {
      return GameState.Type.RaceGrid;
    }
  }

  public override void OnEnter(bool fromSave = false)
  {
    base.OnEnter(fromSave);
    Game.instance.sessionManager.circuit.ClearGridCrowds();
    Game.instance.sessionManager.SetCircuitActive(true);
    Game.instance.sessionManager.SessionStarting();
    App.instance.cameraManager.SetTarget((Vehicle) Game.instance.vehicleManager.GetVehicle(Game.instance.player.team.GetSelectedDriver(0)), CameraManager.Transition.Smooth);
    Game.instance.time.SetSpeed(GameTimer.Speed.Slow);
    Game.instance.sessionManager.SetEndCondition(SessionManager.EndCondition.LapCount);
    Game.instance.vehicleManager.PrepareForSession();
    UIManager.instance.ClearBackStack();
    UIManager.instance.GetScreen<SessionHUD>().sponsorStatusChangeWidget.OnSessionStart();
    this.StartCommentarySystem();
  }

  public override void SetStartingStandings()
  {
  }

  public override void UpdateStandings()
  {
    RaceSessionState.UpdateStandingsToTrackPosition();
  }

  public override void GetFirstScreenForState(out string screenName, out UIManager.ScreenTransition screenTransition, out float transitionDuration, bool fromSave = false)
  {
    screenName = !Game.instance.tutorialSystem.isTutorialActive || Game.instance.tutorialSystem.IsTutorialSectionComplete(TutorialSystem_v1.TutorialSection.HasRunFirstRace) ? "GridScreen" : "SessionHUD";
    screenTransition = UIManager.ScreenTransition.Fade;
    transitionDuration = 1.5f;
  }

  public override GameState.Type GetNextState()
  {
    return GameState.Type.Race;
  }

  public override void OnSessionStart()
  {
    base.OnSessionStart();
    Game.instance.stateInfo.GoToNextState();
  }
}
