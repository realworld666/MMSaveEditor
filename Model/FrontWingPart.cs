using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class FrontWingPart : CarPart
{
    public override CarPart.PartType GetPartType()
    {
        return CarPart.PartType.FrontWing;
    }

    public override string GetPartName()
    {
        return Localisation.LocaliseID("PSG_10001651");
    }
}
