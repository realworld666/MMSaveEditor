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
    private RacingVehicle mVehicle;
    public SessionTimer.LapData lastLap;
    public int currentSector;
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

    public enum SectorStatus
    {
        NoStatus,
        Slower,
        DriverFastest,
        SessionFastest,
    }

    [fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
    public class LapData
    {
        public float[] sector;
        public float time;
        public bool isOutLap;
        public bool isInLap;
        public bool isFormationLap;
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
    }
}
