// Decompiled with JetBrains decompiler
// Type: SessionHUBStandingsScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SessionHUBStandingsScreen : UIScreen
{
  private List<SessionHUBStandingsScreen.HUBStanding> mDriverStandings = new List<SessionHUBStandingsScreen.HUBStanding>();
  private List<SessionHUBStandingsScreen.HUBStanding> mTeamStandings = new List<SessionHUBStandingsScreen.HUBStanding>();
  public UIGridList driversGrid;
  public UIGridList teamsGrid;
  public TextMeshProUGUI raceStatus;
  public TextMeshProUGUI raceNumber;
  public GameObject headerTeams;
  public GameObject headerDrivers;
  public Toggle teamsToggle;
  public Toggle driversToggle;
  private SessionHUBStandingsScreen.Mode mMode;
  private SessionDetails.SessionType mSession;
  private Championship mChampionship;
  private ChampionshipRules mRules;
  private UIGridList mCurrentGrid;
  private bool mSessionEnded;

  public override void OnStart()
  {
    base.OnStart();
    this.driversToggle.isOn = true;
    this.teamsToggle.isOn = false;
    this.driversToggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnChangeMode(this.driversToggle, SessionHUBStandingsScreen.Mode.Drivers)));
    this.teamsToggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnChangeMode(this.teamsToggle, SessionHUBStandingsScreen.Mode.Teams)));
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.showNavigationBars = true;
    this.mSession = Game.instance.sessionManager.eventDetails.currentSession.sessionType;
    this.mSessionEnded = Game.instance.sessionManager.hasSessionEnded;
    this.LoadStandings();
    this.ActivateHeader();
    this.SetStatusTitle();
    this.ResetItems();
    this.SetGrid();
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionHome, 0.0f);
  }

  public void SetGrid()
  {
    this.PredictStandings();
    this.UpdateGrid();
  }

  public void LoadStandings()
  {
    this.mDriverStandings.Clear();
    this.mTeamStandings.Clear();
    this.mChampionship = Game.instance.sessionManager.championship;
    this.mRules = this.mChampionship.rules;
    ChampionshipStandings standings = this.mChampionship.standings;
    RaceEventResults results = Game.instance.sessionManager.eventDetails.results;
    int driverEntryCount = standings.driverEntryCount;
    int teamEntryCount = standings.teamEntryCount;
    int eventNumber = this.mChampionship.eventNumber;
    for (int inIndex = 0; inIndex < driverEntryCount; ++inIndex)
    {
      SessionHUBStandingsScreen.HUBStanding hubStanding = new SessionHUBStandingsScreen.HUBStanding();
      hubStanding.type = SessionHUBStandingsScreen.HUBStanding.Type.Driver;
      hubStanding.csEntry = standings.GetDriverEntry(inIndex);
      hubStanding.driver = hubStanding.csEntry.GetEntity<Driver>();
      hubStanding.team = hubStanding.driver.contract.GetTeam();
      hubStanding.vehicle = Game.instance.vehicleManager.GetVehicle(hubStanding.driver);
      hubStanding.championshipPoints = hubStanding.csEntry.GetCurrentPoints();
      if (this.mSession == SessionDetails.SessionType.Race && this.mSessionEnded)
      {
        RaceEventResults.ResultData resultForDriver = results.GetResultsForSession(SessionDetails.SessionType.Race).GetResultForDriver(hubStanding.driver);
        if (resultForDriver != null)
          hubStanding.championshipPoints += resultForDriver.points;
      }
      hubStanding.championshipPosition = hubStanding.csEntry.GetCurrentChampionshipPosition();
      hubStanding.eventNumber = eventNumber;
      this.mDriverStandings.Add(hubStanding);
    }
    for (int inIndex = 0; inIndex < teamEntryCount; ++inIndex)
    {
      SessionHUBStandingsScreen.HUBStanding hubStanding = new SessionHUBStandingsScreen.HUBStanding();
      hubStanding.type = SessionHUBStandingsScreen.HUBStanding.Type.Team;
      hubStanding.csEntry = standings.GetTeamEntry(inIndex);
      hubStanding.team = hubStanding.csEntry.GetEntity<Team>();
      RacingVehicle[] vehiclesByTeam = Game.instance.vehicleManager.GetVehiclesByTeam(hubStanding.team);
      hubStanding.vehicle = vehiclesByTeam[0];
      hubStanding.vehicle2 = vehiclesByTeam[1];
      hubStanding.championshipPoints = hubStanding.csEntry.GetCurrentPoints();
      hubStanding.championshipPosition = hubStanding.csEntry.GetCurrentChampionshipPosition();
      if (this.mSession == SessionDetails.SessionType.Race && this.mSessionEnded)
      {
        RaceEventResults.ResultData resultForDriver1 = results.GetResultsForSession(SessionDetails.SessionType.Race).GetResultForDriver(hubStanding.vehicle.driver);
        RaceEventResults.ResultData resultForDriver2 = results.GetResultsForSession(SessionDetails.SessionType.Race).GetResultForDriver(hubStanding.vehicle2.driver);
        if (resultForDriver1 != null && resultForDriver2 != null)
        {
          hubStanding.championshipPoints += resultForDriver1.points;
          hubStanding.championshipPoints += resultForDriver2.points;
        }
      }
      hubStanding.eventNumber = eventNumber;
      this.mTeamStandings.Add(hubStanding);
    }
  }

  public void PredictStandings()
  {
    if (this.mMode == SessionHUBStandingsScreen.Mode.Drivers)
      this.PredictDriverStandings();
    else
      this.PredictTeamStandings();
  }

  public void PredictDriverStandings()
  {
    int count = this.mDriverStandings.Count;
    for (int index = 0; index < count; ++index)
    {
      SessionHUBStandingsScreen.HUBStanding mDriverStanding = this.mDriverStandings[index];
      if (mDriverStanding.vehicle != null)
      {
        mDriverStanding.racePosition = mDriverStanding.vehicle.standingsPosition;
        if (this.mSession != SessionDetails.SessionType.Practice)
        {
          mDriverStanding.predictedPoints = mDriverStanding.championshipPoints + (this.mSessionEnded ? 0 : this.mRules.GetPointsForPosition(mDriverStanding.racePosition));
          mDriverStanding.hasFastestLap = this.mRules.fastestLapPointBonus > 0 && Game.instance.sessionManager.GetVehicleWithFastestLap() == mDriverStanding.vehicle;
          bool flag = this.mRules.qualifyingBasedActive && this.mRules.polePositionPointBonus > 0;
          mDriverStanding.hasPolePosition = flag && (this.mSession == SessionDetails.SessionType.Qualifying && mDriverStanding.racePosition == 1 || this.mSession == SessionDetails.SessionType.Race && mDriverStanding.vehicle.resultData != null && mDriverStanding.vehicle.resultData.gridPosition == 1);
          if (!this.mSessionEnded)
          {
            if (mDriverStanding.hasFastestLap)
              mDriverStanding.predictedPoints += this.mRules.fastestLapPointBonus;
            if (this.mRules.finalRacePointsDouble && this.mChampionship.GetCurrentEventDetails() == this.mChampionship.GetFinalEventDetails() && this.mChampionship.eventCount > 1)
              mDriverStanding.predictedPoints += this.mRules.GetPointsForPosition(mDriverStanding.racePosition);
            if (mDriverStanding.hasPolePosition)
              mDriverStanding.predictedPoints += this.mRules.polePositionPointBonus;
          }
        }
        else
          mDriverStanding.predictedPoints = mDriverStanding.championshipPoints;
      }
      else
      {
        mDriverStanding.racePosition = 20;
        mDriverStanding.predictedPoints = mDriverStanding.championshipPoints;
      }
    }
    this.mDriverStandings.Sort();
    for (int index = 0; index < this.mDriverStandings.Count; ++index)
      Debug.Log((object) (index.ToString() + " - " + this.mDriverStandings[index].driver.name + " - " + this.mDriverStandings[index].predictedPoints.ToString()), (UnityEngine.Object) null);
  }

  public void PredictTeamStandings()
  {
    int count = this.mTeamStandings.Count;
    for (int index = 0; index < count; ++index)
    {
      SessionHUBStandingsScreen.HUBStanding mTeamStanding = this.mTeamStandings[index];
      mTeamStanding.racePosition = mTeamStanding.vehicle.standingsPosition;
      mTeamStanding.racePosition2 = mTeamStanding.vehicle2.standingsPosition;
      if (this.mSession != SessionDetails.SessionType.Practice)
      {
        mTeamStanding.predictedPoints = mTeamStanding.championshipPoints + (this.mSessionEnded ? 0 : this.mRules.GetPointsForPosition(mTeamStanding.racePosition) + this.mRules.GetPointsForPosition(mTeamStanding.racePosition2));
        Vehicle vehicleWithFastestLap = (Vehicle) Game.instance.sessionManager.GetVehicleWithFastestLap();
        mTeamStanding.hasFastestLap = this.mRules.fastestLapPointBonus > 0 && (vehicleWithFastestLap == mTeamStanding.vehicle || vehicleWithFastestLap == mTeamStanding.vehicle2);
        bool flag1 = this.mRules.qualifyingBasedActive && this.mRules.polePositionPointBonus > 0;
        bool flag2 = this.mSession == SessionDetails.SessionType.Qualifying && mTeamStanding.racePosition == 1 || this.mSession == SessionDetails.SessionType.Race && mTeamStanding.vehicle.resultData != null && mTeamStanding.vehicle.resultData.gridPosition == 1;
        bool flag3 = this.mSession == SessionDetails.SessionType.Qualifying && mTeamStanding.racePosition2 == 1 || this.mSession == SessionDetails.SessionType.Race && mTeamStanding.vehicle2.resultData != null && mTeamStanding.vehicle2.resultData.gridPosition == 1;
        mTeamStanding.hasPolePosition = flag1 && (flag2 || flag3);
        if (!this.mSessionEnded)
        {
          if (mTeamStanding.hasFastestLap)
            mTeamStanding.predictedPoints += this.mRules.fastestLapPointBonus;
          if (this.mRules.finalRacePointsDouble && this.mChampionship.GetCurrentEventDetails() == this.mChampionship.GetFinalEventDetails() && this.mChampionship.eventCount > 1)
          {
            mTeamStanding.predictedPoints += this.mRules.GetPointsForPosition(mTeamStanding.racePosition);
            mTeamStanding.predictedPoints += this.mRules.GetPointsForPosition(mTeamStanding.racePosition2);
          }
          if (mTeamStanding.hasPolePosition)
            mTeamStanding.predictedPoints += this.mRules.polePositionPointBonus;
        }
      }
      else
        mTeamStanding.predictedPoints = mTeamStanding.championshipPoints;
    }
    this.mTeamStandings.Sort();
  }

  public void ChangeMode(SessionHUBStandingsScreen.Mode inMode, bool inForce)
  {
    if (this.mMode == inMode && !inForce)
      return;
    this.mMode = inMode;
    this.ActivateHeader();
    this.ResetItems();
    this.SetGrid();
  }

  public void ActivateHeader()
  {
    this.headerTeams.SetActive(this.mMode == SessionHUBStandingsScreen.Mode.Teams);
    this.headerDrivers.SetActive(this.mMode == SessionHUBStandingsScreen.Mode.Drivers);
  }

  public void SetStatusTitle()
  {
    this.raceStatus.text = this.mSessionEnded ? Localisation.LocaliseID("PSG_10010596", (GameObject) null) : Localisation.LocaliseID("PSG_10010595", (GameObject) null);
    StringVariableParser.intValue1 = Game.instance.sessionManager.championship.eventNumber + 1;
    StringVariableParser.intValue2 = Game.instance.sessionManager.championship.eventCount;
    this.raceNumber.text = Localisation.LocaliseID("PSG_10002217", (GameObject) null);
  }

  public void ResetItems()
  {
    if ((UnityEngine.Object) this.mCurrentGrid != (UnityEngine.Object) null)
      this.mCurrentGrid.DestroyListItems();
    GameUtility.SetActive(this.driversGrid.itemPrefab, false);
    GameUtility.SetActive(this.teamsGrid.itemPrefab, false);
  }

  public void UpdateGrid()
  {
    int a = this.mMode != SessionHUBStandingsScreen.Mode.Drivers ? this.mTeamStandings.Count : this.mDriverStandings.Count;
    this.mCurrentGrid = this.mMode != SessionHUBStandingsScreen.Mode.Drivers ? this.teamsGrid : this.driversGrid;
    int itemCount = this.mCurrentGrid.itemCount;
    int num = Mathf.Max(a, itemCount);
    for (int inIndex = 0; inIndex < num; ++inIndex)
    {
      UISessionHUBStandingsEntry hubStandingsEntry = this.mCurrentGrid.GetOrCreateItem<UISessionHUBStandingsEntry>(inIndex);
      GameUtility.SetActive(hubStandingsEntry.gameObject, inIndex < a);
      if (hubStandingsEntry.gameObject.activeSelf)
      {
        hubStandingsEntry.OnStart();
        SessionHUBStandingsScreen.HUBStanding inStanding;
        if (this.mMode == SessionHUBStandingsScreen.Mode.Drivers)
        {
          inStanding = this.mDriverStandings[inIndex];
          hubStandingsEntry.barType = this.GetBarType(inStanding.driver, inIndex);
          hubStandingsEntry.button.interactable = inStanding.vehicle != null;
        }
        else
        {
          inStanding = this.mTeamStandings[inIndex];
          hubStandingsEntry.barType = this.GetBarType(inStanding.team, inIndex);
        }
        inStanding.predictedPosition = inIndex + 1;
        inStanding.sessionType = this.mSession;
        inStanding.isSessionEnd = this.mSessionEnded;
        hubStandingsEntry.Setup(inStanding);
      }
    }
    this.mCurrentGrid.ResetScrollbar();
  }

  public void OnChangeMode(Toggle inToggle, SessionHUBStandingsScreen.Mode inMode)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (!inToggle.isOn || this.mMode == inMode)
      return;
    this.ChangeMode(inMode, false);
  }

  public UISessionHUBStandingsEntry.BarType GetBarType(Driver inDriver, int inIndex)
  {
    if (inDriver.IsPlayersDriver())
      return UISessionHUBStandingsEntry.BarType.PlayerOwned;
    return inIndex % 2 == 0 ? UISessionHUBStandingsEntry.BarType.Lighter : UISessionHUBStandingsEntry.BarType.Darker;
  }

  public UISessionHUBStandingsEntry.BarType GetBarType(Team inTeam, int inIndex)
  {
    if (inTeam.IsPlayersTeam())
      return UISessionHUBStandingsEntry.BarType.PlayerOwned;
    return inIndex % 2 == 0 ? UISessionHUBStandingsEntry.BarType.Lighter : UISessionHUBStandingsEntry.BarType.Darker;
  }

  public class HUBStanding : IComparable<SessionHUBStandingsScreen.HUBStanding>
  {
    public SessionHUBStandingsScreen.HUBStanding.Type type;
    public Driver driver;
    public Team team;
    public RacingVehicle vehicle;
    public RacingVehicle vehicle2;
    public ChampionshipEntry_v1 csEntry;
    public int eventNumber;
    public int racePosition;
    public int racePosition2;
    public int championshipPosition;
    public int championshipPoints;
    public int predictedPoints;
    public int predictedPosition;
    public bool hasFastestLap;
    public bool hasPolePosition;
    public SessionDetails.SessionType sessionType;
    public bool isSessionEnd;

    public int CompareTo(SessionHUBStandingsScreen.HUBStanding inOther)
    {
      int num1 = 0;
      if (inOther == null)
        num1 = -1;
      int num2 = inOther.predictedPoints.CompareTo(this.predictedPoints);
      if (num2 == 0)
      {
        if (this.csEntry.races > 0 && inOther.csEntry.races > 0)
        {
          for (int inPosition = 1; inPosition < 21; ++inPosition)
          {
            num2 = inOther.csEntry.GetNumberOfPositions(inPosition).CompareTo(this.csEntry.GetNumberOfPositions(inPosition));
            if (num2 != 0)
              break;
          }
        }
        else if (this.csEntry.races == 0 && inOther.csEntry.races > 0 || this.csEntry.races > 0 && inOther.csEntry.races == 0)
          num2 = inOther.csEntry.races.CompareTo(this.csEntry.races);
        if (num2 == 0)
        {
          if (this.type == SessionHUBStandingsScreen.HUBStanding.Type.Driver)
          {
            num2 = this.racePosition.CompareTo(inOther.racePosition);
            if (num2 == 0)
              num2 = string.Compare(this.driver.name, inOther.driver.name);
          }
          else
          {
            num2 = (this.racePosition + this.racePosition2).CompareTo(inOther.racePosition + inOther.racePosition2);
            if (num2 == 0)
              num2 = string.Compare(this.team.name, inOther.team.name);
          }
        }
      }
      return num2;
    }

    public enum Type
    {
      Driver,
      Team,
    }
  }

  public enum Mode
  {
    Drivers,
    Teams,
  }
}
