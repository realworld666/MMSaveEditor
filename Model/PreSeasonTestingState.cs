// Decompiled with JetBrains decompiler
// Type: PreSeasonTestingState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;

public class PreSeasonTestingState : GameState
{
  public Action OnPreSeasonTestingEnd;

  public override GameState.Type type
  {
    get
    {
      return GameState.Type.PreSeasonTestingState;
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
  }

  public override void OnExit(GameState inNextState)
  {
    base.OnExit(inNextState);
    if (this.OnPreSeasonTestingEnd == null)
      return;
    this.OnPreSeasonTestingEnd();
  }

  public override void Update()
  {
    base.Update();
    this.CheckForEscapeButton();
    Game instance = Game.instance;
    instance.time.UpdateInput();
    instance.time.Update();
    instance.calendar.Update();
    instance.entityManager.Update();
  }

  public override void GetFirstScreenForState(out string screenName, out UIManager.ScreenTransition screenTransition, out float transitionDuration, bool fromSave = false)
  {
    screenName = "PreSeasonTestingScreen";
    screenTransition = UIManager.ScreenTransition.Fade;
    transitionDuration = 1.5f;
  }
}
