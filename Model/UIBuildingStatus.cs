// Decompiled with JetBrains decompiler
// Type: UIBuildingStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UIBuildingStatus : MonoBehaviour
{
  private Image mImage;
  private HQsBuilding_v1 mBuilding;

  public HQsBuilding_v1 building
  {
    get
    {
      return this.mBuilding;
    }
  }

  private void Awake()
  {
    this.mImage = this.GetComponent<Image>();
  }

  private void OnEnable()
  {
    if ((Object) this.mImage == (Object) null)
      this.mImage = this.GetComponent<Image>();
    this.UpdateIcon();
  }

  public void SetBuilding(HQsBuilding_v1 inBuilding)
  {
    this.mBuilding = inBuilding;
    this.UpdateIcon();
  }

  public void UpdateIcon()
  {
    if (!((Object) this.mImage != (Object) null) || this.mBuilding == null)
      return;
    this.mImage.gameObject.SetActive(this.mBuilding.state != HQsBuilding_v1.BuildingState.NotBuilt && this.mBuilding.state != HQsBuilding_v1.BuildingState.Constructed);
    switch (this.mBuilding.state)
    {
      case HQsBuilding_v1.BuildingState.BuildingInProgress:
        this.mImage.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Frontend1, "CarDevScreen-BuildingIcon");
        break;
      case HQsBuilding_v1.BuildingState.Upgrading:
        this.mImage.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Frontend1, "CarDevScreen-UpgradeIcon");
        break;
    }
  }
}
