// Decompiled with JetBrains decompiler
// Type: Minimap_AdjustBounds
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[ExecuteInEditMode]
public class Minimap_AdjustBounds : MonoBehaviour
{
  [Tooltip("Copy and Paste Track Meshes Inside this GameObject, Activate Script and Press Calculate to Get Center / Size Values on Console, Insert Values Manually on the Box Collider, ignoring all Y Values, and then Delete the copied Track Meshes, Drag this GameObject to the CircuitScene Script - Track Layout Reference")]
  public string HoverForInstructions = string.Empty;
  public bool calculate;
  [SerializeField]
  public BoxCollider bCollider;

  private void Update()
  {
    if (!this.calculate)
      return;
    Bounds bounds = new Bounds();
    foreach (Renderer componentsInChild in this.gameObject.GetComponentsInChildren<MeshRenderer>())
      bounds.Encapsulate(componentsInChild.bounds);
    this.bCollider.size = bounds.size;
    this.bCollider.center = bounds.center;
    Debug.Log((object) "Box Collider Center", (Object) null);
    Debug.Log((object) bounds.center, (Object) null);
    Debug.Log((object) "Box Collider Size", (Object) null);
    Debug.Log((object) bounds.size, (Object) null);
    this.calculate = false;
  }
}
