// Decompiled with JetBrains decompiler
// Type: TrackGuideCamera
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class TrackGuideCamera : MonoBehaviour
{
  private GameCamera mGameCamera;

  public void SetGameCamera(GameCamera inGameCamera)
  {
    this.mGameCamera = inGameCamera;
  }

  private void OnEnable()
  {
    if (!((Object) this.mGameCamera != (Object) null))
      return;
    this.mGameCamera.SetTiltShiftActive(true);
  }

  private void Update()
  {
  }
}
