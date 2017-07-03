
using System;
using System.Collections.Generic;
using FullSerializer;

[fsObject("v1", new System.Type[] { typeof(TutorialSystem) }, MemberSerialization = fsMemberSerialization.OptOut)]
public class TutorialSystem_v1
{
    private string mCurrentTutorialName = string.Empty;
    private Map<string, List<int>> mFinishedTutorialSequences;
    private bool mTutorialActive;
    [NonSerialized]
    private UITutorialSequence mCurrentTutorialSequence;
    private UITutorialSequence.GameStateTrigger mLastTutorialSequenceStateTrigger;
    private int mLastEventNumber;
    private bool mBlockSimulationInput;
    private bool mBlockEscapeButtonInput;
    private int mCurrentTutorialSequenceID;
    private bool[] mIsTutorialSectionComplete;

    public TutorialSystem_v1(TutorialSystem inOldVersionTutorial)
    {
        throw new NotImplementedException();
    }

    public TutorialSystem_v1()
    {
        this.mFinishedTutorialSequences = new Map<string, List<int>>();
        this.mTutorialActive = false;
        this.mLastTutorialSequenceStateTrigger = UITutorialSequence.GameStateTrigger.Frontend;
        this.mLastEventNumber = -1;
        this.mBlockSimulationInput = true;
        this.mBlockEscapeButtonInput = true;
        this.mIsTutorialSectionComplete = new bool[4];
    }

    public enum TutorialSection
    {
        HasRunFirstRace,
        ScreenHotkeysCheck,
        TurnRadioMessagesOn,
        SkipTeamReportScreen,
        Count,
    }
}
