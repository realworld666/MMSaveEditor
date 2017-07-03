using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class Calendar
{
  private List<CalendarEvent_v1> mDelayedEvents = new List<CalendarEvent_v1>();
  private DateTime mPreviousEventForTimeBar = new DateTime();
  private List<CalendarEvent_v1> mPastEvents = new List<CalendarEvent_v1>();
  private CalendarEventCategory mEventTypesToPauseOn;
  private bool mSortCalendarEvents;
}
