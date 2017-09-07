public class PoliticalImpactGridSettings : PoliticalImpact
{
    public PoliticalImpactGridSettings.ImpactType impactType;

    public PoliticalImpactGridSettings(string inName, string inEffects)
    {
        switch (inEffects)
        {
            case "Qualifying":
                this.impactType = PoliticalImpactGridSettings.ImpactType.QualifyingBased;
                break;
            case "Qualifying3Sessions":
                this.impactType = PoliticalImpactGridSettings.ImpactType.QualifyingBased3Sessions;
                break;
            case "Random":
                this.impactType = PoliticalImpactGridSettings.ImpactType.Random;
                break;
            case "ReversedDC":
                this.impactType = PoliticalImpactGridSettings.ImpactType.ReverseChampionshipStandings;
                break;
        }
    }

    public enum ImpactType
    {
        QualifyingBased,
        QualifyingBased3Sessions,
        Random,
        ReverseChampionshipStandings,
    }
}
