using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class Scout : Person
{
    public ScoutStats stats = new ScoutStats();
    public Scout.FavouredLocations favouredLocation;


    public enum FavouredLocations
    {
        None,
        Argentina,
        Australia,
        Brazil,
        Canada,
        Chile,
        China,
        Colombia,
        Ecuador,
        Egypt,
        Finland,
        France,
        Germany,
        India,
        Italy,
        Japan,
        Korea,
        Mexico,
        NewZealand,
        Poland,
        Russia,
        SouthAfrica,
        Sweden,
        Switzerland,
        UK,
        USA,
        Venezuela,
    }
}
