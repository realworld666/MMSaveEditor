// Decompiled with JetBrains decompiler
// Type: StringVariableParser
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

public class StringVariableParser
{
  public static long minimumErsCost = 0;
  public static long budgetDelta = 0;
  public static string newSetupTimeString = string.Empty;
  public static bool playerTeamPromoted = false;
  public static bool playerTeamRelegated = false;
  public static Team promotedTeam = (Team) null;
  public static CarPart partWithComponent = (CarPart) null;
  public static CarPartComponent component = (CarPartComponent) null;
  public static CarPart partForUI = (CarPart) null;
  public static CarPart.PartType weakestPart = CarPart.PartType.None;
  public static CarPart.PartType partFrontendUI = CarPart.PartType.None;
  public static int projectedPartDesignDays = 0;
  public static CarPartStats.CarPartStat partStat = CarPartStats.CarPartStat.Condition;
  public static CarPart partWithRandomComponent = (CarPart) null;
  public static CarPartComponent randomComponent = (CarPartComponent) null;
  public static MediaOutlet mediaOutlet = (MediaOutlet) null;
  public static Person personReplaced = (Person) null;
  private static Person mSubject = (Person) null;
  public static Person interviewSubject = (Person) null;
  public static Team subjectPreviousTeam = (Team) null;
  public static Person sender = (Person) null;
  public static Driver anyDriver = (Driver) null;
  public static Driver selectedDriver = (Driver) null;
  public static Driver otherDriver = (Driver) null;
  public static CarPart partBanned = (CarPart) null;
  public static CarPart.PartType levelGapPartType = CarPart.PartType.None;
  public static CarPart.PartType reliabilityPartType = CarPart.PartType.None;
  public static Driver rumourDriver = (Driver) null;
  public static Team rumourTeam = (Team) null;
  public static Person randomJournalist = (Person) null;
  public static Driver randomDriver = (Driver) null;
  public static Driver randomSessionDriver = (Driver) null;
  public static Driver randomPlayerDriver = (Driver) null;
  public static Driver randomPlayerSessionDriver = (Driver) null;
  public static Person randomFan = (Person) null;
  public static Person exDriver = (Person) null;
  public static Person exEngineer = (Person) null;
  public static Person exTeamPrincipal = (Person) null;
  public static Person randomFemale = (Person) null;
  public static Person exMechanic = (Person) null;
  public static Person technicalExpert = (Person) null;
  public static Person randomNonPlayerTeamChairman = (Person) null;
  public static Team teamAppliedTo = (Team) null;
  public static Team randomTeam = (Team) null;
  public static Team randomSessionTeam = (Team) null;
  public static Championship randomChampionship = (Championship) null;
  public static Circuit randomCircuit = (Circuit) null;
  public static int sessionLenghtMinutes = 0;
  public static float raceLengthMiles = 0.0f;
  public static int raceLengthAverageLaps = 0;
  public static Circuit newTrack = (Circuit) null;
  public static string newTrackLayout = (string) null;
  public static Circuit oldTrackTrack = (Circuit) null;
  public static string oldTrackLayout = (string) null;
  public static string oldTrackWeekNumber = (string) null;
  public static float fuelLimit = 0.0f;
  public static Driver dilemmaBirthdayDriver = (Driver) null;
  public static Mechanic dilemmaRandomMechanic = (Mechanic) null;
  private static Person mDilemmaSubject = (Person) null;
  public static string dilemmaBonus = (string) null;
  public static Team dilemmaTeam = (Team) null;
  public static int dilemmaTransaction = 0;
  public static Sponsor sponsor = (Sponsor) null;
  public static ContractSponsor contractSponsor = (ContractSponsor) null;
  public static bool jobAccepted = false;
  public static HQsBuilding_v1 building = (HQsBuilding_v1) null;
  public static HQsBuildingInfo buildingInfo = (HQsBuildingInfo) null;
  public static string nationalityGender = string.Empty;
  public static Person impactVictim = (Person) null;
  public static Person overtakeVictim = (Person) null;
  public static Person attackingDriver = (Person) null;
  public static Person teamOrderVictim = (Person) null;
  public static int purpleSectorNumber = 0;
  public static int greenSectorNumber = 0;
  public static float fastestPitstopTime = 0.0f;
  public static float purpleSectorTime = 0.0f;
  public static float greenSectorTime = 0.0f;
  public static int playerDriversDropedPositionCount = 0;
  public static int knowledgeLevel = 0;
  public static float knowledgeAmount = 0.0f;
  public static bool useLinkData = false;
  public static Penalty.PenaltyCause penaltyCause = Penalty.PenaltyCause.Count;
  public static int penaltyTimeSeconds = 0;
  public static CarPart carPart = (CarPart) null;
  public static PoliticalVote vote = (PoliticalVote) null;
  public static TyreSet.Compound practiceKnowledgeCompound = TyreSet.Compound.Soft;
  public static int supplierOriginalPrice = 0;
  public static float supplierDiscountPercent = 0.0f;
  public static Supplier supplier = (Supplier) null;
  public static Driver mechanicRelationshipDriver = (Driver) null;
  public static int mechanicRelationshipWeeks = 0;
  public static Driver personalityTraitDriver = (Driver) null;
  public static string personalityTraitName = (string) null;
  public static Driver fightWithDriverTrait = (Driver) null;
  public static Circuit traitBigCrashLocation = (Circuit) null;
  public static bool includeSeconds = false;
  public static TeamFinanceController.NextYearCarInvestement nextYearCarInvestment = TeamFinanceController.NextYearCarInvestement.Low;
  public static Challenge.ChallengeName challengeName = Challenge.ChallengeName.Underdog;
  public static string contractNegotiationYear = (string) null;
  public static string contractNegotiationMonths = (string) null;
  public static string contractNegotiationPersonLastName = (string) null;
  public static long longValue = 0;
  public static int intValue1 = 0;
  public static int intValue2 = 0;
  public static int intValue3 = 0;
  public static float floatValue1 = 0.0f;
  public static string stringValue1 = string.Empty;
  public static string sessionTime = string.Empty;
  public static Contract.Job contractJob = Contract.Job.IMAPresident;
  public static Transaction.Group financialTransaction = Transaction.Group.Count;
  public static SessionDetails.SessionType sessionType = SessionDetails.SessionType.Practice;
  public static PoliticalSystem.VoteResult voteResult = PoliticalSystem.VoteResult.Tie;
  public static string workshopErrorType = string.Empty;
  public static string workshopCreatorName = string.Empty;
  public static string modName = string.Empty;
  public static ulong modID = 0;
  public static string modStatus = string.Empty;
  public static string duplicateDatabaseType = string.Empty;
  private static List<string> mAllGenderVariations = new List<string>();
  private static List<string> mPossibleResults = new List<string>();
  private static List<string> mPossibleResultsGenderMatch = new List<string>();
  private static List<int> mPossibleResultsPeopleCount = new List<int>();
  private static FieldInfo[] mFields = (FieldInfo[]) null;
  public static int month;
  public static string ordinalNumberString;

  public static Person dilemmaSubject
  {
    get
    {
      return StringVariableParser.mDilemmaSubject;
    }
    set
    {
      StringVariableParser.mDilemmaSubject = value;
    }
  }

  public static string currentEventTitle
  {
    get
    {
      return Localisation.LocaliseID("PSG_10010582", (GameObject) null);
    }
  }

  public static Person subject
  {
    get
    {
      return StringVariableParser.mSubject;
    }
    set
    {
      StringVariableParser.mSubject = value;
    }
  }

  public static void OnStart()
  {
    if (StringVariableParser.mFields != null)
      return;
    StringVariableParser.mFields = typeof (StringVariableParser).GetFields();
  }

  public static void RandomizeEntities(Person inSender = null)
  {
    MediaManager mediaManager = Game.instance.mediaManager;
    StringVariableParser.randomJournalist = mediaManager.GetEntity(RandomUtility.GetRandom(0, mediaManager.count));
    StringVariableParser.exDriver = (Person) Game.instance.celebrityManager.GetCelebrityByJob("ExDriver");
    StringVariableParser.exEngineer = (Person) Game.instance.celebrityManager.GetCelebrityByJob("ExEngineer");
    StringVariableParser.exMechanic = (Person) Game.instance.celebrityManager.GetCelebrityByJob("ExMechanic");
    StringVariableParser.technicalExpert = (Person) Game.instance.celebrityManager.GetCelebrityByJob("TechnicalExpert");
    StringVariableParser.randomFan = App.instance.regenManager.CreateFan(RegenManager.RegenType.Poor, Person.Gender.Male, false);
    StringVariableParser.exTeamPrincipal = (Person) Game.instance.celebrityManager.GetCelebrityByJob("ExTeamPrincipal");
    StringVariableParser.randomFemale = App.instance.regenManager.CreateFan(RegenManager.RegenType.Poor, Person.Gender.Female, true);
    Championship championship = !Game.instance.player.IsUnemployed() ? Game.instance.player.team.championship : Game.instance.championshipManager.GetMainChampionship(Championship.Series.SingleSeaterSeries);
    StringVariableParser.randomDriver = championship.standings.GetNonPlayerRandomDriverEntry().GetEntity<Driver>();
    StringVariableParser.randomTeam = championship.standings.GetNonPlayerRandomTeamEntry().GetEntity<Team>();
    StringVariableParser.randomChampionship = Game.instance.championshipManager.GetRandomChampionship(Championship.Series.SingleSeaterSeries);
    StringVariableParser.randomNonPlayerTeamChairman = StringVariableParser.randomTeam.contractManager.GetPersonOnJob(Contract.Job.Chairman);
    List<RacingVehicle> racingVehicleList = new List<RacingVehicle>((IEnumerable<RacingVehicle>) Game.instance.sessionManager.standings);
    for (int index = 0; index < racingVehicleList.Count; ++index)
    {
      if (racingVehicleList[index].driver.contract.GetTeam() == Game.instance.player.team)
      {
        racingVehicleList.RemoveAt(index);
        --index;
      }
    }
    if (racingVehicleList.Count > 0)
    {
      StringVariableParser.randomSessionDriver = racingVehicleList[RandomUtility.GetRandom(0, racingVehicleList.Count)].driver;
      StringVariableParser.randomSessionTeam = racingVehicleList[RandomUtility.GetRandom(0, racingVehicleList.Count)].driver.contract.GetTeam();
    }
    StringVariableParser.randomPlayerSessionDriver = Game.instance.player.team.GetSelectedDriver(RandomUtility.GetRandomInc(0, 1));
    StringVariableParser.randomPlayerDriver = Game.instance.player.team.GetDriver(RandomUtility.GetRandomInc(0, 1));
    if (inSender == null)
      inSender = (Person) StringVariableParser.randomPlayerDriver;
    if (StringVariableParser.anyDriver == null && inSender is Driver && inSender.contract.GetTeam() == Game.instance.player.team)
      StringVariableParser.anyDriver = (Driver) inSender;
    if (StringVariableParser.anyDriver == Game.instance.player.team.GetDriver(0))
      StringVariableParser.otherDriver = Game.instance.player.team.GetDriver(1);
    else if (StringVariableParser.anyDriver == Game.instance.player.team.GetDriver(1))
      StringVariableParser.otherDriver = Game.instance.player.team.GetDriver(0);
    else
      StringVariableParser.otherDriver = Game.instance.player.team.GetDriver(RandomUtility.GetRandomInc(0, 1));
  }

  public static void ResetAllStaticReferences()
  {
    if (StringVariableParser.mFields == null)
      StringVariableParser.OnStart();
    StringVariableParser.mAllGenderVariations.Clear();
    StringVariableParser.mPossibleResults.Clear();
    StringVariableParser.mPossibleResultsGenderMatch.Clear();
    StringVariableParser.mPossibleResultsPeopleCount.Clear();
    for (int index = 0; index < StringVariableParser.mFields.Length; ++index)
      typeof (StringVariableParser).GetField(StringVariableParser.mFields[index].Name).SetValue((object) null, (object) null);
  }

  public static void SetStaticData(Person inSender)
  {
    StringVariableParser.RandomizeEntities(inSender);
    if (inSender == null)
      return;
    StringVariableParser.mediaOutlet = inSender.contract.GetMediaOutlet();
    StringVariableParser.sender = inSender;
  }

  public static string GetGenderText(string inID, string inString, GameObject inObject = null, string inGender = "")
  {
    return StringVariableParser.GetGenderText(inID, inString, Localisation.currentLanguage, inObject, inGender);
  }

  public static string GetGenderText(string inID, string inString, string inLanguage, GameObject inObject = null, string inGender = "")
  {
    if (!inString.Contains("</gender>"))
      return inString;
    StringVariableParser.mAllGenderVariations.Clear();
    int startIndex = 0;
    for (int inIndex = 0; inIndex < inString.Length; ++inIndex)
    {
      if (StringVariableParser.CheckForClosingTag(inIndex, inString))
      {
        if (inIndex - startIndex - 9 < 0)
          return "Error: index value cannot be negative.";
        StringVariableParser.mAllGenderVariations.Add(inString.Substring(startIndex, inIndex - startIndex - 9));
        startIndex = inIndex;
      }
    }
    string str1 = string.Empty;
    StringVariableParser.mPossibleResults.Clear();
    StringVariableParser.mPossibleResultsGenderMatch.Clear();
    StringVariableParser.mPossibleResultsPeopleCount.Clear();
    int count1 = StringVariableParser.mAllGenderVariations.Count;
    for (int index1 = 0; index1 < count1; ++index1)
    {
      string allGenderVariation = StringVariableParser.mAllGenderVariations[index1];
      string str2 = string.Empty;
      string str3 = string.Empty;
      for (int index2 = 0; index2 < allGenderVariation.Length; ++index2)
      {
        if ((int) allGenderVariation[index2] == 94)
        {
          int index3 = index2;
          while ((int) allGenderVariation[index3] != 62 && index3 < allGenderVariation.Length)
            ++index3;
          if (index3 < allGenderVariation.Length)
          {
            str2 = allGenderVariation.Substring(index2 + 1, index3 - (index2 + 1));
            str3 = allGenderVariation.Substring(index3 + 1, allGenderVariation.Length - (index3 + 1));
            break;
          }
          break;
        }
      }
      if (!(str3 == string.Empty) && !(str2 == string.Empty))
      {
        string[] strArray1 = str2.Split(',');
        string empty = string.Empty;
        bool flag = true;
        for (int index2 = 0; index2 < strArray1.Length; ++index2)
        {
          strArray1[index2] = strArray1[index2].Trim();
          string[] strArray2 = strArray1[index2].Split('=');
          if (strArray2.Length < 2)
            return "Error retrieving gender string";
          string inGenderTarget = strArray2[0].Trim();
          string inGender1 = strArray2[1].Trim();
          empty += inGender1;
          if (!StringVariableParser.CheckPersonGender(inGenderTarget, inGender1, inLanguage, inObject, inGender))
            flag = false;
        }
        if (flag)
        {
          StringVariableParser.mPossibleResults.Add(str3);
          StringVariableParser.mPossibleResultsGenderMatch.Add(empty);
          StringVariableParser.mPossibleResultsPeopleCount.Add(strArray1.Length);
        }
        else
          str1 = str3.Trim();
      }
      else
        break;
    }
    int index4 = 0;
    int num = 0;
    int count2 = StringVariableParser.mPossibleResultsPeopleCount.Count;
    for (int index1 = 0; index1 < count2; ++index1)
    {
      if (StringVariableParser.mPossibleResultsPeopleCount[index1] > num)
      {
        num = StringVariableParser.mPossibleResultsPeopleCount[index1];
        index4 = index1;
      }
      else
      {
        num = StringVariableParser.mPossibleResultsPeopleCount[index1];
        index4 = index1;
      }
    }
    if (index4 <= count2 - 1)
      return StringVariableParser.mPossibleResults[index4].Trim();
    if (StringVariableParser.mPossibleResults.Count > 0)
      return StringVariableParser.mPossibleResults[0].Trim();
    return str1;
  }

  private static bool CheckPersonGender(string inGenderTarget, string inGender, string inLanguage, GameObject inObject, string inGenderOverride = "")
  {
    try
    {
      Person.Gender gender = !(inGender == "M") ? Person.Gender.Female : Person.Gender.Male;
      if (inGenderOverride != string.Empty)
        return inGenderOverride == "Male" && gender == Person.Gender.Male || inGenderOverride == "Female" && gender == Person.Gender.Female;
      object inObject1 = StringVariableParser.GetObject(inGenderTarget);
      if (inObject1 is Person)
        return ((Person) inObject1).gender == gender;
      string empty = string.Empty;
      string str = inObject1 != null ? StringVariableParser.GetWordGender(inObject1, inGenderTarget, inLanguage) : StringVariableParser.GetWordGender(inGenderTarget, inLanguage);
      if (str != string.Empty)
        return str == inGender || str == gender.ToString();
    }
    catch (Exception ex)
    {
      Debug.LogError((object) string.Format("Error with string: {0} \n {1}", (object) inGenderTarget, (object) ex.Message), (UnityEngine.Object) null);
    }
    if (!Application.isPlaying)
      Debug.LogWarningFormat((UnityEngine.Object) inObject, "Object uses a gender sensitive string, check if it needs a target for {0}, gameObject :{1}{2}", (object) inGenderTarget, (object) inObject.name, (object) inObject.GetInstanceID());
    return false;
  }

