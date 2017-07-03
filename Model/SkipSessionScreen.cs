// Decompiled with JetBrains decompiler
// Type: SkipSessionScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkipSessionScreen : UIScreen
{
  public Image[] speedIcons = new Image[0];
  public Color selectedColor = Color.white;
  public Color inactiveColor = Color.white;
  private int mLapNumber = -1;
  private float mTime = -1f;
  public Slider progressSlider;
  public TextMeshProUGUI descriptionLabel;
  public TextMeshProUGUI sessionTimeLabel;
  public MinimapWidget minimapWidget;
  public FullStandingsDropdown fullStandingsDropdown;
  public UICommentaryFeedWidget commentaryWidget;
  public Button speedButton;
  public GameObject playIcon;
  public GameObject[] pauseGameObjects;
  public Button pausePlayButton;
  public CanvasGroup canvasGroup;
  private bool mExitScreen;
  private scSoundContainer mTimerSound;
  private scSoundContainer mTimerSound2;
  private SessionDetails mSimulatedSession;
  private GameTimer.SimSkipSpeed mSkipSpeed;

  public override void OnStart()
  {
    base.OnStart();
    this.fullStandingsDropdown.gameObject.SetActive(false);
    this.speedButton.onClick.AddListener(new UnityAction(this.IncreaseSpeed));
    this.pausePlayButton.onClick.AddListener(new UnityAction(this.PauseOrPlay));
  }

  private void PauseOrPlay()
  {
    Game.instance.time.PauseOrPlaySkipSim();
    this.UpdateIcons();
  }

  private void IncreaseSpeed()
  {
    Game.instance.time.PlaySkipSim();
    Game.instance.time.IncreaseSkipSimSpeed();
    this.UpdateIcons();
  }

  private void UpdateIcons()
  {
    if (Game.instance.sessionManager.eventDetails.currentSession.hasEnded)
      return;
    if (Game.instance.time.simSkipSpeed == GameTimer.SimSkipSpeed.Pause)
    {
      this.StopSounds();
    }
    else
    {
      scSoundManager.CheckPlaySound(SoundID.Sfx_TimerTickLoop, ref this.mTimerSound, 0.0f);
      scSoundManager.CheckPlaySound(SoundID.Sfx_TimerTickStart, ref this.mTimerSound2, 0.0f);
    }
    for (int index = 0; index < this.speedIcons.Length; ++index)
    {
      if ((GameTimer.SimSkipSpeed) index < Game.instance.time.simSkipSpeed)
        this.speedIcons[index].color = this.selectedColor;
      else
        this.speedIcons[index].color = this.inactiveColor;
    }
    GameUtility.SetActive(this.playIcon, Game.instance.time.simSkipSpeed != GameTimer.SimSkipSpeed.Pause);
    for (int index = 0; index < this.pauseGameObjects.Length; ++index)
      GameUtility.SetActive(this.pauseGameObjects[index], Game.instance.time.simSkipSpeed == GameTimer.SimSkipSpeed.Pause);
    this.mSkipSpeed = Game.instance.time.simSkipSpeed;
  }

  public override void OnEnter()
  {
    base.OnEnter();
    Game.instance.time.PlaySkipSim();
    this.showNavigationBars = true;
    this.mExitScreen = false;
    UIManager.instance.navigationBars.topBar.SetMode(UITopBar.Mode.Session);
    UIManager.instance.navigationBars.topBar.topBarSessionStandingsContainer.gameObject.SetActive(false);
    UIManager.instance.navigationBars.bottomBar.SetMode(UIBottomBar.Mode.PlayerAction);
    UIManager.instance.navigationBars.bottomBar.ShowCancelButton(false);
    UIManager.instance.navigationBars.bottomBar.SetCoreButtonsActive(false);
    UIManager.instance.ClearBackStack();
    Game.instance.sessionManager.SetCircuitActive(true);
    App.instance.cameraManager.Deactivate();
    App.instance.cameraManager.gameCamera.SetBlurActive(true);
    UIManager.instance.UIBackground.Activate2DBackground();
    RaceEventDetails eventDetails = Game.instance.sessionManager.eventDetails;
    switch (eventDetails.currentSession.sessionType)
    {
      case SessionDetails.SessionType.Practice:
        this.descriptionLabel.text = Localisation.LocaliseID("PSG_10005009", (GameObject) null);
        break;
      case SessionDetails.SessionType.Qualifying:
        if (Game.instance.sessionManager.eventDetails.hasSeveralQualifyingSessions)
        {
          StringVariableParser.intValue1 = Game.instance.sessionManager.eventDetails.currentSession.sessionNumberForUI;
          this.descriptionLabel.text = Localisation.LocaliseID("PSG_10011524", (GameObject) null);
          break;
        }
        this.descriptionLabel.text = Localisation.LocaliseID("PSG_10009385", (GameObject) null);
        break;
      case SessionDetails.SessionType.Race:
        this.descriptionLabel.text = Localisation.LocaliseID("PSG_10005009", (GameObject) null);
        break;
    }
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionScreen, 0.0f);
    scSoundManager.CheckPlaySound(SoundID.Sfx_TimerTickLoop, ref this.mTimerSound, 0.0f);
    scSoundManager.CheckPlaySound(SoundID.Sfx_TimerTickStart, ref this.mTimerSound2, 0.0f);
    this.minimapWidget.Setup();
    this.fullStandingsDropdown.gameObject.SetActive(true);
    this.commentaryWidget.Setup(this.mSimulatedSession != eventDetails.currentSession);
    this.mSimulatedSession = eventDetails.currentSession;
    this.UpdateIcons();
  }

  public override void OnExit()
  {
    base.OnExit();
    App.instance.cameraManager.gameCamera.SetBlurActive(false);
    this.fullStandingsDropdown.gameObject.SetActive(false);
    UIManager.instance.navigationBars.topBar.topBarSessionStandingsContainer.gameObject.SetActive(true);
  }

  private void Update()
  {
    SessionHeaderWidget.GetRaceTypeString(ref this.sessionTimeLabel, ref this.mLapNumber, ref this.mTime);
    this.progressSlider.value = Game.instance.sessionManager.GetNormalizedSessionTime();
    bool hasEnded = Game.instance.sessionManager.eventDetails.currentSession.hasEnded;
    if (hasEnded && !App.instance.gameStateManager.isChangingState)
    {
      if (!this.mExitScreen && UIManager.instance.canChangeScreen)
      {
        this.mExitScreen = true;
        scSoundManager.Instance.PlaySound(SoundID.Sfx_TimerTickEnd, 0.0f);
        this.StopSounds();
        UIManager.instance.navigationBars.bottomBar.SetPlayerActionContinueInteractable(true);
      }
    }
    else if (!hasEnded)
      UIManager.instance.navigationBars.bottomBar.SetPlayerActionContinueInteractable(false);
    if (hasEnded)
    {
      this.canvasGroup.alpha = 0.5f;
      this.canvasGroup.interactable = false;
    }
    else
    {
      this.canvasGroup.alpha = 1f;
      this.canvasGroup.interactable = true;
    }
    if (this.mSkipSpeed == Game.instance.time.simSkipSpeed)
      return;
    this.UpdateIcons();
  }

  public override UIScreen.NavigationButtonEvent OnContinueButton()
  {
    Game.instance.stateInfo.GoToNextState();
    this.StopSounds();
    return UIScreen.NavigationButtonEvent.HandledByScreen;
  }

  private void OnDisable()
  {
    this.StopSounds();
  }

  private void StopSounds()
  {
    scSoundManager.CheckStopSound(ref this.mTimerSound);
    scSoundManager.CheckStopSound(ref this.mTimerSound2);
  }
}
