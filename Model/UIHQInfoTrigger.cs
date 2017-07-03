// Decompiled with JetBrains decompiler
// Type: UIHQInfoTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;

public class UIHQInfoTrigger : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
  public UIHQInfo widget;

  public void OnPointerEnter(PointerEventData inEventData)
  {
    if (!((Object) this.widget != (Object) null) || this.widget.toggle.isOn)
      return;
    this.widget.Highlight(true);
  }

  public void OnPointerExit(PointerEventData inEventData)
  {
    if (!((Object) this.widget != (Object) null) || this.widget.toggle.isOn)
      return;
    this.widget.Highlight(false);
  }
}
