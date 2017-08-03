using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class GearBoxGTPart : CarPart
{
    public override CarPart.PartType GetPartType()
    {
        return CarPart.PartType.GearboxGT;
    }
}
