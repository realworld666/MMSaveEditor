using FullSerializer;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class HQsManager
{
  public Dictionary<int, Headquarters> headquarters = new Dictionary<int, Headquarters>();
    
}
