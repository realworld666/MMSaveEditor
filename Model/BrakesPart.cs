using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class BrakesPart : CarPart
{
    public override CarPart.PartType GetPartType()
    {
        return CarPart.PartType.Brakes;
    }

    public override string GetPartName()
    {
        return Localisation.LocaliseID("PSG_10001657");
    }
}
