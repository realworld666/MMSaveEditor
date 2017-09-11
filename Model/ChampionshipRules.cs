using FullSerializer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class ChampionshipRules : Entity
{
    public string ruleSetName = string.Empty;
    private List<float> practiceDuration = new List<float>();
    private List<float> qualifyingDuration = new List<float>();
    public List<SessionLength> raceLength = new List<ChampionshipRules.SessionLength>();
    private List<float> prizePoolPercentage = new List<float>();
    public DateTime carDevelopmenStartDate = new DateTime();
    private Dictionary<CarPart.PartType, int> partStatSeasonMinValue = new Dictionary<CarPart.PartType, int>();
    private Dictionary<CarPart.PartType, int> partStatSeasonMaxValue = new Dictionary<CarPart.PartType, int>();
    public string tyreSupplier = string.Empty;
    public int tyreSupplierID;
    public TyreType tyreType;
    private int maxSlickTyresPerEvent = 15;
    private ChampionshipRules.CompoundChoice compoundChoice;
    private int wetWeatherTyreCount = 5;
    private int compoundsAvailable = 3;
    private float pitlaneSpeed;
    private float tyreSpeedBonus;
    private float tyreSupplierBonus;
    private ChampionshipRules.TyreWearRate tyreWearRate;
    private ChampionshipRules.EnergySystemBattery batterySize;
    private bool isEnergySystemActive;
    private bool isHybridModeActive;
    private bool isSprinklingSystemOn;
    private bool staffTransferWindowPreseason;
    private bool driverAidsOn;
    private bool isRefuelingOn;
    private float fuelLimitForRaceDistanceNormalized;
    private ChampionshipRules.SafetyCarUsage safetyCarUsage = ChampionshipRules.SafetyCarUsage.Both;
    private ChampionshipRules.GridSetup gridSetup;
    private ChampionshipRules.PitStopCrewSize pitCrewSize;
    private List<int> points = new List<int>();
    private bool finalRacePointsDouble;
    private int fastestLapPointBonus;
    private int polePositionPointBonus;
    private ChampionshipRules.MaxFinancialBudget maxFinancialBudget;
    private int maxDriverBudget;
    private int maxHQBudget;
    private int maxCarPartsBudget;
    private int maxNextYearCarBudget;
    private int maxNextYearDrivers;
    private int maxTravelBudget;
    private bool promotionBonus;
    private bool lastPlaceBonus;
    public SimulationSettings practiceSettings;
    public SimulationSettings qualifyingSettings;
    public SimulationSettings raceSettings;
    private List<CarPart.PartType> specParts = new List<CarPart.PartType>();
    private ObservableCollection<PoliticalVote> mRules = new ObservableCollection<PoliticalVote>();
    private Championship mChampionship;
    public const float maxTyreSpeedBonus = 45f;
    private bool shouldChargeUsingStandingsPosition;

    public List<float> PracticeDuration
    {
        get
        {
            return practiceDuration;
        }

        set
        {
            practiceDuration = value;
        }
    }

    public List<float> QualifyingDuration
    {
        get
        {
            return qualifyingDuration;
        }

        set
        {
            qualifyingDuration = value;
        }
    }

    public List<float> PrizePoolPercentage
    {
        get
        {
            return prizePoolPercentage;
        }

        set
        {
            prizePoolPercentage = value;
        }
    }

    public Dictionary<CarPart.PartType, int> PartStatSeasonMinValue
    {
        get
        {
            return partStatSeasonMinValue;
        }

        set
        {
            partStatSeasonMinValue = value;
        }
    }

    public Dictionary<CarPart.PartType, int> PartStatSeasonMaxValue
    {
        get
        {
            return partStatSeasonMaxValue;
        }

        set
        {
            partStatSeasonMaxValue = value;
        }
    }

    [Range(5, 15)]
    public int MaxSlickTyresPerEvent
    {
        get
        {
            return maxSlickTyresPerEvent;
        }

        set
        {
            maxSlickTyresPerEvent = value;
        }
    }

    public CompoundChoice CompoundChoice1
    {
        get
        {
            return compoundChoice;
        }

        set
        {
            compoundChoice = value;
        }
    }

    public int CompoundsAvailable
    {
        get
        {
            return compoundsAvailable;
        }

        set
        {
            compoundsAvailable = value;
        }
    }

    public float PitlaneSpeed
    {
        get
        {
            return pitlaneSpeed;
        }

        set
        {
            pitlaneSpeed = value;
        }
    }

    public float TyreSpeedBonus
    {
        get
        {
            return tyreSpeedBonus;
        }

        set
        {
            tyreSpeedBonus = value;
        }
    }

    public float TyreSupplierBonus
    {
        get
        {
            return tyreSupplierBonus;
        }

        set
        {
            tyreSupplierBonus = value;
        }
    }

    public TyreWearRate TyreWearRate1
    {
        get
        {
            return tyreWearRate;
        }

        set
        {
            tyreWearRate = value;
        }
    }

    public EnergySystemBattery BatterySize
    {
        get
        {
            return batterySize;
        }

        set
        {
            batterySize = value;
        }
    }

    public bool IsEnergySystemActive
    {
        get
        {
            return isEnergySystemActive;
        }

        set
        {
            isEnergySystemActive = value;
        }
    }

    public bool IsHybridModeActive
    {
        get
        {
            return isHybridModeActive;
        }

        set
        {
            isHybridModeActive = value;
        }
    }

    public bool IsSprinklingSystemOn
    {
        get
        {
            return isSprinklingSystemOn;
        }

        set
        {
            isSprinklingSystemOn = value;
        }
    }

    public bool StaffTransferWindowPreseason
    {
        get
        {
            return staffTransferWindowPreseason;
        }

        set
        {
            staffTransferWindowPreseason = value;
        }
    }

    public bool DriverAidsOn
    {
        get
        {
            return driverAidsOn;
        }

        set
        {
            driverAidsOn = value;
        }
    }

    public bool IsRefuelingOn
    {
        get
        {
            return isRefuelingOn;
        }

        set
        {
            isRefuelingOn = value;
        }
    }

    public float FuelLimitForRaceDistanceNormalized
    {
        get
        {
            return fuelLimitForRaceDistanceNormalized;
        }

        set
        {
            fuelLimitForRaceDistanceNormalized = value;
        }
    }

    public SafetyCarUsage SafetyCarUsage1
    {
        get
        {
            return safetyCarUsage;
        }

        set
        {
            safetyCarUsage = value;
        }
    }

    public GridSetup GridSetup1
    {
        get
        {
            return gridSetup;
        }

        set
        {
            gridSetup = value;
        }
    }

    public PitStopCrewSize PitCrewSize
    {
        get
        {
            return pitCrewSize;
        }

        set
        {
            pitCrewSize = value;
        }
    }

    public List<int> Points
    {
        get
        {
            return points;
        }

        set
        {
            points = value;
        }
    }

    public bool FinalRacePointsDouble
    {
        get
        {
            return finalRacePointsDouble;
        }

        set
        {
            finalRacePointsDouble = value;
        }
    }

    public int FastestLapPointBonus
    {
        get
        {
            return fastestLapPointBonus;
        }

        set
        {
            fastestLapPointBonus = value;
        }
    }

    public int PolePositionPointBonus
    {
        get
        {
            return polePositionPointBonus;
        }

        set
        {
            polePositionPointBonus = value;
        }
    }

    public MaxFinancialBudget MaxFinancialBudget1
    {
        get
        {
            return maxFinancialBudget;
        }

        set
        {
            maxFinancialBudget = value;
        }
    }

    public int MaxDriverBudget
    {
        get
        {
            return maxDriverBudget;
        }

        set
        {
            maxDriverBudget = value;
        }
    }

    public int MaxHQBudget
    {
        get
        {
            return maxHQBudget;
        }

        set
        {
            maxHQBudget = value;
        }
    }

    public int MaxCarPartsBudget
    {
        get
        {
            return maxCarPartsBudget;
        }

        set
        {
            maxCarPartsBudget = value;
        }
    }

    public int MaxNextYearCarBudget
    {
        get
        {
            return maxNextYearCarBudget;
        }

        set
        {
            maxNextYearCarBudget = value;
        }
    }

    public int MaxNextYearDrivers
    {
        get
        {
            return maxNextYearDrivers;
        }

        set
        {
            maxNextYearDrivers = value;
        }
    }

    public int MaxTravelBudget
    {
        get
        {
            return maxTravelBudget;
        }

        set
        {
            maxTravelBudget = value;
        }
    }

    public bool PromotionBonus
    {
        get
        {
            return promotionBonus;
        }

        set
        {
            promotionBonus = value;
        }
    }

    public bool LastPlaceBonus
    {
        get
        {
            return lastPlaceBonus;
        }

        set
        {
            lastPlaceBonus = value;
        }
    }

    public List<CarPart.PartType> SpecParts
    {
        get
        {
            return specParts;
        }

        set
        {
            specParts = value;
        }
    }

    public bool ShouldChargeUsingStandingsPosition
    {
        get
        {
            return shouldChargeUsingStandingsPosition;
        }

        set
        {
            shouldChargeUsingStandingsPosition = value;
        }
    }

    public ObservableCollection<PoliticalVote> ActiveRules
    {
        get
        {
            return mRules;
        }

        set
        {
            mRules = value;
        }
    }

    [Browsable(false)]
    public Championship championship
    {
        get
        {
            return this.mChampionship;
        }
        set
        {
            this.mChampionship = value;
        }
    }

    public void AddRule(PoliticalVote inRule)
    {
        for (int index = 0; index < this.mRules.Count; ++index)
        {
            PoliticalVote mRule = this.mRules[index];
            if (mRule.group == inRule.group)
            {
                this.mRules.Remove(mRule);
                --index;
            }
        }
        this.mRules.Add(inRule);
    }

    public void RemoveRule(PoliticalVote inRule)
    {
        for (int index = 0; index < this.mRules.Count; ++index)
        {
            PoliticalVote mRule = this.mRules[index];
            if (mRule.ID == inRule.ID)
            {
                this.mRules.Remove(mRule);
                --index;
            }
        }
    }

    public void ActivateRules()
    {
        for (int index = 0; index < this.mRules.Count; ++index)
            this.mRules[index].ApplyImpacts(this);
    }

    public PoliticalVote GetVoteForGroup(string inGroup)
    {
        for (int index = 0; index < this.mRules.Count; ++index)
        {
            PoliticalVote mRule = this.mRules[index];
            if (mRule.group == inGroup)
                return mRule;
        }
        return (PoliticalVote)null;
    }

    public void ValidateChampionshipRules()
    {
        string[] strArray = new string[6]
        {
      "PromotionBonus",
      "LastPlaceBonus",
      "EnergyRecoverySystem",
      "HybridPower",
      "ChargeBasedOnStandings",
      "Sprinklers"
        };
        int[] numArray = new int[6] { 81, 75, 76, 80, 95, 72 };
        for (int index = 0; index < strArray.Length; ++index)
        {
            if (this.GetVoteForGroup(strArray[index]) == null)
            {
                int key = numArray[index];
                if (!VotesManager.Instance.votes.ContainsKey(key))
                {
                    Console.WriteLine("Championship {0} references invalid rule {1}; ignoring rule", new object[2]
                    {
            (object) this.mChampionship.championshipID,
            (object) key
                    });
                }
                else
                {
                    PoliticalVote inRule = VotesManager.Instance.votes[key];//.Clone();
                    inRule.Initialize(this.mChampionship);
                    this.AddRule(inRule);
                }
            }
        }
    }

    public bool qualifyingBasedActive
    {
        get
        {
            if (this.gridSetup != ChampionshipRules.GridSetup.QualifyingBased)
                return this.gridSetup == ChampionshipRules.GridSetup.QualifyingBased3Sessions;
            return true;
        }
    }

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

    public int GetQualifyingSessionCount()
    {
        return this.qualifyingBasedActive && this.gridSetup != ChampionshipRules.GridSetup.QualifyingBased ? 3 : 1;
    }

    public void ApplySimulationSettings()
    {
        if (this.practiceSettings != null)
            this.practiceSettings.Apply(this);
        if (this.qualifyingSettings != null)
            this.qualifyingSettings.Apply(this);
        if (this.raceSettings == null)
            return;
        this.raceSettings.Apply(this);
    }

    public void ApplySpecParts()
    {
        int teamEntryCount = this.championship.standings.teamEntryCount;
        this.championship.ResetPartTypeStatsProgression(this.specParts.ToArray());
        for (int inIndex = 0; inIndex < teamEntryCount; ++inIndex)
            this.ApplySpecPart(this.championship.standings.GetTeamEntry(inIndex).GetEntity<Team>());
    }

    public void ApplySpecPart(Team inTeam)
    {
        CarManager carManager = inTeam.carManager;
        carManager.UnfitAllParts(carManager.GetCar(0));
        carManager.UnfitAllParts(carManager.GetCar(1));
        CarPartInventory partInventory = inTeam.carManager.partInventory;
        for (int index = 0; index < this.specParts.Count; ++index)
        {
            partInventory.DestroyParts(this.specParts[index]);
            partInventory.AddPart(this.GetSpecPart(this.specParts[index], inTeam));
            partInventory.AddPart(this.GetSpecPart(this.specParts[index], inTeam));
        }
        carManager.AutoFit(carManager.GetCar(0), CarManager.AutofitOptions.Performance, CarManager.AutofitAvailabilityOption.UnfitedParts);
        carManager.AutoFit(carManager.GetCar(1), CarManager.AutofitOptions.Performance, CarManager.AutofitAvailabilityOption.UnfitedParts);
    }

    private CarPart GetSpecPart(CarPart.PartType inType, Team inTeam)
    {
        CarPart partEntity = CarPart.CreatePartEntity(inType, this.championship);
        partEntity.stats.SetStat(CarPartStats.CarPartStat.MainStat, (float)GameStatsConstants.specPartValues[this.championship.championshipID]);
        partEntity.stats.SetStat(CarPartStats.CarPartStat.Reliability, 0.8f);
        partEntity.stats.partCondition.SetCondition(0.8f);
        partEntity.stats.partCondition.redZone = GameStatsConstants.initialRedZone;
        partEntity.stats.maxReliability = 1f;
        partEntity.stats.maxPerformance = 0.0f;
        partEntity.buildDate = Game.instance.time.now;
        partEntity.stats.level = -1;
        partEntity.PostStatsSetup(this.championship);
        return partEntity;
    }

    public void GenerateDefaultParts(CarPart.PartType inPartType)
    {
        int teamEntryCount = this.championship.standings.teamEntryCount;
        for (int inIndex = 0; inIndex < teamEntryCount; ++inIndex)
            this.ApplyDefaultPart(this.championship.standings.GetTeamEntry(inIndex).GetEntity<Team>(), inPartType);
    }

    public void ApplyDefaultPart(Team inTeam, CarPart.PartType inPartType)
    {
        CarManager carManager = inTeam.carManager;
        carManager.UnfitAllParts(carManager.GetCar(0));
        carManager.UnfitAllParts(carManager.GetCar(1));
        CarPartInventory partInventory = inTeam.carManager.partInventory;
        partInventory.DestroyParts(inPartType);
        partInventory.AddPart(this.GetDefaultPart(inPartType, inTeam));
        partInventory.AddPart(this.GetDefaultPart(inPartType, inTeam));
        carManager.AutoFit(carManager.GetCar(0), CarManager.AutofitOptions.Performance, CarManager.AutofitAvailabilityOption.UnfitedParts);
        carManager.AutoFit(carManager.GetCar(1), CarManager.AutofitOptions.Performance, CarManager.AutofitAvailabilityOption.UnfitedParts);
    }

    private CarPart GetDefaultPart(CarPart.PartType inType, Team inTeam)
    {
        CarPart partEntity = CarPart.CreatePartEntity(inType, this.championship);
        partEntity.stats.SetStat(CarPartStats.CarPartStat.MainStat, (float)(GameStatsConstants.specPartValues[this.championship.championshipID] + RandomUtility.GetRandom(0, 10)));
        partEntity.stats.SetStat(CarPartStats.CarPartStat.Reliability, 0.8f);
        partEntity.stats.partCondition.SetCondition(0.8f);
        partEntity.stats.partCondition.redZone = GameStatsConstants.initialRedZone;
        partEntity.stats.maxReliability = 1f;
        partEntity.stats.maxPerformance = 0.0f;
        partEntity.buildDate = Game.instance.time.now;
        partEntity.stats.level = 0;
        partEntity.PostStatsSetup(this.championship);
        return partEntity;
    }
}
