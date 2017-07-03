// Decompiled with JetBrains decompiler
// Type: TeamPitGarage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class TeamPitGarage : MonoBehaviour
{
  [SerializeField]
  private TeamPitGarage.Direction direction = TeamPitGarage.Direction.AntiClockwise;
  private TeamPitGarage.Direction mDirection = TeamPitGarage.Direction.AntiClockwise;
  public PathDataComponent pitboxEntryPath;
  public PathDataComponent pitboxExitPath;
  [SerializeField]
  private PathDataComponent[] garageEntryPath;
  [SerializeField]
  private PathDataComponent[] garageExitPath;
  private TeamPitCrew mPitCrew;

  public TeamPitCrew pitCrew
  {
    get
    {
      return this.mPitCrew;
    }
  }

  private void Awake()
  {
    this.mDirection = this.direction;
    this.mPitCrew = this.gameObject.AddComponent<TeamPitCrew>();
  }

  public PathDataComponent GetGarageEntryForVehicle(Vehicle inVehicle)
  {
    if (inVehicle is RacingVehicle)
      return this.garageEntryPath[((RacingVehicle) inVehicle).carID];
    return this.garageEntryPath[0];
  }

  public PathDataComponent GetGarageExitForVehicle(Vehicle inVehicle)
  {
    if (inVehicle is RacingVehicle)
      return this.garageExitPath[((RacingVehicle) inVehicle).carID];
    return this.garageExitPath[0];
  }

  public void SetTeam(Team inTeam)
  {
    this.mPitCrew.SetTeam(inTeam);
  }

  public void ClearTeam()
  {
    this.mPitCrew.ClearTeam();
  }

  private void OnValidate()
  {
    if (Application.isPlaying || this.mDirection == this.direction)
      return;
    this.ChangeDirection();
  }

  public void ValidateDirection(CircuitScene inCircuitScene)
  {
    GameUtility.Assert(this.pitboxEntryPath.data.gates.Count > 0, "pitboxEntryPath.data.gates.Count > 0", (Object) this);
    if ((double) Vector3.Angle(inCircuitScene.pitlanePath.data.gates[0].normal, this.pitboxEntryPath.data.GetLastGate().normal) <= 90.0)
      return;
    this.ChangeDirection();
  }

  private void ChangeDirection()
  {
    List<PathData.Gate> inGates1 = new List<PathData.Gate>((IEnumerable<PathData.Gate>) this.pitboxEntryPath.data.gates.ToArray());
    List<PathData.Gate> inGates2 = new List<PathData.Gate>((IEnumerable<PathData.Gate>) this.pitboxExitPath.data.gates.ToArray());
    PathDataComponent pitboxEntryPath = this.pitboxEntryPath;
    this.pitboxEntryPath = this.pitboxExitPath;
    this.pitboxExitPath = pitboxEntryPath;
    this.pitboxEntryPath.gameObject.name = "PitboxEntry";
    this.pitboxExitPath.gameObject.name = "PitboxExit";
    this.ReversePathDirection(this.pitboxEntryPath.data);
    this.ReversePathDirection(this.pitboxExitPath.data);
    this.SwapPathGates(this.pitboxEntryPath.data, inGates1);
    this.SwapPathGates(this.pitboxExitPath.data, inGates2);
    for (int index = 0; index < 2; ++index)
    {
      List<PathData.Gate> inGates3 = new List<PathData.Gate>((IEnumerable<PathData.Gate>) this.garageEntryPath[index].data.gates.ToArray());
      List<PathData.Gate> inGates4 = new List<PathData.Gate>((IEnumerable<PathData.Gate>) this.garageExitPath[index].data.gates.ToArray());
      PathDataComponent pathDataComponent = this.garageEntryPath[index];
      this.garageEntryPath[index] = this.garageExitPath[index];
      this.garageExitPath[index] = pathDataComponent;
      this.garageEntryPath[index].gameObject.name = "GarageEntry " + (index + 1).ToString();
      this.garageExitPath[index].gameObject.name = "GarageExit " + (index + 1).ToString();
      this.ReversePathDirection(this.garageEntryPath[index].data);
      this.ReversePathDirection(this.garageExitPath[index].data);
      this.SwapPathGates(this.garageEntryPath[index].data, inGates3);
      this.SwapPathGates(this.garageExitPath[index].data, inGates4);
    }
    this.pitboxEntryPath.Prepare();
    this.pitboxExitPath.Prepare();
    this.garageEntryPath[0].Prepare();
    this.garageEntryPath[1].Prepare();
    this.garageExitPath[0].Prepare();
    this.garageExitPath[1].Prepare();
    this.mDirection = this.direction;
  }

  private void ReversePathDirection(PathData inData)
  {
    PathData.SplineMode splineMode = inData.splineMode;
    for (int index1 = 0; index1 < 2; ++index1)
    {
      inData.SetSplineMode((PathData.SplineMode) index1);
      if (inData.points.Count > 0)
      {
        Vector3[] vector3Array = new Vector3[inData.points.Count];
        for (int index2 = 0; index2 < vector3Array.Length; ++index2)
          vector3Array[index2] = inData.points[index2];
        inData.RemoveAllPoints();
        if (inData.type == PathData.Type.Loop)
        {
          inData.AppendPoint(vector3Array[0]);
          for (int index2 = vector3Array.Length - 1; index2 > 0; --index2)
            inData.AppendPoint(vector3Array[index2]);
        }
        else
        {
          for (int index2 = vector3Array.Length - 1; index2 >= 0; --index2)
            inData.AppendPoint(vector3Array[index2]);
        }
      }
      inData.Build();
    }
    inData.SetSplineMode(splineMode);
  }

  private void SwapPathGates(PathData inData, List<PathData.Gate> inGates)
  {
    int count = inData.gates.Count;
    for (int index = 0; index < count; ++index)
    {
      if (index < inGates.Count)
        inData.gates[index].gateType = inGates[index].gateType;
    }
  }

  public enum Direction
  {
    Clockwise,
    AntiClockwise,
  }
}
