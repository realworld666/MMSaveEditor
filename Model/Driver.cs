using System;
using FullSerializer;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight.Command;
using MMSaveEditor.ViewModel;
using GalaSoft.MvvmLight.Ioc;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class Driver : Person
{
    public DriverCareerForm careerForm = new DriverCareerForm();
    public DriverMentalState mentalState = new DriverMentalState();
    public CarOpinion carOpinion = new CarOpinion();

    public PersonalityTraitController_v2 personalityTraitController;

    private int desiredChampionships;
    private long desiredBudget;
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
    private int driverNumber;
    private int startOfSeasonExpectedChampionshipPosition;
    private int expectedChampionshipPosition;
    private int expectedRacePosition;

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

    public bool hasBeenScouted
    {
        get
        {
            return this.mHasBeenScouted;
        }
        set
        {
            mHasBeenScouted = value;
        }
    }

    public int daysToScoutShort
    {
        get
        {
            return this.mDaysToScoutShort;
        }
        set
        {
            mDaysToScoutShort = value;
        }
    }

    public int daysToScoutLong
    {
        get
        {
            return this.mDaysToScoutLong;
        }
        set
        {
            mDaysToScoutLong = value;
        }
    }

    public Championship.Series preferedSeries
    {
        get
        {
            return this.mPreferedSeries;
        }
        set
        {
            mPreferedSeries = value;
        }
    }
    public IEnumerable<Championship.Series> SeriesTypes
    {
        get
        {
            return Enum.GetValues(typeof(Championship.Series)).Cast<Championship.Series>();
        }
    }

    public bool joinsAnySeries
    {
        get
        {
            return this.mJoinsAnySeries;
        }
        set
        {
            mJoinsAnySeries = value;
        }
    }

    public int DesiredChampionships
    {
        get
        {
            return desiredChampionships;
        }

        set
        {
            desiredChampionships = value;
        }
    }

    public long DesiredBudget
    {
        get
        {
            return desiredBudget;
        }

        set
        {
            desiredBudget = value;
        }
    }

    public int MDesiredWins
    {
        get
        {
            return mDesiredWins;
        }

        set
        {
            mDesiredWins = value;
        }
    }

    public long MDesiredEarnings
    {
        get
        {
            return mDesiredEarnings;
        }

        set
        {
            mDesiredEarnings = value;
        }
    }

    public int DriverNumber
    {
        get
        {
            return driverNumber;
        }

        set
        {
            driverNumber = value;
        }
    }

    public int StartOfSeasonExpectedChampionshipPosition
    {
        get
        {
            return startOfSeasonExpectedChampionshipPosition;
        }

        set
        {
            startOfSeasonExpectedChampionshipPosition = value;
        }
    }

    public int ExpectedChampionshipPosition
    {
        get
        {
            return expectedChampionshipPosition;
        }

        set
        {
            expectedChampionshipPosition = value;
        }
    }

    public int ExpectedRacePosition
    {
        get
        {
            return expectedRacePosition;
        }

        set
        {
            expectedRacePosition = value;
        }
    }

    public float MPotential
    {
        get
        {
            return mPotential;
        }

        set
        {
            mPotential = value;
        }
    }

    public float MModifiedPotential
    {
        get
        {
            return mModifiedPotential;
        }

        set
        {
            mModifiedPotential = value;
        }
    }

    public DriverStats MStats
    {
        get
        {
            return mStats;
        }

        set
        {
            mStats = value;
        }
    }

    public RelayCommand<Driver> ViewDriver { get; private set; }

    public Driver()
    {
        ViewDriver = new RelayCommand<Driver>((s) => _viewDriver(s));
    }
    private void _viewDriver(Driver d)
    {
        var driverVM = SimpleIoc.Default.GetInstance<DriverViewModel>();
        driverVM.SetModel(this);
    }
}
