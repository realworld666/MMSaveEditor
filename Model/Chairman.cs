using FullSerializer;
using MM2;
using System;
using System.Collections.Generic;
using System.Text;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class Chairman : Person
{
    private static readonly float happinessPodiumBonus = 5f;
    private static readonly float happinessFixedChange = 5f;
    private static readonly float happinessMultiplier = 10f;
    private static readonly float happinessChampionshipPositionNormalMultiplier = 1f;
    private static readonly float happinessKeptExpectedChampionshipPositionMultiplier = 0.1f;
    private static readonly float maxRaceHappinessChangePerEvent = 100f;
    private static readonly float happinessUltimatumBoostSameManager = 35f;
    private static readonly float happinessUltimatumBoostNewManager = 50f;
    private static readonly float happinessFinancesFixedChange = 1f;
    private static readonly float happinessResetValue = 60f;
    private static readonly float happinessMinimumRequestFundsValueNormalized = 0.55f;
    private static readonly float happinessRequestFundsValue = -50f;
    private static readonly float ultimatumMaxPositionPercentage = 0.7f;
    private static readonly float[] happinessNegativeMultiplier = new float[3]
    {
        0.4f,
        0.5f,
        0.6f
    };
    private static readonly float[] happinessPositiveMultiplier = new float[3]
    {
        0.6f,
        0.5f,
        0.4f
    };

    public ChairmanUltimatum ultimatum = new ChairmanUltimatum();
    public int ultimatumsGeneratedThisSeason;
    public int costFocus;
    public int patience;
    public int patienceStrikesCount;
    public int playerChosenExpectedTeamChampionshipPosition;
    private float mHappiness;
    public int happinessBeforeEvent;

    private StatModificationHistory mHappinessModificationHistory = new StatModificationHistory();

    public enum EstimatedPosition
    {
        Low,
        Medium,
        High,
    }

    public enum RequestFundsAnswer
    {
        Accepted,
        DeclinedLowHappiness,
        DeclinedPreSeason,
        DeclinedSeasonStart,
        Declined,
    }

    public int UltimatumsGeneratedThisSeason
    {
        get { return ultimatumsGeneratedThisSeason; }
        set { ultimatumsGeneratedThisSeason = value; }
    }

    public int CostFocus
    {
        get { return costFocus; }
        set { costFocus = value; }
    }

    public int Patience
    {
        get { return patience; }
        set { patience = value; }
    }

    public int PlayerChosenExpectedTeamChampionshipPosition
    {
        get { return playerChosenExpectedTeamChampionshipPosition; }
        set { playerChosenExpectedTeamChampionshipPosition = value; }
    }

    public float Happiness
    {
        get { return mHappiness; }
        set { mHappiness = value; }
    }

    public void ResetHappiness()
    {
        this.mHappiness = Chairman.happinessResetValue;
    }
}
