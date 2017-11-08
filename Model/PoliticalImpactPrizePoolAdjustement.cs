
using MMSaveEditor.Utils;
using System;
using System.Collections.Generic;

public class PoliticalImpactPrizePoolAdjustement : PoliticalImpact
{
    public EasingUtility.Easing curveType;
    public float poolPercentageSplit01;

    public PoliticalImpactPrizePoolAdjustement(string inName, string inEffect)
    {
        string[] strArray = inEffect.Split(',');
        this.poolPercentageSplit01 = (float.Parse(strArray[1]) / 100f).Clamp(0f, 1f);
        this.curveType = (EasingUtility.Easing)Enum.Parse(typeof(EasingUtility.Easing), strArray[2]);
    }

    public override void SetImpact(ChampionshipRules inRules)
    {
        List<float> floatList = new List<float>();
        int teamEntryCount = inRules.championship.standings.teamEntryCount;
        int prizeFund = inRules.championship.prizeFund;
        float num1 = (1f - this.poolPercentageSplit01).Clamp(0f, 1f) * (float)prizeFund / (float)teamEntryCount;
        float num2 = this.poolPercentageSplit01 * (float)prizeFund;
        float num3 = 0.0f;
        for (int index = 0; index < teamEntryCount; ++index)
        {
            float num4 = EasingUtility.EaseByType(this.curveType, 0.0f, 1f, 1f - ((float)index / (float)teamEntryCount).Clamp(0f, 1f));
            num3 += num4;
        }
        float num5 = 1f / num3;
        for (int index = 0; index < teamEntryCount; ++index)
        {
            float num4 = EasingUtility.EaseByType(this.curveType, 0.0f, 1f, 1f - ((float)index / (float)teamEntryCount).Clamp(0f, 1f));
            float num6 = ((num1 + num2 * num4 * num5) / (float)prizeFund).Clamp(0f, 1f) * 100f;
            floatList.Add(num6);
        }
        inRules.prizePoolPercentage.Clear();
        inRules.prizePoolPercentage = new List<float>((IEnumerable<float>)floatList);
    }
}
