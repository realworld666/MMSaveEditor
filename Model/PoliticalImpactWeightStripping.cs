
using System.Collections.Generic;

public class PoliticalImpactWeightStripping : PoliticalImpact
{
    private bool mEnabled;

    public PoliticalImpactWeightStripping(string inName, string inEffect)
    {
        string key = inEffect;
        if (key == null)
            return;

    }

    public override void SetImpact(ChampionshipRules inRules)
    {
        inRules.isWeightStrippingEnabled = this.mEnabled;
    }
}
