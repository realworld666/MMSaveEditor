// Decompiled with JetBrains decompiler
// Type: MMFog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[ExecuteInEditMode]
public class MMFog : MonoBehaviour
{
  private static readonly string SHADER_NAME = "Custom/MMFog";
  private Material mMaterial;
  private Camera mCamera;
  public Texture LUTTex;
  public float FogDensity;

  private void Start()
  {
    this.mCamera = this.GetComponent<Camera>();
    this.mCamera.depthTextureMode = DepthTextureMode.Depth;
    this.mMaterial = new Material(Shader.Find(MMFog.SHADER_NAME));
  }

  private void OnRenderImage(RenderTexture inSource, RenderTexture inDestination)
  {
    this.mMaterial.SetTexture(MaterialPropertyHashes._LUTTEX, this.LUTTex);
    this.mMaterial.SetFloat(MaterialPropertyHashes._FogDensity, this.FogDensity);
    Graphics.Blit((Texture) inSource, inDestination, this.mMaterial);
  }
}
