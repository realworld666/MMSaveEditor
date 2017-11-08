
using System.Collections.Generic;

public class PoliticalImpactRaceStart : PoliticalImpact
{
    public ChampionshipRules.RaceStart raceStart;

    public PoliticalImpactRaceStart(string inName, string inEffect)
    {
        string key = inEffect;
        if (key == null)
            return;


        this.raceStart = ChampionshipRules.RaceStart.StandingStart;
    }

    public override void SetImpact(ChampionshipRules inRules)
    {
        inRules.raceStart = this.raceStart;
    }

    public override bool VoteCanBeUsed(Championship inChampionship)
    {
        return true;
    }
}
