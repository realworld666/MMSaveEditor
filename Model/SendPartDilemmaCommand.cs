
using FullSerializer;

[fsObject("v1", new System.Type[] { }, MemberSerialization = fsMemberSerialization.OptOut)]
internal class SendPartDilemmaCommand
{
    private PreSeasonState preSeasonState;
    private CarPart.PartType partType;
    private bool isLastPart;


}
