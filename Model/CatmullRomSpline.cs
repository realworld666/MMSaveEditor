// Decompiled with JetBrains decompiler
// Type: CatmullRomSpline
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class CatmullRomSpline : BaseSpline
{
  public override SplineTypes SplineType
  {
    get
    {
      return this._type;
    }
    set
    {
      if (this._type == value)
        return;
      this._type = value;
      this._recalcSegmentsLength = true;
      this.UpdateAdjacentSegments(0);
      this.UpdateAdjacentSegments(this._data.Count - 1);
    }
  }

  public static CatmullRomSpline Create(IList<Vector3> points, SplineTypes type)
  {
    CatmullRomSpline catmullRomSpline = new CatmullRomSpline();
    catmullRomSpline._type = type;
    catmullRomSpline._data = new List<BaseSpline.ItemData>(points.Count);
    for (int index = 0; index < points.Count; ++index)
      catmullRomSpline._data.Add(new BaseSpline.ItemData()
      {
        Position = points[index]
      });
    if (points.Count >= 2)
    {
      int index = 0;
      for (int segmentCount = catmullRomSpline.SegmentCount; index < segmentCount; ++index)
        catmullRomSpline.UpdateSegment(index);
    }
    catmullRomSpline._recalcSegmentsLength = true;
    return catmullRomSpline;
  }

  private void UpdateSegment(int index)
  {
    int index1 = this.SegmentCount - 1;
    int count = this._data.Count;
    Vector3 position1 = this._data[index].Position;
    Vector3 position2;
    Vector3 vector3_1;
    Vector3 vector3_2;
    if (this._type == SplineTypes.Open)
    {
      position2 = this._data[index + 1].Position;
      vector3_1 = index != 0 ? this._data[index - 1].Position : position1;
      vector3_2 = index != index1 ? this._data[index + 2].Position : position2;
    }
    else
    {
      vector3_1 = index != 0 ? this._data[index - 1].Position : this._data[index1].Position;
      position2 = this._data[(index + 1) % count].Position;
      vector3_2 = this._data[(index + 2) % count].Position;
    }
    BaseSpline.ItemData itemData = this._data[index];
    itemData.A = position1;
    itemData.B = 0.5f * (-vector3_1 + position2);
    itemData.C = vector3_1 - 2.5f * position1 + 2f * position2 - 0.5f * vector3_2;
    itemData.D = 0.5f * (-vector3_1 + 3f * position1 - 3f * position2 + vector3_2);
  }

  private void UpdateAdjacentSegments(int vertexIndex)
  {
    int count = this._data.Count;
    if (count < 2)
      return;
    int index1 = vertexIndex - 2;
    int index2 = vertexIndex - 1;
    int index3 = vertexIndex + 1;
    if (this._type == SplineTypes.Open)
    {
      int segmentCount = this.SegmentCount;
      if (index1 >= 0)
        this.UpdateSegment(index1);
      if (index2 >= 0)
        this.UpdateSegment(index2);
      if (vertexIndex >= 0 && vertexIndex < segmentCount)
        this.UpdateSegment(vertexIndex);
      if (index3 >= segmentCount)
        return;
      this.UpdateSegment(index3);
    }
    else
    {
      if (index1 < 0)
        index1 += count;
      if (index2 < 0)
        index2 += count;
      if (vertexIndex < 0)
        vertexIndex += count;
      if (index3 >= count)
        index3 -= count;
      this.UpdateSegment(index1);
      this.UpdateSegment(index2);
      this.UpdateSegment(vertexIndex);
      this.UpdateSegment(index3);
    }
  }

  public override void AddVertexFirst(Vector3 position)
  {
    this.InsertBefore(0, position);
  }

  public override void AddVertexLast(Vector3 position)
  {
    this._data.Add(new BaseSpline.ItemData()
    {
      Position = position
    });
    this.UpdateAdjacentSegments(this._data.Count - 1);
    this._recalcSegmentsLength = true;
  }

  public override void RemoveVertex(int index)
  {
    this._data.RemoveAt(index);
    --index;
    this.UpdateAdjacentSegments(index);
    this._recalcSegmentsLength = true;
  }

  public override void Clear()
  {
    this._data.Clear();
    this._recalcSegmentsLength = true;
  }

  public override void InsertBefore(int vertexIndex, Vector3 position)
  {
    this._data.Insert(vertexIndex, new BaseSpline.ItemData()
    {
      Position = position
    });
    this.UpdateAdjacentSegments(vertexIndex);
    this._recalcSegmentsLength = true;
  }

  public override void InsertAfter(int vertexIndex, Vector3 position)
  {
    this._data.Insert(vertexIndex + 1, new BaseSpline.ItemData()
    {
      Position = position
    });
    this.UpdateAdjacentSegments(vertexIndex + 1);
    this._recalcSegmentsLength = true;
  }

  public override Vector3 GetVertex(int vertexIndex)
  {
    return this._data[vertexIndex].Position;
  }

  public override void SetVertex(int vertexIndex, Vector3 position)
  {
    this._data[vertexIndex].Position = position;
    this.UpdateAdjacentSegments(vertexIndex);
    this._recalcSegmentsLength = true;
  }
}
