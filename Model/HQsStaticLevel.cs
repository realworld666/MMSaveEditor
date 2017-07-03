// Decompiled with JetBrains decompiler
// Type: HQsStaticLevel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class HQsStaticLevel : MonoBehaviour
{
  public bool keepModelActiveDuringUpgrade = true;
  private bool mDefaultKeepModelActiveDuringUpgrade = true;
  public GameObject model;
  public GameObject upgrade;
  private bool mIsMeshLoadedByMod;
  private GameObject mModelMod;
  private bool mIsUpgradeMeshLoadedByMod;
  private GameObject mUpgradeModelMod;
  private bool mTriedToLoadMesh;
  private bool mTriedToLoadUpgradeMesh;

  public GameObject GetMesh()
  {
    if (this.mIsMeshLoadedByMod)
      return this.mModelMod;
    return this.model;
  }

  public GameObject GetUpgradeMesh()
  {
    if (this.mIsUpgradeMeshLoadedByMod)
      return this.mUpgradeModelMod;
    return this.upgrade;
  }

  public void TrySetNewBuildingMesh(HQsBuildingInfo.Type inBuildingType, int inLevel)
  {
    if (this.mTriedToLoadMesh)
      return;
    GameObject headquartersBuilding = App.instance.assetManager.GetHeadquartersBuilding(inBuildingType, inLevel, false);
    if ((Object) headquartersBuilding != (Object) null)
    {
      this.mModelMod = headquartersBuilding;
      this.mModelMod.transform.SetParent(this.transform, false);
      this.mIsMeshLoadedByMod = true;
      this.mDefaultKeepModelActiveDuringUpgrade = this.keepModelActiveDuringUpgrade;
      this.keepModelActiveDuringUpgrade = true;
      GameUtility.SetActive(this.model, false);
    }
    this.mTriedToLoadMesh = true;
  }

  public void TrySetNewBuildingScaffoldingMesh(HQsBuildingInfo.Type inBuildingType, int inLevel)
  {
    if (this.mTriedToLoadUpgradeMesh)
      return;
    GameObject headquartersBuilding = App.instance.assetManager.GetHeadquartersBuilding(inBuildingType, inLevel, true);
    if ((Object) headquartersBuilding != (Object) null)
    {
      this.mUpgradeModelMod = headquartersBuilding;
      this.mUpgradeModelMod.transform.SetParent(this.transform, false);
      this.mIsUpgradeMeshLoadedByMod = true;
      GameUtility.SetActive(this.mUpgradeModelMod, false);
      if ((Object) this.upgrade != (Object) null)
        GameUtility.SetActive(this.upgrade, false);
    }
    this.mTriedToLoadUpgradeMesh = true;
  }

  public void ResetMeshToDefault()
  {
    if (this.mIsMeshLoadedByMod)
    {
      GameUtility.SetActive(this.mModelMod, false);
      Object.Destroy((Object) this.mModelMod);
      this.mModelMod = (GameObject) null;
      this.mIsMeshLoadedByMod = false;
      GameUtility.SetActive(this.model, true);
      this.keepModelActiveDuringUpgrade = this.mDefaultKeepModelActiveDuringUpgrade;
    }
    if (!this.mIsUpgradeMeshLoadedByMod)
      return;
    GameUtility.SetActive(this.mUpgradeModelMod, false);
    Object.Destroy((Object) this.mUpgradeModelMod);
    this.mUpgradeModelMod = (GameObject) null;
    this.mIsUpgradeMeshLoadedByMod = false;
  }

  public void ReloadMeshes(HQsBuildingInfo.Type inBuildingType, int inLevel)
  {
    this.mTriedToLoadMesh = false;
    this.mTriedToLoadUpgradeMesh = false;
    this.ResetMeshToDefault();
    this.TrySetNewBuildingMesh(inBuildingType, inLevel);
    this.TrySetNewBuildingScaffoldingMesh(inBuildingType, inLevel);
  }
}
