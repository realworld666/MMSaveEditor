// Decompiled with JetBrains decompiler
// Type: PracticePreSessionState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class PracticePreSessionState : PreSessionState
{
  public override GameState.Type type
  {
    get
    {
      return GameState.Type.PracticePreSession;
    }
  }

  public override void OnEnter(bool fromSave = false)
  {
    UIManager.instance.ClearNavigationStacks();
    Game.instance.player.team.ClearSelectedDriversForSession();
    Game.instance.persistentEventData.OnEventStart();
    Game.instance.player.team.StoreTeamDataBeforeEvent();
    base.OnEnter(fromSave);
    Game.instance.mediaManager.ClearStories();
    Game.instance.sessionManager.circuit.ClearGridCrowds();
    Game.instance.player.team.ClearSelectedDriversForSession();
  }

  public override void GetFirstScreenForState(out string screenName, out UIManager.ScreenTransition screenTransition, out float transitionDuration, bool fromSave = false)
  {
    screenName = "TrackDetailsScreen";
    screenName = "DayTransitionScreen";
    transitionDuration = 1.5f;
    screenTransition = UIManager.ScreenTransition.Fade;
  }
}
