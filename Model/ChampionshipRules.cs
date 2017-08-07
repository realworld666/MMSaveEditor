using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ChampionshipRules : Entity
{
    public string ruleSetName = string.Empty;
    public List<float> practiceDuration = new List<float>();
    public List<float> qualifyingDuration = new List<float>();
    public List<ChampionshipRules.SessionLength> raceLength = new List<ChampionshipRules.SessionLength>();
    public List<float> prizePoolPercentage = new List<float>();
    public DateTime carDevelopmenStartDate = new DateTime();
    public Dictionary<CarPart.PartType, int> partStatSeasonMinValue = new Dictionary<CarPart.PartType, int>();
    public Dictionary<CarPart.PartType, int> partStatSeasonMaxValue = new Dictionary<CarPart.PartType, int>();
    public string tyreSupplier = string.Empty;
    public int tyreSupplierID;
    public ChampionshipRules.TyreType tyreType;
    public int maxSlickTyresPerEvent = 15;
    public ChampionshipRules.CompoundChoice compoundChoice;
    public int wetWeatherTyreCount = 5;
    public int compoundsAvailable = 3;
    public float pitlaneSpeed;
    public float tyreSpeedBonus;
    public float tyreSupplierBonus;
    public ChampionshipRules.TyreWearRate tyreWearRate;
    public ChampionshipRules.EnergySystemBattery batterySize;
    public bool isEnergySystemActive;
    public bool isHybridModeActive;
    public bool isSprinklingSystemOn;
    public bool staffTransferWindowPreseason;
    public bool driverAidsOn;
    public bool isRefuelingOn;
    public float fuelLimitForRaceDistanceNormalized;
    public ChampionshipRules.SafetyCarUsage safetyCarUsage = ChampionshipRules.SafetyCarUsage.Both;
    public ChampionshipRules.GridSetup gridSetup;
    public ChampionshipRules.PitStopCrewSize pitCrewSize;
    public List<int> points = new List<int>();
    public bool finalRacePointsDouble;
    public int fastestLapPointBonus;
    public int polePositionPointBonus;
    public ChampionshipRules.MaxFinancialBudget maxFinancialBudget;
    public int maxDriverBudget;
    public int maxHQBudget;
    public int maxCarPartsBudget;
    public int maxNextYearCarBudget;
    public int maxNextYearDrivers;
    public int maxTravelBudget;
    public bool promotionBonus;
    public bool lastPlaceBonus;
    public SimulationSettings practiceSettings;
    public SimulationSettings qualifyingSettings;
    public SimulationSettings raceSettings;
    public List<CarPart.PartType> specParts = new List<CarPart.PartType>();
    private List<PoliticalVote> mRules = new List<PoliticalVote>();
    private Championship mChampionship;

    public enum MaxFinancialBudget
    {
        None,
        Low,
        Medium,
        High,
    }

    public enum TyreWearRate
    {
        Low,
        High,
    }

    public enum CompoundChoice
    {
        Free,
        Locked,
    }

    public enum TyreType
    {
        Normal,
        Wide,
        Grooved,
        Low,
        Road,
    }

    public enum SessionLength
    {
        Short,
        Medium,
        Long,
    }

    public enum SafetyCarUsage
    {
        RealSafetyCar,
        VirtualSafetyCar,
        Both,
    }

    public enum GridSetup
    {
        QualifyingBased,
        QualifyingBased3Sessions,
        Random,
        InvertedDriverChampionship,
    }

    public enum PitStopCrewSize
    {
        Small,
        Large,
    }

    public enum EnergySystemBattery
    {
        Small,
        Large,
    }
}
