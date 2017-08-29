using System.Collections.Generic;
using ModdingSystem;

public class Database
{
    public List<DatabaseEntry> nationalityData;
    public List<DatabaseEntry> playerDefaultsData;
    public List<DatabaseEntry> createTeamDefaultsData;
    public List<DatabaseEntry> createTeamDriversData;
    public List<DatabaseEntry> challengesData;
    public List<DatabaseEntry> challengeRewardsData;
    public List<DatabaseEntry> liveryData;
    public List<DatabaseEntry> carPartModelsData;
    public List<DatabaseEntry> locationData;
    public List<DatabaseEntry> votesData;
    public List<DatabaseEntry> clauseData;
    public List<DatabaseEntry> celebritiesData;
    public List<DatabaseEntry> frontEndDialogData;
    public List<DatabaseEntry> mediaStoriesDialogData;
    public List<DatabaseEntry> mediaTweetsDialogData;
    public List<DatabaseEntry> mediaInterviewsDialogData;
    public List<DatabaseEntry> messagesDialogData;
    public List<DatabaseEntry> raceEventDialogData;
    public List<DatabaseEntry> preRaceTalkDialogData;
    public List<DatabaseEntry> simulationDialogData;
    public List<DatabaseEntry> teamRadioDialogData;
    public List<DatabaseEntry> dilemmasDialogData;
    public List<DatabaseEntry> tutorialsDialogData;
    public List<DatabaseEntry> specialStringTableData;
    public List<DatabaseEntry> investorData;
    public List<DatabaseEntry> engineerSessionSetupsData;
    public List<DatabaseEntry> teamExpectationWeightings;
    public List<DatabaseEntry> personExpectationWeightings;
    public List<DatabaseEntry> mechanicBonuses;
    public List<DatabaseEntry> teamAIWeightings;
    public List<DatabaseEntry> sessionAIOrders;
    private List<DatabaseEntry> mTeamData;
    private List<DatabaseEntry> mDriverData;
    private List<DatabaseEntry> mSponsorData;
    private List<DatabaseEntry> mChampionshipData;
    private List<DatabaseEntry> mEngineersData;
    private List<DatabaseEntry> mMechanicsData;
    private List<DatabaseEntry> mAssistantsData;
    private List<DatabaseEntry> mChairmanData;
    private List<DatabaseEntry> mClimateData;
    private List<DatabaseEntry> mJournalistsData;
    private List<DatabaseEntry> mMediaOutletData;
    private List<DatabaseEntry> mPoliticsPresidentsData;
    private List<DatabaseEntry> mScoutsData;
    private List<DatabaseEntry> mTeamPrincipalsData;
    private List<DatabaseEntry> mSimulationSettingsData;
    private List<DatabaseEntry> mTeamColourData;
    private List<DatabaseEntry> mCarPartsData;
    private List<DatabaseEntry> mPartsSettingsData;
    private List<DatabaseEntry> mSuppliersData;
    private List<DatabaseEntry> mCarChassisData;
    private List<DatabaseEntry> mHeadquartersData;
    private List<DatabaseEntry> mPersonalityTraitsData;
    private List<DatabaseEntry> mPartComponentsData;
    private List<DatabaseEntry> mDriverStatProgressionData;
    private List<DatabaseEntry> mEngineerStatProgressionData;
    private List<DatabaseEntry> mMechanicStatProgressionData;
    private List<DatabaseEntry> mBuildingData;
    private List<DatabaseEntry> mDriverModData;
    private List<DatabaseEntry> mMechanicModData;
    private List<DatabaseEntry> mEngineerModData;
    private List<DatabaseEntry> mTeamModData;
    private List<DatabaseEntry> mSponsorModData;
    private List<DatabaseEntry> mChampionshipModData;
    private List<DatabaseEntry> mAssistantsModData;
    private List<DatabaseEntry> mChairmanModData;
    private List<DatabaseEntry> mClimateModData;
    private List<DatabaseEntry> mJournalistsModData;
    private List<DatabaseEntry> mMediaOutletModData;
    private List<DatabaseEntry> mPoliticsPresidentsModData;
    private List<DatabaseEntry> mScoutsModData;
    private List<DatabaseEntry> mTeamPrincipalsModData;
    private List<DatabaseEntry> mSimulationSettingsModData;
    private List<DatabaseEntry> mTeamColoursModData;
    private List<DatabaseEntry> mDefaultPartsModData;
    private List<DatabaseEntry> mPartsModData;
    private List<DatabaseEntry> mPartSuppliersModData;
    private List<DatabaseEntry> mChassisModData;
    private List<DatabaseEntry> mHQModData;
    private List<DatabaseEntry> mPersonalityTraitsModData;
    private List<DatabaseEntry> mPartComponentsModData;
    private List<DatabaseEntry> mDriverStatsProgressionModData;
    private List<DatabaseEntry> mEngineerStatsProgressionModData;
    private List<DatabaseEntry> mMechanicStatsProgressionModData;
    private List<DatabaseEntry> mBuildingsModData;
    private AssetManager mAssetManager;
    private ModManager mModManager;

    public enum DatabaseType
    {
        Challenges,
        Teams,
        TeamColours,
        Liveries,
        CarParts,
        CarPartModels,
        CarChassis,
        Championships,
        Locations,
        Climate,
        Buildings,
        Votes,
        Headquarters,
        Sponsors,
        Clauses,
        Suppliers,
        PartsSettings,
        PartComponents,
        MediaOutlets,
        PersonalityTraits,
        SimulationSettings,
        Investors,
        FrontEnd,
        MediaStories,
        MediaTweets,
        MediaInterviews,
        MessageDialogs,
        RaceEventDialogs,
        PreRaceTalkDialogs,
        SimulationDialogs,
        TeamRadioDialogs,
        DilemmaDialogs,
        TutorialDialogs,
        SpecialStringTable,
        CreateTeamDefaults,
        CreateTeamDrivers,
        PlayerDefaults,
        Drivers,
        TeamAssistants,
        Journalists,
        Mechanics,
        Scouts,
        Engineers,
        Chairman,
        TeamPrincipals,
        Celebrities,
        PoliticsPresidents,
        DriverStatsProgression,
        EngineerStatsProgression,
        MechanicStatsProgression,
        EngineerSessionSetups,
        TeamExpectationWeightings,
        PersonExpectationWeightings,
        MechanicBonuses,
        TeamAIWeightings,
        Nationality,
        SessionAIOrders,
    }
}
