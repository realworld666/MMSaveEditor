// Decompiled with JetBrains decompiler
// Type: SkipSessionState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class SkipSessionState : SessionState
{
  private bool mSessionStarted;
  private SessionState mSessionState;

  public override GameState.Type type
  {
    get
    {
      return GameState.Type.SkipSession;
    }
  }

  public override UIManager.ScreenSet screenSet
  {
    get
    {
      return UIManager.ScreenSet.RaceEvent;
    }
  }

  public override GameState.Group group
  {
    get
    {
      return GameState.Group.Simulation;
    }
  }

  public bool sessionStarted
  {
    get
    {
      return this.mSessionStarted;
    }
    set
    {
      this.mSessionStarted = value;
    }
  }

  public override void OnEnter(bool fromSave = false)
  {
    Game.instance.sessionManager.SetSkippingActive(true);
    switch (Game.instance.sessionManager.eventDetails.currentSession.sessionType)
    {
      case SessionDetails.SessionType.Practice:
        this.mSessionState = App.instance.gameStateManager.GetState(GameState.Type.Practice) as SessionState;
        break;
      case SessionDetails.SessionType.Qualifying:
        this.mSessionState = App.instance.gameStateManager.GetState(GameState.Type.Qualifying) as SessionState;
        if (Game.instance.vehicleManager.vehicleCount == 0)
        {
          App.instance.gameStateManager.GetState(GameState.Type.QualifyingPreSession).OnEnter(false);
          break;
        }
        break;
      case SessionDetails.SessionType.Race:
        this.mSessionState = App.instance.gameStateManager.GetState(GameState.Type.Race) as SessionState;
        break;
    }
    if (!Game.instance.sessionManager.isSessionActive)
      this.mSessionState.OnEnter(false);
    Game.instance.time.SetSpeed(GameTimer.Speed.Fast);
  }

  public override void OnSessionStarting()
  {
    this.mSessionState.OnSessionStarting();
  }

  public override void OnExit(GameState inNextState)
  {
    Game.instance.sessionManager.SetSkippingActive(false);
    this.mSessionState.OnExit(inNextState);
  }

  public override void Update()
  {
    base.Update();
    this.mSessionState.Update();
  }

  public override void SimulationUpdate()
  {
    this.mSessionState.SimulationUpdate();
  }

  public override void UpdateStandings()
  {
    this.mSessionState.UpdateStandings();
  }

  public override void GetFirstScreenForState(out string screenName, out UIManager.ScreenTransition screenTransition, out float transitionDuration, bool fromSave = false)
  {
    screenName = "SkipSessionScreen";
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
        type = GameState.Type.PracticePostSession;
        break;
      case SessionDetails.SessionType.Qualifying:
        type = GameState.Type.QualifyingPostSession;
        break;
      case SessionDetails.SessionType.Race:
        type = GameState.Type.RacePostSession;
        break;
    }
    return type;
  }
}
