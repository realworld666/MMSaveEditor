using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class RearWingGTPart : CarPart
{
    public override CarPart.PartType GetPartType()
    {
        return CarPart.PartType.RearWingGT;
    }

    public override string GetPartName()
    {
        return Localisation.LocaliseID("PSG_10001652");
    }
}
