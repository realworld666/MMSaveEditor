// Decompiled with JetBrains decompiler
// Type: BaseMovieScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public abstract class BaseMovieScreen : UIScreen
{
  private string mNextScreen = string.Empty;
  private bool mCanSkipMovie = true;
  public GameObject movie;
  public UITeamLogo teamLogo;
  private string[] mMovieQueue;
  private int mNextMovieID;
  private float mPlaybackTimer;
  private SoundID mSoundID;
  private scSoundContainer mSoundContainer;
  private float mNextScreenFadeTime;
  private Entity mNextScreenFocusEntity;
  private GameState.Type mNextGameState;
  private bool mWasCircuitActive;

  public bool hasFinished
  {
    get
    {
      if (this.mMovieQueue != null && this.mNextMovieID >= this.mMovieQueue.Length && BinkWrapper.HasInstance)
        return !BinkWrapper.instance.IsPlaying();
      return false;
    }
  }

  public override void OnStart()
  {
    base.OnStart();
    this.dontAddToBackStack = true;
    BinkWrapper.instance.EmptyInit();
    if (!((UnityEngine.Object) this.teamLogo != (UnityEngine.Object) null))
      return;
    GameUtility.SetActive(this.teamLogo.gameObject, false);
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.canUseEscapeButton = false;
    this.showNavigationBars = false;
    this.mWasCircuitActive = Game.IsActive() && Game.instance.sessionManager.isCircuitActive;
    if (Game.IsActive())
    {
      Game.instance.sessionManager.SetCircuitActive(false);
      App.instance.cameraManager.GetCamera().enabled = false;
    }
    if (!this.hasFinished)
      this.PlayNextMovieInQueue();
    else
      this.Continue();
  }

  public override void OnExit()
  {
    base.OnExit();
    this.mMovieQueue = (string[]) null;
    this.mNextMovieID = 0;
    this.mPlaybackTimer = 0.0f;
    this.mNextScreen = string.Empty;
    this.mNextScreenFadeTime = 0.0f;
    if (Game.IsActive())
    {
      Game.instance.sessionManager.SetCircuitActive(this.mWasCircuitActive);
      App.instance.cameraManager.GetCamera().enabled = this.mWasCircuitActive;
    }
    this.mCanSkipMovie = true;
  }

  protected virtual void Update()
  {
    if (this.mMovieQueue == null || this.mMovieQueue.Length <= 0 || !BinkWrapper.HasInstance)
      return;
    if (this.mCanSkipMovie && (double) this.mPlaybackTimer > 0.800000011920929 && (InputManager.instance.GetKeyDown(KeyBinding.Name.Escape) || InputManager.instance.GetKeyDown(KeyBinding.Name.MouseLeft) || GamePad.GetButton(CButton.A, PlayerIndex.Any)))
      this.OnVideoFinished();
    else
      this.mPlaybackTimer += GameTimer.deltaTime;
  }

  public void SetNextScreen(string inNextScreen, float inNextScreenFadeTime, Entity inFocusEntity = null)
  {
    this.mNextScreen = inNextScreen;
    this.mNextScreenFadeTime = inNextScreenFadeTime;
    this.mNextScreenFocusEntity = inFocusEntity;
  }

  public void SetNextState(GameState.Type inState)
  {
    this.mNextGameState = inState;
  }

  public void PlayMovie(params string[] inMovies)
  {
    this.mMovieQueue = inMovies;
  }

  public void PlayMovie(SoundID soundID, params string[] inMovies)
  {
    this.PlayMovie(inMovies);
    this.mSoundID = soundID;
  }

  public void PlayTeamMovie(SoundID soundID, int inTeamId)
  {
    if ((UnityEngine.Object) this.teamLogo != (UnityEngine.Object) null)
      GameUtility.SetActive(this.teamLogo.gameObject, Game.IsActive() && Game.instance.player.hasCreatedTeam && Game.instance.player.createdTeamID == inTeamId);
    if ((UnityEngine.Object) this.teamLogo != (UnityEngine.Object) null && this.teamLogo.gameObject.activeSelf)
    {
      this.PlayMovie(soundID, "TeamIntroMovies/TeamIntroCreateTeam");
      this.teamLogo.SetTeam(inTeamId);
    }
    else
      this.PlayMovie(soundID, "TeamIntroMovies/TeamIntro" + (inTeamId + 1).ToString());
  }

  private void PlayNextMovieInQueue()
  {
    if (this.mMovieQueue == null)
      return;
    App.instance.gameStateManager.Pause();
    if (!BinkWrapper.HasInstance)
      return;
    BinkWrapper.instance.PrepareMovie(this.mMovieQueue[this.mNextMovieID]);
    this.PlayMovie(this.mMovieQueue[this.mNextMovieID]);
  }

  private void PlayMovie(string inNextMoviePath)
  {
    if (BinkWrapper.HasInstance)
    {
      BinkWrapper.instance.Play(1, new Action(this.OnVideoFinished));
      this.mSoundContainer = scSoundManager.Instance.PlaySound(this.mSoundID, 0.0f);
      this.mPlaybackTimer = 0.0f;
      ++this.mNextMovieID;
    }
    else
    {
      Debug.LogWarningFormat("Couldn't find movie \"{0}\"; skipping.", (object) inNextMoviePath);
      this.Continue();
    }
  }

  public void MarkMovieUnskippable()
  {
    this.mCanSkipMovie = false;
  }

  protected virtual void Continue()
  {
    if (this.mNextGameState != GameState.Type.None)
      this.GoToNextState();
    else
      this.GoToNextScreen();
  }

  private void GoToNextState()
  {
    this.MarkMovieUnskippable();
    App.instance.gameStateManager.UnPause();
    App.instance.gameStateManager.SetState(this.mNextGameState, GameStateManager.StateChangeType.CheckForFadedScreenChange, false);
    this.mNextGameState = GameState.Type.None;
  }

  private void GoToNextScreen()
  {
    this.MarkMovieUnskippable();
    App.instance.gameStateManager.UnPause();
    if (string.IsNullOrEmpty(this.mNextScreen))
      return;
    if (Mathf.Approximately(this.mNextScreenFadeTime, 0.0f))
      UIManager.instance.ChangeScreen(this.mNextScreen, this.mNextScreenFocusEntity, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
    else
      UIManager.instance.ChangeScreen(this.mNextScreen, this.mNextScreenFocusEntity, UIManager.ScreenTransition.FadeFrom, this.mNextScreenFadeTime, UIManager.NavigationType.Normal);
    if (App.instance.gameStateManager.currentState is PreSeasonState)
      App.instance.saveSystem.TryDispatchDelayedAutoSave();
    this.mNextScreen = string.Empty;
  }

  protected virtual void AudioStop()
  {
    if (!((UnityEngine.Object) this.mSoundContainer != (UnityEngine.Object) null))
      return;
    this.mSoundContainer.StopSound(false);
  }

  protected void VideoStop()
  {
    if (!BinkWrapper.HasInstance)
      return;
    BinkWrapper.instance.Stop();
  }

  public void OnVideoFinished()
  {
    if ((UnityEngine.Object) this.teamLogo != (UnityEngine.Object) null)
      GameUtility.SetActive(this.teamLogo.gameObject, false);
    this.VideoStop();
    this.AudioStop();
    if (this.mMovieQueue == null)
      return;
    if (this.mNextMovieID < this.mMovieQueue.Length)
    {
      this.PlayNextMovieInQueue();
    }
    else
    {
      this.mNextMovieID = this.mMovieQueue.Length;
      this.Continue();
    }
  }
}
