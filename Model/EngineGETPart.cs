
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class EngineGETPart : CarPart
{
    public override CarPart.PartType GetPartType()
    {
        return CarPart.PartType.EngineGET;
    }

    public override string GetPartName()
    {
        return Localisation.LocaliseID("PSG_10001653", null);
    }
}
