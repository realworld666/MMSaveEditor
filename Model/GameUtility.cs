using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

public class GameUtility
{
	public static GameUtility.StringBuilderPool GlobalStringBuilderPool = new GameUtility.StringBuilderPool();
	public static int mMainThreadId = Thread.CurrentThread.ManagedThreadId;
	public const float defaultImageFillAmountIfDifferentTolerance = 0.001953125f;
	public const float defaultImageFillAmountIfDifferentToleranceTyres = 0.002777778f;
	public const float defaultSliderIncrements = 1000f;

	public static GameUtility.AssertMode assertMode { get; set; }

	public static bool IsInMainThread
	{
		get
		{
			return Thread.CurrentThread.ManagedThreadId == GameUtility.mMainThreadId;
		}
	}


	public static void GetMinMaxX( ref Vector3[] vectors, ref float min, ref float max )
	{
		if( vectors.Length <= 0 )
			return;
		min = vectors[0].x;
		max = vectors[0].x;
		for( int index = 1; index < vectors.Length; ++index )
		{
			if( (double)max < (double)vectors[index].x )
				max = vectors[index].x;
			if( (double)min > (double)vectors[index].x )
				min = vectors[index].x;
		}
	}

	public static void GetMinMaxY( ref Vector3[] vectors, ref float min, ref float max )
	{
		if( vectors.Length <= 0 )
			return;
		min = vectors[0].y;
		max = vectors[0].y;
		for( int index = 1; index < vectors.Length; ++index )
		{
			if( (double)max < (double)vectors[index].y )
				max = vectors[index].y;
			if( (double)min > (double)vectors[index].y )
				min = vectors[index].y;
		}
	}

	public static string FormatChampionshipPoints( int inValue )
	{
		throw new NotImplementedException();
		//StringVariableParser.intValue1 = inValue;
		//return Localisation.LocaliseID( "PSG_10010222", (GameObject)null );
	}

	public static float MetersToMiles( float inValue )
	{
		return inValue * 0.0006213712f;
	}

	public static float MilesToMeters( float inValue )
	{
		return inValue * 1609.344f;
	}

	public static string GetDistanceTextFromMiles( float inMiles, float inToNearest = 1 )
	{
		throw new NotImplementedException();
		/*PrefGameSpeedUnits.Type speedUnits = App.instance.preferencesManager.gamePreferences.GetSpeedUnits();
        switch( speedUnits )
        {
            case PrefGameSpeedUnits.Type.MPH:
                return MathsUtility.NearestMultipleOf( inMiles, inToNearest ).ToString( (IFormatProvider)Localisation.numberFormatter ) + " " + Localisation.LocaliseID( "PSG_10000836", (GameObject)null );
            case PrefGameSpeedUnits.Type.KMPH:
                return MathsUtility.NearestMultipleOf( GameUtility.MilesToMeters( inMiles ) / 1000f, inToNearest ).ToString( (IFormatProvider)Localisation.numberFormatter ) + " " + Localisation.LocaliseID( "PSG_10000837", (GameObject)null );
            default:
                Debug.LogErrorFormat( "Could not find format for speed unit type {0}.", (object)speedUnits );
                return string.Empty;
        }*/
	}

	public static float MilesPerHourToMetersPerSecond( float inValue )
	{
		return GameUtility.MilesToMeters( inValue * 0.0002777778f );
	}

	public static float MetersPerSecondToMilesPerHour( float inValue )
	{
		return GameUtility.MetersToMiles( inValue * 3600f );
	}

	public static float MetersPerSecondToKilometersPerHour( float inValue )
	{
		return (float)( (double)inValue * 3600.0 / 1000.0 );
	}

	public static string GetAccelerationSpeedRangeText()
	{
		throw new NotImplementedException();/*
        PrefGameSpeedUnits.Type speedUnits = App.instance.preferencesManager.gamePreferences.GetSpeedUnits();
        switch( speedUnits )
        {
            case PrefGameSpeedUnits.Type.MPH:
                return "0-60" + Localisation.LocaliseID( "PSG_10000514", (GameObject)null );
            case PrefGameSpeedUnits.Type.KMPH:
                return "0-100" + Localisation.LocaliseID( "PSG_10000515", (GameObject)null );
            default:
                Debug.LogErrorFormat( "Could not find format for speed unit type {0}.", (object)speedUnits );
                return string.Empty;
        }*/
	}

