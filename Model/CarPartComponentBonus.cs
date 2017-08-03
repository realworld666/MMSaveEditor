using FullSerializer;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class CarPartComponentBonus
{
    public string name = string.Empty;
    public float bonusValue;
    public CarPartComponent component;

    public virtual void ApplyBonus(CarPartDesign inDesign, CarPart inPart)
    {
    }

    public virtual void OnPartBuilt(CarPartDesign inDesign, CarPart inPart)
    {
    }
}
