// Decompiled with JetBrains decompiler
// Type: MinimapWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class MinimapWidget : MonoBehaviour
{
  private List<RacingVehicle> vehicles = new List<RacingVehicle>();
  private Dictionary<Vehicle, MinimapVehicleEntry> vehicleEntries = new Dictionary<Vehicle, MinimapVehicleEntry>();
  private List<MinimapDriverEntry> myDrivers = new List<MinimapDriverEntry>();
  private List<MinimapDriverEntry> driverMinimapEntry = new List<MinimapDriverEntry>();
  private List<int> mPlayerVehiclePositions = new List<int>(2);
  private Vector2 mTrackSize = Vector2.zero;
  private Vector2 mTrackOffset = Vector2.zero;
  private Vector2 mMinimapSize = Vector2.zero;
  private Vector2 mMinimapMaxSize = Vector2.zero;
  private Vector2 mMinimapBounds = Vector2.zero;
  public MinimapOptions options;
  public RectTransform minimapBounds;
  public RectTransform minimapMaxSize;
  public RectTransform minimap;
  public RectTransform minimapArea;
  public UITrackLayout trackLayout;
  public UIGridList driverGrid;
  public RectTransform itemCamera;
  public RectTransform itemFinishLine;
  public RectTransform[] itemSectors;
  public GameObject itemDriver;
  public GameObject itemRival;
  public MinimapVehicleEntry safetyVehicleMinimapEntry;
  public bool isSimulation2D;
  public bool hideCamera;
  private SafetyVehicle safetyVehicle;
  private SessionDetails mSessionDetails;
  private Circuit mCircuit;
  private CircuitScene mCircuitScene;
  private bool mReady;

  public bool loaded
  {
    get
    {
      return this.mReady;
    }
  }

  public void Setup()
  {
    CircuitScene circuit = Game.instance.sessionManager.circuit;
    if ((UnityEngine.Object) circuit != (UnityEngine.Object) null && (UnityEngine.Object) circuit.trackBounds != (UnityEngine.Object) null)
    {
      GameUtility.SetActive(this.gameObject, true);
      SessionDetails currentSession = Game.instance.sessionManager.eventDetails.currentSession;
      if (this.mSessionDetails != currentSession || (UnityEngine.Object) this.mCircuitScene != (UnityEngine.Object) circuit)
      {
        this.mCircuitScene = circuit;
        this.mCircuit = Game.instance.sessionManager.eventDetails.circuit;
        this.mSessionDetails = currentSession;
        this.UnloadMinimap();
      }
      this.LoadMinimap();
      this.SetTrackDimensions();
      this.SetOtherObjects();
      this.SetVehicles();
      this.SetGrid();
      this.SetVehicleEntries();
      this.UpdateDriverPositions();
    }
    else
      GameUtility.SetActive(this.gameObject, false);
  }

  public void SetPositionAndSize(Vector2 inPosition, Vector2 inSize)
  {
    this.mMinimapSize = inSize;
    this.minimap.sizeDelta = this.mMinimapSize;
    this.SetOtherObjects();
    this.minimap.parent.transform.localPosition = (Vector3) inPosition;
  }

  private void Update()
  {
    this.RefreshWidget();
  }

  public void RefreshWidget()
  {
    if (!this.mReady)
      return;
    GameUtility.SetActive(this.itemFinishLine.gameObject, this.options.displayFinishLine);
    GameUtility.SetActive(this.itemCamera.gameObject, this.options.displayGameCamera && !this.hideCamera);
    GameUtility.SetActive(this.itemSectors[0].gameObject, this.options.displaySectors);
    GameUtility.SetActive(this.itemSectors[1].gameObject, this.options.displaySectors);
    this.UpdateDriverPositions();
    this.UpdateCameraPosition();
    this.UpdateSafetyCar();
    this.ScaleItems();
  }

  private void ScaleItems()
  {
    if (!this.isSimulation2D)
      return;
    Vector3 one = Vector3.one;
    one.x = Mathf.Lerp(0.5f, 1f, 1f - Simulation2D.instance.camera2D.zoomNormalized);
    one.y = one.x;
    this.itemFinishLine.transform.localScale = one * Simulation2D.instance.scale;
  }

  public void UnloadMinimap()
  {
    this.vehicles.Clear();
    this.driverMinimapEntry.Clear();
    this.driverGrid.DestroyListItems();
  }

  public void LoadMinimap()
  {
    this.trackLayout.SetCircuitIcon(this.mCircuit);
    this.mMinimapMaxSize = this.minimapMaxSize.sizeDelta;
    if ((UnityEngine.Object) this.trackLayout.sprite != (UnityEngine.Object) null)
    {
      this.mMinimapSize = this.GetMinimapDimensions(this.trackLayout.sprite.rect.width, this.trackLayout.sprite.rect.height, this.mMinimapMaxSize);
      this.mMinimapBounds.Set((float) (((double) this.minimapBounds.rect.width - (double) this.mMinimapSize.x) / 2.0), (float) (((double) this.minimapBounds.rect.height - (double) this.mMinimapSize.y) / 2.0));
      GameUtility.SetActive(this.minimapBounds.gameObject, false);
      this.minimap.sizeDelta = this.mMinimapSize;
      this.minimapArea.sizeDelta = this.mMinimapSize;
    }
    else
      Debug.LogErrorFormat("Minimap couldn't be loaded, wrong minimap sprite name, should be - Minimap_{0}{1}", new object[2]
      {
        (object) this.mCircuit.spriteName,
        (object) this.mCircuit.GetTrackVariation()
      });
  }

  public void SetTrackDimensions()
  {
    Bounds bounds = this.mCircuitScene.trackBounds.bounds;
    if (!this.mCircuit.minimapAxisX)
    {
      this.mTrackSize.Set(bounds.size.z, bounds.size.x);
      this.mTrackOffset.Set(bounds.center.z, bounds.center.x);
    }
    else
    {
      this.mTrackSize.Set(bounds.size.x, bounds.size.z);
      this.mTrackOffset.Set(bounds.center.x, bounds.center.z);
    }
  }

  public void SetOtherObjects()
  {
    List<PathData.Gate> gates = this.mCircuitScene.GetTrackPath().data.gates;
    List<PathData.Gate> gateList = new List<PathData.Gate>();
    GameUtility.SetActive(this.itemFinishLine.gameObject, this.options.displayFinishLine);
    if (this.options.displayFinishLine)
    {
      PathData.Gate gate1 = gates[0];
      PathData.Gate gate2 = gates[1];
      float angle = this.GetAngle(new Vector2(gate1.position.x, gate1.position.z), new Vector2(gate2.position.x, gate2.position.z));
      this.itemFinishLine.anchoredPosition = this.CalculateItemPosition(gate1.position);
      this.itemFinishLine.transform.eulerAngles = new Vector3(0.0f, 0.0f, angle + (float) this.mCircuit.minimapRotation);
    }
    GameUtility.SetActive(this.itemCamera.gameObject, this.options.displayGameCamera && !this.hideCamera);
    this.UpdateCameraPosition();
    GameUtility.SetActive(this.itemSectors[0].gameObject, this.options.displaySectors);
    GameUtility.SetActive(this.itemSectors[1].gameObject, this.options.displaySectors);
    if (!this.options.displaySectors)
      return;
    for (int index = 0; index < gates.Count; ++index)
    {
      if (gates[index].gateType == PathData.GateType.Sector)
        gateList.Add(gates[index]);
    }
    if (gateList.Count < 3)
      return;
    this.itemSectors[0].anchoredPosition = this.CalculateItemPosition(gateList[1].position);
    this.itemSectors[1].anchoredPosition = this.CalculateItemPosition(gateList[2].position);
  }

  public void SetVehicles()
  {
    if (this.vehicles.Count == 0)
      this.vehicles = new List<RacingVehicle>((IEnumerable<RacingVehicle>) Game.instance.sessionManager.standings);
    this.safetyVehicle = Game.instance.vehicleManager.safetyVehicle;
    this.safetyVehicleMinimapEntry.Setup((Vehicle) this.safetyVehicle, MinimapOptions.ChangeColor.None);
  }

  public void HideAllVehicles()
  {
    for (int index = 0; index < this.vehicles.Count; ++index)
      this.options.DisplayVehicle((Vehicle) this.vehicles[index], false);
  }

  public void ShowAllVehicles()
  {
    for (int index = 0; index < this.vehicles.Count; ++index)
      this.options.DisplayVehicle((Vehicle) this.vehicles[index], true);
    this.options.displayGameCamera = false;
  }

  public void ShowOnlyPlayerVehicles()
  {
    this.HideAllVehicles();
    for (int index = 0; index < this.vehicles.Count; ++index)
    {
      RacingVehicle vehicle = this.vehicles[index];
      if (vehicle.driver.IsPlayersDriver())
        this.options.DisplayVehicle((Vehicle) vehicle, true);
    }
    this.options.displayGameCamera = false;
  }

  public void ShowPlayerAndRivalVehicles()
  {
    this.HideAllVehicles();
    this.mPlayerVehiclePositions.Clear();
    for (int index = 0; index < this.vehicles.Count; ++index)
    {
      RacingVehicle vehicle = this.vehicles[index];
      if (vehicle.driver.IsPlayersDriver())
      {
        this.mPlayerVehiclePositions.Add(vehicle.standingsPosition);
        this.options.DisplayVehicle((Vehicle) vehicle, true);
      }
    }
    for (int index1 = 0; index1 < this.vehicles.Count; ++index1)
    {
      RacingVehicle vehicle = this.vehicles[index1];
      for (int index2 = 0; index2 < this.mPlayerVehiclePositions.Count; ++index2)
      {
        if (Mathf.Abs(vehicle.standingsPosition - this.mPlayerVehiclePositions[index2]) == 1)
          this.options.DisplayVehicle((Vehicle) vehicle, true);
      }
    }
    this.options.displayGameCamera = false;
  }

  public void SetVehicleEntries()
  {
    this.vehicleEntries.Clear();
    int count = this.driverMinimapEntry.Count;
    for (int index = 0; index < count; ++index)
    {
      MinimapDriverEntry minimapDriverEntry = this.driverMinimapEntry[index];
      this.vehicleEntries.Add(minimapDriverEntry.vehicle, (MinimapVehicleEntry) minimapDriverEntry);
    }
    this.vehicleEntries.Add((Vehicle) this.safetyVehicle, this.safetyVehicleMinimapEntry);
  }

  public void SetGrid()
  {
    this.myDrivers.Clear();
    int count = this.vehicles.Count;
    int itemCount = this.driverGrid.itemCount;
    if (count - itemCount != 0)
    {
      this.driverMinimapEntry.Clear();
      this.driverGrid.DestroyListItems();
      GameUtility.SetActive(this.itemDriver, true);
      GameUtility.SetActive(this.itemRival, true);
      for (int index = 0; index < count; ++index)
      {
        RacingVehicle vehicle = this.vehicles[index];
        this.driverGrid.itemPrefab = !vehicle.driver.IsPlayersDriver() ? this.itemRival : this.itemDriver;
        MinimapDriverEntry listItem = this.driverGrid.CreateListItem<MinimapDriverEntry>();
        listItem.gameObject.name = "Minimap-" + vehicle.driver.shortName;
        listItem.transform.SetAsLastSibling();
        if ((UnityEngine.Object) this.driverGrid.itemPrefab == (UnityEngine.Object) this.itemDriver)
          this.myDrivers.Add(listItem);
        listItem.OnStart();
        listItem.Setup((Vehicle) vehicle, this.options.driverColors);
        this.driverMinimapEntry.Add(listItem);
      }
      GameUtility.SetActive(this.itemDriver, false);
      GameUtility.SetActive(this.itemRival, false);
    }
    for (int index = 0; index < this.myDrivers.Count; ++index)
      this.myDrivers[index].transform.SetAsLastSibling();
    this.safetyVehicleMinimapEntry.OnStart();
    this.mReady = true;
  }

  public void UpdateDriverPositions()
  {
    int count1 = this.driverMinimapEntry.Count;
    int count2 = this.vehicles.Count;
    for (int index = 0; index < count2; ++index)
    {
      if (index < count1)
        this.UpdateDriverPosition(this.vehicles[index], this.driverMinimapEntry[index]);
    }
  }

  private void UpdateSafetyCar()
  {
    this.safetyVehicleMinimapEntry.SetPosition(this.CalculateItemPosition(this.safetyVehicle.transform.position), 0, Game.instance.sessionManager.flag == SessionManager.Flag.SafetyCar, false, this.options.rotateVehicleLabelsWithCamera, MinimapOptions.ChangeColor.None);
  }

  public void UpdateDriverPosition(RacingVehicle inVehicle, MinimapDriverEntry inEntry)
  {
    Vector2 itemPosition = this.CalculateItemPosition(inVehicle.transform.position);
    bool flag = inVehicle.driver.IsPlayersDriver();
    bool inDisplay = true;
    if (this.options.IsVehicleDisplayed((Vehicle) inVehicle))
    {
      if (this.isSimulation2D)
      {
        if (inVehicle.pathState.pathStateGroup == PathStateManager.PathStateGroup.InGarage)
          inDisplay = false;
      }
      else if (inVehicle.pathState.pathStateGroup == PathStateManager.PathStateGroup.InGarage || inVehicle.timer.hasSeenChequeredFlag || inVehicle.behaviourManager.currentBehaviour.behaviourType == AIBehaviourStateManager.Behaviour.Crashed)
        inDisplay = false;
      if (!this.options.displayPlayerDrivers && flag)
        inDisplay = false;
      else if (!this.options.displayOtherDrivers && !flag)
        inDisplay = false;
    }
    else
      inDisplay = false;
    inEntry.SetPosition(itemPosition, inVehicle.standingsPosition, inDisplay, this.options.displayDriverNamesAlways, this.options.rotateVehicleLabelsWithCamera, this.options.driverColors);
  }

  public void UpdateCameraPosition()
  {
    if (!this.options.displayGameCamera)
      return;
    bool flag = !App.instance.preferencesManager.videoPreferences.isSimulationRunning2DMode;
    Camera camera = !flag ? Simulation2D.instance.camera : App.instance.cameraManager.GetCamera();
    GameUtility.SetActive(this.itemCamera.gameObject, (UnityEngine.Object) camera != (UnityEngine.Object) null && !this.hideCamera);
    if (!this.itemCamera.gameObject.activeSelf)
      return;
    Vector3 vector3 = !flag ? Simulation2D.instance.camera2D.target.transform.position : camera.transform.position;
    Vector3 eulerAngles = camera.transform.rotation.eulerAngles;
    this.itemCamera.anchoredPosition = this.CalculateCameraPosition(!flag ? this.GetCameraOffset2D(Simulation2D.instance.camera2D.target, eulerAngles.z + 90f + (float) this.mCircuit.minimapRotation) : vector3);
    if (flag)
      this.itemCamera.transform.rotation = Quaternion.Euler(0.0f, 0.0f, (float) (360.0 - (double) eulerAngles.y + 90.0) + (float) this.mCircuit.minimapRotation);
    else
      this.itemCamera.transform.rotation = Quaternion.Euler(0.0f, 0.0f, eulerAngles.z + 90f + (float) this.mCircuit.minimapRotation);
  }

  public Vector3 GetCameraOffset2D(Vehicle inVehicle, float inAgle)
  {
    float num1 = -120f * Mathf.Cos(inAgle * ((float) Math.PI / 180f));
    float num2 = -120f * Mathf.Sin(inAgle * ((float) Math.PI / 180f));
    Vector3 position = inVehicle.transform.position;
    position.x += num1;
    position.z += num2;
    return position;
  }

  public Vector2 CalculateItemPosition(Vector3 inWorldPosition)
  {
    Vector2 zero = Vector2.zero;
    if (this.mCircuit.minimapRotation != 0)
    {
      if (this.mCircuit.minimapRotation != 90 && this.mCircuit.minimapRotation != 180 && this.mCircuit.minimapRotation == 270)
      {
        inWorldPosition = this.GetMinimapRotation() * inWorldPosition;
        zero.Set((float) ((double) inWorldPosition.x + (double) this.mTrackSize.x / 2.0 + -(double) this.mTrackOffset.x), (float) ((double) inWorldPosition.z + (double) this.mTrackSize.y / 2.0 + (double) this.mTrackOffset.y * 1.0));
      }
    }
    else
      zero.Set((float) ((double) inWorldPosition.x - (double) this.mTrackOffset.x + (double) this.mTrackSize.x / 2.0), (float) ((double) inWorldPosition.z - (double) this.mTrackOffset.y + (double) this.mTrackSize.y / 2.0));
    Vector2 vector2_1 = new Vector2(zero.x / this.mTrackSize.x, zero.y / this.mTrackSize.y);
    Vector2 vector2_2 = new Vector2(vector2_1.x * this.mMinimapSize.x, vector2_1.y * this.mMinimapSize.y);
    return new Vector2(vector2_2.x, -this.mMinimapSize.y + vector2_2.y);
  }

  public Vector2 CalculateCameraPosition(Vector3 inCameraPosition)
  {
    Vector2 itemPosition = this.CalculateItemPosition(inCameraPosition);
    return new Vector2(Mathf.Clamp(itemPosition.x, -this.mMinimapBounds.x, this.mMinimapSize.x + this.mMinimapBounds.x), Mathf.Clamp(itemPosition.y, -this.mMinimapSize.y - this.mMinimapBounds.y, this.mMinimapBounds.y));
  }

  public Vector2 GetMinimapDimensions(float inWidth, float inHeight, Vector2 inBounds)
  {
    Vector2 vector2 = new Vector2(inWidth, inHeight);
    float num = Mathf.Min(inBounds.x / inWidth, inBounds.y / inHeight);
    vector2 = new Vector2(inWidth * num, inHeight * num);
    return vector2;
  }

  private float GetAngle(Vector2 inPositionA, Vector2 inPositionB)
  {
    Vector2 vector2 = inPositionB - inPositionA;
    return Mathf.Atan2(vector2.y, vector2.x) * 57.29578f;
  }

  private Quaternion GetMinimapRotation()
  {
    if (this.mCircuit.minimapRotation == 90)
      return Quaternion.Euler(0.0f, 270f, 180f);
    if (this.mCircuit.minimapRotation == 180)
      return Quaternion.Euler(0.0f, 90f, 180f);
    if (this.mCircuit.minimapRotation == 270)
      return Quaternion.Euler(0.0f, 90f, 0.0f);
    return Quaternion.Euler(0.0f, 0.0f, 0.0f);
  }

  public Vector3 GetWorldPositionOfEntry(Vehicle inVehicle)
  {
    if (this.vehicleEntries.ContainsKey(inVehicle))
      return this.vehicleEntries[inVehicle].transform.position;
    return Vector3.zero;
  }
}
