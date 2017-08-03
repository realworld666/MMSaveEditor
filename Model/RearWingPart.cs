using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class RearWingPart : CarPart
{
    public override CarPart.PartType GetPartType()
    {
        return CarPart.PartType.RearWing;
    }
}
