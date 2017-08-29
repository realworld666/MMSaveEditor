using FullSerializer;
using System;
using System.Collections.Generic;
using UnityEngine;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class Championship : Entity
{
    public int championshipID = -1;
    public float championshipOrderRelative;
    public bool allowPromotions = true;
    public int championshipOrder;
    public int championshipAboveID = -1;
    public int championshipBelowID = -1;
    public int DlcID;
    public int logoID;
    public Color uiColor = new Color();
    public bool isChoosable;
    public bool isChoosableCreateTeam;
    public bool isBlockedByChallenge;
    public DateTime currentSeasonEndDate = DateTime.Today;
    public DateTime currentPreSeasonEndDate = DateTime.Today;
    public DateTime currentPreSeasonStartDate = DateTime.Today;
    public DateTime seasonEndDate = DateTime.Today;
    public DateTime preSeasonEndDate = DateTime.Today;
    public DateTime preSeasonStartDate = DateTime.Today;
    public List<RaceEventCalendarData> calendarData = new List<RaceEventCalendarData>();
    public List<RaceEventDetails> calendar = new List<RaceEventDetails>();
    public List<RaceEventDetails> nextYearsCalendar = new List<RaceEventDetails>();
    public int seasonStart;
    public int seasonEnd;
    public ChampionshipRecords records = new ChampionshipRecords();
    public ChampionshipStandings standings = new ChampionshipStandings();
    public ChampionshipStandingsHistory standingsHistory = new ChampionshipStandingsHistory();
    public PreSeasonTesting preSeasonTesting;
    public PoliticalSystem politicalSystem = new PoliticalSystem();
    public ChampionshipRules rules = new ChampionshipRules();
    public ChampionshipRules nextYearsRules = new ChampionshipRules();
    public int prizeFund;
    public int tvAudience;
    public int historySeed;
    public int historyMinStartAge = 19;
    public int historyMaxStartAge = 25;
    public float historyVariance = 0.35f;
    public float historyDNFChance = 0.03f;
    public int historyYears = 30;
    public string modelID = string.Empty;
    public int weightKG = 100;
    public int topSpeedMPH = 200;
    public string accelerationID = string.Empty;
    public string partManufacturingID = string.Empty;
    public int qualityTeamAverage;
    public int qualityCars;
    public int qualityDrivers;
    public int qualityHQ;
    public int qualityStaff;
    public int qualityFinances;
    public int eventLocations;
    public bool readyForPromotions;
    public bool completedPromotions;
    public Championship.Series series;
    private string mName = string.Empty;
    private string mAcronymn = string.Empty;
    private int mEventNumber;
    private string descriptionID = string.Empty;
    private string theChampionshipID = string.Empty;
    private string theChampionshipIDUpperCase = string.Empty;
    private string mCustomChampionshipName = string.Empty;
    private string mCustomAcronym = string.Empty;
    private string mCustomTheChampionship = string.Empty;
    private string mCustomTheChampionshipUppercase = string.Empty;
    private string mCustomDescription = string.Empty;
    private ChampionshipPromotions mChampionshipPromotions = new ChampionshipPromotions();
    public const float teamPromotionAcceptChance = 0.85f;
    public const float teamChampionMarketabilityReward = 25f;
    public const int invalidChampionshipID = -1;
    public SeasonDirector seasonDirector;

    private ChampionshipPromotionData inPromotedTeamFromLowerTier;
    private ChampionshipPromotionData inRelegatedTeamFromHigherTier;

    public string GetChampionshipName(bool getCachedVersion = false)
    {
        if (!string.IsNullOrEmpty(this.mCustomChampionshipName) && this.mCustomChampionshipName != "0")
            return this.mCustomChampionshipName;
        if (GameUtility.IsInMainThread)
        {
            return "Unknown";
        }
        return this.mName;
    }

    public int eventNumberForUI
    {
        get
        {
            return this.mEventNumber + 1;
        }
    }

    public int eventCount
    {
        get
        {
            return this.calendar.Count;
        }
    }

    public int eventNumber
    {
        get
        {
            return this.mEventNumber;
        }
    }

    public RaceEventDetails GetCurrentEventDetails()
    {
        return this.calendar[this.mEventNumber];
    }

    public enum Series
    {
        [LocalisationID("PSG_10011514")] SingleSeaterSeries,
        [LocalisationID("PSG_10011515")] GTSeries,
        Count,
    }
}
