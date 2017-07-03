public class PoliticalImpactSafetyCar : PoliticalImpact
{
    public PoliticalImpactSafetyCar.ImpactType impactType = PoliticalImpactSafetyCar.ImpactType.Both;
    public enum ImpactType
    {
        VirtualSafetyCar,
        SafetyCar,
        Both,
    }
}
