// Decompiled with JetBrains decompiler
// Type: BaseSceneManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class BaseSceneManager
{
  public List<BaseScene> baseScenes = new List<BaseScene>();

  public void RegisterBaseScene(BaseScene inScene)
  {
    if (this.CheckBaseScene(inScene) != -1)
      return;
    this.baseScenes.Add(inScene);
  }

  public void UnregisterBaseScene(BaseScene inScene)
  {
    int index = this.CheckBaseScene(inScene);
    if (index < 0)
      return;
    this.baseScenes.RemoveAt(index);
  }

  public BaseScene[] GetActiveBaseScenes()
  {
    List<BaseScene> baseSceneList = new List<BaseScene>();
    int count = this.baseScenes.Count;
    for (int index = 0; index < count; ++index)
    {
      BaseScene baseScene = this.baseScenes[index];
      if (baseScene.gameObject.activeSelf)
        baseSceneList.Add(baseScene);
    }
    return baseSceneList.ToArray();
  }

  private int CheckBaseScene(BaseScene inListener)
  {
    int count = this.baseScenes.Count;
    for (int index = 0; index < count; ++index)
    {
      if ((Object) this.baseScenes[index] == (Object) inListener)
        return index;
    }
    return -1;
  }
}
