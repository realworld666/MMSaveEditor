// Decompiled with JetBrains decompiler
// Type: HeadquartersScene
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class HeadquartersScene : BackgroundScene
{
  public Transform[] parentsForVehiclePrefab = new Transform[0];
  private List<HQsStaticBuilding> mBuildings = new List<HQsStaticBuilding>();
  public GameCamera cameraController;
  [SerializeField]
  private Material primaryTeamColorMat;
  [SerializeField]
  private Material secondaryTeamColorMat;
  public HeadquartersScreen screen;
  private HQsStaticBuilding mHoverBuilding;
  private HQsStaticBuilding mSelectedBuilding;
  private HQsStaticBuilding mLastBuilding;
  private UnityVehicleManager mVehicleManager;
  private Vehicle mVehicle;
  private bool mIsDragging;
  private int m_CameraEffectsSetFrames;

  public bool isHoverClickableBuilding
  {
    get
    {
      if ((UnityEngine.Object) this.mHoverBuilding != (UnityEngine.Object) null)
        return this.mHoverBuilding.isClickable;
      return false;
    }
  }

  public bool isHoverEmptyArea
  {
    get
    {
      if ((UnityEngine.Object) this.mHoverBuilding == (UnityEngine.Object) null)
        return true;
      if ((UnityEngine.Object) this.mHoverBuilding != (UnityEngine.Object) null && (UnityEngine.Object) this.mHoverBuilding != (UnityEngine.Object) this.mSelectedBuilding)
        return !this.mHoverBuilding.isClickable;
      return false;
    }
  }

  public bool isDragging
  {
    get
    {
      return this.mIsDragging;
    }
  }

  public HQsStaticBuilding selectedBuilding
  {
    get
    {
      return this.mSelectedBuilding;
    }
    set
    {
      this.mSelectedBuilding = value;
    }
  }

  public void OnStart()
  {
    IT_Gesture.onDraggingStartE -= new IT_Gesture.DraggingStartHandler(this.OnDragStart);
    IT_Gesture.onDraggingStartE += new IT_Gesture.DraggingStartHandler(this.OnDragStart);
    IT_Gesture.onDraggingEndE -= new IT_Gesture.DraggingEndHandler(this.OnDragEnd);
    IT_Gesture.onDraggingEndE += new IT_Gesture.DraggingEndHandler(this.OnDragEnd);
    TeamColor teamColor = Game.instance.player.team.GetTeamColor();
    this.primaryTeamColorMat.color = teamColor.primaryUIColour.normal;
    this.secondaryTeamColorMat.color = teamColor.secondaryUIColour.normal;
    this.m_CameraEffectsSetFrames = 2;
    this.SetupVehicles();
  }

  private void SetupVehicles()
  {
    if (this.mVehicleManager == null)
      this.mVehicleManager = new UnityVehicleManager();
    this.mVehicle = (Vehicle) new RacingVehicle(0, 0, Game.instance.player.team.GetDriver(0), Game.instance.player.team.carManager.GetCar(0), this.mVehicleManager);
    this.mVehicleManager.DestroyCars();
    for (int index1 = 0; index1 < this.parentsForVehiclePrefab.Length; ++index1)
    {
      UnityVehicle car = this.mVehicleManager.CreateCar("TestTrackCar", this.mVehicle);
      car.enabled = false;
      for (int index2 = this.parentsForVehiclePrefab[index1].childCount - 1; index2 >= 0; --index2)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.parentsForVehiclePrefab[index1].GetChild(index2).gameObject);
      car.transform.SetParent(this.parentsForVehiclePrefab[index1]);
      car.transform.position = this.parentsForVehiclePrefab[index1].position;
      car.transform.rotation = this.parentsForVehiclePrefab[index1].rotation;
    }
  }

  public void OnExit()
  {
    IT_Gesture.onDraggingStartE -= new IT_Gesture.DraggingStartHandler(this.OnDragStart);
    IT_Gesture.onDraggingEndE -= new IT_Gesture.DraggingEndHandler(this.OnDragEnd);
    this.cameraController.freeRoamCamera.OnTargetChange -= new Action<GameObject>(this.OnCameraChangeTarget);
  }

  private void Update()
  {
    if (App.instance.preferencesManager.videoPreferences.isRunning2DMode)
      return;
    if (this.m_CameraEffectsSetFrames > 0)
    {
      --this.m_CameraEffectsSetFrames;
      this.SetCameraEffectFromEnvironmentManager();
    }
    this.HandleMouseSelection();
  }

  public void SetDefaultCamera(HQsBuilding_v1 inBuilding)
  {
    this.EnableCamera("GameCamera");
    this.cameraController.freeRoamCamera.enabled = true;
    if (inBuilding != null)
    {
      this.mSelectedBuilding = this.GetBuilding(inBuilding);
      this.cameraController.freeRoamCamera.SetTarget(this.mSelectedBuilding.center, CameraManager.Transition.Instant, 0.0f);
    }
    else
    {
      this.cameraController.freeRoamCamera.SetTarget(this.GetBuilding(this.screen.headquarters.GetBuilding(HQsBuildingInfo.Type.Factory)).center, CameraManager.Transition.Instant, 0.0f);
      this.cameraController.transform.localEulerAngles = new Vector3(25f, 345f, 0.0f);
      this.cameraController.freeRoamCamera.zoom = 0.75f;
      this.mSelectedBuilding = (HQsStaticBuilding) null;
    }
    this.cameraController.freeRoamCamera.UpdateFollowingTarget();
    this.cameraController.freeRoamCamera.OnTargetChange -= new Action<GameObject>(this.OnCameraChangeTarget);
    this.cameraController.freeRoamCamera.OnTargetChange += new Action<GameObject>(this.OnCameraChangeTarget);
  }

  public void SetCameraEffectFromEnvironmentManager()
  {
    EnvironmentManager componentInChildren1 = this.GetComponentInChildren<EnvironmentManager>(true);
    GameCameraEffectsHolder componentInChildren2 = this.cameraController.GetComponentInChildren<GameCameraEffectsHolder>();
    if (!((UnityEngine.Object) componentInChildren1 != (UnityEngine.Object) null) || !((UnityEngine.Object) componentInChildren2 != (UnityEngine.Object) null))
      return;
    CameraEffectsScriptableObject scriptableObject = componentInChildren1.GetCameraEffectsScriptableObject();
    componentInChildren2.SetScriptableObject(ref scriptableObject);
  }

  public void SetCameraTarget(HQsBuilding_v1 inBuilding)
  {
    if (inBuilding == null)
      return;
    HQsStaticBuilding building = this.GetBuilding(inBuilding);
    if (!((UnityEngine.Object) building != (UnityEngine.Object) null) || !((UnityEngine.Object) building != (UnityEngine.Object) this.mSelectedBuilding))
      return;
    this.SetCameraTarget(building);
  }

  public void SetCameraTarget(HQsStaticBuilding inBuilding)
  {
    if (!((UnityEngine.Object) inBuilding != (UnityEngine.Object) null) || !((UnityEngine.Object) this.mSelectedBuilding != (UnityEngine.Object) inBuilding))
      return;
    this.mSelectedBuilding = inBuilding;
    this.cameraController.freeRoamCamera.SetTarget(this.mSelectedBuilding.center, CameraManager.Transition.Smooth, 0.0f);
  }

  public void ResetCameraTarget()
  {
    this.mSelectedBuilding = (HQsStaticBuilding) null;
    if (!((UnityEngine.Object) this.cameraController.freeRoamCamera.target != (UnityEngine.Object) null))
      return;
    this.cameraController.freeRoamCamera.SetTarget((GameObject) null, CameraManager.Transition.Instant, 0.0f);
  }

  private void OnCameraChangeTarget(GameObject inNewTarget)
  {
    if (!this.gameObject.activeSelf || !((UnityEngine.Object) inNewTarget == (UnityEngine.Object) null) || !((UnityEngine.Object) this.selectedBuilding != (UnityEngine.Object) null))
      return;
    this.screen.DeselectBuilding();
  }

  public HQsStaticBuilding GetBuilding(HQsBuilding_v1 inBuilding)
  {
    int count = this.mBuildings.Count;
    for (int index = 0; index < count; ++index)
    {
      HQsStaticBuilding mBuilding = this.mBuildings[index];
      if (mBuilding.sBuilding == inBuilding)
        return mBuilding;
    }
    return (HQsStaticBuilding) null;
  }

  public List<HQsStaticBuilding> GetBuildings()
  {
    this.mBuildings.Clear();
    HQsStaticBuilding[] componentsInChildren = this.gameObject.GetComponentsInChildren<HQsStaticBuilding>(true);
    for (int index = 0; index < componentsInChildren.Length; ++index)
    {
      componentsInChildren[index].OnStart();
      this.mBuildings.Add(componentsInChildren[index]);
    }
    return this.mBuildings;
  }

  public void HandleMouseSelection()
  {
    this.mHoverBuilding = this.GetMouseBuilding();
    if (!this.isDragging && InputManager.instance.GetKeyUp(KeyBinding.Name.MouseLeft) && (!this.screen.isMouseHoverUI && (UnityEngine.Object) this.mHoverBuilding != (UnityEngine.Object) null) && (this.mHoverBuilding.isClickable && this.mHoverBuilding.isActive))
      this.screen.SelectBuilding(this.mHoverBuilding);
    UIManager.instance.SetMouseCursorState(this.screen.isMouseHoverUI || !((UnityEngine.Object) this.mHoverBuilding != (UnityEngine.Object) null) || (!this.mHoverBuilding.isClickable || !this.mHoverBuilding.isActive) ? CursorManager.State.Normal : CursorManager.State.Clickable);
    if ((UnityEngine.Object) this.mSelectedBuilding == (UnityEngine.Object) null)
    {
      if ((UnityEngine.Object) this.mHoverBuilding != (UnityEngine.Object) null && this.mHoverBuilding.isClickable && this.mHoverBuilding.isHighlightable)
        this.mHoverBuilding.PreviewBuilding(true, false);
      if ((UnityEngine.Object) this.mLastBuilding != (UnityEngine.Object) null && (UnityEngine.Object) this.mLastBuilding != (UnityEngine.Object) this.mHoverBuilding && (this.mLastBuilding.isClickable && !this.mLastBuilding.isHighlightable))
        this.mLastBuilding.PreviewBuilding(false, false);
    }
    this.mLastBuilding = this.mHoverBuilding;
  }

  public HQsStaticBuilding GetMouseBuilding()
  {
    RaycastHit hitInfo;
    if (Physics.Raycast(this.cameraController.GetCamera().ScreenPointToRay(Input.mousePosition), out hitInfo))
    {
      HQsStaticBuilding component1 = hitInfo.collider.gameObject.GetComponent<HQsStaticBuilding>();
      if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
        return component1;
      HQsStaticCollider component2 = hitInfo.collider.gameObject.GetComponent<HQsStaticCollider>();
      if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
        return component2.building;
    }
    return (HQsStaticBuilding) null;
  }

  private void OnDragStart(DragInfo dragInfo)
  {
    this.mIsDragging = true;
  }

  private void OnDragEnd(DragInfo dragInfo)
  {
    this.mIsDragging = false;
  }
}
