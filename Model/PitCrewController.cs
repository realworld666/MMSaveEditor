
using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class PitCrewController
{
    private Team mTeam;
    private PitCrewApplicationManager mPitCrewApplicationManager;
    private PitStopManager mPitStopManager;
    private AIPitCrew mAIPitCrew;
    private List<PitCrewMember> mPitCrewTeamMembers = new List<PitCrewMember>();
    private PitStopRaceLog[] mPitStopLogs;
    private PitCrewController.PitCrewFunding mCurrentPitCrewFunding = PitCrewController.PitCrewFunding.Medium;
    private PitCrewAutomanaging mPitCrewAutomanaging = new PitCrewAutomanaging();

    [NonSerialized]
    private List<PitCrewMember> mPitCrewListCache = new List<PitCrewMember>();

    public AIPitCrew AIPitCrew
    {
        get
        {
            return this.mAIPitCrew;
        }
    }

    public enum PitCrewFunding
    {
        Low,
        Medium,
        High,
    }
}
