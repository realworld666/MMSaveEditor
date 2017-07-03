// Decompiled with JetBrains decompiler
// Type: NewCareerScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using ModdingSystem;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NewCareerScreen : UIScreen
{
  public Toggle tutorialToggle;
  public Toggle shortRaceLengthToggle;
  public Toggle mediumRaceLengthToggle;
  public Toggle longRaceLengthToggle;
  public UIPreferencesDropdown currencySymbol;
  public UIPreferencesDropdown dateFormat;
  public UIPreferencesDropdown aiDifficulty;
  public Toggle autosave;
  public UIWorkshopModsAndAssetsWidget modsAndAssetsWidget;
  public GameObject workshopContainer;
  private bool mAddedListener_OnMakeSoundToggleChanged;
  private bool mAllowMenuSounds;
  private bool mTogglesChanged;
  private bool mLeftScreenByBackButton;

  public override void OnStart()
  {
    base.OnStart();
    if (this.mAddedListener_OnMakeSoundToggleChanged)
      return;
    this.mAddedListener_OnMakeSoundToggleChanged = true;
    this.shortRaceLengthToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnMakeSoundToggleChanged));
    this.mediumRaceLengthToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnMakeSoundToggleChanged));
    this.longRaceLengthToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnMakeSoundToggleChanged));
    this.tutorialToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnMakeSoundOptionToggleChanged));
    this.autosave.onValueChanged.AddListener(new UnityAction<bool>(this.OnMakeSoundOptionToggleChanged));
    this.shortRaceLengthToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggleValueChanged));
    this.mediumRaceLengthToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggleValueChanged));
    this.longRaceLengthToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggleValueChanged));
    this.tutorialToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggleValueChanged));
    this.autosave.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggleValueChanged));
  }

  private void OnToggleValueChanged(bool inBool)
  {
    this.mTogglesChanged = true;
  }

  public override void OnEnter()
  {
    base.OnEnter();
    if (App.instance.gameStateManager.currentState.type == GameState.Type.NewGameSetup)
    {
      if (Game.instance != null)
      {
        Game.instance.Destroy();
        Game.instance = (Game) null;
      }
      Game.instance = new Game();
      Game.instance.gameType = Game.GameType.Career;
      Game.instance.SetupNewGame();
      Game.instance.queuedAutosave = true;
    }
    this.mAllowMenuSounds = false;
    UIManager.instance.ClearForwardStack();
    this.showNavigationBars = true;
    this.SetTopBarMode(UITopBar.Mode.Core);
    this.SetBottomBarMode(UIBottomBar.Mode.Core);
    this.EnableDropdownSound(this.dateFormat, false);
    this.EnableDropdownSound(this.currencySymbol, false);
    this.EnableDropdownSound(this.aiDifficulty, false);
    this.StartAllDropdowns();
    this.StartAllToggles();
    if (Game.instance.tutorialSystem.isTutorialActive)
      Game.instance.tutorialSystem.CancelAndResetTutorial(false);
    Game.instance.challengeManager.NotifyChallengeManagerOfGameEventAndCheckWin(ChallengeManager.ChallengeManagerGameEvents.NewCareer);
    if (SteamManager.Initialized && App.instance.gameStateManager.currentState.type == GameState.Type.NewGameSetup)
    {
      GameUtility.SetActive(this.workshopContainer, true);
      if (App.instance.modManager.GetNewGameModsCount(false) > 0)
        this.modsAndAssetsWidget.SetFilter(UIWorkshopModsAndAssetsWidget.FilterType.SubscribedItems);
      else
        this.modsAndAssetsWidget.SetFilter(UIWorkshopModsAndAssetsWidget.FilterType.AllItems);
      this.modsAndAssetsWidget.SetupCurrentView(UIWorkshopModsAndAssetsWidget.ViewType.Mods);
      App.instance.modManager.modLoader.activeModsController.RecordActiveMods();
    }
    else
      GameUtility.SetActive(this.workshopContainer, false);
    this.mAllowMenuSounds = true;
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionScreen, 0.0f);
    this.aiDifficulty.gameObject.SetActive(false);
    this.EnableDropdownSound(this.dateFormat, true);
    this.EnableDropdownSound(this.currencySymbol, true);
    this.EnableDropdownSound(this.aiDifficulty, true);
  }

  public override void OnExit()
  {
    if (!this.mLeftScreenByBackButton)
      Game.instance.tutorialSystem.SetTutorialActive(this.tutorialToggle.isOn);
    this.mLeftScreenByBackButton = false;
    if (this.workshopContainer.activeInHierarchy)
    {
      if (!this.modsAndAssetsWidget.activateStagingMod.isOn)
        App.instance.modManager.SaveActiveModsFile();
      App.instance.modManager.ClearQueriedItemsLists();
    }
    SteamMod.UnloadDownloadedTextures();
    base.OnExit();
  }

  private void LateUpdate()
  {
    if (!this.mTogglesChanged)
      return;
    this.mTogglesChanged = false;
    this.UpdatePreferences();
  }

  public override UIScreen.NavigationButtonEvent OnBackButton()
  {
    this.mLeftScreenByBackButton = true;
    return base.OnBackButton();
  }

  public override UIScreen.NavigationButtonEvent OnContinueButton()
  {
    this.UpdatePreferences();
    if (App.instance.gameStateManager.currentState.type == GameState.Type.NewGameSetup)
    {
      ModManager modManager = App.instance.modManager;
      if (modManager.IsAssetLoadRequiredForActiveMods() || modManager.modLoader.activeModsController.ActiveModsHaveChangedSinceLastRecording())
      {
        Action onConfirmValidation = (Action) (() =>
        {
          UIManager.instance.dialogBoxManager.GetDialog<WorkshopAssetLoading>().SetLoadingCompleteAction(WorkshopAssetLoading.LoadingCompleteAction.CreateNewGame, (SaveFileInfo) null);
          UIManager.instance.dialogBoxManager.Show("WorkshopAssetLoading");
        });
        if (modManager.modValidator.ValidateModOnNewCareer(onConfirmValidation))
          onConfirmValidation();
      }
      else
      {
        Game.instance.StartNewGame();
        UIManager.instance.ChangeScreen("CreatePlayerScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
      }
    }
    else
      UIManager.instance.ChangeScreen("CreatePlayerScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
    return base.OnContinueButton();
  }

  private void StartAllToggles()
  {
    switch (App.instance.preferencesManager.GetSettingEnum<PrefGameRaceLength.Type>(Preference.pName.Game_RaceLenghts, false))
    {
      case PrefGameRaceLength.Type.Short:
        this.shortRaceLengthToggle.isOn = true;
        break;
      case PrefGameRaceLength.Type.Medium:
        this.mediumRaceLengthToggle.isOn = true;
        break;
      case PrefGameRaceLength.Type.Long:
        this.longRaceLengthToggle.isOn = true;
        break;
    }
    this.autosave.isOn = App.instance.preferencesManager.GetSettingBool(Preference.pName.Game_Autosave, false);
  }

  private void OnMakeSoundToggleChanged(bool isOn)
  {
    if (isOn || !this.mAllowMenuSounds)
      return;
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
  }

  private void OnMakeSoundOptionToggleChanged(bool isOn)
  {
    if (!this.mAllowMenuSounds)
      return;
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
  }

  private void StartAllDropdowns()
  {
    this.currencySymbol.OnStart();
    this.dateFormat.OnStart();
    this.aiDifficulty.OnStart();
    this.RefreshDropdown(Preference.pName.Game_DateFormat, this.dateFormat);
    this.RefreshDropdown(Preference.pName.Game_CurrencySymbol, this.currencySymbol);
    this.RefreshDropdown(Preference.pName.Game_AIDevDifficulty, this.aiDifficulty);
  }

  private void EnableDropdownSound(UIPreferencesDropdown inDropDown, bool inValue)
  {
    inDropDown.allowMenuSounds = inValue;
  }

  private void RefreshDropdown(Preference.pName inName, UIPreferencesDropdown inDropDown)
  {
    string settingString = App.instance.preferencesManager.GetSettingString(inName, false);
    if (string.IsNullOrEmpty(settingString))
      return;
    inDropDown.SetValue(settingString);
  }

  private void UpdatePreferences()
  {
    this.UpdateRaceLengthPreferenceFromToggleGroup();
    this.UpdatePreference(Preference.pName.Game_Autosave, (object) this.autosave.isOn);
    this.UpdatePreference(Preference.pName.Game_CurrencySymbol, (object) this.currencySymbol.GetValue());
    this.UpdatePreference(Preference.pName.Game_DateFormat, (object) this.dateFormat.GetValue());
    this.UpdatePreference(Preference.pName.Game_AIDevDifficulty, (object) this.aiDifficulty.GetValue());
    App.instance.preferencesManager.ApplyChanges();
    App.instance.preferencesManager.gamePreferences.Load();
  }

  private void UpdatePreference(Preference.pName inName, object inValue)
  {
    App.instance.preferencesManager.SetSetting(inName, inValue, false);
    App.instance.preferencesManager.SetSetting(inName, inValue, true);
  }

  private void UpdateRaceLengthPreferenceFromToggleGroup()
  {
    bool isOn1 = this.shortRaceLengthToggle.isOn;
    bool isOn2 = this.mediumRaceLengthToggle.isOn;
    bool isOn3 = this.longRaceLengthToggle.isOn;
    PrefGameRaceLength.Type type = PrefGameRaceLength.Type.Medium;
    if (!isOn1 && isOn2 ^ isOn3 || isOn1 && !isOn2 && !isOn3)
    {
      if (isOn1)
        type = PrefGameRaceLength.Type.Short;
      else if (isOn2)
        type = PrefGameRaceLength.Type.Medium;
      else if (isOn3)
        type = PrefGameRaceLength.Type.Long;
      this.UpdatePreference(Preference.pName.Game_RaceLenghts, (object) type.ToString());
    }
    else
      Debug.LogWarningFormat("Could not set the race length. Toggle values are short {0}, medium {1}, long {2}.", (object) isOn1, (object) isOn2, (object) isOn3);
  }
}
