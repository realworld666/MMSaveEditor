
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class BrakesGTPart : CarPart
{
    public override CarPart.PartType GetPartType()
    {
        return CarPart.PartType.BrakesGT;
    }

    public override string GetPartName()
    {
        return Localisation.LocaliseID("PSG_10001657");
    }
}
