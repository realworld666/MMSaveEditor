public class PoliticalImpactFuelSettings : PoliticalImpact
{
    private PoliticalImpactFuelSettings.ImpactType impactType;
    private float fuelLimit;
    private bool refuelling;

    public PoliticalImpactFuelSettings(string inName, string inEffect)
    {
        if (inName != null)
        {
            switch (inName)
            {
                case "FuelLimit":
                    this.impactType = PoliticalImpactFuelSettings.ImpactType.Limit;
                    this.fuelLimit = float.Parse(inEffect);
                    break;
                case "Refuelling":
                    this.impactType = PoliticalImpactFuelSettings.ImpactType.Refueling;
                    this.refuelling = bool.Parse(inEffect);
                    break;
            }
        }
    }

    public override void SetImpact(ChampionshipRules inRules)
    {
        switch (this.impactType)
        {
            case PoliticalImpactFuelSettings.ImpactType.Limit:
                inRules.FuelLimitForRaceDistanceNormalized = this.fuelLimit / 100f;
                break;
            case PoliticalImpactFuelSettings.ImpactType.Refueling:
                inRules.IsRefuelingOn = this.refuelling;
                break;
        }
    }

    private enum ImpactType
    {
        Limit,
        Refueling,
    }
}
