// Decompiled with JetBrains decompiler
// Type: RegenManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class RegenManager
{
  public List<RegenNameLUTEntry> secondNameLUTEntries = new List<RegenNameLUTEntry>();
  public List<DatabaseEntry> assistantData;
  public List<DatabaseEntry> fanData;
  public List<DatabaseEntry> chairmanData;
  public List<DatabaseEntry> driverData;
  public List<DatabaseEntry> engineerData;
  public List<DatabaseEntry> mechanicData;
  public List<DatabaseEntry> scoutData;
  public List<DatabaseEntry> teamPrincipalData;
  public List<DatabaseEntry> firstNamesData;
  public List<DatabaseEntry> secondNamesData;
  public List<DatabaseEntry> blockedNamesData;
  public List<DatabaseEntry> secondNamesLUTData;
  public List<DatabaseEntry> driverConfigData;
  private Person.Gender mLastPersonGender;

  public void Start()
  {
    this.fanData = DatabaseReader.LoadFromFile("Data/Regen/Fan");
    this.assistantData = DatabaseReader.LoadFromFile("Data/Regen/Assistant");
    this.chairmanData = DatabaseReader.LoadFromFile("Data/Regen/Chairman");
    this.driverData = DatabaseReader.LoadFromFile("Data/Regen/Driver");
    this.engineerData = DatabaseReader.LoadFromFile("Data/Regen/Engineer");
    this.mechanicData = DatabaseReader.LoadFromFile("Data/Regen/Mechanic");
    this.scoutData = DatabaseReader.LoadFromFile("Data/Regen/Scout");
    this.teamPrincipalData = DatabaseReader.LoadFromFile("Data/Regen/TeamPrincipal");
    this.driverConfigData = DatabaseReader.LoadFromFile("Data/Regen/Driver Config");
    this.firstNamesData = DatabaseReader.LoadFromFile("Data/Regen/First Names");
    this.secondNamesData = DatabaseReader.LoadFromFile("Data/Regen/Second Names");
    this.blockedNamesData = DatabaseReader.LoadFromFile("Data/Regen/Blocked Names");
    this.secondNamesLUTData = DatabaseReader.LoadFromFile("Data/Regen/Second Names LUT");
    this.LoadLUTFromDatabase();
  }

  public void LoadLUTFromDatabase()
  {
    for (int index = 0; index < this.secondNamesLUTData.Count; ++index)
    {
      DatabaseEntry databaseEntry = this.secondNamesLUTData[index];
      this.secondNameLUTEntries.Add(new RegenNameLUTEntry()
      {
        mNation = databaseEntry.GetStringValue("Nation"),
        mMaleNamesStart = databaseEntry.GetIntValue("Male Start"),
        mMaleNamesEnd = databaseEntry.GetIntValue("Male End"),
        mFemaleNamesStart = databaseEntry.GetIntValue("Female Start"),
        mFemaleNamesEnd = databaseEntry.GetIntValue("Female End"),
        mFirstNamesStart = databaseEntry.GetIntValue("First Name Start"),
        mFirstNamesEnd = databaseEntry.GetIntValue("First Name End"),
        mWeightingStart = databaseEntry.GetFloatValue("Weighting Start"),
        mWeightingEnd = databaseEntry.GetFloatValue("Weighting End"),
        mFemaleFirstNamesStart = databaseEntry.GetIntValue("Female First Name Start"),
        mFemaleFirstNamesEnd = databaseEntry.GetIntValue("Female First Name End")
      });
    }
  }

  public void Test()
  {
    List<Driver> driverList = new List<Driver>();
    for (int index = 0; index < 32; ++index)
      driverList.Add(this.CreateDriver(RegenManager.RegenType.Random));
    List<Engineer> engineerList = new List<Engineer>();
    for (int index = 0; index < 32; ++index)
      engineerList.Add(this.CreateEngineer(RegenManager.RegenType.Random));
    List<TeamPrincipal> teamPrincipalList = new List<TeamPrincipal>();
    for (int index = 0; index < 32; ++index)
      teamPrincipalList.Add(this.CreateTeamPrincipal(RegenManager.RegenType.Random));
    List<Chairman> chairmanList = new List<Chairman>();
    for (int index = 0; index < 32; ++index)
      chairmanList.Add(this.CreateChairman(RegenManager.RegenType.Random));
    List<Scout> scoutList = new List<Scout>();
    for (int index = 0; index < 32; ++index)
      scoutList.Add(this.CreateScout(RegenManager.RegenType.Random, (Nationality) null));
    List<Assistant> assistantList = new List<Assistant>();
    for (int index = 0; index < 32; ++index)
      assistantList.Add(this.CreateAssistant(RegenManager.RegenType.Random, (Nationality) null));
    using (List<Driver>.Enumerator enumerator = driverList.GetEnumerator())
    {
      while (enumerator.MoveNext())
        enumerator.Current.SimulateStats();
    }
  }

  public Driver CreateDriver(RegenManager.RegenType inType = RegenManager.RegenType.Random)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    RegenManager.\u003CCreateDriver\u003Ec__AnonStorey4B driverCAnonStorey4B = new RegenManager.\u003CCreateDriver\u003Ec__AnonStorey4B();
    EntityManager entityManager = Game.instance.entityManager;
    DriverManager driverManager = Game.instance.driverManager;
    Driver entity = entityManager.CreateEntity<Driver>();
    DatabaseEntry inEntry = this.driverData[0];
    this.GeneratePersonalDetails((Person) entity, inEntry, (Person) null, false, (Nationality) null);
    // ISSUE: reference to a compiler-generated field
    driverCAnonStorey4B.driverRegenType = Enum.GetName(typeof (RegenManager.RegenType), (object) inType);
    // ISSUE: reference to a compiler-generated method
    driverManager.PopulateWithRandomData(entity, inEntry, inType, this.driverConfigData.Find(new Predicate<DatabaseEntry>(driverCAnonStorey4B.\u003C\u003Em__4F)));
    driverManager.AddEntity(entity);
    entity.contract.SetPerson((Person) entity);
    return entity;
  }

  public Driver CreateDriver(Person inPerson, RegenManager.RegenType inType = RegenManager.RegenType.Random)
  {
    EntityManager entityManager = Game.instance.entityManager;
    DriverManager driverManager = Game.instance.driverManager;
    Driver entity = entityManager.CreateEntity<Driver>();
    DatabaseEntry inEntry = this.driverData[0];
    this.GeneratePersonalDetails((Person) entity, inEntry, inPerson, false, (Nationality) null);
    driverManager.PopulateWithData(entity, inPerson, inEntry, inType);
    driverManager.AddEntity(entity);
    entity.contract.SetPerson((Person) entity);
    return entity;
  }

  public Engineer CreateEngineer(RegenManager.RegenType inType = RegenManager.RegenType.Random)
  {
    EntityManager entityManager = Game.instance.entityManager;
    EngineerManager engineerManager = Game.instance.engineerManager;
    Engineer entity = entityManager.CreateEntity<Engineer>();
    DatabaseEntry inEntry = this.engineerData[0];
    this.GeneratePersonalDetails((Person) entity, inEntry, (Person) null, false, (Nationality) null);
    engineerManager.PopulateWithRandomData(entity, inEntry, inType);
    engineerManager.AddEntity(entity);
    entity.contract.SetPerson((Person) entity);
    return entity;
  }

  public TeamPrincipal CreateTeamPrincipal(RegenManager.RegenType inType = RegenManager.RegenType.Random)
  {
    EntityManager entityManager = Game.instance.entityManager;
    TeamPrincipalManager principalManager = Game.instance.teamPrincipalManager;
    TeamPrincipal entity = entityManager.CreateEntity<TeamPrincipal>();
    DatabaseEntry inEntry = this.teamPrincipalData[0];
    this.GeneratePersonalDetails((Person) entity, inEntry, (Person) null, false, (Nationality) null);
    principalManager.PopulateWithRandomData(entity, inEntry, inType);
    principalManager.AddEntity(entity);
    entity.contract.SetPerson((Person) entity);
    return entity;
  }

  public Chairman CreateChairman(RegenManager.RegenType inType = RegenManager.RegenType.Random)
  {
    EntityManager entityManager = Game.instance.entityManager;
    ChairmanManager chairmanManager = Game.instance.chairmanManager;
    Chairman entity = entityManager.CreateEntity<Chairman>();
    DatabaseEntry inEntry = this.chairmanData[0];
    this.GeneratePersonalDetails((Person) entity, inEntry, (Person) null, false, (Nationality) null);
    chairmanManager.PopulateWithRandomData(entity, inEntry, inType);
    chairmanManager.AddEntity(entity);
    entity.contract.SetPerson((Person) entity);
    return entity;
  }

  public Mechanic CreateMechanic(RegenManager.RegenType inType = RegenManager.RegenType.Random)
  {
    EntityManager entityManager = Game.instance.entityManager;
    MechanicManager mechanicManager = Game.instance.mechanicManager;
    Mechanic entity = entityManager.CreateEntity<Mechanic>();
    DatabaseEntry inEntry = this.mechanicData[0];
    this.GeneratePersonalDetails((Person) entity, inEntry, (Person) null, false, (Nationality) null);
    mechanicManager.PopulateWithRandomData(entity, inEntry, inType);
    mechanicManager.AddEntity(entity);
    entity.contract.SetPerson((Person) entity);
    return entity;
  }

  public Scout CreateScout(RegenManager.RegenType inType = RegenManager.RegenType.Random, Nationality inNationality = null)
  {
    EntityManager entityManager = Game.instance.entityManager;
    ScoutManager scoutManager = Game.instance.scoutManager;
    Scout entity = entityManager.CreateEntity<Scout>();
    DatabaseEntry inEntry = this.scoutData[0];
    this.GeneratePersonalDetails((Person) entity, inEntry, (Person) null, false, inNationality);
    scoutManager.PopulateWithRandomData(entity, inEntry, inType);
    scoutManager.AddEntity(entity);
    entity.contract.SetPerson((Person) entity);
    return entity;
  }

  public Assistant CreateAssistant(RegenManager.RegenType inType = RegenManager.RegenType.Random, Nationality inNationality = null)
  {
    EntityManager entityManager = Game.instance.entityManager;
    AssistantManager assistantManager = Game.instance.assistantManager;
    Assistant entity = entityManager.CreateEntity<Assistant>();
    DatabaseEntry inEntry = this.assistantData[0];
    this.GeneratePersonalDetails((Person) entity, inEntry, (Person) null, false, inNationality);
    assistantManager.PopulateWithRandomData(entity, inEntry, inType);
    assistantManager.AddEntity(entity);
    entity.contract.SetPerson((Person) entity);
    return entity;
  }

  public Person CreateFan(RegenManager.RegenType inType = RegenManager.RegenType.Random, Person.Gender inGender = Person.Gender.Male, bool inForceGender = false)
  {
    Person inPerson = new Person();
    if (inForceGender)
      inPerson.gender = inGender;
    DatabaseEntry inEntry = this.fanData[0];
    this.GeneratePersonalDetails(inPerson, inEntry, (Person) null, inForceGender, (Nationality) null);
    inPerson.contract.job = Contract.Job.Fan;
    return inPerson;
  }

  private void GeneratePersonalDetails(Person inPerson, DatabaseEntry inEntry, Person inParent = null, bool inForceGender = false, Nationality inNationality = null)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    RegenManager.\u003CGeneratePersonalDetails\u003Ec__AnonStorey4C detailsCAnonStorey4C = new RegenManager.\u003CGeneratePersonalDetails\u003Ec__AnonStorey4C();
    // ISSUE: reference to a compiler-generated field
    detailsCAnonStorey4C.inNationality = inNationality;
    // ISSUE: reference to a compiler-generated field
    if (!inForceGender && detailsCAnonStorey4C.inNationality == (Nationality) null)
    {
      inPerson.gender = this.mLastPersonGender != Person.Gender.Male ? Person.Gender.Male : Person.Gender.Female;
      this.mLastPersonGender = inPerson.gender;
      inForceGender = true;
    }
    bool flag = true;
    int num = 0;
    RegenManager.RegenEthnicity inEthnicity = RegenManager.RegenEthnicity.Unknown;
    while (flag)
    {
      RegenNameLUTEntry regenNameLutEntry = (RegenNameLUTEntry) null;
      while (regenNameLutEntry == null)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        RegenManager.\u003CGeneratePersonalDetails\u003Ec__AnonStorey4D detailsCAnonStorey4D = new RegenManager.\u003CGeneratePersonalDetails\u003Ec__AnonStorey4D();
        // ISSUE: reference to a compiler-generated field
        detailsCAnonStorey4D.\u003C\u003Ef__ref\u002476 = detailsCAnonStorey4C;
        // ISSUE: reference to a compiler-generated field
        detailsCAnonStorey4D.lRandomWeighting = RandomUtility.GetRandom(0.0f, 100f);
        // ISSUE: reference to a compiler-generated field
        if (detailsCAnonStorey4C.inNationality != (Nationality) null)
        {
          // ISSUE: reference to a compiler-generated method
          regenNameLutEntry = this.secondNameLUTEntries.Find(new Predicate<RegenNameLUTEntry>(detailsCAnonStorey4D.\u003C\u003Em__50));
          if (regenNameLutEntry == null)
          {
            // ISSUE: reference to a compiler-generated field
            detailsCAnonStorey4C.inNationality = (Nationality) null;
          }
        }
        if (regenNameLutEntry == null)
        {
          // ISSUE: reference to a compiler-generated method
          regenNameLutEntry = this.secondNameLUTEntries.Find(new Predicate<RegenNameLUTEntry>(detailsCAnonStorey4D.\u003C\u003Em__51));
        }
        if (regenNameLutEntry != null && inForceGender && (inPerson.gender == Person.Gender.Female && regenNameLutEntry.mFemaleFirstNamesStart == -1))
          regenNameLutEntry = (RegenNameLUTEntry) null;
      }
      DatabaseEntry databaseEntry1 = this.firstNamesData[Mathf.Clamp(!inForceGender || regenNameLutEntry.mFemaleFirstNamesEnd == -1 ? RandomUtility.GetRandomInc(regenNameLutEntry.mFirstNamesStart, regenNameLutEntry.mFirstNamesEnd) : (inPerson.gender != Person.Gender.Female ? RandomUtility.GetRandomInc(regenNameLutEntry.mFemaleFirstNamesEnd + 1, regenNameLutEntry.mFirstNamesEnd) : RandomUtility.GetRandomInc(regenNameLutEntry.mFemaleFirstNamesStart, regenNameLutEntry.mFemaleFirstNamesEnd)), 0, this.firstNamesData.Count)];
      num = databaseEntry1.GetIntValue("Female");
      if (inParent == null)
      {
        inPerson.nationality = Nationality.GetNationalityByName(regenNameLutEntry.mNation);
        if (!(inPerson.nationality == (Nationality) null))
        {
          DatabaseEntry databaseEntry2 = this.secondNamesData[num != 1 ? RandomUtility.GetRandomInc(regenNameLutEntry.mMaleNamesStart, regenNameLutEntry.mMaleNamesEnd) : RandomUtility.GetRandomInc(regenNameLutEntry.mFemaleNamesStart, regenNameLutEntry.mFemaleNamesEnd)];
          inPerson.SetName(databaseEntry1.GetStringValue("Name"), databaseEntry2.GetStringValue("Name"));
          inEthnicity = (RegenManager.RegenEthnicity) databaseEntry2.GetIntValue("Ethnicity");
          flag = this.IsBlockedName(inPerson.firstName, inPerson.lastName);
        }
      }
      else
      {
        inPerson.SetName(databaseEntry1.GetStringValue("First Name"), inParent.lastName);
        inPerson.nationality = inParent.nationality;
        flag = this.IsBlockedName(inPerson.firstName, inPerson.lastName);
      }
    }
    inPerson.gender = num != 1 ? Person.Gender.Male : Person.Gender.Female;
    int year = Game.instance.time.now.Year - RandomUtility.GetRandom(inEntry.GetIntValue("Age Min"), inEntry.GetIntValue("Age Max"));
    int randomInc1 = RandomUtility.GetRandomInc(1, 12);
    int randomInc2 = RandomUtility.GetRandomInc(1, DateTime.DaysInMonth(year, randomInc1));
    inPerson.dateOfBirth = new DateTime(year, randomInc1, randomInc2);
    inPerson.portrait.GeneratePortrait(inPerson, this.EthnicityToSkinColour(inEthnicity));
  }

  private bool IsBlockedName(string inFirstName, string inLastName)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: reference to a compiler-generated method
    return this.blockedNamesData.Find(new Predicate<DatabaseEntry>(new RegenManager.\u003CIsBlockedName\u003Ec__AnonStorey4E() { inFirstName = inFirstName, inLastName = inLastName }.\u003C\u003Em__52)) != null;
  }

  public int EthnicityToSkinColour(RegenManager.RegenEthnicity inEthnicity)
  {
    int num = 0;
    switch (inEthnicity + 1)
    {
      case RegenManager.RegenEthnicity.NorthernEuropean:
        num = RandomUtility.GetRandom(0, 6);
        break;
      case RegenManager.RegenEthnicity.MediterraneanHispanic:
        num = RandomUtility.GetRandom(0, 3);
        break;
      case RegenManager.RegenEthnicity.NorthAfricanMiddleEastern:
        num = RandomUtility.GetRandom(2, 5);
        break;
      case RegenManager.RegenEthnicity.NorthAmericanMiddleEastern:
        num = RandomUtility.GetRandom(2, 5);
        break;
      case RegenManager.RegenEthnicity.AfricanCaribbean:
        num = RandomUtility.GetRandom(2, 5);
        break;
      case RegenManager.RegenEthnicity.Asian:
        num = RandomUtility.GetRandom(3, 6);
        break;
      case RegenManager.RegenEthnicity.SouthEastAsian:
        num = RandomUtility.GetRandom(2, 5);
        break;
      case RegenManager.RegenEthnicity.PacificIslander:
        num = RandomUtility.GetRandom(2, 5);
        break;
      case RegenManager.RegenEthnicity.NativeAmerican:
        num = RandomUtility.GetRandom(2, 5);
        break;
      case RegenManager.RegenEthnicity.NativeAustralian:
        num = RandomUtility.GetRandom(2, 5);
        break;
      case RegenManager.RegenEthnicity.MixedRaceWhiteBlack:
        num = RandomUtility.GetRandom(2, 5);
        break;
      case RegenManager.RegenEthnicity.EastAsian:
        num = RandomUtility.GetRandom(2, 5);
        break;
      case RegenManager.RegenEthnicity.AfricanCaribbean | RegenManager.RegenEthnicity.NativeAmerican:
        num = RandomUtility.GetRandom(2, 5);
        break;
    }
    return num;
  }

  public enum RegenType
  {
    Random,
    Good,
    Poor,
    Average,
    FromRetired,
    Replacement,
  }

  public enum RegenEthnicity
  {
    Unknown = -1,
    NorthernEuropean = 0,
    MediterraneanHispanic = 1,
    NorthAfricanMiddleEastern = 2,
    NorthAmericanMiddleEastern = 3,
    AfricanCaribbean = 4,
    Asian = 5,
    SouthEastAsian = 6,
    PacificIslander = 7,
    NativeAmerican = 8,
    NativeAustralian = 9,
    MixedRaceWhiteBlack = 10,
    EastAsian = 11,
  }
}
