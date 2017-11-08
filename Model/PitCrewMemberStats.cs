

public class PitCrewMemberStats : PersonStats
{
    private float[] mPitStats = new float[5];
    private float mConfidence = 1f;
    private float mMaxConfidence = 1f;
    public const float MAX_STAT_VALUE = 20f;
    public const float MED_STAT_VALUE = 10f;
    public const float MAX_TOTAL_STAT_POOL = 100f;
    private PitCrewMember mOwner;


    public enum PitCrewStatType
    {
        [LocalisationID("PSG_10013429")] Tyres,
        [LocalisationID("PSG_10013430")] FrontJack,
        [LocalisationID("PSG_10013431")] RearJack,
        [LocalisationID("PSG_10013432")] FixingParts,
        [LocalisationID("PSG_10013433")] Refuelling,
        Count,
    }
}
