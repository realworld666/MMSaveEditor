public class PoliticalImpactDriverAids : PoliticalImpact
{
    public PoliticalImpactDriverAids.ImpactType impactType;

    public override void SetImpact(ChampionshipRules inRules)
    {
        switch (this.impactType)
        {
            case PoliticalImpactDriverAids.ImpactType.Active:
                inRules.DriverAidsOn = true;
                break;
            case PoliticalImpactDriverAids.ImpactType.Banned:
                inRules.DriverAidsOn = false;
                break;
        }
    }

    public enum ImpactType
    {
        Active,
        Banned,
    }
}
