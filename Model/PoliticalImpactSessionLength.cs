public class PoliticalImpactSessionLength : PoliticalImpact
{
    public ChampionshipRules.SessionLength sessionLength;
    public PoliticalImpactSessionLength.ImpactType impactType;
    public enum ImpactType
    {
        PracticeSession,
        QualifyingSession,
        RaceSession,
    }
}
