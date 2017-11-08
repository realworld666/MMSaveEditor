public class PoliticalImpactPromotionBonus : PoliticalImpact
{
    public bool active;

    public PoliticalImpactPromotionBonus(string inName, string inEffect)
    {
        if (inEffect == null)
            return;

        switch (inName)
        {
            case "Active":
                this.active = true;
                break;
            case "Inactive":
                this.active = false;
                break;
        }
    }

    public override void SetImpact(ChampionshipRules inRules)
    {
        inRules.PromotionBonus = this.active;
    }

    public override bool VoteCanBeUsed(Championship inChampionship)
    {
        return inChampionship.championshipBelowID != -1;
    }
}
