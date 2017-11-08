
using System.Collections.Generic;

public class PoliticalImpactPoints : PoliticalImpact
{
    private List<int> points = new List<int>();
    private PoliticalImpactPoints.ImpactType impactType;
    private bool remove;

    public PoliticalImpactPoints(string inName, string inEffect)
    {
        if (inEffect == "Remove")
        {
            this.remove = true;
        }
        switch (inName)
        {
            case "FinalRacePointsDoubled":
                this.impactType = PoliticalImpactPoints.ImpactType.FinalRacePoints;
                break;
            case "FastestLapBonus":
                this.impactType = PoliticalImpactPoints.ImpactType.FastestLap;
                break;
            case "PoleBonus":
                this.impactType = PoliticalImpactPoints.ImpactType.PoleBonus;
                break;
            case "Points":
                if (inEffect == "All")
                {
                    this.impactType = PoliticalImpactPoints.ImpactType.Points;
                    int num2 = 20;
                    for (int i = num2; i > 0; i--)
                    {
                        this.points.Add(i);
                    }
                }
                else
                {
                    this.impactType = PoliticalImpactPoints.ImpactType.Points;
                    string[] array = inEffect.Split(new char[]
                    {
                    ','
                    });
                    string[] array2 = array;
                    for (int j = 0; j < array2.Length; j++)
                    {
                        string s = array2[j];
                        int item = int.Parse(s);
                        this.points.Add(item);
                    }
                }
                break;
        }
    }

    public override void SetImpact(ChampionshipRules inRules)
    {
        switch (this.impactType)
        {
            case PoliticalImpactPoints.ImpactType.Points:
                inRules.Points.Clear();
                inRules.Points = new List<int>((IEnumerable<int>)this.points);
                break;
            case PoliticalImpactPoints.ImpactType.FinalRacePoints:
                if (this.remove)
                {
                    inRules.FinalRacePointsDouble = false;
                    break;
                }
                inRules.FinalRacePointsDouble = true;
                break;
            case PoliticalImpactPoints.ImpactType.FastestLap:
                if (this.remove)
                {
                    inRules.FastestLapPointBonus = 0;
                    break;
                }
                inRules.FastestLapPointBonus = 2;
                break;
            case PoliticalImpactPoints.ImpactType.PoleBonus:
                if (this.remove)
                {
                    inRules.PolePositionPointBonus = 0;
                    break;
                }
                inRules.PolePositionPointBonus = 2;
                break;
        }
    }


    public enum ImpactType
    {
        Points,
        FinalRacePoints,
        FastestLap,
        PoleBonus,
        None,
    }
}
