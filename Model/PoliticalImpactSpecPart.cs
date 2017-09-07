using System;
using System.Collections.Generic;

public class PoliticalImpactSpecPart : PoliticalImpact
{
    public PoliticalImpactSpecPart.ImpactType impactType;
    public List<CarPart.PartType> partTypes = new List<CarPart.PartType>();
    private string inName;
    private string inEffect;

    public PoliticalImpactSpecPart(string inName, string inEffects)
    {
        if (inEffects.Contains("Remove"))
        {
            this.impactType = PoliticalImpactSpecPart.ImpactType.Remove;
        }
        else
        {
            this.impactType = PoliticalImpactSpecPart.ImpactType.Activate;
        }
        inEffects = inEffects.Replace("Remove", string.Empty);
        CarPart.PartType item = (CarPart.PartType)((int)Enum.Parse(typeof(CarPart.PartType), inEffects));
        this.partTypes.Add(item);
    }

    public enum ImpactType
    {
        Activate,
        Remove,
    }
}