  private static void GetHashSplitVariables(string inPerson, out int index, out string person)
  {
    int num = 0;
    int result1 = 0;
    int result2 = 0;
    bool flag1 = int.TryParse(inPerson[1].ToString(), out result1);
    bool flag2 = int.TryParse(inPerson[2].ToString(), out result2);
    if (flag1 && flag2)
    {
      inPerson = inPerson.Remove(0, 3);
      num = int.Parse(result1.ToString() + result2.ToString());
    }
    else if (flag1)
    {
      inPerson = inPerson.Remove(0, 2);
      num = result1;
    }
    if (num > 0)
      --num;
    index = num;
    person = inPerson;
  }

  private static bool CheckForClosingTag(int inIndex, string inString)
  {
    if (inIndex - 8 < 0 || inIndex + 1 > inString.Length)
      return false;
    return inString.Substring(inIndex - 8, 9) == "</gender>";
  }

  public static void GetData(string inString, string inLanguage, ref StringBuilder string_builder)
  {
    if (inString == string.Empty)
      return;
    try
    {
      string[] strArray1 = inString.Split(':');
      string inString1 = strArray1[0];
      string inData = "No Special Data";
      if (strArray1.Length > 1)
        inData = strArray1[1];
      object obj = StringVariableParser.GetObject(inString1);
      if (obj is Person)
      {
        StringVariableParser.GetPersonSpecialData((Person) obj, inData, inLanguage, ref string_builder);
        return;
      }
      if (obj is Team)
      {
        StringVariableParser.GetTeamSpecialData((Team) obj, inData, inLanguage, ref string_builder);
        return;
      }
      if (obj is Sponsor)
      {
        StringVariableParser.GetSponsorSpecialData((Sponsor) obj, inData, inLanguage, ref string_builder);
        return;
      }
      if (obj is HQsBuilding_v1)
      {
        StringVariableParser.GetBuildingSpecialData((HQsBuilding_v1) obj, inData, inLanguage, ref string_builder);
        return;
      }
      if (obj is Circuit)
      {
        StringVariableParser.GetCircuitSpecialData((Circuit) obj, inData, inLanguage, ref string_builder);
        return;
      }
      if (obj is CarPart)
      {
        StringVariableParser.GetCarPartSpecialData((CarPart) obj, inData, inLanguage, ref string_builder);
        return;
      }
      if (obj is Championship)
      {
        StringVariableParser.GetChampionshipSpecialData((Championship) obj, inData, inLanguage, ref string_builder);
        return;
      }
      inString = inString.Trim('{', '}', ' ');
      if ((int) inString[0] == 35)
      {
        int index = 0;
        StringVariableParser.GetHashSplitVariables(inString, out index, out inString);
        string[] strArray2 = inString.Split(':');
        string str1 = strArray2[0];
        string str2 = "No Special Data";
        if (strArray2.Length > 1)
          str2 = strArray2[1];
        string key = str1;
        if (key != null)
        {
          // ISSUE: reference to a compiler-generated field
          if (StringVariableParser.\u003C\u003Ef__switch\u0024map28 == null)
          {
            // ISSUE: reference to a compiler-generated field
            StringVariableParser.\u003C\u003Ef__switch\u0024map28 = new Dictionary<string, int>(2)
            {
              {
                "VoteTopic",
                0
              },
              {
                "VoteDate",
                1
              }
            };
          }
          int num;
          // ISSUE: reference to a compiler-generated field
          if (StringVariableParser.\u003C\u003Ef__switch\u0024map28.TryGetValue(key, out num))
          {
            if (num != 0)
            {
              if (num == 1)
              {
                if (index >= Game.instance.player.team.championship.politicalSystem.votesForSeason.Count)
                  return;
                string_builder.Append(Game.instance.player.team.championship.politicalSystem.voteCalendarEventsForSeason[index].triggerDate.ToString());
                return;
              }
            }
            else
            {
              if (index >= Game.instance.player.team.championship.politicalSystem.votesForSeason.Count)
                return;
              string_builder.Append(Game.instance.player.team.championship.politicalSystem.votesForSeason[index].GetName());
              return;
            }
          }
        }
        if (inLanguage == Localisation.currentLanguage)
          Debug.LogError((object) (inString + "-- Does not have a return case --"), (UnityEngine.Object) null);
        string_builder.Append("{");
        string_builder.Append(inString);
        string_builder.Append("}");
        return;
      }
      string key1 = inString1;
      if (key1 != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (StringVariableParser.\u003C\u003Ef__switch\u0024map29 == null)
        {
          // ISSUE: reference to a compiler-generated field
          StringVariableParser.\u003C\u003Ef__switch\u0024map29 = new Dictionary<string, int>(202)
          {
            {
              "ChangeLiveryCost",
              0
            },
            {
              "ProjectedPartDesignDays",
              1
            },
            {
              "Parts",
              2
            },
            {
              "Part",
              3
            },
            {
              "Points",
              4
            },
            {
              "PenaltyAmount",
              4
            },
            {
              "Level",
              4
            },
            {
              "ChampionshipAbove",
              5
            },
            {
              "TheChampionshipAbove",
              6
            },
            {
              "TheChampionshipAboveUppercase",
              7
            },
            {
              "Championship",
              8
            },
            {
              "TheChampionship",
              9
            },
            {
              "TheChampionshipUppercase",
              10
            },
            {
              "ChampionshipBelow",
              11
            },
            {
              "TheChampionshipBelow",
              12
            },
            {
              "TheChampionshipBelowUppercase",
              13
            },
            {
              "T1ChampionshipAcronym",
              14
            },
            {
              "T2ChampionshipAcronym",
              15
            },
            {
              "T3ChampionshipAcronym",
              16
            },
            {
              "Tier1GT",
              17
            },
            {
              "Tier2GT",
              18
            },
            {
              "Tier1",
              19
            },
            {
              "Tier2",
              20
            },
            {
              "Tier3",
              21
            },
            {
              "PromotedTeam",
              22
            },
            {
              "PartBanned",
              23
            },
            {
              "Stat",
              24
            },
            {
              "CarPartStat",
              25
            },
            {
              "ComponentCost",
              26
            },
            {
              "RandomComponentName",
              27
            },
            {
              "ReliabilityPartName",
              28
            },
            {
              "LevelGapPartName",
              29
            },
            {
              "InterviewPullQuote",
              30
            },
            {
              "NextVoteStartDays",
              31
            },
            {
              "VoteTopic",
              32
            },
            {
              "PoliticalVoteSubject",
              32
            },
            {
              "LineBreak",
              33
            },
            {
              "NewSetupTime",
              34
            },
            {
              "Location",
              35
            },
            {
              "1stTo2ndGap",
              36
            },
            {
              "Gap1stTo2nd",
              36
            },
            {
              "Gap1stTo3rd",
              37
            },
            {
              "ChampionshipLead",
              38
            },
            {
              "PointsGap",
              38
            },
            {
              "TCLead",
              39
            },
            {
              "Publication",
              40
            },
            {
              "MediaOutlet",
              40
            },
            {
              "Year",
              41
            },
            {
              "CompletedPartDesign",
              42
            },
            {
              "Car1ConditionLoss",
              43
            },
            {
              "Car2ConditionLoss",
              44
            },
            {
              "ConditionFixTotalTime",
              45
            },
            {
              "NextYear",
              46
            },
            {
              "TwoYearsAhead",
              47
            },
            {
              "ThisYear",
              48
            },
            {
              "Yesterday",
              49
            },
            {
              "AdaptedPart",
              50
            },
            {
              "PartBuilt",
              51
            },
            {
              "CurrentWeakestPart",
              52
            },
            {
              "ISCarLowQualityPart",
              53
            },
            {
              "KnowledgeCompound",
              54
            },
            {
              "RuleChanges",
              55
            },
            {
              "PrizeMoneyAmount",
              56
            },
            {
              "PositiveColor",
              57
            },
            {
              "NegativeColor",
              58
            },
            {
              "BudgetDelta",
              59
            },
            {
              "CurrentWeather",
              60
            },
            {
              "PredictedWeather",
              61
            },
            {
              "TimeToWeatherChange",
              62
            },
            {
              "HighRacePayment",
              63
            },
            {
              "MedRacePayment",
              64
            },
            {
              "LowRacePayment",
              65
            },
            {
              "HighRacePaymentYearly",
              66
            },
            {
              "MedRacePaymentYearly",
              67
            },
            {
              "LowRacePaymentYearly",
              68
            },
            {
              "PreferencesArea",
              69
            },
            {
              "ChampionshipAcronym",
              70
            },
            {
              "SupplierOriginalPrice",
              71
            },
            {
              "SupplierDiscountPercent",
              72
            },
            {
              "MechanicRelationshipDriver",
              73
            },
            {
              "MechanicRelationshipWeeks",
              74
            },
            {
              "DriverName",
              75
            },
            {
              "PayDriverOffer",
              76
            },
            {
              "DilemmaTraitName",
              77
            },
            {
              "PersonalityTraitName",
              77
            },
            {
              "ContractOfferRacesToGo",
              78
            },
            {
              "ContractRacesToGo",
              79
            },
            {
              "DilemmaTransaction",
              80
            },
            {
              "DilemmaTeam",
              81
            },
            {
              "DilemmaBonus",
              82
            },
            {
              "DilemmaSubject",
              83
            },
            {
              "ContractNegsYear",
              84
            },
            {
              "ContractNegsMonth",
              85
            },
            {
              "ContractNegLastName",
              86
            },
            {
              "HQPartDesignDays",
              87
            },
            {
              "HQPartDevStaff",
              88
            },
            {
              "HQImproveSlots",
              89
            },
            {
              "HQScoutingDriverTier",
              90
            },
            {
              "HQRoadCarIncome",
              91
            },
            {
              "HQTourCentreIncome",
              92
            },
            {
              "HQThemeParkIncome",
              93
            },
            {
              "HQNextLevelPartDesignDays",
              94
            },
            {
              "HQNextLevelPartDevStaff",
              95
            },
            {
              "HQNextLevelImproveSlots",
              96
            },
            {
              "HQNextLevelScoutingDriverTier",
              97
            },
            {
              "HQNextLevelRoadCarIncome",
              98
            },
            {
              "HQNextLevelTourCentreIncome",
              99
            },
            {
              "HQNextLevelThemeParkIncome",
              100
            },
            {
              "PromiseBuilding",
              101
            },
            {
              "PitStrategy",
              102
            },
            {
              "PitTyreCompound",
              102
            },
            {
              "PitFuelLaps",
              103
            },
            {
              "RuleSessionLength",
              104
            },
            {
              "RuleRaceLength",
              105
            },
            {
              "RuleRaceAverageLaps",
              106
            },
            {
              "RuleNewCircuitName",
              107
            },
            {
              "RuleNewCircuitLayout",
              108
            },
            {
              "RuleOldCircuitName",
              109
            },
            {
              "RuleOldCircuitLayout",
              110
            },
            {
              "RuleNewLayout",
              111
            },
            {
              "RuleNewTrackWeekNumber",
              112
            },
            {
              "RuleOldTrackWeekNumber",
              113
            },
            {
              "FastestPitstopTime",
              114
            },
            {
              "PurpleSectorNumber",
              115
            },
            {
              "GreenSectorNumber",
              116
            },
            {
              "PurpleSectorTime",
              117
            },
            {
              "GreenSectorTime",
              118
            },
            {
              "LapCount",
              119
            },
            {
              "LargePitCrewCost",
              120
            },
            {
              "TyreCost",
              121
            },
            {
              "TyreSupplierCost",
              122
            },
            {
              "RefuellingCost",
              123
            },
            {
              "FuelWeight",
              124
            },
            {
              "FastPitSpeedLimit",
              125
            },
            {
              "SlowPitSpeedLimit",
              126
            },
            {
              "MinimumERSCost",
              (int) sbyte.MaxValue
            },
            {
              "HybridModeCost",
              128
            },
            {
              "PromotionBonus",
              129
            },
            {
              "LastPlaceBonus",
              130
            },
            {
              "PlayerVotePrice",
              131
            },
            {
              "KnowledgeLevel",
              132
            },
            {
              "KnowledgeAmount",
              133
            },
            {
              "PenaltyType",
              134
            },
            {
              "PenaltyTime",
              135
            },
            {
              "CalendarPartName",
              136
            },
            {
              "AmountOfNewParts",
              137
            },
            {
              "RoundNumber",
              137
            },
            {
              "CurrentRoundNumber",
              137
            },
            {
              "ChallengesRemainingToComplete",
              137
            },
            {
              "EventNumber",
              137
            },
            {
              "WeeksUntilRace",
              137
            },
            {
              "TrackGuideLaps",
              137
            },
            {
              "Days",
              137
            },
            {
              "Minutes",
              137
            },
            {
              "FinancesAmountOfTransactions",
              137
            },
            {
              "FinalRoundNumber",
              138
            },
            {
              "ContractJob",
              139
            },
            {
              "Job",
              139
            },
            {
              "Time",
              140
            },
            {
              "NewTrackLayout",
              141
            },
            {
              "CircuitName",
              142
            },
            {
              "TrackGuideLapLength",
              143
            },
            {
              "FloatNumber",
              144
            },
            {
              "number",
              145
            },
            {
              "Number",
              145
            },
            {
              "Inches",
              146
            },
            {
              "Number2",
              146
            },
            {
              "Number3",
              147
            },
            {
              "Feet",
              148
            },
            {
              "String",
              149
            },
            {
              "PartSet",
              149
            },
            {
              "CurrentChallengeName",
              149
            },
            {
              "RiskString",
              149
            },
            {
              "DriverNameShort",
              149
            },
            {
              "PersonNameShort",
              149
            },
            {
              "LastName",
              149
            },
            {
              "Month",
              150
            },
            {
              "Reward",
              151
            },
            {
              "FinancesCost",
              152
            },
            {
              "CarPartName",
              153
            },
            {
              "CarPart",
              154
            },
            {
              "SupplierName",
              155
            },
            {
              "SponsorName",
              156
            },
            {
              "BuildingName",
              157
            },
            {
              "NextYearCarInvestement",
              158
            },
            {
              "BuildingInfoName",
              159
            },
            {
              "ChallengeName",
              160
            },
            {
              "SessionType",
              161
            },
            {
              "Session",
              161
            },
            {
              "SessionTime",
              162
            },
            {
              "Age",
              163
            },
            {
              "VoteResult",
              164
            },
            {
              "WorkshopErrorType",
              165
            },
            {
              "WorkshopCreatorName",
              166
            },
            {
              "ModName",
              167
            },
            {
              "ModID",
              168
            },
            {
              "ModStatus",
              169
            },
            {
              "DuplicateDatabaseType",
              170
            },
            {
              "MMWebsiteLink",
              171
            },
            {
              "ColorCode",
              172
            },
            {
              "EndColorCode",
              173
            },
            {
              "PartType",
              174
            }
          };
        }
        int num1;
        // ISSUE: reference to a compiler-generated field
        if (StringVariableParser.\u003C\u003Ef__switch\u0024map29.TryGetValue(key1, out num1))
        {
          switch (num1)
          {
            case 0:
              string_builder.Append(GameUtility.GetCurrencyString(GameStatsConstants.liveryEditCost, 0));
              return;
            case 1:
              string_builder.Append(StringVariableParser.projectedPartDesignDays.ToString());
              return;
            case 2:
              string_builder.Append(Localisation.LocaliseEnum((Enum) (CarPart.PartTypePlural) StringVariableParser.partFrontendUI, inLanguage, string.Empty));
              return;
            case 3:
              string_builder.Append(Localisation.LocaliseEnum((Enum) StringVariableParser.partFrontendUI, inLanguage, string.Empty));
              return;
            case 4:
              string_builder.Append(StringVariableParser.intValue1.ToString());
              return;
            case 5:
              Championship championshipById1 = Game.instance.championshipManager.GetChampionshipByID(Game.instance.player.team.championship.championshipAboveID);
              string_builder.Append(championshipById1.GetChampionshipName(false));
              return;
            case 6:
              Championship championshipById2 = Game.instance.championshipManager.GetChampionshipByID(Game.instance.player.team.championship.championshipAboveID);
              string_builder.Append(championshipById2.GetTheChampionshipString());
              return;
            case 7:
              Championship championshipById3 = Game.instance.championshipManager.GetChampionshipByID(Game.instance.player.team.championship.championshipAboveID);
              string_builder.Append(championshipById3.GetTheChampionshipUppercaseString());
              return;
            case 8:
              string_builder.Append(Game.instance.player.team.championship.GetChampionshipName(false));
              return;
            case 9:
              string_builder.Append(Game.instance.player.team.championship.GetTheChampionshipString());
              return;
            case 10:
              string_builder.Append(Game.instance.player.team.championship.GetTheChampionshipUppercaseString());
              return;
            case 11:
              Championship championshipById4 = Game.instance.championshipManager.GetChampionshipByID(Game.instance.player.team.championship.championshipBelowID);
              string_builder.Append(championshipById4.GetChampionshipName(false));
              return;
            case 12:
              Championship championshipById5 = Game.instance.championshipManager.GetChampionshipByID(Game.instance.player.team.championship.championshipBelowID);
              string_builder.Append(championshipById5.GetTheChampionshipString());
              return;
            case 13:
              Championship championshipById6 = Game.instance.championshipManager.GetChampionshipByID(Game.instance.player.team.championship.championshipBelowID);
              string_builder.Append(championshipById6.GetTheChampionshipUppercaseString());
              return;
            case 14:
              string_builder.Append(Game.instance.championshipManager.GetChampionshipsByOrder(Championship.Series.SingleSeaterSeries)[0].GetAcronym(false));
              return;
            case 15:
              string_builder.Append(Game.instance.championshipManager.GetChampionshipsByOrder(Championship.Series.SingleSeaterSeries)[1].GetAcronym(false));
              return;
            case 16:
              string_builder.Append(Game.instance.championshipManager.GetChampionshipsByOrder(Championship.Series.SingleSeaterSeries)[2].GetAcronym(false));
              return;
            case 17:
              string_builder.Append(Game.instance.championshipManager.GetChampionshipsByOrder(Championship.Series.GTSeries)[0].GetChampionshipName(false));
              return;
            case 18:
              string_builder.Append(Game.instance.championshipManager.GetChampionshipsByOrder(Championship.Series.GTSeries)[1].GetChampionshipName(false));
              return;
            case 19:
              string_builder.Append(Game.instance.championshipManager.GetChampionshipsByOrder(Championship.Series.SingleSeaterSeries)[0].GetChampionshipName(false));
              return;
            case 20:
              string_builder.Append(Game.instance.championshipManager.GetChampionshipsByOrder(Championship.Series.SingleSeaterSeries)[1].GetChampionshipName(false));
              return;
            case 21:
              string_builder.Append(Game.instance.championshipManager.GetChampionshipsByOrder(Championship.Series.SingleSeaterSeries)[2].GetChampionshipName(false));
              return;
            case 22:
              string_builder.Append(StringVariableParser.promotedTeam.name);
              return;
            case 23:
              string_builder.Append(Localisation.LocaliseEnum((Enum) StringVariableParser.partBanned.GetPartType(), inLanguage, string.Empty));
              return;
            case 24:
              if (StringVariableParser.partWithComponent == null)
              {
                string_builder.Append(Localisation.LocaliseID("PSG_10001454", inLanguage, (GameObject) null, string.Empty));
                return;
              }
              string_builder.Append(Localisation.LocaliseEnum((Enum) StringVariableParser.partWithComponent.stats.statType, inLanguage, string.Empty));
              return;
            case 25:
              string_builder.Append(Localisation.LocaliseEnum((Enum) StringVariableParser.partStat, inLanguage, string.Empty));
              return;
            case 26:
              string_builder.Append(GameUtility.GetCurrencyString((long) (int) StringVariableParser.component.cost, 0));
              return;
            case 27:
              string_builder.Append(StringVariableParser.randomComponent.GetName(StringVariableParser.partWithRandomComponent));
              return;
            case 28:
              string_builder.Append(Localisation.LocaliseEnum((Enum) StringVariableParser.reliabilityPartType, inLanguage, string.Empty));
              return;
            case 29:
              string_builder.Append(Localisation.LocaliseEnum((Enum) StringVariableParser.levelGapPartType, inLanguage, string.Empty));
              return;
            case 30:
              string_builder.Append(Localisation.LocaliseID(StringVariableParser.sender.dialogQuery.GetMemory("InterviewPullQuote").mCriteriaInfo, inLanguage, (GameObject) null, string.Empty));
              return;
            case 31:
              TimeSpan timeSpan = Game.instance.player.team.championship.politicalSystem.nextVoteCalendarEvent.triggerDate - Game.instance.time.now;
              string_builder.Append(timeSpan.Days.ToString());
              return;
            case 32:
              if (StringVariableParser.vote != null)
              {
                string_builder.Append(StringVariableParser.vote.GetName());
                return;
              }
              string_builder.Append(Game.instance.player.team.championship.politicalSystem.activeVote.GetName());
              return;
            case 33:
              string_builder.Append("\n");
              return;
            case 34:
              string_builder.Append(StringVariableParser.newSetupTimeString);
              return;
            case 35:
              string_builder.Append(Localisation.LocaliseID(Game.instance.sessionManager.eventDetails.circuit.locationNameID, (GameObject) null));
              return;
            case 36:
              string_builder.Append(StringVariableParser.GetGap(0, 1));
              return;
            case 37:
              string_builder.Append(StringVariableParser.GetGap(0, 2));
              return;
            case 38:
              string_builder.Append((Game.instance.sessionManager.championship.standings.GetDriverEntry(0).GetCurrentPoints() - Game.instance.sessionManager.championship.standings.GetDriverEntry(1).GetCurrentPoints()).ToString());
              return;
            case 39:
              string_builder.Append((Game.instance.sessionManager.championship.standings.GetTeamEntry(0).GetCurrentPoints() - Game.instance.sessionManager.championship.standings.GetTeamEntry(1).GetCurrentPoints()).ToString());
              return;
            case 40:
              string_builder.Append(StringVariableParser.mediaOutlet == null ? "[Media Outlet]" : StringVariableParser.mediaOutlet.name);
              return;
            case 41:
              string_builder.Append(Game.instance.time.now.Year.ToString());
              return;
            case 42:
              string_builder.Append(Localisation.LocaliseEnum((Enum) Game.instance.player.team.carManager.carPartDesign.part.GetPartType(), inLanguage, string.Empty));
              return;
            case 43:
              string_builder.Append(string.Format("{0}%", (object) Mathf.RoundToInt(Game.instance.player.team.carManager.GetCar(0).carConditionAfterEvent * 100f)));
              return;
            case 44:
              string_builder.Append(string.Format("{0}%", (object) Mathf.RoundToInt(Game.instance.player.team.carManager.GetCar(1).carConditionAfterEvent * 100f)));
              return;
            case 45:
              int num2 = Mathf.RoundToInt((float) Game.instance.player.team.carManager.partImprovement.GetTimeToFinishWork(CarPartStats.CarPartStat.Condition).TotalHours);
              string_builder.Append(num2.ToString());
              return;
            case 46:
              string_builder.Append((Game.instance.time.now.Year + 1).ToString());
              return;
            case 47:
              string_builder.Append((Game.instance.time.now.Year + 2).ToString());
              return;
            case 48:
              string_builder.Append(Game.instance.time.now.Year.ToString());
              return;
            case 49:
              DayOfWeek dayOfWeek = Game.instance.time.now.AddDays(-1.0).DayOfWeek;
              string_builder.Append(GameUtility.GetLocalisedDay(dayOfWeek));
              return;
            case 50:
              string_builder.Append(Localisation.LocaliseEnum((Enum) StringVariableParser.partFrontendUI, inLanguage, string.Empty));
              return;
            case 51:
              string_builder.Append(Localisation.LocaliseEnum((Enum) StringVariableParser.partFrontendUI, inLanguage, string.Empty));
              return;
            case 52:
              string_builder.Append(Localisation.LocaliseEnum((Enum) StringVariableParser.weakestPart, inLanguage, string.Empty));
              return;
            case 53:
              string_builder.Append(Localisation.LocaliseEnum((Enum) TeamStatistics.GetLowQualityPart(Game.instance.player.team.championship), inLanguage, string.Empty));
              return;
            case 54:
              string_builder.Append(Localisation.LocaliseEnum((Enum) StringVariableParser.practiceKnowledgeCompound));
              return;
            case 55:
              List<PoliticalSystem.VoteResults> resultsForSeason = Game.instance.player.team.championship.politicalSystem.voteResultsForSeason;
              for (int index = 0; index < resultsForSeason.Count; ++index)
              {
                if (resultsForSeason[index].voteResult == PoliticalSystem.VoteResult.Accepted)
                  string_builder.Append(resultsForSeason[index].votedSubject.GetName() + "\n");
              }
              return;
            case 56:
              Championship championship = Game.instance.player.team.championship;
              int inIndex = championship.standings.GetTeamList().IndexOf(Game.instance.player.team);
              string_builder.Append(GameUtility.GetCurrencyString(championship.rules.GetPrizeMoney(inIndex, championship.prizeFund), 0));
              return;
            case 57:
              string_builder.Append(GameUtility.ColorToRichTextHex(UIConstants.positiveColor));
              return;
            case 58:
              string_builder.Append(GameUtility.ColorToRichTextHex(UIConstants.negativeColor));
              return;
            case 59:
              string_builder.Append(GameUtility.GetCurrencyString(StringVariableParser.budgetDelta, 0));
              return;
            case 60:
              string_builder.Append(Game.instance.sessionManager.currentSessionWeather.currentWeatherChange);
              return;
            case 61:
              string_builder.Append(Game.instance.sessionManager.currentSessionWeather.forecastWeatherChange);
              return;
            case 62:
              string_builder.Append(Game.instance.sessionManager.currentSessionWeather.forecastMinutes.ToString());
              return;
            case 63:
              string_builder.Append(GameUtility.GetCurrencyStringMillions(GameUtility.ToMillions(Game.instance.player.team.financeController.GetRacePaymentValue(TeamFinanceController.RacePaymentType.High)), 2));
              return;
            case 64:
              string_builder.Append(GameUtility.GetCurrencyStringMillions(GameUtility.ToMillions(Game.instance.player.team.financeController.GetRacePaymentValue(TeamFinanceController.RacePaymentType.Medium)), 2));
              return;
            case 65:
              string_builder.Append(GameUtility.GetCurrencyStringMillions(GameUtility.ToMillions(Game.instance.player.team.financeController.GetRacePaymentValue(TeamFinanceController.RacePaymentType.Low)), 2));
              return;
            case 66:
              string_builder.Append(GameUtility.GetCurrencyStringMillions(GameUtility.ToMillions((long) Game.instance.player.team.championship.eventsLeft * Game.instance.player.team.financeController.GetRacePaymentValue(TeamFinanceController.RacePaymentType.High)), 2));
              return;
            case 67:
              string_builder.Append(GameUtility.GetCurrencyStringMillions(GameUtility.ToMillions((long) Game.instance.player.team.championship.eventsLeft * Game.instance.player.team.financeController.GetRacePaymentValue(TeamFinanceController.RacePaymentType.Medium)), 2));
              return;
            case 68:
              string_builder.Append(GameUtility.GetCurrencyStringMillions(GameUtility.ToMillions((long) Game.instance.player.team.championship.eventsLeft * Game.instance.player.team.financeController.GetRacePaymentValue(TeamFinanceController.RacePaymentType.Low)), 2));
              return;
            case 69:
              if (UIManager.instance.currentScreen is PreferencesScreen)
              {
                PreferencesScreen currentScreen = (PreferencesScreen) UIManager.instance.currentScreen;
                string_builder.Append(Localisation.LocaliseEnum((Enum) currentScreen.mode, inLanguage, string.Empty));
                return;
              }
              string_builder.Append("Error");
              return;
            case 70:
              string_builder.Append(Game.instance.player.team.championship.GetAcronym(false));
              return;
            case 71:
              string_builder.Append(GameUtility.GetCurrencyString((long) StringVariableParser.supplierOriginalPrice, 0));
              return;
            case 72:
              string_builder.Append(StringVariableParser.supplierDiscountPercent.ToString((IFormatProvider) Localisation.numberFormatter));
              return;
            case 73:
              string_builder.Append(StringVariableParser.mechanicRelationshipDriver.name);
              return;
            case 74:
              string_builder.Append(StringVariableParser.mechanicRelationshipWeeks.ToString());
              return;
            case 75:
              string_builder.Append(StringVariableParser.personalityTraitDriver.name);
              return;
            case 76:
              if (StringVariableParser.anyDriver == null)
                return;
              string_builder.Append(GameUtility.GetCurrencyString((long) StringVariableParser.anyDriver.personalityTraitController.GetTieredPayDriverAmountForPlayer(), 0));
              return;
            case 77:
              string_builder.Append(StringVariableParser.personalityTraitName);
              return;
            case 78:
              string_builder.Append(StringVariableParser.contractSponsor.offerRacesLeft.ToString());
              return;
            case 79:
              string_builder.Append(StringVariableParser.contractSponsor.contractRacesLeft.ToString());
              return;
            case 80:
              string_builder.Append(GameUtility.GetCurrencyString((long) StringVariableParser.dilemmaTransaction, 0));
              return;
            case 81:
              string_builder.Append(StringVariableParser.dilemmaTeam.name);
              return;
            case 82:
              string_builder.Append(StringVariableParser.dilemmaBonus);
              return;
            case 83:
              string_builder.Append(StringVariableParser.dilemmaSubject.name);
              return;
            case 84:
              string_builder.Append(StringVariableParser.contractNegotiationYear);
              return;
            case 85:
              string_builder.Append(StringVariableParser.contractNegotiationMonths);
              return;
            case 86:
              string_builder.Append(StringVariableParser.contractNegotiationPersonLastName);
              return;
            case 87:
              string_builder.Append(Game.instance.player.team.headquarters.GetBuilding(HQsBuildingInfo.Type.DesignCentre).GetBuildingEffectString(false, 0));
              return;
            case 88:
              string_builder.Append(Game.instance.player.team.headquarters.GetBuilding(HQsBuildingInfo.Type.Factory).GetBuildingEffectString(false, 0));
              return;
            case 89:
              string_builder.Append(Game.instance.player.team.headquarters.GetBuilding(HQsBuildingInfo.Type.Factory).GetBuildingEffectString(false, 1));
              return;
            case 90:
              string_builder.Append(Game.instance.player.team.headquarters.GetBuilding(HQsBuildingInfo.Type.ScoutingFacility).GetBuildingEffectString(false, 0));
              return;
            case 91:
              string_builder.Append(Game.instance.player.team.headquarters.GetBuilding(HQsBuildingInfo.Type.RoadCarFactory).GetBuildingEffectString(false, 0));
              return;
            case 92:
              string_builder.Append(Game.instance.player.team.headquarters.GetBuilding(HQsBuildingInfo.Type.TourCentre).GetBuildingEffectString(false, 0));
              return;
            case 93:
              string_builder.Append(Game.instance.player.team.headquarters.GetBuilding(HQsBuildingInfo.Type.ThemePark).GetBuildingEffectString(false, 0));
              return;
            case 94:
              string_builder.Append(Game.instance.player.team.headquarters.GetBuilding(HQsBuildingInfo.Type.DesignCentre).GetBuildingEffectString(true, 0));
              return;
            case 95:
              string_builder.Append(Game.instance.player.team.headquarters.GetBuilding(HQsBuildingInfo.Type.Factory).GetBuildingEffectString(true, 0));
              return;
            case 96:
              string_builder.Append(Game.instance.player.team.headquarters.GetBuilding(HQsBuildingInfo.Type.Factory).GetBuildingEffectString(true, 1));
              return;
            case 97:
              string_builder.Append(Game.instance.player.team.headquarters.GetBuilding(HQsBuildingInfo.Type.ScoutingFacility).GetBuildingEffectString(true, 0));
              return;
            case 98:
              string_builder.Append(Game.instance.player.team.headquarters.GetBuilding(HQsBuildingInfo.Type.RoadCarFactory).GetBuildingEffectString(true, 0));
              return;
            case 99:
              string_builder.Append(Game.instance.player.team.headquarters.GetBuilding(HQsBuildingInfo.Type.TourCentre).GetBuildingEffectString(true, 0));
              return;
            case 100:
              string_builder.Append(Game.instance.player.team.headquarters.GetBuilding(HQsBuildingInfo.Type.ThemePark).GetBuildingEffectString(true, 0));
              return;
            case 101:
              string_builder.Append(Game.instance.player.promisesController.nextBuildingToBuildPromised.buildingName);
              return;
            case 102:
              string_builder.Append(StringVariableParser.stringValue1);
              return;
            case 103:
              string_builder.Append(StringVariableParser.intValue1);
              return;
            case 104:
              string_builder.Append(GameUtility.FormatMinutesToString(StringVariableParser.sessionLenghtMinutes));
              return;
            case 105:
              string_builder.Append(GameUtility.FormatMilesToString((int) StringVariableParser.raceLengthMiles));
              return;
            case 106:
              string_builder.Append(StringVariableParser.raceLengthAverageLaps.ToString());
              return;
            case 107:
              string_builder.Append(Localisation.LocaliseID(StringVariableParser.newTrack.locationNameID, inLanguage, (GameObject) null, string.Empty));
              return;
            case 108:
              string_builder.Append(StringVariableParser.newTrackLayout);
              return;
            case 109:
              string_builder.Append(Localisation.LocaliseID(StringVariableParser.oldTrackTrack.locationNameID, inLanguage, (GameObject) null, string.Empty));
              return;
            case 110:
              string_builder.Append(StringVariableParser.oldTrackLayout);
              return;
            case 111:
              string_builder.Append(StringVariableParser.newTrackLayout);
              return;
            case 112:
              string_builder.Append("48");
              return;
            case 113:
              string_builder.Append(StringVariableParser.oldTrackWeekNumber);
              return;
            case 114:
              string_builder.Append(StringVariableParser.fastestPitstopTime.ToString("0.00", (IFormatProvider) Localisation.numberFormatter));
              return;
            case 115:
              string_builder.Append((StringVariableParser.purpleSectorNumber + 1).ToString());
              return;
            case 116:
              string_builder.Append((StringVariableParser.greenSectorNumber + 1).ToString());
              return;
            case 117:
              string_builder.Append(GameUtility.GetLapTimeText(StringVariableParser.purpleSectorTime, false));
              return;
            case 118:
              string_builder.Append(GameUtility.GetLapTimeText(StringVariableParser.greenSectorTime, false));
              return;
            case 119:
              string_builder.Append(Game.instance.sessionManager.lap.ToString());
              return;
            case 120:
              string_builder.Append(GameUtility.GetCurrencyString(GameStatsConstants.largePitCrewCost, 0));
              return;
            case 121:
              string_builder.Append(GameUtility.GetCurrencyString(StringVariableParser.longValue, 0));
              return;
            case 122:
              string_builder.Append(GameUtility.GetCurrencyString(StringVariableParser.longValue, 0));
              return;
            case 123:
              string_builder.Append(GameUtility.GetCurrencyString(GameStatsConstants.refuellingCost, 0));
              return;
            case 124:
              string_builder.Append(GameUtility.GetWeightText(StringVariableParser.fuelLimit, 2));
              return;
            case 125:
              string_builder.Append(GameUtility.GetSpeedText(GameUtility.MilesPerHourToMetersPerSecond(60f), 10f));
              return;
            case 126:
              string_builder.Append(GameUtility.GetSpeedText(GameUtility.MilesPerHourToMetersPerSecond(40f), 10f));
              return;
            case (int) sbyte.MaxValue:
              string_builder.Append(GameUtility.GetCurrencyString(StringVariableParser.minimumErsCost, 0));
              return;
            case 128:
              string_builder.Append(GameUtility.GetCurrencyString((long) GameStatsConstants.hybridModeCost, 0));
              return;
            case 129:
              string_builder.Append(GameUtility.GetCurrencyString(GameStatsConstants.promotionBonus, 0));
              return;
            case 130:
              string_builder.Append(GameUtility.GetCurrencyString(GameStatsConstants.lastPlaceBonus, 0));
              return;
            case 131:
              string_builder.Append(GameUtility.GetCurrencyString(GameStatsConstants.playerVotePrice, 0));
              return;
            case 132:
              string_builder.Append(StringVariableParser.knowledgeLevel.ToString());
              return;
            case 133:
              string_builder.Append(StringVariableParser.knowledgeAmount.ToString((IFormatProvider) Localisation.numberFormatter));
              return;
            case 134:
              string_builder.Append(Localisation.LocaliseEnum((Enum) StringVariableParser.penaltyCause));
              return;
            case 135:
              if (StringVariableParser.penaltyTimeSeconds == 0)
              {
                string_builder.Append(StringVariableParser.intValue1.ToString());
                return;
              }
              string_builder.Append(StringVariableParser.penaltyTimeSeconds.ToString());
              return;
            case 136:
              string_builder.Append(Localisation.LocaliseEnum((Enum) StringVariableParser.carPart.GetPartType(), inLanguage, string.Empty));
              return;
            case 137:
              string_builder.Append(StringVariableParser.intValue1.ToString());
              return;
            case 138:
              string_builder.Append(StringVariableParser.intValue2.ToString());
              return;
            case 139:
              string_builder.Append(Localisation.LocaliseEnum((Enum) StringVariableParser.contractJob));
              return;
            case 140:
              string_builder.Append(StringVariableParser.intValue1.ToString());
              string_builder.Append(":");
              string_builder.Append(StringVariableParser.intValue2.ToString("00"));
              if (!StringVariableParser.includeSeconds)
                return;
              string_builder.Append(":");
              string_builder.Append(StringVariableParser.intValue3.ToString("00"));
              return;
            case 141:
              string_builder.Append(StringVariableParser.newTrackLayout);
              return;
            case 142:
              string_builder.Append(Localisation.LocaliseID(StringVariableParser.randomCircuit.locationNameID, inLanguage, (GameObject) null, string.Empty));
              return;
            case 143:
              string_builder.Append(StringVariableParser.floatValue1.ToString((IFormatProvider) Localisation.numberFormatter));
              return;
            case 144:
              string_builder.Append(StringVariableParser.floatValue1.ToString((IFormatProvider) Localisation.numberFormatter));
              return;
            case 145:
              if (string.IsNullOrEmpty(StringVariableParser.ordinalNumberString))
              {
                string_builder.Append(StringVariableParser.intValue1.ToString());
                return;
              }
              string_builder.Append(StringVariableParser.ordinalNumberString);
              StringVariableParser.ordinalNumberString = string.Empty;
              return;
            case 146:
              string_builder.Append(StringVariableParser.intValue2.ToString());
              return;
            case 147:
              string_builder.Append(StringVariableParser.intValue3.ToString());
              return;
            case 148:
              string_builder.Append(StringVariableParser.intValue1.ToString());
              return;
            case 149:
              string_builder.Append(StringVariableParser.stringValue1);
              StringVariableParser.stringValue1 = string.Empty;
              return;
            case 150:
              string_builder.Append(GameUtility.GetLocalisedMonth(StringVariableParser.month));
              return;
            case 151:
              string_builder.Append(GameUtility.GetCurrencyString((long) StringVariableParser.intValue2, 0));
              return;
            case 152:
              string_builder.Append(Localisation.LocaliseEnum((Enum) StringVariableParser.financialTransaction, inLanguage, "Male"));
              return;
            case 153:
              string_builder.Append(StringVariableParser.partForUI.GetPartName());
              return;
            case 154:
              string_builder.Append(Localisation.LocaliseEnum((Enum) StringVariableParser.partForUI.GetPartType()));
              return;
            case 155:
              string_builder.Append(StringVariableParser.supplier.name);
              return;
            case 156:
              string_builder.Append(StringVariableParser.sponsor.name);
              return;
            case 157:
              string_builder.Append(StringVariableParser.building.buildingName);
              return;
            case 158:
              string_builder.Append(Localisation.LocaliseEnum((Enum) StringVariableParser.nextYearCarInvestment));
              return;
            case 159:
              string_builder.Append(StringVariableParser.building.buildingName);
              return;
            case 160:
              string_builder.Append(Localisation.LocaliseEnum((Enum) StringVariableParser.challengeName));
              return;
            case 161:
              string_builder.Append(Localisation.LocaliseEnum((Enum) StringVariableParser.sessionType));
              return;
            case 162:
              string_builder.Append(StringVariableParser.sessionTime);
              return;
            case 163:
              string_builder.Append(StringVariableParser.intValue1.ToString());
              return;
            case 164:
              string_builder.Append(Localisation.LocaliseEnum((Enum) StringVariableParser.voteResult));
              return;
            case 165:
              string_builder.Append(StringVariableParser.workshopErrorType);
              return;
            case 166:
              string_builder.Append(StringVariableParser.workshopCreatorName);
              return;
            case 167:
              string_builder.Append(StringVariableParser.modName);
              return;
            case 168:
              string_builder.Append(StringVariableParser.modID.ToString((IFormatProvider) Localisation.numberFormatter));
              return;
            case 169:
              string_builder.Append(StringVariableParser.modStatus);
              return;
            case 170:
              string_builder.Append(StringVariableParser.duplicateDatabaseType);
              return;
            case 171:
              StringVariableParser.GetWebsiteLinkOnOverlay(ref string_builder, "http://www.motorsportmanager.com/news");
              return;
            case 172:
              string_builder.Append(StringVariableParser.stringValue1);
              return;
            case 173:
              string_builder.Append("</color>");
              return;
            case 174:
              string_builder.Append(Localisation.LocaliseEnum((Enum) StringVariableParser.partFrontendUI));
              return;
          }
        }
      }
      if (inLanguage == Localisation.currentLanguage)
        Debug.LogError((object) (inString + "-- Does not have a return case --"), (UnityEngine.Object) null);
      string_builder.Append("{");
      string_builder.Append(inString);
      string_builder.Append("}");
      return;
    }
    catch (Exception ex)
    {
      Debug.LogError((object) string.Format("Error with string: {0} \n {1}", (object) inString, (object) ex.Message), (UnityEngine.Object) null);
    }
    string_builder.Append("{");
    string_builder.Append(inString);
    string_builder.Append("}");
  }

