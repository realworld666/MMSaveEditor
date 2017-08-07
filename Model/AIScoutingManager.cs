using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class AIScoutingManager
{
    public readonly int maxCurrentScoutings = 4;
    private readonly int mBaseScoutingSlotsCount = 3;
    private int mUnlockedScoutingSlots;
    private int mMaxScoutingLevelFacility = 3;
    private Team mTeam;
    private List<AIScoutingManager.DriverScoutingEntry> mCurrentDriverScoutingEntries = new List<AIScoutingManager.DriverScoutingEntry>();
    private List<AIScoutingManager.DriverScoutingEntry> mQueuedDriverScoutingEntries = new List<AIScoutingManager.DriverScoutingEntry>();
    private List<AIScoutingManager.DriverCompletedScoutEntry> mDriverScoutingAssignmentsComplete = new List<AIScoutingManager.DriverCompletedScoutEntry>();
    private List<AIScoutingManager.MechanicScoutingEntry> mCurrentMechanicScoutingEntries = new List<AIScoutingManager.MechanicScoutingEntry>();
    private List<AIScoutingManager.MechanicScoutingEntry> mQueuedMechanicScoutingEntries = new List<AIScoutingManager.MechanicScoutingEntry>();
    private List<AIScoutingManager.MechanicCompletedScoutEntry> mMechanicScoutingAssignmentsComplete = new List<AIScoutingManager.MechanicCompletedScoutEntry>();
    private List<AIScoutingManager.EngineerScoutingEntry> mCurrentEngineerScoutingEntries = new List<AIScoutingManager.EngineerScoutingEntry>();
    private List<AIScoutingManager.EngineerScoutingEntry> mQueuedEngineerScoutingEntries = new List<AIScoutingManager.EngineerScoutingEntry>();
    private List<AIScoutingManager.EngineerCompletedScoutEntry> mEngineerScoutingAssignmentsComplete = new List<AIScoutingManager.EngineerCompletedScoutEntry>();
    private const int maxScoutingQueueSize = 5;


    private bool mIsValid;

    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public class DriverCompletedScoutEntry
    {
        public DateTime timeCompleted;
        public Driver driver;
    }

    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public class DriverScoutingEntry
    {
        public CalendarEvent_v1 calendarEvent;
        public int scoutingDays;
        public Driver driver;
    }

    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public class MechanicCompletedScoutEntry
    {
        public DateTime timeCompleted;
        public Mechanic mechanic;
    }

    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public class MechanicScoutingEntry
    {
        public CalendarEvent_v1 calendarEvent;
        public int scoutingDays;
        public Mechanic mechanic;
    }

    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public class EngineerCompletedScoutEntry
    {
        public DateTime timeCompleted;
        public Engineer engineer;
    }

    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public class EngineerScoutingEntry
    {
        public CalendarEvent_v1 calendarEvent;
        public int scoutingDays;
        public Engineer engineer;
    }
}
