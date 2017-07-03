// Decompiled with JetBrains decompiler
// Type: CareerHistoryGenerator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

public class CareerHistoryGenerator
{
  private readonly int mMinTeamChangeYearDriver = 2;
  private readonly int mMaxTeamChangeYearDriver = 5;
  private readonly int mMinTeamChangeYearEngineer = 5;
  private readonly int mMaxTeamChangeYearEngineer = 10;
  private readonly int mMinTeamChangeYearMechanic = 4;
  private readonly int mMaxTeamChangeYearMechanic = 8;
  private int mNumRacesPerSeason;
  private int mRandomSeed;
  private float mStatsVariance;
  private float mDNFChance;
  private int mStartAgeMin;
  private int mStartAgeMax;
  private int mNumYears;
  public int mLowestTeamReputation;
  private List<CareerHistoryGenerator.TeamHistoryYear> mAllTeams;
  private Championship mChampionship;

  public CareerHistoryGenerator(Championship inChampionship)
  {
    this.mChampionship = inChampionship;
    this.mNumRacesPerSeason = inChampionship.eventCount;
    this.mRandomSeed = inChampionship.historySeed;
    this.mStatsVariance = inChampionship.historyVariance;
    this.mDNFChance = inChampionship.historyDNFChance;
    this.mStartAgeMin = inChampionship.historyMinStartAge;
    this.mStartAgeMax = inChampionship.historyMaxStartAge;
    this.mNumYears = inChampionship.historyYears;
    this.mAllTeams = new List<CareerHistoryGenerator.TeamHistoryYear>();
    int teamEntryCount = this.mChampionship.standings.teamEntryCount;
    for (int inIndex = 0; inIndex < teamEntryCount; ++inIndex)
    {
      Team entity = this.mChampionship.standings.GetTeamEntry(inIndex).GetEntity<Team>();
      if (entity != null && !(entity is NullTeam) && !entity.isCreatedAndManagedByPlayerFirstYear())
        this.mAllTeams.Add(new CareerHistoryGenerator.TeamHistoryYear()
        {
          team = entity
        });
    }
    this.mAllTeams.Sort((Comparison<CareerHistoryGenerator.TeamHistoryYear>) ((x, y) => x.team.reputation.CompareTo(y.team.reputation)));
    this.mLowestTeamReputation = this.mAllTeams[0].team.reputation;
  }

