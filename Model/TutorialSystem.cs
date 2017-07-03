using System;
using System.Collections.Generic;
using FullSerializer;

[fsObject("v0", new System.Type[] { }, MemberSerialization = fsMemberSerialization.OptOut)]
public class TutorialSystem
{
    private Map<string, List<int>> mFinishedTutorialSequences = new Map<string, List<int>>();
    private UITutorialSequence.GameStateTrigger mLastTutorialSequenceStateTrigger = UITutorialSequence.GameStateTrigger.Frontend;
    private int mLastEventNumber = -1;
    private bool mBlockSimulationInput = true;
    private bool[] mIsTutorialSectionComplete = new bool[2];
    private bool mTutorialActive;
    [NonSerialized]
    private UITutorialSequence mCurrentTutorialSequence;
    private bool mBlockEscapeButtonInput;

    public enum TutorialSection
    {
        HasRunFirstRace,
        ScreenHotkeysCheck,
        Count,
    }
}
