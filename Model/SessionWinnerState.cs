// Decompiled with JetBrains decompiler
// Type: SessionWinnerState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class SessionWinnerState : GameState
{
  private float mDisplayDuration = 10f;
  private float mTimer;
  private bool mIsChangingState;

  public override GameState.Type type
  {
    get
    {
      return GameState.Type.SessionWinner;
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

  public override void OnEnter(bool fromSave = false)
  {
    base.OnEnter(fromSave);
    this.SetAsSaveEntryPoint();
    if (fromSave)
      Game.instance.sessionManager.PrepareForSessionAfterLoad();
    Game.instance.time.SetSpeed(GameTimer.Speed.Slow);
    Vehicle leader = (Vehicle) Game.instance.sessionManager.GetLeader();
    App.instance.cameraManager.SetTarget(leader, CameraManager.Transition.Smooth);
    this.mTimer = 0.0f;
    this.mIsChangingState = false;
    if (Game.instance.sessionManager.sessionType != SessionDetails.SessionType.Qualifying)
      return;
    foreach (RacingVehicle playerVehicle in Game.instance.vehicleManager.GetPlayerVehicles())
      playerVehicle.teamRadio.GetRadioMessage<RadioMessageQualifying>().OnSessionEnd();
  }

  public override void Update()
  {
    base.Update();
    this.CheckForEscapeButton();
    Game.instance.vehicleManager.SimulationUpdate();
    if (Game.instance.time.isPaused)
      return;
    this.mTimer += GameTimer.deltaTime;
    if ((double) this.mTimer < (double) this.mDisplayDuration || this.mIsChangingState)
      return;
    Game.instance.stateInfo.GoToNextState();
    UIManager.instance.dialogBoxManager.HideAll();
    this.mIsChangingState = true;
  }

  public override void OnExit(GameState inNextState)
  {
    base.OnExit(inNextState);
  }

  public override GameState.Type GetNextState()
  {
    RaceEventDetails eventDetails = Game.instance.sessionManager.eventDetails;
    GameState.Type type = GameState.Type.None;
    switch (eventDetails.currentSession.sessionType)
    {
      case SessionDetails.SessionType.Qualifying:
        type = GameState.Type.QualifyingPostSession;
        break;
      case SessionDetails.SessionType.Race:
        type = GameState.Type.PostSessionDataCenter;
        break;
    }
    return type;
  }
}
