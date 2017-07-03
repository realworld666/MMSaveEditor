// Decompiled with JetBrains decompiler
// Type: HQsStaticBuilding
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class HQsStaticBuilding : MonoBehaviour
{
  public List<HQsStaticLevel> levels = new List<HQsStaticLevel>();
  public HQsBuildingInfo.Type buildingType;
  public GameObject selection;
  public GameObject empty;
  public GameObject building;
  public GameObject center;
  public BoxCollider boxCollider;
  private GameObject mActiveStage;
  private GameObject mLastColliderStage;
  private HQsStaticBuilding.Status mStatus;
  private HQsBuilding_v1 mBuilding;

  public bool isClickable
  {
    get
    {
      if (this.mStatus == HQsStaticBuilding.Status.Clickable && this.mBuilding != null)
        return this.mBuilding.state != HQsBuilding_v1.BuildingState.NotBuilt;
      return false;
    }
  }

  public bool isHighlightable
  {
    get
    {
      if (this.mStatus == HQsStaticBuilding.Status.Clickable && this.mBuilding != null && !this.mBuilding.isBuilt)
        return !this.selection.activeSelf;
      return false;
    }
  }

  public bool canHighlight
  {
    get
    {
      if (this.mBuilding != null)
        return !this.mBuilding.isBuilt;
      return false;
    }
  }

  public bool isActive
  {
    get
    {
      return (UnityEngine.Object) this.mActiveStage != (UnityEngine.Object) null;
    }
  }

  public HQsBuilding_v1 sBuilding
  {
    get
    {
      return this.mBuilding;
    }
  }

  public void OnStart()
  {
    GameUtility.SetActive(this.selection, false);
    this.LoadAllUpgrades();
  }

  public void Load(HQsBuilding_v1 inBuilding)
  {
    if (this.mBuilding != null)
      this.mBuilding.OnNotification -= new Action<HQsBuilding_v1.NotificationState>(this.GetNotification);
    if (inBuilding == null)
      return;
    this.mBuilding = inBuilding;
    this.SetUpgradeLevel(this.mBuilding.currentLevel, this.mBuilding.state);
    this.SetStage(this.mBuilding.state);
    this.SetColliderSize();
    this.mBuilding.OnNotification += new Action<HQsBuilding_v1.NotificationState>(this.GetNotification);
    this.mStatus = HQsStaticBuilding.Status.Clickable;
  }

  public void PreLoad(HQsBuilding_v1 inBuilding)
  {
    if (inBuilding == null)
      return;
    this.mBuilding = inBuilding;
    this.SetUpgradeLevel(this.mBuilding.currentLevel, this.mBuilding.state);
    this.SetStage(this.mBuilding.state);
    this.SetColliderSize();
  }

  private void SetUpgradeLevel(int inLevel, HQsBuilding_v1.BuildingState inState)
  {
    int count = this.levels.Count;
    for (int index = 0; index < count; ++index)
    {
      HQsStaticLevel level = this.levels[index];
      GameUtility.SetActive(level.gameObject, (inState == HQsBuilding_v1.BuildingState.Constructed || inState == HQsBuilding_v1.BuildingState.Upgrading) && index == inLevel);
      if (level.gameObject.activeSelf)
        this.SetActiveStage(level, inState);
    }
  }

  private void SetActiveStage(HQsStaticLevel inActiveStage, HQsBuilding_v1.BuildingState inState)
  {
    GameObject mesh = inActiveStage.GetMesh();
    if ((UnityEngine.Object) mesh != (UnityEngine.Object) null)
    {
      GameUtility.SetActive(mesh, inState != HQsBuilding_v1.BuildingState.Upgrading || inState == HQsBuilding_v1.BuildingState.Upgrading && inActiveStage.keepModelActiveDuringUpgrade);
      if (mesh.activeSelf)
        this.mActiveStage = mesh;
    }
    GameObject upgradeMesh = inActiveStage.GetUpgradeMesh();
    if (!((UnityEngine.Object) upgradeMesh != (UnityEngine.Object) null))
      return;
    GameUtility.SetActive(upgradeMesh, inState == HQsBuilding_v1.BuildingState.Upgrading);
    if (!upgradeMesh.activeSelf)
      return;
    this.mActiveStage = upgradeMesh;
  }

  private void LoadAllUpgrades()
  {
    for (int inLevel = 0; inLevel < this.levels.Count; ++inLevel)
    {
      this.levels[inLevel].TrySetNewBuildingMesh(this.buildingType, inLevel);
      this.levels[inLevel].TrySetNewBuildingScaffoldingMesh(this.buildingType, inLevel);
    }
  }

  private void SetStage(HQsBuilding_v1.BuildingState inBuildingState)
  {
    GameUtility.SetActive(this.empty, inBuildingState == HQsBuilding_v1.BuildingState.NotBuilt);
    if (this.empty.activeSelf)
      this.mActiveStage = this.empty;
    GameUtility.SetActive(this.building, inBuildingState == HQsBuilding_v1.BuildingState.BuildingInProgress);
    if (!this.building.activeSelf)
      return;
    this.mActiveStage = this.building;
  }

  private void SetLevel()
  {
    if (this.mBuilding == null)
      return;
    this.SetUpgradeLevel(this.mBuilding.currentLevel, this.mBuilding.state);
  }

  public void PreviewBuilding(bool inValue, bool inForce)
  {
    if (!inForce && (!inValue || !this.isHighlightable) && (inValue || this.isHighlightable))
      return;
    GameUtility.SetActive(this.selection, inValue);
  }

  public void GetNotification(HQsBuilding_v1.NotificationState inState)
  {
    switch (inState)
    {
      case HQsBuilding_v1.NotificationState.BuildStarted:
        this.SetStage(HQsBuilding_v1.BuildingState.BuildingInProgress);
        break;
      case HQsBuilding_v1.NotificationState.BuildComplete:
        this.SetStage(HQsBuilding_v1.BuildingState.Constructed);
        break;
      case HQsBuilding_v1.NotificationState.UpgradeStarted:
        this.SetStage(HQsBuilding_v1.BuildingState.Upgrading);
        break;
      case HQsBuilding_v1.NotificationState.UpgradeComplete:
        this.SetStage(HQsBuilding_v1.BuildingState.Constructed);
        break;
      case HQsBuilding_v1.NotificationState.Demolish:
        this.SetStage(HQsBuilding_v1.BuildingState.NotBuilt);
        break;
    }
    this.PreviewBuilding(this.mBuilding.state == HQsBuilding_v1.BuildingState.NotBuilt, true);
    this.SetLevel();
    if (!this.mBuilding.isBuilt)
      return;
    this.SetColliderSize();
  }

  private void SetColliderSize()
  {
    if (!((UnityEngine.Object) this.boxCollider != (UnityEngine.Object) null) || !((UnityEngine.Object) this.mActiveStage != (UnityEngine.Object) null) || !((UnityEngine.Object) this.mLastColliderStage != (UnityEngine.Object) this.mActiveStage))
      return;
    Bounds inBounds = new Bounds(this.mActiveStage.transform.position, Vector3.zero);
    int childCount = this.mActiveStage.transform.childCount;
    for (int index = 0; index < childCount; ++index)
      this.UpdateColliderSize(this.mActiveStage.transform.GetChild(index).gameObject, ref inBounds);
    this.boxCollider.size = inBounds.size;
    this.boxCollider.center = inBounds.center - this.gameObject.transform.position;
    this.center.transform.localPosition = this.boxCollider.center;
    this.mLastColliderStage = this.mActiveStage;
  }

  private void UpdateColliderSize(GameObject inGameObject, ref Bounds inBounds)
  {
    if (inGameObject.name.ToLower().Contains("_ignore"))
      return;
    MeshRenderer[] componentsInChildren = inGameObject.GetComponentsInChildren<MeshRenderer>(true);
    int length = componentsInChildren.Length;
    for (int index = 0; index < length; ++index)
    {
      if (!componentsInChildren[index].name.ToLower().Contains("_ignore"))
      {
        if (inBounds.size == Vector3.zero)
          inBounds = componentsInChildren[index].bounds;
        else
          inBounds.Encapsulate(componentsInChildren[index].bounds);
      }
    }
  }

  public void SetStatus(HQsStaticBuilding.Status inStatus)
  {
    this.mStatus = inStatus;
  }

  private void OnDisable()
  {
    if (this.mBuilding == null)
      return;
    this.mBuilding.OnNotification -= new Action<HQsBuilding_v1.NotificationState>(this.GetNotification);
  }

  public enum Status
  {
    Clickable,
    Locked,
  }
}
