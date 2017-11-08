
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class RearWingGETPart : CarPart
{
    public override CarPart.PartType GetPartType()
    {
        return CarPart.PartType.RearWingGET;
    }

    public override string GetPartName()
    {
        return Localisation.LocaliseID("PSG_10001652", null);
    }
}
