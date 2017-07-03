// Decompiled with JetBrains decompiler
// Type: UIMouseOverColorChange
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UIMouseOverColorChange : MonoBehaviour
{
  public Color color = new Color();
  private Color mInitialColor = new Color();
  public Image image;

  private void Start()
  {
    this.mInitialColor = this.image.color;
  }

  private void OnMouseEnter()
  {
    this.image.color = this.color;
  }

  private void OnMouseExit()
  {
    this.image.color = this.mInitialColor;
  }
}
