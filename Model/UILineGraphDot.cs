// Decompiled with JetBrains decompiler
// Type: UILineGraphDot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UILineGraphDot : MonoBehaviour
{
  public Vector2 dotData = new Vector2();
  private Vector2 mSize = new Vector2();
  public UILineGraph graph;
  public string tooltipText;

  private void Start()
  {
    EventTrigger eventTrigger = this.gameObject.AddComponent<EventTrigger>();
    EventTrigger.Entry entry1 = new EventTrigger.Entry();
    entry1.eventID = UnityEngine.EventSystems.EventTriggerType.PointerExit;
    entry1.callback.AddListener((UnityAction<BaseEventData>) (eventData => this.graph.HideTooltip()));
    entry1.callback.AddListener((UnityAction<BaseEventData>) (eventData => this.AnimateOff()));
    EventTrigger.Entry entry2 = new EventTrigger.Entry();
    entry2.eventID = UnityEngine.EventSystems.EventTriggerType.PointerEnter;
    entry2.callback.AddListener((UnityAction<BaseEventData>) (eventData => this.graph.ShowTooltip(this)));
    entry2.callback.AddListener((UnityAction<BaseEventData>) (eventData => this.Animate()));
    if (eventTrigger.get_triggers() == null)
      eventTrigger.set_triggers(new List<EventTrigger.Entry>());
    eventTrigger.get_triggers().Add(entry1);
    eventTrigger.get_triggers().Add(entry2);
  }

  public void AnimateOff()
  {
    this.gameObject.GetComponent<RectTransform>().sizeDelta = this.mSize;
  }

  public void Animate()
  {
    this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(20f, 20f);
  }

  public void SetColor(Color inColor)
  {
    this.gameObject.GetComponent<Image>().color = inColor;
  }

  public void SetPosition(Vector2 inSpacing)
  {
    this.transform.localPosition = (Vector3) inSpacing;
  }

  public void SetSize(Vector2 inSize)
  {
    RectTransform component = this.gameObject.GetComponent<RectTransform>();
    this.mSize = inSize;
    component.sizeDelta = inSize;
  }
}
