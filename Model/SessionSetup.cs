using FullSerializer;
using System.Collections.Generic;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class SessionSetup
{
	public SessionSetupChange changes = new SessionSetupChange();
	private List<SessionSetupRepairPart> mRepairParts = new List<SessionSetupRepairPart>();
	private SessionSetup.SetupDetails mCurrentSetup = new SessionSetup.SetupDetails();
	private SessionSetup.SetupDetails mTargetSetup = new SessionSetup.SetupDetails();
	private SessionSetup.SetupOutput mAISetupOutput = new SessionSetup.SetupOutput();
	private SessionSetup.PitCrewSizeDependentSteps mCurrentLimitingStep = SessionSetup.PitCrewSizeDependentSteps.All;
	public const float rechargeStep = 0.25f;
	public const float rechargeRiskStep = 0.125f;
	private SessionSetup.State mState;
	private float mSetupTimer;
	private float mSetupDuration;
	private Driver mDriver;
	private RacingVehicle mVehicle;
	private SetupDesignData mSetupDesignData;
	private ChampionshipRules.PitStopCrewSize mPitCrewSize;
	private int mSavedFuelLevel;
	private int mTargetFuelLevel;
	private float mTargetBatteryCharge;
	private float mBatteryChargeRisk;
	private bool mIsBatteryGoingToBreak;


	[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
	public class SetupOutput
	{
		public float aerodynamics;
		public float speedBalance;
		public float handling;
	}

	public class SetupDetails
	{
		public SetupInput_v1 input = new SetupInput_v1();
		public TyreSet tyreSet;
		public SessionSetup.Trim trim;
	}

	public enum State
	{
		Setup,
		ChangingSetup,
	}

	public enum Trim
	{
		Qualifying,
		Race,
	}

	public enum SetupOpinion
	{
		None,
		VeryPoor,
		Poor,
		OK,
		Good,
		Great,
		Excellent,
	}

	public enum PitCrewSizeDependentSteps
	{
		Condition,
		Tyres,
		Fuel,
		Setup,
		Trim,
		Battery,
		All,
	}

	[fsObject( "v0", new System.Type[] { }, MemberSerialization = fsMemberSerialization.OptOut )]
	public class SetupInput
	{
		public float tyrePressure;
		public float tyreCamber;
		public float suspensionStiffness;
		public float gearRatio;
		public float frontWingAngle;
		public float rearWingAngle;
	}
}
