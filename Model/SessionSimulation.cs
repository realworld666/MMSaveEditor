// Decompiled with JetBrains decompiler
// Type: SessionSimulation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

public class SessionSimulation
{
  public static void SimulateEvent(Championship inChampionship)
  {
    GameUtility.Assert(App.instance.saveSystem.status != SaveSystem.Status.Saving, "SimulateEvent called during save can corrupt the savedata!", (UnityEngine.Object) null);
    Game.instance.sessionManager.SetChampionship(inChampionship);
    Game.instance.persistentEventData.OnEventStart();
    bool flag = Game.IsSimulatingSeason && inChampionship.isPlayerChampionship;
    while (!inChampionship.GetCurrentEventDetails().hasEventEnded)
    {
      SessionSimulation.SimulateNextSession(inChampionship, false);
      inChampionship.GoToNextSession();
      if (flag && inChampionship.GetCurrentEventDetails().hasEventEnded)
        Game.instance.dialogSystem.OnEventEndMessages();
    }
    inChampionship.EndEvent();
    Game.instance.persistentEventData.OnEventEnd();
    Game.instance.vehicleManager.RemoveVehicles();
  }

  public static void SimulateNextSession(Championship inChampionship, bool hasSessionStarted = false)
  {
    SessionManager sessionManager = Game.instance.sessionManager;
    sessionManager.eventDetails.currentSession.isSessionSimulated = true;
    GameUtility.Assert(App.instance.saveSystem.status != SaveSystem.Status.Saving, "SimulateNextSession called during save can corrupt the savedata!", (UnityEngine.Object) null);
    sessionManager.SetChampionship(inChampionship);
    if (!hasSessionStarted)
    {
      inChampionship.SetupAITeamsForEvent();
      inChampionship.OnSessionStart();
    }
    sessionManager.SetSimulateSessionWeather();
    if (Game.instance.tutorialSystem.isTutorialFirstRaceActive)
      UITutorialWeatherControl.ClearSky();
    if (!hasSessionStarted)
    {
      Game.instance.vehicleManager.RemoveVehicles();
      if (App.instance.gameStateManager.currentState.IsFrontend())
      {
        Game.instance.vehicleManager.CreateVehiclesForSession(inChampionship, VehicleManager.Visuals.DontCreate);
      }
      else
      {
        sessionManager.PrepareForSession();
        Game.instance.vehicleManager.CreateVehiclesForSession(inChampionship, VehicleManager.Visuals.Create);
        Game.instance.vehicleManager.PrepareForSession();
        Game.instance.vehicleManager.OnSessionStart();
      }
    }
    List<RacingVehicle> allVehicles = Game.instance.vehicleManager.GetAllVehicles();
    List<RacingVehicle> inStandings = sessionManager.standings;
    List<RaceEventResults.ResultData> outResultData = new List<RaceEventResults.ResultData>();
    inStandings.Clear();
    inStandings.InsertRange(0, (IEnumerable<RacingVehicle>) allVehicles);
    if (!inChampionship.rules.qualifyingBasedActive && sessionManager.sessionType == SessionDetails.SessionType.Qualifying)
    {
      inStandings = SessionSimulation.GetStandingsForQualifying(inChampionship, inStandings);
      for (int index = 0; index < inStandings.Count; ++index)
      {
        RacingVehicle racingVehicle = inStandings[index];
        RaceEventResults.ResultData resultData = new RaceEventResults.ResultData();
        if (racingVehicle.resultData == null)
          racingVehicle.resultData = resultData;
        else
          resultData = new RaceEventResults.ResultData(racingVehicle.resultData);
        resultData.driver = racingVehicle.driver;
        resultData.gridPosition = racingVehicle.stats.qualifyingPosition;
        racingVehicle.resultData = resultData;
      }
    }
    else
    {
      sessionManager.simulationUtility.FrontEndSimulation(inChampionship, ref inStandings, sessionManager.eventDetails, out outResultData);
      for (int index = 0; index < inStandings.Count; ++index)
      {
        RacingVehicle racingVehicle = inStandings[index];
        RaceEventResults.ResultData resultData1 = new RaceEventResults.ResultData();
        RaceEventResults.ResultData resultData2 = outResultData[index];
        racingVehicle.resultData = resultData2;
      }
    }
    if (sessionManager.sessionType == SessionDetails.SessionType.Qualifying)
      SessionSimulation.SetStandingsOptionsForQuickRace(ref inStandings);
    sessionManager.SetStandings(inStandings);
    sessionManager.InformVehiclesOfTheirStandings();
    inChampionship.OnSessionEnd();
    if (inChampionship == Game.instance.player.team.championship)
    {
      SessionDetails nextSession = Game.instance.sessionManager.eventDetails.nextSession;
      if (nextSession != null && !(App.instance.gameStateManager.currentState is SkipSessionState))
        Game.instance.time.SetTime(nextSession.sessionDateTime);
    }
    if (!inChampionship.isPlayerChampionship)
      inStandings.Clear();
    if (!App.instance.gameStateManager.currentState.IsFrontend())
      return;
    Game.instance.vehicleManager.RemoveVehicles();
  }

