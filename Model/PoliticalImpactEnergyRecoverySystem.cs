public class PoliticalImpactEnergyRecoverySystem : PoliticalImpact
{
    private PoliticalImpactEnergyRecoverySystem.ImpactType impactType;


    private enum ImpactType
    {
        Banned,
        LargeBatteries,
        SmallBatteries,
        ActiveHybrid,
        InactiveHybrid,
    }
}