	public static string GetSpeedText( float inSpeedMetersPerSecond, float outMultipleOf = 1 )
	{
		throw new NotImplementedException();
		/*PrefGameSpeedUnits.Type speedUnits = App.instance.preferencesManager.gamePreferences.GetSpeedUnits();
        switch( speedUnits )
        {
            case PrefGameSpeedUnits.Type.MPH:
                return ( (int)MathsUtility.NearestMultipleOf( GameUtility.MetersPerSecondToMilesPerHour( inSpeedMetersPerSecond ), outMultipleOf ) ).ToString( (IFormatProvider)Localisation.numberFormatter ) + Localisation.LocaliseID( "PSG_10000514", (GameObject)null );
            case PrefGameSpeedUnits.Type.KMPH:
                return ( (int)MathsUtility.NearestMultipleOf( GameUtility.MetersPerSecondToKilometersPerHour( inSpeedMetersPerSecond ), outMultipleOf ) ).ToString( (IFormatProvider)Localisation.numberFormatter ) + Localisation.LocaliseID( "PSG_10000515", (GameObject)null );
            default:
                Debug.LogErrorFormat( "Could not find format for speed unit type {0}.", (object)speedUnits );
                return string.Empty;
        }*/
	}

	public static float CelsiusToFahrenheit( float inCelsius )
	{
		return (float)( (double)inCelsius * 1.79999995231628 + 32.0 );
	}

	public static string GetTemperatureText( float inCelsius )
	{
		throw new NotImplementedException();
		/*PrefGameTemperatureUnits.Type temperatureUnits = App.instance.preferencesManager.gamePreferences.GetTemperatureUnits();
        switch( temperatureUnits )
        {
            case PrefGameTemperatureUnits.Type.Celsius:
                return ( (int)inCelsius ).ToString() + (object)'°' + "C";
            case PrefGameTemperatureUnits.Type.Fahrenheit:
                return ( (int)GameUtility.CelsiusToFahrenheit( inCelsius ) ).ToString() + (object)'°' + "F";
            default:
                Debug.LogErrorFormat( "Could not find format for temperature type {0}.", (object)temperatureUnits );
                return string.Empty;
        }*/
	}

	public static float MinutesToSeconds( float inMinutes )
	{
		return inMinutes * 60f;
	}

	public static float SecondsToMinutes( float inSeconds )
	{
		return inSeconds / 60f;
	}

	public static float SecondsToHours( float inSeconds )
	{
		return GameUtility.SecondsToMinutes( inSeconds ) / 60f;
	}

	public static float HoursToSeconds( float inHours )
	{
		return GameUtility.MinutesToSeconds( inHours * 60f );
	}

	public static float KilogramsToPounds( float inKg )
	{
		return inKg * 2.20462f;
	}

	public static void MetersToFeetInches( float inM, out int outFt, out float outInches )
	{
		float num = inM * 39.3701f;
		outFt = (int)Math.Floor( num / 12f );
		outInches = num % 12f;
	}

	public static string GetHeightText( float inCm )
	{
		throw new NotImplementedException();
		/*PrefGameHeight.Type heightUnits = App.instance.preferencesManager.gamePreferences.GetHeightUnits();
        switch( heightUnits )
        {
            case PrefGameHeight.Type.ftInches:
                int outFt;
                float outInches;
                GameUtility.MetersToFeetInches( inCm / 100f, out outFt, out outInches );
                if( outFt > 0 )
                {
                    StringVariableParser.intValue1 = outFt;
                    StringVariableParser.intValue2 = Mathf.RoundToInt( outInches );
                    return Localisation.LocaliseID( "PSG_10010626", (GameObject)null );
                }
                StringVariableParser.intValue1 = Mathf.RoundToInt( outInches );
                return Localisation.LocaliseID( "PSG_10010938", (GameObject)null );
            case PrefGameHeight.Type.cm:
                StringVariableParser.intValue1 = Mathf.RoundToInt( inCm );
                return Localisation.LocaliseID( "PSG_10010628", (GameObject)null );
            case PrefGameHeight.Type.m:
                StringVariableParser.floatValue1 = (float)Mathf.RoundToInt( inCm / 10f ) / 100f;
                return Localisation.LocaliseID( "PSG_10010937", (GameObject)null );
            default:
                Debug.LogErrorFormat( "Could not find format for height type {0}.", (object)heightUnits );
                return string.Empty;
        }*/
	}

	public static string GetWeightText( float inKg, int inDecimalPlaces = 2 )
	{
		throw new NotImplementedException();
		/*PrefGameWeight.Type weightUnits = App.instance.preferencesManager.gamePreferences.GetWeightUnits();
        switch (weightUnits)
        {
            case PrefGameWeight.Type.kg:
                StringVariableParser.floatValue1 = (float)Mathf.RoundToInt(inKg * Mathf.Pow(10f, (float)inDecimalPlaces)) / Mathf.Pow(10f, (float)inDecimalPlaces);
                return Localisation.LocaliseID("PSG_10010624", (GameObject)null);
            case PrefGameWeight.Type.lbs:
                StringVariableParser.floatValue1 = (float)Mathf.RoundToInt(GameUtility.KilogramsToPounds(inKg));
                return Localisation.LocaliseID("PSG_10010625", (GameObject)null);
            default:
                Debug.LogErrorFormat("Could not find format for weight type {0}.", (object)weightUnits);
                return string.Empty;
        }*/
	}

