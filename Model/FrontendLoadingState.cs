// Decompiled with JetBrains decompiler
// Type: FrontendLoadingState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class FrontendLoadingState : GameState
{
  private bool mReadyToMoveOn;

  public override GameState.Type type
  {
    get
    {
      return GameState.Type.FrontendLoading;
    }
  }

  public override void OnEnter(bool fromSave = false)
  {
    base.OnEnter(fromSave);
    Time.timeScale = 1f;
    Game.instance.time.Pause(GameTimer.PauseType.Game);
    if (Game.instance.sessionManager.isCircuitLoaded)
      Game.instance.sessionManager.UnloadCircuitScene();
    Game.instance.sessionManager.ClearPreviousSessionData();
    Game.instance.vehicleManager.HideVehicles();
    this.mReadyToMoveOn = false;
    App.instance.StartCoroutine(this.DoSceneLoading());
    App.instance.cameraManager.Deactivate();
    App.instance.cameraManager.gameCamera.SetBlurActive(false);
  }

  [DebuggerHidden]
  private IEnumerator DoSceneLoading()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new FrontendLoadingState.\u003CDoSceneLoading\u003Ec__Iterator4() { \u003C\u003Ef__this = this };
  }

  public override void GetFirstScreenForState(out string screenName, out UIManager.ScreenTransition screenTransition, out float transitionDuration, bool fromSave = false)
  {
    screenName = "FrontendLoadingScreen";
    screenTransition = UIManager.ScreenTransition.Fade;
    transitionDuration = 1.5f;
  }

  public override void Update()
  {
    base.Update();
    if (App.instance.gameStateManager.isChangingState || !this.mReadyToMoveOn || (!UIManager.instance.IsScreenSetLoaded(UIManager.ScreenSet.Frontend) || !UIManager.instance.IsScreenSetLoaded(UIManager.ScreenSet.Shared)) || (!SceneManager.instance.HasSceneSetLoaded(SceneSet.SceneSetType.FrontEnd) || !SceneManager.instance.HasSceneSetLoaded(SceneSet.SceneSetType.Shared)))
      return;
    Game.instance.stateInfo.GoToNextState();
  }

  public override GameState.Type GetNextState()
  {
    return !Game.instance.tutorialSystem.IsTutorialSectionComplete(TutorialSystem_v1.TutorialSection.SkipTeamReportScreen) ? GameState.Type.FrontendState : GameState.Type.PostEventFrontendState;
  }
}
