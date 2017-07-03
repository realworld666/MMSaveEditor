// Decompiled with JetBrains decompiler
// Type: PreferencesScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PreferencesScreen : UIScreen
{
  public UIPreferencesBar preferencesBar;
  public UIPreferencesGameSettings gameSettingsWidget;
  public UIPreferencesVideoSettings videoSettingsWidget;
  public UIPreferencesAudioSettings audioSettingsWidget;
  public UIPreferencesControlSettings controlsSettingsWidget;
  public GameObject gameSettingsRoot;
  public GameObject videoSettingsRoot;
  public GameObject audioSettingsRoot;
  public GameObject controlsSettingsRoot;
  public GameObject currentModeRoot;
  public bool defaultReset;
  private PreferencesScreen.Mode mMode;
  private UIPreferencesButtonWidget mButtons;
  private bool mVideoSettingsChanged;
  private bool mGameSettingsChanged;
  private bool mAudioSettingsChanged;
  private bool mControlSettingsChanged;
  private bool mRefreshDropdownLocalisation;
  private bool mLeavingScreen;
  private bool mUnpauseGameOnExit;

  public PreferencesScreen.Mode mode
  {
    get
    {
      return this.mMode;
    }
  }

  public bool leavingScreen
  {
    get
    {
      return this.mLeavingScreen;
    }
  }

  public override void OnStart()
  {
    base.OnStart();
    scSoundManager.BlockSoundEvents = true;
    this.mButtons = UIManager.instance.navigationBars.bottomBar.preferences.GetComponent<UIPreferencesButtonWidget>();
    this.mButtons.gameSettingsButton.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SwitchMode(PreferencesScreen.Mode.gameSettings)));
    this.mButtons.videoSettingsButton.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SwitchMode(PreferencesScreen.Mode.videoSettings)));
    this.mButtons.audioSettingsButton.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SwitchMode(PreferencesScreen.Mode.audioSettings)));
    this.mButtons.controlsSettingsButton.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.SwitchMode(PreferencesScreen.Mode.controlSettings)));
    this.preferencesBar.OnStart();
    this.gameSettingsWidget.OnStart();
    this.videoSettingsWidget.OnStart();
    this.audioSettingsWidget.OnStart();
    this.controlsSettingsWidget.OnStart();
    Localisation.OnLanguageChange += new Action(this.SetRefreshDropdownFlag);
    scSoundManager.BlockSoundEvents = false;
  }

  public override void OnEnter()
  {
    this.dontAddToBackStack = true;
    if (App.instance.preferencesManager.videoPreferences.currentFullscreenMode != Screen.fullScreen)
    {
      App.instance.preferencesManager.videoPreferences.currentFullscreenMode = Screen.fullScreen;
      App.instance.preferencesManager.SetSetting(Preference.pName.Video_Display, !Screen.fullScreen ? (object) "Windowed" : (object) "Fullscreen", false);
      App.instance.preferencesManager.SetSetting(Preference.pName.Video_Display, !Screen.fullScreen ? (object) "Windowed" : (object) "Fullscreen", true);
      this.videoSettingsWidget.Refresh();
      this.videoSettingsWidget.RefreshUIDropdownLocalisation();
    }
    if (this.mRefreshDropdownLocalisation)
    {
      this.gameSettingsWidget.RefreshUIDropdownLocalisation();
      this.videoSettingsWidget.RefreshUIDropdownLocalisation();
      this.mRefreshDropdownLocalisation = false;
    }
    base.OnEnter();
    this.showNavigationBars = true;
    this.SetTopBarMode(UITopBar.Mode.Core);
    this.SetBottomBarMode(UIBottomBar.Mode.Preferences);
    GameUtility.SetActive(UIManager.instance.navigationBars.bottomBar.preferences, true);
    this.SwitchMode(PreferencesScreen.Mode.videoSettings);
    this.ResetSettingsChangedStates();
    if (Game.IsActive())
    {
      if (Game.instance.tutorialSystem.isTutorialActive)
        Game.instance.tutorialSystem.PauseTutorial();
      this.mUnpauseGameOnExit = Game.IsActive() && !Game.instance.time.isPaused;
      if (this.mUnpauseGameOnExit)
      {
        Game.instance.time.Pause(GameTimer.PauseType.Game);
        scSoundManager.Instance.Pause(false, false, false);
      }
    }
    this.continueButtonLabel = Localisation.LocaliseID("PSG_10003949", (GameObject) null);
    this.RefreshAll();
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionScreen, 0.0f);
  }

  public override void OnExit()
  {
    if (Game.IsActive())
    {
      if (Game.instance.tutorialSystem.isTutorialActive)
        Game.instance.tutorialSystem.UnPauseTutorial();
      if (this.mUnpauseGameOnExit)
      {
        Game.instance.time.UnPause(GameTimer.PauseType.Game);
        scSoundManager.Instance.UnPause(false);
      }
    }
    base.OnExit();
  }

  public void ResetWidgets()
  {
    GameUtility.SetActive(this.gameSettingsRoot, false);
    GameUtility.SetActive(this.videoSettingsRoot, false);
    GameUtility.SetActive(this.audioSettingsRoot, false);
    GameUtility.SetActive(this.controlsSettingsRoot, false);
  }

  public void SetActiveButton(Toggle inToggle)
  {
    this.mButtons.settingsButtonsGroup.SetAllTogglesOff();
    inToggle.isOn = true;
  }

  public void SwitchMode(PreferencesScreen.Mode inMode)
  {
    if (inMode == this.mMode)
      return;
    if (inMode == PreferencesScreen.Mode.controlSettings)
      this.controlsSettingsWidget.CancelListen();
    this.mMode = inMode;
    this.ResetWidgets();
    switch (this.mMode)
    {
      case PreferencesScreen.Mode.gameSettings:
        this.currentModeRoot = this.gameSettingsRoot;
        this.SetActiveButton(this.mButtons.gameSettingsButton);
        break;
      case PreferencesScreen.Mode.videoSettings:
        this.currentModeRoot = this.videoSettingsRoot;
        this.SetActiveButton(this.mButtons.videoSettingsButton);
        break;
      case PreferencesScreen.Mode.audioSettings:
        this.currentModeRoot = this.audioSettingsRoot;
        this.SetActiveButton(this.mButtons.audioSettingsButton);
        break;
      case PreferencesScreen.Mode.controlSettings:
        this.currentModeRoot = this.controlsSettingsRoot;
        this.SetActiveButton(this.mButtons.controlsSettingsButton);
        break;
    }
    this.preferencesBar.UpdateButtonsState();
    GameUtility.SetActive(this.preferencesBar.gameObject, true);
    if (this.currentModeRoot.activeSelf)
      return;
    GameUtility.SetActive(this.currentModeRoot, true);
  }

  public void RefreshScreen()
  {
    switch (this.mMode)
    {
      case PreferencesScreen.Mode.gameSettings:
        this.gameSettingsWidget.Refresh();
        break;
      case PreferencesScreen.Mode.videoSettings:
        this.videoSettingsWidget.Refresh();
        break;
      case PreferencesScreen.Mode.audioSettings:
        this.audioSettingsWidget.Refresh();
        break;
      case PreferencesScreen.Mode.controlSettings:
        this.controlsSettingsWidget.Refresh();
        break;
    }
  }

  public void RefreshAll()
  {
    this.audioSettingsWidget.Refresh();
    this.controlsSettingsWidget.Refresh();
    this.gameSettingsWidget.Refresh();
    this.videoSettingsWidget.Refresh();
  }

  public void ResetDefault()
  {
    switch (this.mMode)
    {
      case PreferencesScreen.Mode.gameSettings:
        App.instance.preferencesManager.ResetGroupDefault(Preference.pGroup.Game);
        break;
      case PreferencesScreen.Mode.videoSettings:
        App.instance.preferencesManager.ResetGroupDefault(Preference.pGroup.Video);
        break;
      case PreferencesScreen.Mode.audioSettings:
        App.instance.preferencesManager.ResetGroupDefault(Preference.pGroup.Audio);
        break;
      case PreferencesScreen.Mode.controlSettings:
        App.instance.preferencesManager.ResetGroupDefault(Preference.pGroup.Controls);
        this.controlsSettingsWidget.CancelListen();
        break;
    }
    this.UpdateSettingsChangedStates(true);
    this.RefreshScreen();
  }

  public void CancelChanges()
  {
    App.instance.preferencesManager.CancelChanges();
    if (this.mMode == PreferencesScreen.Mode.controlSettings)
      this.controlsSettingsWidget.CancelListen();
    this.RefreshScreen();
    this.ResetSettingsChangedStates();
  }

  public void UpdateSettingsChangedStates(bool inChanged)
  {
    this.UpdateSettingsChangedStates(inChanged, this.mMode);
  }

  public void UpdateSettingsChangedStates(bool inChanged, PreferencesScreen.Mode inMode)
  {
    switch (inMode)
    {
      case PreferencesScreen.Mode.gameSettings:
        this.mGameSettingsChanged = inChanged;
        break;
      case PreferencesScreen.Mode.videoSettings:
        this.mVideoSettingsChanged = inChanged;
        this.videoSettingsWidget.OnSetupChange();
        break;
      case PreferencesScreen.Mode.audioSettings:
        this.mAudioSettingsChanged = inChanged;
        break;
      case PreferencesScreen.Mode.controlSettings:
        this.mControlSettingsChanged = inChanged;
        break;
    }
    this.ResolveSettingsChangedStates();
  }

  private void ResolveSettingsChangedStates()
  {
    if (this.mMode == PreferencesScreen.Mode.videoSettings)
      this.preferencesBar.applyVideoChangesButton.interactable = this.mVideoSettingsChanged;
    this.needsPlayerConfirmation = this.mVideoSettingsChanged || this.mGameSettingsChanged || this.mControlSettingsChanged || this.mAudioSettingsChanged;
    this.preferencesBar.UpdateButtonsState();
  }

  public void ResetSettingsChangedStates()
  {
    this.UpdateSettingsChangedStates(false, PreferencesScreen.Mode.audioSettings);
    this.UpdateSettingsChangedStates(false, PreferencesScreen.Mode.controlSettings);
    this.UpdateSettingsChangedStates(false, PreferencesScreen.Mode.gameSettings);
    this.UpdateSettingsChangedStates(false, PreferencesScreen.Mode.videoSettings);
  }

  public override void OpenConfirmDialogBox(Action inAction)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    PreferencesScreen.\u003COpenConfirmDialogBox\u003Ec__AnonStorey72 boxCAnonStorey72 = new PreferencesScreen.\u003COpenConfirmDialogBox\u003Ec__AnonStorey72();
    // ISSUE: reference to a compiler-generated field
    boxCAnonStorey72.inAction = inAction;
    // ISSUE: reference to a compiler-generated field
    boxCAnonStorey72.\u003C\u003Ef__this = this;
    GenericConfirmation dialog = UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>();
    // ISSUE: reference to a compiler-generated method
    Action inCancelAction = new Action(boxCAnonStorey72.\u003C\u003Em__F9);
    // ISSUE: reference to a compiler-generated method
    Action inConfirmAction = new Action(boxCAnonStorey72.\u003C\u003Em__FA);
    string inTitle = Localisation.LocaliseID("PSG_10003942", (GameObject) null);
    string inText = Localisation.LocaliseID("PSG_10009114", (GameObject) null);
    string inCancelString = Localisation.LocaliseID("PSG_10009115", (GameObject) null);
    string inConfirmString = Localisation.LocaliseID("PSG_10009116", (GameObject) null);
    dialog.Show(inCancelAction, inCancelString, inConfirmAction, inConfirmString, inText, inTitle);
  }

  public void ApplyAllChanges()
  {
    this.ApplyChanges(PreferencesScreen.Mode.audioSettings, PreferencesScreen.Mode.controlSettings, PreferencesScreen.Mode.gameSettings, PreferencesScreen.Mode.videoSettings);
  }

  public void ApplyChanges(params PreferencesScreen.Mode[] inModesToApply)
  {
    foreach (int num in inModesToApply)
    {
      switch (num)
      {
        case 0:
          if (this.mGameSettingsChanged)
          {
            this.gameSettingsWidget.ConfirmSettings();
            this.UpdateSettingsChangedStates(false, PreferencesScreen.Mode.gameSettings);
            break;
          }
          break;
        case 1:
          if (this.mVideoSettingsChanged)
          {
            this.videoSettingsWidget.ConfirmSettings();
            this.UpdateSettingsChangedStates(false, PreferencesScreen.Mode.videoSettings);
            break;
          }
          break;
        case 2:
          if (this.mAudioSettingsChanged)
          {
            this.audioSettingsWidget.ConfirmSettings();
            this.UpdateSettingsChangedStates(false, PreferencesScreen.Mode.audioSettings);
            break;
          }
          break;
        case 3:
          if (this.mControlSettingsChanged)
          {
            this.controlsSettingsWidget.ConfirmSettings();
            this.UpdateSettingsChangedStates(false, PreferencesScreen.Mode.controlSettings);
            break;
          }
          break;
      }
    }
    this.RefreshScreen();
    App.instance.preferencesManager.ApplyChanges();
  }

  public override UIScreen.NavigationButtonEvent OnContinueButton()
  {
    this.mLeavingScreen = true;
    this.ApplyAllChanges();
    this.OnContinueButtonComplete(false);
    return UIScreen.NavigationButtonEvent.HandledByScreen;
  }

  public void OnContinueButtonComplete(bool inForce = false)
  {
    if (UIManager.instance.dialogBoxManager.GetActiveDialogBoxCount() > 0 && !inForce)
      return;
    UIManager.instance.OnBackButton();
    this.mLeavingScreen = false;
  }

  public void SetRefreshDropdownFlag()
  {
    this.mRefreshDropdownLocalisation = true;
  }

  public enum Mode
  {
    [LocalisationID("PSG_10002451")] gameSettings,
    [LocalisationID("PSG_10004769")] videoSettings,
    [LocalisationID("PSG_10004770")] audioSettings,
    [LocalisationID("PSG_10004771")] controlSettings,
  }
}
