// Decompiled with JetBrains decompiler
// Type: PathData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PathData
{
  [SerializeField]
  private bool mCrashLaneForAnyLayout = true;
  [SerializeField]
  private PathData.GateParticleType[] mGateParticleType = new PathData.GateParticleType[0];
  [SerializeField]
  private PathData.SplineData[] mSplineData = new PathData.SplineData[2];
  [SerializeField]
  private float mPrecompDiv = 1f;
  [SerializeField]
  private int mStepCount = 32;
  [NonSerialized]
  private float mPathWidth = 7f;
  [NonSerialized]
  private List<PathData.Gate> mGates = new List<PathData.Gate>();
  [NonSerialized]
  private List<PathData.Corner> mCorners = new List<PathData.Corner>();
  [NonSerialized]
  private List<PathData.Straight> mStraights = new List<PathData.Straight>();
  [SerializeField]
  private bool mIsCrashLane;
  [SerializeField]
  private Circuit.TrackLayout mCrashSplineForLayout;
  [SerializeField]
  private bool mCrashHasMaxRotation;
  [SerializeField]
  private float mCrashMaxRotationValue;
  [SerializeField]
  private bool mCrashHasMinRotation;
  [SerializeField]
  private float mCrashMinRotationValue;
  [SerializeField]
  private bool mSetParticleTypePerGate;
  [SerializeField]
  private PathData.Type mTrackType;
  private PathData.SplineMode mSplineMode;
  private int mBuildNum;
  [NonSerialized]
  private Transform mTransform;
  [NonSerialized]
  private PathSpline mCenterLineSpline;
  [NonSerialized]
  private PathSpline mRacingLineSpline;

  public float width
  {
    get
    {
      return this.mPathWidth;
    }
  }

  public List<Vector3> points
  {
    get
    {
      return this.mSplineData[(int) this.mSplineMode].points;
    }
    set
    {
      this.mSplineData[(int) this.mSplineMode].points = value;
    }
  }

  public Vector3[] divisions
  {
    get
    {
      return this.mSplineData[(int) this.mSplineMode].divisions;
    }
    set
    {
      this.mSplineData[(int) this.mSplineMode].divisions = value;
    }
  }

  public PathData.Segment[] segments
  {
    get
    {
      return this.mSplineData[(int) this.mSplineMode].segments;
    }
    set
    {
      this.mSplineData[(int) this.mSplineMode].segments = value;
    }
  }

  public float length
  {
    get
    {
      return this.mSplineData[(int) this.mSplineMode].length;
    }
    set
    {
      this.mSplineData[(int) this.mSplineMode].length = value;
    }
  }

  public PathData.SplineData layout
  {
    get
    {
      return this.mSplineData[0];
    }
  }

  private PathData.SplineData racingLineData
  {
    get
    {
      return this.mSplineData[1];
    }
  }

  public List<PathData.Gate> gates
  {
    get
    {
      return this.mGates;
    }
  }

  public List<PathData.Corner> corners
  {
    get
    {
      return this.mCorners;
    }
  }

  public List<PathData.Straight> straights
  {
    get
    {
      return this.mStraights;
    }
  }

  public int buildNum
  {
    get
    {
      return this.mBuildNum;
    }
  }

  public PathData.Type type
  {
    get
    {
      return this.mTrackType;
    }
    set
    {
      this.mTrackType = value;
    }
  }

  public PathData.SplineMode splineMode
  {
    get
    {
      return this.mSplineMode;
    }
  }

  public Transform transform
  {
    get
    {
      return this.mTransform;
    }
  }

  public PathSpline centerLineSpline
  {
    get
    {
      return this.mCenterLineSpline;
    }
  }

  public PathSpline racingLineSpline
  {
    get
    {
      return this.mRacingLineSpline;
    }
  }

  public bool crashHasMaxRotation
  {
    get
    {
      return this.mCrashHasMaxRotation;
    }
    set
    {
      this.mCrashHasMaxRotation = value;
    }
  }

  public float crashMaxRotationValue
  {
    get
    {
      return this.mCrashMaxRotationValue;
    }
    set
    {
      this.mCrashMaxRotationValue = value;
    }
  }

  public Circuit.TrackLayout crashSplineForLayout
  {
    get
    {
      return this.mCrashSplineForLayout;
    }
    set
    {
      this.mCrashSplineForLayout = value;
    }
  }

  public bool crashLaneForAnyLayout
  {
    get
    {
      return this.mCrashLaneForAnyLayout;
    }
    set
    {
      this.mCrashLaneForAnyLayout = value;
    }
  }

  public bool isCrashLane
  {
    get
    {
      return this.mIsCrashLane;
    }
    set
    {
      this.mIsCrashLane = value;
    }
  }

  public float crashMinRotationValue
  {
    get
    {
      return this.mCrashMinRotationValue;
    }
    set
    {
      this.mCrashMinRotationValue = value;
    }
  }

  public bool crashHasMinRotation
  {
    get
    {
      return this.mCrashHasMinRotation;
    }
    set
    {
      this.mCrashHasMinRotation = value;
    }
  }

  public bool setParticleTypePerGate
  {
    get
    {
      return this.mSetParticleTypePerGate;
    }
    set
    {
      this.mSetParticleTypePerGate = value;
    }
  }

  public PathData.GateParticleType[] gateParticleType
  {
    get
    {
      return this.mGateParticleType;
    }
    set
    {
      this.mGateParticleType = value;
    }
  }

  public PathData()
  {
    for (int index = 0; index < this.mSplineData.Length; ++index)
      this.mSplineData[index] = new PathData.SplineData();
  }

  public void Prepare(Transform inTransform, bool inCalculateCorners)
  {
    this.mTransform = inTransform;
    this.mPathWidth = this.mTrackType != PathData.Type.Loop ? 3.5f : 7f;
    if (!((UnityEngine.Object) this.mTransform != (UnityEngine.Object) null) || this.racingLineData.divisions == null || (this.racingLineData.divisions.Length <= 0 || this.racingLineData.points.Count < 3))
      return;
    this.CalculateGates();
    this.CalculateRuntimeSplines();
    if (!inCalculateCorners)
      return;
    this.CalculateCorners(7f);
    this.CalculateStraights();
    this.CalculateLockUpZones();
    this.CalculateGateDistanceToBrakingGates();
  }

  public void CalculateGates()
  {
    if (this.layout.points == null || this.layout.points.Count < 2)
      return;
    List<Vector3> vector3List = new List<Vector3>((IEnumerable<Vector3>) this.layout.points);
    for (int index = 0; index < vector3List.Count; ++index)
      vector3List[index] = this.mTransform.localToWorldMatrix.MultiplyPoint3x4(vector3List[index]);
    int inParametrizationCount = Mathf.Max(Mathf.FloorToInt(this.layout.length / 15f), 8);
    PathSpline pathSpline = new PathSpline(vector3List.ToArray(), this.type, inParametrizationCount);
    this.mGates = new List<PathData.Gate>();
    int num1 = inParametrizationCount / 3;
    int num2 = inParametrizationCount / 3 * 2;
    for (int index = 0; index < pathSpline.splinePositions.Length; ++index)
    {
      if (this.type == PathData.Type.Line || this.type == PathData.Type.Loop && index < pathSpline.splinePositions.Length - 1)
      {
        PathData.Gate gate = new PathData.Gate();
        gate.id = index;
        PathSpline.SplinePosition splinePosition = pathSpline.splinePositions[index];
        gate.position = splinePosition.position;
        gate.unblendedNormal = splinePosition.forward;
        gate.unblendedTangent = splinePosition.right;
        gate.gateType = this.mTrackType != PathData.Type.Loop || index != 0 && index != num1 && index != num2 ? PathData.GateType.Normal : PathData.GateType.Sector;
        this.mGates.Add(gate);
      }
    }
    int count = this.mGates.Count;
    for (int idx = 0; idx < count; ++idx)
    {
      int index1;
      int index2;
      int index3;
      if (this.mTrackType == PathData.Type.Loop)
      {
        index1 = PathUtility.WrapIndex(idx, count);
        index2 = PathUtility.WrapIndex(idx - 1, count);
        index3 = PathUtility.WrapIndex(idx + 1, count);
      }
      else
      {
        index1 = PathUtility.ClampIndex(idx, count);
        index2 = PathUtility.ClampIndex(idx - 1, count);
        index3 = PathUtility.ClampIndex(idx + 1, count);
      }
      if (index3 != index2)
      {
        this.mGates[index1].normal = (this.mGates[index3].position - this.mGates[index2].position).normalized;
        Vector3 normalized = (this.mGates[index3].position - this.mGates[index1].position).normalized;
        this.mGates[index1].normal.y = normalized.y;
        this.mGates[index1].normal.Normalize();
      }
      else
        this.mGates[index1].normal = (this.mGates[index1].position - this.mGates[index2].position).normalized;
      this.mGates[index1].tangent = Vector3.Cross(Vector3.up, this.mGates[index1].normal);
    }
  }

  private void CalculateCorners(float minCornerAngle)
  {
    this.mCorners.Clear();
    int num1 = 3;
    int num2 = 50;
    int count = this.mGates.Count;
    for (int index1 = 0; index1 < count; ++index1)
    {
      PathData.Gate mGate1 = this.mGates[index1];
      PathData.Gate gate = mGate1;
      int num3 = 1;
      for (int index2 = 1; index2 <= num2; ++index2)
      {
        PathData.Gate mGate2 = this.mGates[PathUtility.CalculatePathIndex(index1 + index2, count, this.type)];
        if ((double) Vector3.Angle(gate.unblendedNormal, mGate2.unblendedNormal) >= (double) minCornerAngle)
        {
          gate = mGate2;
          ++num3;
        }
        else
          break;
      }
      if (num3 >= num1)
      {
        PathData.Corner corner = new PathData.Corner();
        corner.id = this.mCorners.Count;
        corner.startGateID = PathUtility.CalculatePathIndex(mGate1.id - 1, count, this.type);
        corner.endGateID = PathUtility.CalculatePathIndex(gate.id + 1, count, this.type);
        corner.gateCount = num3 + 2;
        int index2 = mGate1.id;
        for (int index3 = 0; index3 < this.mGates.Count && this.mGates[index2].gateType != PathData.GateType.Normal; ++index3)
          index2 = PathUtility.CalculatePathIndex(index2 + 1, count, this.type);
        this.mGates[index2].gateType = PathData.GateType.BrakingZone;
        int num4 = num3 / 2;
        int pathIndex1 = PathUtility.CalculatePathIndex(index2 + num4, count, this.type);
        for (int index3 = 0; index3 < this.mGates.Count && this.mGates[pathIndex1].gateType != PathData.GateType.Normal; ++index3)
          pathIndex1 = PathUtility.CalculatePathIndex(pathIndex1 + 1, count, this.type);
        this.mGates[pathIndex1].gateType = PathData.GateType.AccelerationZone;
        corner.gateCount = 0;
        corner.endGateID = PathUtility.CalculatePathIndex(pathIndex1, count, this.type);
        for (int index3 = 0; index3 <= num2; ++index3)
        {
          int pathIndex2 = PathUtility.CalculatePathIndex(corner.startGateID + index3, count, this.type);
          PathData.Gate mGate2 = this.mGates[pathIndex2];
          mGate2.corner = corner;
          corner.length += mGate2.distance;
          corner.angle += mGate2.racingLineAngle;
          ++corner.gateCount;
          if (pathIndex2 == corner.endGateID)
            break;
        }
        Vector3 vector3 = Vector3.Cross(this.mGates[index2].normal, this.mGates[pathIndex1].normal);
        corner.direction = (double) vector3.y >= 0.0 ? PathData.Corner.Direction.Right : PathData.Corner.Direction.Left;
        this.mCorners.Add(corner);
        index1 = corner.endGateID + 1;
      }
    }
    float num5 = 80f;
    float num6 = 140f;
    float num7 = 250f;
    float num8 = 320f;
    for (int index = 0; index < this.mCorners.Count; ++index)
    {
      PathData.Corner mCorner = this.mCorners[index];
      float num3 = 360f / mCorner.angle * mCorner.length;
      mCorner.radius = num3 / 3.141593f;
      if ((double) mCorner.radius < (double) num6)
      {
        mCorner.type = PathData.Corner.Type.LowSpeed;
        mCorner.speed = Mathf.Clamp01((float) (((double) mCorner.radius - (double) num5) / ((double) num6 - (double) num5)));
      }
      else if ((double) mCorner.radius >= (double) num6 && (double) mCorner.radius < (double) num7)
      {
        mCorner.type = PathData.Corner.Type.MediumSpeed;
        mCorner.speed = Mathf.Clamp01((float) (((double) mCorner.radius - (double) num6) / ((double) num7 - (double) num6)));
      }
      else if ((double) mCorner.radius >= (double) num7)
      {
        mCorner.type = PathData.Corner.Type.HighSpeed;
        mCorner.speed = Mathf.Clamp01((float) (((double) mCorner.radius - (double) num7) / ((double) num8 - (double) num7)));
      }
    }
    if (this.type != PathData.Type.Loop || this.mCorners.Count >= 4 || (double) minCornerAngle <= 0.0)
      return;
    for (int index = 0; index < count; ++index)
    {
      PathData.Gate mGate = this.mGates[index];
      mGate.corner = (PathData.Corner) null;
      if (mGate.gateType == PathData.GateType.BrakingZone || mGate.gateType == PathData.GateType.AccelerationZone)
        mGate.gateType = PathData.GateType.Normal;
    }
    this.CalculateCorners(minCornerAngle - 0.5f);
  }

  private void CalculateStraights()
  {
    this.mStraights.Clear();
    if (this.type != PathData.Type.Loop)
      return;
    PathData.Straight straight1 = (PathData.Straight) null;
    for (int index1 = 0; index1 < this.mCorners.Count; ++index1)
    {
      int index2 = PathUtility.WrapIndex(index1 + 1, this.mCorners.Count);
      PathData.Corner mCorner1 = this.mCorners[index1];
      PathData.Corner mCorner2 = this.mCorners[index2];
      if (mCorner1.endGateID != mCorner2.startGateID && (mCorner1.endGateID < mCorner2.startGateID || mCorner1.endGateID > mCorner2.startGateID && Mathf.Abs(mCorner1.endGateID - mCorner2.startGateID) > this.mGates.Count / 2))
      {
        PathData.Straight straight2 = new PathData.Straight();
        straight2.id = this.mStraights.Count;
        straight2.startGateID = mCorner1.endGateID;
        straight2.endGateID = mCorner2.startGateID;
        mCorner1.nextStraight = straight2;
        straight2.nextCorner = mCorner2;
        int index3 = straight2.startGateID;
        straight2.gateCount = 0;
        bool flag = false;
        while (index3 != straight2.endGateID)
        {
          PathData.Gate mGate = this.mGates[index3];
          mGate.straight = straight2;
          straight2.length += mGate.distance;
          if ((double) straight2.length > 450.0 && !flag && (double) RandomUtility.GetRandom01() < 0.300000011920929)
          {
            mGate.isSparkGate = true;
            flag = true;
          }
          index3 = PathUtility.CalculatePathIndex(index3 + 1, this.mGates.Count, this.type);
          ++straight2.gateCount;
        }
        int index4 = straight2.startGateID;
        for (int index5 = 0; index5 <= straight2.gateCount; ++index5)
        {
          this.mGates[index4].distanceToCorner = (1f - Mathf.Clamp01((float) index5 / (float) straight2.gateCount)) * straight2.length;
          index4 = PathUtility.CalculatePathIndex(index4 + 1, this.mGates.Count, this.type);
        }
        if (straight1 == null || (double) straight2.length > (double) straight1.length)
          straight1 = straight2;
        this.mStraights.Add(straight2);
      }
    }
    for (int index = 0; index < this.mStraights.Count; ++index)
      this.mStraights[index].overtakePossibility = Mathf.Clamp01(this.mStraights[index].length / straight1.length);
    int index6 = 0;
    if (straight1 != null)
    {
      index6 = straight1.startGateID;
      for (int index1 = straight1.gateCount - straight1.gateCount / 3; index6 != straight1.endGateID && index1 > 0; --index1)
        index6 = PathUtility.CalculatePathIndex(index6 + 1, this.mGates.Count, this.type);
    }
    this.mGates[index6].isSpeedTrap = true;
  }

  private void CalculateLockUpZones()
  {
    float num = 400f;
    for (int index1 = 0; index1 < this.mStraights.Count; ++index1)
    {
      PathData.Straight mStraight = this.mStraights[index1];
      if ((double) mStraight.length > (double) num && (mStraight.nextCorner.type == PathData.Corner.Type.MediumSpeed || mStraight.nextCorner.type == PathData.Corner.Type.LowSpeed))
      {
        for (int index2 = 0; index2 < mStraight.nextCorner.gateCount; ++index2)
        {
          if (this.mGates[PathUtility.WrapIndex(mStraight.nextCorner.startGateID + index2, this.mGates.Count)].gateType == PathData.GateType.BrakingZone)
          {
            this.mGates[PathUtility.WrapIndex(mStraight.nextCorner.startGateID + index2 + 1, this.mGates.Count)].isLockUpGate = true;
            break;
          }
        }
      }
    }
  }

  private void CalculateGateDistanceToBrakingGates()
  {
    int count = this.mGates.Count;
    for (int index1 = 0; index1 < count; ++index1)
    {
      int index2 = PathUtility.CalculatePathIndex(index1 + 1, count, this.type);
      float num = 0.0f;
      while (this.mGates[index2].gateType != PathData.GateType.BrakingZone)
      {
        if (this.type == PathData.Type.Loop)
        {
          index2 = PathUtility.WrapIndex(index2 + 1, count);
        }
        else
        {
          ++index2;
          if (index2 >= count)
            break;
        }
        num += this.gates[index2].distance;
      }
      this.mGates[index1].distanceToBrakingGate = num;
    }
  }

  private void CalculateRacingLineAngle(PathData.Gate inGate)
  {
    float num1 = 0.0f;
    int num2 = 0;
    int pathIndex;
    for (int index = inGate.racingLineStart; index != inGate.racingLineEnd; index = pathIndex)
    {
      pathIndex = PathUtility.CalculatePathIndex(index + 1, this.mRacingLineSpline.splinePositions.Length, this.type);
      if (index != pathIndex)
      {
        Vector3 forward1 = this.mRacingLineSpline.splinePositions[index].forward;
        Vector3 forward2 = this.mRacingLineSpline.splinePositions[pathIndex].forward;
        num1 += Vector3.Angle(forward1, forward2);
        ++num2;
      }
    }
    inGate.racingLineAngle = num1;
  }

  private Vector3 GetRacingLinePositionForGate(int inGateID)
  {
    return this.mRacingLineSpline.splinePositions[this.mGates[inGateID].racingLineStart].position;
  }

  private void CalculateRuntimeSplines()
  {
    List<Vector3> vector3List1 = new List<Vector3>((IEnumerable<Vector3>) this.racingLineData.points);
    for (int index = 0; index < vector3List1.Count; ++index)
      vector3List1[index] = this.mTransform.localToWorldMatrix.MultiplyPoint3x4(vector3List1[index]);
    this.mRacingLineSpline = new PathSpline(vector3List1.ToArray(), this.type, this.racingLineData.divisions.Length * 2);
    List<Vector3> vector3List2 = new List<Vector3>((IEnumerable<Vector3>) this.layout.points);
    for (int index = 0; index < vector3List2.Count; ++index)
      vector3List2[index] = this.mTransform.localToWorldMatrix.MultiplyPoint3x4(vector3List2[index]);
    this.mCenterLineSpline = new PathSpline(vector3List2.ToArray(), this.type, this.layout.divisions.Length);
    for (int index1 = 0; index1 < this.mGates.Count; ++index1)
    {
      PathData.Gate mGate1 = this.mGates[index1];
      int idx1 = index1;
      int idx2 = index1 + 1;
      int index2;
      int index3;
      if (this.type == PathData.Type.Loop)
      {
        index2 = PathUtility.WrapIndex(idx1, this.mGates.Count);
        index3 = PathUtility.WrapIndex(idx2, this.mGates.Count);
      }
      else
      {
        index2 = PathUtility.ClampIndex(idx1, this.mGates.Count);
        index3 = PathUtility.ClampIndex(idx2, this.mGates.Count);
      }
      PathData.Gate mGate2 = this.mGates[index2];
      mGate1.centerLineStart = this.mCenterLineSpline.FindClosestPointIDToPosition(mGate2.position);
      mGate1.racingLineStart = this.mRacingLineSpline.FindClosestPointIDToPosition(mGate2.position);
      PathSpline.SplinePosition splinePosition = this.mRacingLineSpline.splinePositions[mGate1.racingLineStart];
      mGate1.normal = splinePosition.forward;
      mGate1.tangent = splinePosition.right;
      mGate1.startDistance = splinePosition.pathDistance;
      PathData.Gate mGate3 = this.mGates[index3];
      mGate1.centerLineEnd = this.mCenterLineSpline.FindClosestPointIDToPosition(mGate3.position);
      mGate1.racingLineEnd = this.mRacingLineSpline.FindClosestPointIDToPosition(mGate3.position);
      mGate1.endDistance = this.mRacingLineSpline.splinePositions[mGate1.racingLineEnd].pathDistance;
      mGate1.distance = mGate1.endDistance - mGate1.startDistance;
      if ((double) mGate1.distance < 0.0)
        mGate1.distance = this.length - mGate1.startDistance + mGate1.endDistance;
      this.mRacingLineSpline.SetPositionsToGatePlane(mGate1);
      this.CalculateRacingLineAngle(mGate1);
    }
  }

  public void SetSplineMode(PathData.SplineMode inSplineMode)
  {
    if (this.mSplineMode == inSplineMode)
      return;
    this.mSplineMode = inSplineMode;
  }

  public void AppendPoint(Vector3 pos)
  {
    this.points.Add(pos);
  }

  public void RemoveLastPoint()
  {
    this.points.RemoveAt(this.points.Count - 1);
  }

  public void RemoveAllPoints()
  {
    this.points.Clear();
  }

  public void ReversePoints()
  {
    this.points.Reverse();
  }

  public void InsertPoint(int idx, Vector3 pos)
  {
    if (idx < 0 || idx > this.points.Count)
      throw new IndexOutOfRangeException();
    this.points.Insert(idx, pos);
  }

  public void Build()
  {
    if (this.points.Count < 2)
    {
      this.segments = (PathData.Segment[]) null;
      this.divisions = (Vector3[]) null;
      this.length = 0.0f;
    }
    else
    {
      this.CalculateSegments();
      this.CalculateDivisions();
      if (this.splineMode == PathData.SplineMode.Layout && (this.racingLineData.divisions == null || this.racingLineData.divisions.Length == 0))
      {
        this.SetSplineMode(PathData.SplineMode.RacingLine);
        this.ResetRacingLine();
        this.SetSplineMode(PathData.SplineMode.Layout);
      }
      ++this.mBuildNum;
    }
  }

  private void CalculateSegments()
  {
    Vector3 zero1 = Vector3.zero;
    Vector3 zero2 = Vector3.zero;
    Vector3 zero3 = Vector3.zero;
    Vector3 zero4 = Vector3.zero;
    int length = this.mTrackType != PathData.Type.Loop ? this.points.Count - 1 : this.points.Count;
    this.segments = new PathData.Segment[length];
    this.length = 0.0f;
    int idx = 0;
    if (this.mTrackType == PathData.Type.Loop)
    {
      for (; idx < length; ++idx)
      {
        Vector3 point1 = this.points[PathUtility.WrapIndex(idx, this.points.Count)];
        Vector3 point2 = this.points[PathUtility.WrapIndex(idx - 1, this.points.Count)];
        Vector3 point3 = this.points[PathUtility.WrapIndex(idx + 2, this.points.Count)];
        Vector3 point4 = this.points[PathUtility.WrapIndex(idx + 1, this.points.Count)];
        this.segments[idx] = new PathData.Segment();
        this.BuildSegment(this.segments[idx], point1, point2, point3, point4);
      }
    }
    else
    {
      for (; idx < length; ++idx)
      {
        Vector3 point1 = this.points[PathUtility.ClampIndex(idx, this.points.Count)];
        Vector3 point2 = this.points[PathUtility.ClampIndex(idx - 1, this.points.Count)];
        Vector3 point3 = this.points[PathUtility.ClampIndex(idx + 2, this.points.Count)];
        Vector3 point4 = this.points[PathUtility.ClampIndex(idx + 1, this.points.Count)];
        this.segments[idx] = new PathData.Segment();
        this.BuildSegment(this.segments[idx], point1, point2, point3, point4);
      }
    }
  }

  public void CalculateDivisions()
  {
    int length = this.segments.Length;
    int index1 = 0;
    this.divisions = new Vector3[length * this.mStepCount + 1];
    float num = 1f / (float) this.mStepCount;
    this.divisions[index1] = this.GetDivisionPosition(this.segments, 0, 0.0f);
    int index2 = index1 + 1;
    for (int segidx = 0; segidx < length; ++segidx)
    {
      for (int index3 = 1; index3 < this.mStepCount + 1; ++index3)
      {
        this.divisions[index2] = this.GetDivisionPosition(this.segments, segidx, (float) index3 * num);
        ++index2;
      }
    }
  }

  public void ResetRacingLine()
  {
    int index1 = 0;
    int mSplineMode = (int) this.mSplineMode;
    if (mSplineMode == index1)
      return;
    if (this.mSplineData[index1].points.Count > 3)
    {
      int count = this.mSplineData[index1].points.Count;
      int num1 = 1;
      List<Vector3> vector3List = new List<Vector3>();
      int index2 = 0;
      while (index2 < count)
      {
        Vector3 point = this.mSplineData[index1].points[index2];
        vector3List.Add(point);
        index2 += num1;
      }
      if (this.type == PathData.Type.Loop)
      {
        float num2 = this.mPathWidth * 0.5f;
        for (int index3 = 0; index3 < vector3List.Count; ++index3)
        {
          int index4 = PathUtility.WrapIndex(index3 - 1, vector3List.Count);
          int index5 = PathUtility.WrapIndex(index3 + 1, vector3List.Count);
          Vector3 vector3_1 = vector3List[index3] - vector3List[index4];
          Vector3 vector3_2 = vector3List[index5] - vector3List[index3];
          Vector3 normalized = Vector3.Cross(vector3List[index5] - vector3List[index4], Vector3.up).normalized;
          float num3 = Mathf.Min(Vector3.Angle(vector3_1, vector3_2), 80f) / 80f;
          if ((double) Vector3.Cross(vector3_1, vector3_2).y <= 0.0)
            vector3List[index3] += normalized * (num2 * num3);
          else
            vector3List[index3] -= normalized * (num2 * num3);
        }
      }
      this.mSplineData[mSplineMode].points = vector3List;
    }
    else
      this.mSplineData[mSplineMode].points = new List<Vector3>((IEnumerable<Vector3>) this.mSplineData[index1].points);
    this.Build();
  }

  private void BuildSegment(PathData.Segment ss, Vector3 sp, Vector3 sc, Vector3 ec, Vector3 ep)
  {
    ss.startPos = sp;
    ss.endPos = ep;
    ss.startCtrl = sc;
    ss.endCtrl = ec;
    ss.startLength = this.length;
    float length = this.GetLength(ss);
    this.length += length;
    ss.length = length;
    ss.endLength = this.length;
    this.mPrecompDiv = 1f / (float) this.mStepCount;
    float t = 0.0f;
    ss.parameters = new float[this.mStepCount + 1];
    ss.precomps = new float[this.mStepCount + 1];
    float num = 0.0f;
    for (int index = 1; index < this.mStepCount + 1; ++index)
    {
      Vector3 position1 = this.GetPosition(ss, t);
      t += this.mPrecompDiv;
      Vector3 position2 = this.GetPosition(ss, t);
      num += (position2 - position1).magnitude;
      ss.precomps[index] = num / length;
      ss.parameters[index] = t;
    }
    ss.parameters[0] = 0.0f;
    ss.parameters[this.mStepCount] = 1f;
    ss.precomps[0] = 0.0f;
    ss.precomps[this.mStepCount] = 1f;
    this.mPrecompDiv = 1f / (float) this.mStepCount;
  }

  private float GetLength(PathData.Segment ss)
  {
    float num1 = 0.0f;
    float t = 0.0f;
    float num2 = 1f / (float) this.mStepCount;
    int num3 = 0;
    Vector3 vector3 = ss.startPos;
    for (; num3 < this.mStepCount; ++num3)
    {
      t += num2;
      Vector3 position = this.GetPosition(ss, t);
      num1 += (position - vector3).magnitude;
      vector3 = position;
    }
    return num1;
  }

  private Vector3 GetPosition(PathData.Segment ss, float t)
  {
    float num1 = t * t;
    float num2 = num1 * t;
    return ss.startPos * (float) (1.5 * (double) num2 - 2.5 * (double) num1 + 1.0) + ss.startCtrl * (float) (-0.5 * (double) num2 + (double) num1 - 0.5 * (double) t) + ss.endCtrl * (float) (0.5 * (double) num2 - 0.5 * (double) num1) + ss.endPos * (float) (-1.5 * (double) num2 + 2.0 * (double) num1 + 0.5 * (double) t);
  }

  private Vector3 GetTangent(PathData.Segment ss, float t)
  {
    float num = t * t;
    return ss.startPos * (float) (4.5 * (double) t - 5.0) * t + ss.startCtrl * (float) (-1.5 * (double) num + 2.0 * (double) t - 0.5) + ss.endCtrl * (float) (1.5 * (double) t - 1.0) * t + ss.endPos * (float) (-4.5 * (double) num + 4.0 * (double) t + 0.5);
  }

  private Vector3 GetNormal(PathData.Segment ss, float t)
  {
    return ss.startPos * (float) (9.0 * (double) t - 5.0) - ss.startCtrl * (float) (2.0 - 3.0 * (double) t) + 9f * ss.endPos * t + 3f * ss.endCtrl * t + 4f * ss.endPos - ss.endCtrl;
  }

  private float GetReparam(PathData.Segment ss, float u)
  {
    if ((double) u <= 0.0)
      return 0.0f;
    if ((double) u >= 1.0)
      return 1f;
    int index1 = 0;
    for (int index2 = 1; index2 < this.mStepCount + 1; ++index2)
    {
      if ((double) ss.precomps[index2] > (double) u)
      {
        index1 = index2 - 1;
        break;
      }
    }
    float t = (float) (((double) u - (double) ss.precomps[index1]) / ((double) ss.precomps[index1 + 1] - (double) ss.precomps[index1]));
    return Mathf.Lerp(ss.parameters[index1], ss.parameters[index1 + 1], t);
  }

  public int GetPointCount()
  {
    return this.points.Count;
  }

  public int GetSegmentCount()
  {
    if (this.segments != null)
      return this.segments.Length;
    return 0;
  }

  public float GetSegmentLength(int segidx)
  {
    return this.segments[segidx].length;
  }

  public float GetSegmentStartLength(int segidx)
  {
    return this.segments[segidx].startLength;
  }

  public float GetSegmentEndLength(int segidx)
  {
    return this.segments[segidx].endLength;
  }

  public int FindSegment(float offset)
  {
    for (int index = 0; index < this.segments.Length; ++index)
    {
      if ((double) this.segments[index].startLength <= (double) offset && (double) this.segments[index].endLength > (double) offset)
        return index;
    }
    return this.segments.Length - 1;
  }

  public PathData.Gate FindNextGateOfType(PathData.Gate inGate, PathData.GateType inType)
  {
    PathData.Gate gate = (PathData.Gate) null;
    int count = this.gates.Count;
    if (inGate != null)
    {
      int index1 = inGate.id;
      for (int index2 = 0; index2 < count; ++index2)
      {
        if (this.type == PathData.Type.Loop)
          index1 = PathUtility.WrapIndex(index1 + 1, count);
        else
          ++index1;
        if (this.type != PathData.Type.Line || index1 < count)
        {
          if (this.gates[index1].gateType == inType)
          {
            gate = this.gates[index1];
            break;
          }
        }
        else
          break;
      }
    }
    return gate;
  }

  public PathData.Gate FindNextGateOfType(PathData.Gate inGate, PathData.GateType[] inType)
  {
    PathData.Gate gate = (PathData.Gate) null;
    int count = this.gates.Count;
    if (inGate != null)
    {
      int index1 = inGate.id;
      for (int index2 = 0; index2 < count; ++index2)
      {
        if (this.type == PathData.Type.Loop)
          index1 = PathUtility.WrapIndex(index1 + 1, count);
        else
          ++index1;
        if (this.type != PathData.Type.Line || index1 < count)
        {
          for (int index3 = 0; index3 < inType.Length; ++index3)
          {
            if (this.gates[index1].gateType == inType[index3])
              gate = this.gates[index1];
          }
          if (gate != null)
            break;
        }
        else
          break;
      }
    }
    return gate;
  }

  public PathData.Gate GetFirstGate()
  {
    if (this.mGates.Count == 0)
      Debug.LogError((object) "No Gates are generated for path", (UnityEngine.Object) null);
    return this.mGates[0];
  }

  public PathData.Gate GetLastGate()
  {
    return this.mGates[this.mGates.Count - 1];
  }

  private Vector3 GetDivisionPosition(PathData.Segment[] centerSegments, int segidx, float t)
  {
    return this.GetPosition(centerSegments[segidx], t);
  }

  public Vector3 GetPosition(int segidx, float segpos)
  {
    PathData.Segment segment = this.segments[segidx];
    return this.GetPosition(segment, this.GetReparam(segment, segpos / segment.length));
  }

  public Vector3 GetTangent(int segidx, float segpos)
  {
    PathData.Segment segment = this.segments[segidx];
    return this.GetTangent(segment, this.GetReparam(segment, segpos / segment.length));
  }

  public Vector3 GetNormal(int segidx, float segpos)
  {
    PathData.Segment segment = this.segments[segidx];
    return this.GetNormal(segment, this.GetReparam(segment, segpos / segment.length));
  }

  public PathEditHelper GetEditHelper()
  {
    return new PathEditHelper(this);
  }

  public enum Type
  {
    Loop,
    Line,
  }

  public enum GateType
  {
    Normal,
    Sector,
    BrakingZone,
    AccelerationZone,
  }

  public enum GateParticleType
  {
    KeepPrevious,
    None,
    Dust,
    Grass,
    Sand,
    TyreWhiteSmoke,
    BlackSmoke,
    BlackSmokeIntense,
  }

  public enum SplineMode
  {
    Layout,
    RacingLine,
  }

  [Serializable]
  public class Segment
  {
    public Vector3 startPos;
    public Vector3 startCtrl;
    public Vector3 endCtrl;
    public Vector3 endPos;
    public float startLength;
    public float endLength;
    public float length;
    public float[] parameters;
    public float[] precomps;
  }

  [Serializable]
  public class SplineData
  {
    public List<Vector3> points = new List<Vector3>();
    public Vector3[] divisions;
    public PathData.Segment[] segments;
    public float length;
  }

  public class Gate
  {
    public int id;
    public PathData.GateType gateType;
    public PathData.Corner corner;
    public PathData.Straight straight;
    public Vector3 position;
    public Vector3 normal;
    public Vector3 tangent;
    public Vector3 unblendedNormal;
    public Vector3 unblendedTangent;
    public float distance;
    public float startDistance;
    public float endDistance;
    public float distanceToBrakingGate;
    public int racingLineStart;
    public int racingLineEnd;
    public float racingLineAngle;
    public int centerLineStart;
    public int centerLineEnd;
    public float distanceToCorner;
    public bool isSpeedTrap;
    public bool isSparkGate;
    public bool isLockUpGate;
  }

  public class Corner
  {
    public PathData.Corner.Type type = PathData.Corner.Type.MediumSpeed;
    public int id;
    public PathData.Corner.Direction direction;
    public float speed;
    public int startGateID;
    public int endGateID;
    public int gateCount;
    public float radius;
    public float angle;
    public float length;
    public PathData.Straight nextStraight;

    public enum Type
    {
      LowSpeed,
      MediumSpeed,
      HighSpeed,
    }

    public enum Direction
    {
      Left,
      Right,
    }
  }

  public class Straight
  {
    public float overtakePossibility = 1f;
    public int id;
    public int startGateID;
    public int endGateID;
    public int gateCount;
    public float length;
    public PathData.Corner nextCorner;
  }
}
