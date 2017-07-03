// Decompiled with JetBrains decompiler
// Type: UIGraphHighlight
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIGraphHighlight : MonoBehaviour
{
  public int index;
  public UIGraphRadar graph;

  private void Start()
  {
    EventTrigger component = this.gameObject.GetComponent<EventTrigger>();
    EventTrigger.Entry entry1 = new EventTrigger.Entry();
    entry1.eventID = UnityEngine.EventSystems.EventTriggerType.PointerExit;
    entry1.callback.AddListener((UnityAction<BaseEventData>) (eventData => this.TurnOff()));
    EventTrigger.Entry entry2 = new EventTrigger.Entry();
    entry2.eventID = UnityEngine.EventSystems.EventTriggerType.PointerEnter;
    entry2.callback.AddListener((UnityAction<BaseEventData>) (eventData => this.TurnOn()));
    if (component.get_triggers() == null)
      component.set_triggers(new List<EventTrigger.Entry>());
    component.get_triggers().Add(entry1);
    component.get_triggers().Add(entry2);
    this.SetGraphCircleEvents();
  }

  private void SetGraphCircleEvents()
  {
    EventTrigger component = this.graph.GetHighlightObject(this.index).GetComponent<EventTrigger>();
    EventTrigger.Entry entry1 = new EventTrigger.Entry();
    entry1.eventID = UnityEngine.EventSystems.EventTriggerType.PointerExit;
    entry1.callback.AddListener((UnityAction<BaseEventData>) (eventData => this.TurnThisOff()));
    EventTrigger.Entry entry2 = new EventTrigger.Entry();
    entry2.eventID = UnityEngine.EventSystems.EventTriggerType.PointerEnter;
    entry2.callback.AddListener((UnityAction<BaseEventData>) (eventData => this.TurnThisOn()));
    if (component.get_triggers() == null)
      component.set_triggers(new List<EventTrigger.Entry>());
    component.get_triggers().Add(entry1);
    component.get_triggers().Add(entry2);
  }

  public void TurnThisOn()
  {
    this.gameObject.GetComponent<Button>().Select();
  }

  public void TurnThisOff()
  {
    this.DeselectObject();
  }

  public void TurnOn()
  {
    this.graph.Highlight(this.index, true);
  }

  public void TurnOff()
  {
    this.DeselectObject();
  }

  public void DeselectObject()
  {
    GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject((GameObject) null);
  }
}
