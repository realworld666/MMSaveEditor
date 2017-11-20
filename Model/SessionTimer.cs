using FullSerializer;
using System.Collections.Generic;

public class SessionTimer
{
    public static int sectorCount = 3;
    public List<SessionTimer.LapData> lapData = new List<SessionTimer.LapData>();
    public SessionTimer.LapData currentLap = new SessionTimer.LapData();
    public float[] fastestSector = new float[SessionTimer.sectorCount];
    public List<SessionTimer.PitstopData> pitstopData = new List<SessionTimer.PitstopData>();
    public SessionTimer.PitstopData currentPitstop = new SessionTimer.PitstopData();
    private Dictionary<Driver, float> mDriversTimeDriven = new Dictionary<Driver, float>();
    private RacingVehicle mVehicle;
    public SessionTimer.LapData lastLap;
    public int currentSector = -1;
    public int lapOfFastestTime;
    public TyreSet fastestLapTyre;
    public SessionTimer.LapData fastestLap;
    public bool wasLastLapAnOutLap;
    public float fastestPitStop;
    public float sessionDistanceTraveled;
    public float sessionTimePenalty;
    public float sessionTime;
    public float pace;
    public int lastActiveGateID;
    public bool hasSeenChequeredFlag;
    public bool hasCrossedStartLine;
    private float mGapToLeader;
    private float mGapToAhead;
    private float mGapToBehind;
    public GateInfo lastGatePassed;
    public float[] slowestSector = new float[SessionTimer.sectorCount];
    private List<SessionTimer.LapData> mLapDataCache = new List<SessionTimer.LapData>();
    private int mNextGateID = int.MinValue;
    private int mLapsOnAttackMode;
    private int mLapCurrentPointer;

    public enum SectorStatus
    {
        NoStatus,
        Slower,
        DriverFastest,
        SessionFastest,
    }

    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public class LapData
    {
        public float[] sector = new float[SessionTimer.sectorCount];
        public float time;
        public bool isOutLap;
        public bool isInLap;
        public bool isFormationLap;
        public LapDetailsData lapDetailsData = new LapDetailsData();
        public Driver driver;
    }

    public class PitstopData
    {
        public int lapNumber;
        public int entrancePosition;
        public int exitPosition;
        public float estimatedPitStopTime;
        public float pitlaneTime;
        public float stopTime;
        public bool isChangingTyres;
        public SessionStrategy.PitStrategy strategy = SessionStrategy.PitStrategy.Balanced;
        public List<SessionTimer.PitstopData.SetupChange> setupChanges = new List<SessionTimer.PitstopData.SetupChange>();
        public float timeWaitingForOtherDriver;
        public float mistakeTime;
        public float strategyTime;
        public Driver driver;

        public struct SetupChange
        {
            public SessionSetupChangeEntry.Target target;
            public float time;
            public bool hadMistake;
        }
    }
}