  public void GenerateHistory()
  {
    Random random = new Random(this.mRandomSeed);
    List<CareerHistoryGenerator.PersonHistoryYear> personHistoryYearList1 = new List<CareerHistoryGenerator.PersonHistoryYear>();
    List<CareerHistoryGenerator.PersonHistoryYear> personHistoryYearList2 = new List<CareerHistoryGenerator.PersonHistoryYear>();
    List<CareerHistoryGenerator.PersonHistoryYear> personHistoryYearList3 = new List<CareerHistoryGenerator.PersonHistoryYear>();
    List<CareerHistoryGenerator.PersonHistoryYear> personHistoryYearList4 = new List<CareerHistoryGenerator.PersonHistoryYear>();
    List<CareerHistoryGenerator.PersonHistoryYear> personHistoryYearList5 = new List<CareerHistoryGenerator.PersonHistoryYear>();
    this.GetAllDrivers(personHistoryYearList1, personHistoryYearList2, personHistoryYearList3, personHistoryYearList4, personHistoryYearList5);
    for (int index = 0; index < personHistoryYearList3.Count; ++index)
    {
      CareerHistoryGenerator.PersonHistoryYear lCurPerson = personHistoryYearList3[index];
      this.SetupCareerHistory(random, lCurPerson);
    }
    for (int index = 0; index < personHistoryYearList1.Count; ++index)
    {
      CareerHistoryGenerator.PersonHistoryYear lCurPerson = personHistoryYearList1[index];
      this.SetupCareerHistory(random, lCurPerson);
    }
    for (int index = 0; index < personHistoryYearList2.Count; ++index)
    {
      CareerHistoryGenerator.PersonHistoryYear lCurPerson = personHistoryYearList2[index];
      this.SetupCareerHistory(random, lCurPerson);
    }
    this.SetupTeamChangesForPeople(personHistoryYearList3, random, this.mMinTeamChangeYearDriver, this.mMaxTeamChangeYearDriver);
    this.SetupTeamChangesForPeople(personHistoryYearList1, random, this.mMinTeamChangeYearEngineer, this.mMaxTeamChangeYearEngineer);
    this.SetupTeamChangesForPeople(personHistoryYearList2, random, this.mMinTeamChangeYearMechanic, this.mMaxTeamChangeYearMechanic);
    for (int i = 0; i < this.mNumYears; ++i)
    {
      List<CareerHistoryGenerator.PersonHistoryYear> personHistoryYearList6 = new List<CareerHistoryGenerator.PersonHistoryYear>();
      List<CareerHistoryGenerator.PersonHistoryYear> personHistoryYearList7 = new List<CareerHistoryGenerator.PersonHistoryYear>();
      List<CareerHistoryGenerator.PersonHistoryYear> personHistoryYearList8 = new List<CareerHistoryGenerator.PersonHistoryYear>();
      List<CareerHistoryGenerator.PersonHistoryYear> personHistoryYearList9 = new List<CareerHistoryGenerator.PersonHistoryYear>();
      this.AddPersonForYear(personHistoryYearList4, i, personHistoryYearList6);
      this.AddPersonForYear(personHistoryYearList5, i, personHistoryYearList7);
      this.AddPersonForYear(personHistoryYearList1, i, personHistoryYearList8);
      this.AddPersonForYear(personHistoryYearList2, i, personHistoryYearList9);
      this.ClearTeamsForYear();
      for (int index1 = 0; index1 < this.mNumRacesPerSeason; ++index1)
      {
        this.SetupDriverDNF(random, personHistoryYearList6);
        personHistoryYearList6.Sort((Comparison<CareerHistoryGenerator.PersonHistoryYear>) ((x, y) => y.careerFactor.CompareTo(x.careerFactor)));
        int num = random.Next(0, personHistoryYearList6.Count);
        for (int index2 = 0; index2 < personHistoryYearList8.Count; ++index2)
          ++personHistoryYearList8[index2].races;
        for (int index2 = 0; index2 < personHistoryYearList9.Count; ++index2)
          ++personHistoryYearList9[index2].races;
        for (int k = 0; k < personHistoryYearList6.Count; ++k)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          CareerHistoryGenerator.\u003CGenerateHistory\u003Ec__AnonStorey45 historyCAnonStorey45 = new CareerHistoryGenerator.\u003CGenerateHistory\u003Ec__AnonStorey45();
          // ISSUE: reference to a compiler-generated field
          historyCAnonStorey45.lCurDriver = personHistoryYearList6[k];
          // ISSUE: reference to a compiler-generated field
          if ((double) historyCAnonStorey45.lCurDriver.careerFactor != -3.0)
          {
            if (k == num)
            {
              // ISSUE: reference to a compiler-generated field
              ++historyCAnonStorey45.lCurDriver.poles;
            }
            // ISSUE: reference to a compiler-generated field
            ++historyCAnonStorey45.lCurDriver.races;
          }
          else
          {
            // ISSUE: reference to a compiler-generated method
            CareerHistoryGenerator.PersonHistoryYear personHistoryYear = personHistoryYearList7.Find(new Predicate<CareerHistoryGenerator.PersonHistoryYear>(historyCAnonStorey45.\u003C\u003Em__3F));
            if (personHistoryYear != null)
            {
              if (k == num)
                ++personHistoryYear.poles;
              ++personHistoryYear.races;
            }
          }
          if (k == 0)
          {
            // ISSUE: reference to a compiler-generated field
            ++historyCAnonStorey45.lCurDriver.wins;
            // ISSUE: reference to a compiler-generated field
            ++historyCAnonStorey45.lCurDriver.teamHistory.wins;
          }
          if (k < 3)
          {
            // ISSUE: reference to a compiler-generated field
            ++historyCAnonStorey45.lCurDriver.podiums;
            // ISSUE: reference to a compiler-generated field
            ++historyCAnonStorey45.lCurDriver.teamHistory.podiums;
          }
          int pointsForPosition = this.mChampionship.rules.GetPointsForPosition(k + 1);
          // ISSUE: reference to a compiler-generated field
          historyCAnonStorey45.lCurDriver.racePoints = pointsForPosition;
          // ISSUE: reference to a compiler-generated field
          historyCAnonStorey45.lCurDriver.points += pointsForPosition;
          // ISSUE: reference to a compiler-generated field
          historyCAnonStorey45.lCurDriver.teamHistory.points += pointsForPosition;
          // ISSUE: reference to a compiler-generated field
          if ((double) historyCAnonStorey45.lCurDriver.careerFactor == -1.0)
          {
            // ISSUE: reference to a compiler-generated field
            ++historyCAnonStorey45.lCurDriver.DNFs;
            // ISSUE: reference to a compiler-generated field
            ++historyCAnonStorey45.lCurDriver.teamHistory.DNFs;
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            if ((double) historyCAnonStorey45.lCurDriver.careerFactor == -2.0)
            {
              // ISSUE: reference to a compiler-generated field
              ++historyCAnonStorey45.lCurDriver.DNFsByError;
              // ISSUE: reference to a compiler-generated field
              ++historyCAnonStorey45.lCurDriver.teamHistory.DNFs;
            }
            else
            {
              // ISSUE: reference to a compiler-generated field
              if ((double) historyCAnonStorey45.lCurDriver.careerFactor == -3.0)
              {
                // ISSUE: reference to a compiler-generated field
                ++historyCAnonStorey45.lCurDriver.DNS;
                // ISSUE: reference to a compiler-generated field
                ++historyCAnonStorey45.lCurDriver.teamHistory.DNFs;
              }
            }
          }
          // ISSUE: reference to a compiler-generated field
          this.ApplyHistoryForEngineersFromDriver(personHistoryYearList8, k, historyCAnonStorey45.lCurDriver);
          // ISSUE: reference to a compiler-generated field
          this.ApplyHistoryForMechanicsFromDriver(personHistoryYearList9, k, historyCAnonStorey45.lCurDriver);
        }
      }
      personHistoryYearList6.Sort((Comparison<CareerHistoryGenerator.PersonHistoryYear>) ((x, y) => y.points.CompareTo(x.points)));
      this.mAllTeams.Sort((Comparison<CareerHistoryGenerator.TeamHistoryYear>) ((x, y) => y.points.CompareTo(x.points)));
      int num1 = Game.instance.time.now.Year - (this.mNumYears - i);
      this.ApplyChampionshipWins(personHistoryYearList6, personHistoryYearList8, personHistoryYearList9, num1);
      this.ApplyCareerHistoryForYear(personHistoryYearList6, num1);
      this.ApplyCareerHistoryForYear(personHistoryYearList7, num1);
      this.ApplyCareerHistoryForYear(personHistoryYearList9, num1);
      this.ApplyCareerHistoryForYear(personHistoryYearList8, num1);
    }
    for (int index = 0; index < personHistoryYearList3.Count; ++index)
      personHistoryYearList3[index].person.careerHistory.AddHistory();
    for (int index = 0; index < personHistoryYearList1.Count; ++index)
      personHistoryYearList1[index].person.careerHistory.AddHistory();
    for (int index = 0; index < personHistoryYearList2.Count; ++index)
      personHistoryYearList2[index].person.careerHistory.AddHistory();
  }

  private void ApplyChampionshipWins(List<CareerHistoryGenerator.PersonHistoryYear> lMainDriversInYear, List<CareerHistoryGenerator.PersonHistoryYear> lEngineersInYear, List<CareerHistoryGenerator.PersonHistoryYear> lMechanicsInYear, int lCurYear)
  {
    ++lMainDriversInYear[0].championships;
    ++this.mAllTeams[0].championships;
    if (!lMainDriversInYear[0].isDummy)
      this.mChampionship.records.PostChampionshipWinnersData(new ChampionshipWinnersEntry()
      {
        driverChampion = lMainDriversInYear[0].person as Driver,
        driverPodiums = lMainDriversInYear[0].podiums,
        driverRaces = lMainDriversInYear[0].races,
        driverPoints = lMainDriversInYear[0].points,
        driverWins = lMainDriversInYear[0].wins,
        driverDNFs = lMainDriversInYear[0].DNFs,
        driversTeam = lMainDriversInYear[0].teamHistory.team,
        driversTeamPrincipal = (Person) lMainDriversInYear[0].teamHistory.team.teamPrincipal,
        teamChampion = this.mAllTeams[0].team,
        teamPoints = this.mAllTeams[0].points
      }, lCurYear);
    for (int index = 0; index < lEngineersInYear.Count; ++index)
    {
      CareerHistoryGenerator.PersonHistoryYear personHistoryYear = lEngineersInYear[index];
      if (personHistoryYear.teamHistory == lMainDriversInYear[0].teamHistory)
        ++personHistoryYear.championships;
    }
    for (int index = 0; index < lMechanicsInYear.Count; ++index)
    {
      CareerHistoryGenerator.PersonHistoryYear personHistoryYear = lMechanicsInYear[index];
      if (personHistoryYear.teamHistory == lMainDriversInYear[0].teamHistory)
      {
        Mechanic person = personHistoryYear.person as Mechanic;
        if (person.driver == 1 && personHistoryYear.teamHistory.team.GetDriver(0) == lMainDriversInYear[0].person || person.driver == 2 && personHistoryYear.teamHistory.team.GetDriver(1) == lMainDriversInYear[0].person)
          ++personHistoryYear.championships;
      }
    }
  }

  private void SetupDriverDNF(Random lRandom, List<CareerHistoryGenerator.PersonHistoryYear> lMainDriversInYear)
  {
    for (int index = 0; index < lMainDriversInYear.Count; ++index)
    {
      CareerHistoryGenerator.PersonHistoryYear personHistoryYear = lMainDriversInYear[index];
      float num1 = (float) lRandom.NextDouble();
      if ((double) num1 <= (double) this.mDNFChance * 0.100000001490116)
        personHistoryYear.careerFactor = -3f;
      else if ((double) num1 <= (double) this.mDNFChance * 0.5)
        personHistoryYear.careerFactor = -2f;
      else if ((double) num1 <= (double) this.mDNFChance)
      {
        personHistoryYear.careerFactor = -1f;
      }
      else
      {
        float num2 = (float) (lRandom.NextDouble() * (double) this.mStatsVariance - (double) this.mStatsVariance * 0.5);
        personHistoryYear.careerFactor = (personHistoryYear.person as Driver).GetDriverStats().GetUnitAverage() + (float) personHistoryYear.teamHistory.team.reputation / 200f + num2;
      }
    }
  }

  private void ApplyHistoryForMechanicsFromDriver(List<CareerHistoryGenerator.PersonHistoryYear> lMechanicsInYear, int k, CareerHistoryGenerator.PersonHistoryYear lCurDriver)
  {
    for (int index = 0; index < lMechanicsInYear.Count; ++index)
    {
      CareerHistoryGenerator.PersonHistoryYear personHistoryYear = lMechanicsInYear[index];
      if (lCurDriver.teamHistory == personHistoryYear.teamHistory)
      {
        Mechanic person = personHistoryYear.person as Mechanic;
        if (person.driver == 1 && personHistoryYear.teamHistory.team.GetDriver(0) == lCurDriver.person || person.driver == 2 && personHistoryYear.teamHistory.team.GetDriver(1) == lCurDriver.person)
        {
          if (k == 0)
            ++personHistoryYear.wins;
          if (k < 3)
            ++personHistoryYear.podiums;
          personHistoryYear.points += this.mChampionship.rules.GetPointsForPosition(k + 1);
          if ((double) lCurDriver.careerFactor <= -1.0)
            ++personHistoryYear.DNFs;
        }
      }
    }
  }

  private void ApplyHistoryForEngineersFromDriver(List<CareerHistoryGenerator.PersonHistoryYear> lEngineersInYear, int k, CareerHistoryGenerator.PersonHistoryYear lCurDriver)
  {
    for (int index = 0; index < lEngineersInYear.Count; ++index)
    {
      CareerHistoryGenerator.PersonHistoryYear personHistoryYear = lEngineersInYear[index];
      if (lCurDriver.teamHistory == personHistoryYear.teamHistory)
      {
        if (k == 0)
          ++personHistoryYear.wins;
        if (k < 3)
          ++personHistoryYear.podiums;
        personHistoryYear.points += this.mChampionship.rules.GetPointsForPosition(k + 1);
        if ((double) lCurDriver.careerFactor <= -1.0)
          ++personHistoryYear.DNFs;
      }
    }
  }

  private void AddPersonForYear(List<CareerHistoryGenerator.PersonHistoryYear> lPeople, int i, List<CareerHistoryGenerator.PersonHistoryYear> lPeopleInYear)
  {
    for (int index1 = 0; index1 < lPeople.Count; ++index1)
    {
      CareerHistoryGenerator.PersonHistoryYear personHistoryYear = lPeople[index1];
      int num = personHistoryYear.person.GetAge() - (this.mNumYears - i);
      personHistoryYear.points = 0;
      personHistoryYear.races = 0;
      personHistoryYear.wins = 0;
      personHistoryYear.podiums = 0;
      personHistoryYear.poles = 0;
      personHistoryYear.DNFs = 0;
      personHistoryYear.championships = 0;
      personHistoryYear.DNFsByError = 0;
      personHistoryYear.DNS = 0;
      personHistoryYear.isDummy = num < personHistoryYear.careerStartAge;
      if (!personHistoryYear.isDummy)
      {
        for (int val1 = 0; val1 < personHistoryYear.teamsInCareer.Count && personHistoryYear.teamsInCareer[val1].year <= i; ++val1)
        {
          int index2 = Math.Max(val1, 0);
          personHistoryYear.teamHistory = personHistoryYear.teamsInCareer[index2].team;
        }
      }
      lPeopleInYear.Add(personHistoryYear);
    }
  }

  private void SetupCareerHistory(Random lRandom, CareerHistoryGenerator.PersonHistoryYear lCurPerson)
  {
    lCurPerson.person.careerHistory.career.Clear();
    int val2 = lRandom.Next(this.mStartAgeMin, this.mStartAgeMax);
    int age = lCurPerson.person.GetAge();
    int num = Math.Min(age, val2);
    lCurPerson.years = age - num;
    lCurPerson.careerStartAge = num;
    lCurPerson.teamsInCareer = new List<CareerHistoryGenerator.PersonHistoryTeamInYear>();
  }

  private void ClearTeamsForYear()
  {
    for (int index = 0; index < this.mAllTeams.Count; ++index)
    {
      CareerHistoryGenerator.TeamHistoryYear mAllTeam = this.mAllTeams[index];
      mAllTeam.podiums = 0;
      mAllTeam.points = 0;
      mAllTeam.races = 0;
      mAllTeam.wins = 0;
    }
  }

  private void ApplyCareerHistoryForYear(List<CareerHistoryGenerator.PersonHistoryYear> inPeople, int inYear)
  {
    for (int index = 0; index < inPeople.Count; ++index)
    {
      CareerHistoryGenerator.PersonHistoryYear personHistoryYear = inPeople[index];
      if (!personHistoryYear.isDummy)
      {
        CareerHistoryEntry inEntry = new CareerHistoryEntry(new DateTime(inYear, 1, 1)) { team = personHistoryYear.teamHistory.team, championship = this.mChampionship, year = inYear, wins = personHistoryYear.wins, podiums = personHistoryYear.podiums, races = personHistoryYear.races, DNFs = personHistoryYear.DNFs, DNFsViaError = personHistoryYear.DNFsByError, DNS = personHistoryYear.DNS, careerPoints = personHistoryYear.points, championships = personHistoryYear.championships };
        inEntry.MarkEntryAsFinished(new DateTime(inYear, 12, 31));
        personHistoryYear.person.careerHistory.AddHistory(inEntry);
      }
    }
  }

  private void GetAllDrivers(List<CareerHistoryGenerator.PersonHistoryYear> lAllEngineers, List<CareerHistoryGenerator.PersonHistoryYear> lAllMechanics, List<CareerHistoryGenerator.PersonHistoryYear> lAllDrivers, List<CareerHistoryGenerator.PersonHistoryYear> lMainDrivers, List<CareerHistoryGenerator.PersonHistoryYear> lReserveDrivers)
  {
    for (int index1 = 0; index1 < this.mAllTeams.Count; ++index1)
    {
      CareerHistoryGenerator.TeamHistoryYear mAllTeam = this.mAllTeams[index1];
      Team team = mAllTeam.team;
      CareerHistoryGenerator.PersonHistoryYear personHistoryYear1 = new CareerHistoryGenerator.PersonHistoryYear() { person = (Person) team.GetDriver(0), teamHistory = mAllTeam };
      CareerHistoryGenerator.PersonHistoryYear personHistoryYear2 = new CareerHistoryGenerator.PersonHistoryYear() { person = (Person) team.GetDriver(1), teamHistory = mAllTeam };
      CareerHistoryGenerator.PersonHistoryYear personHistoryYear3 = new CareerHistoryGenerator.PersonHistoryYear() { person = (Person) team.GetDriver(2), teamHistory = mAllTeam };
      lMainDrivers.Add(personHistoryYear1);
      lMainDrivers.Add(personHistoryYear2);
      lReserveDrivers.Add(personHistoryYear3);
      lAllDrivers.Add(personHistoryYear1);
      lAllDrivers.Add(personHistoryYear2);
      lAllDrivers.Add(personHistoryYear3);
      List<Person> allPeopleOnJob1 = team.contractManager.GetAllPeopleOnJob(Contract.Job.EngineerLead);
      for (int index2 = 0; index2 < allPeopleOnJob1.Count; ++index2)
      {
        Person person = allPeopleOnJob1[index2];
        lAllEngineers.Add(new CareerHistoryGenerator.PersonHistoryYear()
        {
          person = person,
          teamHistory = mAllTeam
        });
      }
      List<Person> allPeopleOnJob2 = team.contractManager.GetAllPeopleOnJob(Contract.Job.Mechanic);
      for (int index2 = 0; index2 < allPeopleOnJob2.Count; ++index2)
      {
        Person person = allPeopleOnJob2[index2];
        lAllMechanics.Add(new CareerHistoryGenerator.PersonHistoryYear()
        {
          person = person,
          teamHistory = mAllTeam
        });
      }
    }
  }

  private void SetupTeamChangesForPeople(List<CareerHistoryGenerator.PersonHistoryYear> inPeople, Random inRandom, int inMinChangeTime, int inMaxChangeTime)
  {
    using (List<CareerHistoryGenerator.PersonHistoryYear>.Enumerator enumerator = inPeople.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        CareerHistoryGenerator.PersonHistoryYear current = enumerator.Current;
        int num1 = current.person.peakAge.Year - current.person.dateOfBirth.Year;
        int num2 = num1 + current.person.peakDuration;
        bool flag1 = current.person.GetAge() > num2;
        bool flag2 = current.person.GetAge() < num1;
        int years = current.years;
        int reputation = current.teamHistory.team.reputation;
        while (years > inMinChangeTime)
        {
          int num3 = inRandom.Next(inMinChangeTime, inMaxChangeTime);
          if (years >= num3)
          {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            CareerHistoryGenerator.\u003CSetupTeamChangesForPeople\u003Ec__AnonStorey46 peopleCAnonStorey46 = new CareerHistoryGenerator.\u003CSetupTeamChangesForPeople\u003Ec__AnonStorey46();
            int num4 = current.person.GetAge() - years;
            // ISSUE: reference to a compiler-generated field
            peopleCAnonStorey46.lTestReputation = reputation;
            // ISSUE: reference to a compiler-generated field
            peopleCAnonStorey46.lTestReputation = num4 <= num2 ? (num4 >= num1 ? (!flag1 ? reputation : (int) ((double) reputation * 1.29999995231628)) : (!flag1 ? (!flag2 ? (int) ((double) reputation * 0.800000011920929) : reputation) : (int) ((double) reputation * 1.10000002384186))) : (!flag1 ? (int) ((double) reputation * 0.75) : reputation);
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            peopleCAnonStorey46.lTestReputation = Math.Max(peopleCAnonStorey46.lTestReputation, this.mLowestTeamReputation);
            // ISSUE: reference to a compiler-generated method
            List<CareerHistoryGenerator.TeamHistoryYear> all = this.mAllTeams.FindAll(new Predicate<CareerHistoryGenerator.TeamHistoryYear>(peopleCAnonStorey46.\u003C\u003Em__42));
            int index = inRandom.Next(all.Count - 1);
            current.teamsInCareer.Add(new CareerHistoryGenerator.PersonHistoryTeamInYear()
            {
              team = all[index],
              year = this.mNumYears - years
            });
            years -= num3;
          }
          else
            break;
        }
        current.teamsInCareer.Add(new CareerHistoryGenerator.PersonHistoryTeamInYear()
        {
          team = current.teamHistory,
          year = this.mNumYears - years
        });
      }
    }
  }

  public class TeamHistoryYear
  {
    public Team team;
    public int races;
    public int wins;
    public int points;
    public int podiums;
    public int DNFs;
    public int championships;
    public int racePoints;
  }

  public class PersonHistoryTeamInYear
  {
    public CareerHistoryGenerator.TeamHistoryYear team;
    public int year;
  }

  public class PersonHistoryYear
  {
    public Person person;
    public CareerHistoryGenerator.TeamHistoryYear teamHistory;
    public bool isDummy;
    public int races;
    public int wins;
    public int points;
    public int podiums;
    public int DNFs;
    public int DNFsByError;
    public int poles;
    public int DNS;
    public int championships;
    public int racePoints;
    public int careerStartAge;
    public int years;
    public float careerFactor;
    public List<CareerHistoryGenerator.PersonHistoryTeamInYear> teamsInCareer;
  }
}
