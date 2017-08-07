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
