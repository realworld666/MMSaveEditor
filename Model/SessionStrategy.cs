
using System.Collections.Generic;

public class SessionStrategy
{
    private RacingVehicle mVehicle;
    private SessionStrategy.Status mStatus;
    private SessionStrategy.Status mQueuedStatus;
    private SessionStrategy.Status mPreviousStatus;
    private SessionStrategy.PitStrategy mPitStrategy = SessionStrategy.PitStrategy.Balanced;
    private SessionStrategy.TeamOrders mTeamOrders;
    private SessionStrategy.StintStrategyType mStintStrategyType;
    private TyreSet[] mFirstTyreOption;
    private TyreSet[] mSecondTyreOption;
    private TyreSet[] mThirdTyreOption;
    private TyreSet[] mIntermediates;
    private TyreSet[] mWets;
    private TyreSet mLockedTyres;
    private List<SessionStrategy.PitOrder> mPitOrders = new List<SessionStrategy.PitOrder>();
    private List<TyreSet.Compound> mTyreHistory = new List<TyreSet.Compound>();
    private Championship mChampionship;
    private bool mRefreshCarCrashData;
    private bool mUsedTeamOrders;
    private TyreStrategyOption[] mTyreStrategyOption;
    private int mOrderedLapCount;
    private int mPitlaneDriveTrough;
    private SessionStints.ReasonForStint mReasonForPreviousPit = SessionStints.ReasonForStint.None;
    private const float lapDistanceMultiplier = 0.6666667f;
    private bool mUsesAIForStrategy;
    private TargetPointSteeringBehaviour mTargetPointSteeringBehaviour;

    public enum Status
    {
        NoActionRequired,
        Pitting,
        ReturningToGarage,
        WaitingForSetupCompletion,
        PitThruPenalty,
        Crashing,
    }

    public enum PitStrategy
    {
        [LocalisationID("PSG_10005763")] Safe,
        [LocalisationID("PSG_10005764")] Balanced,
        [LocalisationID("PSG_10005765")] Fast,
    }

    public enum TyreOption
    {
        First,
        Second,
        Third,
        Intermediates,
        Wets,
    }

    public enum PitOrder
    {
        Tyres,
        Refuel,
        FixPart,
        Recharge,
        CancelPit,
    }

    public enum TeamOrders
    {
        Race,
        AllowTeamMateThrough,
    }

    public enum StintStrategyType
    {
        Normal,
        Conservative,
    }
}
