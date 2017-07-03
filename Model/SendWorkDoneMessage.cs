
using FullSerializer;

[fsObject("v1", new System.Type[] { }, MemberSerialization = fsMemberSerialization.OptOut)]
internal class SendWorkDoneMessage
{
    private PartImprovement partImprovement;
    private CarPartStats.CarPartStat targetStat;


}
