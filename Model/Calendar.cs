using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class Calendar
{
    private static readonly int daysToKeepOldEventsInCalendar = 14;
    private static readonly Predicate<CalendarEvent_v1> oldEventRemovalPredicate;
    private List<CalendarEvent_v1> mDelayedEvents = new List<CalendarEvent_v1>();
    private DateTime mPreviousEventForTimeBar = new DateTime();
    private List<CalendarEvent_v1> mPastEvents = new List<CalendarEvent_v1>();
    private CalendarEventCategory mEventTypesToPauseOn;
    private bool mSortCalendarEvents;
}
