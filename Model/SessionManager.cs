
using System;
using System.Collections.Generic;
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SessionManager
{
    public CommentaryManager commentaryManager = new CommentaryManager();
    public RaceDirector raceDirector = new RaceDirector();
    public SessionWeatherDetails currentSessionWeather;
    public SimulationUtility simulationUtility = new SimulationUtility();
    public TeamRadioManager teamRadioManager = new TeamRadioManager();
    private List<Championship> mChampionships;
    private Championship mChampionship;
    private RaceEventDetails mEventDetails;
    private RaceEventDetails mPlayerChampEventDetails;
    private SessionManager.EndCondition mEndCondition;
    private SessionManager.Flag mFlag;
    private SessionManager.Flag mPreviousFlag;
    private int mYellowFlagSector;
    private int mLap;
    private int mLapCount;
    private float mTime;
    private float mDuration;
    private float mFastestPitStop;
    private List<RacingVehicle> mStandings = new List<RacingVehicle>();
    private SessionFastestLapData mSessionFastestLapData;
    private float[] mFastestSector;
    private PrefGameRaceLength.Type mPrefSessionLength = PrefGameRaceLength.Type.Medium;
    public Action OnFastestLapChanged;
    public Action OnLeaderLapEnd;
    public Action FlagChanged;
    public Action OnSessionStart;
    public Action OnSessionEnd;
    private int mLapsRemainingCached;
    private float mEstimatedLapTime;
    private float mPreviousTime;
    private bool mIsRollingOut;



    private RacingVehicle mFastestLapVehicle;
    private SessionTimer.LapData mFastestLap;
    private Weather mPreviousWeather;
    private Weather mForecastWeather;
    private Weather mCurrentWeather;
    private int mForecastMinutes;
    private bool mHasSessionEnded;
    private GateInfo[] mGateInfo;
    private GateInfo mStartFinishGate;
    private bool mIsSkippingSession;

    public SessionDetails.SessionType sessionType
    {
        get
        {
            if (this.mEventDetails == null)
                return SessionDetails.SessionType.Practice;
            return this.mEventDetails.currentSession.sessionType;
        }
    }

    public enum Flag
    {
        None,
        [LocalisationID("PSG_10000398")]
        Green,
        [LocalisationID("PSG_10000399")]
        Yellow,
        [LocalisationID("PSG_10000402")]
        Chequered,
        [LocalisationID("PSG_10002927")]
        SafetyCar,
        [LocalisationID("PSG_10008865")]
        VirtualSafetyCar,
    }

    public enum EndCondition
    {
        Time,
        LapCount,
    }

}
