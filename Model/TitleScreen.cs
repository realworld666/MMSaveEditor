// Decompiled with JetBrains decompiler
// Type: TitleScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Diagnostics;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TitleScreen : UIScreen
{
  public GameObject profile;
  public GameObject continuePanel;
  public GameObject newCareerSmall;
  public GameObject newCareerLarge;
  public GameObject newCareerCamera;
  public GameObject newCareerBackground;
  public GameObject continueCareerCamera;
  public GameObject continueCareerBackground;
  public UIGenericCarBackground continueCareerCarBackground;
  public GameObject steamWorkshopPanel;
  public TextMeshProUGUI lastGameDate;
  public TextMeshProUGUI lastGameTeamName;
  public TextMeshProUGUI lastGameChampionship;
  public TextMeshProUGUI lastGamePlayerName;
  public TextMeshProUGUI challengesAvailableLabel;
  public UITeamLogo lastGameTeamLogo;
  public GameObject workshopLogo;
  public UICharacterPortrait playerPortrait;
  public Flag playerFlag;
  public Button continueCareerButton;
  public Button newCareerButton;
  public Button challengesButton;
  public Button singleRaceButton;
  public Button loadCareerButton;
  public Button preferencesButton;
  public Button newContentButton;
  public Button steamWorkshopButton;
  public Button exitButton;
  public Button newsButton;
  public Button communityButton;
  public Button facebookButton;
  public Button twitterButton;
  public Button creditsButton;
  [SerializeField]
  private GameObject newContentOffline;
  [SerializeField]
  private GameObject workshopOffline;
  private StudioScene mStudioScene;

  public override void OnStart()
  {
    base.OnStart();
    this.continueCareerButton.onClick.AddListener(new UnityAction(this.OnContinueCareerButton));
    this.newCareerButton.onClick.AddListener(new UnityAction(this.OnNewCareerButton));
    this.challengesButton.onClick.AddListener(new UnityAction(this.OnChallengesButton));
    this.singleRaceButton.onClick.AddListener(new UnityAction(this.OnSingleRaceButton));
    this.loadCareerButton.onClick.AddListener(new UnityAction(this.OnLoadCareerButton));
    this.preferencesButton.onClick.AddListener(new UnityAction(this.OnPreferencesButton));
    this.exitButton.onClick.AddListener(new UnityAction(this.OnExitButton));
    this.newsButton.onClick.AddListener(new UnityAction(this.OnNewsButton));
    this.communityButton.onClick.AddListener(new UnityAction(this.OnCommunityButton));
    this.facebookButton.onClick.AddListener(new UnityAction(this.OnFacebookButton));
    this.twitterButton.onClick.AddListener(new UnityAction(this.OnTwitterButton));
    this.creditsButton.onClick.AddListener(new UnityAction(this.OnCreditsButton));
  }

  private void SetupSteamButtons()
  {
    GameUtility.SetActive(this.steamWorkshopButton.gameObject, SteamManager.Initialized);
    GameUtility.SetActive(this.newContentButton.gameObject, SteamManager.Initialized);
    if (!SteamManager.Initialized)
      return;
    this.newContentButton.onClick.AddListener(new UnityAction(this.OnNewContentButton));
    this.steamWorkshopButton.onClick.AddListener(new UnityAction(this.OnSteamWorkshopButton));
    bool flag = SteamManager.IsSteamOnline();
    GameUtility.SetActive(this.newContentOffline, !flag);
    GameUtility.SetActive(this.workshopOffline, !flag);
  }

  public override void OnEnter()
  {
    base.OnEnter();
    if (Game.instance != null)
    {
      Game.instance.Destroy();
      Game.instance = (Game) null;
    }
    UIManager.instance.ClearNavigationStacks();
    this.showNavigationBars = false;
    this.SetupSteamButtons();
    App.instance.assetManager.AllowModdingFrontendCarModel(false);
    this.OnFocus();
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionHome, 0.0f);
    scMusicController.Start();
    App.instance.dlcManager.OnOwnedDlcChanged += new Action(this.OnFocus);
  }

  private void BlockChallenges()
  {
    this.challengesButton.interactable = false;
  }

  public override void OnFocus()
  {
    App.instance.saveSystem.Refresh();
    SaveFileInfo mostRecentSave = App.instance.saveSystem.mostRecentSave;
    this.mStudioScene = (StudioScene) null;
    bool inIsActive1;
    if (mostRecentSave != null)
    {
      inIsActive1 = true;
      bool flag = mostRecentSave.gameInfo.teamName == string.Empty;
      using (GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe())
      {
        StringBuilder stringBuilder = builderSafe.stringBuilder;
        stringBuilder.Append(GameUtility.FormatDateTimeToLongDateStringWithDOW(mostRecentSave.gameInfo.gameTime, string.Empty));
        stringBuilder.Append(" ");
        stringBuilder.Append(GameUtility.GetLocalised12HourTime(mostRecentSave.gameInfo.gameTime, true));
        this.lastGameDate.text = stringBuilder.ToString();
      }
      this.lastGameTeamName.text = mostRecentSave.gameInfo.teamName;
      this.lastGameChampionship.text = mostRecentSave.gameInfo.championship;
      this.lastGamePlayerName.text = mostRecentSave.gameInfo.playerName;
      if (mostRecentSave.gameInfo.teamLogo == null)
      {
        this.lastGameTeamLogo.SetTeam(!flag ? mostRecentSave.gameInfo.teamLogoID : -1);
      }
      else
      {
        TeamLogo teamLogo = mostRecentSave.gameInfo.teamLogo;
        this.lastGameTeamLogo.SetCustomTeamLogo(teamLogo.styleID, teamLogo.teamFirstName, teamLogo.teamLasttName, mostRecentSave.gameInfo.teamColor);
      }
      this.playerPortrait.SetPortrait(mostRecentSave.gameInfo.playerPortrait, mostRecentSave.gameInfo.playerGender, mostRecentSave.gameInfo.playerAge, mostRecentSave.gameInfo.teamLogoID, mostRecentSave.gameInfo.teamColor, UICharacterPortraitBody.BodyType.Chairman, -1, -1);
      this.playerFlag.SetNationality(mostRecentSave.gameInfo.playerNationality);
      this.continueCareerCarBackground.SetCarBackground(mostRecentSave.gameInfo.championshipSeries);
      App.instance.uiTeamColourManager.SetCurrentColour(mostRecentSave.gameInfo.teamColor, true, (GameObject) null);
      this.LoadScene(mostRecentSave);
    }
    else
    {
      inIsActive1 = false;
      this.continueCareerButton.interactable = false;
      App.instance.uiTeamColourManager.SetToPlayerTeamColour(true);
      this.LoadScene((SaveFileInfo) null);
    }
    this.continueCareerButton.interactable = inIsActive1;
    this.profile.SetActive(inIsActive1);
    this.continuePanel.SetActive(inIsActive1);
    GameUtility.SetActive(this.newCareerSmall, inIsActive1);
    GameUtility.SetActive(this.newCareerLarge, !inIsActive1);
    GameUtility.SetActive(this.newCareerCamera, this.screenMode == UIScreen.ScreenMode.Mode3D);
    GameUtility.SetActive(this.newCareerBackground, this.screenMode == UIScreen.ScreenMode.Mode2D);
    bool inIsActive2 = mostRecentSave != null && mostRecentSave.IsWorkshopSave();
    GameUtility.SetActive(this.continueCareerCamera, !inIsActive2 && this.screenMode == UIScreen.ScreenMode.Mode3D);
    GameUtility.SetActive(this.continueCareerBackground, inIsActive2 || this.screenMode == UIScreen.ScreenMode.Mode2D);
    GameUtility.SetActive(this.lastGameTeamLogo.gameObject, !inIsActive2);
    GameUtility.SetActive(this.workshopLogo, inIsActive2);
    StringVariableParser.intValue1 = 2;
    this.challengesAvailableLabel.text = Localisation.LocaliseID("PSG_10010219", (GameObject) null);
  }

  private void LoadScene(SaveFileInfo mostRecentSave)
  {
    SceneManager.instance.SwitchScene("TrackFrontEnd");
    GameObject sceneGameObject = SceneManager.instance.GetSceneGameObject("TrackFrontEnd");
    if (!((UnityEngine.Object) sceneGameObject != (UnityEngine.Object) null) || this.screenMode != UIScreen.ScreenMode.Mode3D)
      return;
    this.mStudioScene = sceneGameObject.GetComponent<StudioScene>();
    this.mStudioScene.SetCarType(StudioScene.Car.CurrentCar);
    this.mStudioScene.ResetMode();
    if (mostRecentSave != null)
    {
      if (mostRecentSave.gameInfo.teamName == string.Empty)
        this.mStudioScene.SetDefaultCarVisuals(-1, 0);
      else
        this.mStudioScene.SetSavedCarVisuals(-1, 0, mostRecentSave.gameInfo.playerTeamCarData);
      this.mStudioScene.EnableCamera("ToTextureProfileCamera2");
    }
    else
    {
      this.mStudioScene.SetDefaultCarVisuals(-1, 0);
      this.mStudioScene.EnableCamera("ToTextureProfileCamera3");
      this.mStudioScene.SetSeries(Championship.Series.SingleSeaterSeries);
    }
  }

  private void UnloadScene()
  {
    if ((UnityEngine.Object) this.mStudioScene == (UnityEngine.Object) null)
      return;
    this.mStudioScene.TuneSpotlight(true);
    this.mStudioScene.SetCameraTargetToTrackAlongCar(false);
    SceneManager.instance.LeaveCurrentScene();
  }

  public override void OnExit()
  {
    App.instance.assetManager.AllowModdingFrontendCarModel(true);
    base.OnExit();
    App.instance.dlcManager.OnOwnedDlcChanged -= new Action(this.OnFocus);
  }

  public override void OnExit(UIScreen inNextScreen)
  {
    App.instance.assetManager.AllowModdingFrontendCarModel(true);
    this.UnloadScene();
    App.instance.dlcManager.OnOwnedDlcChanged -= new Action(this.OnFocus);
  }

  private void OnNewCareerButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Continue, 0.0f);
    GameUtility.Assert(Game.instance == null, "Game.instance == null", (UnityEngine.Object) null);
    App.instance.gameStateManager.SetState(GameState.Type.NewGameSetup, GameStateManager.StateChangeType.CheckForFadedScreenChange, false);
  }

  private void OnContinueCareerButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Continue, 0.0f);
    Action onConfirmValidation = (Action) (() =>
    {
      if (App.instance.modManager.IsAssetLoadRequiredForActiveMods())
      {
        UIManager.instance.dialogBoxManager.GetDialog<WorkshopAssetLoading>().SetLoadingCompleteAction(WorkshopAssetLoading.LoadingCompleteAction.LoadMostRecentSave, (SaveFileInfo) null);
        UIManager.instance.dialogBoxManager.Show("WorkshopAssetLoading");
      }
      else
        App.instance.saveSystem.LoadMostRecentFile();
    });
    if (!App.instance.modManager.modValidator.ValidateSubscribedModsOnContinueGame(App.instance.saveSystem.mostRecentSave, onConfirmValidation))
      return;
    onConfirmValidation();
  }

  private void OnLoadCareerButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    SaveLoadDialog.ShowLoad();
  }

  private void OnPreferencesButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    UIManager.instance.ChangeScreen("PreferencesScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }

  private void OnChallengesButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Continue, 0.0f);
    GameUtility.Assert(Game.instance == null, "Game.instance == null", (UnityEngine.Object) null);
    App.instance.gameStateManager.SetState(GameState.Type.ChallengeSetup, GameStateManager.StateChangeType.CheckForFadedScreenChange, false);
  }

  private void OnSteamWorkshopButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    UIManager.instance.ChangeScreen("WorkshopScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }

  private void OnNewContentButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    UIManager.instance.ChangeScreen("NewContentScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }

  private void OnExitButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Back, 0.0f);
    Application.Quit();
  }

  private void OnNewsButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    Process.Start("http://www.motorsportmanager.com/news");
  }

  private void OnCommunityButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    Process.Start("http://www.steamcommunity.com/app/415200/discussions/");
  }

  private void OnFacebookButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    Process.Start("https://www.facebook.com/playmotorsport");
  }

  private void OnTwitterButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    Process.Start("https://www.twitter.com/playmotorsport");
  }

  private void OnSingleRaceButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Continue, 0.0f);
    App.instance.gameStateManager.SetState(GameState.Type.QuickRaceSetup, GameStateManager.StateChangeType.CheckForFadedScreenChange, false);
  }

  private void OnCreditsButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    UIManager.instance.ChangeScreen("CreditsScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }
}