  private static void GetLinkForPerson(Person inPerson, string inData, ref StringBuilder string_builder)
  {
    if (StringVariableParser.useLinkData && (inPerson is Driver || inPerson is Engineer || inPerson is Mechanic))
    {
      string_builder.Append("<u><link=\"PersonScreen=");
      string_builder.Append(inPerson.id.ToString("D"));
      string_builder.Append("\">");
      string_builder.Append(inData);
      string_builder.Append("</link></u>");
    }
    else
      string_builder.Append(inData);
  }

  private static void GetWebsiteLinkOnOverlay(ref StringBuilder string_builder, string inWebsite)
  {
    string_builder.Append("<u><link=\"WebsiteLinkOverlay=");
    string_builder.Append(inWebsite);
    string_builder.Append("\">");
    string_builder.Append(inWebsite);
    string_builder.Append("</link></u>");
  }

  public static void GetPersonSpecialData(Person inPerson, string inData, string inLanguage, ref StringBuilder string_builder)
  {
    try
    {
      string key = inData;
      if (key != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (StringVariableParser.\u003C\u003Ef__switch\u0024map2A == null)
        {
          // ISSUE: reference to a compiler-generated field
          StringVariableParser.\u003C\u003Ef__switch\u0024map2A = new Dictionary<string, int>(71)
          {
            {
              "TwitterName",
              0
            },
            {
              "PreSeasonPosition",
              1
            },
            {
              "Name",
              2
            },
            {
              "FirstName",
              3
            },
            {
              "LastName",
              4
            },
            {
              "ShortName",
              5
            },
            {
              "Age",
              6
            },
            {
              "UpperCaseNationality",
              7
            },
            {
              "Nationality",
              8
            },
            {
              "Country",
              9
            },
            {
              "Wages",
              10
            },
            {
              "ContractEndDate",
              11
            },
            {
              "Job",
              12
            },
            {
              "HighestStat",
              13
            },
            {
              "LowestStat",
              14
            },
            {
              "SessionType",
              15
            },
            {
              "Employer",
              16
            },
            {
              "Celebrity",
              17
            },
            {
              "Salary",
              18
            },
            {
              "SeasonalBonus",
              19
            },
            {
              "PreviousTeam",
              20
            },
            {
              "PreviousTeamLocation",
              21
            },
            {
              "PreviousTeamNationality",
              22
            },
            {
              "Laptime",
              23
            },
            {
              "EventPosition",
              24
            },
            {
              "EventPositionOrdinal",
              25
            },
            {
              "DCPosition",
              26
            },
            {
              "LastRacePosition",
              27
            },
            {
              "Team",
              28
            },
            {
              "TeamName",
              28
            },
            {
              "Teammate",
              29
            },
            {
              "TeamTwitter",
              30
            },
            {
              "TeamPredictedPosition",
              31
            },
            {
              "PredictedEndSeasonPosition",
              32
            },
            {
              "Mechanic",
              33
            },
            {
              "MechanicName",
              33
            },
            {
              "Engineer",
              34
            },
            {
              "Manager",
              35
            },
            {
              "Chairman",
              36
            },
            {
              "TeammateRival",
              37
            },
            {
              "Rival",
              37
            },
            {
              "FoughtWith",
              38
            },
            {
              "Location",
              39
            },
            {
              "TeamLocation",
              39
            },
            {
              "TeamNationality",
              40
            },
            {
              "UltimatumPosition",
              41
            },
            {
              "HighEstimatedPosition",
              42
            },
            {
              "MedEstimatedPosition",
              43
            },
            {
              "LowEstimatedPosition",
              44
            },
            {
              "Championship",
              45
            },
            {
              "TheChampionship",
              46
            },
            {
              "TheChampionshipUppercase",
              47
            },
            {
              "ChampionshipAcronym",
              48
            },
            {
              "PredictedPosition",
              49
            },
            {
              "CurrentTeamStay",
              50
            },
            {
              "BestTeam",
              51
            },
            {
              "PredictedRacePosition",
              52
            },
            {
              "PreviousSessionPosition",
              53
            },
            {
              "CurrentSessionPosition",
              53
            },
            {
              "ChampionshipPositionPromise",
              54
            },
            {
              "RaceWins",
              55
            },
            {
              "ChampionshipWins",
              56
            },
            {
              "T1ChampionshipWins",
              57
            },
            {
              "T2ChampionshipWins",
              58
            },
            {
              "FastestLap",
              59
            },
            {
              "Position",
              60
            },
            {
              "CurrentCompound",
              61
            },
            {
              "PreviousLapTime",
              62
            },
            {
              "StintLength",
              63
            },
            {
              "FastestPitstopTime",
              64
            },
            {
              "PayDriverAmount",
              65
            }
          };
        }
        int num;
        // ISSUE: reference to a compiler-generated field
        if (StringVariableParser.\u003C\u003Ef__switch\u0024map2A.TryGetValue(key, out num))
        {
          switch (num)
          {
            case 0:
              string_builder.Append(inPerson.twitterHandle);
              return;
            case 1:
              if (inPerson.IsFreeAgent())
                return;
              RaceEventResults.ResultData seasonResultData = inPerson.contract.GetTeam().championship.preSeasonTesting.GetDriverPreSeasonResultData(inPerson as Driver);
              if (seasonResultData == null)
                return;
              string_builder.Append(GameUtility.FormatForPosition(seasonResultData.position, inLanguage));
              return;
            case 2:
              StringVariableParser.GetLinkForPerson(inPerson, inPerson.name, ref string_builder);
              return;
            case 3:
              StringVariableParser.GetLinkForPerson(inPerson, inPerson.firstName, ref string_builder);
              return;
            case 4:
              StringVariableParser.GetLinkForPerson(inPerson, inPerson.lastName, ref string_builder);
              return;
            case 5:
              StringVariableParser.GetLinkForPerson(inPerson, inPerson.shortName, ref string_builder);
              return;
            case 6:
              string_builder.Append(inPerson.GetAge().ToString());
              return;
            case 7:
              StringVariableParser.nationalityGender = inPerson.gender.ToString();
              string_builder.Append(GameUtility.ChangeFirstCharToUpperCase(inPerson.nationality.localisedNationality));
              return;
            case 8:
              StringVariableParser.nationalityGender = inPerson.gender.ToString();
              string_builder.Append(inPerson.nationality.localisedNationality);
              return;
            case 9:
              StringVariableParser.nationalityGender = inPerson.gender.ToString();
              string_builder.Append(inPerson.nationality.localisedCountry);
              return;
            case 10:
              string_builder.Append(GameUtility.GetCurrencyString((long) inPerson.contract.yearlyWages, 0));
              return;
            case 11:
              string_builder.Append(inPerson.contract.endDate.Year.ToString());
              return;
            case 12:
              string_builder.Append(Localisation.LocaliseEnum((Enum) inPerson.contract.job));
              return;
            case 13:
              Driver driver1 = inPerson as Driver;
              if (driver1 != null)
              {
                string_builder.Append(DriverStats.GetTranslatedStatName(driver1.GetDriverStats().GetBestStat().Key));
                return;
              }
              Mechanic mechanic1 = inPerson as Mechanic;
              if (mechanic1 != null)
              {
                string_builder.Append(MechanicStats.GetTranslatedStatName(mechanic1.stats.GetBestStat().Key));
                return;
              }
              Engineer engineer1 = inPerson as Engineer;
              if (engineer1 != null)
              {
                string_builder.Append(EngineerStats.GetTranslatedStatName(engineer1.stats.GetBestStat().Key));
                return;
              }
              string_builder.Append("[{HighestStat} failed]");
              return;
            case 14:
              Driver driver2 = inPerson as Driver;
              if (driver2 != null)
              {
                string_builder.Append(DriverStats.GetTranslatedStatName(driver2.GetDriverStats().GetWorstStat().Key));
                return;
              }
              Mechanic mechanic2 = inPerson as Mechanic;
              if (mechanic2 != null)
              {
                string_builder.Append(MechanicStats.GetTranslatedStatName(mechanic2.stats.GetWorstStat().Key));
                return;
              }
              Engineer engineer2 = inPerson as Engineer;
              if (engineer2 != null)
              {
                string_builder.Append(EngineerStats.GetTranslatedStatName(engineer2.stats.GetWorstStat().Key));
                return;
              }
              string_builder.Append("[{LowestStat} failed]");
              return;
            case 15:
              string_builder.Append(Localisation.LocaliseEnum((Enum) Game.instance.sessionManager.sessionType));
              return;
            case 16:
              string_builder.Append(inPerson.contract.employeerName);
              return;
            case 17:
              Driver driver3 = inPerson as Driver;
              if (driver3.gender == Person.Gender.Male)
              {
                if (driver3.celebrity == null)
                  driver3.celebrity = App.instance.regenManager.CreateFan(RegenManager.RegenType.Poor, Person.Gender.Female, true);
              }
              else if (driver3.celebrity == null)
                driver3.celebrity = App.instance.regenManager.CreateFan(RegenManager.RegenType.Poor, Person.Gender.Male, true);
              string_builder.Append(driver3.celebrity.name);
              return;
            case 18:
              string_builder.Append(GameUtility.GetCurrencyString((long) (inPerson.contract.yearlyWages / 10), 0));
              return;
            case 19:
              string_builder.Append(GameUtility.GetCurrencyString((long) inPerson.contract.championBonus, 0));
              return;
            case 20:
              StringVariableParser.GetTeamSpecialData(inPerson.careerHistory.previousTeam, "Name", inLanguage, ref string_builder);
              return;
            case 21:
              string_builder.Append(Localisation.LocaliseID(inPerson.careerHistory.previousTeam.locationID, inLanguage, (GameObject) null, string.Empty));
              return;
            case 22:
              StringVariableParser.nationalityGender = inPerson.gender.ToString();
              string_builder.Append(inPerson.careerHistory.previousTeam.nationality.localisedNationality);
              return;
            case 23:
              SessionManager sessionManager = Game.instance.sessionManager;
              SessionDetails.SessionType sessionTypeWithData1 = sessionManager.eventDetails.results.GetLastestSessionTypeWithData();
              RaceEventResults.SessonResultData resultsForSession = sessionManager.eventDetails.results.GetResultsForSession(sessionTypeWithData1);
              string_builder.Append(GameUtility.GetLapTimeText(resultsForSession.GetResultForDriver((Driver) inPerson).bestLapTime, false));
              return;
            case 24:
              string_builder.Append(Game.instance.sessionManager.GetDriversCar((Driver) inPerson).standingsPosition.ToString());
              return;
            case 25:
              string_builder.Append(GameUtility.FormatForPosition(Game.instance.sessionManager.GetDriversCar((Driver) inPerson).standingsPosition, inLanguage));
              return;
            case 26:
              string_builder.Append(GameUtility.FormatForPosition(((Driver) inPerson).GetChampionshipEntry().GetCurrentChampionshipPosition(), inLanguage));
              return;
            case 27:
              string_builder.Append(GameUtility.FormatForPosition(((Driver) inPerson).GetChampionshipEntry().GetLatestRacePosition(), inLanguage));
              return;
            case 28:
              StringVariableParser.GetTeamSpecialData(inPerson.contract.GetTeam(), "Name", inLanguage, ref string_builder);
              return;
            case 29:
              StringVariableParser.GetPersonSpecialData((Person) inPerson.contract.GetTeam().GetTeamMate(inPerson as Driver), "Name", inLanguage, ref string_builder);
              return;
            case 30:
              string_builder.Append(inPerson.contract.GetTeam().twitterHandle);
              return;
            case 31:
              string_builder.Append(GameUtility.FormatForPosition(inPerson.contract.GetTeam().GetExpectedChampionshipResult(), inLanguage));
              return;
            case 32:
              string_builder.Append(GameUtility.FormatForPosition(Mathf.RoundToInt(inPerson.GetChampionshipExpectation()), inLanguage));
              return;
            case 33:
              Driver inDriver = inPerson as Driver;
              if (!inPerson.IsFreeAgent() && inDriver.IsMainDriver())
              {
                Mechanic mechanicOfDriver = inPerson.contract.GetTeam().GetMechanicOfDriver(inDriver);
                if (mechanicOfDriver != null)
                {
                  StringVariableParser.GetPersonSpecialData((Person) mechanicOfDriver, "Name", inLanguage, ref string_builder);
                  return;
                }
              }
              string_builder.Append("[MechanicName]");
              return;
            case 34:
              if (!inPerson.IsFreeAgent())
              {
                StringVariableParser.GetPersonSpecialData(inPerson.contract.GetTeam().contractManager.GetPersonOnJob(Contract.Job.EngineerLead), "Name", inLanguage, ref string_builder);
                return;
              }
              string_builder.Append("[EngineerName]");
              return;
            case 35:
              StringVariableParser.GetPersonSpecialData((Person) inPerson.contract.GetTeam().teamPrincipal, "Name", inLanguage, ref string_builder);
              return;
            case 36:
              StringVariableParser.GetPersonSpecialData((Person) inPerson.contract.GetTeam().chairman, "Name", inLanguage, ref string_builder);
              return;
            case 37:
              Driver driver4 = inPerson as Driver;
              if (driver4.GetRivalDriver() != null)
              {
                string_builder.Append(driver4.GetRivalDriver().name);
                return;
              }
              string_builder.Append("[TraitRivalName]");
              return;
            case 38:
              if (StringVariableParser.fightWithDriverTrait != null)
              {
                string_builder.Append(StringVariableParser.fightWithDriverTrait.name);
                return;
              }
              string_builder.Append("[FightWithTeammate]");
              return;
            case 39:
              string_builder.Append(Localisation.LocaliseID(inPerson.contract.GetTeam().locationID, inLanguage, (GameObject) null, string.Empty));
              return;
            case 40:
              string_builder.Append(inPerson.contract.GetTeam().nationality.localisedCountry);
              return;
            case 41:
              string_builder.Append(GameUtility.FormatForPosition(((Chairman) inPerson).ultimatum.positionExpected, inLanguage));
              return;
            case 42:
              string_builder.Append(GameUtility.FormatForPosition(((Chairman) inPerson).GetEstimatedPosition(Chairman.EstimatedPosition.High, Game.instance.player.team), inLanguage));
              return;
            case 43:
              string_builder.Append(GameUtility.FormatForPosition(((Chairman) inPerson).GetEstimatedPosition(Chairman.EstimatedPosition.Medium, Game.instance.player.team), inLanguage));
              return;
            case 44:
              string_builder.Append(GameUtility.FormatForPosition(((Chairman) inPerson).GetEstimatedPosition(Chairman.EstimatedPosition.Low, Game.instance.player.team), inLanguage));
              return;
            case 45:
              string_builder.Append(inPerson.contract.GetTeam().championship.GetChampionshipName(false));
              return;
            case 46:
              string_builder.Append(inPerson.contract.GetTeam().championship.GetTheChampionshipString());
              return;
            case 47:
              string_builder.Append(inPerson.contract.GetTeam().championship.GetTheChampionshipUppercaseString());
              return;
            case 48:
              string_builder.Append(inPerson.contract.GetTeam().championship.GetAcronym(false));
              return;
            case 49:
              string_builder.Append(GameUtility.FormatForPosition(((Driver) inPerson).expectedChampionshipPosition, inLanguage));
              return;
            case 50:
              string_builder.Append(inPerson.careerHistory.GetYearsAtCurrentTeam().ToString());
              return;
            case 51:
              StringVariableParser.GetTeamSpecialData(inPerson.GetBestTeam(), "Name", inLanguage, ref string_builder);
              return;
            case 52:
              string_builder.Append(GameUtility.FormatForPosition(((Driver) inPerson).GetRaceExpectedPosition(), inLanguage));
              return;
            case 53:
              SessionDetails.SessionType sessionTypeWithData2 = Game.instance.sessionManager.eventDetails.results.GetLastestSessionTypeWithData();
              RaceEventResults.ResultData resultForDriver = Game.instance.sessionManager.eventDetails.results.GetResultsForSession(sessionTypeWithData2).GetResultForDriver((Driver) inPerson);
              string_builder.Append(GameUtility.FormatForPosition(resultForDriver.position, inLanguage));
              return;
            case 54:
              int positionPromised = Game.instance.player.promisesController.championshipPositionPromised;
              string_builder.Append(GameUtility.FormatForPosition(positionPromised, inLanguage));
              return;
            case 55:
              string_builder.Append(inPerson.careerHistory.GetTotalCareerWins().ToString());
              return;
            case 56:
              string_builder.Append(inPerson.careerHistory.GetTotalCareerChampionships().ToString());
              return;
            case 57:
              string_builder.Append(inPerson.careerHistory.GetTotalCareerChampionships(0).ToString());
              return;
            case 58:
              string_builder.Append(inPerson.careerHistory.GetTotalCareerChampionships(1).ToString());
              return;
            case 59:
              RacingVehicle vehicle1 = Game.instance.vehicleManager.GetVehicle((Driver) inPerson);
              string_builder.Append(GameUtility.GetLapTimeText(vehicle1.timer.GetFastestLapTime(), false));
              return;
            case 60:
              RacingVehicle vehicle2 = Game.instance.vehicleManager.GetVehicle((Driver) inPerson);
              string_builder.Append(GameUtility.FormatForPosition(vehicle2.standingsPosition, inLanguage));
              return;
            case 61:
              RacingVehicle vehicle3 = Game.instance.vehicleManager.GetVehicle((Driver) inPerson);
              string_builder.Append(Localisation.LocaliseEnum((Enum) vehicle3.setup.tyreSet.GetCompound()));
              return;
            case 62:
              RacingVehicle vehicle4 = Game.instance.vehicleManager.GetVehicle((Driver) inPerson);
              string_builder.Append(GameUtility.GetLapTimeText(vehicle4.timer.GetPreviousLapData().time, false));
              return;
            case 63:
              RacingVehicle vehicle5 = Game.instance.vehicleManager.GetVehicle((Driver) inPerson);
              string_builder.Append(vehicle5.stints.GetCurrentStint().lapCount.ToString());
              return;
            case 64:
              RacingVehicle vehicle6 = Game.instance.vehicleManager.GetVehicle((Driver) inPerson);
              string_builder.Append(vehicle6.timer.fastestPitStop.ToString("0.00", (IFormatProvider) Localisation.numberFormatter));
              return;
            case 65:
              Driver driver5 = inPerson as Driver;
              if (driver5 == null)
                return;
              string_builder.Append(GameUtility.GetCurrencyString((long) driver5.personalityTraitController.GetTieredPayDriverAmountForPlayer(), 0));
              return;
          }
        }
      }
    }
    catch (Exception ex)
    {
      Debug.LogError((object) string.Format("Error with string: {0} \n{1}", (object) inData, (object) ex.Message), (UnityEngine.Object) null);
    }
    if (inLanguage == Localisation.currentLanguage)
      Debug.LogError((object) ("Person: {" + inData + "} -- Does not have a return case --"), (UnityEngine.Object) null);
    string_builder.Append("{");
    string_builder.Append(inData);
    string_builder.Append("}");
  }