	public static string GetLapTimeText( float inLapTime, bool inIsCurrentLap )
	{
		throw new NotImplementedException();
		/*
		using (GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe())
        {
            float a = inLapTime;
            StringBuilder stringBuilder = builderSafe.stringBuilder;
            int num1 = (int)((double)inLapTime / 60.0);
            inLapTime -= (float)(num1 * 60);
            int num2 = (int)inLapTime;
            inLapTime -= (float)num2;
            int num3 = (int)((double)inLapTime * 1000.0);
            if (!MathsUtility.ApproximatelyZero(a))
            {
                if (num1 == 0)
                    GameUtility.Format(stringBuilder, Localisation.GetLapTimeFormatting(true), num2, num3);
                else
                    GameUtility.Format(stringBuilder, Localisation.GetLapTimeFormatting(false), num1, num2, num3);
                if (inIsCurrentLap)
                    stringBuilder.Remove(stringBuilder.Length - 2, 2);
            }
            return stringBuilder.ToString();
        }*/
	}

	public static void HoursMinutesSeconds( float inSeconds, out int outHours )
	{
		outHours = (int)( (double)inSeconds / 3600.0 );
	}

	public static void HoursMinutesSeconds( float inSeconds, out int outHours, out int outMinutes )
	{
		outHours = (int)( (double)inSeconds / 3600.0 );
		inSeconds -= (float)outHours * 3600f;
		outMinutes = (int)( (double)inSeconds / 60.0 );
	}

	public static void HoursMinutesSeconds( float inSeconds, out int outHours, out int outMinutes, out int outSeconds )
	{
		outHours = (int)( (double)inSeconds / 3600.0 );
		inSeconds -= (float)outHours * 3600f;
		outMinutes = (int)( (double)inSeconds / 60.0 );
		inSeconds -= (float)outMinutes * 60f;
		outSeconds = (int)inSeconds;
	}

	public static void HoursMinutesSeconds( float inSeconds, out int outHours, out int outMinutes, out int outSeconds, out int outMilliseconds )
	{
		outHours = (int)( (double)inSeconds / 3600.0 );
		inSeconds -= (float)outHours * 3600f;
		outMinutes = (int)( (double)inSeconds / 60.0 );
		inSeconds -= (float)outMinutes * 60f;
		outSeconds = (int)inSeconds;
		inSeconds -= (float)outSeconds;
		outMilliseconds = (int)( (double)inSeconds * 1000.0 );
	}

	public static void MinutesSeconds( float inSeconds, out int outMinutes, out int outSeconds, out int outMilliseconds )
	{
		outMinutes = (int)( (double)inSeconds / 60.0 );
		inSeconds -= (float)outMinutes * 60f;
		outSeconds = (int)inSeconds;
		inSeconds -= (float)outSeconds;
		outMilliseconds = (int)( (double)inSeconds * 1000.0 );
	}

	public static void Seconds( float inSeconds, out int outSeconds, out int outMilliseconds )
	{
		outSeconds = (int)inSeconds;
		inSeconds -= (float)outSeconds;
		outMilliseconds = (int)( (double)inSeconds * 1000.0 );
	}

	public static string GetSessionTimeText( float inTime )
	{
		using( GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe() )
		{
			StringBuilder stringBuilder = builderSafe.stringBuilder;
			int outHours;
			int outMinutes;
			int outSeconds;
			GameUtility.HoursMinutesSeconds( inTime, out outHours, out outMinutes, out outSeconds );
			if( outHours > 0 )
				GameUtility.Format( stringBuilder, "0:0:00", outHours, outMinutes, outSeconds );
			else
				GameUtility.Format( stringBuilder, "0:00", outMinutes, outSeconds );
			return stringBuilder.ToString();
		}
	}

	public static DateTime FromUnixTime( uint inUnixTime )
	{
		DateTime dateTime = new DateTime( 1970, 1, 1 );
		dateTime = dateTime.AddSeconds( (double)inUnixTime );
		return dateTime;
	}

