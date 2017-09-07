using System;

public class PoliticalImpactTyreSettings : PoliticalImpact
{
    public ChampionshipRules.TyreType tyreType = ChampionshipRules.TyreType.Grooved;
    public ChampionshipRules.CompoundChoice compoundChoice;
    public PoliticalImpactTyreSettings.ImpactType impactType = PoliticalImpactTyreSettings.ImpactType.Speed;
    public ChampionshipRules.TyreWearRate wearRate;
    public string tyreSupplier = string.Empty;
    public float speedModifier;
    public float speedBonusModifier;
    public int tyreCompoundsAvailable;
    public int tyresAvailable;
    private string inName;
    private string inEffect;

    public PoliticalImpactTyreSettings(string inName, string inEffect)
    {
        switch (inName)
        {
            case "TyresAvailable":
                this.impactType = PoliticalImpactTyreSettings.ImpactType.TyresAvailable;
                this.tyresAvailable = int.Parse(inEffect);
                break;
            case "TyreCompoundsAvailable":
                this.impactType = PoliticalImpactTyreSettings.ImpactType.CompoundAvailable;
                this.tyreCompoundsAvailable = int.Parse(inEffect);
                break;
            case "TyreCompoundChoice":
                this.impactType = PoliticalImpactTyreSettings.ImpactType.CompoundChoice;
                if (inEffect == "Free")
                {
                    this.compoundChoice = ChampionshipRules.CompoundChoice.Free;
                }
                else
                {
                    this.compoundChoice = ChampionshipRules.CompoundChoice.Locked;
                }
                break;
            case "TyreSupplier":
                this.impactType = PoliticalImpactTyreSettings.ImpactType.Supplier;
                this.tyreSupplier = inEffect;
                break;
            case "TyreType":
                this.impactType = PoliticalImpactTyreSettings.ImpactType.TyreType;
                this.tyreType = (ChampionshipRules.TyreType)((int)Enum.Parse(typeof(ChampionshipRules.TyreType), inEffect));
                break;
            case "TyreSupplierSpeedBonus":
                this.impactType = PoliticalImpactTyreSettings.ImpactType.SpeedBonus;
                this.speedBonusModifier = float.Parse(inEffect);
                break;
            case "TyreSpeed":
                this.impactType = PoliticalImpactTyreSettings.ImpactType.Speed;
                this.speedModifier = float.Parse(inEffect);
                break;
            case "TyreWearRate":
                this.impactType = PoliticalImpactTyreSettings.ImpactType.WearRate;
                this.wearRate = (ChampionshipRules.TyreWearRate)((int)Enum.Parse(typeof(ChampionshipRules.TyreWearRate), inEffect));
                break;
        }
    }

    public enum ImpactType
    {
        TyreType,
        WearRate,
        Speed,
        SpeedBonus,
        Supplier,
        CompoundChoice,
        CompoundAvailable,
        TyresAvailable,
    }
}