  private static void GetTeamSpecialData(Team inTeam, string inData, string inLanguage, ref StringBuilder string_builder)
  {
    try
    {
      string key = inData;
      if (key != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (StringVariableParser.\u003C\u003Ef__switch\u0024map2B == null)
        {
          // ISSUE: reference to a compiler-generated field
          StringVariableParser.\u003C\u003Ef__switch\u0024map2B = new Dictionary<string, int>(30)
          {
            {
              "Name",
              0
            },
            {
              "Nationality",
              1
            },
            {
              "UpperCaseNationality",
              2
            },
            {
              "Country",
              3
            },
            {
              "Manager",
              4
            },
            {
              "ManagerName",
              4
            },
            {
              "EngineerName",
              5
            },
            {
              "Engineer",
              5
            },
            {
              "ChairmanName",
              6
            },
            {
              "Chairman",
              6
            },
            {
              "Driver1",
              7
            },
            {
              "Driver2",
              8
            },
            {
              "RandomDriver",
              9
            },
            {
              "Location",
              10
            },
            {
              "PredictedPosition",
              11
            },
            {
              "PredictedRacePosition",
              12
            },
            {
              "PredictedResult",
              12
            },
            {
              "Points",
              13
            },
            {
              "Championship",
              14
            },
            {
              "TheChampionship",
              15
            },
            {
              "TheChampionshipUppercase",
              16
            },
            {
              "ChampionshipAcronym",
              17
            },
            {
              "TCPosition",
              18
            },
            {
              "LastRacePosition",
              19
            },
            {
              "PreviousQualifyingPosition",
              20
            },
            {
              "PreviousPracticePosition",
              21
            },
            {
              "ColouredName",
              22
            },
            {
              "ChairmanTarget",
              23
            },
            {
              "Marketability",
              24
            },
            {
              "TwitterName",
              25
            }
          };
        }
        int num;
        // ISSUE: reference to a compiler-generated field
        if (StringVariableParser.\u003C\u003Ef__switch\u0024map2B.TryGetValue(key, out num))
        {
          switch (num)
          {
            case 0:
              if (StringVariableParser.useLinkData)
              {
                string_builder.Append("<u><link=\"TeamScreen=");
                string_builder.Append(inTeam.id.ToString("D"));
                string_builder.Append("\">");
                string_builder.Append(inTeam.name);
                string_builder.Append("</link></u>");
                return;
              }
              string_builder.Append(inTeam.name);
              return;
            case 1:
              StringVariableParser.nationalityGender = StringVariableParser.GetWordGender((object) inTeam, string.Empty, inLanguage);
              string_builder.Append(inTeam.nationality.localisedNationality);
              return;
            case 2:
              StringVariableParser.nationalityGender = StringVariableParser.GetWordGender((object) inTeam, string.Empty, inLanguage);
              string_builder.Append(GameUtility.ChangeFirstCharToUpperCase(inTeam.nationality.localisedNationality));
              return;
            case 3:
              StringVariableParser.nationalityGender = StringVariableParser.GetWordGender((object) inTeam, string.Empty, inLanguage);
              string_builder.Append(inTeam.nationality.localisedCountry);
              return;
            case 4:
              StringVariableParser.GetPersonSpecialData(inTeam.contractManager.GetPersonOnJob(Contract.Job.TeamPrincipal), "Name", inLanguage, ref string_builder);
              return;
            case 5:
              StringVariableParser.GetPersonSpecialData(inTeam.contractManager.GetPersonOnJob(Contract.Job.EngineerLead), "Name", inLanguage, ref string_builder);
              return;
            case 6:
              StringVariableParser.GetPersonSpecialData(inTeam.contractManager.GetPersonOnJob(Contract.Job.Chairman), "Name", inLanguage, ref string_builder);
              return;
            case 7:
              StringVariableParser.GetPersonSpecialData((Person) inTeam.GetDriver(0), "Name", inLanguage, ref string_builder);
              return;
            case 8:
              StringVariableParser.GetPersonSpecialData((Person) inTeam.GetDriver(1), "Name", inLanguage, ref string_builder);
              return;
            case 9:
              StringVariableParser.GetPersonSpecialData((Person) inTeam.GetDriver(RandomUtility.GetRandomInc(0, 2)), "Name", inLanguage, ref string_builder);
              return;
            case 10:
              string_builder.Append(Localisation.LocaliseID(inTeam.locationID, inLanguage, (GameObject) null, string.Empty));
              return;
            case 11:
              string_builder.Append(GameUtility.FormatForPosition(inTeam.GetExpectedChampionshipResult(), inLanguage));
              return;
            case 12:
              string_builder.Append(GameUtility.FormatForPosition(inTeam.expectedRacePosition, inLanguage));
              return;
            case 13:
              string_builder.Append(inTeam.championship.standings.GetEntry((Entity) inTeam).GetCurrentPoints());
              return;
            case 14:
              string_builder.Append(inTeam.championship.GetChampionshipName(false));
              return;
            case 15:
              string_builder.Append(inTeam.championship.GetTheChampionshipString());
              return;
            case 16:
              string_builder.Append(inTeam.championship.GetTheChampionshipUppercaseString());
              return;
            case 17:
              string_builder.Append(inTeam.championship.GetAcronym(false));
              return;
            case 18:
              string_builder.Append(GameUtility.FormatForPosition(inTeam.GetChampionshipEntry().GetCurrentChampionshipPosition(), inLanguage));
              return;
            case 19:
              string_builder.Append(GameUtility.FormatForPosition(inTeam.championship.GetPreviousEventDetails().results.GetResultsForSession(SessionDetails.SessionType.Race).GetBestPlayerDriverResult().position, inLanguage));
              return;
            case 20:
              string_builder.Append(GameUtility.FormatForPosition(inTeam.championship.GetPreviousEventDetails().results.GetResultsForSession(SessionDetails.SessionType.Qualifying).GetBestPlayerDriverResult().position, inLanguage));
              return;
            case 21:
              string_builder.Append(GameUtility.FormatForPosition(inTeam.championship.GetPreviousEventDetails().results.GetResultsForSession(SessionDetails.SessionType.Practice).GetBestPlayerDriverResult().position, inLanguage));
              return;
            case 22:
              string_builder.Append(inTeam.GetTeamNameColoured());
              return;
            case 23:
              string_builder.Append(GameUtility.FormatForPosition(inTeam.chairman.playerChosenExpectedTeamChampionshipPosition, inLanguage));
              return;
            case 24:
              string_builder.Append(inTeam.GetTeamTotalMarketability().ToString("P0", (IFormatProvider) Localisation.numberFormatter));
              return;
            case 25:
              string_builder.Append(inTeam.twitterHandle);
              return;
          }
        }
      }
    }
    catch (Exception ex)
    {
      Debug.LogError((object) string.Format("Error with string: {0} \n {1}", (object) inData, (object) ex.Message), (UnityEngine.Object) null);
    }
    if (inLanguage == Localisation.currentLanguage)
      Debug.LogError((object) ("Team: {" + inData + "} -- Does not have a return case --"), (UnityEngine.Object) null);
    string_builder.Append("{");
    string_builder.Append(inData);
    string_builder.Append("}");
  }

