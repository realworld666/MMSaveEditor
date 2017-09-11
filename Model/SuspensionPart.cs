using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SuspensionPart : CarPart
{
    public override CarPart.PartType GetPartType()
    {
        return CarPart.PartType.Suspension;
    }

    public override string GetPartName()
    {
        return Localisation.LocaliseID("PSG_10001655");
    }
}
