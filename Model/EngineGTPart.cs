using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class EngineGTPart : CarPart
{
    public override CarPart.PartType GetPartType()
    {
        return CarPart.PartType.EngineGT;
    }

    public override string GetPartName()
    {
        return Localisation.LocaliseID("PSG_10001653");
    }
}
