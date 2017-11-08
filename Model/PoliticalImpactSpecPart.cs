using System;
using System.Collections.Generic;

public class PoliticalImpactSpecPart : PoliticalImpact
{
    public PoliticalImpactSpecPart.ImpactType impactType;
    public List<CarPart.PartType> partTypes = new List<CarPart.PartType>();

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

    public override bool VoteCanBeUsed(Championship inChampionship)
    {
        List<CarPart.PartType> partTypeList = new List<CarPart.PartType>((IEnumerable<CarPart.PartType>)CarPart.GetPartType(inChampionship.series, false));
        bool flag = true;
        for (int index = 0; index < this.partTypes.Count; ++index)
        {
            if (!partTypeList.Contains(this.partTypes[index]))
                flag = false;
        }
        return flag;
    }

    public override void SetImpact(ChampionshipRules inRules)
    {
        switch (this.impactType)
        {
            case PoliticalImpactSpecPart.ImpactType.Activate:
                for (int index = 0; index < this.partTypes.Count; ++index)
                {
                    CarPart.PartType partType = this.partTypes[index];
                    if (!inRules.SpecParts.Contains(partType))
                        inRules.SpecParts.Add(this.partTypes[index]);
                }
                inRules.ApplySpecParts();
                break;
            case PoliticalImpactSpecPart.ImpactType.Remove:
                for (int index = 0; index < this.partTypes.Count; ++index)
                {
                    CarPart.PartType partType = this.partTypes[index];
                    if (inRules.SpecParts.Contains(partType))
                    {
                        inRules.SpecParts.Remove(partType);
                        inRules.GenerateDefaultParts(partType);
                    }
                }
                break;
        }
    }

    public enum ImpactType
    {
        Activate,
        Remove,
    }
}
