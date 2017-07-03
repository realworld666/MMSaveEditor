// Decompiled with JetBrains decompiler
// Type: TrackDetailsScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;

public class TrackDetailsScreen : UIScreen
{
  public Flag flag;
  public TextMeshProUGUI yearBuiltLabel;
  public TextMeshProUGUI circuitDirectionLabel;
  public TextMeshProUGUI trackTypeLabel;
  public TextMeshProUGUI safteyCarChanceLabel;
  public TextMeshProUGUI historyLabel;
  public TrackInfoWidget trackInfo;
  public LayoutInfoWidget layoutInfo;
  public SectorsWidget sectors;
  public CornersWidget corners;
  public ElevationWidget elevation;
  public MinimapWidget minimapWidget;
  private CameraManager.CameraMode mPreviousCameraMode;
  private TrackDetailsScreen.State mState;
  private BaseTrackDetailsWidget currentWidget;

  public override void OnEnter()
  {
    base.OnEnter();
    this.showNavigationBars = true;
    this.mPreviousCameraMode = App.instance.cameraManager.activeMode;
    App.instance.cameraManager.ActivateMode(CameraManager.CameraMode.TrackGuide);
    App.instance.cameraManager.gameCamera.SetBlurActive(false);
    Game.instance.sessionManager.SetCircuitActive(true);
    if (App.instance.gameStateManager.currentState.type == GameState.Type.PreSessionHUB)
    {
      this.SetTopBarMode(UITopBar.Mode.Session);
      this.SetBottomBarMode(UIBottomBar.Mode.PreSession);
    }
    else
    {
      this.SetTopBarMode(UITopBar.Mode.Session);
      this.SetBottomBarMode(UIBottomBar.Mode.Core);
    }
    this.minimapWidget.Setup();
    this.SetState(TrackDetailsScreen.State.TrackInfo);
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionDrivers, 0.0f);
  }

  private void SetCircuitInfo()
  {
    this.yearBuiltLabel.text = "1983";
    this.circuitDirectionLabel.text = "Counter Clockwise";
    this.trackTypeLabel.text = "Street";
    this.safteyCarChanceLabel.text = "High Chance";
    this.historyLabel.text = "History Text";
  }

  public override void OnExit()
  {
    base.OnExit();
    if (this.mPreviousCameraMode == CameraManager.CameraMode.None)
      return;
    App.instance.cameraManager.ActivateMode(this.mPreviousCameraMode);
  }

  private void Update()
  {
    if (!this.currentWidget.isComplete)
      return;
    this.GoToNextState();
  }

  private void GoToNextState()
  {
    bool flag = false;
    switch (this.mState)
    {
      case TrackDetailsScreen.State.TrackInfo:
        this.SetState(TrackDetailsScreen.State.LayoutInfo);
        break;
      case TrackDetailsScreen.State.LayoutInfo:
        this.SetState(TrackDetailsScreen.State.Sectors);
        break;
      case TrackDetailsScreen.State.Sectors:
        this.SetState(TrackDetailsScreen.State.Corners);
        break;
      case TrackDetailsScreen.State.Corners:
        this.SetState(TrackDetailsScreen.State.Elevation);
        break;
      case TrackDetailsScreen.State.Elevation:
        this.SetState(TrackDetailsScreen.State.TrackInfo);
        flag = true;
        break;
    }
    if (!flag || App.instance.gameStateManager.currentState.type == GameState.Type.PreSessionHUB)
      return;
    this.ContinueToNextScreen();
  }

  private void SetState(TrackDetailsScreen.State inState)
  {
    this.mState = inState;
    this.trackInfo.Hide();
    this.layoutInfo.Hide();
    this.sectors.Hide();
    this.corners.Hide();
    this.elevation.Hide();
    switch (this.mState)
    {
      case TrackDetailsScreen.State.TrackInfo:
        this.currentWidget = (BaseTrackDetailsWidget) this.trackInfo;
        break;
      case TrackDetailsScreen.State.LayoutInfo:
        this.currentWidget = (BaseTrackDetailsWidget) this.layoutInfo;
        break;
      case TrackDetailsScreen.State.Sectors:
        this.currentWidget = (BaseTrackDetailsWidget) this.sectors;
        break;
      case TrackDetailsScreen.State.Corners:
        this.currentWidget = (BaseTrackDetailsWidget) this.corners;
        break;
      case TrackDetailsScreen.State.Elevation:
        this.currentWidget = (BaseTrackDetailsWidget) this.elevation;
        break;
    }
    this.currentWidget.Show();
  }

  private void SetCircuitDetails()
  {
  }

  private void SetCircuitStats()
  {
  }

  public override UIScreen.NavigationButtonEvent OnContinueButton()
  {
    if (App.instance.gameStateManager.currentState.type != GameState.Type.PracticePreSession)
      return base.OnContinueButton();
    this.ContinueToNextScreen();
    return UIScreen.NavigationButtonEvent.HandledByScreen;
  }

  private void ContinueToNextScreen()
  {
    if (UIManager.instance.IsScreenOpen("DayTransitionScreen"))
      return;
    UIManager.instance.ChangeScreen("DayTransitionScreen", UIManager.ScreenTransition.Fade, 1f, (Action) null, UIManager.NavigationType.Normal);
  }

  public enum State
  {
    TrackInfo,
    LayoutInfo,
    Sectors,
    Corners,
    Elevation,
  }
}
