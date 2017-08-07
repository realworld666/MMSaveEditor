using System.Collections.Generic;

public class PoliticalImpactSpecPart : PoliticalImpact
{
    public PoliticalImpactSpecPart.ImpactType impactType;
    public List<CarPart.PartType> partTypes = new List<CarPart.PartType>();

    public enum ImpactType
    {
        Activate,
        Remove,
    }
}
