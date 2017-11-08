
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class GearboxGETPart : CarPart
{
    public override CarPart.PartType GetPartType()
    {
        return CarPart.PartType.GearboxGET;
    }

    public override string GetPartName()
    {
        return Localisation.LocaliseID("PSG_10001654", null);
    }
}
