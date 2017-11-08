
public class PoliticalImpactPitlaneSpeed : PoliticalImpact
{
    private float pitlaneSpeed;

    public PoliticalImpactPitlaneSpeed(string inName, string inEffect)
    {
        this.pitlaneSpeed = GameUtility.MilesPerHourToMetersPerSecond(float.Parse(inEffect));
    }

    public override void SetImpact(ChampionshipRules inRules)
    {
        inRules.PitlaneSpeed = this.pitlaneSpeed;
    }
}
