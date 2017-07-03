using System;
using System.Collections.Generic;
using FullSerializer;


[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ScoutingManager
{
    private readonly int mBaseScoutingSlotsCount = 3;
    private int mMaxScoutingLevelFacility = 3;
    private List<ScoutingManager.ScoutingEntry> mCurrentScoutingEntries = new List<ScoutingManager.ScoutingEntry>();
    private List<ScoutingManager.ScoutingEntry> mQueuedScoutingEntries = new List<ScoutingManager.ScoutingEntry>();
    private List<ScoutingManager.CompletedScoutEntry> mScoutingAssignmentsComplete = new List<ScoutingManager.CompletedScoutEntry>();
    private int mUnlockedScoutingSlots;

    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public class CompletedScoutEntry
    {
        public DateTime timeCompleted;
        public Driver driver;
    }

    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public class ScoutingEntry
    {
        public CalendarEvent_v1 calendarEvent;
        public int scoutingDays;
        public Driver driver;
    }
}