  private static void GetSponsorSpecialData(Sponsor inSponsor, string inData, string inLanguage, ref StringBuilder string_builder)
  {
    try
    {
      string key = inData;
      if (key != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (StringVariableParser.\u003C\u003Ef__switch\u0024map2C == null)
        {
          // ISSUE: reference to a compiler-generated field
          StringVariableParser.\u003C\u003Ef__switch\u0024map2C = new Dictionary<string, int>(9)
          {
            {
              "Name",
              0
            },
            {
              "Nationality",
              1
            },
            {
              "UpperCaseNationality",
              2
            },
            {
              "OfferUpfront",
              3
            },
            {
              "OfferBonus",
              4
            },
            {
              "OfferPerRace",
              5
            },
            {
              "DealWorth",
              6
            },
            {
              "DealTime",
              7
            },
            {
              "BonusDifficulty",
              8
            }
          };
        }
        int num;
        // ISSUE: reference to a compiler-generated field
        if (StringVariableParser.\u003C\u003Ef__switch\u0024map2C.TryGetValue(key, out num))
        {
          switch (num)
          {
            case 0:
              string_builder.Append(inSponsor.name);
              return;
            case 1:
              string_builder.Append(inSponsor.nationality.localisedCountry);
              return;
            case 2:
              string_builder.Append(GameUtility.ChangeFirstCharToUpperCase(inSponsor.nationality.localisedCountry));
              return;
            case 3:
              string_builder.Append(GameUtility.GetCurrencyString((long) inSponsor.upfrontValue, 0));
              return;
            case 4:
              string_builder.Append(GameUtility.GetCurrencyString((long) inSponsor.totalBonusAmount, 0));
              return;
            case 5:
              string_builder.Append(GameUtility.GetCurrencyString((long) inSponsor.perRacePayment, 0));
              return;
            case 6:
              string_builder.Append(GameUtility.GetCurrencyString((long) inSponsor.totalBonusAmount, 0));
              return;
            case 7:
              string_builder.Append(inSponsor.offerTimer.ToString());
              return;
            case 8:
              string_builder.Append(Localisation.LocaliseEnum((Enum) inSponsor.difficulty));
              return;
          }
        }
      }
    }
    catch (Exception ex)
    {
      Debug.LogError((object) string.Format("Error with string: {0} \n {1}", (object) inData, (object) ex.Message), (UnityEngine.Object) null);
    }
    if (inLanguage == Localisation.currentLanguage)
      Debug.LogError((object) ("Sponsor: {" + inData + "} -- Does not have a return case --"), (UnityEngine.Object) null);
    string_builder.Append("{");
    string_builder.Append(inData);
    string_builder.Append("}");
  }

