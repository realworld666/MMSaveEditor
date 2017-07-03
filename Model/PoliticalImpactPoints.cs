
using System.Collections.Generic;

public class PoliticalImpactPoints : PoliticalImpact
{
    private List<int> points = new List<int>();
    private PoliticalImpactPoints.ImpactType impactType;
    private bool remove;
    public enum ImpactType
    {
        Points,
        FinalRacePoints,
        FastestLap,
        PoleBonus,
        None,
    }
}
