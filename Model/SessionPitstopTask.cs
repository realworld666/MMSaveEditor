
using FullSerializer;
using System.Collections.Generic;
using System.Text;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SessionPitstopTask : InstanceCounter
{
    private SessionSetupChangeEntry.Target mTaskType = SessionSetupChangeEntry.Target.Count;
    private SessionSetupChangeEntry.TyreSlot mTyreSlot = SessionSetupChangeEntry.TyreSlot.BackLeft;
    private CarPart.PartType mCarPartType = CarPart.PartType.None;
    private List<SessionPitstopMistake> mPitStopMistakes = new List<SessionPitstopMistake>();
    private SessionPitstop.Outcome mOutcome;
    private float mExpectedStartTaskTime;
    private float mStartTaskTime;
    private float mTaskTime;
    private float mVarianceTime;
    private float mPitStrategyImpact;
    private SessionPitstop mSessionPitStop;
    private List<PitCrewMember> mPitCrewMembersForTask = new List<PitCrewMember>();
    private AIPitCrewTaskStat mAIPitstopTaskStat;
    private RacingVehicle mVehicle;
    private SetupDesignData mSetupDesignData;



    public class TaskComparer : IEqualityComparer<SessionSetupChangeEntry.Target>
    {
        public bool Equals(SessionSetupChangeEntry.Target x, SessionSetupChangeEntry.Target y)
        {
            return x == y;
        }

        public int GetHashCode(SessionSetupChangeEntry.Target codeh)
        {
            return (int)codeh;
        }
    }
}