  private static void GetCarPartSpecialData(CarPart inCarPart, string inData, string inLanguage, ref StringBuilder string_builder)
  {
    try
    {
      string key = inData;
      if (key != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (StringVariableParser.\u003C\u003Ef__switch\u0024map2D == null)
        {
          // ISSUE: reference to a compiler-generated field
          StringVariableParser.\u003C\u003Ef__switch\u0024map2D = new Dictionary<string, int>(4)
          {
            {
              "Name",
              0
            },
            {
              "PerformanceColoured",
              1
            },
            {
              "MaxPerformanceColoured",
              2
            },
            {
              "ReliabilityColoured",
              3
            }
          };
        }
        int num;
        // ISSUE: reference to a compiler-generated field
        if (StringVariableParser.\u003C\u003Ef__switch\u0024map2D.TryGetValue(key, out num))
        {
          switch (num)
          {
            case 0:
              string_builder.Append(Localisation.LocaliseEnum((Enum) inCarPart.GetPartType()));
              return;
            case 1:
              string_builder.Append(GameUtility.ColorToRichTextHex(Color.black) + Mathf.RoundToInt(inCarPart.stats.statWithPerformance).ToString() + "</color>");
              return;
            case 2:
              string_builder.Append(GameUtility.ColorToRichTextHex(Color.black) + Mathf.RoundToInt(inCarPart.stats.stat + inCarPart.stats.maxPerformance).ToString() + "</color>");
              return;
            case 3:
              string_builder.Append(GameUtility.ColorToRichTextHex(Color.black) + inCarPart.stats.reliability.ToString("P0") + "</color>");
              return;
          }
        }
      }
    }
    catch (Exception ex)
    {
      Debug.LogError((object) string.Format("Error with string: {0} \n {1}", (object) inData, (object) ex.Message), (UnityEngine.Object) null);
    }
    if (inLanguage == Localisation.currentLanguage)
      Debug.LogError((object) ("Building: {" + inData + "} -- Does not have a return case --"), (UnityEngine.Object) null);
    string_builder.Append("{");
    string_builder.Append(inData);
    string_builder.Append("}");
  }

  private static void GetCircuitSpecialData(Circuit inCircuit, string inData, string inLanguage, ref StringBuilder string_builder)
  {
    try
    {
      string key = inData;
      if (key != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (StringVariableParser.\u003C\u003Ef__switch\u0024map2E == null)
        {
          // ISSUE: reference to a compiler-generated field
          StringVariableParser.\u003C\u003Ef__switch\u0024map2E = new Dictionary<string, int>(16)
          {
            {
              "No Special Data",
              0
            },
            {
              "Location",
              0
            },
            {
              "Name",
              0
            },
            {
              "Nationality",
              1
            },
            {
              "UpperCaseNationality",
              2
            },
            {
              "Layout",
              3
            },
            {
              "Country",
              4
            },
            {
              "Laps",
              5
            },
            {
              "LapLength",
              6
            },
            {
              "LapRecord",
              7
            },
            {
              "LapRecordHolder",
              8
            },
            {
              "Compound1",
              9
            },
            {
              "Compound2",
              10
            },
            {
              "Compound3",
              11
            },
            {
              "DaysToGo",
              12
            },
            {
              "ChanceOfRain",
              13
            }
          };
        }
        int num;
        // ISSUE: reference to a compiler-generated field
        if (StringVariableParser.\u003C\u003Ef__switch\u0024map2E.TryGetValue(key, out num))
        {
          switch (num)
          {
            case 0:
              string_builder.Append(Localisation.LocaliseID(inCircuit.locationNameID, (GameObject) null));
              return;
            case 1:
              StringVariableParser.nationalityGender = App.instance.genderTable.GetGenderForLanguage(inCircuit.locationNameID, inLanguage);
              string_builder.Append(inCircuit.nationality.localisedNationality);
              return;
            case 2:
              string_builder.Append(GameUtility.ChangeFirstCharToUpperCase(inCircuit.nationality.localisedNationality));
              return;
            case 3:
              string_builder.Append(Localisation.LocaliseEnum((Enum) inCircuit.trackLayout));
              return;
            case 4:
              string_builder.Append(Localisation.LocaliseID(inCircuit.countryNameID, (GameObject) null));
              return;
            case 5:
              string_builder.Append(DesignDataManager.CalculateRaceLapCount(Game.instance.player.team.championship, inCircuit.trackLengthMiles, false).ToString());
              return;
            case 6:
              string_builder.Append(inCircuit.trackLengthMiles.ToString((IFormatProvider) Localisation.numberFormatter));
              return;
            case 7:
              string_builder.Append(GameUtility.GetLapTimeText(inCircuit.fastestLapData.Value, false));
              return;
            case 8:
              string_builder.Append(inCircuit.fastestLapData.Key.name);
              return;
            case 9:
              string_builder.Append(Localisation.LocaliseEnum((Enum) inCircuit.firstTyreOption));
              return;
            case 10:
              string_builder.Append(Localisation.LocaliseEnum((Enum) inCircuit.secondTyreOption));
              return;
            case 11:
              string_builder.Append(Localisation.LocaliseEnum((Enum) inCircuit.thirdTyreOption));
              return;
            case 12:
              string_builder.Append("12");
              return;
            case 13:
              string_builder.Append((inCircuit.climate.GetCloudyChance(5) * 100f).ToString((IFormatProvider) Localisation.numberFormatter));
              return;
          }
        }
      }
    }
    catch (Exception ex)
    {
      Debug.LogError((object) string.Format("Error with string: {0} \n {1}", (object) inData, (object) ex.Message), (UnityEngine.Object) null);
    }
    if (inLanguage == Localisation.currentLanguage)
      Debug.LogError((object) ("Track: {" + inData + "} -- Does not have a return case --"), (UnityEngine.Object) null);
    string_builder.Append("{");
    string_builder.Append(inData);
    string_builder.Append("}");
  }

  private static void GetChampionshipSpecialData(Championship inChampionship, string inData, string inLanguage, ref StringBuilder string_builder)
  {
    try
    {
      string key = inData;
      if (key != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (StringVariableParser.\u003C\u003Ef__switch\u0024map2F == null)
        {
          // ISSUE: reference to a compiler-generated field
          StringVariableParser.\u003C\u003Ef__switch\u0024map2F = new Dictionary<string, int>(6)
          {
            {
              "TheChampionship",
              0
            },
            {
              "Acronym",
              1
            },
            {
              "Name",
              2
            },
            {
              "RacesFromStartOfSeason",
              3
            },
            {
              "RacesToEndOfSeason",
              4
            },
            {
              "PromotedTeam",
              5
            }
          };
        }
        int num;
        // ISSUE: reference to a compiler-generated field
        if (StringVariableParser.\u003C\u003Ef__switch\u0024map2F.TryGetValue(key, out num))
        {
          switch (num)
          {
            case 0:
              string_builder.Append(inChampionship.GetTheChampionshipString());
              return;
            case 1:
              string_builder.Append(inChampionship.GetAcronym(false));
              return;
            case 2:
              string_builder.Append(inChampionship.GetChampionshipName(false));
              return;
            case 3:
              string_builder.Append(inChampionship.eventNumberForUI.ToString());
              return;
            case 4:
              string_builder.Append(inChampionship.eventsLeft.ToString());
              return;
            case 5:
              string_builder.Append(inChampionship.InPromotedTeamFromLowerTier.team.name);
              return;
          }
        }
      }
    }
    catch (Exception ex)
    {
      Debug.LogError((object) string.Format("Error with string: {0} \n {1}", (object) inData, (object) ex.Message), (UnityEngine.Object) null);
    }
    if (inLanguage == Localisation.currentLanguage)
      Debug.LogError((object) ("Championship: {" + inData + "} -- Does not have a return case --"), (UnityEngine.Object) null);
    string_builder.Append("{");
    string_builder.Append(inData);
    string_builder.Append("}");
  }

  private static void GetBuildingSpecialData(HQsBuilding_v1 inBuilding, string inData, string inLanguage, ref StringBuilder string_builder)
  {
    try
    {
      string key = inData;
      if (key != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (StringVariableParser.\u003C\u003Ef__switch\u0024map30 == null)
        {
          // ISSUE: reference to a compiler-generated field
          StringVariableParser.\u003C\u003Ef__switch\u0024map30 = new Dictionary<string, int>(11)
          {
            {
              "Name",
              0
            },
            {
              "Cost",
              1
            },
            {
              "BuildCost",
              2
            },
            {
              "PreviousUpgradeCost",
              3
            },
            {
              "UpgradeCost",
              4
            },
            {
              "BuildTime",
              5
            },
            {
              "PreviousUpgradeTime",
              6
            },
            {
              "UpgradeTime",
              7
            },
            {
              "Level",
              8
            },
            {
              "NextLevel",
              9
            },
            {
              "MaxLevel",
              10
            }
          };
        }
        int num;
        // ISSUE: reference to a compiler-generated field
        if (StringVariableParser.\u003C\u003Ef__switch\u0024map30.TryGetValue(key, out num))
        {
          switch (num)
          {
            case 0:
              string_builder.Append(inBuilding.buildingName);
              return;
            case 1:
              string_builder.Append(GameUtility.GetCurrencyString((long) inBuilding.info.initialCost, 0));
              return;
            case 2:
              string_builder.Append(GameUtility.GetCurrencyString((long) inBuilding.costs.GetBuildTotalCost(), 0));
              return;
            case 3:
              string_builder.Append(GameUtility.GetCurrencyString((long) inBuilding.costs.GetUpgradeCostLevel(inBuilding.currentLevel - 1), 0));
              return;
            case 4:
              string_builder.Append(GameUtility.GetCurrencyString((long) inBuilding.costs.GetUpgradeCost(), 0));
              return;
            case 5:
              string_builder.Append(inBuilding.GetBuildTime().ToString());
              return;
            case 6:
              string_builder.Append(inBuilding.GetUpgradeTimeLevel(inBuilding.currentLevel - 1).ToString());
              return;
            case 7:
              string_builder.Append(inBuilding.GetUpgradeTime().ToString());
              return;
            case 8:
              string_builder.Append(inBuilding.currentLevelUI.ToString());
              return;
            case 9:
              string_builder.Append(inBuilding.nextLevelUI.ToString());
              return;
            case 10:
              string_builder.Append(inBuilding.maxLevelUI.ToString());
              return;
          }
        }
      }
    }
    catch (Exception ex)
    {
      Debug.LogError((object) string.Format("Error with string: {0} \n {1}", (object) inData, (object) ex.Message), (UnityEngine.Object) null);
    }
    if (inLanguage == Localisation.currentLanguage)
      Debug.LogError((object) ("Building: {" + inData + "} -- Does not have a return case --"), (UnityEngine.Object) null);
    string_builder.Append("{");
    string_builder.Append(inData);
    string_builder.Append("}");
  }

  public static string GetWordGender(object inObject, string inGenderTarget, string inLanguage)
  {
    if (inObject is Circuit)
    {
      Circuit circuit = inObject as Circuit;
      return App.instance.genderTable.GetGenderForLanguage(circuit.locationNameID, inLanguage);
    }
    if (inObject is Team)
    {
      Team team = inObject as Team;
      return App.instance.genderTable.GetGenderForLanguage(string.Format("Team_{0}", (object) team.teamID), inLanguage);
    }
    if (inObject is HQsBuilding_v1)
    {
      HQsBuilding_v1 hqsBuildingV1 = inObject as HQsBuilding_v1;
      return App.instance.genderTable.GetGenderForLanguage(hqsBuildingV1.info.GetNameID(), inLanguage);
    }
    if (!(inObject is Championship))
      return string.Empty;
    Championship championship = inObject as Championship;
    if (inGenderTarget == "Championship")
      return App.instance.genderTable.GetGenderForLanguage(string.Format("Championship_{0}", (object) championship.championshipID), inLanguage);
    return App.instance.genderTable.GetGenderForLanguage(string.Format("ChampionshipAcronym_{0}", (object) championship.championshipID), inLanguage);
  }

  public static string GetWordGender(string inString, string inLanguage)
  {
    string key = inString;
    if (key != null)
    {
      // ISSUE: reference to a compiler-generated field
      if (StringVariableParser.\u003C\u003Ef__switch\u0024map31 == null)
      {
        // ISSUE: reference to a compiler-generated field
        StringVariableParser.\u003C\u003Ef__switch\u0024map31 = new Dictionary<string, int>(10)
        {
          {
            "Championship",
            0
          },
          {
            "TheChampionship",
            0
          },
          {
            "ChampionshipAcronym",
            0
          },
          {
            "TheChampionshipBelow",
            1
          },
          {
            "TheChampionshipAbove",
            2
          },
          {
            "CurrentWeakestPart",
            3
          },
          {
            "ISCarLowQualityPart",
            4
          },
          {
            "Part",
            5
          },
          {
            "NationalityGender",
            6
          },
          {
            "VoteTopic",
            7
          }
        };
      }
      int num;
      // ISSUE: reference to a compiler-generated field
      if (StringVariableParser.\u003C\u003Ef__switch\u0024map31.TryGetValue(key, out num))
      {
        switch (num)
        {
          case 0:
            return StringVariableParser.GetWordGender((object) Game.instance.player.team.championship, inString, inLanguage);
          case 1:
            return StringVariableParser.GetWordGender((object) Game.instance.championshipManager.GetChampionshipByID(Game.instance.player.team.championship.championshipBelowID), inString, inLanguage);
          case 2:
            return StringVariableParser.GetWordGender((object) Game.instance.championshipManager.GetChampionshipByID(Game.instance.player.team.championship.championshipBelowID), inString, inLanguage);
          case 3:
            return App.instance.genderTable.GetGenderForLanguage(StringVariableParser.weakestPart.ToString(), inLanguage);
          case 4:
            return App.instance.genderTable.GetGenderForLanguage(TeamStatistics.GetLowQualityPart(Game.instance.player.team.championship).ToString(), inLanguage);
          case 5:
            return App.instance.genderTable.GetGenderForLanguage(StringVariableParser.partFrontendUI.ToString(), inLanguage);
          case 6:
            return StringVariableParser.nationalityGender;
          case 7:
            return App.instance.genderTable.GetGenderForLanguage(Game.instance.player.team.championship.politicalSystem.activeVote.nameID, inLanguage);
        }
      }
    }
    return string.Empty;
  }

