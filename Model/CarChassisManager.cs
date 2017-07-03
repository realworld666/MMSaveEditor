using FullSerializer;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class CarChassisManager
{
  public Dictionary<int, CarChassisStats> chassisStats = new Dictionary<int, CarChassisStats>();

}
