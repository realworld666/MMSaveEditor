
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class MechanicBonus
{
	public bool usefullForAnySession = true;
	public MechanicBonus.Trait trait;
	public int bonusID;
	public string traitName;
	public string nameLocalisationID;
	public string flavourText;
	public string textLocalisationID;
	public int icon;
	public int level;
	public string objectType;
	public string bonus;
	public int bonusUnlockAt;
	public SessionDetails.SessionType usefullForSpecificSession;

	public enum Trait
	{
		[LocalisationID( "PSG_10004091" )] EngineExpert = 0,
		[LocalisationID( "PSG_10004092" )] SweeterSpots = 1,
		[LocalisationID( "PSG_10004093" )] PitStopGuruTyreChanges = 2,
		[LocalisationID( "PSG_10004094" )] PitStopGuruRefuelling = 3,
		[LocalisationID( "PSG_10008695" )] LightfootedUltraSoftTyres = 6,
		[LocalisationID( "PSG_10004097" )] LightfootedSuperSoftTyres = 7,
		[LocalisationID( "PSG_10004098" )] LightfootedSoftTyres = 8,
		[LocalisationID( "PSG_10004099" )] LightfootedMediumTyres = 9,
		[LocalisationID( "PSG_10004100" )] LightfootedHardTyres = 10,
		[LocalisationID( "PSG_10004101" )] LightfootedIntermediateTyres = 11,
		[LocalisationID( "PSG_10004102" )] LightfootedWetTyres = 12,
		[LocalisationID( "PSG_10004103" )] FuelEconomy = 13,
		[LocalisationID( "PSG_10004104" )] SuperOvertakeMode = 14,
		[LocalisationID( "PSG_10004105" )] PitStopLegend = 15,
		[LocalisationID( "PSG_10004106" )] QuickFixes = 16,
		[LocalisationID( "PSG_10004107" )] Nurse = 17,
		[LocalisationID( "PSG_10004108" )] PushItToTheLimit = 18,
		[LocalisationID( "PSG_10004109" )] TheSweetestSpots = 19,
		[LocalisationID( "PSG_10004110" )] RiskTaker = 20,
		Count = 21,
	}
}
