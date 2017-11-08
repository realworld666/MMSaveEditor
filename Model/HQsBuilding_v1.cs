using System;
using FullSerializer;
using System.Collections.Generic;
using System.Linq;

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
    private HQsBuilding_v1.SpecialState specialState;

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

    public HQsBuildingInfo Info
    {
        get { return info; }
        set { info = value; }
    }

    public int CurrentLevel
    {
        get { return currentLevel; }
        set { currentLevel = value; }
    }

    public SpecialState SpecialState1
    {
        get
        {
            return specialState;
        }

        set
        {
            specialState = value;
        }
    }
    public List<SpecialState> SpecialStateTypes
    {
        get
        {
            return new List<SpecialState>(new[] { SpecialState.None, SpecialState.BurnedDown });
        }
    }

    public HQsBuilding_v1.BuildingState BuildingState1
    {
        get
        {
            return state;
        }

        set
        {
            state = value;
        }
    }
    public IEnumerable<HQsBuilding_v1.BuildingState> BuildingStateTypes
    {
        get
        {
            return Enum.GetValues(typeof(HQsBuilding_v1.BuildingState)).Cast<HQsBuilding_v1.BuildingState>();
        }
    }

    public int MaxLevel
    {
        get
        {
            return info.maxLevel;
        }
    }

    public DateTime DateProgressStarted
    {
        get
        {
            return mDateProgressStarted;
        }

        set
        {
            mDateProgressStarted = value;
        }
    }

    public DateTime DateProgressEnd
    {
        get
        {
            return mDateProgressEnd;
        }

        set
        {
            mDateProgressEnd = value;
        }
    }
}
