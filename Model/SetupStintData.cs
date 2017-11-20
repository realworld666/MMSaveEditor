public class SetupStintData
{
    private SetupInput_v1 mSetupInput = new SetupInput_v1();
    private SessionSetup.SetupOutput mSetupOutput = new SessionSetup.SetupOutput();
    private TyreSet.Compound mTyreCompound = TyreSet.Compound.Soft;
    private float mTyreCondition;
    private int mLapCount;
    private float mTotalTime;
    private float mAverageLapTime;
    private float mAccumulativeTrackRubber;
    private float mAverageTrackRubber;
    private float mTrackWater;
    private SessionSetup.SetupOpinion mAerodynamicsOpinion;
    private SessionSetup.SetupOpinion mSpeedBalanceOpinion;
    private SessionSetup.SetupOpinion mHandlingOpinion;
    private float mDeltaAerodynamics;
    private float mDeltaSpeedBalance;
    private float mDeltaHandling;
    private float mRollingSetupKnowledge;
    private PracticeKnowledge.SetupKnowledgeLevel mSetupKnowledgeLevel;
    private float mSetupKnowledgeNormalised;
    private SessionDetails.SessionType mSessionType;
}
