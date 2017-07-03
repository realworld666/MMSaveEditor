using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
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

	[ContractAnnotation( "inExpression:false => halt" )]
	public static void Assert( bool inExpression )
	{
		if( inExpression )
			return;
		if( GameUtility.assertMode == GameUtility.AssertMode.LogError )
			Debug.LogError( (object)"Assertion failed", (UnityEngine.Object)null );
		else if( GameUtility.assertMode == GameUtility.AssertMode.ThrowException )
			throw new GameUtility.AssertException( "Assertion failed" );
	}

	[ContractAnnotation( "inExpression:false => halt" )]
	public static void Assert( bool inExpression, string inErrorMessage, UnityEngine.Object relatedUnityObject = null )
	{
		if( inExpression )
			return;
		if( GameUtility.assertMode == GameUtility.AssertMode.LogError )
			Debug.LogError( (object)inErrorMessage, relatedUnityObject );
		else if( GameUtility.assertMode == GameUtility.AssertMode.ThrowException )
			throw new GameUtility.AssertException( inErrorMessage );
	}

	public static void SetActiveForSeries( Championship inChampionship, params ActivateForSeries.GameObjectData[] inData )
	{
		GameUtility.SetActiveForSeries( inChampionship.series, inData );
	}

	public static void SetActiveForSeries( Championship.Series inSeries, params ActivateForSeries.GameObjectData[] inData )
	{
		if( inData == null )
			return;
		Championship.Series series = inSeries;
		for( int index1 = 0; index1 < inData.Length; ++index1 )
		{
			ActivateForSeries.GameObjectData gameObjectData = inData[index1];
			bool inIsActive = series == gameObjectData.series;
			for( int index2 = 0; index2 < gameObjectData.targetObjects.Length; ++index2 )
				GameUtility.SetActive( gameObjectData.targetObjects[index2], inIsActive );
		}
	}

	public static void SetActive( GameObject inGameObject, bool inIsActive )
	{
		if( inGameObject.activeSelf == inIsActive )
			return;
		inGameObject.SetActive( inIsActive );
	}

	public static void SetActiveAndCheckNull( GameObject inGameObject, bool inIsActive )
	{
		if( !( (UnityEngine.Object)inGameObject != (UnityEngine.Object)null ) || inGameObject.activeSelf == inIsActive )
			return;
		inGameObject.SetActive( inIsActive );
	}

	public static bool SetSiblingIndex( GameObject inGameObject, int inSiblingIndex )
	{
		return GameUtility.SetSiblingIndex( inGameObject.transform, inSiblingIndex );
	}

	public static bool SetSiblingIndex( Transform inTransform, int inSiblingIndex )
	{
		if( inTransform.GetSiblingIndex() == inSiblingIndex )
			return false;
		inTransform.SetSiblingIndex( inSiblingIndex );
		return true;
	}

	public static bool SetParent( GameObject inGameObject, GameObject inParent, bool inWorldPositionStays = false )
	{
		return GameUtility.SetParent( inGameObject.transform, inParent.transform, inWorldPositionStays );
	}

	public static bool SetParent( Transform inTransform, Transform inParent, bool inWorldPositionStays = false )
	{
		if( !( (UnityEngine.Object)inTransform.parent != (UnityEngine.Object)inParent ) )
			return false;
		inTransform.SetParent( inParent, inWorldPositionStays );
		return true;
	}

	public static string ObjectFullPath( Transform transform )
	{
		if( (bool)( (UnityEngine.Object)transform ) )
			return GameUtility.ObjectFullPath( transform.parent ) + "/" + transform.name;
		return string.Empty;
	}

	public static Transform FindInAllDescendents( Transform parent, string name )
	{
		for( int index = 0; index < parent.childCount; ++index )
		{
			Transform child = parent.GetChild( index );
			if( child.name == name )
				return child;
			Transform inAllDescendents = GameUtility.FindInAllDescendents( child, name );
			if( (bool)( (UnityEngine.Object)inAllDescendents ) )
				return inAllDescendents;
		}
		return (Transform)null;
	}

	public static bool IsParentInHierarchy( Transform inTransform, Transform inParent )
	{
		if( !( (UnityEngine.Object)inTransform != (UnityEngine.Object)null ) || !( (UnityEngine.Object)inTransform.parent != (UnityEngine.Object)null ) )
			return false;
		if( (UnityEngine.Object)inTransform.parent == (UnityEngine.Object)inParent )
			return true;
		return GameUtility.IsParentInHierarchy( inTransform.parent, inParent );
	}

	public static bool IsChildInHierarchy( Transform inTransform, Transform inChild )
	{
		return GameUtility.IsParentInHierarchy( inChild, inTransform );
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

	public static void SetTooltipTransformInsideScreen( RectTransform inToolTipTransform, RectTransform inReferencePoint, [Optional] Vector3 inMouseOffset, bool inIgnoreRotation = false, RectTransform inScreenTransform = null )
	{
		Vector3 vector3_1 = new Vector3();
		if( (UnityEngine.Object)inReferencePoint == (UnityEngine.Object)null )
		{
			Vector3 vector3_2 = !( inMouseOffset == Vector3.zero ) ? inMouseOffset : new Vector3( 30f, 0.0f, 0.0f );
			float x = 0.0f;
			if( !inIgnoreRotation && (double)Input.mousePosition.x > (double)Screen.width * 0.5 )
			{
				vector3_2.x = -vector3_2.x;
				x = 1f;
			}
			inToolTipTransform.position = UIManager.instance.UICamera.ScreenToWorldPoint( Input.mousePosition + vector3_2 );
			inToolTipTransform.pivot = !inIgnoreRotation ? new Vector2( x, inToolTipTransform.pivot.y ) : new Vector2( 0.0f, inToolTipTransform.pivot.y );
		}
		else
		{
			inToolTipTransform.position = inReferencePoint.position;
			Vector3[] vectors = new Vector3[4];
			inReferencePoint.GetWorldCorners( vectors );
			float max = 0.0f;
			float min = 0.0f;
			GameUtility.GetMinMaxX( ref vectors, ref min, ref max );
			if( (double)inReferencePoint.position.x < 0.0 )
			{
				vector3_1.x = (float)( ( (double)max - (double)min ) / 2.0 );
				inToolTipTransform.pivot = new Vector2( 0.0f, inToolTipTransform.pivot.y );
			}
			else
			{
				vector3_1.x = (float)-( ( (double)max - (double)min ) / 2.0 );
				inToolTipTransform.pivot = new Vector2( 1f, inToolTipTransform.pivot.y );
			}
		}
		Vector3[] vectors1 = new Vector3[4];
		inToolTipTransform.GetWorldCorners( vectors1 );
		float max1 = 0.0f;
		float min1 = 0.0f;
		GameUtility.GetMinMaxY( ref vectors1, ref min1, ref max1 );
		if( (double)min1 < -1.0 )
			vector3_1.y += Mathf.Abs( -1f - min1 );
		else if( (double)max1 > 1.0 )
			vector3_1.y -= max1 - 1f;
		if( (UnityEngine.Object)inScreenTransform != (UnityEngine.Object)null )
		{
			float max2 = 0.0f;
			float min2 = 0.0f;
			GameUtility.GetMinMaxX( ref vectors1, ref min2, ref max2 );
			Vector3[] vectors2 = new Vector3[4];
			inScreenTransform.GetWorldCorners( vectors2 );
			float max3 = 0.0f;
			float min3 = 0.0f;
			GameUtility.GetMinMaxX( ref vectors2, ref min3, ref max3 );
			if( (double)min2 < (double)min3 )
				vector3_1.x = Mathf.Abs( min3 - min2 );
			else if( (double)max2 > (double)max3 )
				vector3_1.x = -1f * Mathf.Abs( max3 - max2 );
		}
		RectTransform rectTransform = inToolTipTransform;
		Vector3 vector3_3 = rectTransform.position + vector3_1;
		rectTransform.position = vector3_3;
	}

	public static void SetCanvasEnabled( Canvas inCanvas, bool inEnable )
	{
		if( !( (UnityEngine.Object)inCanvas != (UnityEngine.Object)null ) || inCanvas.isActiveAndEnabled == inEnable )
			return;
		GameUtility.SetRaycasterEnabled( inCanvas.gameObject.GetComponent<GraphicRaycaster>(), inEnable );
		inCanvas.enabled = inEnable;
	}

	public static void SetRaycasterEnabled( GraphicRaycaster inRaycaster, bool inEnable )
	{
		if( !( (UnityEngine.Object)inRaycaster != (UnityEngine.Object)null ) || inRaycaster.isActiveAndEnabled == inEnable )
			return;
		inRaycaster.enabled = inEnable;
	}

	public static bool HasParameterOfType( Animator animator, string name, AnimatorControllerParameterType type )
	{
		foreach( AnimatorControllerParameter parameter in animator.parameters )
		{
			if( parameter.type == type && parameter.name == name )
				return true;
		}
		return false;
	}

	public static bool HasStateWithName( Animator animator, string name )
	{
		return animator.HasState( 0, Animator.StringToHash( name ) );
	}

	public static string FormatChampionshipPoints( int inValue )
	{
		StringVariableParser.intValue1 = inValue;
		return Localisation.LocaliseID( "PSG_10010222", (GameObject)null );
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
		PrefGameSpeedUnits.Type speedUnits = App.instance.preferencesManager.gamePreferences.GetSpeedUnits();
		switch( speedUnits )
		{
			case PrefGameSpeedUnits.Type.MPH:
				return MathsUtility.NearestMultipleOf( inMiles, inToNearest ).ToString( (IFormatProvider)Localisation.numberFormatter ) + " " + Localisation.LocaliseID( "PSG_10000836", (GameObject)null );
			case PrefGameSpeedUnits.Type.KMPH:
				return MathsUtility.NearestMultipleOf( GameUtility.MilesToMeters( inMiles ) / 1000f, inToNearest ).ToString( (IFormatProvider)Localisation.numberFormatter ) + " " + Localisation.LocaliseID( "PSG_10000837", (GameObject)null );
			default:
				Debug.LogErrorFormat( "Could not find format for speed unit type {0}.", (object)speedUnits );
				return string.Empty;
		}
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
		}
	}

	public static string GetSpeedText( float inSpeedMetersPerSecond, float outMultipleOf = 1 )
	{
		PrefGameSpeedUnits.Type speedUnits = App.instance.preferencesManager.gamePreferences.GetSpeedUnits();
		switch( speedUnits )
		{
			case PrefGameSpeedUnits.Type.MPH:
				return ( (int)MathsUtility.NearestMultipleOf( GameUtility.MetersPerSecondToMilesPerHour( inSpeedMetersPerSecond ), outMultipleOf ) ).ToString( (IFormatProvider)Localisation.numberFormatter ) + Localisation.LocaliseID( "PSG_10000514", (GameObject)null );
			case PrefGameSpeedUnits.Type.KMPH:
				return ( (int)MathsUtility.NearestMultipleOf( GameUtility.MetersPerSecondToKilometersPerHour( inSpeedMetersPerSecond ), outMultipleOf ) ).ToString( (IFormatProvider)Localisation.numberFormatter ) + Localisation.LocaliseID( "PSG_10000515", (GameObject)null );
			default:
				Debug.LogErrorFormat( "Could not find format for speed unit type {0}.", (object)speedUnits );
				return string.Empty;
		}
	}

	public static float CelsiusToFahrenheit( float inCelsius )
	{
		return (float)( (double)inCelsius * 1.79999995231628 + 32.0 );
	}

	public static string GetTemperatureText( float inCelsius )
	{
		PrefGameTemperatureUnits.Type temperatureUnits = App.instance.preferencesManager.gamePreferences.GetTemperatureUnits();
		switch( temperatureUnits )
		{
			case PrefGameTemperatureUnits.Type.Celsius:
				return ( (int)inCelsius ).ToString() + (object)'°' + "C";
			case PrefGameTemperatureUnits.Type.Fahrenheit:
				return ( (int)GameUtility.CelsiusToFahrenheit( inCelsius ) ).ToString() + (object)'°' + "F";
			default:
				Debug.LogErrorFormat( "Could not find format for temperature type {0}.", (object)temperatureUnits );
				return string.Empty;
		}
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
		outFt = Mathf.FloorToInt( num / 12f );
		outInches = num % 12f;
	}

	public static string GetHeightText( float inCm )
	{
		PrefGameHeight.Type heightUnits = App.instance.preferencesManager.gamePreferences.GetHeightUnits();
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
		}
	}

	public static string GetWeightText( float inKg, int inDecimalPlaces = 2 )
	{
		PrefGameWeight.Type weightUnits = App.instance.preferencesManager.gamePreferences.GetWeightUnits();
		switch( weightUnits )
		{
			case PrefGameWeight.Type.kg:
				StringVariableParser.floatValue1 = (float)Mathf.RoundToInt( inKg * Mathf.Pow( 10f, (float)inDecimalPlaces ) ) / Mathf.Pow( 10f, (float)inDecimalPlaces );
				return Localisation.LocaliseID( "PSG_10010624", (GameObject)null );
			case PrefGameWeight.Type.lbs:
				StringVariableParser.floatValue1 = (float)Mathf.RoundToInt( GameUtility.KilogramsToPounds( inKg ) );
				return Localisation.LocaliseID( "PSG_10010625", (GameObject)null );
			default:
				Debug.LogErrorFormat( "Could not find format for weight type {0}.", (object)weightUnits );
				return string.Empty;
		}
	}

	public static string GetLapTimeText( float inLapTime, bool inIsCurrentLap )
	{
		using( GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe() )
		{
			float a = inLapTime;
			StringBuilder stringBuilder = builderSafe.stringBuilder;
			int num1 = (int)( (double)inLapTime / 60.0 );
			inLapTime -= (float)( num1 * 60 );
			int num2 = (int)inLapTime;
			inLapTime -= (float)num2;
			int num3 = (int)( (double)inLapTime * 1000.0 );
			if( !MathsUtility.ApproximatelyZero( a ) )
			{
				if( num1 == 0 )
					GameUtility.Format( stringBuilder, Localisation.GetLapTimeFormatting( true ), num2, num3 );
				else
					GameUtility.Format( stringBuilder, Localisation.GetLapTimeFormatting( false ), num1, num2, num3 );
				if( inIsCurrentLap )
					stringBuilder.Remove( stringBuilder.Length - 2, 2 );
			}
			return stringBuilder.ToString();
		}
	}

	public static void SetGapTimeText( TextMeshProUGUI inTextElement, RaceEventResults.ResultData inResult, bool inGapToLeader = true )
	{
		float inTimeGap = !inGapToLeader ? inResult.gapToAhead : inResult.gapToLeader;
		float f = !inGapToLeader ? (float)inResult.lapsToAhead : (float)inResult.lapsToLeader;
		if( inResult.carState == RaceEventResults.ResultData.CarState.None )
		{
			if( (double)inTimeGap != 0.0 )
			{
				if( (double)f == 0.0 )
				{
					inTextElement.text = GameUtility.GetGapTimeText( inTimeGap, false );
				}
				else
				{
					string inID = (double)f != 1.0 ? "PSG_10000435" : "PSG_10000434";
					inTextElement.text = string.Format( "(+{0} {1})", (object)Mathf.Abs( f ), (object)Localisation.LocaliseID( inID, (GameObject)null ) );
				}
			}
			else
				inTextElement.text = "-";
		}
		else
			inTextElement.text = Localisation.LocaliseEnum( (Enum)inResult.carState );
	}

	public static string GetGapTimeText( float inTimeGap, bool trimTo1DecimalPlace = false )
	{
		using( GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe() )
		{
			StringBuilder stringBuilder = builderSafe.stringBuilder;
			float num1 = Mathf.Abs( inTimeGap );
			int num2 = (int)num1;
			int num3 = (int)( (double)( num1 - (float)num2 ) * 1000.0 );
			if( (double)inTimeGap > 0.0 || MathsUtility.ApproximatelyZero( inTimeGap ) )
			{
				stringBuilder.Append( "+" );
				GameUtility.Format( stringBuilder, Localisation.GetLapTimeFormatting( true ), num2, num3 );
			}
			else
			{
				stringBuilder.Append( "-" );
				GameUtility.Format( stringBuilder, Localisation.GetLapTimeFormatting( true ), num2, num3 );
			}
			if( trimTo1DecimalPlace )
				stringBuilder.Remove( stringBuilder.Length - 2, 2 );
			return stringBuilder.ToString();
		}
	}

	public static string GetSectorTimeText( float inTimeSector )
	{
		Debug.Assert( (double)inTimeSector >= 0.0 );
		using( GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe() )
		{
			float a = inTimeSector;
			StringBuilder stringBuilder = builderSafe.stringBuilder;
			int num1 = (int)( (double)inTimeSector / 60.0 );
			inTimeSector -= (float)( num1 * 60 );
			int num2 = (int)inTimeSector;
			inTimeSector -= (float)num2;
			int num3 = (int)( (double)inTimeSector * 1000.0 );
			if( !MathsUtility.ApproximatelyZero( a ) )
			{
				if( num1 == 0 )
					GameUtility.Format( stringBuilder, Localisation.GetLapTimeFormatting( true ), num2, num3 );
				else
					GameUtility.Format( stringBuilder, Localisation.GetLapTimeFormatting( false ), num1, num2, num3 );
			}
			return stringBuilder.ToString();
		}
	}

	public static float GetSectorTime( float[] inSectors, int inSector )
	{
		return inSector != 1 ? ( inSector != 2 ? inSectors[0] : inSectors[2] - inSectors[1] ) : inSectors[1] - inSectors[0];
	}

	public static float GetRangePerformance( float inValue, Range inTotalRange, Range inOptimalRange, EasingUtility.Easing inCurve )
	{
		float t = 1f;
		inValue = Mathf.Clamp( inValue, inTotalRange.min, inTotalRange.max );
		if( (double)inValue < (double)inOptimalRange.min )
			t = Mathf.Lerp( 0.0f, 1f, Mathf.Clamp01( (float)( ( (double)inValue - (double)inTotalRange.min ) / ( (double)inOptimalRange.min - (double)inTotalRange.min ) ) ) );
		else if( (double)inValue > (double)inOptimalRange.max )
			t = Mathf.Lerp( 1f, 0.0f, Mathf.Clamp01( (float)( ( (double)inValue - (double)inOptimalRange.max ) / ( (double)inTotalRange.max - (double)inOptimalRange.max ) ) ) );
		return EasingUtility.EaseByType( inCurve, 0.0f, 1f, t );
	}

	public static Sprite GetTyreSprite( TyreSet.Compound inCompound )
	{
		Sprite sprite = (Sprite)null;
		switch( inCompound )
		{
			case TyreSet.Compound.SuperSoft:
				sprite = App.instance.atlasManager.GetSprite( AtlasManager.Atlas.Simulation1, "RaceHUD-Standings-HudTyresIconSSoft" );
				break;
			case TyreSet.Compound.Soft:
				sprite = App.instance.atlasManager.GetSprite( AtlasManager.Atlas.Simulation1, "RaceHUD-Standings-HudTyresIconSoft" );
				break;
			case TyreSet.Compound.Medium:
				sprite = App.instance.atlasManager.GetSprite( AtlasManager.Atlas.Simulation1, "RaceHUD-Standings-HudTyresIconMed" );
				break;
			case TyreSet.Compound.Hard:
				sprite = App.instance.atlasManager.GetSprite( AtlasManager.Atlas.Simulation1, "RaceHUD-Standings-HudTyresIconHard" );
				break;
			case TyreSet.Compound.Intermediate:
				sprite = App.instance.atlasManager.GetSprite( AtlasManager.Atlas.Simulation1, "RaceHUD-Standings-HudTyresIconInters" );
				break;
			case TyreSet.Compound.Wet:
				sprite = App.instance.atlasManager.GetSprite( AtlasManager.Atlas.Simulation1, "RaceHUD-Standings-HudTyresIconWet" );
				break;
		}
		return sprite;
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
		string str1 = !( inLanguage != string.Empty ) ? Localisation.currentLanguage : inLanguage;
		GameUtility.GetLanguageCulture( str1 );
		CultureInfo languageCulture = GameUtility.GetLanguageCulture( Localisation.currentLanguage );
		string str2 = !Localisation.UseOrdinalsInDates( str1 ) ? inStartDate.Day.ToString() + "-" + (object)inEndDate.Day : GameUtility.OrdinalString( inStartDate.Day, str1 ) + "-" + GameUtility.OrdinalString( inEndDate.Day, str1 );
		using( GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe() )
		{
			StringBuilder stringBuilder = builderSafe.stringBuilder;
			switch( App.instance.preferencesManager.GetSettingEnum<PrefGameDateFormat.Type>( Preference.pName.Game_DateFormat, false ) )
			{
				case PrefGameDateFormat.Type.DDMMYYYY:
					stringBuilder.Append( str2 );
					stringBuilder.Append( inEndDate.ToString( " MMMM", (IFormatProvider)languageCulture ) );
					break;
				case PrefGameDateFormat.Type.MMDDYYYY:
					stringBuilder.Append( inEndDate.ToString( "MMMM ", (IFormatProvider)languageCulture ) );
					stringBuilder.Append( str2 );
					break;
				default:
					return "Could not get date format";
			}
			if( string.Equals( str1, "hungarian", StringComparison.OrdinalIgnoreCase ) )
			{
				stringBuilder.Insert( 0, ". " );
				stringBuilder.Insert( 0, inEndDate.Year.ToString() );
			}
			else
			{
				stringBuilder.Append( " " );
				stringBuilder.Append( inEndDate.Year.ToString() );
			}
			return stringBuilder.ToString();
		}
	}

	public static string FormatDateTimeToDateNoYear( DateTime inDate, string inLanguage = "" )
	{
		string str = !( inLanguage != string.Empty ) ? Localisation.currentLanguage : inLanguage;
		CultureInfo languageCulture = GameUtility.GetLanguageCulture( str );
		using( GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe() )
		{
			StringBuilder stringBuilder = builderSafe.stringBuilder;
			switch( App.instance.preferencesManager.GetSettingEnum<PrefGameDateFormat.Type>( Preference.pName.Game_DateFormat, false ) )
			{
				case PrefGameDateFormat.Type.DDMMYYYY:
					GameUtility.AppendDayToDateString( stringBuilder, inDate, str );
					if( string.Equals( str, "hungarian", StringComparison.OrdinalIgnoreCase ) )
					{
						stringBuilder.Append( " " );
						stringBuilder.Append( GameUtility.ForceCapitalizeFirstLetter( inDate.ToString( "MMMM", (IFormatProvider)languageCulture ) ) );
					}
					else
						stringBuilder.Append( inDate.ToString( " MMMM", (IFormatProvider)languageCulture ) );
					return stringBuilder.ToString();
				case PrefGameDateFormat.Type.MMDDYYYY:
					if( string.Equals( str, "hungarian", StringComparison.OrdinalIgnoreCase ) )
						stringBuilder.Append( GameUtility.ForceCapitalizeFirstLetter( inDate.ToString( "MMMM ", (IFormatProvider)languageCulture ) ) );
					else
						stringBuilder.Append( inDate.ToString( "MMMM ", (IFormatProvider)languageCulture ) );
					GameUtility.AppendDayToDateString( stringBuilder, inDate, str );
					return stringBuilder.ToString();
				default:
					return "Could not get date format!";
			}
		}
	}

	public static string FormatDateTimeToAbbrevMonthNoYear( DateTime inDate, string inLanguage = "" )
	{
		string inLanguage1 = !( inLanguage != string.Empty ) ? Localisation.currentLanguage : inLanguage;
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
		}
	}

	public static string FormatDateTimeToShortDateString( DateTime inDate )
	{
		return inDate.ToString( App.instance.preferencesManager.gamePreferences.GetCurrentDateFormat() );
	}

	public static string FormatDateTimeToLongDateStringWithDOW( DateTime inDate, string inLanguage = "" )
	{
		string str = !( inLanguage != string.Empty ) ? Localisation.currentLanguage : inLanguage;
		CultureInfo languageCulture = GameUtility.GetLanguageCulture( str );
		using( GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe() )
		{
			StringBuilder stringBuilder = builderSafe.stringBuilder;
			if( string.Equals( str, "hungarian", StringComparison.OrdinalIgnoreCase ) )
			{
				stringBuilder.Append( GameUtility.FormatDateTimeToLongDateString( inDate, str ) );
				stringBuilder.Append( ", " );
				stringBuilder.Append( GameUtility.ForceCapitalizeFirstLetter( inDate.ToString( "dddd", (IFormatProvider)languageCulture ) ) );
				return stringBuilder.ToString();
			}
			stringBuilder.Append( inDate.ToString( "dddd ", (IFormatProvider)languageCulture ) );
			stringBuilder.Append( GameUtility.FormatDateTimeToLongDateString( inDate, str ) );
			return stringBuilder.ToString();
		}
	}

	public static string FormatDateTimeToLongDateString( DateTime inDate, string inLanguage = "" )
	{
		string str = !( inLanguage != string.Empty ) ? Localisation.currentLanguage : inLanguage;
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
		}
	}

	private static void AppendDayToDateString( StringBuilder inStringBuilder, DateTime inDate, string inLanguage )
	{
		if( Localisation.UseOrdinalsInDates( inLanguage ) )
			inStringBuilder.Append( GameUtility.OrdinalString( inDate.Day, inLanguage ) );
		else
			inStringBuilder.Append( inDate.Day );
	}

	public static string FormatTimeSpan( TimeSpan inTimeSpan )
	{
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
		return stringBuilder.ToString();
	}

	public static string FormatTimeSpanWeeks( TimeSpan inTimeSpan )
	{
		StringBuilder stringBuilder = new StringBuilder();
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
		return stringBuilder.ToString();
	}

	public static string FormatTimeSpanDays( TimeSpan inTimeSpan )
	{
		StringBuilder stringBuilder = new StringBuilder();
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
		return stringBuilder.ToString();
	}

	public static string FormatTimeSpanDays( float inDays )
	{
		StringBuilder stringBuilder = new StringBuilder();
		StringVariableParser.ordinalNumberString = inDays.ToString();
		stringBuilder.Append( (double)inDays != 1.0 ? Localisation.LocaliseID( "PSG_10010542", (GameObject)null ) : Localisation.LocaliseID( "PSG_10010541", (GameObject)null ) );
		return stringBuilder.ToString();
	}

	public static string FormatTimeSpanMinutes( TimeSpan inTimeSpan )
	{
		return inTimeSpan.Minutes.ToString() + " " + Localisation.LocaliseID( "PSG_10002863", (GameObject)null ) + " " + inTimeSpan.Seconds.ToString() + " " + Localisation.LocaliseID( "PSG_10002864", (GameObject)null );
	}

	public static GameUtility.Month GetMonthAsEnum( DateTime inDateTime )
	{
		return (GameUtility.Month)( inDateTime.Month - 1 );
	}

	public static string FormatForPosition( int inPosition, string inLanguage = null )
	{
		if( inLanguage == null )
			inLanguage = Localisation.currentLanguage;
		return GameUtility.OrdinalString( inPosition, inLanguage );
	}

	public static string FormatForPositionOrAbove( int inPosition, string inLanguage = null )
	{
		if( inLanguage == null )
			inLanguage = Localisation.currentLanguage;
		if( inPosition <= 1 )
			return GameUtility.OrdinalString( inPosition, inLanguage );
		StringVariableParser.ordinalNumberString = GameUtility.OrdinalString( inPosition, inLanguage );
		return Localisation.LocaliseID( "PSG_10010361", (GameObject)null );
	}

	public static string FormatForBestOnGrid( int inPosition, string inLanguage = null )
	{
		if( inLanguage == null )
			inLanguage = Localisation.currentLanguage;
		if( inPosition == 1 )
			return Localisation.LocaliseID( "PSG_10010857", (GameObject)null );
		if( inPosition == 10 )
			return Localisation.LocaliseID( "PSG_10010858", (GameObject)null );
		StringVariableParser.ordinalNumberString = GameUtility.FormatForPosition( inPosition, inLanguage );
		return Localisation.LocaliseID( "PSG_10010400", (GameObject)null );
	}

	public static string FormatForLaps( float inLaps )
	{
		StringVariableParser.stringValue1 = inLaps.ToString( "0.00" );
		if( Mathf.Approximately( inLaps, 0.0f ) || (double)Mathf.Abs( inLaps ) > 1.0 )
			return Localisation.LocaliseID( "PSG_10011103", (GameObject)null );
		return Localisation.LocaliseID( "PSG_10011102", (GameObject)null );
	}

	public static string FormatForLaps( int inLaps )
	{
		StringVariableParser.stringValue1 = inLaps.ToString();
		if( Mathf.Approximately( (float)inLaps, 0.0f ) || Mathf.Abs( inLaps ) > 1 )
			return Localisation.LocaliseID( "PSG_10011103", (GameObject)null );
		return Localisation.LocaliseID( "PSG_10011102", (GameObject)null );
	}

	public static string GetPercentageText( float inNormValue, float inDecimalPlaces = 0, bool inSign = false, bool inForceDecimalPlaces = false )
	{
		string format;
		if( (double)inDecimalPlaces <= 0.0 )
		{
			format = "0";
		}
		else
		{
			format = "0.";
			for( int index = 0; (double)index < (double)inDecimalPlaces; ++index )
				format = !inForceDecimalPlaces ? format + "#" : format + "0";
		}
		string empty = string.Empty;
		if( inSign && (double)inNormValue >= 0.0 )
			empty += "+";
		return empty + MathsUtility.RoundToDecimal( inNormValue * 100f ).ToString( format ) + "%";
	}

	public static string GetLocalised12HourTime( DateTime inDate, bool include_seconds )
	{
		StringVariableParser.intValue1 = inDate.Hour != 0 ? ( inDate.Hour <= 12 ? inDate.Hour : inDate.Hour - 12 ) : 12;
		StringVariableParser.intValue2 = inDate.Minute;
		StringVariableParser.includeSeconds = include_seconds;
		StringVariableParser.intValue3 = inDate.Second;
		if( inDate.Hour > 11 )
			return Localisation.LocaliseID( "PSG_10010254", (GameObject)null );
		return Localisation.LocaliseID( "PSG_10010253", (GameObject)null );
	}

	public static string GetLocalisedDay( DateTime inDate )
	{
		return Localisation.LocaliseEnum( (Enum)(GameUtility.Day)inDate.DayOfWeek );
	}

	public static string GetLocalisedDay( DayOfWeek inDay )
	{
		return Localisation.LocaliseEnum( (Enum)(GameUtility.Day)inDay );
	}

	public static string GetLocalisedMonth( DateTime inDate )
	{
		return GameUtility.GetLocalisedMonth( inDate.Month - 1 );
	}

	public static string GetLocalisedMonth( int month_num )
	{
		return Localisation.LocaliseEnum( (Enum)(GameUtility.Month)month_num );
	}

	public static string GetLocalisedMonthShort( DateTime inDate )
	{
		return GameUtility.GetLocalisedMonthShort( inDate.Month - 1 );
	}

	public static string GetLocalisedMonthShort( int month_num )
	{
		return Localisation.LocaliseEnum( (Enum)(GameUtility.MonthShort)month_num );
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

	public static CultureInfo GetLanguageCulture( string inLanguage )
	{
		string lower = inLanguage.ToLower();
		if( lower != null )
		{
			// ISSUE: reference to a compiler-generated field
			if( GameUtility.\u003C\u003Ef__switch\u0024map41 == null)
      {
				// ISSUE: reference to a compiler-generated field
				GameUtility.\u003C\u003Ef__switch\u0024map41 = new Dictionary<string, int>( 10 )
		{
		  {
			"french",
			0
		  },
		  {
			"italian",
			1
		  },
		  {
			"german",
			2
		  },
		  {
			"spanish",
			3
		  },
		  {
			"brasilian",
			4
		  },
		  {
			"dutch",
			5
		  },
		  {
			"hungarian",
			6
		  },
		  {
			"polish",
			7
		  },
		  {
			"russian",
			8
		  },
		  {
			"english",
			9
		  }
		};
			}
			int num;
			// ISSUE: reference to a compiler-generated field
			if( GameUtility.\u003C\u003Ef__switch\u0024map41.TryGetValue( lower, out num ))
      {
				switch( num )
				{
					case 0:
						return new CultureInfo( 1036 );
					case 1:
						return new CultureInfo( 1040 );
					case 2:
						return new CultureInfo( 1031 );
					case 3:
						return new CultureInfo( 3082 );
					case 4:
						return new CultureInfo( 1046 );
					case 5:
						return new CultureInfo( 1043 );
					case 6:
						return new CultureInfo( 1038 );
					case 7:
						return new CultureInfo( 1045 );
					case 8:
						return new CultureInfo( 1049 );
				}
			}
		}
		return new CultureInfo( 2057 );
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

	public static string GetCurrencyStringWithSign( long inValue, int inDecimalPlaces = 0 )
	{
		if( inValue > 0L )
			return "+" + GameUtility.GetCurrencyString( inValue, 0 );
		return GameUtility.GetCurrencyString( inValue, 0 );
	}

	public static string GetCurrencyString( long inValue, int inDecimalPlaces = 0 )
	{
		CultureInfo currencyCultureFormat = App.instance.preferencesManager.gamePreferences.GetCurrencyCultureFormat();
		return inValue.ToString( "C0" + inDecimalPlaces.ToString(), (IFormatProvider)currencyCultureFormat.NumberFormat );
	}

	public static string GetCurrencyStringMillions( long inValue, int inDecimalPlaces = 0 )
	{
		return GameUtility.GetCurrencyStringMillions( (float)inValue, inDecimalPlaces );
	}

	public static string GetCurrencyStringMillions( float inValue, int inDecimalPlaces = 0 )
	{
		CultureInfo currencyCultureFormat = App.instance.preferencesManager.gamePreferences.GetCurrencyCultureFormat();
		string str = inValue.ToString( "C0" + inDecimalPlaces.ToString(), (IFormatProvider)currencyCultureFormat.NumberFormat );
		if( char.IsDigit( str[str.Length - 1] ) )
			return str + Localisation.LocaliseID( "PSG_10010407", (GameObject)null );
		char ch = str[str.Length - 1];
		return str.Substring( 0, str.Length - 1 ) + Localisation.LocaliseID( "PSG_10010407", (GameObject)null ) + " " + (object)ch;
	}

	public static Color GetCurrencyColor( int inValue )
	{
		return GameUtility.GetCurrencyColor( (long)inValue );
	}

	public static Color GetCurrencyColor( float inValue )
	{
		return GameUtility.GetCurrencyColor( (long)inValue );
	}

	public static Color GetCurrencyColor( long inValue )
	{
		if( inValue > 0L )
			return UIConstants.positiveColor;
		if( inValue == 0L )
			return UIConstants.whiteColor;
		return UIConstants.negativeColor;
	}

	public static Color GetCurrencyBackingColor( long inValue )
	{
		if( inValue > 0L )
			return UIConstants.financeBackingPositiveColor;
		return UIConstants.financeBackingNegativeColor;
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

	public static float RoundCurrency( float inValue )
	{
		bool flag = (double)inValue < 0.0;
		if( flag )
			inValue = Mathf.Abs( inValue );
		if( (double)inValue > 1000.0 )
			inValue = (float)Mathf.RoundToInt( inValue / 1000f ) * 1000f;
		else if( (double)inValue > 100.0 )
			inValue = (float)Mathf.RoundToInt( inValue / 100f ) * 100f;
		else if( (double)inValue > 10.0 )
			inValue = (float)Mathf.RoundToInt( inValue / 10f ) * 10f;
		if( flag )
			return -inValue;
		return inValue;
	}

	public static string FormatNumberString( int inValue )
	{
		return inValue.ToString( "N0", (IFormatProvider)Localisation.numberFormatter );
	}

	public static void SnapScrollRectTo( RectTransform target, ScrollRect inScrollRect, GameUtility.AnchorType inAnchorType, GameUtility.AnchorLocation inLocation )
	{
		App.instance.StartCoroutine( GameUtility.ResetElasticity( inScrollRect, 1 ) );
		inScrollRect.elasticity = 0.0f;
		LayoutRebuilder.ForceRebuildLayoutImmediate( inScrollRect.content );
		RectTransform content = inScrollRect.content;
		Vector2 vector2 = (Vector2)inScrollRect.transform.InverseTransformPoint( content.position ) - (Vector2)inScrollRect.transform.InverseTransformPoint( target.position );
		switch( inAnchorType )
		{
			case GameUtility.AnchorType.X:
				vector2.y = content.anchoredPosition.y;
				break;
			case GameUtility.AnchorType.Y:
				switch( inLocation )
				{
					case GameUtility.AnchorLocation.Top:
						vector2.y -= target.sizeDelta.y / 2f;
						break;
					case GameUtility.AnchorLocation.Center:
						vector2.y -= inScrollRect.GetComponent<RectTransform>().sizeDelta.y / 2f;
						break;
					case GameUtility.AnchorLocation.Bottom:
						vector2.y -= inScrollRect.GetComponent<RectTransform>().sizeDelta.y - target.sizeDelta.y / 2f;
						break;
				}
				vector2.x = content.anchoredPosition.x;
				break;
		}
		content.anchoredPosition = vector2;
	}

	[DebuggerHidden]
	public static IEnumerator ResetElasticity( ScrollRect inScrollRect, int inFrameWait )
	{
		// ISSUE: object of a compiler-generated type is created
		return (IEnumerator)new GameUtility.\u003CResetElasticity\u003Ec__Iterator23()
  
	{
			inScrollRect = inScrollRect,
      inFrameWait = inFrameWait,
      \u003C\u0024\u003EinScrollRect = inScrollRect,
      \u003C\u0024\u003EinFrameWait = inFrameWait

	};
	}

	public static Color ColorFromInts( int inR, int inG, int inB, int inA )
	{
		return new Color( (float)inR / (float)byte.MaxValue, (float)inG / (float)byte.MaxValue, (float)inB / (float)byte.MaxValue, (float)inA / (float)byte.MaxValue );
	}

	public static Color ColorFromInts( int inR, int inG, int inB )
	{
		return GameUtility.ColorFromInts( inR, inG, inB, (int)byte.MaxValue );
	}

	public static string ColorToRichTextHex( Color inColor )
	{
		return "<color=" + GameUtility.ColorToHex( (Color32)inColor ) + ">";
	}

	public static string ColorToHex( Color32 inColor )
	{
		return "#" + ( inColor.r.ToString( "X2" ) + inColor.g.ToString( "X2" ) + inColor.b.ToString( "X2" ) + inColor.a.ToString( "X2" ) );
	}

	public static Color HexStringToColour( string hexString )
	{
		if( hexString.Length != 7 )
			throw new ArgumentException();
		return GameUtility.ColorFromInts( Convert.ToInt32( hexString.Substring( 1, 2 ), 16 ), Convert.ToInt32( hexString.Substring( 3, 2 ), 16 ), Convert.ToInt32( hexString.Substring( 5, 2 ), 16 ) );
	}

	public static Color RGBCSVIntStringToColour( string rgbstring )
	{
		string[] strArray = rgbstring.Split( ',' );
		if( strArray.Length != 3 )
			throw new ArgumentException();
		return new Color( (float)int.Parse( strArray[0] ) / (float)byte.MaxValue, (float)int.Parse( strArray[1] ) / (float)byte.MaxValue, (float)int.Parse( strArray[2] ) / (float)byte.MaxValue );
	}

	public static string ColourToRGBComponentString( Color colour )
	{
		return ( (int)( (double)colour.r * (double)byte.MaxValue ) ).ToString() + "," + ( (int)( (double)colour.g * (double)byte.MaxValue ) ).ToString() + "," + ( (int)( (double)colour.b * (double)byte.MaxValue ) ).ToString();
	}

	public static bool ColorEquals( Color inA, Color inB )
	{
		if( Mathf.Approximately( inA.a, inB.a ) && Mathf.Approximately( inA.r, inB.r ) && Mathf.Approximately( inA.g, inB.g ) )
			return Mathf.Approximately( inA.b, inB.b );
		return false;
	}

	public static string OrdinalString( int inNumber, string inLanguage )
	{
		return Localisation.LocaliseID( GameUtility.OrdinalLocalisationID( inNumber ), inLanguage, (GameObject)null, string.Empty );
	}

	public static string OrdinalLocalisationID( int inNumber )
	{
		switch( inNumber )
		{
			case 0:
				Debug.LogWarning( (object)"Tried to ordinize 0 - possibly unset value. Defaulting to 1st.", (UnityEngine.Object)null );
				return "PSG_10000846";
			case 1:
				return "PSG_10000846";
			case 2:
				return "PSG_10000847";
			case 3:
				return "PSG_10000848";
			case 4:
				return "PSG_10000849";
			case 5:
				return "PSG_10000850";
			case 6:
				return "PSG_10000851";
			case 7:
				return "PSG_10000852";
			case 8:
				return "PSG_10000853";
			case 9:
				return "PSG_10000854";
			case 10:
				return "PSG_10000855";
			case 11:
				return "PSG_10000856";
			case 12:
				return "PSG_10000861";
			case 13:
				return "PSG_10000862";
			case 14:
				return "PSG_10000864";
			case 15:
				return "PSG_10000865";
			case 16:
				return "PSG_10000866";
			case 17:
				return "PSG_10000867";
			case 18:
				return "PSG_10000868";
			case 19:
				return "PSG_10000869";
			case 20:
				return "PSG_10000870";
			case 21:
				return "PSG_10000857";
			case 22:
				return "PSG_10000858";
			case 23:
				return "PSG_10000859";
			case 24:
				return "PSG_10000860";
			case 25:
				return "PSG_10009397";
			case 26:
				return "PSG_10009398";
			case 27:
				return "PSG_10009399";
			case 28:
				return "PSG_10009400";
			case 29:
				return "PSG_10009401";
			case 30:
				return "PSG_10009402";
			case 31:
				return "PSG_10009403";
			case 32:
				return "PSG_10011158";
			case 33:
				return "PSG_10011159";
			case 34:
				return "PSG_10011160";
			case 35:
				return "PSG_10011161";
			case 36:
				return "PSG_10011162";
			case 37:
				return "PSG_10011163";
			case 38:
				return "PSG_10011164";
			case 39:
				return "PSG_10011165";
			case 40:
				return "PSG_10011166";
			case 41:
				return "PSG_10011167";
			case 42:
				return "PSG_10011168";
			case 43:
				return "PSG_10011169";
			case 44:
				return "PSG_10011170";
			case 45:
				return "PSG_10011171";
			case 46:
				return "PSG_10011172";
			case 47:
				return "PSG_10011173";
			case 48:
				return "PSG_10011174";
			case 49:
				return "PSG_10011175";
			case 50:
				return "PSG_10011176";
			case 51:
				return "PSG_10011177";
			case 52:
				return "PSG_10011178";
			case 53:
				return "PSG_10011179";
			case 54:
				return "PSG_10011180";
			case 55:
				return "PSG_10011181";
			case 56:
				return "PSG_10011182";
			case 57:
				return "PSG_10011183";
			case 58:
				return "PSG_10011184";
			case 59:
				return "PSG_10011185";
			case 60:
				return "PSG_10011186";
			case 61:
				return "PSG_10011187";
			case 62:
				return "PSG_10011188";
			case 63:
				return "PSG_10011189";
			case 64:
				return "PSG_10011190";
			case 65:
				return "PSG_10011191";
			case 66:
				return "PSG_10011192";
			case 67:
				return "PSG_10011193";
			case 68:
				return "PSG_10011194";
			case 69:
				return "PSG_10011195";
			case 70:
				return "PSG_10011196";
			case 71:
				return "PSG_10011197";
			case 72:
				return "PSG_10011198";
			case 73:
				return "PSG_10011199";
			case 74:
				return "PSG_10011200";
			case 75:
				return "PSG_10011201";
			case 76:
				return "PSG_10011202";
			case 77:
				return "PSG_10011203";
			case 78:
				return "PSG_10011204";
			case 79:
				return "PSG_10011205";
			case 80:
				return "PSG_10011206";
			case 81:
				return "PSG_10011207";
			case 82:
				return "PSG_10011208";
			case 83:
				return "PSG_10011209";
			case 84:
				return "PSG_10011210";
			case 85:
				return "PSG_10011211";
			case 86:
				return "PSG_10011212";
			case 87:
				return "PSG_10011213";
			case 88:
				return "PSG_10011214";
			case 89:
				return "PSG_10011215";
			case 90:
				return "PSG_10011216";
			case 91:
				return "PSG_10011217";
			case 92:
				return "PSG_10011218";
			case 93:
				return "PSG_10011219";
			case 94:
				return "PSG_10011220";
			case 95:
				return "PSG_10011221";
			case 96:
				return "PSG_10011222";
			case 97:
				return "PSG_10011223";
			case 98:
				return "PSG_10011224";
			case 99:
				return "PSG_10011225";
			case 100:
				return "PSG_10011226";
			default:
				Debug.LogWarningFormat( "Tried to ordinalize the number {0} when current values only go to 31", (object)inNumber );
				return inNumber.ToString();
		}
	}

	public static string SecondsToString( float number )
	{
		return number.ToString() + " " + Localisation.LocaliseID( "PSG_10002864", (GameObject)null );
	}

	public static string FormatSecondsToString( float inNumber, int inNumberDecimalPlaces = 1 )
	{
		bool flag = (double)inNumber % 1.0 == 0.0;
		string format = "0.";
		for( int index = 0; index < inNumberDecimalPlaces; ++index )
			format += "0";
		return ( !flag ? inNumber.ToString( format ) : Mathf.RoundToInt( inNumber ).ToString() ) + " " + Localisation.LocaliseID( "PSG_10002864", (GameObject)null );
	}

	public static string FormatSecondsToStringWithSign( float inNumber )
	{
		bool flag = (double)inNumber % 1.0 == 0.0;
		return ( (double)inNumber < 0.0 ? string.Empty : "+" ) + ( !flag ? inNumber.ToString( "0.0", (IFormatProvider)Localisation.numberFormatter ) : Mathf.RoundToInt( inNumber ).ToString() ) + " " + Localisation.LocaliseID( "PSG_10002864", (GameObject)null );
	}

	public static string FormatMinutesToString( int inNumber )
	{
		StringVariableParser.ordinalNumberString = inNumber.ToString( "0.0", (IFormatProvider)Localisation.numberFormatter );
		return Localisation.LocaliseID( "PSG_10010417", (GameObject)null );
	}

	public static string FormatMilesToString( int inNumber )
	{
		StringVariableParser.ordinalNumberString = inNumber.ToString( "0.0", (IFormatProvider)Localisation.numberFormatter );
		return Localisation.LocaliseID( "PSG_10010640", (GameObject)null );
	}

	public static void SetLoadingTip( TextMeshProUGUI inTextelement )
	{
		DialogQuery inQuery = new DialogQuery();
		inQuery.AddCriteria( "Source", "LoadingHints" );
		inQuery.AddCriteria( "Who", "HUDText" );
		DialogRule dialogRule = App.instance.dialogRulesManager.ProcessQuery( inQuery, false );
		inTextelement.text = Localisation.LocaliseID( dialogRule.localisationID, (GameObject)null );
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

	public static void SetImageFillAmountIfDifferent( Image image, float fillAmount, float tolerance = 0.001953125f )
	{
		if( float.IsNaN( fillAmount ) || float.IsInfinity( fillAmount ) )
		{
			fillAmount = 0.0f;
			Debug.LogErrorFormat( "Cannot set {0} fill to equal NAN or Inf!", (object)image.name );
		}
		if( (double)fillAmount > 1.0 || (double)fillAmount < 0.0 )
			fillAmount = Mathf.Clamp01( fillAmount );
		if( MathsUtility.Approximately( image.fillAmount, fillAmount, tolerance ) )
			return;
		image.fillAmount = fillAmount;
	}

	public static void SetSliderAmountIfDifferent( Slider inSlider, float inValue, float inSliderSteps = 1000f )
	{
		Debug.Assert( (double)inSliderSteps > 0.0, "Cannot set a slider with 0 steps!" );
		if( float.IsNaN( inValue ) || float.IsInfinity( inValue ) )
		{
			inValue = 0.0f;
			Debug.LogErrorFormat( "Cannot set {0} slider to equal NAN!", (object)inSlider.name );
		}
		if( (double)inValue > (double)inSlider.maxValue || (double)inValue < (double)inSlider.minValue )
			inValue = Mathf.Clamp( inValue, inSlider.minValue, inSlider.maxValue );
		if( MathsUtility.Approximately( inValue, inSlider.value, Mathf.Abs( inSlider.maxValue - inSlider.minValue ) / inSliderSteps ) )
			return;
		inSlider.value = inValue;
	}

	public static void SetInteractable( Selectable selectable, bool interactable )
	{
		if( selectable.interactable == interactable )
			return;
		selectable.interactable = interactable;
	}

	public static void SetInteractable( Button button, bool interactable )
	{
		if( button.interactable == interactable )
			return;
		button.interactable = interactable;
	}

	public static void SetAlpha( CanvasGroup inCanvasGroup, float inAlpha )
	{
		if( MathsUtility.Approximately( inCanvasGroup.alpha, inAlpha, 0.01f ) )
			return;
		inCanvasGroup.alpha = inAlpha;
	}

	public static void SetColorBlock( Selectable selectable, ColorBlock colours )
	{
		if( !( (UnityEngine.Object)selectable != (UnityEngine.Object)null ) || !( selectable.colors != colours ) )
			return;
		selectable.colors = colours;
	}

	public static void DisableIfMouseExit( GameObject inContainer, GameObject[] inHitObjects )
	{
		if( !inContainer.activeSelf )
			return;
		bool flag = false;
		for( int index = 0; index < inHitObjects.Length; ++index )
		{
			if( UIManager.instance.IsObjectAtMousePosition( inHitObjects[index] ) )
			{
				flag = true;
				break;
			}
		}
		if( flag )
			return;
		inContainer.SetActive( false );
	}

	public static void HandlePopup( ref bool inBoolState, GameObject inObject, Action inOnOpen, Action inOnClose )
	{
		if( UIManager.instance.blur.isActive )
			return;
		if( UIManager.instance.IsObjectAtMousePosition( inObject ) && !inBoolState )
		{
			inOnOpen();
			inBoolState = true;
		}
		else
		{
			if( UIManager.instance.IsObjectAtMousePosition( inObject ) || !inBoolState )
				return;
			inOnClose();
			inBoolState = false;
		}
	}

	public static void SetActiveIfInsideScreen( GameObject inObject )
	{
		Rect rect = new Rect( 0.0f, 0.0f, (float)Screen.width, (float)Screen.height );
		GameUtility.SetActive( inObject, rect.Contains( RectTransformUtility.WorldToScreenPoint( UIManager.instance.UICamera, inObject.transform.position ) ) );
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

	public static void EnableEmmission( ParticleSystem inSystem, bool inValue )
	{
		if( !( (UnityEngine.Object)inSystem != (UnityEngine.Object)null ) || inSystem.emission.enabled == inValue )
			return;
		inSystem.emission.enabled = inValue;
	}

	public static T MoveObjectToEndOfList<T>( ref List<T> inList, T inItem )
	{
		inList.Remove( inItem );
		inList.Add( inItem );
		return inItem;
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
			GameUtility.Assert( GameUtility.IsInMainThread );
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
			GameUtility.Assert( GameUtility.IsInMainThread );
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
			GameUtility.Assert( inStringBuilder != null );
			this.mStringBuilder = inStringBuilder;
		}

		public void Dispose()
		{
			GameUtility.GlobalStringBuilderPool.ReturnBuilderWrapper( this );
		}
	}
}
