
using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class PitstopTasksManagingHelper
{
    private int mTasksPerformed;
    private float mTotalPitstopMistake;
    private List<SessionPitstopMistake> mPitStopMistakes = new List<SessionPitstopMistake>();
    private List<SessionSetupChangeEntry.Target> mJobSpecificMistakesHappenedSoFar = new List<SessionSetupChangeEntry.Target>();
    [NonSerialized]
    private RacingVehicle mVehicle;
    [NonSerialized]
    private PitCrewController mPitCrewController;
    [NonSerialized]
    private SessionPitstop mSessionPitstop;
}
