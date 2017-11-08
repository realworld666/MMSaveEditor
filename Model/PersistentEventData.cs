using FullSerializer;
using System;
using System.Collections.Generic;
using System.Linq;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class PersistentEventData
{
    public PracticeReportSessionData practiceReportData = new PracticeReportSessionData();
    private List<SetupStintData>[] mSetupStintData = new List<SetupStintData>[Team.mainDriverCount];
    private SetupPerformance.OptimalSetup[] mOptimalSetupOutput = new SetupPerformance.OptimalSetup[0];
    private float[] mFastestLap = new float[Team.mainDriverCount];
    private SetupInput_v1[] mCurrentSetup = new SetupInput_v1[Team.mainDriverCount];
    private bool[] mPlayerHasSeenKnowledge = new bool[Team.mainDriverCount];
    private int[] mPlayerFirstTyreOptionCount = new int[Team.mainDriverCount];
    private int[] mPlayerSecondTyreOptionCount = new int[Team.mainDriverCount];
    private int[] mPlayerThirdTyreOptionCount = new int[Team.mainDriverCount];
    private List<int> mERSStartGateIds = new List<int>();
    private List<MechanicBonus.Trait[]> mActiveBonuses = new List<MechanicBonus.Trait[]>();
    private List<PracticeReportSessionData.KnowledgeType[]> mActiveKnowledge = new List<PracticeReportSessionData.KnowledgeType[]>();
    private List<EnduranceDriverSetupPreferences> mEnduranceDriverSetupPreferences;
    private PracticeReportSessionData[] mPracticeReportData;
    private bool mOptimalSetupOutputSet;
    private PersistentEventData.EventTyreData[] mEventTyreData;

    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public class EventTyreData
    {
        public TyreSet.Compound currentTyreCompound = TyreSet.Compound.Soft;
        public int currentTyreIndex;
        public int firstTyreOptionCount;
        public int secondTyreOptionCount;
        public int thirdTyreOptionCount;
        public float[] firstTyreOptionCondition;
        public float[] secondTyreOptionCondition;
        public float[] thirdTyreOptionCondition;
        public float[] wetTyreCondition;
        public float[] intermediateTyreCondition;
    }
}
