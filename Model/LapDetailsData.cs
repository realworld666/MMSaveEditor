using FullSerializer;
using System.Collections.Generic;
using System.Runtime.InteropServices;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class LapDetailsData
{
    private TyreSet.Compound[] mCompounds = new TyreSet.Compound[SessionTimer.sectorCount];
    private int[] mStandingsPosition = new int[SessionTimer.sectorCount];
    private float[] mTyreWear = new float[SessionTimer.sectorCount];
    private float[] mTyreTemperature = new float[SessionTimer.sectorCount];
    private float[] mFuel = new float[SessionTimer.sectorCount];
    private LapDetailsData.UptimeData<ERSController.Mode>[] mERSUptime = new LapDetailsData.UptimeData<ERSController.Mode>[SessionTimer.sectorCount];
    private LapDetailsData.UptimeData<DrivingStyle.Mode>[] mDrivingStyleUptime = new LapDetailsData.UptimeData<DrivingStyle.Mode>[SessionTimer.sectorCount];
    private LapDetailsData.UptimeData<Fuel.EngineMode>[] mEngineModeUptime = new LapDetailsData.UptimeData<Fuel.EngineMode>[SessionTimer.sectorCount];
    private float[] mSetupQuality = new float[SessionTimer.sectorCount];
    private SessionSetup.Trim[] mSessionTrim = new SessionSetup.Trim[SessionTimer.sectorCount];
    private float[] mTrackWater = new float[SessionTimer.sectorCount];
    private float[] mTrackRubber = new float[SessionTimer.sectorCount];
    private SessionManager.Flag[] mFlag = new SessionManager.Flag[SessionTimer.sectorCount];
    private float[] mGapToLeader = new float[SessionTimer.sectorCount];
    private float[] mGapToCarAhead = new float[SessionTimer.sectorCount];
    private float[] mGapToCarBehind = new float[SessionTimer.sectorCount];
    private float[] mTopSpeed = new float[SessionTimer.sectorCount];
    private float[] mSectorTime = new float[SessionTimer.sectorCount];
    private LapDetailsData.LapEventsData[] mLapEvents = new LapDetailsData.LapEventsData[SessionTimer.sectorCount];
    private List<LapDetailsData.LapEvents> mLapEventsRecorded = new List<LapDetailsData.LapEvents>();
    private float mHighestTopSpeed;
    private int mSectorGates;
    private int mCurrentSector;


    public enum LapEvents
    {
        [LocalisationID("PSG_10008848")] PitStop,
        [LocalisationID("PSG_10012521")] Puncture,
        [LocalisationID("PSG_10012522")] OutOfFuel,
        [LocalisationID("PSG_10000538")] PenaltyDriveTrought,
        [LocalisationID("PSG_10010042")] PenaltyTime,
        [LocalisationID("PSG_10000527")] Collision,
        [LocalisationID("PSG_10007789")] Crashed,
        [LocalisationID("PSG_10007790")] PartFailure,
        [LocalisationID("PSG_10012523")] LockUp,
        [LocalisationID("PSG_10012524")] RunWide,
        [LocalisationID("PSG_10012525")] CutCorner,
    }

    public enum LapData
    {
        [LocalisationID("PSG_10012381")] TyreCompound,
        [LocalisationID("PSG_10004259")] TyreWear,
        [LocalisationID("PSG_10008851")] TyreTemperature,
        [LocalisationID("PSG_10005798")] Position,
        [LocalisationID("PSG_10001559")] Fuel,
        [LocalisationID("PSG_10012389")] ERSUptime,
        [LocalisationID("PSG_10012387")] DrivingStyleUptime,
        [LocalisationID("PSG_10012388")] EngineModeUptime,
        [LocalisationID("PSG_10012383")] SetupQuality,
        [LocalisationID("PSG_10012382")] SessionTrim,
        [LocalisationID("PSG_10012384")] TrackWater,
        [LocalisationID("PSG_10012385")] TrackRubber,
        [LocalisationID("PSG_10012465")] Flag,
        [LocalisationID("PSG_10007971")] GapToLeader,
        [LocalisationID("PSG_10012394")] GapToBehind,
        [LocalisationID("PSG_10012395")] GapToAhead,
        [LocalisationID("PSG_10002084")] TopSpeed,
        [LocalisationID("PSG_10008595")] LapTime,
        [LocalisationID("PSG_10012464")] LapEvents,
        [LocalisationID("PSG_10000406")] Sector1,
        [LocalisationID("PSG_10000409")] Sector2,
        [LocalisationID("PSG_10000407")] Sector3,
        Count,
    }

    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public struct UptimeData<T>
    {
        public Dictionary<T, float> data;
    }

    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public struct LapEventsData
    {
        public List<LapDetailsData.LapEvents> lapEvents;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    public struct ERSControllerEnumComparer : IEqualityComparer<ERSController.Mode>
    {
        public bool Equals(ERSController.Mode x, ERSController.Mode y)
        {
            return x == y;
        }

        public int GetHashCode(ERSController.Mode obj)
        {
            return (int)obj;
        }
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    public struct DrivingStyleEnumComparer : IEqualityComparer<DrivingStyle.Mode>
    {
        public bool Equals(DrivingStyle.Mode x, DrivingStyle.Mode y)
        {
            return x == y;
        }

        public int GetHashCode(DrivingStyle.Mode obj)
        {
            return (int)obj;
        }
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    public struct EngineModeEnumComparer : IEqualityComparer<Fuel.EngineMode>
    {
        public bool Equals(Fuel.EngineMode x, Fuel.EngineMode y)
        {
            return x == y;
        }

        public int GetHashCode(Fuel.EngineMode obj)
        {
            return (int)obj;
        }
    }
}
