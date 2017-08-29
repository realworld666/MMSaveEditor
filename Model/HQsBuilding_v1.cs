using System;
using FullSerializer;

[fsObject("v1", new System.Type[] { typeof(HQsBuilding) }, MemberSerialization = fsMemberSerialization.OptOut)]
public class HQsBuilding_v1 : Entity
{
    public static Action<HQsBuilding_v1.NotificationState, HQsBuilding_v1> OnBuildingNotification = (Action<HQsBuilding_v1.NotificationState, HQsBuilding_v1>)null;
    public static float[] designCentrePartDaysPerLevel = new float[4] { -0.1f, -0.3f, -0.6f, -1f };
    public Action<HQsBuilding_v1.NotificationState> OnNotification;
    public Team team;
    public int currentLevel;
    public float normalizedProgress;
    public HQsBuilding_v1.BuildingState state;
    public HQsBuildingInfo info;
    public HQsBuildingCosts costs = new HQsBuildingCosts();
    private int mStaffNumber;
    private DateTime mDateProgressStarted;
    private DateTime mDateProgressEnd;
    public HQsBuilding_v1.SpecialState specialState;

    public HQsBuilding_v1()
    {
    }

    public HQsBuilding_v1(HQsBuildingInfo inBuilding)
    {
        this.info = inBuilding;
        this.name = this.info.name;
        // this.costs.Start(this);
    }

    public HQsBuilding_v1(HQsBuilding v0)
    {
        this.team = v0.team;
        this.currentLevel = v0.currentLevel;
        this.normalizedProgress = v0.normalizedProgress;
        this.state = v0.state;
        this.info = v0.info;
        this.costs = v0.costs;
        this.mStaffNumber = v0.mStaffNumber;
        this.mDateProgressStarted = v0.mDateProgressStarted;
        this.mDateProgressEnd = v0.mDateProgressEnd;
    }

    public enum NotificationState
    {
        UnLocked,
        BuildStarted,
        BuildComplete,
        UpgradeStarted,
        UpgradeComplete,
        Demolish,
    }

    public enum BuildingState
    {
        NotBuilt,
        BuildingInProgress,
        Constructed,
        Upgrading,
    }



    public enum SpecialState
    {
        None,
        BurnedDown,
        Count,
    }
}
