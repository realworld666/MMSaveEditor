using FullSerializer;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ClimateManager
{
  private List<Climate> mClimates = new List<Climate>();
}
