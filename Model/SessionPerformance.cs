
using FullSerializer;
using System;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
[Serializable]
public class SessionPerformance
{
    private static CarStats mMaxChampionshipCarStats = new CarStats();
    private static CarStats mMinChampionshipCarStats = new CarStats();
    private ERSPerformance mERSPerformance = new ERSPerformance();
    private CarConditionPerformance mCarConditionPerformance = new CarConditionPerformance();
    private CriticalCarPartPerformance mCriticalCarPartPerformance = new CriticalCarPartPerformance();
    private DriverSessionForm mDriverForm = new DriverSessionForm();
    private DriverPerformance mDriverPerformance = new DriverPerformance();
    private DrivingStyle mDrivingStyle = new DrivingStyle();
    private Fuel mFuel = new Fuel();
    private TemperatureOptimisation mTemperatureOptimisation = new TemperatureOptimisation();
    private SessionBonuses mSessionBonuses = new SessionBonuses();
    private SetupPerformance mSetupPerformance = new SetupPerformance();
    private WeatherImpact mWeatherImpact = new WeatherImpact();
    private TyrePerformance mTyrePerformance = new TyrePerformance();
    private TrackExpertPerformance mTrackExpertPerformance = new TrackExpertPerformance();
    private DriverStaminaPerformance mDriverStaminaPerformance = new DriverStaminaPerformance();
    private PerformanceImpact[] mPerformanceImpacts = new PerformanceImpact[14];
    private Vehicle mVehicle;
    private RacingVehicle mRacingVehicle;
    private SafetyVehicle mSafetyVehicle;
    private CarStats mCarStats = new CarStats();
    private CarStats mRacingPerformance = new CarStats();
    private CarStats mRaceStartPerformance = new CarStats();
    private CarStats mNeutralPerformance = new CarStats();
    private CarStats mCurrentPerformance = new CarStats();
    private CarStats mImpactPerformance = new CarStats();
    private const float timeCostLerp = 0.0f;
    private SessionPerformance.PerformanceState mPerformanceState;
    private SessionPerformance.RaceStartType mRaceStartType;
    private float mEstimatedBestLapTime;
    private float mTimeSinceLastUpdate;
    private bool mUpdateDriverStatsOnEnterGate;
    private bool mUpdateDriverStatsOnNewLap;
    private bool mUpdateDriverStatsOnNewSetup;
    private bool mIsExperiencingCriticalIssue;
    private bool mRefreshCarStats;
    private float mTimeCost;

    private float mChampionshipTierTimeCost;

    public enum PerformanceState
    {
        Racing,
        RaceStart,
        Neutral,
    }

    public enum RaceStartType
    {
        Average,
        Great,
        Poor,
    }

    public enum Impacts
    {
        CarCondition,
        CriticalParts,
        DriverForm,
        DriverPerformance,
        DrivingStyle,
        Fuel,
        Bonuses,
        Setup,
        TemperatureOptimisation,
        Tyres,
        Weather,
        TrackExpert,
        ERS,
        Count,
    }
}
