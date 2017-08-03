using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class RearWingGTPart : CarPart
{
    public override CarPart.PartType GetPartType()
    {
        return CarPart.PartType.RearWingGT;
    }
}
