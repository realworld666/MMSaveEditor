// Decompiled with JetBrains decompiler
// Type: PathEditHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PathEditHelper
{
  private int mIndex = -1;
  private int mSelectedIndex = -1;
  private int mGateIndex = -1;
  private int mSelectedGateIndex = -1;
  private PathData mData;
  private PathEditHelper.EditMode mEditMode;

  public PathEditHelper.EditMode editMode
  {
    get
    {
      return this.mEditMode;
    }
    set
    {
      this.mEditMode = value;
    }
  }

  public Vector3 point
  {
    get
    {
      return this.mData.points[this.mIndex];
    }
    set
    {
      this.mData.points[this.mIndex] = value;
    }
  }

  public Vector3 selectedPoint
  {
    get
    {
      return this.mData.points[this.mSelectedIndex];
    }
    set
    {
      this.mData.points[this.mSelectedIndex] = value;
    }
  }

  public bool selected
  {
    get
    {
      return this.mIndex == this.mSelectedIndex;
    }
    set
    {
      if (value)
        this.mSelectedIndex = this.mIndex;
      else
        this.mSelectedIndex = -1;
    }
  }

  public bool isSomethingSelected
  {
    get
    {
      return this.mSelectedIndex != -1;
    }
  }

  public int index
  {
    get
    {
      return this.mIndex;
    }
  }

  public int selectedIndex
  {
    get
    {
      return this.mSelectedIndex;
    }
    set
    {
      this.mSelectedIndex = value;
    }
  }

  public Vector3 gatePoint
  {
    get
    {
      return this.mData.gates[this.mGateIndex].position;
    }
    set
    {
    }
  }

  public bool gateSelected
  {
    get
    {
      return this.mGateIndex == this.mSelectedGateIndex;
    }
    set
    {
      if (value)
        this.mSelectedGateIndex = this.mGateIndex;
      else
        this.mSelectedGateIndex = -1;
    }
  }

  public bool isAGateSelected
  {
    get
    {
      return this.mSelectedGateIndex != -1;
    }
  }

  public int gateIndex
  {
    get
    {
      return this.mGateIndex;
    }
  }

  public int selectedGateIndex
  {
    get
    {
      return this.mSelectedGateIndex;
    }
    set
    {
      this.mSelectedGateIndex = value;
    }
  }

  public PathData.GateType selectedGateType
  {
    get
    {
      return this.mData.gates[this.mSelectedGateIndex].gateType;
    }
    set
    {
      this.mData.gates[this.mSelectedGateIndex].gateType = value;
    }
  }

  internal PathEditHelper(PathData inData)
  {
    this.mData = inData;
  }

  public bool MoveNext()
  {
    ++this.mIndex;
    return this.mIndex < this.mData.points.Count;
  }

  public bool MoveNextGate()
  {
    ++this.mGateIndex;
    return this.mGateIndex < this.mData.gates.Count;
  }

  public void Reset()
  {
    this.mIndex = -1;
    this.mGateIndex = -1;
  }

  public void AppendPoint()
  {
    if (this.mData.points.Count == 0)
      this.mData.AppendPoint(Vector3.zero);
    else
      this.mData.AppendPoint(this.mData.points[this.mData.points.Count - 1] + Vector3.right * 200f);
    this.mSelectedIndex = this.mData.points.Count - 1;
  }

  public void InsertBefore()
  {
    if (this.mData.points.Count == 1)
    {
      this.mData.InsertPoint(0, this.mData.points[this.mData.points.Count - 1] + Vector3.right);
    }
    else
    {
      int mSelectedIndex = this.mSelectedIndex;
      --this.mSelectedIndex;
      if (this.mSelectedIndex < 0)
        this.mSelectedIndex = this.mData.points.Count - 1;
      this.mData.InsertPoint(mSelectedIndex, (this.mData.points[this.mSelectedIndex] + this.mData.points[mSelectedIndex]) * 0.5f);
      this.mSelectedIndex = mSelectedIndex;
    }
  }

  public void InsertAfter()
  {
    if (this.mData.points.Count == 1)
    {
      this.mData.InsertPoint(0, this.mData.points[this.mData.points.Count - 1] + Vector3.right);
    }
    else
    {
      int mSelectedIndex = this.mSelectedIndex;
      ++this.mSelectedIndex;
      if (this.mSelectedIndex == this.mData.points.Count)
        this.mSelectedIndex = this.mData.points.Count - 1;
      this.mData.InsertPoint(this.mSelectedIndex, (this.mData.points[this.mSelectedIndex] + this.mData.points[mSelectedIndex]) * 0.5f);
    }
  }

  public void ReverseDirection()
  {
    PathData.SplineMode splineMode = this.mData.splineMode;
    for (int index1 = 0; index1 < 2; ++index1)
    {
      this.mData.SetSplineMode((PathData.SplineMode) index1);
      if (this.mData.points.Count > 0)
      {
        Vector3[] vector3Array = new Vector3[this.mData.points.Count];
        for (int index2 = 0; index2 < vector3Array.Length; ++index2)
          vector3Array[index2] = this.mData.points[index2];
        this.mData.RemoveAllPoints();
        if (this.mData.type == PathData.Type.Loop)
        {
          this.mData.AppendPoint(vector3Array[0]);
          for (int index2 = vector3Array.Length - 1; index2 > 0; --index2)
            this.mData.AppendPoint(vector3Array[index2]);
        }
        else
        {
          for (int index2 = vector3Array.Length - 1; index2 >= 0; --index2)
            this.mData.AppendPoint(vector3Array[index2]);
        }
      }
    }
    this.mData.SetSplineMode(splineMode);
    this.mData.Build();
  }

  public void MakeFinishLine()
  {
    if (this.mData.points.Count <= 0)
      return;
    if (this.mSelectedIndex == this.mData.points.Count)
      this.mSelectedIndex = this.mData.points.Count - 1;
    Vector3[] vector3Array = new Vector3[this.mData.points.Count];
    for (int index = 0; index < vector3Array.Length; ++index)
      vector3Array[index] = this.mData.points[index];
    this.mData.RemoveAllPoints();
    int num = 0;
    int index1 = this.mSelectedIndex;
    for (; num < vector3Array.Length; ++num)
    {
      this.mData.AppendPoint(vector3Array[index1]);
      ++index1;
      if (index1 >= vector3Array.Length)
        index1 = 0;
    }
    this.mData.Build();
  }

  public void Remove()
  {
    this.mData.points.RemoveAt(this.mSelectedIndex);
    if (this.mSelectedIndex < this.mData.points.Count)
      return;
    this.mSelectedIndex = this.mData.points.Count - 1;
  }

  public void RemoveLast()
  {
    if (this.mData.points.Count > 0)
      this.mData.RemoveLastPoint();
    if (this.mSelectedIndex < this.mData.points.Count)
      return;
    this.mSelectedIndex = this.mData.points.Count - 1;
  }

  public void SelectFirst()
  {
    if (this.mData.points.Count > 0)
      this.mSelectedIndex = 0;
    else
      this.mSelectedIndex = -1;
  }

  public void SelectNext()
  {
    if (this.mSelectedIndex < this.mData.points.Count - 1)
      ++this.mSelectedIndex;
    else
      this.mSelectedIndex = 0;
  }

  public void SelectPrev()
  {
    if (this.mSelectedIndex > 0)
      --this.mSelectedIndex;
    else
      this.mSelectedIndex = this.mData.points.Count - 1;
  }

  public void ScaleUp()
  {
    this.Scale(1.025f);
  }

  public void ScaleDown()
  {
    this.Scale(0.975f);
  }

  private void Scale(float inValue)
  {
    PathData.SplineMode splineMode = this.mData.splineMode;
    for (int index1 = 0; index1 < 2; ++index1)
    {
      this.mData.SetSplineMode((PathData.SplineMode) index1);
      for (int index2 = 0; index2 < this.mData.points.Count; ++index2)
        this.mData.points[index2] *= inValue;
    }
    for (int index = 0; index < this.mData.gates.Count; ++index)
      this.mData.gates[index].position *= inValue;
    this.mData.Build();
    this.mData.SetSplineMode(splineMode);
  }

  public enum EditMode
  {
    Layout,
    RacingLine,
    Gates,
  }
}
