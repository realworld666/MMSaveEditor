// Decompiled with JetBrains decompiler
// Type: EventTriggerExample
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;

public class EventTriggerExample : EventTrigger
{
  public override void OnBeginDrag(PointerEventData data)
  {
    Debug.Log((object) "OnBeginDrag called.", (Object) null);
    this.DebugData(data);
  }

  public override void OnCancel(BaseEventData data)
  {
    Debug.Log((object) "OnCancel called.", (Object) null);
  }

  public override void OnDeselect(BaseEventData data)
  {
    Debug.Log((object) "OnDeselect called.", (Object) null);
  }

  public override void OnDrag(PointerEventData data)
  {
    Debug.Log((object) "OnDrag called.", (Object) null);
    this.DebugData(data);
  }

  public override void OnDrop(PointerEventData data)
  {
    Debug.Log((object) "OnDrop called.", (Object) null);
  }

  public override void OnEndDrag(PointerEventData data)
  {
    Debug.Log((object) "OnEndDrag called.", (Object) null);
  }

  public override void OnInitializePotentialDrag(PointerEventData data)
  {
    Debug.Log((object) "OnInitializePotentialDrag called.", (Object) null);
    this.DebugData(data);
  }

  public override void OnMove(AxisEventData data)
  {
    Debug.Log((object) "OnMove called.", (Object) null);
  }

  public override void OnPointerClick(PointerEventData data)
  {
    Debug.Log((object) "OnPointerClick called.", (Object) null);
  }

  public override void OnPointerDown(PointerEventData data)
  {
    Debug.Log((object) "OnPointerDown called.", (Object) null);
    this.DebugData(data);
  }

  public override void OnPointerEnter(PointerEventData data)
  {
    Debug.Log((object) "OnPointerEnter called.", (Object) null);
  }

  public override void OnPointerExit(PointerEventData data)
  {
    Debug.Log((object) "OnPointerExit called.", (Object) null);
  }

  public override void OnPointerUp(PointerEventData data)
  {
    Debug.Log((object) "OnPointerUp called.", (Object) null);
  }

  public override void OnScroll(PointerEventData data)
  {
    Debug.Log((object) "OnScroll called.", (Object) null);
  }

  public override void OnSelect(BaseEventData data)
  {
    Debug.Log((object) "OnSelect called.", (Object) null);
  }

  public override void OnSubmit(BaseEventData data)
  {
    Debug.Log((object) "OnSubmit called.", (Object) null);
  }

  public override void OnUpdateSelected(BaseEventData data)
  {
    Debug.Log((object) "OnUpdateSelected called.", (Object) null);
  }

  private void DebugData(PointerEventData data)
  {
    if (data == null)
      return;
    Debug.Log((object) ("<color=yellow>Dragging " + (object) data.dragging + " Delta " + (object) data.delta + " Pos " + (object) data.position + " Press Pos " + (object) data.pressPosition + "</color>"), (Object) null);
  }
}
