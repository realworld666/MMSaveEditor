// Decompiled with JetBrains decompiler
// Type: SoundCameraZoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SoundCameraZoom : MonoBehaviour
{
  public bool setSoundsVolume = true;
  [Range(0.0f, 1f)]
  public float closeZoomVolume = 1f;
  public Sound[] sounds;
  public bool invertVolume;
  [Range(0.0f, 1f)]
  public float farZoomVolume;
  private float mZoom;
  private float mVolume;

  public float volume
  {
    get
    {
      return this.mVolume;
    }
  }

  public float zoom
  {
    get
    {
      return this.mZoom;
    }
  }

  public void Update()
  {
    if (!Game.IsActive() || !((Object) App.instance.cameraManager.gameCamera != (Object) null))
      return;
    this.mZoom = App.instance.cameraManager.gameCamera.freeRoamCamera.zoomNormalized;
    this.mVolume = Mathf.Lerp(this.farZoomVolume, this.closeZoomVolume, !this.invertVolume ? this.mZoom : 1f - this.mZoom);
  }
}
