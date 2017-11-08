
public class PoliticalImpactDrivingTime : PoliticalImpact
{
    private float mDrivingTime;

    public PoliticalImpactDrivingTime(string inName, string inEffect)
    {
        this.mDrivingTime = float.Parse(inEffect) / 100f;
    }

    public override void SetImpact(ChampionshipRules inRules)
    {
        inRules.drivingTimeEndurance = this.mDrivingTime;
    }
}
