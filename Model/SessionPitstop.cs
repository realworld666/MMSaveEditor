
using FullSerializer;
using System.Collections.Generic;
using System.Text;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SessionPitstop : InstanceCounter
{
    private RacingVehicle mVehicle;
    private Dictionary<SessionSetupChangeEntry.Target, List<SessionPitstopTask>> mPitStopTasks = new Dictionary<SessionSetupChangeEntry.Target, List<SessionPitstopTask>>();
    private SetupDetails mCurrentSetup = new SetupDetails();
    private SetupDetails mTargetSetup = new SetupDetails();
    private List<SessionSetupRepairPart> mRepairParts = new List<SessionSetupRepairPart>();
    private Dictionary<SessionSetupChangeEntry.Target, SessionPitstop.Outcome> mOutcomes = new Dictionary<SessionSetupChangeEntry.Target, SessionPitstop.Outcome>((IEqualityComparer<SessionSetupChangeEntry.Target>)new SessionPitstopTask.TaskComparer());
    private bool mQueuedCarBehindTeamMate;
    private float mBatteryChargeRisk;
    private bool mIsPitStopCatastrophic;
    private PitCrewController mPitCrewController;
    private ChampionshipRules.PitStopCrewSize mPitCrewSize = ChampionshipRules.PitStopCrewSize.Large;
    private SetupDesignData mSetupDesignData;
    private PitCrewDesignData mPitCrewDesignData;
    private float mCatatrophicFireStartTime;
    private float mCatatrophicFireEndTime;
    private float mLastSetupTimer;
    private bool mFixingLooseTyre;

    private PitStopOutcomesHelper mOutcomeHelper = new PitStopOutcomesHelper();
    private PitstopTimesHelper mTimesHelper = new PitstopTimesHelper();
    private PitstopTasksManagingHelper mTasksManagingHelper = new PitstopTasksManagingHelper();



    public enum Outcome
    {
        [LocalisationID("PSG_10009318")] None,
        [LocalisationID("PSG_10010811")] Bad,
        [LocalisationID("PSG_10010209")] Good,
        [LocalisationID("PSG_10010180")] Great,
    }
}