	public static string FormatDateTimesToLongDateRange( DateTime inStartDate, DateTime inEndDate, string inLanguage = "" )
	{
		throw new NotImplementedException();
		/*string str1 = !(inLanguage != string.Empty) ? Localisation.currentLanguage : inLanguage;
        GameUtility.GetLanguageCulture(str1);
        CultureInfo languageCulture = GameUtility.GetLanguageCulture(Localisation.currentLanguage);
        string str2 = !Localisation.UseOrdinalsInDates(str1) ? inStartDate.Day.ToString() + "-" + (object)inEndDate.Day : GameUtility.OrdinalString(inStartDate.Day, str1) + "-" + GameUtility.OrdinalString(inEndDate.Day, str1);
        using (GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe())
        {
            StringBuilder stringBuilder = builderSafe.stringBuilder;
            switch (App.instance.preferencesManager.GetSettingEnum<PrefGameDateFormat.Type>(Preference.pName.Game_DateFormat, false))
            {
                case PrefGameDateFormat.Type.DDMMYYYY:
                    stringBuilder.Append(str2);
                    stringBuilder.Append(inEndDate.ToString(" MMMM", (IFormatProvider)languageCulture));
                    break;
                case PrefGameDateFormat.Type.MMDDYYYY:
                    stringBuilder.Append(inEndDate.ToString("MMMM ", (IFormatProvider)languageCulture));
                    stringBuilder.Append(str2);
                    break;
                default:
                    return "Could not get date format";
            }
            if (string.Equals(str1, "hungarian", StringComparison.OrdinalIgnoreCase))
            {
                stringBuilder.Insert(0, ". ");
                stringBuilder.Insert(0, inEndDate.Year.ToString());
            }
            else
            {
                stringBuilder.Append(" ");
                stringBuilder.Append(inEndDate.Year.ToString());
            }
            return stringBuilder.ToString();
        }*/
	}

	public static string FormatDateTimeToDateNoYear( DateTime inDate, string inLanguage = "" )
	{
		throw new NotImplementedException();
		/*string str = !(inLanguage != string.Empty) ? Localisation.currentLanguage : inLanguage;
        CultureInfo languageCulture = GameUtility.GetLanguageCulture(str);
        using (GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe())
        {
            StringBuilder stringBuilder = builderSafe.stringBuilder;
            switch (App.instance.preferencesManager.GetSettingEnum<PrefGameDateFormat.Type>(Preference.pName.Game_DateFormat, false))
            {
                case PrefGameDateFormat.Type.DDMMYYYY:
                    GameUtility.AppendDayToDateString(stringBuilder, inDate, str);
                    if (string.Equals(str, "hungarian", StringComparison.OrdinalIgnoreCase))
                    {
                        stringBuilder.Append(" ");
                        stringBuilder.Append(GameUtility.ForceCapitalizeFirstLetter(inDate.ToString("MMMM", (IFormatProvider)languageCulture)));
                    }
                    else
                        stringBuilder.Append(inDate.ToString(" MMMM", (IFormatProvider)languageCulture));
                    return stringBuilder.ToString();
                case PrefGameDateFormat.Type.MMDDYYYY:
                    if (string.Equals(str, "hungarian", StringComparison.OrdinalIgnoreCase))
                        stringBuilder.Append(GameUtility.ForceCapitalizeFirstLetter(inDate.ToString("MMMM ", (IFormatProvider)languageCulture)));
                    else
                        stringBuilder.Append(inDate.ToString("MMMM ", (IFormatProvider)languageCulture));
                    GameUtility.AppendDayToDateString(stringBuilder, inDate, str);
                    return stringBuilder.ToString();
                default:
                    return "Could not get date format!";
            }
        }*/
	}

	public static string FormatDateTimeToAbbrevMonthNoYear( DateTime inDate, string inLanguage = "" )
	{
		throw new NotImplementedException();
		/*string inLanguage1 = !( inLanguage != string.Empty ) ? Localisation.currentLanguage : inLanguage;
		CultureInfo languageCulture = GameUtility.GetLanguageCulture( inLanguage1 );
		using( GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe() )
		{
			StringBuilder stringBuilder = builderSafe.stringBuilder;
			switch( App.instance.preferencesManager.GetSettingEnum<PrefGameDateFormat.Type>( Preference.pName.Game_DateFormat, false ) )
			{
				case PrefGameDateFormat.Type.DDMMYYYY:
					GameUtility.AppendDayToDateString( stringBuilder, inDate, inLanguage1 );
					stringBuilder.Append( inDate.ToString( " MMM", (IFormatProvider)languageCulture ) );
					return stringBuilder.ToString();
				case PrefGameDateFormat.Type.MMDDYYYY:
					stringBuilder.Append( inDate.ToString( "MMM", (IFormatProvider)languageCulture ) );
					stringBuilder.Append( " " );
					GameUtility.AppendDayToDateString( stringBuilder, inDate, inLanguage1 );
					return stringBuilder.ToString();
				default:
					return "Could not get date string!";
			}
		}*/
	}

