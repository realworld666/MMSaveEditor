// Decompiled with JetBrains decompiler
// Type: PrefabPool
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class PrefabPool
{
  private List<GameObject> mUnusedInstanceList = new List<GameObject>();
  private List<GameObject> mUsedInstanceList = new List<GameObject>();
  private GameObject mPrefab;
  private Transform mUnusedInstancesContainer;
  private int mPoolSize;
  private int mPoolUsed;
  private bool mLocalPool;

  public Transform prefabContainer
  {
    get
    {
      return this.mUnusedInstancesContainer;
    }
  }

  public int totalCount
  {
    get
    {
      return this.mUsedInstanceList.Count + this.mUnusedInstanceList.Count;
    }
  }

  public PrefabPool(GameObject inPrefab, int inInitialSize, Transform unusedInstancesContainer = null)
  {
    this.mPrefab = inPrefab;
    this.mPoolSize = inInitialSize;
    this.mLocalPool = (Object) unusedInstancesContainer != (Object) null;
    this.mUnusedInstancesContainer = unusedInstancesContainer ?? new GameObject().transform;
    this.CreatePrefabPool();
  }

  private void CreatePrefabPool()
  {
    this.SetPoolName();
    this.mUnusedInstanceList = new List<GameObject>(this.mPoolSize);
    this.mUsedInstanceList = new List<GameObject>(this.mPoolSize);
    for (int index = 0; index < this.mPoolSize; ++index)
      this.CreateInstance(false);
  }

  public void Destroy()
  {
    for (int index = 0; index < this.mUnusedInstanceList.Count; ++index)
      Object.Destroy((Object) this.mUnusedInstanceList[index]);
    for (int index = 0; index < this.mUsedInstanceList.Count; ++index)
      Object.Destroy((Object) this.mUsedInstanceList[index]);
    Object.Destroy((Object) this.mUnusedInstancesContainer);
    this.mUnusedInstanceList.Clear();
    this.mUsedInstanceList.Clear();
  }

  public void DestroyUnusedInstances()
  {
    for (int index = 0; index < this.mUnusedInstanceList.Count; ++index)
      Object.Destroy((Object) this.mUnusedInstanceList[index]);
    this.mUnusedInstanceList.Clear();
  }

  private GameObject CreateInstance(bool inActivateInstance)
  {
    GameObject inGameObject = Object.Instantiate<GameObject>(this.mPrefab);
    GameUtility.SetActive(inGameObject, inActivateInstance);
    inGameObject.name = this.mPrefab.name;
    GameUtility.SetParent(inGameObject.transform, this.mUnusedInstancesContainer, false);
    this.mUnusedInstanceList.Add(inGameObject);
    if (!inActivateInstance)
    {
      foreach (UIGridList componentsInChild in inGameObject.GetComponentsInChildren<UIGridList>(true))
        componentsInChild.OnStart();
    }
    return inGameObject;
  }

  public GameObject GetInstance()
  {
    if (this.mUnusedInstanceList.Count == 0)
    {
      this.CreateInstance(true);
      ++this.mPoolSize;
    }
    int count = this.mUnusedInstanceList.Count;
    GameObject mUnusedInstance = this.mUnusedInstanceList[count - 1];
    GameUtility.SetActive(mUnusedInstance.gameObject, true);
    if (!this.mLocalPool)
      GameUtility.SetParent(mUnusedInstance.transform, (Transform) null, false);
    this.mUnusedInstanceList.RemoveAt(count - 1);
    this.mUsedInstanceList.Add(mUnusedInstance);
    ++this.mPoolUsed;
    this.SetPoolName();
    return mUnusedInstance;
  }

  public T GetInstance<T>() where T : MonoBehaviour
  {
    return this.GetInstance().GetComponent<T>();
  }

  public void ReturnInstance(GameObject inGameObject)
  {
    Debug.Assert(this.mUsedInstanceList.Remove(inGameObject), "Object returned to pool that wasn't created from it");
    GameUtility.SetActive(inGameObject, false);
    this.mUnusedInstanceList.Add(inGameObject);
    if (!this.mLocalPool)
      GameUtility.SetParent(inGameObject.transform, this.mUnusedInstancesContainer, false);
    --this.mPoolUsed;
    this.SetPoolName();
  }

  public void ResetPool()
  {
    this.mUnusedInstanceList.Clear();
    this.mUsedInstanceList.Clear();
  }

  private void SetPoolName()
  {
    this.mUnusedInstancesContainer.name = "PrefabPool: " + (this.mPoolSize - this.mPoolUsed).ToString() + " / " + this.mPoolSize.ToString() + " : " + this.mPrefab.name;
  }
}
