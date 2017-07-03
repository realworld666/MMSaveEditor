// Decompiled with JetBrains decompiler
// Type: DebugSimulationSession
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class DebugSimulationSession : MonoBehaviour
{
  public int carCount = 1;
  public float playbackSpeed = 2f;

  private void Awake()
  {
    App.instance.cameraManager.Start();
  }

  private void Start()
  {
    App.instance.cameraManager.ActivateMode(CameraManager.CameraMode.FreeRoam);
  }

  private void Update()
  {
  }
}
