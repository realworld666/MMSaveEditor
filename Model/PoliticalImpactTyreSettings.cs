public class PoliticalImpactTyreSettings : PoliticalImpact
{
    public ChampionshipRules.TyreType tyreType = ChampionshipRules.TyreType.Grooved;
    public PoliticalImpactTyreSettings.ImpactType impactType = PoliticalImpactTyreSettings.ImpactType.Speed;
    public string tyreSupplier = string.Empty;
    public ChampionshipRules.CompoundChoice compoundChoice;
    public ChampionshipRules.TyreWearRate wearRate;
    public float speedModifier;
    public float speedBonusModifier;
    public int tyreCompoundsAvailable;
    public int tyresAvailable;

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
