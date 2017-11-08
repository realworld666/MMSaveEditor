
using System.Collections.Generic;

public class PoliticalImpactRaceType : PoliticalImpact
{
    public PoliticalImpactRaceType.ImpactType impactType;
    private int mSessionLengthInHours;

    public PoliticalImpactRaceType(string inName, string inEffect)
    {
        string key = inEffect;

        this.mSessionLengthInHours = int.Parse(inEffect);
        this.impactType = PoliticalImpactRaceType.ImpactType.Active;
    }

    public override void SetImpact(ChampionshipRules inRules)
    {
        switch (this.impactType)
        {
            case PoliticalImpactRaceType.ImpactType.Active:
                inRules.raceType = ChampionshipRules.RaceType.Time;
                inRules.raceLengthInHours = this.mSessionLengthInHours;
                break;
            case PoliticalImpactRaceType.ImpactType.Banned:
                inRules.raceType = ChampionshipRules.RaceType.Laps;
                break;
        }
    }

    public override bool VoteCanBeUsed(Championship inChampionship)
    {
        return true;
    }

    public enum ImpactType
    {
        Active,
        Banned,
    }
}
