public class PoliticalImpactSprinklers : PoliticalImpact
{
    public bool active;

    public PoliticalImpactSprinklers(string inName, string inEffect)
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
        inRules.IsSprinklingSystemOn = this.active;
    }

    public enum ImpactType
    {
        Small,
        Large,
    }
}
