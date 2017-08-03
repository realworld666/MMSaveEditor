using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class GearboxPart : CarPart
{
    public override CarPart.PartType GetPartType()
    {
        return CarPart.PartType.Gearbox;
    }
}
