using FullSerializer;
using System;

[fsObject("v0", new System.Type[] {}, MemberSerialization = fsMemberSerialization.OptOut)]
public class CalendarEvent
{
  public CalendarEvent.UIState uiState = CalendarEvent.UIState.None;
  public CalendarEventCategory category = CalendarEventCategory.Unknown;
  public DateTime triggerDate;
  public GameState.Type triggerState;
  public MMAction OnButtonClick;
  public MMAction OnEventTrigger;
  public Message message;
  public EventEffect effect;
  public DisplayEffect displayEffect;
  public string description;
  public bool mustRespond;
  public bool interruptGameTime;
  public bool showOnCalendar;

  public enum UIState
  {
    TimeBarFillTarget,
    None,
  }
}
