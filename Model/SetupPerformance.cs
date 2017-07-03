using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SetupPerformance : PerformanceImpact
{
    private SessionSetup.SetupOutput mSetupOutput = new SessionSetup.SetupOutput();
    private SetupPerformance.OptimalSetup mOptimalSetup = new SetupPerformance.OptimalSetup();
    private SetupPerformance.SetupArea mSetupAreaToFocusOn;
    private int mRelevantPartCount;



    public enum SetupArea
    {
        Cornering,
        Straights,
        Acceleration,
        TopSpeed,
        Understeer,
        Oversteer,
    }

    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public class OptimalSetup
    {
        public SessionSetup.SetupOutput setupOutput = new SessionSetup.SetupOutput();
        public float[] minVisualRangeOffset = new float[3];
        public float[] maxVisualRangeOffset = new float[3];
        public float[] visualRangeOffsetMultiplier = new float[3];
    }
}
