using FullSerializer;
using System;
using System.Collections.Generic;
using ModdingSystem;
using MM2;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class Game
{
    public List<SavedSubscribedModInfo> savedSubscribedModsInfo = new List<SavedSubscribedModInfo>();
    public GameAchievementDataStorage achievementData = new GameAchievementDataStorage();
    public static Game Instance;
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
    public VehicleManager vehicleManager;
    public SessionManager sessionManager;
    public TutorialInfo tutorialInfo;
    public PersistentEventData persistentEventData;
    public GameStateInfo stateInfo;
    public Game.GameType gameType;
    private bool mQueuedAutosave;
    public SerializedPreferences mSerializedPreferences;


    public Game()
    {
        Instance = this;
    }

    public enum GameType
    {
        Career,
        SingleEvent,
    }
}
