// Decompiled with JetBrains decompiler
// Type: UILineGraphDataEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class UILineGraphDataEntry
{
  public List<Vector2> data = new List<Vector2>();
  public List<UILineGraphDot> dots = new List<UILineGraphDot>();
  public List<UIGraphLineConnector> lines = new List<UIGraphLineConnector>();
  public List<string> tooltipText = new List<string>();
  public Color color = new Color();

  public void Hide()
  {
    for (int index = 0; index < this.dots.Count; ++index)
      this.dots[index].gameObject.SetActive(false);
    for (int index = 0; index < this.lines.Count; ++index)
      this.lines[index].gameObject.SetActive(false);
  }

  public void Show()
  {
    for (int index = 0; index < this.dots.Count; ++index)
      this.dots[index].gameObject.SetActive(true);
    for (int index = 0; index < this.lines.Count; ++index)
      this.lines[index].gameObject.SetActive(true);
  }

  public void RemoveObject()
  {
    for (int index = 0; index < this.dots.Count; ++index)
      Object.Destroy((Object) this.dots[index].gameObject);
    for (int index = 0; index < this.lines.Count; ++index)
      Object.Destroy((Object) this.lines[index].gameObject);
  }
}
