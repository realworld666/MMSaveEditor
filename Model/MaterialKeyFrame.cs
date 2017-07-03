// Decompiled with JetBrains decompiler
// Type: MaterialKeyFrame
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class MaterialKeyFrame : MonoBehaviour
{
  [SerializeField]
  private Material material;
  [SerializeField]
  private string variable;
  [SerializeField]
  private float value;
  public Color color;

  private void Update()
  {
    this.material.SetFloat(this.variable, this.value);
  }
}
