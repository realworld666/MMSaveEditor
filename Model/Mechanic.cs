
using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class Mechanic : Person
{
	public MechanicStats stats = new MechanicStats();
	public MechanicStats lastAccumulatedStats = new MechanicStats();
	public float improvementRate;
	private Dictionary<string, Mechanic.DriverRelationship> mDictDriversRelationships = new Dictionary<string, Mechanic.DriverRelationship>();
	private Dictionary<string, StatModificationHistory> mDictRelationshipModificationHistory = new Dictionary<string, StatModificationHistory>();
	private readonly float weeklyRelationshipIncreaseRate = 2f;
	private readonly float weeklyRelationshipDecreaseRate = 2f;
	private readonly float endRaceRelationshipIncreaseRate = 15f;
	private readonly float endRaceRelationshipDecreaseRate = 15f;
	private readonly float positionRange = 5f;
	private readonly float negativeImprovementHQScalar = 0.9f;
	private readonly float negativeImprovementHQOverallScalar = 0.03f;
	private readonly float negativeMaxImprovementHQ = 0.75f;
	private readonly float maxMechanicRelationshipDecayPercent = 0.5f;
	private readonly float mechanicRelationshipInvalidDecay = -1f;
	public const float minPitStopAddedTime = 0.0f;
	public const float maxPitStopAddedTime = 2f;
	public const float minPitStopAddedError = 0.0f;
	public const float maxPitStopAddedError = 0.1f;
	public int driver;
	public MechanicBonus bonusOne;
	public MechanicBonus bonusTwo;
	private Map<string, Mechanic.DriverRelationship> mDriversRelationships;
	private Map<string, StatModificationHistory> mRelationshipModificationHistory;
	public float driverRelationshipAmountBeforeEvent;


	[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
	public class DriverRelationship
	{
		public float relationshipAmountAfterDecay = -1f;
		public float relationshipAmount;
		public int numberOfWeeks;


	}
}
