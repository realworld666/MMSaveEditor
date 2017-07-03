// Decompiled with JetBrains decompiler
// Type: GenericManager`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class GenericManager<T> : BaseManager where T : Entity
{
  private List<T> mEntities = new List<T>();

  public int count
  {
    get
    {
      return this.mEntities.Count;
    }
  }

  public void AddEntity(T inEntity)
  {
    this.mEntities.Add(inEntity);
  }

  public void RemoveEntity(T inEntity)
  {
    this.mEntities.Remove(inEntity);
  }

  public T GetEntity(int inIndex)
  {
    return this.mEntities[inIndex];
  }

  public T GetEntity(Guid inID)
  {
    T obj = (T) null;
    for (int index = 0; index < this.count; ++index)
    {
      if (this.mEntities[index].id == inID)
      {
        obj = this.mEntities[index];
        break;
      }
    }
    return obj;
  }

  public List<T> GetEntityList()
  {
    return this.mEntities;
  }
}
