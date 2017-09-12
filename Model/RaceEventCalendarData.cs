
using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class RaceEventCalendarData
{
    public Circuit circuit;
    public int week;

    public string CircuitName
    {
        get
        {
            return Localisation.LocaliseID(circuit.locationNameID);
        }
    }

    public string CircuitCountry
    {
        get
        {
            return Localisation.LocaliseID(circuit.countryNameID);
        }
    }

    public string CircuitLayout
    {
        get
        {
            return Localisation.LocaliseID(Localisation.GetEnumLocalisationID(circuit.trackLayout));
        }
    }

    public int WeekNumber
    {
        get
        {
            return week;
        }
    }

    public string SpriteName
    {
        get
        {
            return circuit.spriteName;
        }
    }
}
