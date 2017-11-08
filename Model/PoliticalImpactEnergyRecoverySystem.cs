public class PoliticalImpactEnergyRecoverySystem : PoliticalImpact
{
    [LocalisationID("PSG_asjhdashjdas")]
    private PoliticalImpactEnergyRecoverySystem.ImpactType impactType;

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

    public override void SetImpact(ChampionshipRules inRules)
    {
        switch (this.impactType)
        {
            case PoliticalImpactEnergyRecoverySystem.ImpactType.Banned:
                inRules.IsEnergySystemActive = false;
                break;
            case PoliticalImpactEnergyRecoverySystem.ImpactType.LargeBatteries:
                inRules.IsEnergySystemActive = true;
                inRules.BatterySize = ChampionshipRules.EnergySystemBattery.Large;
                break;
            case PoliticalImpactEnergyRecoverySystem.ImpactType.SmallBatteries:
                inRules.IsEnergySystemActive = true;
                inRules.BatterySize = ChampionshipRules.EnergySystemBattery.Small;
                break;
            case PoliticalImpactEnergyRecoverySystem.ImpactType.ActiveHybrid:
                inRules.IsHybridModeActive = true;
                break;
            case PoliticalImpactEnergyRecoverySystem.ImpactType.InactiveHybrid:
                inRules.IsHybridModeActive = false;
                break;
            case PoliticalImpactEnergyRecoverySystem.ImpactType.ActiveChargeFromStandings:
                inRules.ShouldChargeUsingStandingsPosition = true;
                break;
            case PoliticalImpactEnergyRecoverySystem.ImpactType.InactiveChargeFromStandings:
                inRules.ShouldChargeUsingStandingsPosition = false;
                break;
        }
    }

    public override bool VoteCanBeUsed(Championship inChampionship)
    {
        switch (this.impactType)
        {
            case PoliticalImpactEnergyRecoverySystem.ImpactType.ActiveChargeFromStandings:
            case PoliticalImpactEnergyRecoverySystem.ImpactType.InactiveChargeFromStandings:
                //if (App.instance.dlcManager.IsDlcWithIdInstalled(4))
                return inChampionship.Rules.IsEnergySystemActive;
                return false;
            default:
                //if (!App.instance.dlcManager.IsSeriesAvailable(Championship.Series.GTSeries))
                //  return false;
                switch (this.impactType)
                {
                    case PoliticalImpactEnergyRecoverySystem.ImpactType.ActiveHybrid:
                    case PoliticalImpactEnergyRecoverySystem.ImpactType.InactiveHybrid:
                        return inChampionship.Rules.IsEnergySystemActive;
                    default:
                        return true;
                }
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
