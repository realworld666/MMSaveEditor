using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public abstract class PersonManager<T> : GenericManager<T> where T : Person
{
  protected List<T> mReplacementPeople = new List<T>();
}
