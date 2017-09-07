public class PoliticalImpactFuelSettings : PoliticalImpact
{
    private PoliticalImpactFuelSettings.ImpactType impactType;
    private float fuelLimit;
    private bool refuelling;
    private string inName;
    private string inEffect;

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

    private enum ImpactType
    {
        Limit,
        Refueling,
    }
}
