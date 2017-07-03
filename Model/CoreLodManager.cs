// Decompiled with JetBrains decompiler
// Type: CoreLodManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class CoreLodManager : MonoBehaviour
{
  private List<CoreLod> mCoreLODs = new List<CoreLod>();

  public void RegisterForUpdate(CoreLod core_lod)
  {
    if (!((Object) core_lod != (Object) null))
      return;
    this.mCoreLODs.Add(core_lod);
  }

  private void Update()
  {
    int allCamerasCount = Camera.allCamerasCount;
    int count = this.mCoreLODs.Count;
    if (allCamerasCount <= 0 || count <= 0)
      return;
    Camera camera = App.instance.cameraManager.GetCamera();
    if (!((Object) camera != (Object) null))
      return;
    for (int index = 0; index < count; ++index)
      this.mCoreLODs[index].UpdateLOD(ref camera);
  }
}
