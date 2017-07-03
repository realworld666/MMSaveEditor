// Decompiled with JetBrains decompiler
// Type: SetRenderQueue
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SetRenderQueue : MonoBehaviour
{
  [SerializeField]
  private int renderQueue = 1800;

  private void Awake()
  {
    Renderer component = this.gameObject.GetComponent<Renderer>();
    if ((Object) component != (Object) null)
      component.material.renderQueue = this.renderQueue;
    else
      Debug.Log((object) "Trying to set render queue on object's renderer, but that object has no renderer", (Object) this.gameObject);
  }
}
