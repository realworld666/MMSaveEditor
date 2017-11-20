
using System.Collections.Generic;

public class SessionPenalty
{
    public List<Penalty> penalties = new List<Penalty>();
    private Penalty mCurrentPenalty;
    private SessionPenalty.State mState;
    private float mTimer;
    private RacingVehicle mVehicle;

    public enum State
    {
        None,
        UnderInvestigation,
        Cleared,
        Underway,
    }
}
