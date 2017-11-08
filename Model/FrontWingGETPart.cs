
using FullSerializer;
using UnityEngine;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class FrontWingGETPart : CarPart
{
    public override CarPart.PartType GetPartType()
    {
        return CarPart.PartType.FrontWingGET;
    }

    public override string GetPartName()
    {
        return Localisation.LocaliseID("PSG_10001651", null);
    }
}
