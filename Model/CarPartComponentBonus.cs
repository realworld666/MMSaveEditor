using FullSerializer;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class CarPartComponentBonus
{
  public string name = string.Empty;
  public float bonusValue;
  public CarPartComponent component;
   
}
