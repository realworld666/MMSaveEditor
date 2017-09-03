using System;
using FullSerializer;

[fsObject("v0", MemberSerialization = fsMemberSerialization.OptOut)]
public class Nationality
{
    private Nationality.Continent mContinent;
    private string mCountryKey = string.Empty;
    private string mCountryID = string.Empty;
    private string mNationalityID = string.Empty;

    public Nationality.Continent continent
    {
        get
        {
            return this.mContinent;
        }
        set
        {
            this.mContinent = value;
        }
    }
    public Uri flagSpritePathName
    {
        get
        {
            return new Uri(string.Format("pack://application:,,,/Assets/Flags/{0}.png", localisedCountry));
        }
    }
    public string localisedCountry
    {
        get
        {
            return Localisation.LocaliseID(this.mCountryID, null);
        }
    }
    public string localisedNationality
    {
        get
        {
            return Localisation.LocaliseID(this.mNationalityID, null);
        }
    }
    public string countryKey
    {
        get
        {
            return this.mCountryKey;
        }
    }
    public void SetKey(string inKey)
    {
        this.mCountryKey = inKey;
    }

    public void SetCountry(string inCountryID)
    {
        this.mCountryID = inCountryID;
    }

    public void SetNationality(string inNationalityID)
    {
        this.mNationalityID = inNationalityID;
    }

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
