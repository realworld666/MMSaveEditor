// Decompiled with JetBrains decompiler
// Type: UIGraphRadarCircle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;

public class UIGraphRadarCircle : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
  public UIGraphRadar graph;
  private int mIndex;

  public void Setup(int inIndex)
  {
    this.mIndex = inIndex;
  }

  public void OnPointerEnter(PointerEventData eventData)
  {
    if (!((Object) this.graph != (Object) null) || !this.graph.displayLabel || (this.graph.graphData.Count <= 0 || this.graph.graphData[0].graphLabels.Count <= this.mIndex))
      return;
    if (!this.graph.radarLabel.isOn)
      this.graph.radarLabel.isOn = true;
    this.graph.radarLabel.SetText(this.graph.graphData[0].graphLabels[this.mIndex]);
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    if (!((Object) this.graph != (Object) null) || !this.graph.displayLabel)
      return;
    this.graph.radarLabel.isOn = false;
  }
}
