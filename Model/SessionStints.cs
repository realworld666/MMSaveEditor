
using System.Collections.Generic;

public class SessionStints
{
    private List<SessionStints.SessionStintData> mStints = new List<SessionStints.SessionStintData>();
    private RacingVehicle mVehicle;
    public enum ReasonForStint
    {
        RaceStart,
        PitTyreThreadChange,
        PitTyreChange,
        PitConditionFix,
        PitRefuel,
        RollingStart,
        DriverSwap,
        None,
    }

    public class SessionStintData
    {
        public SessionStints.ReasonForStint reasonForStint = SessionStints.ReasonForStint.None;
        public SessionDetails.SessionType sessionType = SessionDetails.SessionType.Race;
        public TyreSet.Compound tyreCompound = TyreSet.Compound.Soft;
        public int lapCount;
        public float duration;
        public bool changedTyres;
    }
}
