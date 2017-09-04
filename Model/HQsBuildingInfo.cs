
using FullSerializer;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class HQsBuildingInfo
{
    public int buildingID;
    public HQsBuildingInfo.Type type;
    public HQsBuildingInfo.Category category;
    public string name = string.Empty;
    public string description = string.Empty;
    public string[] effects;
    public string[] effectsNextLevel;
    public int maxLevel;
    public int initialCost;
    public int staffSalary;
    public int buildTime;
    public List<HQsDependency> dependencies = new List<HQsDependency>();
    public Dictionary<int, Dictionary<int, List<PartTypeSlotSettings>>> sets = new Dictionary<int, Dictionary<int, List<PartTypeSlotSettings>>>();
    public Dictionary<int, List<CarPartComponent>> components = new Dictionary<int, List<CarPartComponent>>();
    public float[] upgradeCost;
    public float[] upkeepCost;
    public float[] income;
    public int[] upgradeTime;
    public int[] workerCapacity;
    private string nameID = string.Empty;
    private string descriptionID = string.Empty;
    private string mCustomName = string.Empty;
    private string mCustomDescription = string.Empty;

    public string LocalisedName
    {
        get
        {
            return name;
        }
    }

    public enum Type
    {
        DesignCentre,
        Factory,
        TelemetryCentre,
        TestTrack,
        WindTunnel,
        Simulator,
        BrakesResearchFacility,
        RideHandlingDevelopment,
        ScoutingFacility,
        StaffHousing,
        LogisticsCentre,
        RoadCarFactory,
        TourCentre,
        ThemePark,
        Helipad,
        Count,
    }

    public enum Category
    {
        [LocalisationID("PSG_10002076")] Design,
        [LocalisationID("PSG_10002576")] Factory,
        [LocalisationID("PSG_10001454")] Performance,
        [LocalisationID("PSG_10003781")] Staff,
        [LocalisationID("PSG_10004648")] Brand,
    }
}
