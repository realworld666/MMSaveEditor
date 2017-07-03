// Decompiled with JetBrains decompiler
// Type: UIHQInfoBars
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHQInfoBars : MonoBehaviour
{
  private Dictionary<HQsBuilding_v1, UIHQInfo> bars = new Dictionary<HQsBuilding_v1, UIHQInfo>();
  public UIGridList grid;
  public ToggleGroup toggleGroup;
  public HeadquartersScreen screen;

  public void Setup()
  {
    int itemCount1 = this.grid.itemCount;
    int count = this.screen.staticBuildings.Count;
    int num = count - itemCount1;
    for (int index = 0; index < num; ++index)
      this.grid.CreateListItem<UIHQInfo>();
    int itemCount2 = this.grid.itemCount;
    for (int inIndex = 0; inIndex < itemCount2; ++inIndex)
    {
      UIHQInfo uihqInfo = this.grid.GetItem<UIHQInfo>(inIndex);
      if (inIndex < count)
      {
        HQsStaticBuilding staticBuilding = this.screen.staticBuildings[inIndex];
        uihqInfo.Setup(staticBuilding);
        if (!this.bars.ContainsKey(staticBuilding.sBuilding))
          this.bars.Add(staticBuilding.sBuilding, uihqInfo);
      }
      else
        GameUtility.SetActive(uihqInfo.gameObject, false);
    }
    GameUtility.SetActive(this.grid.itemPrefab, false);
  }

  public void HideAll()
  {
    this.ClearNotifications();
    this.toggleGroup.SetAllTogglesOff();
    this.grid.DestroyListItems();
    GameUtility.SetActive(this.grid.itemPrefab, false);
  }

  public void SelectBuilding()
  {
    this.toggleGroup.SetAllTogglesOff();
    int itemCount = this.grid.itemCount;
    bool flag = (Object) this.screen.selectedStaticBuilding != (Object) null && this.screen.selectedStaticBuilding.sBuilding.state == HQsBuilding_v1.BuildingState.NotBuilt;
    for (int inIndex = 0; inIndex < itemCount; ++inIndex)
    {
      UIHQInfo uihqInfo = this.grid.GetItem<UIHQInfo>(inIndex);
      if ((uihqInfo.gameObject.activeSelf || flag) && (Object) uihqInfo.building == (Object) this.screen.selectedStaticBuilding)
      {
        if (flag)
          uihqInfo.Preview(true);
        else
          uihqInfo.Select();
      }
      if (uihqInfo.isPreview && (Object) this.screen.selectedStaticBuilding != (Object) uihqInfo.building)
        uihqInfo.Preview(false);
    }
  }

  public void ClearNotifications()
  {
    int itemCount = this.grid.itemCount;
    for (int inIndex = 0; inIndex < itemCount; ++inIndex)
      this.grid.GetItem<UIHQInfo>(inIndex).ClearNotification();
  }

  public UIHQInfo GetInfoBar(HQsBuilding_v1 inBuilding)
  {
    if (this.bars.ContainsKey(inBuilding))
      return this.bars[inBuilding];
    return (UIHQInfo) null;
  }
}
