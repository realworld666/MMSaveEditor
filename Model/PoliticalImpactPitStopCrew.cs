public class PoliticalImpactPitStopCrew : PoliticalImpact
{
    public PoliticalImpactPitStopCrew.ImpactType impactType;

    public PoliticalImpactPitStopCrew(string inName, string inEffect)
    {
        string key = inEffect;
        if (key == null)
            return;
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
            case PoliticalImpactPitStopCrew.ImpactType.SemiSequential:
                inRules.PitCrewSize = ChampionshipRules.PitStopCrewSize.SemiSequential;
                break;
        }
    }

    public enum ImpactType
    {
        Small,
        Large,
        SemiSequential,
    }
}
