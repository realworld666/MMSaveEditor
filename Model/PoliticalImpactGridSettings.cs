public class PoliticalImpactGridSettings : PoliticalImpact
{
    public PoliticalImpactGridSettings.ImpactType impactType;


    public enum ImpactType
    {
        QualifyingBased,
        QualifyingBased3Sessions,
        Random,
        ReverseChampionshipStandings,
    }
}
