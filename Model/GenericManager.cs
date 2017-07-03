using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class GenericManager<T> : BaseManager where T : Entity
{
  private List<T> mEntities = new List<T>();
    
}
