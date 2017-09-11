
public abstract class PoliticalImpact
{
    public virtual bool VoteCanBeUsed(Championship inChampionship)
    {
        return true;
    }

    public abstract void SetImpact(ChampionshipRules inRules);
}
