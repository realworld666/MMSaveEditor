
public class Penalty
{
    private Penalty.PenaltyCause mCause = Penalty.PenaltyCause.Count;
    private Penalty.PenaltyState mState = Penalty.PenaltyState.PenaltyCleared;

    public virtual Penalty.PenaltyType penaltyType
    {
        get
        {
            return Penalty.PenaltyType.Count;
        }
    }

    public Penalty.PenaltyState state
    {
        get
        {
            return this.mState;
        }
    }

    public Penalty.PenaltyCause cause
    {
        get
        {
            return this.mCause;
        }
        set
        {
            this.mCause = value;
        }
    }

    public virtual void Apply(RacingVehicle inVehicle)
    {
    }

    public virtual string GetDescription()
    {
        return string.Empty;
    }

    public void SetState(Penalty.PenaltyState inState)
    {
        this.mState = inState;
    }

    public enum PenaltyType
    {
        PartPenalty,
        TimePenalty,
        PitlaneDriveThru,
        Count,
    }

    public enum PenaltyCause
    {
        [LocalisationID("PSG_10000529")] Collision,
        [LocalisationID("PSG_10000533")] JumpStart,
        Count,
    }

    public enum PenaltyState
    {
        PenaltyGiven,
        PenaltyCleared,
    }
}
