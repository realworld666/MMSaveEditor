// Decompiled with JetBrains decompiler
// Type: PersonManager`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public abstract class PersonManager<T> : GenericManager<T> where T : Person
{
  protected List<T> mReplacementPeople = new List<T>();

  protected abstract void AddPeopleFromDatabase(Database database);

  public override void OnStart(Database database)
  {
    base.OnStart(database);
    this.AddPeopleFromDatabase(database);
  }

  public void SetPersonalDetails(Person inPerson, DatabaseEntry inData)
  {
    inPerson.SetName(inData.GetStringValue("First Name"), inData.GetStringValue("Last Name"));
    inPerson.gender = PersonManager<T>.GetGender(inData.GetStringValue("Gender"));
    inPerson.nationality = Nationality.GetNationalityByName(inData.GetStringValue("Nationality"));
    string stringValue = inData.GetStringValue("DOB");
    PersonManager<T>.SetDOBFromString(inPerson, stringValue);
    inPerson.peakDuration = inData.GetIntValue("Peak Duration");
    inPerson.peakAge = inPerson.dateOfBirth.AddYears(inData.GetIntValue("Peak Age"));
  }

  public static void SetDOBFromString(Person inPerson, string dobString)
  {
    string[] strArray = dobString.Split('/');
    if (strArray.Length != 3)
    {
      UnityEngine.Debug.LogWarningFormat("Person's DOB in database is in wrong format. Person: {0}", (object) inPerson.name);
    }
    else
    {
      int result1;
      int result2;
      int result3;
      if (!(true & int.TryParse(strArray[2], out result1) & int.TryParse(strArray[1], out result2) & int.TryParse(strArray[0], out result3)))
        UnityEngine.Debug.LogWarningFormat("Person's DOB in database is in wrong format. Person: {0}", (object) inPerson.name);
      else
        inPerson.dateOfBirth = new DateTime(int.Parse(strArray[2]), int.Parse(strArray[1]), int.Parse(strArray[0]));
    }
  }

  public void SetPortraitData(Person inPerson, DatabaseEntry inData)
  {
    Portrait portrait = inPerson.portrait;
    portrait.head = inData.GetIntValue("Skin Colour");
    portrait.facialHair = inData.GetIntValue("Facial Hair");
    portrait.hair = inData.GetIntValue("Hair");
    portrait.hairColor = inData.GetIntValue("Hair Colour");
    portrait.glasses = inData.GetIntValue("Glasses");
    portrait.accessory = inData.GetIntValue("Accessory");
  }

  public static DateTime GetDateOfBirth(string dobString)
  {
    DateTime dateTime = new DateTime();
    string[] strArray = dobString.Split('/');
    if (strArray.Length != 3)
    {
      UnityEngine.Debug.LogWarningFormat("Person's DOB in database is in wrong format.");
      return dateTime;
    }
    int result1;
    int result2;
    int result3;
    if (!(true & int.TryParse(strArray[2], out result1) & int.TryParse(strArray[1], out result2) & int.TryParse(strArray[0], out result3)))
    {
      UnityEngine.Debug.LogWarningFormat("Person's DOB in database is in wrong format.");
      return dateTime;
    }
    dateTime = new DateTime(int.Parse(strArray[2]), int.Parse(strArray[1]), int.Parse(strArray[0]));
    return dateTime;
  }

  public static Portrait GetPortrait(DatabaseEntry inData)
  {
    return new Portrait() { head = inData.GetIntValue("Skin Colour"), facialHair = inData.GetIntValue("Facial Hair"), hair = inData.GetIntValue("Hair"), hairColor = inData.GetIntValue("Hair Colour"), glasses = inData.GetIntValue("Glasses"), accessory = inData.GetIntValue("Accessory") };
  }

  public static Person.Gender GetGender(string inGenderString)
  {
    return inGenderString.Equals("m", StringComparison.OrdinalIgnoreCase) ? Person.Gender.Male : Person.Gender.Female;
  }

  public int CalculateExpectedPositionAmongPeersInChampionship(Person inPerson)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    PersonManager<T>.\u003CCalculateExpectedPositionAmongPeersInChampionship\u003Ec__AnonStorey43 championshipCAnonStorey43 = new PersonManager<T>.\u003CCalculateExpectedPositionAmongPeersInChampionship\u003Ec__AnonStorey43();
    Championship championship = inPerson.contract.GetTeam().championship;
    // ISSUE: reference to a compiler-generated field
    championshipCAnonStorey43.personExpectation = inPerson.GetChampionshipExpectation();
    List<float> floatList = new List<float>();
    // ISSUE: reference to a compiler-generated field
    floatList.Add(championshipCAnonStorey43.personExpectation);
    using (List<T>.Enumerator enumerator = this.GetEntityList().GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        Person current = (Person) enumerator.Current;
        if (current != inPerson && current.contract.job != Contract.Job.Unemployed && (current.contract.GetTeam() != null && current.contract.GetTeam().championship == championship))
          floatList.Add(current.GetChampionshipExpectation());
      }
    }
    floatList.Sort((Comparison<float>) ((x, y) => y.CompareTo(x)));
    // ISSUE: reference to a compiler-generated method
    return floatList.FindIndex(new Predicate<float>(championshipCAnonStorey43.\u003C\u003Em__3A)) + 1;
  }

  public int CalculateExpectedPositionAmongAllPeers(Person inPerson)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    PersonManager<T>.\u003CCalculateExpectedPositionAmongAllPeers\u003Ec__AnonStorey44 peersCAnonStorey44 = new PersonManager<T>.\u003CCalculateExpectedPositionAmongAllPeers\u003Ec__AnonStorey44();
    // ISSUE: reference to a compiler-generated field
    peersCAnonStorey44.personExpectation = inPerson.GetChampionshipExpectation();
    List<float> floatList = new List<float>();
    // ISSUE: reference to a compiler-generated field
    floatList.Add(peersCAnonStorey44.personExpectation);
    using (List<T>.Enumerator enumerator = this.GetEntityList().GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        Person current = (Person) enumerator.Current;
        if (current != inPerson)
          floatList.Add(current.GetChampionshipExpectation());
      }
    }
    floatList.Sort((Comparison<float>) ((x, y) => y.CompareTo(x)));
    // ISSUE: reference to a compiler-generated method
    return floatList.FindIndex(new Predicate<float>(peersCAnonStorey44.\u003C\u003Em__3C)) + 1;
  }

  public bool IsReplacementPerson(T inPerson)
  {
    return this.mReplacementPeople.Contains(inPerson);
  }

  public int GetPersonIndex(T inPerson)
  {
    List<T> entityList = this.GetEntityList();
    for (int index = 0; index < entityList.Count; ++index)
    {
      if ((object) entityList[index] == (object) inPerson)
        return index;
    }
    return -1;
  }

  public int GetNumRetiredThisYear()
  {
    int num = 0;
    for (int inIndex = 0; inIndex < this.count; ++inIndex)
    {
      Person entity = (Person) this.GetEntity(inIndex);
      if (entity.retirementAge == entity.GetAge())
        ++num;
    }
    return num;
  }

  public void OnYearEnd()
  {
    int count = this.count;
    DateTime now = Game.instance.time.now;
    for (int inIndex = 0; inIndex < count; ++inIndex)
    {
      Person entity = (Person) this.GetEntity(inIndex);
      if (entity.IsFreeAgent() && !entity.HasRetired())
      {
        float inImprovementRate = !(entity is Driver) ? (!(entity is Engineer) ? (!(entity is Mechanic) ? 0.0f : ((Mechanic) entity).improvementRate) : ((Engineer) entity).improvementRate) : ((Driver) entity).GetImprovementRate();
        if (entity.WantsToRetire(now, inImprovementRate))
          PersonManager<T>.RetireFreeAgentPerson(entity);
      }
    }
  }

  public static void RetireFreeAgentPerson(Person inPerson)
  {
    bool flag = inPerson.IsReplacementPerson();
    inPerson.Retire();
    Person person = (Person) null;
    if (!flag)
    {
      if (inPerson is Driver)
      {
        person = (Person) App.instance.regenManager.CreateDriver(RegenManager.RegenType.Random);
        Driver driver = person as Driver;
        if (driver != null && !((Driver) inPerson).joinsAnySeries)
          driver.SetPreferedSeries(((Driver) inPerson).preferedSeries);
      }
      else if (inPerson is Mechanic)
        person = (Person) App.instance.regenManager.CreateMechanic(RegenManager.RegenType.Random);
      else if (inPerson is Engineer)
        person = (Person) App.instance.regenManager.CreateEngineer(RegenManager.RegenType.Random);
      else if (inPerson is TeamPrincipal)
        person = (Person) App.instance.regenManager.CreateTeamPrincipal(RegenManager.RegenType.Random);
      if (person != null)
        ;
    }
    else
    {
      if (inPerson is Driver)
        person = (Person) Game.instance.driverManager.RetireReplacement(inPerson as Driver);
      else if (inPerson is Mechanic)
        person = (Person) Game.instance.mechanicManager.RetireReplacement(inPerson as Mechanic);
      else if (inPerson is Engineer)
        person = (Person) Game.instance.engineerManager.RetireReplacement(inPerson as Engineer);
      else if (inPerson is TeamPrincipal)
        person = (Person) Game.instance.teamPrincipalManager.RetireReplacement(inPerson as TeamPrincipal);
      if (person != null)
        ;
    }
  }
}
