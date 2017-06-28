using System;

public class Nationality : IEquatable<Nationality>, IComparable<Nationality>
{
	private string mCountryKey = string.Empty;
	private string mCountryID = string.Empty;
	private string mNationalityID = string.Empty;
	private Nationality.Continent mContinent;

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

	public string localisedCountry
	{
		get
		{
			return "";//Localisation.LocaliseID(this.mCountryID, (GameObject) null);
		}
	}

	public string countryID
	{
		get
		{
			return this.mCountryID;
		}
	}

	public string localisedNationality
	{
		get
		{
			return "";//Localisation.LocaliseID( this.mNationalityID, (GameObject)null );
		}
	}

	public string flagSpritePathName
	{
		get
		{
			return this.mCountryKey;
		}
	}

	public static bool operator ==( Nationality nationalityA, Nationality nationalityB )
	{
		if( object.ReferenceEquals( (object)nationalityA, (object)nationalityB ) )
			return true;
		if( (object)nationalityA == null || (object)nationalityB == null )
			return false;
		return nationalityA.mCountryID == nationalityB.mCountryID;
	}

	public static bool operator !=( Nationality nationalityA, Nationality nationalityB )
	{
		return !( nationalityA == nationalityB );
	}

	public static string GetContinentStringID( Nationality.Continent inContinent )
	{
		switch( inContinent )
		{
			case Nationality.Continent.Africa:
				return "PSG_10000874";
			case Nationality.Continent.Asia:
				return "PSG_10000875";
			case Nationality.Continent.Europe:
				return "PSG_10000871";
			case Nationality.Continent.NorthAmerica:
				return "PSG_10000872";
			case Nationality.Continent.Oceania:
				return "PSG_10000876";
			case Nationality.Continent.SouthAmerica:
				return "PSG_10000873";
			default:
				return "Error";
		}
	}

	public bool Equals( Nationality other )
	{
		if( other == (Nationality)null )
			return false;
		return this.mCountryID.Equals( other.mCountryID );
	}

	public override bool Equals( object obj )
	{
		if( obj == null )
			return false;
		Nationality other = obj as Nationality;
		if( other == (Nationality)null )
			return false;
		return this.Equals( other );
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	public int CompareTo( Nationality inOtherNationality )
	{
		if( inOtherNationality == (Nationality)null )
			return 1;
		return string.Compare( this.localisedCountry, inOtherNationality.localisedCountry );
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