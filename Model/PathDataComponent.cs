// Decompiled with JetBrains decompiler
// Type: PathDataComponent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class PathDataComponent : MonoBehaviour
{
  [SerializeField]
  private PathData mTrackData = new PathData();
  private int mBuildNum = -1;
  private bool mDebugDrawTrackEdges = true;
  private bool mDebugDrawRacingLine = true;
  private bool mDebugDrawGates = true;
  public bool debugDrawInPlayMode;

  public PathData data
  {
    get
    {
      return this.mTrackData;
    }
  }

  public bool debugDrawTrackEdges
  {
    get
    {
      return this.mDebugDrawTrackEdges;
    }
    set
    {
      this.mDebugDrawTrackEdges = value;
    }
  }

  public bool debugDrawRacingLine
  {
    get
    {
      return this.mDebugDrawRacingLine;
    }
    set
    {
      this.mDebugDrawRacingLine = value;
    }
  }

  public bool debugDrawGates
  {
    get
    {
      return this.mDebugDrawGates;
    }
    set
    {
      this.mDebugDrawGates = value;
    }
  }

  private void Awake()
  {
    this.Prepare();
    if (!Application.isPlaying)
      return;
    Renderer component = this.GetComponent<Renderer>();
    if (!((Object) component != (Object) null))
      return;
    component.enabled = false;
  }

  public void Prepare()
  {
    this.mTrackData.Prepare(this.transform, this.data.type == PathData.Type.Loop || this.name.Contains("PitlaneEntry") || this.name.Contains("PitlaneExit"));
  }

  private void OnDrawGizmosSelected()
  {
  }

  private void OnDrawGizmos()
  {
    if (!this.gameObject.activeSelf)
      return;
    this.DrawGizmos();
  }

  private void DrawGizmos()
  {
    if (!this.gameObject.activeSelf)
      return;
    Gizmos.matrix = this.transform.localToWorldMatrix;
    if (this.mBuildNum != this.mTrackData.buildNum)
    {
      this.mTrackData.Build();
      this.mBuildNum = this.mTrackData.buildNum;
    }
    PathData.SplineMode splineMode = this.mTrackData.splineMode;
    bool[] flagArray = new bool[2]{ false, (this.mDebugDrawRacingLine ? 1 : 0) != 0 };
    Color[] colorArray = new Color[2]{ Color.gray, Color.yellow };
    for (int index1 = 0; index1 < 2; ++index1)
    {
      if (flagArray[index1])
      {
        this.mTrackData.SetSplineMode((PathData.SplineMode) index1);
        if (this.mTrackData.divisions != null)
        {
          Gizmos.color = colorArray[index1];
          Vector3 from = Vector3.zero;
          Vector3 zero = Vector3.zero;
          for (int index2 = 0; index2 < this.mTrackData.divisions.Length; ++index2)
          {
            Vector3 division = this.mTrackData.divisions[index2];
            if (index2 != 0)
              Gizmos.DrawLine(from, division);
            from = division;
          }
        }
      }
    }
    this.mTrackData.SetSplineMode(splineMode);
    Gizmos.matrix = Matrix4x4.identity;
    this.DrawGates();
    this.DrawRuntimeRacingLine();
  }

  private void DrawGates()
  {
    if (!this.mDebugDrawGates)
      return;
    List<PathData.Gate> gates = this.mTrackData.gates;
    int count = gates.Count;
    for (int index = 0; index < count; ++index)
    {
      Vector3 from = gates[index].position + gates[index].tangent * this.mTrackData.width;
      Vector3 to = gates[index].position + -gates[index].tangent * this.mTrackData.width;
      if (gates[index].isLockUpGate)
      {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(gates[index].position, 4f);
      }
      switch (gates[index].gateType)
      {
        case PathData.GateType.Sector:
          Gizmos.color = Color.magenta;
          break;
        case PathData.GateType.BrakingZone:
          Gizmos.color = Color.black;
          break;
        case PathData.GateType.AccelerationZone:
          Gizmos.color = Color.cyan;
          break;
        default:
          Gizmos.color = Color.green;
          break;
      }
      Gizmos.DrawLine(from, to);
      Gizmos.DrawLine(gates[index].position, gates[index].position + gates[index].normal * 2f);
    }
  }

  private void DrawRuntimeRacingLine()
  {
  }
}