  public static object GetObject(string inString)
  {
    if (inString == string.Empty)
      return (object) null;
    try
    {
      inString = inString.Trim('{', '}', ' ');
      if ((int) inString[0] == 35)
      {
        int index1 = 0;
        StringVariableParser.GetHashSplitVariables(inString, out index1, out inString);
        string key = inString.Split(':')[0];
        if (key != null)
        {
          // ISSUE: reference to a compiler-generated field
          if (StringVariableParser.\u003C\u003Ef__switch\u0024map32 == null)
          {
            // ISSUE: reference to a compiler-generated field
            StringVariableParser.\u003C\u003Ef__switch\u0024map32 = new Dictionary<string, int>(21)
            {
              {
                "GridPlace",
                0
              },
              {
                "Place",
                1
              },
              {
                "PlaceDriver",
                1
              },
              {
                "SessionPlaceDriver",
                1
              },
              {
                "PreviousRace",
                2
              },
              {
                "PreSeason",
                3
              },
              {
                "PlaceTeam",
                4
              },
              {
                "PlacePlayerDriver",
                5
              },
              {
                "PlayerDriver",
                6
              },
              {
                "SessionPlacePlayerDriver",
                7
              },
              {
                "DC",
                8
              },
              {
                "DCTeam",
                9
              },
              {
                "PlaceManager",
                10
              },
              {
                "TC",
                11
              },
              {
                "TCPlace",
                11
              },
              {
                "TCPlaceRival",
                12
              },
              {
                "TCPlaceChairman",
                13
              },
              {
                "TCPlaceDriver1",
                14
              },
              {
                "TCPlaceDriver2",
                15
              },
              {
                "Tier2Team",
                16
              },
              {
                "Tier1Team",
                17
              }
            };
          }
          int num;
          // ISSUE: reference to a compiler-generated field
          if (StringVariableParser.\u003C\u003Ef__switch\u0024map32.TryGetValue(key, out num))
          {
            switch (num)
            {
              case 0:
                SessionDetails.SessionType sessionTypeWithData = Game.instance.sessionManager.eventDetails.results.GetLastestSessionTypeWithData();
                RaceEventResults.SessonResultData resultsForSession1 = Game.instance.sessionManager.eventDetails.results.GetResultsForSession(sessionTypeWithData);
                for (int index2 = 0; index2 < resultsForSession1.resultData.Count; ++index2)
                {
                  if (resultsForSession1.resultData[index2].gridPosition == index1 + 1)
                    return (object) resultsForSession1.resultData[index2].driver;
                }
                return (object) null;
              case 1:
                return (object) Game.instance.sessionManager.standings[index1].driver;
              case 2:
                RaceEventResults.SessonResultData resultsForSession2 = Game.instance.player.team.championship.GetPreviousEventDetails().results.GetResultsForSession(SessionDetails.SessionType.Race);
                for (int index2 = 0; index2 < resultsForSession2.resultData.Count; ++index2)
                {
                  if (resultsForSession2.resultData[index2].position == index1 + 1)
                    return (object) resultsForSession2.resultData[index2].driver;
                }
                return (object) null;
              case 3:
                return (object) Game.instance.player.team.championship.preSeasonTesting.GetTestingData(index1).driver;
              case 4:
                return (object) Game.instance.sessionManager.standings[index1].driver.contract.GetTeam();
              case 5:
                Driver driver1 = Game.instance.player.team.GetDriver(0);
                Driver driver2 = Game.instance.player.team.GetDriver(1);
                if (driver1.GetChampionshipEntry().GetCurrentPoints() > driver2.GetChampionshipEntry().GetCurrentPoints())
                {
                  if (index1 == 0)
                    return (object) driver1;
                  return (object) driver2;
                }
                if (index1 == 0)
                  return (object) driver2;
                return (object) driver1;
              case 6:
                Driver driver3 = Game.instance.player.team.GetDriver(0);
                Driver driver4 = Game.instance.player.team.GetDriver(1);
                int inEventNumber = driver3.contract.GetTeam().championship.eventNumber - 1;
                if (driver3.GetChampionshipEntry().GetPositionForEvent(inEventNumber) < driver4.GetChampionshipEntry().GetPositionForEvent(inEventNumber))
                {
                  if (index1 == 0)
                    return (object) driver3;
                  return (object) driver4;
                }
                if (index1 == 0)
                  return (object) driver4;
                return (object) driver3;
              case 7:
                Driver selectedDriver1 = Game.instance.player.team.GetSelectedDriver(0);
                Driver selectedDriver2 = Game.instance.player.team.GetSelectedDriver(1);
                if (Game.instance.vehicleManager.GetVehicle(selectedDriver1).standingsPosition < Game.instance.vehicleManager.GetVehicle(selectedDriver2).standingsPosition)
                {
                  if (index1 == 0)
                    return (object) selectedDriver1;
                  return (object) selectedDriver2;
                }
                if (index1 == 0)
                  return (object) selectedDriver2;
                return (object) selectedDriver1;
              case 8:
                return (object) Game.instance.player.team.championship.standings.GetDriverEntry(index1).GetEntity<Driver>();
              case 9:
                return (object) Game.instance.player.team.championship.standings.GetDriverEntry(index1).GetEntity<Driver>().contract.GetTeam();
              case 10:
                return (object) Game.instance.sessionManager.standings[index1].driver.contract.GetTeam().contractManager.GetPersonOnJob(Contract.Job.TeamPrincipal);
              case 11:
                return (object) Game.instance.player.team.championship.standings.GetTeamEntry(index1).GetEntity<Team>();
              case 12:
                return (object) Game.instance.player.team.championship.standings.GetTeamEntry(index1).GetEntity<Team>().rivalTeam;
              case 13:
                return (object) Game.instance.player.team.championship.standings.GetTeamEntry(index1).GetEntity<Team>().chairman;
              case 14:
                return (object) Game.instance.player.team.championship.standings.GetTeamEntry(index1).GetEntity<Team>().GetDriver(0);
              case 15:
                return (object) Game.instance.player.team.championship.standings.GetTeamEntry(index1).GetEntity<Team>().GetDriver(1);
              case 16:
                return (object) Game.instance.championshipManager.GetChampionshipsByOrder(Championship.Series.SingleSeaterSeries)[1].standings.GetTeamEntry(index1).GetEntity<Team>();
              case 17:
                return (object) Game.instance.championshipManager.GetChampionshipsByOrder(Championship.Series.SingleSeaterSeries)[0].standings.GetTeamEntry(index1).GetEntity<Team>();
            }
          }
        }
        return (object) null;
      }
      string key1 = inString.Split(':')[0];
      if (key1 != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (StringVariableParser.\u003C\u003Ef__switch\u0024map33 == null)
        {
          // ISSUE: reference to a compiler-generated field
          StringVariableParser.\u003C\u003Ef__switch\u0024map33 = new Dictionary<string, int>(155)
          {
            {
              "FavouriteTeam",
              0
            },
            {
              "FavouriteDriver",
              1
            },
            {
              "ScoutingTarget",
              2
            },
            {
              "CalendarPerson",
              3
            },
            {
              "Sender",
              3
            },
            {
              "RandomDriver",
              4
            },
            {
              "RandomSessionDriver",
              5
            },
            {
              "FiredDriver",
              6
            },
            {
              "RandomPlayerDriver",
              7
            },
            {
              "RandomPlayerSessionDriver",
              8
            },
            {
              "RandomJournalist",
              9
            },
            {
              "Driver1",
              10
            },
            {
              "PlayerDriver1",
              10
            },
            {
              "Driver2",
              11
            },
            {
              "PlayerDriver2",
              11
            },
            {
              "Driver3",
              12
            },
            {
              "PlayerDriver3",
              12
            },
            {
              "DriverReserve",
              12
            },
            {
              "PlayerDriverReserve",
              12
            },
            {
              "Interviewer",
              13
            },
            {
              "Player",
              14
            },
            {
              "PlayerChairman",
              15
            },
            {
              "PlayerTeamAssistant",
              16
            },
            {
              "PlayerEngineer",
              17
            },
            {
              "OtherPlayerDriver",
              18
            },
            {
              "OtherDriver",
              18
            },
            {
              "Teammate",
              18
            },
            {
              "SubjectManager",
              19
            },
            {
              "SubjectTeamManager",
              19
            },
            {
              "InterviewSubject",
              20
            },
            {
              "Subject",
              21
            },
            {
              "SubjectTeam",
              22
            },
            {
              "SubjectPreviousTeam",
              23
            },
            {
              "RumourTeamManager",
              24
            },
            {
              "RumourDriver",
              25
            },
            {
              "RumourTeam",
              26
            },
            {
              "SubjectTeamPersonReplaced",
              27
            },
            {
              "SubjectDriverMechanicFor",
              28
            },
            {
              "ExManager",
              29
            },
            {
              "ExTeamPrincipal",
              29
            },
            {
              "ExDriver",
              30
            },
            {
              "ExEngineer",
              31
            },
            {
              "ExMechanic",
              32
            },
            {
              "TechnicalExpert",
              33
            },
            {
              "President",
              34
            },
            {
              "Tier2Champions",
              35
            },
            {
              "Tier1RelegationCandidates",
              36
            },
            {
              "T1Championship",
              37
            },
            {
              "T2Championship",
              38
            },
            {
              "T3Championship",
              39
            },
            {
              "T1PromotedTeam",
              40
            },
            {
              "T2PromotedTeam",
              41
            },
            {
              "T3PromotedTeam",
              42
            },
            {
              "T1RelegatedTeam",
              43
            },
            {
              "T2RelegatedTeam",
              44
            },
            {
              "T3RelegatedTeam",
              45
            },
            {
              "T1ChampionshipGT",
              46
            },
            {
              "T2ChampionshipGT",
              47
            },
            {
              "T2PromotedTeamGT",
              48
            },
            {
              "T1RelegatedTeamGT",
              49
            },
            {
              "Chairman",
              50
            },
            {
              "RandomTeam",
              51
            },
            {
              "CalendarChampionship",
              52
            },
            {
              "RandomChampionship",
              52
            },
            {
              "RandomSessionTeam",
              53
            },
            {
              "RandomNonPlayerTeamPerson",
              54
            },
            {
              "PlayerTeamRival",
              55
            },
            {
              "Rival",
              56
            },
            {
              "PlayerTeam",
              57
            },
            {
              "HighestVotePowerManager",
              58
            },
            {
              "HighestVotePowerManagerTeam",
              59
            },
            {
              "ChallengedTeam",
              60
            },
            {
              "BenefittedTeam",
              61
            },
            {
              "UnderPressureManager",
              62
            },
            {
              "UnderPressureManagerTeam",
              63
            },
            {
              "DriverWithBadCar",
              64
            },
            {
              "DriverOfSeason",
              65
            },
            {
              "ManagerOfSeason",
              66
            },
            {
              "SelectedDriver1",
              67
            },
            {
              "SelectedDriver2",
              68
            },
            {
              "Driver1Mechanic",
              69
            },
            {
              "Driver2Mechanic",
              70
            },
            {
              "AnyDriver",
              71
            },
            {
              "Assistant",
              72
            },
            {
              "Engineer",
              73
            },
            {
              "MediaPerson",
              74
            },
            {
              "Sponsor",
              75
            },
            {
              "SponsorLiaison",
              76
            },
            {
              "Mechanic",
              77
            },
            {
              "Chief Scout",
              78
            },
            {
              "Scout",
              78
            },
            {
              "IMAPresident",
              79
            },
            {
              "Fan",
              80
            },
            {
              "RandomMale",
              80
            },
            {
              "BestEngineTeam",
              81
            },
            {
              "BestGearboxTeam",
              82
            },
            {
              "BestBrakesTeam",
              83
            },
            {
              "BestSuspensionTeam",
              84
            },
            {
              "BestFrontWingTeam",
              85
            },
            {
              "BestRearWingTeam",
              86
            },
            {
              "WorstEngineTeam",
              87
            },
            {
              "WorstGearboxTeam",
              88
            },
            {
              "WorstBrakesTeam",
              89
            },
            {
              "WorstSuspensionTeam",
              90
            },
            {
              "WorstFrontWingTeam",
              91
            },
            {
              "WorstRearWingTeam",
              92
            },
            {
              "BuildingTeam",
              93
            },
            {
              "HQsBuilding",
              94
            },
            {
              "HQBuilding",
              94
            },
            {
              "Building",
              94
            },
            {
              "TrackSuitsTeam",
              95
            },
            {
              "TrackDoesNotSuitTeam",
              96
            },
            {
              "LastRaceWinner",
              97
            },
            {
              "BirthdayDriver",
              98
            },
            {
              "RandomMechanic",
              99
            },
            {
              "BestPartTeam",
              100
            },
            {
              "TeamAppliedTo",
              101
            },
            {
              "Championship",
              102
            },
            {
              "BigCrashLocation",
              103
            },
            {
              "CurrentTrack",
              104
            },
            {
              "CurrentCircuit",
              104
            },
            {
              "PoleDriver",
              105
            },
            {
              "PreviousRace",
              106
            },
            {
              "NextRace",
              107
            },
            {
              "FinalRaceLocation",
              108
            },
            {
              "FinalRace",
              108
            },
            {
              "SubjectFinalRaceLocation",
              109
            },
            {
              "TCLastSeasonChamps",
              110
            },
            {
              "DCLastSeasonChamp",
              111
            },
            {
              "RuleOldTrack",
              112
            },
            {
              "CalendarCircuit",
              113
            },
            {
              "RuleNewTrack",
              113
            },
            {
              "OvertakeVictim",
              114
            },
            {
              "ImpactVictim",
              115
            },
            {
              "AttackingDriver",
              116
            },
            {
              "TeamOrderVictim",
              117
            },
            {
              "CarPart",
              118
            },
            {
              "ContractNegotiationScreenPerson",
              119
            },
            {
              "DriverScreenDriver",
              120
            },
            {
              "DriverScreenDriverMechanic",
              121
            },
            {
              "SeriesResultsDriverScreenDriver",
              122
            },
            {
              "SeriesResultsDriverScreenTeamPrincipal",
              123
            },
            {
              "SeriesResultsTeamScreenTeamPrincipal",
              124
            },
            {
              "StaffDetailsScreenPerson",
              125
            },
            {
              "TeamScreenPrincipal",
              126
            },
            {
              "TeamScreenChairman",
              (int) sbyte.MaxValue
            },
            {
              "TeamScreenEngineer",
              128
            },
            {
              "Location",
              129
            },
            {
              "RandomFemale",
              130
            },
            {
              "Circuit",
              131
            },
            {
              "AIDriverHadAwfulRace",
              132
            },
            {
              "DriverTransferRumour",
              133
            },
            {
              "PlayerDriverPerformingBadly",
              134
            },
            {
              "CrashVictim",
              135
            },
            {
              "CrashDriver",
              136
            }
          };
        }
        int num1;
        // ISSUE: reference to a compiler-generated field
        if (StringVariableParser.\u003C\u003Ef__switch\u0024map33.TryGetValue(key1, out num1))
        {
          switch (num1)
          {
            case 0:
              return (object) TeamManager.GetTeamWithBestExpectedRaceResult(Game.instance.player.team.championship);
            case 1:
              return (object) DriverManager.GetDriverWithBestExpectedRaceResult(Game.instance.player.team.championship);
            case 2:
              return (object) StringVariableParser.subject;
            case 3:
              return (object) StringVariableParser.sender;
            case 4:
              return (object) StringVariableParser.randomDriver;
            case 5:
              return (object) StringVariableParser.randomSessionDriver;
            case 6:
              return (object) Game.instance.player.team.contractManager.latestFiredActiveDriver;
            case 7:
              return (object) StringVariableParser.randomPlayerDriver;
            case 8:
              return (object) StringVariableParser.randomPlayerSessionDriver;
            case 9:
              return (object) StringVariableParser.randomJournalist;
            case 10:
              return (object) Game.instance.player.team.GetDriver(0);
            case 11:
              return (object) Game.instance.player.team.GetDriver(1);
            case 12:
              return (object) Game.instance.player.team.GetDriver(2);
            case 13:
              return (object) UIManager.instance.dialogBoxManager.GetDialog<MediaInterviewDialog>().journalist;
            case 14:
              return (object) Game.instance.player;
            case 15:
              return (object) Game.instance.player.team.contractManager.GetPersonOnJob(Contract.Job.Chairman);
            case 16:
              return (object) Game.instance.player.team.contractManager.GetPersonOnJob(Contract.Job.TeamAssistant);
            case 17:
              return (object) Game.instance.player.team.contractManager.GetPersonOnJob(Contract.Job.EngineerLead);
            case 18:
              return (object) StringVariableParser.otherDriver;
            case 19:
              return (object) StringVariableParser.subject.contract.GetTeam().contractManager.GetPersonOnJob(Contract.Job.TeamPrincipal);
            case 20:
              return (object) StringVariableParser.interviewSubject;
            case 21:
              return (object) StringVariableParser.subject;
            case 22:
              return (object) StringVariableParser.subject.contract.GetTeam();
            case 23:
              if (StringVariableParser.subjectPreviousTeam != null)
                return (object) StringVariableParser.subjectPreviousTeam;
              return (object) StringVariableParser.subject.careerHistory.previousTeam;
            case 24:
              return (object) StringVariableParser.rumourTeam.contractManager.GetPersonOnJob(Contract.Job.TeamPrincipal);
            case 25:
              return (object) StringVariableParser.rumourDriver;
            case 26:
              return (object) StringVariableParser.rumourTeam;
            case 27:
              return (object) StringVariableParser.personReplaced;
            case 28:
              Mechanic subject1 = (Mechanic) StringVariableParser.subject;
              return (object) subject1.contract.GetTeam().GetDriver(subject1.driver);
            case 29:
              return (object) StringVariableParser.exTeamPrincipal;
            case 30:
              return (object) StringVariableParser.exDriver;
            case 31:
              return (object) StringVariableParser.exEngineer;
            case 32:
              return (object) StringVariableParser.exMechanic;
            case 33:
              return (object) StringVariableParser.technicalExpert;
            case 34:
              return !Game.instance.player.IsUnemployed() ? (object) Game.instance.player.team.championship.politicalSystem.president : (object) Game.instance.championshipManager.GetChampionshipsByOrder(Championship.Series.SingleSeaterSeries)[0].politicalSystem.president;
            case 35:
              return (object) Game.instance.championshipManager.GetChampionshipsByOrder(Championship.Series.SingleSeaterSeries)[1].championshipPromotions.champion;
            case 36:
              return (object) Game.instance.championshipManager.GetChampionshipsByOrder(Championship.Series.SingleSeaterSeries)[0].championshipPromotions.lastPlace;
            case 37:
              return (object) Game.instance.championshipManager.GetChampionshipsByOrder(Championship.Series.SingleSeaterSeries)[0];
            case 38:
              return (object) Game.instance.championshipManager.GetChampionshipsByOrder(Championship.Series.SingleSeaterSeries)[1];
            case 39:
              return (object) Game.instance.championshipManager.GetChampionshipsByOrder(Championship.Series.SingleSeaterSeries)[2];
            case 40:
              return (object) Game.instance.championshipManager.GetChampionshipsByOrder(Championship.Series.SingleSeaterSeries)[0].championshipPromotions.champion;
            case 41:
              return (object) Game.instance.championshipManager.GetChampionshipsByOrder(Championship.Series.SingleSeaterSeries)[1].championshipPromotions.champion;
            case 42:
              return (object) Game.instance.championshipManager.GetChampionshipsByOrder(Championship.Series.SingleSeaterSeries)[2].championshipPromotions.champion;
            case 43:
              return (object) Game.instance.championshipManager.GetChampionshipsByOrder(Championship.Series.SingleSeaterSeries)[0].championshipPromotions.lastPlace;
            case 44:
              return (object) Game.instance.championshipManager.GetChampionshipsByOrder(Championship.Series.SingleSeaterSeries)[1].championshipPromotions.lastPlace;
            case 45:
              return (object) Game.instance.championshipManager.GetChampionshipsByOrder(Championship.Series.SingleSeaterSeries)[2].championshipPromotions.lastPlace;
            case 46:
              return (object) Game.instance.championshipManager.GetChampionshipsByOrder(Championship.Series.GTSeries)[0];
            case 47:
              return (object) Game.instance.championshipManager.GetChampionshipsByOrder(Championship.Series.GTSeries)[1];
            case 48:
              return (object) Game.instance.championshipManager.GetChampionshipsByOrder(Championship.Series.GTSeries)[1].championshipPromotions.champion;
            case 49:
              return (object) Game.instance.championshipManager.GetChampionshipsByOrder(Championship.Series.GTSeries)[0].championshipPromotions.lastPlace;
            case 50:
              if (Game.IsActive())
                return (object) Game.instance.player.team.chairman;
              return (object) new Person();
            case 51:
              return (object) StringVariableParser.randomTeam;
            case 52:
              return (object) StringVariableParser.randomChampionship;
            case 53:
              return (object) StringVariableParser.randomSessionTeam;
            case 54:
              return (object) StringVariableParser.randomNonPlayerTeamChairman;
            case 55:
              return (object) Game.instance.player.contract.GetTeam().rivalTeam;
            case 56:
              Driver subject2 = StringVariableParser.subject as Driver;
              if (subject2 != null)
              {
                if (subject2.GetRivalDriver() != null)
                  return (object) subject2.GetRivalDriver();
                goto label_253;
              }
              else
                goto label_253;
            case 57:
              return (object) Game.instance.player.contract.GetTeam();
            case 58:
              return (object) Game.instance.player.team.championship.GetTeamWithMostVotingPower().contractManager.GetPersonOnJob(Contract.Job.TeamPrincipal);
            case 59:
              return (object) Game.instance.player.team.championship.GetTeamWithMostVotingPower();
            case 60:
              Championship championship1 = Game.instance.player.team.championship;
              List<Team> teamList1 = championship1.standings.GetTeamList();
              return (object) championship1.politicalSystem.activeVote.GetMostImpactedTeam(PoliticalVote.TeamImpact.Detrimental, teamList1);
            case 61:
              Championship championship2 = Game.instance.player.team.championship;
              List<Team> teamList2 = championship2.standings.GetTeamList();
              return (object) championship2.politicalSystem.activeVote.GetMostImpactedTeam(PoliticalVote.TeamImpact.Beneficial, teamList2);
            case 62:
              return (object) TeamManager.GetTeamWithUnderPressureManager(Game.instance.player.team.championship).contractManager.GetPersonOnJob(Contract.Job.TeamPrincipal);
            case 63:
              return (object) TeamManager.GetTeamWithUnderPressureManager(Game.instance.player.team.championship);
            case 64:
              List<Car> carsOfChampionship = CarManager.GetOverralBestCarsOfChampionship(Game.instance.player.team.championship);
              Car car = carsOfChampionship[carsOfChampionship.Count - 1];
              return (object) car.carManager.team.GetDriver(car.identifier);
            case 65:
              return (object) DriverManager.GetDriverOfTheSeason(Game.instance.player.team.championship);
            case 66:
              return (object) TeamManager.GetTeamOfTheSeason(Game.instance.player.team.championship);
            case 67:
              return (object) Game.instance.player.team.GetSelectedDriver(0);
            case 68:
              return (object) Game.instance.player.team.GetSelectedDriver(1);
            case 69:
              Driver selectedDriver1 = Game.instance.player.team.GetSelectedDriver(0);
              return (object) selectedDriver1.contract.GetTeam().GetMechanicOfDriver(selectedDriver1);
            case 70:
              Driver selectedDriver2 = Game.instance.player.team.GetSelectedDriver(1);
              return (object) selectedDriver2.contract.GetTeam().GetMechanicOfDriver(selectedDriver2);
            case 71:
              if (StringVariableParser.anyDriver == null)
                StringVariableParser.anyDriver = Game.instance.player.team.GetAnyDriver();
              return (object) StringVariableParser.anyDriver;
            case 72:
              return (object) Game.instance.player.team.contractManager.GetPersonOnJob(Contract.Job.TeamAssistant);
            case 73:
              return (object) Game.instance.player.team.contractManager.GetPersonOnJob(Contract.Job.EngineerLead);
            case 74:
              return (object) Game.instance.mediaManager.GetEntity(UnityEngine.Random.Range(0, Game.instance.mediaManager.count));
            case 75:
              return (object) StringVariableParser.sponsor;
            case 76:
              return (object) StringVariableParser.sponsor.liaison;
            case 77:
              return (object) Game.instance.player.team.contractManager.GetPersonOnJob(Contract.Job.Mechanic);
            case 78:
              return (object) Game.instance.player.team.contractManager.GetPersonOnJob(Contract.Job.Scout);
            case 79:
              return (object) Game.instance.championshipManager.GetMainChampionship(Championship.Series.SingleSeaterSeries).politicalSystem.president;
            case 80:
              return (object) StringVariableParser.randomFan;
            case 81:
              return (object) TeamManager.GetTeamWithBestPartOfType(CarPart.GetPartForStatType(CarStats.StatType.TopSpeed, Game.instance.player.team.championship.series), Game.instance.player.team.championship);
            case 82:
              return (object) TeamManager.GetTeamWithBestPartOfType(CarPart.GetPartForStatType(CarStats.StatType.Acceleration, Game.instance.player.team.championship.series), Game.instance.player.team.championship);
            case 83:
              return (object) TeamManager.GetTeamWithBestPartOfType(CarPart.GetPartForStatType(CarStats.StatType.Braking, Game.instance.player.team.championship.series), Game.instance.player.team.championship);
            case 84:
              return (object) TeamManager.GetTeamWithBestPartOfType(CarPart.GetPartForStatType(CarStats.StatType.MediumSpeedCorners, Game.instance.player.team.championship.series), Game.instance.player.team.championship);
            case 85:
              return (object) TeamManager.GetTeamWithBestPartOfType(CarPart.GetPartForStatType(CarStats.StatType.LowSpeedCorners, Game.instance.player.team.championship.series), Game.instance.player.team.championship);
            case 86:
              return (object) TeamManager.GetTeamWithBestPartOfType(CarPart.GetPartForStatType(CarStats.StatType.HighSpeedCorners, Game.instance.player.team.championship.series), Game.instance.player.team.championship);
            case 87:
              return (object) TeamManager.GetTeamWithWorstPartOfType(CarPart.GetPartForStatType(CarStats.StatType.TopSpeed, Game.instance.player.team.championship.series), Game.instance.player.team.championship);
            case 88:
              return (object) TeamManager.GetTeamWithWorstPartOfType(CarPart.GetPartForStatType(CarStats.StatType.Acceleration, Game.instance.player.team.championship.series), Game.instance.player.team.championship);
            case 89:
              return (object) TeamManager.GetTeamWithWorstPartOfType(CarPart.GetPartForStatType(CarStats.StatType.Braking, Game.instance.player.team.championship.series), Game.instance.player.team.championship);
            case 90:
              return (object) TeamManager.GetTeamWithWorstPartOfType(CarPart.GetPartForStatType(CarStats.StatType.MediumSpeedCorners, Game.instance.player.team.championship.series), Game.instance.player.team.championship);
            case 91:
              return (object) TeamManager.GetTeamWithWorstPartOfType(CarPart.GetPartForStatType(CarStats.StatType.LowSpeedCorners, Game.instance.player.team.championship.series), Game.instance.player.team.championship);
            case 92:
              return (object) TeamManager.GetTeamWithWorstPartOfType(CarPart.GetPartForStatType(CarStats.StatType.HighSpeedCorners, Game.instance.player.team.championship.series), Game.instance.player.team.championship);
            case 93:
              return (object) StringVariableParser.randomTeam;
            case 94:
              return (object) StringVariableParser.building;
            case 95:
              return (object) TeamManager.GetTeamThatSuitsTrack(Game.instance.player.team.championship, Game.instance.player.team.championship.GetCurrentEventDetails().circuit);
            case 96:
              return (object) TeamManager.GetTeamThatDoesntSuitTrack(Game.instance.player.team.championship, Game.instance.player.team.championship.GetCurrentEventDetails().circuit);
            case 97:
              return (object) Game.instance.player.team.championship.GetPreviousEventDetails().results.GetResultsForSession(SessionDetails.SessionType.Race).resultData[0].driver;
            case 98:
              return (object) StringVariableParser.dilemmaBirthdayDriver;
            case 99:
              return (object) StringVariableParser.dilemmaRandomMechanic;
            case 100:
              List<Car> carStandingsOnStat = CarManager.GetCarStandingsOnStat(CarPart.GetStatForPartType(StringVariableParser.partFrontendUI), Game.instance.player.team.championship);
              Team team = Game.instance.player.team;
              for (int index = 0; index < carStandingsOnStat.Count; ++index)
              {
                if (carStandingsOnStat[index].carManager.team != team)
                  return (object) carStandingsOnStat[index].carManager.team;
              }
              return (object) carStandingsOnStat[0].carManager.team;
            case 101:
              return (object) StringVariableParser.teamAppliedTo;
            case 102:
              return (object) Game.instance.player.team.championship;
            case 103:
              return (object) StringVariableParser.traitBigCrashLocation;
            case 104:
              if (!Game.instance.sessionManager.eventDetails.hasEventEnded)
                return (object) Game.instance.sessionManager.eventDetails.circuit;
              return (object) Game.instance.player.team.championship.GetNextEventDetails().circuit;
            case 105:
              RaceEventDetails currentEventDetails = Game.instance.player.team.championship.GetCurrentEventDetails();
              RaceEventResults.SessonResultData resultsForSession1 = currentEventDetails.results.GetResultsForSession(SessionDetails.SessionType.Qualifying);
              if (currentEventDetails != null && resultsForSession1 != null)
                return (object) resultsForSession1.resultData[0].driver;
              RaceEventDetails previousEventDetails1 = Game.instance.player.team.championship.GetPreviousEventDetails();
              RaceEventResults.SessonResultData resultsForSession2 = previousEventDetails1.results.GetResultsForSession(SessionDetails.SessionType.Qualifying);
              if (previousEventDetails1 != null && resultsForSession2 != null)
                return (object) resultsForSession2.resultData[0].driver;
              Debug.LogError((object) "Trying to find a pole driver but not session has completed yet.", (UnityEngine.Object) null);
              return (object) "[PoleDriver: ERROR]";
            case 106:
              return (object) Game.instance.player.team.championship.GetPreviousEventDetails().circuit;
            case 107:
              if (Game.instance.player.team.championship.GetCurrentEventDetails().hasEventEnded)
                return (object) Game.instance.player.team.championship.GetNextEventDetails().circuit;
              return (object) Game.instance.player.team.championship.GetCurrentEventDetails().circuit;
            case 108:
              return (object) Game.instance.player.team.championship.GetFinalEventDetails().circuit;
            case 109:
              return (object) StringVariableParser.subject.contract.GetTeam().championship.GetFinalEventDetails().circuit;
            case 110:
              return (object) Game.instance.player.team.championship.records.GetWinnersData(Game.instance.time.now.Year - 1).teamChampion;
            case 111:
              return (object) Game.instance.player.team.championship.records.GetWinnersData(Game.instance.time.now.Year - 1).driverChampion;
            case 112:
              return (object) StringVariableParser.oldTrackTrack;
            case 113:
              return (object) StringVariableParser.newTrack;
            case 114:
              return (object) StringVariableParser.overtakeVictim;
            case 115:
              return (object) StringVariableParser.impactVictim;
            case 116:
              return (object) StringVariableParser.attackingDriver;
            case 117:
              return (object) StringVariableParser.teamOrderVictim;
            case 118:
              return (object) StringVariableParser.partForUI;
            case 119:
              return (object) UIManager.instance.GetScreen<ContractNegotiationScreen>().personTarget;
            case 120:
              return (object) UIManager.instance.GetScreen<DriverScreen>().driver;
            case 121:
              Driver driver1 = UIManager.instance.GetScreen<DriverScreen>().driver;
              if (driver1.contract.GetTeam() is NullTeam)
                return (object) null;
              return (object) driver1.contract.GetTeam().GetMechanicOfDriver(driver1);
            case 122:
              return (object) UIManager.instance.GetScreen<SeriesResultsDriverScreen>().driver;
            case 123:
              return (object) UIManager.instance.GetScreen<SeriesResultsDriverScreen>().driver.contract.GetTeam().teamPrincipal;
            case 124:
              return (object) UIManager.instance.GetScreen<SeriesResultsTeamScreen>().teamPrincipal;
            case 125:
              return (object) UIManager.instance.GetScreen<StaffDetailsScreen>().person;
            case 126:
              return (object) UIManager.instance.GetScreen<TeamScreen>().team.contractManager.GetPersonOnJob(Contract.Job.TeamPrincipal);
            case (int) sbyte.MaxValue:
              return (object) UIManager.instance.GetScreen<TeamScreen>().team.chairman;
            case 128:
              return (object) UIManager.instance.GetScreen<TeamScreen>().team.contractManager.GetPersonOnJob(Contract.Job.EngineerLead);
            case 129:
              return (object) Game.instance.sessionManager.eventDetails.circuit;
            case 130:
              return (object) StringVariableParser.randomFemale;
            case 131:
              return (object) StringVariableParser.randomCircuit;
            case 132:
              if (!Game.instance.player.IsUnemployed())
              {
                for (int inIndex = 0; inIndex < 5; ++inIndex)
                {
                  Championship championship3 = Game.instance.player.team.championship;
                  ChampionshipEntry_v1 driverEntry = championship3.standings.GetDriverEntry(inIndex);
                  RaceEventDetails previousEventDetails2 = championship3.GetPreviousEventDetails();
                  if (previousEventDetails2 != null)
                  {
                    RaceEventResults.SessonResultData resultsForSession3 = previousEventDetails2.results.GetResultsForSession(SessionDetails.SessionType.Race);
                    if (resultsForSession3 != null)
                    {
                      Driver entity = driverEntry.GetEntity<Driver>();
                      RaceEventResults.ResultData resultForDriver = resultsForSession3.GetResultForDriver(entity);
                      if (!entity.IsPlayersDriver() && resultForDriver != null && DialogQueryCreator.GetDriverExpectedResultVsActualResult(resultForDriver) > 4)
                        return (object) entity;
                    }
                  }
                }
              }
              return (object) null;
            case 133:
              if (!Game.instance.player.IsUnemployed())
              {
                List<TeamRumour> currentRumours = Game.instance.teamManager.teamRumourManager.currentRumours;
                for (int index = 0; index < currentRumours.Count; ++index)
                {
                  TeamRumour teamRumour = currentRumours[index];
                  if (teamRumour.mPerson != null && teamRumour.mPerson is Driver && (teamRumour.mDestTeam != null && teamRumour.mDestTeam.championship == Game.instance.player.team.championship))
                    return (object) teamRumour;
                }
              }
              return (object) null;
            case 134:
              if (Game.instance.player.IsUnemployed())
                return (object) null;
              Championship championship4 = Game.instance.player.team.championship;
              int num2 = 0;
              Driver driver2 = (Driver) null;
              for (int inIndex = 0; inIndex < Team.mainDriverCount; ++inIndex)
              {
                Driver driver3 = Game.instance.player.team.GetDriver(inIndex);
                int num3 = 0;
                for (int eventNumber = championship4.eventNumber; eventNumber >= 0; --eventNumber)
                {
                  RaceEventDetails raceEventDetails = championship4.calendar[eventNumber];
                  if (raceEventDetails.hasEventEnded)
                  {
                    RaceEventResults.ResultData resultForDriver = raceEventDetails.results.GetResultsForSession(SessionDetails.SessionType.Race).GetResultForDriver(driver3);
                    if (resultForDriver != null)
                      num3 += DialogQueryCreator.GetDriverExpectedResultVsActualResult(resultForDriver);
                  }
                }
                if (num3 > 5 && num3 > num2)
                {
                  num2 = num3;
                  driver2 = driver3;
                }
              }
              return (object) driver2;
            case 135:
              if (StringVariableParser.interviewSubject is Driver)
                return (object) StringVariableParser.interviewSubject.contract.GetTeam().championship.GetPreviousEventDetails().results.GetResultsForSession(SessionDetails.SessionType.Race).GetResultForDriver(StringVariableParser.interviewSubject as Driver).crashVictim;
              return (object) null;
            case 136:
              if (StringVariableParser.interviewSubject is Driver)
                return (object) StringVariableParser.interviewSubject.contract.GetTeam().championship.GetPreviousEventDetails().results.GetResultsForSession(SessionDetails.SessionType.Race).GetResultForDriver(StringVariableParser.interviewSubject as Driver).crashDriver;
              return (object) null;
          }
        }
      }
      return (object) null;
    }
    catch (Exception ex)
    {
      Debug.LogError((object) string.Format("Error with string: {0} \n {1}", (object) inString, (object) ex.Message), (UnityEngine.Object) null);
    }
label_253:
    return (object) null;
  }

  private static string GetGap(int inPlace, int inPlace2)
  {
    SessionManager sessionManager = Game.instance.sessionManager;
    SessionDetails.SessionType sessionTypeWithData = sessionManager.eventDetails.results.GetLastestSessionTypeWithData();
    RaceEventResults.SessonResultData resultsForSession = sessionManager.eventDetails.results.GetResultsForSession(sessionTypeWithData);
    if (sessionTypeWithData == SessionDetails.SessionType.Race)
      return GameUtility.GetGapTimeText(resultsForSession.resultData[inPlace2].time - resultsForSession.resultData[inPlace].time, false);
    return GameUtility.GetGapTimeText(resultsForSession.resultData[inPlace2].bestLapTime - resultsForSession.resultData[inPlace].bestLapTime, false);
  }
}
