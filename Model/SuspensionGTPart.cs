
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SuspensionGTPart : CarPart
{
    public override CarPart.PartType GetPartType()
    {
        return CarPart.PartType.SuspensionGT;
    }
}