	public static string FormatDateTimeToLongDateString( DateTime inDate, string inLanguage = "" )
	{
		throw new NotImplementedException();
		/*string str = !( inLanguage != string.Empty ) ? Localisation.currentLanguage : inLanguage;
		CultureInfo languageCulture = GameUtility.GetLanguageCulture( str );
		switch( App.instance.preferencesManager.GetSettingEnum<PrefGameDateFormat.Type>( Preference.pName.Game_DateFormat, false ) )
		{
			case PrefGameDateFormat.Type.DDMMYYYY:
				using( GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe() )
				{
					StringBuilder stringBuilder = builderSafe.stringBuilder;
					if( string.Equals( str, "hungarian", StringComparison.OrdinalIgnoreCase ) )
					{
						stringBuilder.Append( inDate.ToString( "yyyy. ", (IFormatProvider)languageCulture ) );
						GameUtility.AppendDayToDateString( stringBuilder, inDate, str );
						stringBuilder.Append( " " );
						stringBuilder.Append( GameUtility.ForceLowerFirstLetter( inDate.ToString( "MMMM", (IFormatProvider)languageCulture ) ) );
						return stringBuilder.ToString();
					}
					GameUtility.AppendDayToDateString( stringBuilder, inDate, str );
					stringBuilder.Append( inDate.ToString( " MMMM yyyy", (IFormatProvider)languageCulture ) );
					return stringBuilder.ToString();
				}
			case PrefGameDateFormat.Type.MMDDYYYY:
				using( GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe() )
				{
					StringBuilder stringBuilder = builderSafe.stringBuilder;
					if( string.Equals( str, "hungarian", StringComparison.OrdinalIgnoreCase ) )
					{
						stringBuilder.Append( inDate.ToString( "yyyy. ", (IFormatProvider)languageCulture ) );
						stringBuilder.Append( GameUtility.ForceLowerFirstLetter( inDate.ToString( "MMMM ", (IFormatProvider)languageCulture ) ) );
						GameUtility.AppendDayToDateString( stringBuilder, inDate, str );
						return stringBuilder.ToString();
					}
					stringBuilder.Append( inDate.ToString( "MMMM ", (IFormatProvider)languageCulture ) );
					GameUtility.AppendDayToDateString( stringBuilder, inDate, str );
					stringBuilder.Append( inDate.ToString( " yyyy", (IFormatProvider)languageCulture ) );
					return stringBuilder.ToString();
				}
			default:
				return "Could not get date format!";
		}*/
	}

	private static void AppendDayToDateString( StringBuilder inStringBuilder, DateTime inDate, string inLanguage )
	{
		throw new NotImplementedException();
		/*if( Localisation.UseOrdinalsInDates( inLanguage ) )
			inStringBuilder.Append( GameUtility.OrdinalString( inDate.Day, inLanguage ) );
		else
			inStringBuilder.Append( inDate.Day );*/
	}

	public static string FormatTimeSpan( TimeSpan inTimeSpan )
	{
		throw new NotImplementedException();
		/*
		StringBuilder stringBuilder = new StringBuilder();
		if( inTimeSpan.TotalDays < 1.0 )
		{
			if( inTimeSpan.TotalHours < 1.0 )
			{
				int int32 = Convert.ToInt32( inTimeSpan.TotalMinutes );
				StringVariableParser.intValue1 = int32;
				stringBuilder.Append( int32 != 1 ? Localisation.LocaliseID( "PSG_10010538", (GameObject)null ) : Localisation.LocaliseID( "PSG_10010537", (GameObject)null ) );
			}
			else
			{
				int int32 = Convert.ToInt32( inTimeSpan.TotalHours );
				StringVariableParser.intValue1 = int32;
				stringBuilder.Append( int32 != 1 ? Localisation.LocaliseID( "PSG_10010540", (GameObject)null ) : Localisation.LocaliseID( "PSG_10010539", (GameObject)null ) );
			}
		}
		else
		{
			int int32 = Convert.ToInt32( inTimeSpan.TotalDays );
			StringVariableParser.intValue1 = int32;
			stringBuilder.Append( int32 != 1 ? Localisation.LocaliseID( "PSG_10010542", (GameObject)null ) : Localisation.LocaliseID( "PSG_10010541", (GameObject)null ) );
		}
		return stringBuilder.ToString();*/
	}

