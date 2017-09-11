
using System.Collections.Generic;

public class PoliticalImpactPrizePoolAdjustement : PoliticalImpact
{
    public List<float> prizePoolPercentage = new List<float>();
    private string inName;
    private string inEffect;

    public PoliticalImpactPrizePoolAdjustement(string inName, string inEffect)
    {
        string[] array = inEffect.Split(new char[]
        {
            ','
        });
        string[] array2 = array;
        for (int i = 0; i < array2.Length; i++)
        {
            string s = array2[i];
            float item = float.Parse(s);
            this.prizePoolPercentage.Add(item);
        }
    }

    public override void SetImpact(ChampionshipRules inRules)
    {
        inRules.PrizePoolPercentage.Clear();
        inRules.PrizePoolPercentage = new List<float>((IEnumerable<float>)this.prizePoolPercentage);
    }
}
