public class PoliticalImpactSafetyCar : PoliticalImpact
{
    public PoliticalImpactSafetyCar.ImpactType impactType = PoliticalImpactSafetyCar.ImpactType.Both;
    private string inName;
    private string inEffect;

    public PoliticalImpactSafetyCar(string inName, string inEffects)
    {
        switch (inEffects)
        {
            case "Virtual":
                this.impactType = PoliticalImpactSafetyCar.ImpactType.VirtualSafetyCar;
                break;
            case "Real":
                this.impactType = PoliticalImpactSafetyCar.ImpactType.SafetyCar;
                break;
            case "Both":
                this.impactType = PoliticalImpactSafetyCar.ImpactType.Both;
                break;
        }
    }

    public override void SetImpact(ChampionshipRules inRules)
    {
        switch (this.impactType)
        {
            case PoliticalImpactSafetyCar.ImpactType.VirtualSafetyCar:
                inRules.SafetyCarUsage1 = ChampionshipRules.SafetyCarUsage.VirtualSafetyCar;
                break;
            case PoliticalImpactSafetyCar.ImpactType.SafetyCar:
                inRules.SafetyCarUsage1 = ChampionshipRules.SafetyCarUsage.RealSafetyCar;
                break;
            case PoliticalImpactSafetyCar.ImpactType.Both:
                inRules.SafetyCarUsage1 = ChampionshipRules.SafetyCarUsage.Both;
                break;
        }
    }

    public enum ImpactType
    {
        VirtualSafetyCar,
        SafetyCar,
        Both,
    }
}
