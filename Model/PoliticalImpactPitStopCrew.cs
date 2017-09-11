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

    public override void SetImpact(ChampionshipRules inRules)
    {
        switch (this.impactType)
        {
            case PoliticalImpactPitStopCrew.ImpactType.Small:
                inRules.PitCrewSize = ChampionshipRules.PitStopCrewSize.Small;
                break;
            case PoliticalImpactPitStopCrew.ImpactType.Large:
                inRules.PitCrewSize = ChampionshipRules.PitStopCrewSize.Large;
                break;
        }
    }

    public enum ImpactType
    {
        Small,
        Large,
    }
}
