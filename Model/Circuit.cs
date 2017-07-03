
using FullSerializer;
using System.Collections.Generic;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class Circuit
{
	public string locationName = string.Empty;
	public string locationNameID = string.Empty;
	public string countryNameID = string.Empty;
	public string spriteName = string.Empty;
	public string scene = string.Empty;
	public bool isInNorthHemisphere = true;
	public bool minimapAxisX = true;
	public KeyValuePair<Driver, float> fastestLapData = new KeyValuePair<Driver, float>();
	public TyreSet.Compound firstTyreOption = TyreSet.Compound.Soft;
	public TyreSet.Compound secondTyreOption = TyreSet.Compound.Soft;
	public TyreSet.Compound thirdTyreOption = TyreSet.Compound.Soft;
	public CarStats trackStatsCharacteristics = new CarStats();
	private string mNationalityKey = string.Empty;
	public int circuitID;
	public float trackLengthMiles;
	public float safetyCarFlagProbability;
	public float virtualSafetyCarProbability;
	public Circuit.TrackLayout trackLayout;
	public int minimapRotation;
	public long travelCost;
	public float bestPossibleLapTime;
	public float worstPossibleLapTime;
	public Circuit.Rate tyreWearRate;
	public Circuit.Rate fuelBurnRate;
	public int globeLocationID;
	public Climate climate;
	public DriverStatsProgression driverStats;


	public enum TrackLayout
	{
		[LocalisationID( "PSG_10008515" )] TrackA,
		[LocalisationID( "PSG_10008516" )] TrackB,
		[LocalisationID( "PSG_10008517" )] TrackC,
		[LocalisationID( "PSG_10008518" )] TrackD,
		[LocalisationID( "PSG_10008519" )] TrackE,
		[LocalisationID( "PSG_10010260" )] TrackF,
	}

	public enum Rate
	{
		[LocalisationID( "PSG_10001436" )] VeryLow,
		[LocalisationID( "PSG_10001437" )] Low,
		[LocalisationID( "PSG_10001438" )] Medium,
		[LocalisationID( "PSG_10001439" )] High,
		[LocalisationID( "PSG_10001440" )] VeryHigh,
	}
}
