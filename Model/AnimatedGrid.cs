// Decompiled with JetBrains decompiler
// Type: AnimatedGrid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AnimatedGrid : UnityBehaviour
{
  public Vector3 spacing = Vector3.zero;
  public EasingUtility.Easing easing = EasingUtility.Easing.OutBack;
  public float animationDuration = 1f;
  private List<RectTransform> mItems = new List<RectTransform>();
  private List<RectTransform> mItemsCached = new List<RectTransform>();
  private Queue<RectTransform> mInsertQueue = new Queue<RectTransform>();
  public GameObject itemPrefab;
  public AnimatedGrid.Anchor anchor;
  public AnimatedGrid.Axis axis;
  private AnimatedGrid.State mState;

  public AnimatedGrid.State state
  {
    get
    {
      return this.mState;
    }
  }

  public int itemCount
  {
    get
    {
      return this.mItems.Count;
    }
  }

  private void Start()
  {
    this.ResetAllItemListsToChildren();
  }

  private void OnEnable()
  {
    this.StopAllCoroutines();
    this.ResetAllItemListsToChildren();
  }

  public T CreateListItem<T>() where T : Component
  {
    GameObject gameObject = Object.Instantiate<GameObject>(this.itemPrefab);
    this.SetIndexInstantly(gameObject.GetComponent<RectTransform>(), this.mItems.Count);
    return gameObject.GetComponent<T>();
  }

  public void DestroyListItems()
  {
    for (int index = 0; index < this.mItems.Count; ++index)
      Object.Destroy((Object) this.mItems[index], 0.0f);
  }

  private void Update()
  {
    if (this.mInsertQueue.Count <= 0)
      return;
    this.PerformNextInsert();
  }

  private void SetChildrenToItemsOrder()
  {
    int count = this.mItems.Count;
    for (int index = 0; index < count; ++index)
      this.mItems[index].SetSiblingIndex(index);
  }

  private void SetCachedItemsToChildrenOrder()
  {
    this.mItemsCached.Clear();
    int childCount = this.transform.childCount;
    for (int index = 0; index < childCount; ++index)
      this.mItemsCached.Add(this.transform.GetChild(index).GetComponent<RectTransform>());
  }

  private void ResetAllItemListsToChildren()
  {
    this.mItems.Clear();
    this.mItemsCached.Clear();
    int childCount = this.transform.childCount;
    for (int index = 0; index < childCount; ++index)
    {
      RectTransform component = this.transform.GetChild(index).GetComponent<RectTransform>();
      this.mItems.Add(component);
      this.mItemsCached.Add(component);
      component.gameObject.SetActive(true);
    }
  }

  [ContextMenu("Setup")]
  public void Reset()
  {
    this.ResetAllItemListsToChildren();
    for (int inIndex = 0; inIndex < this.mItems.Count; ++inIndex)
      this.mItems[inIndex].localPosition = this.GetIndexPosition(inIndex, AnimatedGrid.ListType.Local);
  }

  public void SetIndex(GameObject inGameObject, int inIndex)
  {
    this.SetIndex(inGameObject.GetComponent<RectTransform>(), inIndex);
  }

  public void SetIndex(RectTransform inItem, int inIndex)
  {
    this.mItems.Remove(inItem);
    this.mItems.Insert(inIndex, inItem);
  }

  public void SetIndexInstantly(GameObject inGameObject, int inIndex)
  {
    this.SetIndex(inGameObject.GetComponent<RectTransform>(), inIndex);
  }

  public void SetIndexInstantly(RectTransform inItem, int inIndex)
  {
    inItem.gameObject.SetActive(true);
    inItem.SetParent(this.transform, false);
    inItem.SetSiblingIndex(inIndex);
  }

  [ContextMenu("Order")]
  public void TestOrder()
  {
    this.SetIndex(this.mItems[1], 0);
    this.SetCachedItemsToChildrenOrder();
    this.Order();
  }

  public void Order()
  {
    this.BeginCoroutine(this.DoOrderAnimation());
  }

  [DebuggerHidden]
  private IEnumerator DoOrderAnimation()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new AnimatedGrid.\u003CDoOrderAnimation\u003Ec__Iterator1C() { \u003C\u003Ef__this = this };
  }

  [ContextMenu("Insert")]
  public void TestInsert()
  {
    this.Insert(this.mItems[this.mItems.Count - 1]);
    this.PerformNextInsert();
  }

  public void Insert(GameObject inItem)
  {
    this.Insert(inItem.GetComponent<RectTransform>());
  }

  public void Insert(RectTransform inItem)
  {
    inItem.gameObject.SetActive(false);
    this.mInsertQueue.Enqueue(inItem);
  }

  private void PerformNextInsert()
  {
    if (this.mState != AnimatedGrid.State.Idle)
      return;
    RectTransform inItem = this.mInsertQueue.Dequeue();
    inItem.SetParent(this.transform, false);
    inItem.SetSiblingIndex(0);
    this.SetCachedItemsToChildrenOrder();
    this.BeginCoroutine(this.DoInsertAnimation(inItem));
  }

  public void InsertInstantly(GameObject inItem)
  {
    this.InsertInstantly(inItem.GetComponent<RectTransform>());
  }

  public void InsertInstantly(RectTransform inItem)
  {
    inItem.gameObject.SetActive(true);
    inItem.SetParent(this.transform, false);
    inItem.SetSiblingIndex(0);
    this.Reset();
  }

  [DebuggerHidden]
  private IEnumerator DoInsertAnimation(RectTransform inItem)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new AnimatedGrid.\u003CDoInsertAnimation\u003Ec__Iterator1D() { inItem = inItem, \u003C\u0024\u003EinItem = inItem, \u003C\u003Ef__this = this };
  }

  private Vector3 GetIndexPosition(int inIndex, AnimatedGrid.ListType inListType)
  {
    List<RectTransform> rectTransformList = (List<RectTransform>) null;
    switch (inListType)
    {
      case AnimatedGrid.ListType.Local:
        rectTransformList = this.mItems;
        break;
      case AnimatedGrid.ListType.Cached:
        rectTransformList = this.mItemsCached;
        break;
    }
    Vector3 zero = Vector3.zero;
    for (int index = 0; index < inIndex; ++index)
    {
      RectTransform rectTransform1 = rectTransformList[index];
      RectTransform rectTransform2 = rectTransformList[Mathf.Min(index + 1, rectTransformList.Count)];
      float x = (float) (((double) rectTransform1.rect.width + (double) rectTransform2.rect.width) * 0.5);
      float y = (float) (((double) rectTransform1.rect.height + (double) rectTransform2.rect.height) * 0.5);
      zero += new Vector3(x, y, 0.0f);
    }
    Vector3 spacing = this.spacing;
    switch (this.axis)
    {
      case AnimatedGrid.Axis.Vertical:
        spacing.x = 0.0f;
        zero.x = 0.0f;
        if (this.anchor == AnimatedGrid.Anchor.TopLeft || this.anchor == AnimatedGrid.Anchor.TopRight)
        {
          zero.y = -zero.y;
          spacing.y = -spacing.y;
          break;
        }
        break;
      case AnimatedGrid.Axis.Horizontal:
        spacing.y = 0.0f;
        zero.y = 0.0f;
        break;
    }
    Vector3 vector3 = zero + spacing * (float) inIndex;
    return this.GetAnchorPosition(rectTransformList[0]) + vector3;
  }

  private Vector3 GetAnchorPosition(RectTransform inTransform)
  {
    Vector3 zero = Vector3.zero;
    switch (this.anchor)
    {
      case AnimatedGrid.Anchor.TopLeft:
        zero.y = this.rectTransform.rect.yMax + inTransform.rect.yMin;
        zero.x = this.rectTransform.rect.xMin + inTransform.rect.xMax;
        break;
      case AnimatedGrid.Anchor.TopRight:
        zero.y = this.rectTransform.rect.yMax + inTransform.rect.yMin;
        zero.x = this.rectTransform.rect.xMax + inTransform.rect.xMin;
        break;
      case AnimatedGrid.Anchor.BottomLeft:
        zero.y = this.rectTransform.rect.yMin + inTransform.rect.yMax;
        zero.x = this.rectTransform.rect.xMin + inTransform.rect.xMax;
        break;
      case AnimatedGrid.Anchor.BottomRight:
        zero.y = this.rectTransform.rect.yMin + inTransform.rect.yMax;
        zero.x = this.rectTransform.rect.xMax + inTransform.rect.xMin;
        break;
    }
    return zero;
  }

  public GameObject GetItem(int inIndex)
  {
    return this.mItems[inIndex].gameObject;
  }

  public T GetItem<T>(int inIndex) where T : Component
  {
    return this.mItems[inIndex].GetComponent<T>();
  }

  public GameObject GetCachedItem(int inIndex)
  {
    return this.mItemsCached[inIndex].gameObject;
  }

  public void Clear()
  {
    this.StopAllCoroutines();
    this.mState = AnimatedGrid.State.Idle;
    this.mItems.Clear();
    this.mItemsCached.Clear();
  }

  public enum Axis
  {
    Vertical,
    Horizontal,
  }

  public enum State
  {
    Idle,
    Animating,
  }

  public enum ListType
  {
    Local,
    Cached,
  }

  public enum Anchor
  {
    TopLeft,
    TopRight,
    BottomLeft,
    BottomRight,
  }
}
