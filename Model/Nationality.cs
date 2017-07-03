using System;

public class Nationality 
{
	private string mCountryKey = string.Empty;
	private string mCountryID = string.Empty;
	private string mNationalityID = string.Empty;
	private Nationality.Continent mContinent;
    

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