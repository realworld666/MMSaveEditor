﻿using FullSerializer;
using System;
using System.Collections.Generic;
using ModdingSystem;
using MM2;

[fsObject("v0", MemberSerialization = fsMemberSerialization.OptOut)]
public class Game
{
    public static Game instance;
    public LocalisationGroupManager localisationGroupManager;
    public Player player;
    public GameTimer time;
    public Calendar calendar;
    public EntityManager entityManager;
    public CarChassisManager chassisManager;
    public ClimateManager climateManager;
    public CircuitManager circuitManager;
    public SupplierManager supplierManager;
    public SimulationSettingsManager simulationSettingsManager;
    public TeamManager teamManager;
    public HQsBuildingManager buildingManager;
    public HQsManager headquartersManager;
    public MediaManager mediaManager;
    public SponsorManager sponsorManager;
    public InvestorManager investorManager;
    public DriverManager driverManager;
    public EngineerManager engineerManager;
    public ChampionshipManager championshipManager;
    public MessageManager messageManager;
    public NotificationManager notificationManager;
    public CelebrityManager celebrityManager;
    public MechanicManager mechanicManager;
    public ScoutManager scoutManager;
    public ChairmanManager chairmanManager;
    public AssistantManager assistantManager;
    public TeamPrincipalManager teamPrincipalManager;
    public ScoutingManager scoutingManager;
    public PartTypeSlotSettingsManager partSettingsManager;
    public TutorialSystem_v1 tutorialSystem;
    public LiveryManager liveryManager;
    public PersonalityTraitDataManager personalityTraitManager;
    public MechanicBonusManager mechanicBonusManager;
    public DilemmaSystem dilemmaSystem;
    public ChallengeManager challengeManager;
    public DriverStatsProgressionManager driverStatsProgressionManager;
    public EngineerStatsProgressionManager engineerStatsProgressionManager;
    public MechanicStatsProgressionManager mechanicStatsProgressionManager;
    public PitCrewManager pitCrewManager;
    public VehicleManager vehicleManager;
    public SessionManager sessionManager;
    public TutorialInfo tutorialInfo;
    public PersistentEventData persistentEventData;
    public GameStateInfo stateInfo;
    public Game.GameType gameType;
    private bool mQueuedAutosave;
    public List<SavedSubscribedModInfo> savedSubscribedModsInfo = new List<SavedSubscribedModInfo>();
    public GameAchievementDataStorage achievementData = new GameAchievementDataStorage();
    public SerializedPreferences mSerializedPreferences;
    public static bool IsSimulatingSeason;
    public static Action OnGameDataChanged;
    public static Action OnNewGame;

    public Game()
    {
        instance = this;

        driverStatsProgressionManager = new DriverStatsProgressionManager();
    }

    public enum GameType
    {
        Career,
        SingleEvent,
    }
}
