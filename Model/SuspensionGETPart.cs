using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class SuspensionGETPart : CarPart
{
    public override CarPart.PartType GetPartType()
    {
        return CarPart.PartType.SuspensionGET;
    }

    public override string GetPartName()
    {
        return Localisation.LocaliseID("PSG_10001655", null);
    }
}
