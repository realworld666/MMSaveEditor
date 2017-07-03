using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class Map<TKey, TValue>
{
  public List<TKey> mKeys = new List<TKey>();
  public List<TValue> mValues = new List<TValue>();

}
