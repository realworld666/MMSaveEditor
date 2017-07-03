using FullSerializer;

namespace MM2
{
    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public class SerializedPreferences
    {
        private bool mAutoSave;
        private bool mRollSaves;
        private bool mToolTips;
        private bool mAutoERS;
        private bool mAutoOutlap;
        private int mNumRollingSaves;
        private PrefGameRaceLength.Type mGameRaceLength;
        private PrefGameAIDevDifficulty.Type mGameAIDevDifficulty;
    }
}
