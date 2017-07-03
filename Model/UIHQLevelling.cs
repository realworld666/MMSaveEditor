// Decompiled with JetBrains decompiler
// Type: UIHQLevelling
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class UIHQLevelling : MonoBehaviour
{
  private List<UIHQLevellingEntry> mEntries = new List<UIHQLevellingEntry>();
  public UIGridList grid;
  public HeadquartersScreen screen;
  private bool mDisplay;

  public bool display
  {
    get
    {
      return this.mDisplay;
    }
  }

  public void Setup()
  {
    HQsBuilding_v1.OnBuildingNotification -= new Action<HQsBuilding_v1.NotificationState, HQsBuilding_v1>(this.OnNotification);
    HQsBuilding_v1.OnBuildingNotification += new Action<HQsBuilding_v1.NotificationState, HQsBuilding_v1>(this.OnNotification);
    this.CreateGrid();
  }

  public void CreateGrid()
  {
    int num = this.screen.staticBuildings.Count - this.grid.itemCount;
    for (int index = 0; index < num; ++index)
    {
      UIHQLevellingEntry listItem = this.grid.CreateListItem<UIHQLevellingEntry>();
      listItem.Setup(this.screen.staticBuildings[index].sBuilding);
      this.mEntries.Add(listItem);
    }
    int count = this.mEntries.Count;
    this.UpdateGrid();
  }

  public void UpdateGrid()
  {
    this.mEntries.Sort((Comparison<UIHQLevellingEntry>) ((x, y) => x.secondsRemaining.CompareTo(y.secondsRemaining)));
    int count = this.mEntries.Count;
    int index1 = 0;
    for (int index2 = 0; index2 < count; ++index2)
    {
      UIHQLevellingEntry mEntry = this.mEntries[index2];
      GameUtility.SetActive(mEntry.gameObject, mEntry.building.isLeveling);
      if (mEntry.gameObject.activeSelf)
      {
        if (mEntry.transform.GetSiblingIndex() != index1)
          mEntry.transform.SetSiblingIndex(index1);
        ++index1;
      }
    }
    GameUtility.SetActive(this.grid.itemPrefab, false);
    this.mDisplay = index1 > 0;
    GameUtility.SetActive(this.gameObject, this.mDisplay);
  }

  private void OnNotification(HQsBuilding_v1.NotificationState inState, HQsBuilding_v1 inBuilding)
  {
    if (inState == HQsBuilding_v1.NotificationState.UnLocked)
      return;
    this.UpdateGrid();
  }

  public void HideAll()
  {
    this.ClearNotifications();
    GameUtility.SetActive(this.gameObject, false);
  }

  public void ClearNotifications()
  {
    HQsBuilding_v1.OnBuildingNotification -= new Action<HQsBuilding_v1.NotificationState, HQsBuilding_v1>(this.OnNotification);
  }
}
