using System;
using FullSerializer;

[fsObject("v0", MemberSerialization = fsMemberSerialization.OptOut)]
public class Nationality
{
    private Nationality.Continent mContinent;
    private string mCountryKey = string.Empty;
    private string mCountryID = string.Empty;
    private string mNationalityID = string.Empty;


    public enum Continent
    {
        Africa,
        Asia,
        Europe,
        NorthAmerica,
        Oceania,
        SouthAmerica,
    }
}
