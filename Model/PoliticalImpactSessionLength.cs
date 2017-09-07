using System;

public class PoliticalImpactSessionLength : PoliticalImpact
{
    public ChampionshipRules.SessionLength sessionLength;
    public PoliticalImpactSessionLength.ImpactType impactType;
    private string inName;
    private string inEffect;

    public PoliticalImpactSessionLength(string inName, string inEffects)
    {
        this.sessionLength = (ChampionshipRules.SessionLength)((int)Enum.Parse(typeof(ChampionshipRules.SessionLength), inEffects));
        switch (inName)
        {
            case "Practice":
                this.impactType = PoliticalImpactSessionLength.ImpactType.PracticeSession;
                break;
            case "Qualifying":
                this.impactType = PoliticalImpactSessionLength.ImpactType.QualifyingSession;
                break;
            case "Race":
                this.impactType = PoliticalImpactSessionLength.ImpactType.RaceSession;
                break;
        }
    }

    public enum ImpactType
    {
        PracticeSession,
        QualifyingSession,
        RaceSession,
    }
}
