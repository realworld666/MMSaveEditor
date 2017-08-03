using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class BrakesPart : CarPart
{
    public override CarPart.PartType GetPartType()
    {
        return CarPart.PartType.Brakes;
    }
}
