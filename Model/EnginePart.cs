using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class EnginePart : CarPart
{
    public override CarPart.PartType GetPartType()
    {
        return CarPart.PartType.Engine;
    }
}
