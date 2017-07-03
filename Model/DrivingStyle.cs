
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class DrivingStyle : PerformanceImpact
{
	private DrivingStyle.Mode mMode = DrivingStyle.Mode.Neutral;
	private float mTyreWearRate = 1f;
	private float mDriverStylePerformance;
	private float mTimerSinceLastTemperatureSwitch;

	public enum Mode
	{
		[LocalisationID( "PSG_10010830" )] Attack,
		[LocalisationID( "PSG_10010831" )] Push,
		[LocalisationID( "PSG_10010832" )] Neutral,
		[LocalisationID( "PSG_10010833" )] Conserve,
		[LocalisationID( "PSG_10010834" )] BackUp,
	}
}
