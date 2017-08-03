using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class EngineGTPart : CarPart
{
    public override CarPart.PartType GetPartType()
    {
        return CarPart.PartType.EngineGT;
    }
}
