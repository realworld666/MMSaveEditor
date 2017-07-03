// Decompiled with JetBrains decompiler
// Type: SceneSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class SceneSet
{
  public SceneSet.SceneSetType sceneSetType = SceneSet.SceneSetType.FrontEnd;
  private TextAsset scenesData;
  public string[] scenesList;
  private int scenesCount;
  public int scenesLoaded;

  public void Setup(SceneSet.SceneSetType inSceneSet, TextAsset inScenesData)
  {
    this.scenesData = inScenesData;
    this.sceneSetType = inSceneSet;
    this.scenesCount = 0;
    this.scenesList = this.scenesData.text.Split(new string[3]
    {
      "\r\n",
      "\r",
      "\n"
    }, StringSplitOptions.None);
    for (int index = 0; index < this.scenesList.Length; ++index)
    {
      if (!string.IsNullOrEmpty(this.scenesList[index]))
        ++this.scenesCount;
    }
  }

  public bool HasAllScenesLoaded()
  {
    return this.scenesLoaded == this.scenesCount;
  }

  public bool IsLoaded()
  {
    return this.scenesLoaded > 0;
  }

  public enum SceneSetType
  {
    Core,
    Title,
    Shared,
    FrontEnd,
    RaceEvent,
  }
}
