
using FullSerializer;
using System;
using System.Collections.Generic;
using System.Linq;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class EntityManager
{
  private List<Entity> mEntities = new List<Entity>();
}
