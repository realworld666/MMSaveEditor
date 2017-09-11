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

    public override void SetImpact(ChampionshipRules inRules)
    {
        int qualifyingSessionCount1 = inRules.GetQualifyingSessionCount();
        inRules.GridSetup1 = this.GetSetup(this.impactType);
        int qualifyingSessionCount2 = inRules.GetQualifyingSessionCount();
        if (qualifyingSessionCount1 == qualifyingSessionCount2)
            return;
        //inRules.championship.GenerateNextYearCalendar(false);
        //inRules.championship.SetupNextYearCalendarWeather();
    }

    public ChampionshipRules.GridSetup GetSetup(PoliticalImpactGridSettings.ImpactType inImpact)
    {
        switch (this.impactType)
        {
            case PoliticalImpactGridSettings.ImpactType.QualifyingBased3Sessions:
                return ChampionshipRules.GridSetup.QualifyingBased3Sessions;
            case PoliticalImpactGridSettings.ImpactType.Random:
                return ChampionshipRules.GridSetup.Random;
            case PoliticalImpactGridSettings.ImpactType.ReverseChampionshipStandings:
                return ChampionshipRules.GridSetup.InvertedDriverChampionship;
            default:
                return ChampionshipRules.GridSetup.QualifyingBased;
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
