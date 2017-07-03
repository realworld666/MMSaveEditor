// Decompiled with JetBrains decompiler
// Type: CircuitScene
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class CircuitScene : BaseScene
{
  private static int gridBoxCount = 20;
  [SerializeField]
  private Texture[] mTrackSpecificMaskTextures = new Texture[0];
  private GridBox[] mGridBoxes = new GridBox[CircuitScene.gridBoxCount];
  private Dictionary<Team, TeamPitGarage> mTeamPitGaragesByTeam = new Dictionary<Team, TeamPitGarage>();
  [SerializeField]
  private CircuitScene.PolePositionLocation polePositionLocation;
  [SerializeField]
  private GameObject[] trackLayoutGeometry;
  [SerializeField]
  private PathDataComponent[] trackLayoutPath;
  public PathDataComponent pitlanePath;
  public PathDataComponent pitlaneEntryPath;
  public PathDataComponent pitlaneExitPath;
  public PathDataComponent[] crashPaths;
  private int[] mCrashPathsTrackPathIDs;
  private int[] mCrashPathFirstIDOutsideOfTrackBounds;
  private bool[] mIsCrashPathValidForCurrentLayout;
  public int pitlaneExitTrackPathID;
  public int pitlaneEntryTrackPathID;
  public int safetyCarEndGateID;
  [SerializeField]
  private Camera practiceIntroCamera;
  [SerializeField]
  private Camera qualifyingIntroCamera;
  [SerializeField]
  private Camera raceIntroCamera;
  [SerializeField]
  private Camera practiceHUBCamera;
  [SerializeField]
  private Camera qualifyingHUBCamera;
  [SerializeField]
  private Camera raceHUBCamera;
  [SerializeField]
  private Camera postSessionCamera;
  [SerializeField]
  private Camera simulatingSessionCamera;
  [SerializeField]
  private Transform teamGarages;
  public BoxCollider trackBounds;
  [SerializeField]
  private Material crowdMaterial;
  private GameObject mGridBoxGroup;
  private Circuit.TrackLayout mTrackLayout;
  private TeamPitGarage[] mTeamPitGarages;
  private int mTeamPitGarageCount;
  private EnvironmentManager mEnvironmentManager;
  private bool mIsPathCollisionMeshActive;
  private int mPathCollisionLayerMask;
  private bool mHasLoaded3DGeometry;

  public int teamPitGarageCount
  {
    get
    {
      return this.mTeamPitGarageCount;
    }
  }

  public Circuit.TrackLayout trackLayout
  {
    get
    {
      return this.mTrackLayout;
    }
  }

  public EnvironmentManager environmentManager
  {
    get
    {
      return this.mEnvironmentManager;
    }
  }

  public Texture[] trackSpecificMaskTextures
  {
    get
    {
      return this.mTrackSpecificMaskTextures;
    }
  }

  public int[] crashPathFirstIDOutsideOfTrackBounds
  {
    get
    {
      return this.mCrashPathFirstIDOutsideOfTrackBounds;
    }
  }

  public bool isPathCollisionMeshActive
  {
    get
    {
      return this.mIsPathCollisionMeshActive;
    }
  }

  public int pathCollisionLayerMask
  {
    get
    {
      return this.mPathCollisionLayerMask;
    }
  }

  public bool hasLoaded3DGeometry
  {
    get
    {
      return this.mHasLoaded3DGeometry;
    }
  }

  private void Awake()
  {
    this.mEnvironmentManager = this.GetComponentInChildren<EnvironmentManager>(true);
    GameUtility.Assert((Object) this.mEnvironmentManager != (Object) null, "CircuitScene loaded but doesn't contain a child with an EnvironmentManager", (Object) null);
    for (int index = 0; index < this.trackLayoutPath.Length; ++index)
      GameUtility.SetActive(this.trackLayoutPath[index].gameObject, true);
    this.mIsPathCollisionMeshActive = false;
    int layer = LayerMask.NameToLayer("Env_Track");
    this.mPathCollisionLayerMask = 1 << layer;
    MeshFilter[] componentsInChildren = this.GetComponentsInChildren<MeshFilter>(true);
    if (componentsInChildren == null)
      return;
    for (int index = 0; index < componentsInChildren.Length; ++index)
    {
      GameObject gameObject = componentsInChildren[index].gameObject;
      if (gameObject.layer == layer)
      {
        this.mIsPathCollisionMeshActive = true;
        gameObject.AddComponent<MeshCollider>();
      }
    }
  }

  private void Start()
  {
    if (!((Object) App.instance != (Object) null))
      return;
    CameraManager cameraManager = App.instance.cameraManager;
    cameraManager.Start();
    cameraManager.RegisterCameraForMode(CameraManager.CameraMode.PracticeIntro, this.practiceIntroCamera);
    cameraManager.RegisterCameraForMode(CameraManager.CameraMode.QualifyingIntro, this.qualifyingIntroCamera);
    cameraManager.RegisterCameraForMode(CameraManager.CameraMode.RaceIntro, this.raceIntroCamera);
    cameraManager.RegisterCameraForMode(CameraManager.CameraMode.PracticeHUB, this.practiceHUBCamera);
    cameraManager.RegisterCameraForMode(CameraManager.CameraMode.QualifyingHUB, this.qualifyingHUBCamera);
    cameraManager.RegisterCameraForMode(CameraManager.CameraMode.RaceHUB, this.raceHUBCamera);
    cameraManager.RegisterCameraForMode(CameraManager.CameraMode.PostSession, this.postSessionCamera);
    cameraManager.RegisterCameraForMode(CameraManager.CameraMode.SimulatingSession, this.simulatingSessionCamera);
    this.InitialiseTeamPitGarages();
    foreach (UnityEngine.Behaviour componentsInChild in this.GetComponentsInChildren<Camera>(true))
      componentsInChild.enabled = false;
  }

  public void SetPathIDs()
  {
    PathController.Path path1 = new PathController.Path();
    path1.pathType = PathController.PathType.Track;
    PathController.Path path2 = new PathController.Path();
    path2.pathType = PathController.PathType.PitlaneExit;
    PathController.Path path3 = new PathController.Path();
    path3.pathType = PathController.PathType.PitlaneEntry;
    int refPreviousGateID = 0;
    PathController.CalculatePreviousAndNextGate(path1, path2.data.gates[path2.data.gates.Count - 1].position, ref refPreviousGateID, ref this.pitlaneExitTrackPathID, (Vehicle) null);
    PathController.CalculatePreviousAndNextGate(path1, path3.data.gates[0].position, ref refPreviousGateID, ref this.pitlaneEntryTrackPathID, (Vehicle) null);
    this.safetyCarEndGateID = PathUtility.WrapIndex(this.pitlaneEntryTrackPathID - 10, path1.data.gates.Count);
    if (this.crashPaths == null)
      return;
    this.mIsCrashPathValidForCurrentLayout = new bool[this.crashPaths.Length];
    this.mCrashPathsTrackPathIDs = new int[this.crashPaths.Length];
    this.mCrashPathFirstIDOutsideOfTrackBounds = new int[this.crashPaths.Length];
    for (int index = 0; index < this.crashPaths.Length; ++index)
    {
      PathDataComponent crashPath = this.crashPaths[index];
      this.mCrashPathsTrackPathIDs[index] = PathUtility.GetClosestTrackGateID(crashPath.data.GetFirstGate(), path1);
      this.mCrashPathFirstIDOutsideOfTrackBounds[index] = PathUtility.GetFirstGateIDFurtherAwayThanWidth(crashPath.data, path1);
      bool flag = true;
      if (!crashPath.data.crashLaneForAnyLayout)
        flag = crashPath.data.crashSplineForLayout == this.mTrackLayout;
      this.mIsCrashPathValidForCurrentLayout[index] = this.mCrashPathFirstIDOutsideOfTrackBounds[index] != 0 && flag;
    }
  }

  private void OnDestroy()
  {
    Object.Destroy((Object) this.mGridBoxGroup);
    this.mGridBoxGroup = (GameObject) null;
  }

  private void InitialiseTeamPitGarages()
  {
    this.mTeamPitGarageCount = this.teamGarages.childCount;
    this.mTeamPitGarages = new TeamPitGarage[this.mTeamPitGarageCount];
    for (int index = 0; index < this.mTeamPitGarageCount; ++index)
    {
      this.mTeamPitGarages[index] = this.teamGarages.GetChild(index).GetComponent<TeamPitGarage>();
      this.mTeamPitGarages[index].ValidateDirection(this);
    }
  }

  public void CalculateGridPositions()
  {
    if (!((Object) this.mGridBoxGroup == (Object) null))
      return;
    Object original = Resources.Load("Prefabs/Simulation/GridBox");
    this.mGridBoxGroup = new GameObject("GridBoxes");
    this.mGridBoxGroup.transform.SetParent(this.transform);
    float num1 = 3.75f;
    float num2 = VehicleConstants.vehicleLength * 2f;
    PathController.Path inPath = new PathController.Path();
    inPath.pathType = PathController.PathType.Track;
    PathSpline.SplinePosition outSplinePosition = new PathSpline.SplinePosition();
    Vector3 position = this.GetTrackPath().data.gates[0].position;
    PathController.UpdatePathToNearestGate(inPath, position, (Vehicle) null);
    for (int index = 0; index < CircuitScene.gridBoxCount; ++index)
    {
      this.mGridBoxes[index] = ((GameObject) Object.Instantiate(original)).GetComponent<GridBox>();
      this.mGridBoxes[index].transform.SetParent(this.mGridBoxGroup.transform);
      float num3 = this.polePositionLocation != CircuitScene.PolePositionLocation.Left ? (index % 2 != 0 ? 1f : -1f) : (index % 2 != 0 ? -1f : 1f);
      PathData.Gate gate = inPath.data.gates[inPath.previousGateId];
      position += -gate.normal * num2;
      PathController.UpdatePathToNearestGate(inPath, position, (Vehicle) null);
      inPath.data.centerLineSpline.FindSplinePositionForPoint(position, gate.centerLineStart, gate.centerLineEnd, out outSplinePosition, -1);
      this.mGridBoxes[index].Setup(outSplinePosition.position + outSplinePosition.right * num3 * num1, outSplinePosition.forward);
    }
  }

  public void SetCrowdLevels()
  {
    if (!((Object) this.crowdMaterial != (Object) null))
      return;
    if (Game.instance.sessionManager.eventDetails.currentSession.sessionType == SessionDetails.SessionType.Race)
      this.crowdMaterial.SetFloat(MaterialPropertyHashes._Density, 1f);
    else
      this.crowdMaterial.SetFloat(MaterialPropertyHashes._Density, 0.5f);
  }

  public void PrepareForSession()
  {
    this.environmentManager.PrepareForSession();
  }

  public void SimulationUpdate()
  {
    if (!this.gameObject.activeSelf)
      return;
    this.environmentManager.SimulationUpdate();
  }

  public GridBox GetGridBox(int inGridPosition)
  {
    return this.mGridBoxes[inGridPosition - 1];
  }

  public void ClearGridCrowds()
  {
    for (int index = 0; index < this.mGridBoxes.Length; ++index)
      this.mGridBoxes[index].HideCrowds();
  }

  public void ClearPitGarageTeamAssignment()
  {
    for (int index = 0; index < this.mTeamPitGarages.Length; ++index)
      this.mTeamPitGarages[index].ClearTeam();
    this.mTeamPitGaragesByTeam.Clear();
  }

  public void AssignTeamToPitGarage(int inIndex, Team inTeam)
  {
    TeamPitGarage mTeamPitGarage = this.mTeamPitGarages[inIndex];
    mTeamPitGarage.SetTeam(inTeam);
    this.mTeamPitGaragesByTeam.Add(inTeam, mTeamPitGarage);
  }

  public TeamPitGarage GetGarageForSafetyCar()
  {
    return this.mTeamPitGarages[this.mTeamPitGarages.Length - 1];
  }

  public TeamPitGarage GetTeamPitGarage(Team inTeam)
  {
    TeamPitGarage teamPitGarage;
    if (this.mTeamPitGaragesByTeam.TryGetValue(inTeam, out teamPitGarage))
      return teamPitGarage;
    Debug.LogError((object) ("Failed to find TeamPitGarage for team " + inTeam.name), (Object) null);
    return (TeamPitGarage) null;
  }

  public void SetTrackLayout(Circuit.TrackLayout inLayout)
  {
    this.mTrackLayout = inLayout;
    if (this.mTrackLayout > (Circuit.TrackLayout) this.trackLayoutPath.Length)
      this.mTrackLayout = Circuit.TrackLayout.TrackA;
    for (int index = 0; index < this.trackLayoutPath.Length; ++index)
      this.trackLayoutPath[index].gameObject.SetActive(this.mTrackLayout == (Circuit.TrackLayout) index);
    for (int index = 0; index < this.trackLayoutGeometry.Length; ++index)
    {
      if ((Object) this.trackLayoutGeometry[index] != (Object) null)
        this.trackLayoutGeometry[index].SetActive((Circuit.TrackLayout) index == this.mTrackLayout);
    }
    this.SetPathIDs();
  }

  public bool HasCrashLanes()
  {
    if (this.mIsCrashPathValidForCurrentLayout != null)
    {
      for (int index = 0; index < this.mIsCrashPathValidForCurrentLayout.Length; ++index)
      {
        if (this.mIsCrashPathValidForCurrentLayout[index])
          return true;
      }
    }
    return false;
  }

  public PathDataComponent GetRandomCrashLane(out int outIndex, Vehicle inVehicle = null, int inLaneIndex = -1)
  {
    if (inLaneIndex != -1)
    {
      outIndex = inLaneIndex;
      return this.crashPaths[inLaneIndex];
    }
    if (ConsoleCommands.getNextCrashPath)
      return this.GetClosedCrashLane(out outIndex, inVehicle);
    int num = 0;
    int random = RandomUtility.GetRandom(0, this.mCrashPathsTrackPathIDs.Length);
    PathDataComponent pathDataComponent = (PathDataComponent) null;
    for (int index = 0; index < this.mCrashPathsTrackPathIDs.Length; ++index)
    {
      if (this.mIsCrashPathValidForCurrentLayout[index])
      {
        num = index;
        pathDataComponent = this.crashPaths[index];
        if (index >= random)
          break;
      }
    }
    outIndex = num;
    return pathDataComponent;
  }

  public PathDataComponent GetClosedCrashLane(out int outIndex, Vehicle inVehicle = null)
  {
    if (inVehicle != null)
    {
      int num = int.MaxValue;
      int index1 = 0;
      for (int index2 = 0; index2 < this.mCrashPathsTrackPathIDs.Length; ++index2)
      {
        if (this.mIsCrashPathValidForCurrentLayout[index2] && num > this.mCrashPathsTrackPathIDs[index2] && this.mCrashPathsTrackPathIDs[index2] > inVehicle.pathController.GetNextGate().id)
        {
          num = this.mCrashPathsTrackPathIDs[index2];
          index1 = index2;
        }
      }
      if (num != int.MaxValue)
      {
        outIndex = index1;
        return this.crashPaths[index1];
      }
      int index3 = 0;
      for (int index2 = 0; index2 < this.mCrashPathsTrackPathIDs.Length; ++index2)
      {
        if (this.mIsCrashPathValidForCurrentLayout[index2] && this.mCrashPathsTrackPathIDs[index2] < this.mCrashPathsTrackPathIDs[index3])
          index3 = index2;
      }
      outIndex = index3;
      return this.crashPaths[index3];
    }
    Debug.LogWarning((object) "Trully random crash lane selected, vehicle might teeleport to an invalid track path", (Object) null);
    outIndex = RandomUtility.GetRandom(0, this.crashPaths.Length);
    return this.crashPaths[outIndex];
  }

  public PathDataComponent GetTrackPath()
  {
    return this.trackLayoutPath[(int) this.mTrackLayout];
  }

  public override void SetEnvPreference(Preference.pName inName, bool inValue)
  {
    if (inName == Preference.pName.Video_DynamicLights)
    {
      if (!((Object) this.mEnvironmentManager != (Object) null))
        return;
      this.mEnvironmentManager.SetLightQuality(!inValue ? EnvironmentManager.LightOptions.Off : EnvironmentManager.LightOptions.On);
    }
    else
      base.SetEnvPreference(inName, inValue);
  }

  public void SetHasLoaded3DGeometry(bool inLoaded)
  {
    this.mHasLoaded3DGeometry = inLoaded;
  }

  private enum PolePositionLocation
  {
    Left,
    Right,
  }
}
