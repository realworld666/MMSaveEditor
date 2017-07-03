// Decompiled with JetBrains decompiler
// Type: GameStateManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameStateManager
{
  public GameState.Type initialState = GameState.Type.StartUp;
  private List<GameState> mGameStates = new List<GameState>();
  private GameState mCurrentState;
  private bool mIsChangingState;
  private bool mIsPaused;

  public GameState currentState
  {
    get
    {
      return this.mCurrentState;
    }
  }

  public bool isChangingState
  {
    get
    {
      return this.mIsChangingState;
    }
  }

  public static event Action OnStateChange;

  public void Start()
  {
    this.mGameStates.Add((GameState) new NullState());
    this.mGameStates.Add((GameState) new StartUpState());
    this.mGameStates.Add((GameState) new TitleLoadingState());
    this.mGameStates.Add((GameState) new TitleState());
    this.mGameStates.Add((GameState) new NewGameSetupState());
    this.mGameStates.Add((GameState) new ChallengeSetupState());
    this.mGameStates.Add((GameState) new FrontendLoadingState());
    this.mGameStates.Add((GameState) new FrontendState());
    this.mGameStates.Add((GameState) new PreSeasonState());
    this.mGameStates.Add((GameState) new SimulateEventState());
    this.mGameStates.Add((GameState) new EventLoadingState());
    this.mGameStates.Add((GameState) new TravelArrangementsState());
    this.mGameStates.Add((GameState) new PreSessionHUBState());
    this.mGameStates.Add((GameState) new PostSessionDataCenterState());
    this.mGameStates.Add((GameState) new SessionWinnerState());
    this.mGameStates.Add((GameState) new PracticePreSessionState());
    this.mGameStates.Add((GameState) new PracticePostSessionState());
    this.mGameStates.Add((GameState) new PracticeSessionState());
    this.mGameStates.Add((GameState) new QualifyingPreSessionState());
    this.mGameStates.Add((GameState) new QualifyingPostSessionState());
    this.mGameStates.Add((GameState) new QualifyingSessionState());
    this.mGameStates.Add((GameState) new RacePreSessionState());
    this.mGameStates.Add((GameState) new RacePostSessionState());
    this.mGameStates.Add((GameState) new RaceGridState());
    this.mGameStates.Add((GameState) new RaceSessionState());
    this.mGameStates.Add((GameState) new SkipSessionState());
    this.mGameStates.Add((GameState) new QuickRaceSetupState());
    this.mGameStates.Add((GameState) new PreSeasonTestingState());
    this.mGameStates.Add((GameState) new PostEventFrontendState());
    this.mCurrentState = this.GetState(this.initialState);
    this.mCurrentState.OnEnter(false);
  }

  public void OnLoad()
  {
    for (int index = 0; index < this.mGameStates.Count; ++index)
      this.mGameStates[index].OnLoad();
  }

  public void Update()
  {
    if (this.mCurrentState == null || this.mIsPaused)
      return;
    GameTimer.totalSimulationDeltaTimeCurrentFrame = 0.0f;
    if (this.mCurrentState.IsSimulation())
    {
      Game.instance.time.UpdateInput();
      this.mCurrentState.Update();
      float num1 = Mathf.Min(GameTimer.deltaTime, 0.03333334f);
      float simulationTimeScale = Game.instance.time.GetSimulationTimeScale();
      int num2 = Mathf.CeilToInt(simulationTimeScale);
      GameTimer.baseSimulationDeltaTime = num1 * simulationTimeScale / (float) num2;
      for (int index = 0; index < num2; ++index)
      {
        this.mCurrentState.SimulationUpdate();
        GameTimer.totalSimulationDeltaTimeCurrentFrame += GameTimer.simulationDeltaTime;
      }
    }
    else
      this.mCurrentState.Update();
  }

  public void SetState(GameState.Type inType, GameStateManager.StateChangeType inStateChangeType = GameStateManager.StateChangeType.CheckForFadedScreenChange, bool isLoadingFromSave = false)
  {
    GameState state = this.GetState(inType);
    if (inStateChangeType == GameStateManager.StateChangeType.ChangeInstantly)
    {
      string screenName;
      UIManager.ScreenTransition screenTransition;
      float transitionDuration;
      state.GetFirstScreenForState(out screenName, out screenTransition, out transitionDuration, isLoadingFromSave);
      if (!string.IsNullOrEmpty(screenName))
        UIManager.instance.ChangeScreen(screenName, UIManager.ScreenTransition.FadeFrom, 0.0f, (Action) null, UIManager.NavigationType.Normal);
      this.SetStateNow(state, isLoadingFromSave);
    }
    else
      this.SetStateAfterScreenFade(state, isLoadingFromSave);
  }

  private void SetStateAfterScreenFade(GameState inState, bool isLoadingFromSave = false)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    GameStateManager.\u003CSetStateAfterScreenFade\u003Ec__AnonStorey5D fadeCAnonStorey5D = new GameStateManager.\u003CSetStateAfterScreenFade\u003Ec__AnonStorey5D();
    // ISSUE: reference to a compiler-generated field
    fadeCAnonStorey5D.inState = inState;
    // ISSUE: reference to a compiler-generated field
    fadeCAnonStorey5D.isLoadingFromSave = isLoadingFromSave;
    // ISSUE: reference to a compiler-generated field
    fadeCAnonStorey5D.\u003C\u003Ef__this = this;
    this.mIsChangingState = true;
    string screenName;
    UIManager.ScreenTransition screenTransition;
    float transitionDuration;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    fadeCAnonStorey5D.inState.GetFirstScreenForState(out screenName, out screenTransition, out transitionDuration, fadeCAnonStorey5D.isLoadingFromSave);
    if (!string.IsNullOrEmpty(screenName))
    {
      // ISSUE: reference to a compiler-generated method
      UIManager.instance.ChangeScreen(screenName, screenTransition, transitionDuration, new Action(fadeCAnonStorey5D.\u003C\u003Em__7F), UIManager.NavigationType.Normal);
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      this.SetStateNow(fadeCAnonStorey5D.inState, fadeCAnonStorey5D.isLoadingFromSave);
    }
  }

  private void SetStateNow(GameState inState, bool isLoadingFromSave = false)
  {
    this.mCurrentState.OnExit(inState);
    this.mCurrentState = inState;
    this.mIsChangingState = false;
    if (isLoadingFromSave)
      this.mCurrentState.OnReEnterFromSave();
    else
      this.mCurrentState.OnEnter(false);
    if (GameStateManager.OnStateChange != null)
      GameStateManager.OnStateChange();
    this.UnPause();
  }

  public bool IsInState(GameState.Type inType)
  {
    if (this.mCurrentState != null)
      return this.mCurrentState.type == inType;
    return false;
  }

  public bool IsInState(GameState inState)
  {
    if (this.mCurrentState != null)
      return this.mCurrentState.GetType() == inState.GetType();
    return false;
  }

  public GameState GetState(GameState.Type inType)
  {
    for (int index = 0; index < this.mGameStates.Count; ++index)
    {
      if (this.mGameStates[index].type == inType)
        return this.mGameStates[index];
    }
    Debug.LogError((object) ("GameStateManager.GetState failed to find state with type: " + (object) inType), (UnityEngine.Object) null);
    return (GameState) null;
  }

  public void LoadToTitleScreen(GameStateManager.StateChangeType inStateChangeType = GameStateManager.StateChangeType.CheckForFadedScreenChange)
  {
    if (!this.mIsChangingState)
      App.instance.gameStateManager.SetState(GameState.Type.TitleLoading, inStateChangeType, false);
    scSoundManager.Instance.LoadToTitleScreen();
  }

  public void LoadToFrontend(GameStateManager.StateChangeType inStateChangeType = GameStateManager.StateChangeType.CheckForFadedScreenChange)
  {
    if (!this.mIsChangingState)
      App.instance.gameStateManager.SetState(GameState.Type.FrontendLoading, inStateChangeType, false);
    scSoundManager.Instance.LoadToFrontend();
  }

  public void LoadToRaceEvent(GameStateManager.StateChangeType inStateChangeType = GameStateManager.StateChangeType.CheckForFadedScreenChange)
  {
    if (!this.mIsChangingState)
      App.instance.gameStateManager.SetState(GameState.Type.EventLoading, inStateChangeType, false);
    scSoundManager.Instance.LoadToRaceEvent();
  }

  public void OnContinueButton()
  {
    if (this.mCurrentState == null)
      return;
    this.mCurrentState.OnContinueButton();
  }

  public void OnCancelButton()
  {
    if (this.mCurrentState == null)
      return;
    this.mCurrentState.OnContinueButton();
  }

  public void Pause()
  {
    this.mIsPaused = true;
  }

  public void UnPause()
  {
    this.mIsPaused = false;
  }

  [Conditional("GAME_STATE_LOGGING_ENABLED")]
  private void StateChangeLog(string message)
  {
    Debug.Log((object) ("Frame " + (object) Time.frameCount + ": " + message), (UnityEngine.Object) null);
  }

  public enum StateChangeType
  {
    CheckForFadedScreenChange,
    ChangeInstantly,
  }
}
