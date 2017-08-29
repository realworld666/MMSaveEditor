using FullSerializer;
using MM2;
using System;
using System.Collections.Generic;
using System.Text;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class Chairman : Person
{
    private static readonly float happinessResetValue = 60f;

    public ChairmanUltimatum ultimatum = new ChairmanUltimatum();
    public int ultimatumsGeneratedThisSeason;
    public int costFocus;
    public int patience;
    public int patienceStrikesCount;
    public int playerChosenExpectedTeamChampionshipPosition;
    private float mHappiness;
    public int happinessBeforeEvent;
    private StatModificationHistory mHappinessModificationHistory = new StatModificationHistory();
    private float happinessPodiumBonus;
    private float happinessFixedChange;
    private float happinessMultiplier;
    private float happinessChampionshipPositionNormalMultiplier;
    private float happinessKeptExpectedChampionshipPositionMultiplier;
    private float maxRaceHappinessChangePerEvent;
    private float happinessUltimatumBoostSameManager;
    private float happinessUltimatumBoostNewManager;
    private float happinessFinancesFixedChange;
    private float happinessMinimumRequestFundsValueNormalized;
    private float happinessRequestFundsValue;
    private float[] happinessNegativeMultiplier;
    private float[] happinessPositiveMultiplier;
    private int[] ultimatumPositionTable;

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

    public void ResetHappiness()
    {
        this.mHappiness = Chairman.happinessResetValue;
    }
}
