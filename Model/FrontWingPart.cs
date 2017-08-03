using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class FrontWingPart : CarPart
{
    public override CarPart.PartType GetPartType()
    {
        return CarPart.PartType.FrontWing;
    }
}
