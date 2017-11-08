
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class BrakesGETPart : CarPart
{
    public override CarPart.PartType GetPartType()
    {
        return CarPart.PartType.BrakesGET;
    }

    public override string GetPartName()
    {
        return Localisation.LocaliseID("PSG_10001657", null);
    }
}
