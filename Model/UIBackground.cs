// Decompiled with JetBrains decompiler
// Type: UIBackground
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class UIBackground : MonoBehaviour
{
  private string mLastBackgroundImageSpriteName = string.Empty;
  public BinkWrapper binkPlayer;
  public RawImage backgroundImage;
  public Camera backgroundCamera;
  private Canvas mCanvas;
  private BlurOptimized mBackgroundImageBlur;

  private void Awake()
  {
    this.mCanvas = this.GetComponent<Canvas>();
    GameUtility.SetActive(this.binkPlayer.gameObject, false);
    GameUtility.SetActive(this.backgroundImage.gameObject, false);
    this.backgroundCamera.enabled = false;
    this.mBackgroundImageBlur = this.backgroundCamera.GetComponent<BlurOptimized>();
  }

  public void SetBackground(UIBackground.Type inType)
  {
    this.backgroundCamera.enabled = false;
    switch (inType)
    {
      case UIBackground.Type.None:
      case UIBackground.Type.Scene:
        if (!App.instance.preferencesManager.videoPreferences.isRunning2DMode || UIManager.instance.currentScreen is SessionHUD)
        {
          this.ClearBackground();
          break;
        }
        if (Game.IsActive() && Game.instance.sessionManager.isCircuitLoaded)
        {
          if (UIManager.instance.currentScreen is MovieScreen)
          {
            this.ClearBackground();
            GameUtility.SetActive(this.backgroundImage.gameObject, false);
            break;
          }
          this.Activate2DBackground();
          break;
        }
        string inMovie = UIManager.instance.currentScreen.movieFallBack2DMode;
        if (Game.IsActive() && !Game.instance.player.IsUnemployed() && Game.instance.player.team.championship.series == Championship.Series.GTSeries)
          inMovie = UIManager.instance.currentScreen.movieFallBack2DModeGT;
        if (!string.IsNullOrEmpty(inMovie))
        {
          App.instance.cameraManager.GetCamera().enabled = false;
          UIManager.instance.SetupCanvasCamera(this.mCanvas);
          GameUtility.SetActive(this.backgroundImage.gameObject, false);
          GameUtility.SetActive(this.binkPlayer.gameObject, true);
          if (this.binkPlayer.IsStopped() || inMovie != this.binkPlayer.currentMovieName)
            this.PrepareMovie(inMovie);
          this.PlayVideo();
          break;
        }
        this.ClearBackground();
        break;
      case UIBackground.Type.Movie:
        App.instance.cameraManager.GetCamera().enabled = false;
        UIManager.instance.SetupCanvasCamera(this.mCanvas);
        GameUtility.SetActive(this.backgroundImage.gameObject, false);
        GameUtility.SetActive(this.binkPlayer.gameObject, true);
        if (this.binkPlayer.IsStopped() || this.ChooseBackgroundMovie() != this.binkPlayer.currentMovieName)
          this.PrepareMovie(this.ChooseBackgroundMovie());
        this.PlayVideo();
        break;
    }
  }

  public void Activate2DBackground()
  {
    this.mCanvas.renderMode = UnityEngine.RenderMode.ScreenSpaceCamera;
    this.mCanvas.worldCamera = this.backgroundCamera;
    this.backgroundCamera.enabled = true;
    this.mBackgroundImageBlur.enabled = App.instance.cameraManager.gameCamera.blur.enabled;
    string spriteName = Game.instance.sessionManager.eventDetails.circuit.spriteName;
    if (this.mLastBackgroundImageSpriteName != spriteName)
    {
      this.backgroundImage.texture = UnityEngine.Resources.Load<Texture>("UI/LoadingScreenImages/" + spriteName);
      this.mLastBackgroundImageSpriteName = spriteName;
    }
    GameUtility.SetActive(this.backgroundImage.gameObject, true);
  }

  public void ForceBackgroundChange()
  {
    this.PrepareMovie(this.ChooseBackgroundMovie());
    this.PlayVideo();
  }

  private void PlayVideo()
  {
    if (this.binkPlayer.IsPlaying())
      return;
    this.binkPlayer.Play(0, new Action(this.PlayVideo));
  }

  private string ChooseBackgroundMovie()
  {
    if (Game.IsActive() && !Game.instance.player.IsUnemployed())
    {
      switch (Game.instance.player.team.championship.series)
      {
        case Championship.Series.SingleSeaterSeries:
          return "FrontendBackgound";
        case Championship.Series.GTSeries:
          return "FrontendBackgoundGT";
        default:
          Debug.LogError((object) "Enum out of range, Error preparing the video.", (UnityEngine.Object) null);
          break;
      }
    }
    return "FrontendBackgound";
  }

  private void PrepareMovie(string inMovie)
  {
    this.binkPlayer.PrepareMovie(inMovie);
  }

  private void ClearBackground()
  {
    this.binkPlayer.Pause();
    UIManager.instance.SetupCanvasCamera(this.mCanvas);
    GameUtility.SetActive(this.backgroundImage.gameObject, false);
    GameUtility.SetActive(this.binkPlayer.gameObject, false);
  }

  private void Update()
  {
    if (!this.backgroundCamera.enabled)
      return;
    bool enabled = App.instance.cameraManager.gameCamera.blur.enabled;
    if (enabled == this.mBackgroundImageBlur.enabled)
      return;
    this.mBackgroundImageBlur.enabled = enabled;
  }

  public enum Type
  {
    None,
    Scene,
    Movie,
  }
}
