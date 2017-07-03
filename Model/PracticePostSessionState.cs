// Decompiled with JetBrains decompiler
// Type: PracticePostSessionState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class PracticePostSessionState : PostSessionState
{
  public override GameState.Type type
  {
    get
    {
      return GameState.Type.PracticePostSession;
    }
  }

  public override void OnEnter(bool fromSave = false)
  {
    base.OnEnter(fromSave);
    Game.instance.sessionManager.FixConditionInBetwenSessions();
  }

  public override void OnExit(GameState inNextState)
  {
    base.OnExit(inNextState);
    Game.instance.player.team.ClearSelectedDriversForSession();
  }

  public override void GetFirstScreenForState(out string screenName, out UIManager.ScreenTransition screenTransition, out float transitionDuration, bool fromSave = false)
  {
    MovieScreen screen = UIManager.instance.GetScreen<MovieScreen>();
    screen.PlayMovie(SoundID.Video_ChampVideo, Game.instance.player.team.championship.GetIdentMovieString());
    screen.SetNextScreen("PracticeResultsScreen", 1.5f, (Entity) null);
    screenName = "MovieScreen";
    screenTransition = UIManager.ScreenTransition.Fade;
    transitionDuration = 1.5f;
  }

  public override GameState.Type GetNextState()
  {
    return !Game.instance.player.team.championship.rules.qualifyingBasedActive ? GameState.Type.RacePreSession : GameState.Type.QualifyingPreSession;
  }
}
