using FullSerializer;
using System.Collections.Generic;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class SessionSetupChange
{
	private static readonly Dictionary<SessionSetupChangeEntry.Target, float> outcomePerTargetBad = new Dictionary<SessionSetupChangeEntry.Target, float>()
  {
	{
	  SessionSetupChangeEntry.Target.Fuel,
	  0.5f
	},
	{
	  SessionSetupChangeEntry.Target.PartsRepair,
	  0.5f
	},
	{
	  SessionSetupChangeEntry.Target.Tyres,
	  0.8f
	},
	{
	  SessionSetupChangeEntry.Target.BatteryRecharge,
	  0.8f
	}
  };
	private static readonly Dictionary<SessionSetupChangeEntry.Target, float> outcomePerTargetGreat = new Dictionary<SessionSetupChangeEntry.Target, float>() { { SessionSetupChangeEntry.Target.Fuel, -0.5f }, { SessionSetupChangeEntry.Target.PartsRepair, -0.1f }, { SessionSetupChangeEntry.Target.Tyres, -0.3f }, { SessionSetupChangeEntry.Target.BatteryRecharge, 0.0f } };
	private static readonly Dictionary<SessionSetupChangeEntry.Target, float> errorTimePerTargetMin = new Dictionary<SessionSetupChangeEntry.Target, float>() { { SessionSetupChangeEntry.Target.Fuel, 4f }, { SessionSetupChangeEntry.Target.PartsRepair, 6f }, { SessionSetupChangeEntry.Target.Tyres, 6f }, { SessionSetupChangeEntry.Target.BatteryRecharge, 0.0f } };
	private static readonly Dictionary<SessionSetupChangeEntry.Target, float> errorTimePerTargetMax = new Dictionary<SessionSetupChangeEntry.Target, float>() { { SessionSetupChangeEntry.Target.Fuel, 8f }, { SessionSetupChangeEntry.Target.PartsRepair, 12f }, { SessionSetupChangeEntry.Target.Tyres, 12f }, { SessionSetupChangeEntry.Target.BatteryRecharge, 0.0f } };
	private static readonly Dictionary<SessionSetupChangeEntry.Target, float> timeVariationPerTargetMin = new Dictionary<SessionSetupChangeEntry.Target, float>() { { SessionSetupChangeEntry.Target.Fuel, -0.7f }, { SessionSetupChangeEntry.Target.PartsRepair, -0.8f }, { SessionSetupChangeEntry.Target.Tyres, -0.5f }, { SessionSetupChangeEntry.Target.BatteryRecharge, 0.0f } };
	private static readonly Dictionary<SessionSetupChangeEntry.Target, float> timeVariationPerTargetMax = new Dictionary<SessionSetupChangeEntry.Target, float>() { { SessionSetupChangeEntry.Target.Fuel, 0.5f }, { SessionSetupChangeEntry.Target.PartsRepair, 0.25f }, { SessionSetupChangeEntry.Target.Tyres, 0.5f }, { SessionSetupChangeEntry.Target.BatteryRecharge, 0.0f } };
	private List<SessionSetupChangeEntry> mEntries = new List<SessionSetupChangeEntry>();
	private List<SessionSetupChangeEntry.Target> mTargets = new List<SessionSetupChangeEntry.Target>();
	private List<SessionSetupChangeEntry.Target> mMistakeTargets = new List<SessionSetupChangeEntry.Target>();
	private Dictionary<SessionSetupChangeEntry.Target, SessionSetupChange.Outcome> mOutcomes = new Dictionary<SessionSetupChangeEntry.Target, SessionSetupChange.Outcome>( (IEqualityComparer<SessionSetupChangeEntry.Target>)new SessionSetupChangeEntry.TargetComparer() );
	private SessionSetupChangeEntry.Target mMistake = SessionSetupChangeEntry.Target.Count;
	public const float safeStrategyErrorPercent = 0.0f;
	public const float balancedStrategyErrorPercent = 0.1f;
	public const float fastStrategyErrorPercent = 0.25f;
	public float mistakeTimeCost;
	public bool queuedCarBehindTeamMate;
	private RacingVehicle mVehicle;
	private SessionSetup mSetup;
	private bool mCreateError;
	private float mErrorTime;
	private float mMechanicAddedPitStopTime;

	public enum Outcome
	{
		[LocalisationID( "PSG_10009318" )] None,
		[LocalisationID( "PSG_10010811" )] Bad,
		[LocalisationID( "PSG_10010209" )] Good,
		[LocalisationID( "PSG_10010180" )] Great,
	}
}
