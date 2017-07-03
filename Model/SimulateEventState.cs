// Decompiled with JetBrains decompiler
// Type: SimulateEventState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SimulateEventState : GameState
{
  private EventSummaryScreen mSummaryScreen;

  public override GameState.Type type
  {
    get
    {
      return GameState.Type.SimulateEventState;
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
    Game.instance.time.Pause(GameTimer.PauseType.Game);
    Championship[] championshipsRacingToday = Game.instance.championshipManager.GetChampionshipsRacingToday(true, Championship.Series.Count);
    for (int index = 0; index < championshipsRacingToday.Length; ++index)
    {
      SessionSimulation.SimulateEvent(championshipsRacingToday[index]);
      Debug.Log((object) ("Event Simulated -> " + championshipsRacingToday[index].GetAcronym(false) + " - " + Localisation.LocaliseID(championshipsRacingToday[index].GetPreviousEventDetails().circuit.locationNameID, (GameObject) null)), (Object) null);
    }
  }

  public override void OnExit(GameState inNextState)
  {
    Game.instance.stateInfo.SetIsReadyToSimulateRace(false);
    UIManager.instance.ClearNavigationStacks();
    if (Game.instance.player.IsUnemployed())
      Game.instance.queuedAutosave = true;
    base.OnExit(inNextState);
  }

  public override void GetFirstScreenForState(out string screenName, out UIManager.ScreenTransition screenTransition, out float transitionDuration, bool fromSave = false)
  {
    screenName = "SimulateEventScreen";
    screenTransition = UIManager.ScreenTransition.Fade;
    transitionDuration = 1.5f;
  }

  public override void Update()
  {
    base.Update();
    this.CheckForEscapeButton();
    UIManager.instance.navigationBars.SetContinueInteractable(true);
  }

  public override void OnContinueButton()
  {
    base.OnContinueButton();
    this.mSummaryScreen = UIManager.instance.currentScreen as EventSummaryScreen;
    if (!((Object) this.mSummaryScreen != (Object) null) || !this.mSummaryScreen.isComplete)
      return;
    App.instance.gameStateManager.SetState(GameState.Type.FrontendState, GameStateManager.StateChangeType.CheckForFadedScreenChange, false);
  }
}
