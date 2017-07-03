using System.Collections.Generic;

public class PoliticalImpactSpecPart : PoliticalImpact
{
    public List<CarPart.PartType> partTypes = new List<CarPart.PartType>();
    public PoliticalImpactSpecPart.ImpactType impactType;

    public enum ImpactType
    {
        Activate,
        Remove,
    }
}
