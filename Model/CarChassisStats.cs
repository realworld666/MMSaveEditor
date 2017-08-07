using FullSerializer;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class CarChassisStats
{
    public static CarChassisStats.Stats[] carDesignScreenStats = new CarChassisStats.Stats[4] { CarChassisStats.Stats.TyreWear, CarChassisStats.Stats.TyreHeating, CarChassisStats.Stats.FuelEfficiency, CarChassisStats.Stats.Improvability };
    public Supplier supplierEngine = new Supplier();
    public Supplier supplierBrakes = new Supplier();
    public Supplier supplierFuel = new Supplier();
    public Supplier supplierMaterials = new Supplier();
    public Supplier supplierBattery = new Supplier();

    public float tyreWear;
    public float tyreHeating;
    public float improvability;
    public float fuelEfficiency;
    public float startingCharge;
    public float harvestEfficiency = 1f;

    public enum Stats
    {
        [LocalisationID("PSG_10004259")] TyreWear,
        [LocalisationID("PSG_10004260")] TyreHeating,
        [LocalisationID("PSG_10004256")] FuelEfficiency,
        [LocalisationID("PSG_10004258")] Improvability,
        [LocalisationID("PSG_10004258")] StartingCharge,
        [LocalisationID("PSG_10004258")] HarvestEfficiency,
        Count,
    }
}
