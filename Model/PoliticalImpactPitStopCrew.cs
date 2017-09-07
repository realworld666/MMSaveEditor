public class PoliticalImpactPitStopCrew : PoliticalImpact
{
    public PoliticalImpactPitStopCrew.ImpactType impactType;
    private string inName;
    private string inEffect;

    public PoliticalImpactPitStopCrew(string inName, string inEffect)
    {
        this.inName = inName;
        this.inEffect = inEffect;
    }

    public enum ImpactType
    {
        Small,
        Large,
    }
}
