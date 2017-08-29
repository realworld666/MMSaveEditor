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

    private float mHarvestEfficiency;
    public const float maxSetupStatContribution = 0.5f;
    private float mTyreWear;
    private float mTyreHeating;
    private float mImprovability;
    private float mFuelEfficiency;
    private float mStartingCharge;
    private float mSetupTyreWear;
    private float mSetupTyreHeating;

    public float improvability
    {
        get
        {
            return this.GetStat(CarChassisStats.Stats.Improvability, true);
        }
        set
        {
            this.mImprovability = value;
        }
    }

    public float GetStat(CarChassisStats.Stats inStat, bool inWithNotBonus = true)
    {
        switch (inStat)
        {
            case CarChassisStats.Stats.TyreWear:
                if (inWithNotBonus)
                    return this.mTyreWear + this.mSetupTyreWear;
                return this.mTyreWear;
            case CarChassisStats.Stats.TyreHeating:
                if (inWithNotBonus)
                    return this.mTyreHeating + this.mSetupTyreHeating;
                return this.mTyreHeating;
            case CarChassisStats.Stats.FuelEfficiency:
                return this.mFuelEfficiency;
            case CarChassisStats.Stats.Improvability:
                return this.mImprovability;
            case CarChassisStats.Stats.StartingCharge:
                return this.mStartingCharge;
            case CarChassisStats.Stats.HarvestEfficiency:
                return this.mHarvestEfficiency;
            default:
                return 0.0f;
        }
    }

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
