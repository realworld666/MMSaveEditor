// Decompiled with JetBrains decompiler
// Type: App
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using MM2;
using ModdingSystem;
using System;
using UnityEngine;
using UnityEngine.CrashLog;

public class App : MonoBehaviour
{
  public static App instance;
  public Database database;
  public UITeamColorManager uiTeamColourManager;
  public BaseSceneManager baseSceneManager;
  public AudioListenerManager audioListenerManager;
  public GameCameraManager gameCameraManager;
  public WidgetManager widgetManager;
  public ErrorReporter errorReporter;
  public AssetManager assetManager;
  public AtlasManager atlasManager;
  public PreferencesManager preferencesManager;
  public RegenManager regenManager;
  public ModManager modManager;
  public NationalityManager nationalityManager;
  public CameraManager cameraManager;
  public GameStateManager gameStateManager;
  public InputManager inputManager;
  public DilemmaManager dilemmaManager;
  public FrontendCarManager frontendCarManager;
  public SaveSystem saveSystem;
  public TutorialSimulation tutorialSimulation;
  public Achievements achievements;
  public SteamAchievementsManager steamAchievementsManager;
  public CrossGameAchievementDataStorage crossGameAchievementData;
  public DLCManager dlcManager;
  public AssetBundleManager assetBundleManager;
  private DialogRulesManager mDialogRulesManager;
  private SpecialStringsGenderTable mGenderTable;
  private SessionAIOrdersDatabase mSessionAIOrdersDatabase;
  private CarPartComponentsManager mComponentsManager;
  private TeamColorManager mTeamColorManager;
  private CarPartModelDatabase mCarPartModelDatabase;
  private VotesManager mVotesManager;

  public DialogRulesManager dialogRulesManager
  {
    get
    {
      return this.mDialogRulesManager;
    }
  }

  public SpecialStringsGenderTable genderTable
  {
    get
    {
      return this.mGenderTable;
    }
  }

  public SessionAIOrdersDatabase sessionAIOrdersDatabase
  {
    get
    {
      return this.mSessionAIOrdersDatabase;
    }
  }

  public CarPartComponentsManager componentsManager
  {
    get
    {
      return this.mComponentsManager;
    }
  }

  public TeamColorManager teamColorManager
  {
    get
    {
      return this.mTeamColorManager;
    }
  }

  public CarPartModelDatabase carPartModelDatabase
  {
    get
    {
      return this.mCarPartModelDatabase;
    }
  }

  public VotesManager votesManager
  {
    get
    {
      return this.mVotesManager;
    }
  }

  private void Awake()
  {
    App.instance = this;
    Profiler.maxNumberOfSamplesPerFrame = -1;
    Application.runInBackground = false;
    if (!Environment.CommandLine.ToLower().Contains("-noreport"))
      CrashReporting.Init("2a1e4e19-a299-406e-933b-9e0fc6a24647", GameVersionNumber.version.ToString(), SystemInfo.deviceUniqueIdentifier);
    GameUtility.assertMode = !Application.isEditor ? GameUtility.AssertMode.ThrowException : GameUtility.AssertMode.LogError;
    DesignDataManager.LoadSingleSeaterDesignData();
    scSoundManager.Create();
    this.inputManager = new InputManager();
    this.uiTeamColourManager = new UITeamColorManager();
    this.audioListenerManager = new AudioListenerManager();
    this.baseSceneManager = new BaseSceneManager();
    this.gameCameraManager = new GameCameraManager();
    this.widgetManager = new WidgetManager();
    UIManager.EnsureInstanceExists();
    this.modManager = new ModManager();
    this.modManager.Start();
    this.assetManager = new AssetManager(this.modManager);
    this.atlasManager = new AtlasManager();
    this.mGenderTable = new SpecialStringsGenderTable();
    this.mGenderTable.LoadGenderTableDatabase(this.assetManager);
    this.mSessionAIOrdersDatabase = new SessionAIOrdersDatabase();
    this.mSessionAIOrdersDatabase.LoadAIOrdersData(this.assetManager);
    this.mComponentsManager = new CarPartComponentsManager();
    this.mTeamColorManager = new TeamColorManager();
    this.nationalityManager = new NationalityManager();
    this.nationalityManager.Start();
    this.dilemmaManager = new DilemmaManager();
    this.dilemmaManager.Start();
    this.frontendCarManager = new FrontendCarManager();
    this.saveSystem = new SaveSystem();
    this.tutorialSimulation = new TutorialSimulation();
    this.achievements = new Achievements();
    this.steamAchievementsManager = new SteamAchievementsManager(this.achievements);
    this.crossGameAchievementData = new CrossGameAchievementDataStorage();
    this.assetBundleManager = new AssetBundleManager();
    this.assetBundleManager.Start();
    this.dlcManager = new DLCManager();
    this.dlcManager.Start();
    ConsoleCommands.RegisterAllCommands();
    SteamAchievementsDebug.RegisterConsoleCommands();
  }

  private void Start()
  {
    Localisation.Start();
    this.preferencesManager = new PreferencesManager();
    this.preferencesManager.OnStart();
    SceneManager.EnsureInstanceExists();
    this.cameraManager = new CameraManager();
    this.cameraManager.Start();
    this.gameStateManager = new GameStateManager();
    this.gameStateManager.Start();
  }

  public void LoadGameDatabases()
  {
    this.database = new Database(this.assetManager, this.modManager);
    this.database.LoadGameDatabase();
    this.regenManager = new RegenManager();
    this.regenManager.Start();
    DialogRulesDatabase dialogRulesDatabase = new DialogRulesDatabase();
    dialogRulesDatabase.LoadGameDialogRules(this.assetManager);
    this.mDialogRulesManager = new DialogRulesManager();
    this.mDialogRulesManager.CreateDictionary(dialogRulesDatabase.rulesList);
    this.mCarPartModelDatabase = new CarPartModelDatabase(this.database, false);
    this.mVotesManager = new VotesManager();
    this.mVotesManager.LoadVotesFromDatabase(this.database);
  }

  public void LoadCarPartComponentsAndTeamColors()
  {
    this.mComponentsManager.LoadComponentsFromDatabase(this.database);
    this.mTeamColorManager.LoadColorsFromDatabase(this.database);
  }

  public void Update()
  {
    this.saveSystem.Update();
    if (this.saveSystem.status == SaveSystem.Status.Loading || this.saveSystem.status == SaveSystem.Status.Saving)
      return;
    this.gameStateManager.Update();
  }

  private void OnApplicationFocus(bool inFocusStatus)
  {
    if (this.preferencesManager == null || !this.preferencesManager.GetSettingBool(Preference.pName.Game_PauseGameOnLosingFocus, false))
      return;
    this.OnApplicationPause(!inFocusStatus);
  }

  private void OnApplicationPause(bool inPauseStatus)
  {
    if (!Game.IsActive() || !inPauseStatus)
      ;
  }

  private void OnApplicationQuit()
  {
    if (this.saveSystem != null && (this.saveSystem.status == SaveSystem.Status.Loading || this.saveSystem.status == SaveSystem.Status.Saving))
    {
      Application.CancelQuit();
    }
    else
    {
      if (this.preferencesManager == null)
        return;
      this.preferencesManager.OnApplicationQuit();
    }
  }
}
