// Decompiled with JetBrains decompiler
// Type: MainCameraRender
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class MainCameraRender : MonoBehaviour
{
  [SerializeField]
  private RenderTexture texture;

  private void Start()
  {
  }

  private void OnRenderImage(RenderTexture inSource, RenderTexture inDestination)
  {
    Graphics.Blit((Texture) inSource, inDestination);
    Graphics.Blit((Texture) inDestination, this.texture);
  }
}
