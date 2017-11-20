
using FullSerializer;
using System.Collections.Generic;


[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class Supplier
{
    private readonly float mPriceMultiplier = 0.4f;
    private readonly float mScalar = 1000000f;
    public Supplier.SupplierType supplierType = Supplier.SupplierType.Brakes;
    public string name = string.Empty;
    public int id;
    public int logoIndex;
    public int minEngineLevelModifier;
    public int maxEngineLevelModifier;
    public float minHarvestEfficiencyModifier;
    public float maxHarvestEfficiencyModifier;
    public Dictionary<CarChassisStats.Stats, float> supplierStats = new Dictionary<CarChassisStats.Stats, float>();
    public Dictionary<Supplier.CarAspect, float> carAspectMinBoundary = new Dictionary<Supplier.CarAspect, float>();
    public Dictionary<Supplier.CarAspect, float> carAspectMaxBoundary = new Dictionary<Supplier.CarAspect, float>();

    private int mBasePrice;
    private int mTier;
    private Dictionary<int, float> teamDiscounts = new Dictionary<int, float>();
    private List<int> mTeamsThatCannotBuy = new List<int>();
    private int mRandomEngineLevelModifier;
    private float mRandomHarvestEfficiencyModifier;
    public string descriptionID = string.Empty;
    public int hybridGates;
    public int powerGates;
    public int chargeSize;
    public Supplier.AdvancedERSBatteryType advancedBatteryType;

    public enum SupplierType
    {
        [LocalisationID("PSG_10004263")] Engine,
        [LocalisationID("PSG_10010289")] Brakes,
        [LocalisationID("PSG_10004264")] Fuel,
        [LocalisationID("PSG_10004261")] Materials,
        [LocalisationID("PSG_10011517")] Battery,
        [LocalisationID("PSG_10011517")] ERSAdvanced,
    }

    public enum CarAspect
    {
        [LocalisationID("PSG_10008693")] RearPackage,
        [LocalisationID("PSG_10008694")] NoseHeight,
    }

    public enum AdvancedERSBatteryType
    {
        None,
        [LocalisationID("PSG_10013889")] Flywheel,
        [LocalisationID("PSG_10013891")] Battery,
        [LocalisationID("PSG_10013890")] SuperCapacitor,
    }
}
