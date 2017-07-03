
using System.Collections.Generic;

public class CarPartComponentRequirement
{
    public CarPart part;

    public virtual bool IsLocked(RacingVehicle inVehicle)
    {
        return false;
    }

    public virtual KeyValuePair<string, string> GetStatDescriptionString()
    {
        return new KeyValuePair<string, string>(string.Empty, string.Empty);
    }
}
