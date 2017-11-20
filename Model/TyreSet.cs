
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class TyreSet
{
    public static readonly int sTreadCount = 3;
    private TyreSetPerformanceRange mLowPerformanceRange = new TyreSetPerformanceRange();
    private TyreSetPerformanceRange mMediumPerformanceRange = new TyreSetPerformanceRange();
    private TyreSetPerformanceRange mHighPerformanceRange = new TyreSetPerformanceRange();

    private float mCondition;
    private float mTemperature;
    private float mAirTempRateChange;
    private float mPunctureTimer;
    private float mPunctureDuration = 60f;
    private bool mIsPunctured;
    private float mTyreHeatingChassisStatImpact;
    private RacingVehicle mVehicle;
    private TyreCompoundDesignData mCompoundDesignData;
    private TyreDesignData mTyreDesignData;
    private TyreSet.Compound mWrongCompoundFitted = TyreSet.Compound.UltraSoft;
    private SessionSetupChangeEntry.TyreSlot mWrongCompoundTyreSlot = SessionSetupChangeEntry.TyreSlot.BackLeft;
    private bool mHasLooseWheel;
    private bool mHasRanWide;
    private float mLooseWheelDetachedTimer = 15f;
    private float mCurrentLooseWheelDetachedTimer;
    private SessionSetupChangeEntry.TyreSlot mTargetLooseTyreSlot = SessionSetupChangeEntry.TyreSlot.BackLeft;
    private int mLapWhenLostWheel = -1;
    private bool mHasWrongCompoundFitted;
    private bool mWheelLost;
    private bool mSendLooseWheelReminder;

    public enum Compound
    {
        [LocalisationID("PSG_10000472")] SuperSoft,
        [LocalisationID("PSG_10000473")] Soft,
        [LocalisationID("PSG_10000467")] Medium,
        [LocalisationID("PSG_10000468")] Hard,
        [LocalisationID("PSG_10000471")] Intermediate,
        [LocalisationID("PSG_10000470")] Wet,
        [LocalisationID("PSG_10007137")] UltraSoft,
    }

    public enum Tread
    {
        Slick,
        LightTread,
        HeavyTread,
        None,
    }
}
