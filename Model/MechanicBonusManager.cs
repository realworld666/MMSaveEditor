using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class MechanicBonusManager
{
  public Dictionary<int, MechanicBonus> mechanicBonuses = new Dictionary<int, MechanicBonus>();


}
