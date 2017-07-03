
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class TemperatureOptimisation : PerformanceImpact
{
	private static readonly float mMinDeltaChangeSlider = 0.05f;
	private static readonly float mMaxDeltaChangeSlider = 0.1f;
	private TemperatureOptimisation.Status mTyreStatus = TemperatureOptimisation.Status.Cold;
	private TemperatureOptimisation.Status mBrakeStatus = TemperatureOptimisation.Status.Cold;
	private float mOutLapDistance = 0.8f;
	private float mTimeSinceTargetUpdate = float.MaxValue;
	private float mTargetRefreshTime = 5f;
	private float mBrakeTempAIInfluence = 0.5f;
	private float mTyreTempAIInfluence = 0.8f;
	private TemperatureOptimisation.SweetSpot mTyreSweetSpot;
	private float mTyreTemperature;
	private float mTyreRateChange;
	private TemperatureOptimisation.SweetSpot mBrakeSweetSpot;
	private float mBrakeTemperature;
	private float mBrakeRateChange;
	private float mBrakingInfluence;
	private TemperatureOptimisation.Mode mMode;
	private float mPreviousDistance;
	private float mSpeed01;
	private bool mAutoManageSpeed;
	private float mTargetSpeed01;
	private float mSliderAutoRate;


	public enum Mode
	{
		InActive,
		Active,
		Complete,
	}

	public enum SweetSpot
	{
		Level1,
		Level2,
		Level3,
	}

	public enum Status
	{
		Overheated,
		Cold,
		Good,
		Great,
		Perfect,
	}
}
