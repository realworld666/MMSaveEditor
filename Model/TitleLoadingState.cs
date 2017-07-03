// Decompiled with JetBrains decompiler
// Type: TitleLoadingState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class TitleLoadingState : GameState
{
  private TitleLoadingState.State mState;
  private bool mReadyToMoveOn;

  public override GameState.Type type
  {
    get
    {
      return GameState.Type.TitleLoading;
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
    Time.timeScale = 1f;
    this.mReadyToMoveOn = false;
    if (Game.IsActive() && Game.instance.sessionManager.isCircuitLoaded)
      Game.instance.sessionManager.UnloadCircuitScene();
    if (Game.IsActive() && Game.instance.vehicleManager.vehicleCount > 0)
      Game.instance.vehicleManager.HideVehicles();
    if (Game.IsActive())
      Game.instance.sessionManager.ClearPreviousSessionData();
    App.instance.cameraManager.Deactivate();
    App.instance.cameraManager.gameCamera.SetBlurActive(false);
  }

  private void SetState(TitleLoadingState.State inState)
  {
    TitleLoadingState.State mState = this.mState;
    this.mState = inState;
    switch (inState)
    {
      case TitleLoadingState.State.QualitySelection:
        PlayerPrefs.SetInt("HasSelectedQualityMode", 1);
        PlayerPrefs.Save();
        break;
      case TitleLoadingState.State.Loading:
        if (mState == TitleLoadingState.State.QualitySelection)
          UIManager.instance.ChangeScreen("TitleLoadingScreen", UIManager.ScreenTransition.FadeFrom, 0.75f, (Action) null, UIManager.NavigationType.Normal);
        App.instance.StartCoroutine(this.DoSceneLoading());
        break;
    }
  }

  [DebuggerHidden]
  private IEnumerator DoSceneLoading()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new TitleLoadingState.\u003CDoSceneLoading\u003Ec__Iterator5() { \u003C\u003Ef__this = this };
  }

  public override void GetFirstScreenForState(out string screenName, out UIManager.ScreenTransition screenTransition, out float transitionDuration, bool fromSave = false)
  {
    if (PlayerPrefs.GetInt("HasSelectedQualityMode", 0) == 0)
    {
      screenName = "QualitySelectScreen";
      this.SetState(TitleLoadingState.State.QualitySelection);
    }
    else
    {
      screenName = "TitleLoadingScreen";
      this.SetState(TitleLoadingState.State.Loading);
    }
    if (!App.instance.gameStateManager.currentState.IsFrontend())
    {
      screenTransition = UIManager.ScreenTransition.FadeFrom;
      transitionDuration = 1f;
    }
    else
    {
      screenTransition = UIManager.ScreenTransition.Fade;
      transitionDuration = 1.5f;
    }
  }

  public override void OnContinueButton()
  {
    if (this.mState != TitleLoadingState.State.QualitySelection)
      return;
    this.SetState(TitleLoadingState.State.Loading);
  }

  public override void Update()
  {
    base.Update();
    if (App.instance.gameStateManager.isChangingState)
      return;
    switch (this.mState)
    {
      case TitleLoadingState.State.Loading:
        if (!this.mReadyToMoveOn || !UIManager.instance.IsScreenSetLoaded(UIManager.ScreenSet.Title) || !SceneManager.instance.HasSceneSetLoaded(SceneSet.SceneSetType.Title))
          break;
        App.instance.gameStateManager.SetState(GameState.Type.TitleState, GameStateManager.StateChangeType.CheckForFadedScreenChange, false);
        break;
    }
  }

  private enum State
  {
    None,
    QualitySelection,
    Loading,
  }
}
