
using FullSerializer;
using System;
using System.Collections.Generic;
using System.Diagnostics;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class GameStateInfo
{
    private List<DialogRule> mRulesToIgnore = new List<DialogRule>();
    private GameState.Type mStateToLoadInto;
    private GameState.Type mStateToGoToAfterPlayerConfirms;
    private bool mIsReadyToGoToRace;
    private bool mIsReadyToSimulateRace;
    private PreSeasonState.PreSeasonStage mPreSeasonStage;
    private GameState.Type mQueuedState;
    private bool mIsLoadingFromSave;
}
