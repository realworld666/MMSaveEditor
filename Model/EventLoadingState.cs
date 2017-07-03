// Decompiled with JetBrains decompiler
// Type: EventLoadingState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class EventLoadingState : GameState
{
  private bool mIsLoadingScenes;
  private EventLoadingState.State mState;
  private bool mReadyToMoveOn;

  public override GameState.Type type
  {
    get
    {
      return GameState.Type.EventLoading;
    }
  }

  public override void OnEnter(bool fromSave = false)
  {
    base.OnEnter(fromSave);
    Time.timeScale = 1f;
    Game.instance.time.Pause(GameTimer.PauseType.Game);
    Game.instance.sessionManager.SetChampionship(Game.instance.player.team.championship);
    Game.instance.stateInfo.SetIsReadyToGoToRace(false);
    this.mIsLoadingScenes = false;
    this.mReadyToMoveOn = false;
    this.SetState(EventLoadingState.State.PlayingEventMovie);
  }

  private void SetState(EventLoadingState.State inState)
  {
    this.mState = inState;
    switch (inState)
    {
      case EventLoadingState.State.Loading:
        App.instance.StartCoroutine(this.DoSceneLoading());
        Game.instance.vehicleManager.UnHideVehicles();
        break;
    }
  }

  [DebuggerHidden]
  private IEnumerator DoSceneLoading()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new EventLoadingState.\u003CDoSceneLoading\u003Ec__Iterator2() { \u003C\u003Ef__this = this };
  }

  public override void GetFirstScreenForState(out string screenName, out UIManager.ScreenTransition screenTransition, out float transitionDuration, bool fromSave = false)
  {
    Championship championship = Game.instance.player.team.championship;
    MovieLocationScreen screen = UIManager.instance.GetScreen<MovieLocationScreen>();
    screen.PlayMovie(SoundID.Video_TrackIntro, "TrackMovies/" + championship.GetCurrentEventDetails().circuit.spriteName);
    screen.SetNextScreen("RaceEventLoadingScreen", 1f, (Entity) null);
    screenName = "MovieLocationScreen";
    screenTransition = UIManager.ScreenTransition.Fade;
    transitionDuration = 1.5f;
  }

  public override void Update()
  {
    base.Update();
    if (App.instance.gameStateManager.isChangingState)
      return;
    switch (this.mState)
    {
      case EventLoadingState.State.PlayingEventMovie:
        if (UIManager.instance.isScreenFadeActive || UIManager.instance.IsScreenOpen("MovieLocationScreen"))
          break;
        this.SetState(EventLoadingState.State.Loading);
        break;
      case EventLoadingState.State.Loading:
        if (!this.mReadyToMoveOn || !UIManager.instance.IsScreenSetLoaded(UIManager.ScreenSet.RaceEvent) || (!UIManager.instance.IsScreenSetLoaded(UIManager.ScreenSet.Shared) || !SceneManager.instance.HasSceneSetLoaded(SceneSet.SceneSetType.RaceEvent)) || !SceneManager.instance.HasSceneSetLoaded(SceneSet.SceneSetType.Shared))
          break;
        if (!this.mIsLoadingScenes)
        {
          App.instance.StartCoroutine(this.DoCircuitLoad());
          this.mIsLoadingScenes = true;
        }
        if (!Game.instance.sessionManager.isCircuitLoaded)
          break;
        Game.instance.stateInfo.GoToNextState();
        break;
    }
  }

  [DebuggerHidden]
  private IEnumerator DoCircuitLoad()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    EventLoadingState.\u003CDoCircuitLoad\u003Ec__Iterator3 circuitLoadCIterator3 = new EventLoadingState.\u003CDoCircuitLoad\u003Ec__Iterator3();
    return (IEnumerator) circuitLoadCIterator3;
  }

  public override GameState.Type GetNextState()
  {
    return GameState.Type.None;
  }

  private enum State
  {
    PlayingEventMovie,
    Loading,
  }
}
