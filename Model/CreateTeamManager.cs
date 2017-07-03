// Decompiled with JetBrains decompiler
// Type: CreateTeamManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using MM2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CreateTeamManager
{
  public static Dictionary<CreateTeamManager.Step, bool> mSteps = new Dictionary<CreateTeamManager.Step, bool>();
  private static Team mNewTeam = (Team) null;
  private static Team mSelectedTeam = (Team) null;
  private static TeamColor mTeamColor = (TeamColor) null;
  private static Championship mChampionship = (Championship) null;
  private static List<Team> mTeams = new List<Team>();
  public static Dictionary<int, List<DatabaseEntry>> driverData = new Dictionary<int, List<DatabaseEntry>>();
  public static List<CreateTeamManager.TeamDefaults> defaultTeamSettings = new List<CreateTeamManager.TeamDefaults>();
  public static List<Person> defaultPersons = new List<Person>();
  public static CreateTeamManager.TeamDefaults defaultSettings = (CreateTeamManager.TeamDefaults) null;
  public static string teamFirstName = string.Empty;
  public static string teamLastName = string.Empty;
  public static CreateTeamManager.State state = CreateTeamManager.State.Iddle;

  public static Team newTeam
  {
    get
    {
      return CreateTeamManager.mNewTeam;
    }
  }

  public static TeamColor newTeamColor
  {
    get
    {
      return CreateTeamManager.mTeamColor;
    }
  }

  public static bool isCreatingTeam
  {
    get
    {
      return CreateTeamManager.state == CreateTeamManager.State.CreatingTeam;
    }
  }

  public static Championship championship
  {
    get
    {
      return CreateTeamManager.mChampionship;
    }
  }

  public static void OnGameStart()
  {
    CreateTeamManager.mSteps.Clear();
    for (int index = 0; index < 4; ++index)
      CreateTeamManager.mSteps.Add((CreateTeamManager.Step) index, false);
    List<DatabaseEntry> teamDefaultsData = App.instance.database.createTeamDefaultsData;
    CreateTeamManager.defaultTeamSettings.Clear();
    CreateTeamManager.defaultPersons.Clear();
    CreateTeamManager.driverData.Clear();
    for (int index = 0; index < teamDefaultsData.Count; ++index)
    {
      DatabaseEntry inData = teamDefaultsData[index];
      int result = 0;
      if (int.TryParse(inData.GetStringValue("Championship ID"), out result))
      {
        CreateTeamManager.TeamDefaults teamDefaults = new CreateTeamManager.TeamDefaults();
        teamDefaults.championship = Game.instance.championshipManager.GetChampionshipByID(result);
        teamDefaults.defaultTeamNationality = Nationality.GetNationalityByName(inData.GetStringValue("Nationality"));
        teamDefaults.defaultLastName = inData.GetStringValue("Last Name");
        teamDefaults.defaultEngineSupplier = inData.GetIntValue("Engine Supplier");
        teamDefaults.defaultBrakesSupplier = inData.GetIntValue("Brakes Supplier");
        teamDefaults.defaultFuelSupplier = inData.GetIntValue("Fuel Supplier");
        teamDefaults.defaultMaterialsSupplier = inData.GetIntValue("Materials Supplier");
        teamDefaults.defaultBatterySupplier = inData.GetIntValue("Battery Supplier");
        teamDefaults.defaultPartStatBonusMin = inData.GetIntValue("Part Stat Bonus Min");
        teamDefaults.defaultPartStatBonusMax = inData.GetIntValue("Part Stat Bonus Max");
        teamDefaults.defaultPartMaxPerformanceMin = inData.GetIntValue("Part Max Performance Min");
        teamDefaults.defaultPartMaxPerformanceMax = inData.GetIntValue("Part Max Performance Max");
        teamDefaults.defaultPartReliabilityMin = (float) inData.GetIntValue("Part Reliability Min") / 100f;
        teamDefaults.defaultPartReliabilityMax = (float) inData.GetIntValue("Part Reliability Max") / 100f;
        teamDefaults.defaultPrimaryColor = GameUtility.HexStringToColour(inData.GetStringValue("Staff Primary Color"));
        teamDefaults.defaultSecondaryColor = GameUtility.HexStringToColour(inData.GetStringValue("Staff Secondary Color"));
        teamDefaults.defaultLiveryColor.primary = teamDefaults.defaultPrimaryColor;
        teamDefaults.defaultLiveryColor.secondary = teamDefaults.defaultSecondaryColor;
        teamDefaults.defaultLiveryColor.tertiary = GameUtility.HexStringToColour(inData.GetStringValue("Livery Tertiary Color"));
        teamDefaults.defaultLiveryColor.trim = GameUtility.HexStringToColour(inData.GetStringValue("Livery Trim Color"));
        teamDefaults.defaultLiveryColor.lightSponsor = GameUtility.HexStringToColour(inData.GetStringValue("Livery Light Sponsor Color"));
        teamDefaults.defaultLiveryColor.darkSponsor = GameUtility.HexStringToColour(inData.GetStringValue("Livery Dark Sponsor Color"));
        teamDefaults.defaultCarSponsors[0] = inData.GetIntValue("Car Sponsor 1 ID");
        teamDefaults.defaultCarSponsors[1] = inData.GetIntValue("Car Sponsor 2 ID");
        teamDefaults.defaultCarSponsors[2] = inData.GetIntValue("Car Sponsor 3 ID");
        teamDefaults.defaultCarSponsors[3] = inData.GetIntValue("Car Sponsor 4 ID");
        teamDefaults.defaultCarSponsors[4] = inData.GetIntValue("Car Sponsor 5 ID");
        teamDefaults.defaultCarSponsors[5] = inData.GetIntValue("Car Sponsor 6 ID");
        teamDefaults.defaultCarLiveryID = inData.GetIntValue("Car Livery ID");
        teamDefaults.defaultTeamLogoStyle = inData.GetIntValue("Team Logo Style");
        teamDefaults.defaultHatStyle = inData.GetIntValue("Hat Style");
        teamDefaults.defaultShirtStyle = inData.GetIntValue("Shirt Style");
        CreateTeamManager.defaultTeamSettings.Add(teamDefaults);
      }
      string stringValue = inData.GetStringValue("Person Type");
      if (!string.IsNullOrEmpty(stringValue) && stringValue != "0")
      {
        Person person = new Person();
        CreateTeamManager.defaultPersons.Add(person);
        person.gender = PersonManager<Person>.GetGender(inData.GetStringValue("Gender"));
        person.portrait = PersonManager<Person>.GetPortrait(inData);
      }
    }
    List<DatabaseEntry> createTeamDriversData = App.instance.database.createTeamDriversData;
    for (int index = 0; index < createTeamDriversData.Count; ++index)
    {
      DatabaseEntry databaseEntry = createTeamDriversData[index];
      int intValue = databaseEntry.GetIntValue("Championship ID");
      if (!CreateTeamManager.driverData.ContainsKey(intValue))
        CreateTeamManager.driverData.Add(intValue, new List<DatabaseEntry>());
      CreateTeamManager.driverData[intValue].Add(databaseEntry);
    }
  }

  public static void StartCreateNewTeam()
  {
    switch (CreateTeamManager.state)
    {
      case CreateTeamManager.State.Iddle:
        CreateTeamManager.state = CreateTeamManager.State.CreatingTeam;
        break;
      case CreateTeamManager.State.Hold:
        CreateTeamManager.state = CreateTeamManager.State.CreatingTeam;
        break;
    }
  }

  public static void SelectChampionship(Championship inChampionship)
  {
    if (inChampionship == null || CreateTeamManager.mChampionship == inChampionship || CreateTeamManager.state != CreateTeamManager.State.CreatingTeam)
      return;
    CreateTeamManager.mChampionship = inChampionship;
    CreateTeamManager.mTeams.Clear();
    int teamEntryCount = CreateTeamManager.mChampionship.standings.teamEntryCount;
    for (int inIndex = 0; inIndex < teamEntryCount; ++inIndex)
      CreateTeamManager.mTeams.Add(CreateTeamManager.mChampionship.standings.GetTeamEntry(inIndex).GetEntity<Team>());
    CreateTeamManager.mTeams.Sort((Comparison<Team>) ((x, y) => x.GetStarsStat().CompareTo(y.GetStarsStat())));
    CreateTeamManager.mTeams.Reverse();
    CreateTeamManager.CreateNewTeam(CreateTeamManager.mTeams[CreateTeamManager.mTeams.Count - RandomUtility.GetRandom(1, 3)]);
  }

  public static void CreateNewTeam(Team inTeam)
  {
    if (inTeam == null)
      return;
    CreateTeamManager.ResetSteps();
    CreateTeamManager.mSelectedTeam = inTeam;
    CreateTeamManager.mNewTeam = new Team();
    CreateTeamManager.mTeamColor = new TeamColor();
    CreateTeamManager.defaultSettings = CreateTeamManager.GetTeamDefaultsForChampionship(CreateTeamManager.mChampionship);
    CreateTeamManager.state = CreateTeamManager.State.CreatingTeam;
    string inLastName = Localisation.LocaliseID(CreateTeamManager.defaultSettings.defaultLastName, (GameObject) null);
    if (inLastName.Contains("String in database"))
      inLastName = string.Empty;
    CreateTeamManager.SetTeamName(Game.instance.player.firstName, inLastName);
    CreateTeamManager.SetTeamLogo(CreateTeamManager.defaultSettings.defaultTeamLogoStyle);
    CreateTeamManager.SetTeamNationality(CreateTeamManager.defaultSettings.defaultTeamNationality);
    TeamColor teamColor = CreateTeamManager.mSelectedTeam.GetTeamColor();
    CreateTeamManager.SetUIColors(teamColor.primaryUIColour, teamColor.secondaryUIColour);
    CreateTeamManager.SetLiveryColors(teamColor.carColor, CreateTeamManager.defaultSettings.defaultLiveryColor);
    CreateTeamManager.SetStaffColors(CreateTeamManager.defaultSettings.defaultPrimaryColor, CreateTeamManager.defaultSettings.defaultSecondaryColor);
    CreateTeamManager.SetHatStyle(CreateTeamManager.defaultSettings.defaultHatStyle);
    CreateTeamManager.SetBodyStyle(CreateTeamManager.defaultSettings.defaultShirtStyle);
    CreateTeamManager.mTeamColor.customLogoColor.primary = CreateTeamManager.mTeamColor.staffColor.primary;
    CreateTeamManager.mTeamColor.customLogoColor.secondary = CreateTeamManager.mTeamColor.staffColor.secondary;
    CreateTeamManager.mNewTeam.championship = CreateTeamManager.mSelectedTeam.championship;
    CreateTeamManager.mNewTeam.carManager.Start(CreateTeamManager.mNewTeam);
    CreateTeamManager.mNewTeam.carManager.partInventory = CreateTeamManager.mSelectedTeam.carManager.partInventory;
    CreateTeamManager.mNewTeam.liveryID = CreateTeamManager.defaultSettings.defaultCarLiveryID;
    CreateTeamManager.mTeamColor.liveryEditorOptions = teamColor.liveryEditorOptions;
    CreateTeamManager.mTeamColor.darkSponsorOptions = teamColor.darkSponsorOptions;
    CreateTeamManager.mTeamColor.lighSponsorOptions = teamColor.lighSponsorOptions;
  }

  public static void SetTeamName(string inFirstName, string inLastName)
  {
    using (GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe())
    {
      StringBuilder stringBuilder = builderSafe.stringBuilder;
      stringBuilder.Append(inFirstName);
      stringBuilder.Append(' ');
      stringBuilder.Append(inLastName);
      CreateTeamManager.mNewTeam.name = stringBuilder.ToString();
      CreateTeamManager.teamFirstName = inFirstName;
      CreateTeamManager.teamLastName = inLastName;
    }
  }

  public static void SetTeamNationality(Nationality inNationality)
  {
    CreateTeamManager.mNewTeam.nationality = inNationality;
  }

  public static void SetTeamLogo(int inPresetID)
  {
    CreateTeamManager.mNewTeam.customLogo.styleID = inPresetID;
  }

  public static void SetUIColors(TeamColor.UIColour inPrimaryUIColor, TeamColor.UIColour inSecondaryUIColor)
  {
    CreateTeamManager.mTeamColor.primaryUIColour = inPrimaryUIColor;
    CreateTeamManager.mTeamColor.secondaryUIColour = inSecondaryUIColor;
  }

  public static void SetLiveryColors(Color inCarColor, TeamColor.LiveryColour inLiveryColor)
  {
    CreateTeamManager.mTeamColor.carColor = inCarColor;
    CreateTeamManager.mTeamColor.livery = inLiveryColor;
  }

  public static void SetStaffColors(Color inPrimaryColor, Color inSecondaryColor)
  {
    CreateTeamManager.mTeamColor.staffColor.primary = inPrimaryColor;
    CreateTeamManager.mTeamColor.staffColor.secondary = inSecondaryColor;
    CreateTeamManager.mTeamColor.helmetColor.primary = inPrimaryColor;
    CreateTeamManager.mTeamColor.helmetColor.secondary = inSecondaryColor;
    CreateTeamManager.mTeamColor.helmetColor.tertiary = Color.white;
  }

  public static void SetHatStyle(int inHatStyle)
  {
    CreateTeamManager.mNewTeam.driversHatStyle = inHatStyle;
  }

  public static void SetBodyStyle(int inBodyStyle)
  {
    CreateTeamManager.mNewTeam.driversBodyStyle = inBodyStyle;
  }

  public static Team CompleteCreateNewTeam(Investor inInvestor)
  {
    CreateTeamManager.mSelectedTeam.name = CreateTeamManager.mNewTeam.name;
    TeamColor teamColor = CreateTeamManager.mSelectedTeam.GetTeamColor();
    teamColor.primaryUIColour = CreateTeamManager.mTeamColor.primaryUIColour;
    teamColor.secondaryUIColour = CreateTeamManager.mTeamColor.secondaryUIColour;
    teamColor.staffColor = CreateTeamManager.mTeamColor.staffColor;
    teamColor.carColor = CreateTeamManager.mTeamColor.staffColor.primary;
    teamColor.livery = CreateTeamManager.mTeamColor.livery;
    teamColor.helmetColor = CreateTeamManager.mTeamColor.helmetColor;
    teamColor.customLogoColor = CreateTeamManager.mTeamColor.customLogoColor;
    teamColor.primaryUIColour.normal = CreateTeamManager.mTeamColor.staffColor.primary;
    teamColor.secondaryUIColour.normal = CreateTeamManager.mTeamColor.staffColor.secondary;
    CreateTeamManager.mSelectedTeam.driversHatStyle = CreateTeamManager.mNewTeam.driversHatStyle;
    CreateTeamManager.mSelectedTeam.driversBodyStyle = CreateTeamManager.mNewTeam.driversBodyStyle;
    CreateTeamManager.mSelectedTeam.nationality = CreateTeamManager.mNewTeam.nationality;
    CreateTeamManager.mSelectedTeam.locationID = CreateTeamManager.mSelectedTeam.nationality.countryID;
    CreateTeamManager.mSelectedTeam.rivalTeam = CreateTeamManager.mNewTeam.rivalTeam;
    CreateTeamManager.mSelectedTeam.liveryID = CreateTeamManager.mNewTeam.liveryID;
    CreateTeamManager.mSelectedTeam.customLogo.styleID = CreateTeamManager.mNewTeam.customLogo.styleID;
    CreateTeamManager.mSelectedTeam.customLogo.teamFirstName = CreateTeamManager.teamFirstName;
    CreateTeamManager.mSelectedTeam.customLogo.teamLasttName = CreateTeamManager.teamLastName;
    CreateTeamManager.mSelectedTeam.investor = inInvestor;
    CreateTeamManager.mSelectedTeam.marketability = 0.0f;
    CreateTeamManager.mSelectedTeam.chairman.ResetHappiness();
    CreateTeamManager.mSelectedTeam.headquarters.nationality = CreateTeamManager.mSelectedTeam.nationality;
    CreateTeamManager.ResetTeam(CreateTeamManager.mSelectedTeam);
    CreateTeamManager.mSelectedTeam.investor.ApplyImpactsToTeam(CreateTeamManager.mSelectedTeam);
    CreateTeamManager.mSelectedTeam.isCreatedByPlayer = true;
    Game.instance.player.hasCreatedTeam = true;
    Game.instance.player.createdTeamID = CreateTeamManager.mSelectedTeam.teamID;
    App.instance.steamAchievementsManager.UnlockAchievement(Achievements.AchievementEnum.Create_A_Team);
    Team mSelectedTeam = CreateTeamManager.mSelectedTeam;
    CreateTeamManager.mNewTeam = (Team) null;
    CreateTeamManager.mSelectedTeam = (Team) null;
    CreateTeamManager.mTeamColor = (TeamColor) null;
    CreateTeamManager.state = CreateTeamManager.State.Iddle;
    return mSelectedTeam;
  }

  public static bool GetStep(CreateTeamManager.Step inStep)
  {
    if (CreateTeamManager.mSteps.ContainsKey(inStep))
      return CreateTeamManager.mSteps[inStep];
    return false;
  }

  public static void SetStep(CreateTeamManager.Step inStep, bool inValue)
  {
    if (!CreateTeamManager.mSteps.ContainsKey(inStep))
      return;
    CreateTeamManager.mSteps[inStep] = inValue;
  }

  public static void Reset()
  {
    if (CreateTeamManager.state != CreateTeamManager.State.CreatingTeam)
      return;
    CreateTeamManager.state = CreateTeamManager.State.Hold;
  }

  public static void ResetSteps()
  {
    for (int index = 0; index < 4; ++index)
      CreateTeamManager.mSteps[(CreateTeamManager.Step) index] = false;
  }

  private static void ResetTeam(Team inTeam)
  {
    CreateTeamManager.TeamDefaults defaultsForChampionship = CreateTeamManager.GetTeamDefaultsForChampionship(inTeam.championship);
    List<Driver> allPeopleOnJob = inTeam.contractManager.GetAllPeopleOnJob<Driver>(Contract.Job.Driver);
    int count1 = allPeopleOnJob.Count;
    for (int index = 0; index < count1; ++index)
    {
      Driver driver = allPeopleOnJob[index];
      inTeam.contractManager.FireDriver((Person) driver, Contract.ContractTerminationType.Generic);
      driver.careerHistory.RemoveHistory(driver.careerHistory.currentEntry);
    }
    int count2 = CreateTeamManager.driverData[inTeam.championship.championshipID].Count;
    for (int index = 0; index < count2; ++index)
    {
      DatabaseEntry databaseEntry = CreateTeamManager.driverData[inTeam.championship.championshipID][index];
      databaseEntry.AddEntry("Team", (object) (inTeam.teamID + 2));
      Driver database = Game.instance.driverManager.AddDriverToDatabase(databaseEntry);
      Game.instance.driverManager.LoadPersonalityTraits(database, databaseEntry);
    }
    List<Person> allEmployees = inTeam.contractManager.GetAllEmployees();
    int count3 = allEmployees.Count;
    for (int index = 0; index < count3; ++index)
    {
      Person inPersonToFire = allEmployees[index];
      if (inPersonToFire is Mechanic)
      {
        inTeam.contractManager.FirePerson(inPersonToFire, Contract.ContractTerminationType.Generic);
        inTeam.contractManager.HireReplacementMechanic();
        inPersonToFire.careerHistory.RemoveHistory(inPersonToFire.careerHistory.currentEntry);
      }
      else if (inPersonToFire is Engineer)
      {
        inTeam.contractManager.FirePerson(inPersonToFire, Contract.ContractTerminationType.Generic);
        inTeam.contractManager.HireReplacementEngineer();
        inPersonToFire.careerHistory.RemoveHistory(inPersonToFire.careerHistory.currentEntry);
      }
      else if (inPersonToFire is Assistant)
      {
        Assistant assistant = inPersonToFire as Assistant;
        inTeam.contractManager.FirePerson((Person) assistant, Contract.ContractTerminationType.Generic);
        assistant.Retire();
        inTeam.contractManager.HireReplacementTeamAssistant(inTeam.nationality);
      }
      else if (inPersonToFire is Scout)
      {
        Scout scout = inPersonToFire as Scout;
        inTeam.contractManager.FirePerson((Person) scout, Contract.ContractTerminationType.Generic);
        scout.Retire();
        inTeam.contractManager.HireReplacementScout(inTeam.nationality);
      }
    }
    inTeam.perksManager.Reset();
    inTeam.headquarters.ResetHeadquarters();
    inTeam.sponsorController.ClearAllSponsors();
    inTeam.sponsorController.ClearAllSponsorOffers();
    CarChassisStats carChassisStats = new CarChassisStats();
    carChassisStats.supplierEngine = Game.instance.supplierManager.GetSupplierByID(defaultsForChampionship.defaultEngineSupplier);
    carChassisStats.supplierBrakes = Game.instance.supplierManager.GetSupplierByID(defaultsForChampionship.defaultBrakesSupplier);
    carChassisStats.supplierFuel = Game.instance.supplierManager.GetSupplierByID(defaultsForChampionship.defaultFuelSupplier);
    carChassisStats.supplierMaterials = Game.instance.supplierManager.GetSupplierByID(defaultsForChampionship.defaultMaterialsSupplier);
    carChassisStats.supplierBattery = Game.instance.supplierManager.GetSupplierByID(defaultsForChampionship.defaultBatterySupplier);
    carChassisStats.supplierBattery.RollRandomBaseStatModifier();
    carChassisStats.ApplyChampionshipBaseStat(inTeam.championship);
    carChassisStats.ApplySupplierStats();
    for (int inIndex = 0; inIndex < CarManager.carCount; ++inIndex)
      inTeam.carManager.GetCar(inIndex).chassisStats = carChassisStats;
    CreateTeamManager.ResetTeamCarParts(inTeam);
    inTeam.financeController.availableFunds = inTeam.investor.startingMoney;
    inTeam.startOfSeasonExpectedChampionshipResult = Game.instance.teamManager.CalculateExpectedPositionForChampionship(inTeam);
    inTeam.championship.standings.UpdateStandings();
  }

  private static void ResetTeamCarParts(Team inTeam)
  {
    CarManager carManager = inTeam.carManager;
    CarPart.PartType[] partType = CarPart.GetPartType(inTeam.championship.series, false);
    CreateTeamManager.TeamDefaults defaultsForChampionship = CreateTeamManager.GetTeamDefaultsForChampionship(inTeam.championship);
    for (int index1 = 0; index1 < partType.Length; ++index1)
    {
      CarPart.PartType inPartType = partType[index1];
      if (!inTeam.championship.rules.specParts.Contains(inPartType))
      {
        PartTypeSlotSettings typeSlotSettings = Game.instance.partSettingsManager.championshipPartSettings[inTeam.championship.championshipID][inPartType];
        List<CarPart> partInventory = carManager.partInventory.GetPartInventory(inPartType);
        for (int index2 = 0; index2 < partInventory.Count; ++index2)
        {
          CarPart carPart = partInventory[index2];
          carPart.stats.SetStat(CarPartStats.CarPartStat.MainStat, (float) (typeSlotSettings.baseMinStat + RandomUtility.GetRandomInc(defaultsForChampionship.defaultPartStatBonusMin, defaultsForChampionship.defaultPartStatBonusMax)));
          carPart.stats.SetStat(CarPartStats.CarPartStat.Reliability, RandomUtility.GetRandom(defaultsForChampionship.defaultPartReliabilityMin, defaultsForChampionship.defaultPartReliabilityMax));
          carPart.stats.partCondition.redZone = GameStatsConstants.initialRedZone;
          carPart.stats.maxPerformance = (float) RandomUtility.GetRandomInc(defaultsForChampionship.defaultPartMaxPerformanceMin, defaultsForChampionship.defaultPartMaxPerformanceMax);
          carPart.stats.maxReliability = GameStatsConstants.initialMaxReliabilityValue;
          carPart.buildDate = Game.instance.time.now.AddDays(-1.0);
        }
      }
    }
    Car car1 = carManager.GetCar(0);
    Car car2 = carManager.GetCar(1);
    carManager.UnfitAllParts(car1);
    carManager.UnfitAllParts(car2);
    carManager.AutoFit(car1, CarManager.AutofitOptions.Performance, CarManager.AutofitAvailabilityOption.UnfitedParts);
    carManager.AutoFit(car2, CarManager.AutofitOptions.Performance, CarManager.AutofitAvailabilityOption.UnfitedParts);
    carManager.carPartDesign.SetSeasonStartingStats();
  }

  public static CreateTeamManager.TeamDefaults GetTeamDefaultsForChampionship(Championship inChampionship)
  {
    int count = CreateTeamManager.defaultTeamSettings.Count;
    for (int index = 0; index < count; ++index)
    {
      CreateTeamManager.TeamDefaults defaultTeamSetting = CreateTeamManager.defaultTeamSettings[index];
      if (defaultTeamSetting.championship == inChampionship)
        return defaultTeamSetting;
    }
    return (CreateTeamManager.TeamDefaults) null;
  }

  public enum State
  {
    Iddle,
    CreatingTeam,
    Hold,
  }

  public enum Step
  {
    PickTeamName,
    PickTeamLogo,
    PickTeamUniform,
    PickCarLivery,
    Count,
  }

  public class TeamDefaults
  {
    public string defaultLastName = string.Empty;
    public Color defaultPrimaryColor = Color.white;
    public Color defaultSecondaryColor = Color.white;
    public TeamColor.LiveryColour defaultLiveryColor = new TeamColor.LiveryColour();
    public int[] defaultCarSponsors = new int[6];
    public Championship championship;
    public int defaultEngineSupplier;
    public int defaultBrakesSupplier;
    public int defaultFuelSupplier;
    public int defaultMaterialsSupplier;
    public int defaultBatterySupplier;
    public int defaultPartStatBonusMin;
    public int defaultPartStatBonusMax;
    public int defaultPartMaxPerformanceMin;
    public int defaultPartMaxPerformanceMax;
    public float defaultPartReliabilityMin;
    public float defaultPartReliabilityMax;
    public int defaultTeamLogoStyle;
    public int defaultHatStyle;
    public int defaultShirtStyle;
    public int defaultCarLiveryID;
    public Nationality defaultTeamNationality;
  }
}
