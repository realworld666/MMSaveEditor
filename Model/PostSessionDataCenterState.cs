// Decompiled with JetBrains decompiler
// Type: PostSessionDataCenterState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class PostSessionDataCenterState : GameState
{
  public override GameState.Type type
  {
    get
    {
      return GameState.Type.PostSessionDataCenter;
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
      return GameState.Group.Event;
    }
  }

  public override void OnEnter(bool fromSave = false)
  {
    base.OnEnter(fromSave);
    this.SetAsSaveEntryPoint();
    Game.instance.sessionManager.SetCircuitActive(true);
    App.instance.cameraManager.ActivateMode(CameraManager.CameraMode.PostSession);
    App.instance.cameraManager.gameCamera.SetBlurActive(true);
    UIManager.instance.ClearNavigationStacks();
  }

  public override void GetFirstScreenForState(out string screenName, out UIManager.ScreenTransition screenTransition, out float transitionDuration, bool fromSave = false)
  {
    MovieScreen screen = UIManager.instance.GetScreen<MovieScreen>();
    screen.PlayMovie(SoundID.Video_ChampVideo, Game.instance.player.team.championship.GetIdentMovieString());
    screen.SetNextScreen("TyreHistoryScreen", 1.5f, (Entity) null);
    screenName = "MovieScreen";
    screenTransition = UIManager.ScreenTransition.Fade;
    transitionDuration = 1.5f;
  }

  public override void Update()
  {
    base.Update();
    this.CheckForEscapeButton();
  }

  public override GameState.Type GetNextState()
  {
    return GameState.Type.RacePostSession;
  }

  public override void OnContinueButton()
  {
    Game.instance.stateInfo.GoToNextState();
  }
}
