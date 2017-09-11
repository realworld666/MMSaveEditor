using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class RearWingPart : CarPart
{
    public override CarPart.PartType GetPartType()
    {
        return CarPart.PartType.RearWing;
    }

    public override string GetPartName()
    {
        return Localisation.LocaliseID("PSG_10001652");
    }
}