	public static string FormatTimeSpanWeeks( TimeSpan inTimeSpan )
	{
		throw new NotImplementedException();
		/*StringBuilder stringBuilder = new StringBuilder();
		if( inTimeSpan.TotalDays < 7.0 )
		{
			if( inTimeSpan.TotalDays < 1.0 )
			{
				if( inTimeSpan.TotalHours < 1.0 )
				{
					stringBuilder.Append( Localisation.LocaliseID( "PSG_10010638", (GameObject)null ) );
				}
				else
				{
					int int32 = Convert.ToInt32( inTimeSpan.TotalHours );
					StringVariableParser.intValue1 = int32;
					stringBuilder.Append( int32 != 1 ? Localisation.LocaliseID( "PSG_10010540", (GameObject)null ) : Localisation.LocaliseID( "PSG_10010539", (GameObject)null ) );
				}
			}
			else
			{
				int int32 = Convert.ToInt32( inTimeSpan.TotalDays );
				StringVariableParser.intValue1 = int32;
				stringBuilder.Append( int32 != 1 ? Localisation.LocaliseID( "PSG_10010542", (GameObject)null ) : Localisation.LocaliseID( "PSG_10010541", (GameObject)null ) );
			}
		}
		else
		{
			int num = Mathf.Max( Mathf.RoundToInt( (float)Convert.ToInt32( inTimeSpan.TotalDays ) / 7f ), 1 );
			StringVariableParser.intValue1 = num;
			stringBuilder.Append( num != 1 ? Localisation.LocaliseID( "PSG_10010544", (GameObject)null ) : Localisation.LocaliseID( "PSG_10010543", (GameObject)null ) );
		}
		return stringBuilder.ToString();*/
	}

	public static string FormatTimeSpanDays( TimeSpan inTimeSpan )
	{
		throw new NotImplementedException();
		/*StringBuilder stringBuilder = new StringBuilder();
		if( inTimeSpan.TotalDays < 1.0 )
		{
			if( inTimeSpan.TotalHours < 1.0 )
			{
				stringBuilder.Append( Localisation.LocaliseID( "PSG_10010638", (GameObject)null ) );
			}
			else
			{
				int int32 = Convert.ToInt32( inTimeSpan.TotalHours );
				StringVariableParser.intValue1 = int32;
				stringBuilder.Append( int32 != 1 ? Localisation.LocaliseID( "PSG_10010540", (GameObject)null ) : Localisation.LocaliseID( "PSG_10010539", (GameObject)null ) );
			}
		}
		else
		{
			int int32 = Convert.ToInt32( inTimeSpan.TotalDays );
			StringVariableParser.intValue1 = int32;
			stringBuilder.Append( int32 != 1 ? Localisation.LocaliseID( "PSG_10010542", (GameObject)null ) : Localisation.LocaliseID( "PSG_10010541", (GameObject)null ) );
		}
		return stringBuilder.ToString();*/
	}

	public static string FormatTimeSpanDays( float inDays )
	{
		throw new NotImplementedException();
		/*
		StringBuilder stringBuilder = new StringBuilder();
		StringVariableParser.ordinalNumberString = inDays.ToString();
		stringBuilder.Append( (double)inDays != 1.0 ? Localisation.LocaliseID( "PSG_10010542", (GameObject)null ) : Localisation.LocaliseID( "PSG_10010541", (GameObject)null ) );
		return stringBuilder.ToString();*/
	}

	public static GameUtility.Month GetMonthAsEnum( DateTime inDateTime )
	{
		return (GameUtility.Month)( inDateTime.Month - 1 );
	}




	public static string ChangeFirstCharToUpperCase( string inString )
	{
		string upper = inString[0].ToString().ToUpper();
		inString = inString.Remove( 0, 1 );
		inString = inString.Insert( 0, upper );
		return inString;
	}

	public static string ChangeFirstCharToLowerCase( string inString )
	{
		string lower = inString[0].ToString().ToLower();
		inString = inString.Remove( 0, 1 );
		inString = inString.Insert( 0, lower );
		return inString;
	}

	public static string PrepareStringForMailEntry( string inString, int inMaxLength )
	{
		if( inString.Length == 0 )
			return inString;
		int startIndex1 = inString.IndexOf( "\n" );
		if( startIndex1 > 0 )
			inString = inString.Remove( startIndex1 );
		inString = GameUtility.RemoveLinks( inString );
		if( inString.Length > inMaxLength )
			inString = inString.Remove( inMaxLength );
		inString = inString.Trim();
		int startIndex2 = -1;
		for( int index = inString.Length - 1; index >= 0 && ( index > 25 && !char.IsWhiteSpace( inString[index] ) || char.IsLetterOrDigit( inString[index] ) ); --index )
			startIndex2 = index;
		if( startIndex2 > 0 )
			inString = inString.Remove( startIndex2 );
		if( inString.Length > 0 && !char.IsLetterOrDigit( inString[inString.Length - 1] ) )
			inString = inString.Remove( inString.Length - 1 );
		inString += "...";
		return inString;
	}

