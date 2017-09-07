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

    public enum ImpactType
    {
        VirtualSafetyCar,
        SafetyCar,
        Both,
    }
}
