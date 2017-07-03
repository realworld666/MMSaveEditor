// Decompiled with JetBrains decompiler
// Type: CoreLod
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class CoreLod : MonoBehaviour
{
  public float CollapseDistance = 50f;
  public float HideDistance = 1000f;
  private List<Renderer> mChildObjects = new List<Renderer>();
  private Bounds mBounds = new Bounds();
  private MeshRenderer mCollapsedRenderer;
  private bool mHasBounds;
  private CoreLod.LodDrawMode mCurrentDrawMode;

  private void Start()
  {
    if (this.transform.childCount == 0)
      return;
    Transform child = this.transform.FindChild("LOD_Collapsed");
    if ((Object) child != (Object) null)
    {
      this.mCollapsedRenderer = child.gameObject.GetComponent<MeshRenderer>();
      if ((Object) this.mCollapsedRenderer == (Object) null)
      {
        Debug.LogWarning((object) string.Format("Couldn't find child 'LOD_Collapsed' for group {0}. Did you forget to build it?", (object) this.gameObject.name), (Object) null);
        return;
      }
    }
    this.mChildObjects.Capacity = this.transform.childCount;
    for (int index1 = 0; index1 < this.transform.childCount; ++index1)
    {
      GameObject gameObject = this.transform.GetChild(index1).gameObject;
      MeshRenderer component1 = gameObject.GetComponent<MeshRenderer>();
      LODGroup component2 = gameObject.GetComponent<LODGroup>();
      if (!((Object) gameObject == (Object) child.gameObject))
      {
        if ((Object) component1 != (Object) null)
        {
          this.AddBounds(component1.bounds);
          this.mChildObjects.Add((Renderer) component1);
        }
        else if ((Object) component2 != (Object) null)
        {
          foreach (LOD loD in component2.GetLODs())
          {
            Renderer[] renderers = loD.renderers;
            for (int index2 = 0; index2 < renderers.Length; ++index2)
            {
              this.AddBounds(renderers[index2].bounds);
              this.mChildObjects.Add(renderers[index2]);
            }
          }
        }
      }
    }
    CoreLodManager componentInParent = this.GetComponentInParent<CoreLodManager>();
    if (!((Object) componentInParent != (Object) null))
      return;
    componentInParent.RegisterForUpdate(this);
  }

  public void UpdateLOD(ref Camera camera)
  {
    if (this.mChildObjects.Count == 0)
      return;
    float num1 = this.mBounds.SqrDistance(camera.transform.position);
    float num2 = this.CollapseDistance * this.CollapseDistance;
    float num3 = this.HideDistance * this.HideDistance;
    CoreLod.LodDrawMode inDrawMode = CoreLod.LodDrawMode.Hidden;
    if ((double) num1 < (double) num2)
      inDrawMode = CoreLod.LodDrawMode.High;
    else if ((double) num1 < (double) num3)
      inDrawMode = CoreLod.LodDrawMode.Collapsed;
    if (inDrawMode == this.mCurrentDrawMode)
      return;
    this.SetCurrentDrawMode(inDrawMode);
  }

  private void SetCurrentDrawMode(CoreLod.LodDrawMode inDrawMode)
  {
    switch (this.mCurrentDrawMode)
    {
      case CoreLod.LodDrawMode.High:
        for (int index = 0; index < this.mChildObjects.Count; ++index)
          this.mChildObjects[index].enabled = false;
        break;
      case CoreLod.LodDrawMode.Collapsed:
        this.mCollapsedRenderer.enabled = false;
        break;
    }
    switch (inDrawMode)
    {
      case CoreLod.LodDrawMode.High:
        for (int index = 0; index < this.mChildObjects.Count; ++index)
          this.mChildObjects[index].enabled = true;
        break;
      case CoreLod.LodDrawMode.Collapsed:
        this.mCollapsedRenderer.enabled = true;
        break;
    }
    this.mCurrentDrawMode = inDrawMode;
  }

  private void AddBounds(Bounds inBounds)
  {
    if (!this.mHasBounds)
    {
      this.mBounds = inBounds;
      this.mHasBounds = true;
    }
    else
      this.mBounds.Encapsulate(inBounds);
  }

  private void DebugDrawBounds()
  {
    Color color = Color.blue;
    switch (this.mCurrentDrawMode)
    {
      case CoreLod.LodDrawMode.High:
        color = Color.red;
        break;
      case CoreLod.LodDrawMode.Collapsed:
        color = Color.green;
        break;
    }
    Vector3 min = this.mBounds.min;
    Vector3 max = this.mBounds.max;
    Vector3 start1 = min;
    Vector3 start2 = max;
    Vector3 vector3_1 = new Vector3(max.x, min.y, min.z);
    Vector3 vector3_2 = new Vector3(min.x, max.y, min.z);
    Vector3 vector3_3 = new Vector3(min.x, min.y, max.z);
    Vector3 end1 = new Vector3(max.x, max.y, min.z);
    Vector3 end2 = new Vector3(max.x, min.y, max.z);
    Vector3 end3 = new Vector3(min.x, max.y, max.z);
    Debug.DrawLine(start1, vector3_1, color);
    Debug.DrawLine(start1, vector3_2, color);
    Debug.DrawLine(start1, vector3_3, color);
    Debug.DrawLine(vector3_2, end1, color);
    Debug.DrawLine(vector3_2, end3, color);
    Debug.DrawLine(vector3_1, end1, color);
    Debug.DrawLine(vector3_1, end2, color);
    Debug.DrawLine(vector3_3, end3, color);
    Debug.DrawLine(vector3_3, end2, color);
    Debug.DrawLine(start2, end3, color);
    Debug.DrawLine(start2, end2, color);
    Debug.DrawLine(start2, end1, color);
  }

  private enum LodDrawMode
  {
    High,
    Collapsed,
    Hidden,
  }
}