	public static string RemoveLinks( string inString )
	{
		return GameUtility.RemoveBetween( inString, '<', '>' );
	}

	public static string RemoveBetween( string s, char begin, char end )
	{
		return new Regex( string.Format( "\\{0}.*?\\{1}", (object)begin, (object)end ) ).Replace( s, string.Empty );
	}

	public static string RemoveUnderlineTags( string inString )
	{
		inString = inString.Replace( "<u>", string.Empty );
		inString = inString.Replace( "</u>", string.Empty );
		return inString;
	}



	public static float ToMillions( int inValue )
	{
		return GameUtility.ToMillions( (long)inValue );
	}

	public static float ToMillions( float inValue )
	{
		return GameUtility.ToMillions( (long)inValue );
	}

	public static float ToMillions( long inValue )
	{
		return (float)inValue / 1000000f;
	}

	public static long RoundCurrency( long inValue )
	{
		bool flag = inValue < 0L;
		if( flag )
			inValue = Math.Abs( inValue );
		if( inValue > 1000L )
		{
			inValue /= 1000L;
			inValue *= 1000L;
		}
		else if( inValue > 100L )
		{
			inValue /= 100L;
			inValue *= 100L;
		}
		else if( inValue > 10L )
		{
			inValue /= 10L;
			inValue *= 10L;
		}
		if( flag )
			return -inValue;
		return inValue;
	}



	public static string ForceCapitalizeFirstLetter( string inString )
	{
		Debug.Assert( inString.Length > 0, "Cannot capitalise an empty string!" );
		if( inString.Length <= 1 )
			return inString.ToUpper();
		using( GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe() )
		{
			StringBuilder stringBuilder = builderSafe.stringBuilder;
			stringBuilder.Append( inString.ToUpper()[0] );
			stringBuilder.Append( inString.Substring( 1, inString.Length - 1 ) );
			return stringBuilder.ToString();
		}
	}

	public static string ForceLowerFirstLetter( string inString )
	{
		Debug.Assert( inString.Length > 0, "Cannot capitalise an empty string!" );
		if( inString.Length <= 1 )
			return inString.ToLower();
		using( GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe() )
		{
			StringBuilder stringBuilder = builderSafe.stringBuilder;
			stringBuilder.Append( inString.ToLower()[0] );
			stringBuilder.Append( inString.Substring( 1, inString.Length - 1 ) );
			return stringBuilder.ToString();
		}
	}



	public static bool MethodPresentInAction( Action inAction, string inMethod )
	{
		foreach( Delegate invocation in inAction.GetInvocationList() )
		{
			if( invocation.Method.Name == inMethod )
				return true;
		}
		return false;
	}


	public static void Append( StringBuilder sb, int value, int min_digits )
	{
		int num1 = 1;
		int num2 = 0;
		while( num1 <= value || num2 < min_digits )
		{
			++num2;
			num1 *= 10;
		}
		int num3 = num1 / 10;
		while( num3 > 0 )
		{
			int num4 = value / num3;
			sb.Append( (char)( 48 + num4 ) );
			value -= num4 * num3;
			num3 /= 10;
		}
	}

	public static void AppendAsFractional( StringBuilder sb, int value, int num_digits )
	{
		int num1 = 1;
		int num2 = 0;
		while( num1 <= value || num2 < num_digits )
		{
			++num2;
			num1 *= 10;
		}
		int num3 = num1 / 10;
		for( ; num_digits > 0; --num_digits )
		{
			int num4 = value / num3;
			sb.Append( (char)( 48 + num4 ) );
			value -= num4 * num3;
			num3 /= 10;
		}
	}

	private static void Append( StringBuilder sb, int value, int num_zeroes, bool asFractional )
	{
		if( asFractional )
			GameUtility.AppendAsFractional( sb, value, num_zeroes );
		else
			GameUtility.Append( sb, value, num_zeroes );
	}

	public static void Format( StringBuilder sb, string format, params int[] args )
	{
		int length = format.Length;
		int num1 = 0;
		int num_zeroes1 = 0;
		bool asFractional = false;
		for( int index = 0; index < length; ++index )
		{
			if( (int)format[index] == 48 )
			{
				++num_zeroes1;
			}
			else
			{
				if( num_zeroes1 > 0 )
				{
					GameUtility.Append( sb, args[num1++], num_zeroes1, asFractional );
					num_zeroes1 = 0;
				}
				asFractional = (int)format[index] == 46 || (int)format[index] == 44;
				sb.Append( format[index] );
			}
		}
		if( num_zeroes1 <= 0 )
			return;
		StringBuilder sb1 = sb;
		int[] numArray = args;
		int index1 = num1;
		int num2 = 1;
		int num3 = index1 + num2;
		int num4 = numArray[index1];
		int num_zeroes2 = num_zeroes1;
		int num5 = asFractional ? 1 : 0;
		GameUtility.Append( sb1, num4, num_zeroes2, num5 != 0 );
	}

