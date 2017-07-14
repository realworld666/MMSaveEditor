using System;
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class Driver : Person
{
	public DriverCareerForm careerForm = new DriverCareerForm();
	public DriverMentalState mentalState = new DriverMentalState();
	public CarOpinion carOpinion = new CarOpinion();

	public PersonalityTraitController_v2 personalityTraitController;

	public int desiredChampionships;
	public long desiredBudget;
	private int mDesiredWins;
	private long mDesiredEarnings;
	private DriverStats accumulatedStats = new DriverStats();
	public DriverStats lastAccumulatedStats = new DriverStats();
	private bool mJoinsAnySeries = true;
	private DriverStats mStats = new DriverStats();
	private DriverStats mModifiedStats = new DriverStats();
	private float mImprovementRate;
	private DriverRivalries mDriverRivalries = new DriverRivalries();
	private int mDaysToScoutShort;
	private int mDaysToScoutLong;
	private DateTime lowMoraleStartTime = new DateTime();
	private DateTime mLastMoraleBonusDate = new DateTime();
	private readonly int moraleBonusCooldownDays = 30;
	private readonly float moralePromotionBonus = 0.4f;
	private readonly float moraleDemotionBonus = -0.4f;
	private readonly float moraleBetterContractBonus = 0.4f;
	private readonly float moraleWorseContractBonus = -0.4f;
	private readonly float moraleSignedContractBonus = 0.4f;
	private readonly float moraleFiredBonus = -0.4f;
	private readonly float moraleBetterContractPerRace = 0.025f;
	private readonly float moraleWorseContractPerRace = -0.025f;
	private readonly int lowMoraleOpenToOffersDays = 50;
	private readonly float lowMoraleOpenToOffersAmount = 0.1f;
	private readonly float lowMoraleStopListeningToOffersAmount = 0.3f;
	private readonly float moraleAchieveExpectedPositionBonus = 0.025f;
	private readonly float moraleFailedExpectedPositionBonus = -0.025f;
	private readonly float moraleSessionPodiumBonus = 0.03f;
	private readonly float moraleChampionshipPositionNormalModifier = 1f;
	private readonly float moraleKeptChampionshipExpectedPositionModifier = 0.1f;
	private readonly float moraleMinSessionChange = -0.2f;
	private readonly float moraleMaxSessionChange = 0.2f;
	private readonly float moralePracticeWeight = 0.05f;
	private readonly float moraleQualifyingWeight = 0.3f;
	private readonly float moraleRaceWeight = 0.65f;
	private readonly float moraleRacePerformanceWeight = 1f;
	private readonly float moraleGoalsWeight = 0.05f;
	private readonly float negativeImprovementHQScalar = 0.9f;
	private readonly float negativeImprovementHQOverallScalar = 0.03f;
	private readonly float negativeMaxImprovementHQ = 0.75f;
	public int driverNumber;
	public int startOfSeasonExpectedChampionshipPosition;
	public int expectedChampionshipPosition;
	public int expectedRacePosition;

	public DriverStats statsBeforeEvent;
	public float moraleBeforeEvent;
	public float championshipExpectation;
	public float raceExpectation;
	private Championship.Series mPreferedSeries;
	private float mPotential;
	private float mModifiedPotential;
	private bool mHasBeenScouted;
	private bool mIsReplacementDriver;
	private bool mHasCachedReplacementDriverInfo;
	private int mLastRaceExpectedRacePosition;
	private Person mCelebrity;

}
