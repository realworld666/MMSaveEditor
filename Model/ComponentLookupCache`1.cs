// Decompiled with JetBrains decompiler
// Type: ComponentLookupCache`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class ComponentLookupCache<ComponentType> where ComponentType : MonoBehaviour
{
  private LinkedList<ComponentLookupCache<ComponentType>.CacheItem<ComponentType>> _lruList = new LinkedList<ComponentLookupCache<ComponentType>.CacheItem<ComponentType>>();
  private int _capacity;
  private Dictionary<GameObject, LinkedListNode<ComponentLookupCache<ComponentType>.CacheItem<ComponentType>>> _cacheMap;

  public ComponentLookupCache(int capacity)
  {
    this._capacity = capacity;
    this._cacheMap = new Dictionary<GameObject, LinkedListNode<ComponentLookupCache<ComponentType>.CacheItem<ComponentType>>>(capacity);
  }

  public ComponentType get(GameObject gameObject)
  {
    LinkedListNode<ComponentLookupCache<ComponentType>.CacheItem<ComponentType>> node;
    if (this._cacheMap.TryGetValue(gameObject, out node))
    {
      ComponentType component = node.Value.component;
      this._lruList.Remove(node);
      this._lruList.AddLast(node);
      return component;
    }
    ComponentType component1 = gameObject.GetComponent<ComponentType>();
    this.add(gameObject, component1);
    return component1;
  }

  public void add(GameObject gameObject, ComponentType component)
  {
    if (this._cacheMap.Count >= this._capacity)
      this.RemoveFirst();
    LinkedListNode<ComponentLookupCache<ComponentType>.CacheItem<ComponentType>> node = new LinkedListNode<ComponentLookupCache<ComponentType>.CacheItem<ComponentType>>(new ComponentLookupCache<ComponentType>.CacheItem<ComponentType>(gameObject, component));
    this._lruList.AddLast(node);
    this._cacheMap.Add(gameObject, node);
  }

  private void RemoveFirst()
  {
    LinkedListNode<ComponentLookupCache<ComponentType>.CacheItem<ComponentType>> first = this._lruList.First;
    this._lruList.RemoveFirst();
    this._cacheMap.Remove(first.Value.gameObject);
  }

  private struct CacheItem<V>
  {
    public GameObject gameObject;
    public V component;

    public CacheItem(GameObject gameObject_, V component_)
    {
      this.gameObject = gameObject_;
      this.component = component_;
    }
  }
}
