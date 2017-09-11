using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class EnginePart : CarPart
{
    public override CarPart.PartType GetPartType()
    {
        return CarPart.PartType.Engine;
    }

    public override string GetPartName()
    {
        return Localisation.LocaliseID("PSG_10001653");
    }
}
