public class PoliticalImpactFuelSettings : PoliticalImpact
{
    private PoliticalImpactFuelSettings.ImpactType impactType;
    private float fuelLimit;
    private bool refuelling;


    private enum ImpactType
    {
        Limit,
        Refueling,
    }
}
