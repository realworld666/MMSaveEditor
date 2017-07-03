// Decompiled with JetBrains decompiler
// Type: GameCameraManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class GameCameraManager
{
  public List<GameCamera> cameras = new List<GameCamera>();

  public void RegisterCamera(GameCamera inCamera)
  {
    if (this.CheckCamera(inCamera) != -1)
      return;
    this.cameras.Add(inCamera);
  }

  public void UnregisterCamera(GameCamera inCamera)
  {
    int index = this.CheckCamera(inCamera);
    if (index < 0)
      return;
    this.cameras.RemoveAt(index);
  }

  public void SimulationUpdate()
  {
    int count = this.cameras.Count;
    for (int index = 0; index < count; ++index)
      this.cameras[index].SimulationUpdate();
  }

  public GameCamera GetFirstActiveCamera()
  {
    int count = this.cameras.Count;
    for (int index = 0; index < count; ++index)
    {
      if (this.cameras[index].gameObject.activeSelf)
        return this.cameras[index];
    }
    return (GameCamera) null;
  }

  public GameCamera[] GetActiveCameras()
  {
    List<GameCamera> gameCameraList = new List<GameCamera>();
    int count = this.cameras.Count;
    for (int index = 0; index < count; ++index)
    {
      GameCamera camera = this.cameras[index];
      if (camera.gameObject.activeSelf)
        gameCameraList.Add(camera);
    }
    return gameCameraList.ToArray();
  }

  private int CheckCamera(GameCamera inCamera)
  {
    int count = this.cameras.Count;
    for (int index = 0; index < count; ++index)
    {
      if ((Object) this.cameras[index] == (Object) inCamera)
        return index;
    }
    return -1;
  }
}
