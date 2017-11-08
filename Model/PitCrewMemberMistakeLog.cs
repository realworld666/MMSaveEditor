using FullSerializer;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class PitCrewMemberMistakeLog
{
    private SessionSetupChangeEntry.Target mTaskType = SessionSetupChangeEntry.Target.Count;
    private List<SessionPitstopMistake.MistakeType> mMistakeTypes = new List<SessionPitstopMistake.MistakeType>();


}
