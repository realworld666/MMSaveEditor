// Decompiled with JetBrains decompiler
// Type: UILoadList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UILoadList : UIGridList
{
  private List<GameObject> mItemList = new List<GameObject>();
  private List<GameObject> mActivatedItemList = new List<GameObject>();
  private Vector2 mElementSize = Vector2.zero;
  private Vector2 mBlockSize = Vector2.zero;
  private int mCurrentListSize = 1;
  private int mCurrentActivatedIndex = -1;
  public Action OnScrollRect;
  public GameObject fillerPrefab;
  public ScrollRect scrollRect;
  private PrefabPool mItemPrefabPool;
  private GameObject mTopFiller;
  private GameObject mBotFiller;
  private LayoutElement mItemElement;
  private LayoutElement mTopElement;
  private LayoutElement mBotElement;
  private int mLastListSize;
  private int mLastActivatedIndex;

  public GameObject[] activatedItems
  {
    get
    {
      return this.mActivatedItemList.ToArray();
    }
  }

  public int firstActivatedIndex
  {
    get
    {
      return this.mCurrentActivatedIndex;
    }
  }

  public override void OnStart()
  {
    this.CreatePrefabPools();
    this.scrollRect.onValueChanged.AddListener(new UnityAction<Vector2>(this.OnScrollRectUpdate));
  }

  private void CreatePrefabPools()
  {
    this.mItemPrefabPool = new PrefabPool(this.itemPrefab, this.prefabPoolSize, this.grid);
    for (int index = 0; index < this.prefabPoolSize; ++index)
    {
      GameObject instance = this.mItemPrefabPool.GetInstance();
      instance.transform.SetAsLastSibling();
      GameUtility.SetParent(instance.transform, this.grid, false);
      this.mItemList.Add(instance);
    }
    this.mItemElement = this.itemPrefab.GetComponent<LayoutElement>();
    this.mElementSize.Set(this.mItemElement.preferredWidth, this.mItemElement.preferredHeight);
    this.mBlockSize.Set(0.0f, (float) this.prefabPoolSize * this.mElementSize.y);
    this.mTopFiller = UnityEngine.Object.Instantiate<GameObject>(this.fillerPrefab);
    GameUtility.SetParent(this.mTopFiller.transform, this.grid, false);
    this.mTopElement = this.mTopFiller.GetComponent<LayoutElement>();
    this.mTopFiller.transform.SetAsFirstSibling();
    GameUtility.SetActive(this.mTopFiller, true);
    this.mBotFiller = UnityEngine.Object.Instantiate<GameObject>(this.fillerPrefab);
    GameUtility.SetParent(this.mBotFiller.transform, this.grid, false);
    this.mBotElement = this.mBotFiller.GetComponent<LayoutElement>();
    this.mBotFiller.transform.SetAsLastSibling();
    GameUtility.SetActive(this.mBotFiller, true);
  }

  public bool SetSize(int inListSize, bool inForceUpdate = false)
  {
    this.mLastListSize = this.mCurrentListSize;
    this.mCurrentListSize = inListSize;
    this.mLastActivatedIndex = this.mCurrentActivatedIndex;
    this.mCurrentActivatedIndex = Mathf.Clamp(this.GetIndex(), 0, Mathf.Max(0, this.mCurrentListSize - this.mItemList.Count));
    this.SetFillersSize();
    if (this.mLastListSize != this.mCurrentListSize || this.mLastActivatedIndex != this.mCurrentActivatedIndex || inForceUpdate)
    {
      this.ActivateItems();
      LayoutRebuilder.ForceRebuildLayoutImmediate(this.scrollRect.content);
      EventSystem.current.SetSelectedGameObject((GameObject) null);
      return true;
    }
    LayoutRebuilder.ForceRebuildLayoutImmediate(this.scrollRect.content);
    return false;
  }

  private int GetIndex()
  {
    return Mathf.RoundToInt((float) ((1.0 - (double) this.scrollRect.verticalScrollbar.value) * (double) this.mCurrentListSize - (double) this.mItemList.Count / 2.0));
  }

  private void SetFillersSize()
  {
    this.mTopElement.preferredHeight = (float) this.mCurrentActivatedIndex * this.mElementSize.y;
    this.mBotElement.preferredHeight = (float) Mathf.Max(this.mCurrentListSize - (this.mCurrentActivatedIndex + this.mItemList.Count), 0) * this.mElementSize.y;
  }

  private void ActivateItems()
  {
    int count = this.mItemList.Count;
    this.mActivatedItemList.Clear();
    for (int index = 0; index < count; ++index)
    {
      GameObject mItem = this.mItemList[index];
      GameUtility.SetActive(mItem, index < this.mCurrentListSize);
      if (index < this.mCurrentListSize)
        this.mActivatedItemList.Add(mItem);
    }
  }

  private void OnScrollRectUpdate(Vector2 inVector2)
  {
    if (this.OnScrollRect == null)
      return;
    this.OnScrollRect();
  }

  private void OnDestroy()
  {
    if (this.OnScrollRect != null)
      this.OnScrollRect = (Action) null;
    if (!UIManager.InstanceExists || !this.usePrefabPoolCheck)
      return;
    this.ResetPrefabPool();
  }
}
