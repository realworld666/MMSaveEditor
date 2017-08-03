using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SuspensionPart : CarPart
{
    public override CarPart.PartType GetPartType()
    {
        return CarPart.PartType.Suspension;
    }
}
