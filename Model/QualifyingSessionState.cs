// Decompiled with JetBrains decompiler
// Type: QualifyingSessionState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class QualifyingSessionState : SessionState
{
  public override GameState.Type type
  {
    get
    {
      return GameState.Type.Qualifying;
    }
  }

  public override void OnEnter(bool fromSave = false)
  {
    base.OnEnter(fromSave);
    SessionManager sessionManager = Game.instance.sessionManager;
    int vehicleCount = Game.instance.vehicleManager.vehicleCount;
    for (int inIndex = 0; inIndex < vehicleCount; ++inIndex)
      Game.instance.vehicleManager.GetVehicle(inIndex).pathState.ChangeState(PathStateManager.StateType.Garage);
    this.StartCommentarySystem();
    sessionManager.StartSession();
    Game.instance.vehicleManager.PrepareForSession();
    Game.instance.vehicleManager.OnSessionStart();
  }

  public override void OnExit(GameState inNextState)
  {
    base.OnExit(inNextState);
    this.StopCommentarySystem();
  }

  public override void GetFirstScreenForState(out string screenName, out UIManager.ScreenTransition screenTransition, out float transitionDuration, bool fromSave = false)
  {
    screenName = "SessionHUD";
    screenTransition = UIManager.ScreenTransition.Fade;
    transitionDuration = 1.5f;
  }

  public override void UpdateStandings()
  {
    this.OrderStandingsByFastestLap();
  }

  public override GameState.Type GetNextState()
  {
    return GameState.Type.SessionWinner;
  }

  public override void SetStartingStandings()
  {
    if (Game.instance.sessionManager.eventDetails.sessionID != 0)
      return;
    this.UpdateStandings();
  }
}
