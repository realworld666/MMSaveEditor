// Decompiled with JetBrains decompiler
// Type: UIGridList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIGridList : MonoBehaviour
{
  [SerializeField]
  [FormerlySerializedAs("animation")]
  private UIGridList.Animation shouldAnimate = UIGridList.Animation.DontAnimate;
  private List<GameObject> mItemList = new List<GameObject>();
  public GameObject itemPrefab;
  public Transform grid;
  [SerializeField]
  private Scrollbar scrollbar;
  [SerializeField]
  private float animationInterval;
  [SerializeField]
  private bool usePrefabPool;
  [Range(0.0f, 256f)]
  public int prefabPoolSize;
  private PrefabPool mPrefabPool;
  private float mAnimateTimer;
  private bool mUpdateFlag;
  private bool mAnimateFlag;
  private bool mUpdateScrollbar;

  public bool usePrefabPoolCheck
  {
    get
    {
      if (this.mPrefabPool != null)
        return this.usePrefabPool;
      return false;
    }
  }

  public int itemCount
  {
    get
    {
      return this.mItemList.Count;
    }
  }

  public virtual void OnStart()
  {
    if ((Object) this.scrollbar == (Object) null)
    {
      Scrollbar[] componentsInChildren = this.GetComponentsInChildren<Scrollbar>(true);
      if (componentsInChildren != null && componentsInChildren.Length > 0)
        this.scrollbar = componentsInChildren[0];
    }
    this.CreatePrefabPool();
  }

  private void CreatePrefabPool()
  {
    if (!this.usePrefabPool || this.mPrefabPool != null || !((Object) this.itemPrefab != (Object) null))
      return;
    this.mPrefabPool = new PrefabPool(this.itemPrefab, this.prefabPoolSize, this.grid);
  }

  public void AddListItem(GameObject inItem)
  {
    this.mItemList.Add(inItem);
    this.mUpdateFlag = true;
  }

  public T CreateListItem<T>() where T : Component
  {
    GameObject gameObject = !this.usePrefabPoolCheck ? Object.Instantiate<GameObject>(this.itemPrefab) : this.mPrefabPool.GetInstance();
    this.mItemList.Add(gameObject);
    GameUtility.SetParent(gameObject.GetComponent<Transform>(), this.grid, false);
    gameObject.transform.SetAsLastSibling();
    this.mUpdateFlag = true;
    return gameObject.GetComponent<T>();
  }

  public void SortList(UIGridList.SortOrder inOrder)
  {
    int count = this.mItemList.Count;
    for (int index = 0; index < count; ++index)
      GameUtility.SetParent(this.mItemList[index].transform, (Transform) null, false);
    switch (inOrder)
    {
      case UIGridList.SortOrder.OldestToNewest:
        for (int index = 0; index < count; ++index)
          GameUtility.SetParent(this.mItemList[index].transform, this.grid, false);
        break;
      case UIGridList.SortOrder.NewestToOldest:
        for (int index = count - 1; index >= 0; --index)
          GameUtility.SetParent(this.mItemList[index].transform, this.grid, false);
        break;
    }
  }

  public void ResetScrollbar()
  {
    this.mUpdateScrollbar = true;
  }

  private void Update()
  {
    if (this.mUpdateFlag && this.shouldAnimate == UIGridList.Animation.Animate)
    {
      this.mUpdateFlag = false;
      this.mAnimateFlag = true;
      this.HideListItems();
    }
    if (this.mUpdateScrollbar)
    {
      this.mUpdateScrollbar = false;
      Canvas.ForceUpdateCanvases();
      if ((Object) this.scrollbar != (Object) null)
        this.scrollbar.value = 1f;
    }
    if (!this.mAnimateFlag)
      return;
    this.AnimateListItems();
  }

  public void AnimateListItems()
  {
    this.mAnimateTimer += GameTimer.deltaTime * 1f;
    if ((double) this.mAnimateTimer < (double) this.animationInterval)
      return;
    int count = this.mItemList.Count;
    for (int index = 0; index < count; ++index)
    {
      if (!this.mItemList[index].activeSelf)
      {
        GameUtility.SetActive(this.mItemList[index], true);
        this.mAnimateTimer = 0.0f;
        break;
      }
    }
  }

  public void HideListItems()
  {
    int count = this.mItemList.Count;
    for (int index = 0; index < count; ++index)
      GameUtility.SetActive(this.mItemList[index], false);
  }

  public void ShowListItems()
  {
    int count = this.mItemList.Count;
    for (int index = 0; index < count; ++index)
      GameUtility.SetActive(this.mItemList[index], true);
  }

  public void DestroyListItem(int inIndex)
  {
    if (inIndex >= this.mItemList.Count || !((Object) this.mItemList[inIndex] != (Object) null))
      return;
    GameObject mItem = this.mItemList[inIndex];
    this.mItemList.RemoveAt(inIndex);
    this.DestroyItemObject(mItem);
    this.mUpdateFlag = true;
  }

  public void DestroyListItem(GameObject inGameObject)
  {
    if (!((Object) inGameObject != (Object) null))
      return;
    int count = this.mItemList.Count;
    for (int index = 0; index < count; ++index)
    {
      GameObject mItem = this.mItemList[index];
      if ((Object) mItem == (Object) inGameObject)
      {
        GameObject inItem = mItem;
        this.mItemList.RemoveAt(index);
        this.DestroyItemObject(inItem);
        this.mUpdateFlag = true;
        break;
      }
    }
  }

  public void DestroyListItems()
  {
    int count = this.mItemList.Count;
    for (int index = 0; index < count; ++index)
      this.DestroyItemObject(this.mItemList[index]);
    this.mUpdateFlag = true;
    this.mItemList.Clear();
  }

  private void OnDestroy()
  {
    if (!UIManager.InstanceExists || !this.usePrefabPoolCheck)
      return;
    this.ResetPrefabPool();
  }

  public void ResetPrefabPool()
  {
    this.mPrefabPool.ResetPool();
    this.mPrefabPool = (PrefabPool) null;
  }

  private void DestroyItemObject(GameObject inItem)
  {
    if (!((Object) inItem != (Object) null))
      return;
    if (this.usePrefabPoolCheck)
      this.mPrefabPool.ReturnInstance(inItem);
    else
      Object.Destroy((Object) inItem);
  }

  public void ClearListItems()
  {
    this.mUpdateFlag = true;
    this.mItemList.Clear();
  }

  public void Refresh()
  {
    this.mUpdateFlag = true;
  }

  public GameObject GetItem(int inIndex)
  {
    return this.mItemList[inIndex];
  }

  public T GetOrCreateItem<T>(int inIndex) where T : Component
  {
    if (inIndex >= this.itemCount)
      return this.CreateListItem<T>();
    if ((Object) this.mItemList[inIndex] == (Object) null)
      return (T) null;
    return this.mItemList[inIndex].GetComponent<T>();
  }

  public T GetItem<T>(int inIndex) where T : Component
  {
    if ((Object) this.mItemList[inIndex] == (Object) null)
      return (T) null;
    return this.mItemList[inIndex].GetComponent<T>();
  }

  public List<GameObject> GetGameObjectList()
  {
    return this.mItemList;
  }

  public enum Animation
  {
    Animate,
    DontAnimate,
  }

  public enum SortOrder
  {
    OldestToNewest,
    NewestToOldest,
  }
}
