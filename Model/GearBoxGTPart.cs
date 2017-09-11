using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class GearBoxGTPart : CarPart
{
    public override CarPart.PartType GetPartType()
    {
        return CarPart.PartType.GearboxGT;
    }

    public override string GetPartName()
    {
        return Localisation.LocaliseID("PSG_10001654");
    }
}
