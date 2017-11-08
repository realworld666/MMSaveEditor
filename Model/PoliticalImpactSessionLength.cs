using System;

public class PoliticalImpactSessionLength : PoliticalImpact
{
    public ChampionshipRules.SessionLength sessionLength;
    public PoliticalImpactSessionLength.ImpactType impactType;

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

    public override void SetImpact(ChampionshipRules inRules)
    {
        switch (this.impactType)
        {
            case PoliticalImpactSessionLength.ImpactType.PracticeSession:
                inRules.practiceSettings = Game.instance.simulationSettingsManager.practiceSettings[this.sessionLength];
                break;
            case PoliticalImpactSessionLength.ImpactType.QualifyingSession:
                inRules.qualifyingSettings = Game.instance.simulationSettingsManager.qualifyingSettings[this.sessionLength];
                break;
            case PoliticalImpactSessionLength.ImpactType.RaceSession:
                inRules.raceSettings = Game.instance.simulationSettingsManager.raceSettings[this.sessionLength];
                break;
        }
        inRules.ApplySimulationSettings();
    }

    public override bool VoteCanBeUsed(Championship inChampionship)
    {
        return this.impactType != PoliticalImpactSessionLength.ImpactType.QualifyingSession || (inChampionship.Rules.GridSetup1 == ChampionshipRules.GridSetup.QualifyingBased || inChampionship.Rules.GridSetup1 == ChampionshipRules.GridSetup.QualifyingBased3Sessions);
    }

    public enum ImpactType
    {
        PracticeSession,
        QualifyingSession,
        RaceSession,
    }
}
