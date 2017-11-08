
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class AIPitCrewTaskStat
{
    public float taskConfidence = 1f;
    public SessionSetupChangeEntry.Target taskType;
    public float taskStat;

}
