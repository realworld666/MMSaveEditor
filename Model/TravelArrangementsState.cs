// Decompiled with JetBrains decompiler
// Type: TravelArrangementsState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class TravelArrangementsState : GameState
{
  public override GameState.Type type
  {
    get
    {
      return GameState.Type.TravelArrangements;
    }
  }

  public override GameState.Group group
  {
    get
    {
      return GameState.Group.Frontend;
    }
  }

  public override UIManager.ScreenSet screenSet
  {
    get
    {
      return UIManager.ScreenSet.Frontend;
    }
  }

  public override void OnEnter(bool fromSave = false)
  {
    base.OnEnter(fromSave);
    this.SetAsSaveEntryPoint();
    Game.instance.time.Pause(GameTimer.PauseType.Game);
    UIManager.instance.ClearNavigationStacks();
    Game.instance.player.team.ClearSelectedDriversForSession();
    Game.instance.sessionManager.SetChampionship(Game.instance.player.team.championship);
  }

  public override GameState.Type GetNextState()
  {
    return GameState.Type.PracticePreSession;
  }

  public override void Update()
  {
    base.Update();
    this.CheckForEscapeButton();
  }

  public override void GetFirstScreenForState(out string screenName, out UIManager.ScreenTransition screenTransition, out float transitionDuration, bool fromSave = false)
  {
    screenName = "TravelArrangementsScreen";
    screenTransition = UIManager.ScreenTransition.Fade;
    transitionDuration = 1.5f;
  }
}
