using FullSerializer;

public class SessionTimer
{

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
