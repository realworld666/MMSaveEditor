
using System;
using FullSerializer;

[fsObject("v0", new System.Type[] { }, MemberSerialization = fsMemberSerialization.OptOut)]
public class HQsBuilding
{
    public HQsBuildingCosts costs = new HQsBuildingCosts();
    [NonSerialized]
    public TimeSpan mTimeRemaining = TimeSpan.Zero;
    [NonSerialized]
    public TimeSpan mTimeElapsed = TimeSpan.Zero;
    [NonSerialized]
    public TimeSpan mTotalTime = TimeSpan.Zero;
    public Action<HQsBuilding_v1.NotificationState> OnNotification;
    public Team team;
    public int currentLevel;
    public float normalizedProgress;
    public HQsBuilding_v1.BuildingState state;
    public HQsBuildingInfo info;
    public int mStaffNumber;
    public DateTime mDateProgressStarted;
    public DateTime mDateProgressEnd;
    [NonSerialized]
    public Notification mNotificationProgress;
    public DriverStatsProgression mDriverStatProgression;
}
