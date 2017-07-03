// Decompiled with JetBrains decompiler
// Type: StartUpState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class StartUpState : GameState
{
  private MovieScreen mMovieScreen;
  private AttractIntroScreen mAttractIntroScreen;
  private LegalScreen mLegalScreen;
  private bool mHasShownPrivacyPolicy;

  public override GameState.Type type
  {
    get
    {
      return GameState.Type.StartUp;
    }
  }

  public override void OnEnter(bool fromSave = false)
  {
    base.OnEnter(fromSave);
    App.instance.StartCoroutine(SceneManager.instance.LoadScenesSet(SceneSet.SceneSetType.Core));
    App.instance.StartCoroutine(UIManager.instance.LoadScreens(UIManager.ScreenSet.Core));
  }

  public override void Update()
  {
    base.Update();
    if (!UIManager.instance.IsScreenSetLoaded(UIManager.ScreenSet.Core) || !SceneManager.instance.HasSceneSetLoaded(SceneSet.SceneSetType.Core))
      return;
    if ((UnityEngine.Object) this.mMovieScreen == (UnityEngine.Object) null)
    {
      this.mMovieScreen = UIManager.instance.GetScreen<MovieScreen>();
      this.mAttractIntroScreen = UIManager.instance.GetScreen<AttractIntroScreen>();
      this.mLegalScreen = UIManager.instance.GetScreen<LegalScreen>();
    }
    if (!UIManager.instance.IsScreenOpen((UIScreen) this.mMovieScreen) && !UIManager.instance.IsScreenOpen((UIScreen) this.mAttractIntroScreen) && !UIManager.instance.IsScreenOpen((UIScreen) this.mLegalScreen))
    {
      if (!Application.isEditor)
      {
        this.mMovieScreen.MarkMovieUnskippable();
        this.mAttractIntroScreen.MarkMovieUnskippable();
      }
      scSoundManager.BlockSoundEvents = false;
      this.mMovieScreen.PlayMovie(SoundID.Video_AllLogos, "SplashSequence");
      this.mMovieScreen.SetNextScreen("AttractIntroScreen", 1f, (Entity) null);
      this.mAttractIntroScreen.SetNextScreen("LegalScreen", 1f, (Entity) null);
      UIManager.instance.ChangeScreen("MovieScreen", UIManager.ScreenTransition.FadeFrom, 1f, (Action) null, UIManager.NavigationType.Normal);
    }
    if (!((UnityEngine.Object) this.mLegalScreen != (UnityEngine.Object) null) || !this.mLegalScreen.isFinished || this.mHasShownPrivacyPolicy)
      return;
    if (App.instance.database == null)
    {
      App.instance.LoadGameDatabases();
      App.instance.modManager.LoadSubscribedMods();
    }
    this.mHasShownPrivacyPolicy = true;
    if (PlayerPrefs.GetInt("AcceptedPrivacyPolicy", 0) == 0)
      UIManager.instance.dialogBoxManager.Show("PrivacyPolicy");
    else
      App.instance.gameStateManager.LoadToTitleScreen(GameStateManager.StateChangeType.CheckForFadedScreenChange);
  }
}
