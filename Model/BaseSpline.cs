// Decompiled with JetBrains decompiler
// Type: BaseSpline
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using Dest.Math;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpline
{
  protected List<BaseSpline.ItemData> _data = new List<BaseSpline.ItemData>();
  protected bool _recalcSegmentsLength = true;
  protected BaseSpline.ArcLengthParametrization _parametrization;
  protected SplineTypes _type;
  protected SplinePlaneTypes _creationPlane;

  protected int SegmentCount
  {
    get
    {
      if (this._type == SplineTypes.Open)
        return this._data.Count - 1;
      return this._data.Count;
    }
  }

  public int VertexCount
  {
    get
    {
      return this._data.Count;
    }
  }

  public bool Valid
  {
    get
    {
      return this._data.Count > 1;
    }
  }

  public abstract SplineTypes SplineType { get; set; }

  protected void GetSegmentIndexAndTime(float time, out int segmentIndex, out float segmentTime)
  {
    if ((double) time <= 0.0)
    {
      segmentIndex = 0;
      segmentTime = 0.0f;
    }
    else if ((double) time >= 1.0)
    {
      segmentIndex = this.SegmentCount - 1;
      segmentTime = 1f;
    }
    else
    {
      float segmentCount = (float) this.SegmentCount;
      segmentIndex = (int) ((double) segmentCount * (double) time);
      float num = 1f / segmentCount;
      segmentTime = (time - (float) segmentIndex * num) / num;
    }
  }

  protected void RecalcSegmentsLength()
  {
    if (!this._recalcSegmentsLength)
      return;
    float currentLength = this._data[0].ProcessLength(0.0f);
    int index = 1;
    for (int segmentCount = this.SegmentCount; index < segmentCount; ++index)
      currentLength = this._data[index].ProcessLength(currentLength);
    this._recalcSegmentsLength = false;
  }

  public abstract void AddVertexFirst(Vector3 position);

  public abstract void AddVertexLast(Vector3 position);

  public abstract void RemoveVertex(int index);

  public abstract void Clear();

  public abstract void InsertBefore(int vertexIndex, Vector3 position);

  public abstract void InsertAfter(int vertexIndex, Vector3 position);

  public abstract Vector3 GetVertex(int vertexIndex);

  public abstract void SetVertex(int vertexIndex, Vector3 position);

  public Vector3 EvalPosition(float time)
  {
    int segmentIndex;
    float segmentTime;
    this.GetSegmentIndexAndTime(time, out segmentIndex, out segmentTime);
    return this._data[segmentIndex].EvalPosition(segmentTime);
  }

  public Vector3 EvalTangent(float time)
  {
    int segmentIndex;
    float segmentTime;
    this.GetSegmentIndexAndTime(time, out segmentIndex, out segmentTime);
    return this._data[segmentIndex].EvalTangent(segmentTime);
  }

  public PositionTangent EvalPositionTangent(float time)
  {
    int segmentIndex;
    float segmentTime;
    this.GetSegmentIndexAndTime(time, out segmentIndex, out segmentTime);
    BaseSpline.ItemData itemData = this._data[segmentIndex];
    PositionTangent positionTangent;
    positionTangent.Position = itemData.EvalPosition(segmentTime);
    positionTangent.Tangent = itemData.EvalTangent(segmentTime);
    return positionTangent;
  }

  public void EvalPosition(float time, out Vector3 position)
  {
    int segmentIndex;
    float segmentTime;
    this.GetSegmentIndexAndTime(time, out segmentIndex, out segmentTime);
    position = this._data[segmentIndex].EvalPosition(segmentTime);
  }

  public void EvalTangent(float time, out Vector3 tangent)
  {
    int segmentIndex;
    float segmentTime;
    this.GetSegmentIndexAndTime(time, out segmentIndex, out segmentTime);
    tangent = this._data[segmentIndex].EvalTangent(segmentTime);
  }

  public void EvalPositionTangent(float time, out PositionTangent positionTangent)
  {
    int segmentIndex;
    float segmentTime;
    this.GetSegmentIndexAndTime(time, out segmentIndex, out segmentTime);
    BaseSpline.ItemData itemData = this._data[segmentIndex];
    positionTangent.Position = itemData.EvalPosition(segmentTime);
    positionTangent.Tangent = itemData.EvalTangent(segmentTime);
  }

  public void EvalFrame(float time, out CurveFrame frame)
  {
    int segmentIndex;
    float segmentTime;
    this.GetSegmentIndexAndTime(time, out segmentIndex, out segmentTime);
    BaseSpline.ItemData itemData = this._data[segmentIndex];
    frame.Position = itemData.EvalPosition(segmentTime);
    Vector3 vector3 = itemData.EvalFirstDerivative(segmentTime);
    Vector3 rhs = itemData.EvalSecondDerivative(segmentTime);
    float num1 = Vector3.Dot(vector3, vector3);
    float num2 = Vector3.Dot(vector3, rhs);
    frame.Normal = num1 * rhs - num2 * vector3;
    frame.Normal.Normalize();
    frame.Tangent = vector3;
    frame.Tangent.Normalize();
    frame.Binormal = Vector3.Cross(frame.Tangent, frame.Normal);
  }

  public float EvalCurvature(float time)
  {
    int segmentIndex;
    float segmentTime;
    this.GetSegmentIndexAndTime(time, out segmentIndex, out segmentTime);
    BaseSpline.ItemData itemData = this._data[segmentIndex];
    Vector3 lhs = itemData.EvalFirstDerivative(segmentTime);
    float sqrMagnitude = lhs.sqrMagnitude;
    if ((double) sqrMagnitude < 9.99999974737875E-06)
      return 0.0f;
    Vector3 rhs = itemData.EvalSecondDerivative(segmentTime);
    return Vector3.Cross(lhs, rhs).magnitude / Mathf.Pow(sqrMagnitude, 1.5f);
  }

  public float EvalTorsion(float time)
  {
    int segmentIndex;
    float segmentTime;
    this.GetSegmentIndexAndTime(time, out segmentIndex, out segmentTime);
    BaseSpline.ItemData itemData = this._data[segmentIndex];
    Vector3 lhs = Vector3.Cross(itemData.EvalFirstDerivative(segmentTime), itemData.EvalSecondDerivative(segmentTime));
    float sqrMagnitude = lhs.sqrMagnitude;
    if ((double) sqrMagnitude < 9.99999974737875E-06)
      return 0.0f;
    Vector3 rhs = itemData.EvalThirdDerivative(segmentTime);
    return Vector3.Dot(lhs, rhs) / sqrMagnitude;
  }

  public Vector3 EvalPositionParametrized(float length)
  {
    int segmentIndex;
    float segmentTime;
    this.GetSegmentIndexAndTime(this._parametrization.GetApproximateTimeParameter(length), out segmentIndex, out segmentTime);
    return this._data[segmentIndex].EvalPosition(segmentTime);
  }

  public Vector3 EvalTangentParametrized(float length)
  {
    int segmentIndex;
    float segmentTime;
    this.GetSegmentIndexAndTime(this._parametrization.GetApproximateTimeParameter(length), out segmentIndex, out segmentTime);
    return this._data[segmentIndex].EvalTangent(segmentTime);
  }

  public PositionTangent EvalPositionTangentParametrized(float length)
  {
    int segmentIndex;
    float segmentTime;
    this.GetSegmentIndexAndTime(this._parametrization.GetApproximateTimeParameter(length), out segmentIndex, out segmentTime);
    BaseSpline.ItemData itemData = this._data[segmentIndex];
    PositionTangent positionTangent;
    positionTangent.Position = itemData.EvalPosition(segmentTime);
    positionTangent.Tangent = itemData.EvalTangent(segmentTime);
    return positionTangent;
  }

  public void EvalPositionParametrized(float length, out Vector3 position)
  {
    int segmentIndex;
    float segmentTime;
    this.GetSegmentIndexAndTime(this._parametrization.GetApproximateTimeParameter(length), out segmentIndex, out segmentTime);
    position = this._data[segmentIndex].EvalPosition(segmentTime);
  }

  public void EvalTangentParametrized(float length, out Vector3 tangent)
  {
    int segmentIndex;
    float segmentTime;
    this.GetSegmentIndexAndTime(this._parametrization.GetApproximateTimeParameter(length), out segmentIndex, out segmentTime);
    tangent = this._data[segmentIndex].EvalTangent(segmentTime);
  }

  public void EvalPositionTangentParametrized(float length, out PositionTangent positionTangent)
  {
    int segmentIndex;
    float segmentTime;
    this.GetSegmentIndexAndTime(this._parametrization.GetApproximateTimeParameter(length), out segmentIndex, out segmentTime);
    BaseSpline.ItemData itemData = this._data[segmentIndex];
    positionTangent.Position = itemData.EvalPosition(segmentTime);
    positionTangent.Tangent = itemData.EvalTangent(segmentTime);
  }

  public void EvalFrameParametrized(float length, out CurveFrame frame)
  {
    int segmentIndex;
    float segmentTime;
    this.GetSegmentIndexAndTime(this._parametrization.GetApproximateTimeParameter(length), out segmentIndex, out segmentTime);
    BaseSpline.ItemData itemData = this._data[segmentIndex];
    frame.Position = itemData.EvalPosition(segmentTime);
    Vector3 vector3 = itemData.EvalFirstDerivative(segmentTime);
    Vector3 rhs = itemData.EvalSecondDerivative(segmentTime);
    float num1 = Vector3.Dot(vector3, vector3);
    float num2 = Vector3.Dot(vector3, rhs);
    frame.Normal = num1 * rhs - num2 * vector3;
    frame.Normal.Normalize();
    frame.Tangent = vector3;
    frame.Tangent.Normalize();
    frame.Binormal = Vector3.Cross(frame.Tangent, frame.Normal);
  }

  public float EvalCurvatureParametrized(float length)
  {
    int segmentIndex;
    float segmentTime;
    this.GetSegmentIndexAndTime(this._parametrization.GetApproximateTimeParameter(length), out segmentIndex, out segmentTime);
    BaseSpline.ItemData itemData = this._data[segmentIndex];
    Vector3 lhs = itemData.EvalFirstDerivative(segmentTime);
    float sqrMagnitude = lhs.sqrMagnitude;
    if ((double) sqrMagnitude < 9.99999974737875E-06)
      return 0.0f;
    Vector3 rhs = itemData.EvalSecondDerivative(segmentTime);
    return Vector3.Cross(lhs, rhs).magnitude / Mathf.Pow(sqrMagnitude, 1.5f);
  }

  public float EvalTorsionParametrized(float length)
  {
    int segmentIndex;
    float segmentTime;
    this.GetSegmentIndexAndTime(this._parametrization.GetApproximateTimeParameter(length), out segmentIndex, out segmentTime);
    BaseSpline.ItemData itemData = this._data[segmentIndex];
    Vector3 lhs = Vector3.Cross(itemData.EvalFirstDerivative(segmentTime), itemData.EvalSecondDerivative(segmentTime));
    float sqrMagnitude = lhs.sqrMagnitude;
    if ((double) sqrMagnitude < 9.99999974737875E-06)
      return 0.0f;
    Vector3 rhs = itemData.EvalThirdDerivative(segmentTime);
    return Vector3.Dot(lhs, rhs) / sqrMagnitude;
  }

  public float CalcTotalLength()
  {
    if (this._data.Count < 2)
      return 0.0f;
    this.RecalcSegmentsLength();
    return this._data[this.SegmentCount - 1].AccumulatedLength;
  }

  public float LengthToTime(float length, int iterations, float tolerance)
  {
    if (this._data.Count < 2)
      return 0.0f;
    this.RecalcSegmentsLength();
    int segmentCount = this.SegmentCount;
    int index1 = segmentCount - 1;
    if ((double) length <= 0.0)
      return 0.0f;
    if ((double) length >= (double) this._data[index1].AccumulatedLength)
      return 1f;
    int index2 = 0;
    while (index2 < segmentCount && (double) length >= (double) this._data[index2].AccumulatedLength)
      ++index2;
    BaseSpline.ItemData itemData = this._data[index2];
    float num1 = (float) index2;
    float num2 = index2 != 0 ? length - this._data[index2 - 1].AccumulatedLength : length;
    float length1 = this._data[index2].Length;
    float num3 = 1f;
    float num4 = num3 * num2 / length1;
    float num5 = 0.0f;
    float num6 = num3;
    for (int index3 = 0; index3 < iterations; ++index3)
    {
      float f = itemData.EvalLength(0.0f, num4) - num2;
      if ((double) Mathf.Abs(f) <= (double) tolerance)
        return (num1 + num4) / (float) segmentCount;
      float num7 = num4 - f / itemData.EvalSpeed(num4);
      if ((double) f > 0.0)
      {
        num6 = num4;
        num4 = (double) num7 > (double) num5 ? num7 : (float) (0.5 * ((double) num6 + (double) num5));
      }
      else
      {
        num5 = num4;
        num4 = (double) num7 < (double) num6 ? num7 : (float) (0.5 * ((double) num6 + (double) num5));
      }
    }
    return (num1 + num4) / (float) segmentCount;
  }

  public float LengthToTime(float length)
  {
    return this.LengthToTime(length, 32, 1E-05f);
  }

  public float ParametrizeByArcLength(int pointCount)
  {
    if (!this.Valid)
      return -1f;
    float num1 = this.CalcTotalLength();
    float num2 = num1 / (float) pointCount;
    int length1 = pointCount + 1;
    float[] numArray1 = new float[length1];
    float[] numArray2 = new float[length1];
    float[] numArray3 = new float[length1];
    numArray1[0] = 0.0f;
    numArray2[0] = 0.0f;
    numArray3[0] = 0.0f;
    for (int index = 1; index < pointCount; ++index)
    {
      float length2 = (float) index * num2;
      numArray1[index] = length2;
      numArray2[index] = this.LengthToTime(length2, 32, 1E-05f);
      numArray3[index] = (float) (((double) numArray2[index] - (double) numArray2[index - 1]) / ((double) numArray1[index] - (double) numArray1[index - 1]));
    }
    numArray1[pointCount] = num1;
    numArray2[pointCount] = 1f;
    numArray3[pointCount] = (float) (((double) numArray2[pointCount] - (double) numArray2[pointCount - 1]) / ((double) numArray1[pointCount] - (double) numArray1[pointCount - 1]));
    this._parametrization = new BaseSpline.ArcLengthParametrization()
    {
      sSample = numArray1,
      tSample = numArray2,
      tsSlope = numArray3,
      L = num1
    };
    return num1;
  }

  protected class ItemData
  {
    public Vector3 Position;
    public Vector3 A;
    public Vector3 B;
    public Vector3 C;
    public Vector3 D;
    public float Length;
    public float AccumulatedLength;

    public Vector3 EvalPosition(float t)
    {
      return this.A + t * (this.B + t * (this.C + t * this.D));
    }

    public Vector3 EvalFirstDerivative(float t)
    {
      return this.B + t * (this.C * 2f + this.D * (3f * t));
    }

    public Vector3 EvalSecondDerivative(float t)
    {
      return 2f * this.C + 6f * t * this.D;
    }

    public Vector3 EvalThirdDerivative(float t)
    {
      return 6f * this.D;
    }

    public float EvalSpeed(float t)
    {
      return (this.B + t * (this.C * 2f + this.D * (3f * t))).magnitude;
    }

    public Vector3 EvalTangent(float t)
    {
      return (this.B + t * (this.C * 2f + this.D * (3f * t))).normalized;
    }

    public float EvalLength()
    {
      return Integrator.GaussianQuadrature(new Func<float, float>(this.EvalSpeed), 0.0f, 1f);
    }

    public float EvalLength(float t0, float t1)
    {
      return Integrator.GaussianQuadrature(new Func<float, float>(this.EvalSpeed), t0, t1);
    }

    public float ProcessLength(float currentLength)
    {
      this.Length = this.EvalLength();
      this.AccumulatedLength = currentLength + this.Length;
      return this.AccumulatedLength;
    }
  }

  protected class ArcLengthParametrization
  {
    public float[] sSample;
    public float[] tSample;
    public float[] tsSlope;
    public float L;

    public float GetApproximateTimeParameter(float s)
    {
      if ((double) s <= 0.0)
        return 0.0f;
      if ((double) s >= (double) this.L)
        return 1f;
      int index = Array.BinarySearch<float>(this.sSample, s);
      if (index < 0)
        index = ~index;
      return this.tSample[index - 1] + this.tsSlope[index] * (s - this.sSample[index - 1]);
    }
  }
}