	public enum AssertMode
	{
		LogError,
		ThrowException,
	}

	public enum Month
	{
		[LocalisationID( "PSG_10000813" )] January,
		[LocalisationID( "PSG_10000814" )] February,
		[LocalisationID( "PSG_10000815" )] March,
		[LocalisationID( "PSG_10000816" )] April,
		[LocalisationID( "PSG_10000817" )] May,
		[LocalisationID( "PSG_10000818" )] June,
		[LocalisationID( "PSG_10000819" )] July,
		[LocalisationID( "PSG_10000820" )] August,
		[LocalisationID( "PSG_10000821" )] September,
		[LocalisationID( "PSG_10000822" )] October,
		[LocalisationID( "PSG_10000823" )] November,
		[LocalisationID( "PSG_10000824" )] December,
	}

	public enum MonthShort
	{
		[LocalisationID( "PSG_10000826" )] Jan,
		[LocalisationID( "PSG_10000825" )] Feb,
		[LocalisationID( "PSG_10000827" )] Mar,
		[LocalisationID( "PSG_10000828" )] Apr,
		[LocalisationID( "PSG_10000817" )] May,
		[LocalisationID( "PSG_10000830" )] Jun,
		[LocalisationID( "PSG_10000831" )] Jul,
		[LocalisationID( "PSG_10003615" )] Aug,
		[LocalisationID( "PSG_10000832" )] Sep,
		[LocalisationID( "PSG_10000834" )] Oct,
		[LocalisationID( "PSG_10000833" )] Nov,
		[LocalisationID( "PSG_10000835" )] Dec,
	}

	public enum Day
	{
		[LocalisationID( "PSG_10000812" )] Sunday,
		[LocalisationID( "PSG_10000008" )] Monday,
		[LocalisationID( "PSG_10000010" )] Tuesday,
		[LocalisationID( "PSG_10000011" )] Wednesday,
		[LocalisationID( "PSG_10000697" )] Thursday,
		[LocalisationID( "PSG_10000698" )] Friday,
		[LocalisationID( "PSG_10000811" )] Saturday,
	}

	public class AssertException : Exception
	{
		public AssertException()
		{
		}

		public AssertException( string message )
		  : base( message )
		{
		}

		public AssertException( string message, Exception inner )
		  : base( message, inner )
		{
		}
	}

	public enum AnchorType
	{
		X,
		Y,
		Both,
	}

	public enum AnchorLocation
	{
		Top,
		Center,
		Bottom,
	}

	public class StringBuilderPool
	{
		private Stack<StringBuilder> builders = new Stack<StringBuilder>();
		private Stack<GameUtility.StringBuilderWrapper> wrappers = new Stack<GameUtility.StringBuilderWrapper>();

		public StringBuilder GetBuilder()
		{
			//GameUtility.Assert( GameUtility.IsInMainThread );
			if( this.builders.Count == 0 )
				return new StringBuilder( 640 );
			return this.builders.Pop();
		}

		public GameUtility.StringBuilderWrapper GetBuilderSafe()
		{
			lock( this.wrappers )
			{
				if( this.wrappers.Count == 0 )
					return new GameUtility.StringBuilderWrapper( new StringBuilder( 640 ) );
				return this.wrappers.Pop();
			}
		}

		public void ReturnBuilder( StringBuilder builder )
		{
			//GameUtility.Assert( GameUtility.IsInMainThread );
			builder.Length = 0;
			this.builders.Push( builder );
		}

		public void ReturnBuilderWrapper( GameUtility.StringBuilderWrapper wrapper )
		{
			wrapper.stringBuilder.Length = 0;
			lock( this.wrappers )
				this.wrappers.Push( wrapper );
		}
	}

	public class StringBuilderWrapper : IDisposable
	{
		private StringBuilder mStringBuilder;

		public StringBuilder stringBuilder
		{
			get
			{
				return this.mStringBuilder;
			}
		}

		public StringBuilderWrapper( StringBuilder inStringBuilder )
		{
			//GameUtility.Assert( inStringBuilder != null );
			this.mStringBuilder = inStringBuilder;
		}

		public void Dispose()
		{
			GameUtility.GlobalStringBuilderPool.ReturnBuilderWrapper( this );
		}
	}
}
