// Decompiled with JetBrains decompiler
// Type: UIHQGroupIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UIHQGroupIcon : MonoBehaviour
{
  private HQsBuildingInfo.Category mCategory = HQsBuildingInfo.Category.Factory;
  public GameObject factory;
  public GameObject design;
  public GameObject performance;
  public GameObject team;
  public GameObject brand;

  public void SetIcon(HQsBuilding_v1 inBuilding)
  {
    this.mCategory = inBuilding.info.category;
    this.UpdateIcon();
  }

  public void SetIcon(HQsBuildingInfo.Category inCategory)
  {
    this.mCategory = inCategory;
    this.UpdateIcon();
  }

  public void UpdateIcon()
  {
    GameUtility.SetActive(this.factory, this.mCategory == HQsBuildingInfo.Category.Factory);
    GameUtility.SetActive(this.design, this.mCategory == HQsBuildingInfo.Category.Design);
    GameUtility.SetActive(this.performance, this.mCategory == HQsBuildingInfo.Category.Performance);
    GameUtility.SetActive(this.team, this.mCategory == HQsBuildingInfo.Category.Staff);
    GameUtility.SetActive(this.brand, this.mCategory == HQsBuildingInfo.Category.Brand);
  }
}
