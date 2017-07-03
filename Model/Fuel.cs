
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class Fuel : PerformanceImpact
{
	private Fuel.EngineMode mEngineMode = Fuel.EngineMode.Medium;
	private float mEngineModeFuelBurnRate = 1f;
	private float mDriverFuelBurnRate = 1f;
	private float mMechanicFuelBurnRate = 1f;
	private float mChassisFuelBurnRate = 1f;
	private float mScaledTimeCostPerLap = 0.3f;
	private float mCircuitLapDistance;
	private float mFuelLapDistance;
	private float mTargetFuelLapDistance;
	private float mFuelTankLapDistanceCapacity;
	private int mFuelTankLapCountCapacity;
	private float mEnginePerformance;
	private float mTotalFuelBurnt;

	public enum EngineMode
	{
		[LocalisationID( "PSG_10010825" )] SuperOvertake,
		[LocalisationID( "PSG_10010826" )] Overtake,
		[LocalisationID( "PSG_10010827" )] High,
		[LocalisationID( "PSG_10010828" )] Medium,
		[LocalisationID( "PSG_10010829" )] Low,
	}
}
