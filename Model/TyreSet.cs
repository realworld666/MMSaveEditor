
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class TyreSet
{
    public static readonly int sTreadCount = 3;
    private float mPunctureDuration = 60f;
    private float mCondition;
    private float mTemperature;
    private float mAirTempRateChange;
    private float mPunctureTimer;
    private bool mIsPunctured;
    private float mTyreHeatingChassisStatImpact;
    private RacingVehicle mVehicle;
    private TyreCompoundDesignData mCompoundDesignData;

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
    }
}
