public class PoliticalImpactEnergyRecoverySystem : PoliticalImpact
{
    private PoliticalImpactEnergyRecoverySystem.ImpactType impactType;
    private string inName;
    private string inEffect;

    public PoliticalImpactEnergyRecoverySystem(string inName, string inEffect)
    {
        switch (inEffect)
        {
            case "Banned":
                this.impactType = PoliticalImpactEnergyRecoverySystem.ImpactType.Banned;
                break;
            case "LargeBatteries":
                this.impactType = PoliticalImpactEnergyRecoverySystem.ImpactType.LargeBatteries;
                break;
            case "SmallBatteries":
                this.impactType = PoliticalImpactEnergyRecoverySystem.ImpactType.SmallBatteries;
                break;
            case "ActiveHybridPower":
                this.impactType = PoliticalImpactEnergyRecoverySystem.ImpactType.ActiveHybrid;
                break;
            case "InactiveHybridPower":
                this.impactType = PoliticalImpactEnergyRecoverySystem.ImpactType.InactiveHybrid;
                break;
            case "ChargeFromStandingsActive":
                this.impactType = PoliticalImpactEnergyRecoverySystem.ImpactType.ActiveChargeFromStandings;
                break;
            case "ChargeFromStandingsInactive":
                this.impactType = PoliticalImpactEnergyRecoverySystem.ImpactType.InactiveChargeFromStandings;
                break;
        }
    }

    private enum ImpactType
    {
        Banned,
        LargeBatteries,
        SmallBatteries,
        ActiveHybrid,
        InactiveHybrid,
        ActiveChargeFromStandings,
        InactiveChargeFromStandings
    }
}
