using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class GearboxPart : CarPart
{
    public override CarPart.PartType GetPartType()
    {
        return CarPart.PartType.Gearbox;
    }

    public override string GetPartName()
    {
        return Localisation.LocaliseID("PSG_10001654");
    }
}
