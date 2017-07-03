
using FullSerializer;

[fsObject("v1", new System.Type[] { }, MemberSerialization = fsMemberSerialization.OptOut)]
internal class ImprovePartsCommand
{
    private PartImprovement partImprovement;
    private Championship championship;
    private CarPartStats.CarPartStat targetStat;

}