  private static void SetStandingsOptionsForQuickRace(ref List<RacingVehicle> inStandings)
  {
    if (Game.instance.gameType != Game.GameType.SingleEvent)
      return;
    QuickRaceSetupState state = (QuickRaceSetupState) App.instance.gameStateManager.GetState(GameState.Type.QuickRaceSetup);
    if (state.raceWeekend != QuickRaceSetupState.RaceWeekend.RaceOnly || state.gridOptions != QuickRaceSetupState.GridOptions.Predefined)
      return;
    SessionManager sessionManager = Game.instance.sessionManager;
    int sessionId = sessionManager.eventDetails.sessionID;
    bool flag = sessionManager.sessionType == SessionDetails.SessionType.Qualifying && sessionManager.eventDetails.hasSeveralQualifyingSessions && sessionId > 0;
    RacingVehicle[] racingVehicleArray = new RacingVehicle[inStandings.Count];
    if (flag)
    {
      RaceEventResults.SessonResultData sessonResultData = sessionManager.eventDetails.results.GetAllResultsForSession(SessionDetails.SessionType.Qualifying)[0];
      int positionThreshold1 = RaceEventResults.GetPositionThreshold(sessionId);
      int positionThreshold2 = RaceEventResults.GetPositionThreshold(sessionId - 1);
      int num = positionThreshold2 != 0 ? positionThreshold2 : int.MaxValue;
      for (int index1 = 0; index1 < inStandings.Count; ++index1)
      {
        RacingVehicle racingVehicle = inStandings[index1];
        int index2 = sessonResultData.GetResultForDriver(racingVehicle.driver).position - 1;
        racingVehicleArray[index2] = racingVehicle;
      }
      for (int index = 0; index < racingVehicleArray.Length; ++index)
      {
        RaceEventResults.ResultData resultData = new RaceEventResults.ResultData(racingVehicleArray[index].resultData);
        resultData.CopyRacingData(sessonResultData.resultData[index]);
        racingVehicleArray[index].resultData = resultData;
      }
      for (int index = 0; index < racingVehicleArray.Length; ++index)
        racingVehicleArray[index].resultData.qualifyingPositionRecorded = index >= positionThreshold1 && index < num;
      inStandings = new List<RacingVehicle>((IEnumerable<RacingVehicle>) racingVehicleArray);
    }
    else
    {
      for (int index1 = 0; index1 < inStandings.Count; ++index1)
      {
        RacingVehicle racingVehicle1 = inStandings[index1];
        if (racingVehicle1.driver.IsPlayersDriver())
        {
          int index2 = racingVehicle1.carID != 0 ? state.driver2GridPosition - 1 : state.driver1GridPosition - 1;
          RacingVehicle racingVehicle2 = inStandings[index2];
          RaceEventResults.ResultData inResultData = new RaceEventResults.ResultData(racingVehicle2.resultData);
          inStandings[index2] = racingVehicle1;
          inStandings[index1] = racingVehicle2;
          inStandings[index1].resultData.CopyRacingData(racingVehicle1.resultData);
          inStandings[index2].resultData.CopyRacingData(inResultData);
        }
      }
    }
  }

  public static List<RacingVehicle> GetStandingsForQualifying(Championship inChampionship, List<RacingVehicle> inVehicles)
  {
    List<RacingVehicle> inStandings = new List<RacingVehicle>();
    List<RacingVehicle> inVehicles1 = new List<RacingVehicle>((IEnumerable<RacingVehicle>) inVehicles);
    switch (inChampionship.rules.gridSetup)
    {
      case ChampionshipRules.GridSetup.Random:
        inStandings.InsertRange(0, (IEnumerable<RacingVehicle>) inVehicles);
        SessionSimulation.RandomiseStandings(inStandings);
        break;
      case ChampionshipRules.GridSetup.InvertedDriverChampionship:
        int count = inVehicles1.Count;
        List<ChampionshipEntry_v1> activeDriverList = inChampionship.standings.GetActiveDriverList();
        for (int index = 0; index < count; ++index)
        {
          ChampionshipEntry_v1 championshipEntryV1 = activeDriverList[index];
          Driver entity = championshipEntryV1.GetEntity<Driver>();
          if (entity.contract.GetTeam().championship == inChampionship)
          {
            RacingVehicle vehicle = VehicleManager.GetVehicle(entity, inVehicles1);
            vehicle.stats.qualifyingPosition = 20 - (championshipEntryV1.GetCurrentChampionshipPosition() - 1);
            vehicle.driver = entity;
            inStandings.Insert(0, vehicle);
          }
        }
        inStandings.Sort((Comparison<RacingVehicle>) ((x, y) => x.stats.qualifyingPosition.CompareTo(y.stats.qualifyingPosition)));
        for (int index = 0; index < inStandings.Count; ++index)
          inStandings[index].stats.qualifyingPosition = index + 1;
        break;
    }
    return inStandings;
  }

  public static void RandomiseStandings(List<RacingVehicle> inStandings)
  {
    int count = inStandings.Count;
    System.Random random = new System.Random();
    while (count > 1)
    {
      int index = random.Next(0, count) % count;
      --count;
      RacingVehicle inStanding = inStandings[index];
      inStandings[index] = inStandings[count];
      inStandings[index].stats.qualifyingPosition = index + 1;
      inStandings[count] = inStanding;
      inStandings[count].stats.qualifyingPosition = count + 1;
    }
    if (Game.instance.gameType != Game.GameType.SingleEvent)
      return;
    QuickRaceSetupState state = (QuickRaceSetupState) App.instance.gameStateManager.GetState(GameState.Type.QuickRaceSetup);
    if (state.raceWeekend != QuickRaceSetupState.RaceWeekend.RaceOnly || state.gridOptions != QuickRaceSetupState.GridOptions.Predefined)
      return;
    for (int index1 = 0; index1 < inStandings.Count; ++index1)
    {
      RacingVehicle inStanding1 = inStandings[index1];
      if (inStanding1.driver.IsPlayersDriver())
      {
        int index2 = inStanding1.carID != 0 ? state.driver2GridPosition - 1 : state.driver1GridPosition - 1;
        RacingVehicle inStanding2 = inStandings[index2];
        inStandings[index2] = inStanding1;
        inStandings[index1] = inStanding2;
      }
    }
  }
}
