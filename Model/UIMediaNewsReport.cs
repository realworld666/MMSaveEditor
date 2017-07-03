// Decompiled with JetBrains decompiler
// Type: UIMediaNewsReport
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UIMediaNewsReport : MonoBehaviour
{
  public TextMeshProUGUI sessionLabel;
  public TextMeshProUGUI headlineLabel;
  public TextMeshProUGUI bodyLabel;
  public TextMeshProUGUI writerLabel;
  public UICharacterPortrait portrait;

  private void Awake()
  {
  }

  public void GenerateNewsReportForSession()
  {
    switch (Game.instance.sessionManager.eventDetails.currentSession.sessionType)
    {
      case SessionDetails.SessionType.Practice:
        this.GenerateForPractice();
        break;
      case SessionDetails.SessionType.Qualifying:
        this.GenerateForQualifying();
        break;
      case SessionDetails.SessionType.Race:
        this.GenerateForRace();
        break;
    }
    this.SetJournalist();
  }

  private void GenerateForPractice()
  {
    this.sessionLabel.text = "Post Practice Reactions";
    this.headlineLabel.text = "Headline";
    this.bodyLabel.text = "<size=24>" + Localisation.GetTextFromGroup("PressureOnManager") + "\n\n<size=18>" + Localisation.GetTextFromGroup("PressureOnManager") + "\n\n" + Localisation.GetTextFromGroup("PressureOnManager");
  }

  private void GenerateForQualifying()
  {
    this.sessionLabel.text = "Post Qualifying Reactions";
    Championship championship = Game.instance.sessionManager.championship;
    Driver driver1 = Game.instance.sessionManager.standings[0].driver;
    Team team1 = Game.instance.sessionManager.standings[0].driver.contract.GetTeam();
    Driver driver2 = Game.instance.sessionManager.standings[1].driver;
    Driver driver3 = Game.instance.sessionManager.standings[2].driver;
    string name = Game.instance.player.name;
    Team team2 = Game.instance.player.team;
    Driver driver4 = Game.instance.player.team.GetDriver(0);
    Driver driver5 = Game.instance.player.team.GetDriver(1);
    int qualifyingPosition1 = driver4.GetChampionshipEntry().GetLatestQualifyingPosition();
    int qualifyingPosition2 = driver5.GetChampionshipEntry().GetLatestQualifyingPosition();
    Team entity1 = championship.standings.GetTeamEntry(0).GetEntity<Team>();
    ChampionshipEntry_v1 driverEntry1 = championship.standings.GetDriverEntry(0);
    Driver entity2 = driverEntry1.GetEntity<Driver>();
    int qualifyingPosition3 = driverEntry1.GetLatestQualifyingPosition();
    ChampionshipEntry_v1 driverEntry2 = championship.standings.GetDriverEntry(1);
    Driver entity3 = driverEntry2.GetEntity<Driver>();
    int qualifyingPosition4 = driverEntry2.GetLatestQualifyingPosition();
    int num = driverEntry1.GetCurrentPoints() - driverEntry2.GetCurrentPoints();
    string championshipName = championship.GetChampionshipName(false);
    float fastestLapTime = Game.instance.sessionManager.standings[0].timer.GetFastestLapTime();
    float inTimeGap = Game.instance.sessionManager.standings[0].timer.GetFastestLapTime() - Game.instance.sessionManager.standings[1].timer.GetFastestLapTime();
    string locationName = Game.instance.sessionManager.eventDetails.circuit.locationName;
    int length = 4;
    string[] strArray = new string[length];
    strArray[0] = Localisation.GetTextFromGroup("QualiResultsHeadline");
    strArray[1] = Localisation.GetTextFromGroup("QualiResults1");
    strArray[2] = Localisation.GetTextFromGroup("QualiResults2");
    strArray[3] = driver1.IsPlayersDriver() || driver2.IsPlayersDriver() || driver3.IsPlayersDriver() ? Localisation.GetTextFromGroup("QualiResults3") : Localisation.GetTextFromGroup("QualiResultsPlayerBelowThird3");
    for (int index = 0; index < length; ++index)
      strArray[index] = Localisation.ProcessVariables(strArray[index], "FirstPlaceDriverName", driver1.name, "FirstPlaceDriverLastName", driver1.lastName, "FirstPlaceDriverTeamName", team1.name, "SecondPlaceDriverName", driver2.name, "SecondPlaceDriverTeamName", driver2.contract.GetTeam().name, "ThirdPlaceDriverName", driver3.name, "ThirdPlaceDriverTeamName", driver3.contract.GetTeam().name, "PlayerName", name, "PlayerTeam", team2.name, "PlayerDriver1", driver4.name, "PlayerDriver2", driver5.name, "PlayerDriver1Position", GameUtility.FormatForPosition(qualifyingPosition1, (string) null), "PlayerDriver2Position", GameUtility.FormatForPosition(qualifyingPosition2, (string) null), "DriversChampionshipLeaderName", entity2.name, "DriversChampionshipLeaderPosition", GameUtility.FormatForPosition(qualifyingPosition3, (string) null), "DriversChampionshipLeaderNationality", entity2.nationality.localisedCountry, "DriversChampionshipLeaderTeamName", entity1.name, "DriversChampionshipSecondName", entity3.name, "DriversChampionshipSecondPosition", GameUtility.FormatForPosition(qualifyingPosition4, (string) null), "RivalDriverLastName", entity3.lastName, "WinningManagerName", driver1.contract.GetTeam().contractManager.GetPersonOnJob(Contract.Job.TeamPrincipal).name, "Location", locationName, "FirstPlaceLaptime", GameUtility.GetLapTimeText(fastestLapTime, false), "Gap1stTo2nd", GameUtility.GetGapTimeText(inTimeGap, false), "PointsGap", num.ToString(), "ChampionshipName", championshipName);
    this.headlineLabel.text = strArray[0];
    this.bodyLabel.text = "<size=24>" + strArray[1] + "\n\n<size=18>" + strArray[2] + "\n\n" + strArray[3];
  }

  private void GenerateForRace()
  {
    this.sessionLabel.text = "Post Race Reactions";
    Championship championship = Game.instance.sessionManager.championship;
    Driver driver1 = Game.instance.sessionManager.standings[0].driver;
    Team team1 = Game.instance.sessionManager.standings[0].driver.contract.GetTeam();
    Driver driver2 = Game.instance.sessionManager.standings[1].driver;
    Driver driver3 = Game.instance.sessionManager.standings[2].driver;
    string name = Game.instance.player.name;
    Team team2 = Game.instance.player.team;
    Driver driver4 = Game.instance.player.team.GetDriver(0);
    Driver driver5 = Game.instance.player.team.GetDriver(1);
    int finalRacePosition1 = driver4.GetChampionshipEntry().GetFinalRacePosition();
    int finalRacePosition2 = driver5.GetChampionshipEntry().GetFinalRacePosition();
    Team entity1 = championship.standings.GetTeamEntry(0).GetEntity<Team>();
    ChampionshipEntry_v1 driverEntry1 = championship.standings.GetDriverEntry(0);
    Driver entity2 = driverEntry1.GetEntity<Driver>();
    int finalRacePosition3 = driverEntry1.GetFinalRacePosition();
    ChampionshipEntry_v1 driverEntry2 = championship.standings.GetDriverEntry(1);
    Driver entity3 = driverEntry2.GetEntity<Driver>();
    int finalRacePosition4 = driverEntry2.GetFinalRacePosition();
    int num = driverEntry1.GetCurrentPoints() - driverEntry2.GetCurrentPoints();
    string championshipName = championship.GetChampionshipName(false);
    float fastestLapTime = Game.instance.sessionManager.standings[0].timer.GetFastestLapTime();
    float inTimeGap = Game.instance.sessionManager.standings[0].timer.GetFastestLapTime() - Game.instance.sessionManager.standings[1].timer.GetFastestLapTime();
    string locationName = Game.instance.sessionManager.eventDetails.circuit.locationName;
    int length = 4;
    string[] strArray = new string[length];
    if (num >= 25 || Game.instance.sessionManager.championship.eventNumberForUI == Game.instance.sessionManager.championship.eventCount)
    {
      strArray[0] = Localisation.GetTextFromGroup("DriversChampionHeadline");
      strArray[1] = Localisation.GetTextFromGroup("DriversChampion1");
      if (num > 0)
      {
        strArray[2] = Localisation.GetTextFromGroup("DriversChampion2");
        strArray[3] = Localisation.GetTextFromGroup("DriversChampion3");
      }
      else
      {
        strArray[2] = Localisation.GetTextFromGroup("DriversChampion2Equal");
        strArray[3] = Localisation.GetTextFromGroup("DriversChampion3");
      }
    }
    else
    {
      strArray[0] = Localisation.GetTextFromGroup("RaceWinHeadline");
      strArray[1] = Localisation.GetTextFromGroup("RaceWin1");
      strArray[2] = Localisation.GetTextFromGroup("RaceWin2");
      strArray[3] = driver1.IsPlayersDriver() || driver2.IsPlayersDriver() || driver3.IsPlayersDriver() ? Localisation.GetTextFromGroup("RaceWin3") : Localisation.GetTextFromGroup("RaceWinPlayerBelowThird3");
    }
    for (int index = 0; index < length; ++index)
      strArray[index] = Localisation.ProcessVariables(strArray[index], "FirstPlaceDriverName", driver1.name, "SecondPlaceDriverName", driver2.name, "ThirdPlaceDriverName", driver3.name, "FirstPlaceDriverLastName", driver1.lastName, "FirstPlaceDriverTeamName", team1.name, "PlayerName", name, "PlayerTeam", team2.name, "PlayerDriver1", driver4.name, "PlayerDriver2", driver5.name, "PlayerDriver1Position", GameUtility.FormatForPosition(finalRacePosition1, (string) null), "PlayerDriver2Position", GameUtility.FormatForPosition(finalRacePosition2, (string) null), "DriversChampionshipLeaderName", entity2.name, "DriversChampionshipLeaderLastName", entity2.lastName, "DriversChampionshipLeaderPosition", GameUtility.FormatForPosition(finalRacePosition3, (string) null), "DriversChampionshipLeaderNationality", entity2.nationality.localisedCountry, "DriversChampionshipLeaderTeamName", entity1.name, "DriversChampionshipSecondName", entity3.name, "DriversChampionshipSecondPosition", GameUtility.FormatForPosition(finalRacePosition4, (string) null), "RivalDriverLastName", entity3.lastName, "WinningManagerName", driver1.contract.GetTeam().contractManager.GetPersonOnJob(Contract.Job.TeamPrincipal).name, "Location", locationName, "FirstPlaceLaptime", GameUtility.GetLapTimeText(fastestLapTime, false), "Gap1stTo2nd", GameUtility.GetGapTimeText(inTimeGap, false), "PointsGap", num.ToString(), "ChampionshipName", championshipName);
    this.headlineLabel.text = strArray[0];
    this.bodyLabel.text = "<size=24>" + strArray[1] + "\n\n<size=18>" + strArray[2] + "\n\n" + strArray[3];
  }

  private void SetJournalist()
  {
    this.writerLabel.text = "<size=22>BBC SPORTS\n<size=18>" + Game.instance.time.now.ToLongDateString() + "\nMARK MONKFISH";
  }
}
