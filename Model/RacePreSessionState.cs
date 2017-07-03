// Decompiled with JetBrains decompiler
// Type: RacePreSessionState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

public class RacePreSessionState : PreSessionState
{
  public override GameState.Type type
  {
    get
    {
      return GameState.Type.RacePreSession;
    }
  }

  public override void OnEnter(bool fromSave = false)
  {
    TutorialSystem_v1 tutorialSystem = Game.instance.tutorialSystem;
    bool flag = tutorialSystem.isTutorialActive && !tutorialSystem.IsTutorialSectionComplete(TutorialSystem_v1.TutorialSection.HasRunFirstRace);
    QuickRaceSetupState state = (QuickRaceSetupState) App.instance.gameStateManager.GetState(GameState.Type.QuickRaceSetup);
    if (flag || Game.instance.gameType == Game.GameType.SingleEvent && state.raceWeekend == QuickRaceSetupState.RaceWeekend.RaceOnly)
    {
      Championship championship = Game.instance.player.team.championship;
      while (championship.GetCurrentEventDetails().currentSession.sessionType != SessionDetails.SessionType.Race)
      {
        SessionSimulation.SimulateNextSession(championship, false);
        championship.GetCurrentEventDetails().GoToNextSession();
      }
      Game.instance.vehicleManager.RemoveVehicles();
    }
    else
    {
      Championship championship = Game.instance.player.team.championship;
      if (!championship.rules.qualifyingBasedActive)
      {
        SessionSimulation.SimulateNextSession(championship, false);
        championship.GetCurrentEventDetails().GoToNextSession();
        Game.instance.vehicleManager.RemoveVehicles();
      }
    }
    base.OnEnter(fromSave);
    this.PlaceCarsOnGrid();
  }

  public override void OnReEnterFromSave()
  {
    base.OnReEnterFromSave();
    VehicleManager vehicleManager = Game.instance.vehicleManager;
    for (int inIndex = 0; inIndex < vehicleManager.vehicleCount; ++inIndex)
    {
      RacingVehicle vehicle = vehicleManager.GetVehicle(inIndex);
      if (vehicle.pathState.stateType == PathStateManager.StateType.Grid)
        Game.instance.sessionManager.circuit.GetGridBox(vehicle.standingsPosition).PreparForRaceStart(vehicle);
      else
        vehicle.pathState.ChangeState(PathStateManager.StateType.Grid);
    }
  }

  public override void OnExit(GameState inNextState)
  {
    base.OnExit(inNextState);
  }

  private void PlaceCarsOnGrid()
  {
    List<RacingVehicle> standings = Game.instance.sessionManager.standings;
    standings.Clear();
    List<RaceEventResults.ResultData> resultData1 = Game.instance.sessionManager.eventDetails.results.GetResultsForSession(SessionDetails.SessionType.Qualifying).resultData;
    int vehicleCount = Game.instance.vehicleManager.vehicleCount;
    int count = resultData1.Count;
    if (vehicleCount != count)
      Debug.LogException(new Exception("Qualifying results don't match grid size"));
    VehicleManager vehicleManager = Game.instance.vehicleManager;
    for (int index = 0; index < count; ++index)
    {
      RaceEventResults.ResultData resultData2 = resultData1[index];
      RacingVehicle racingVehicle = vehicleManager.GetVehicle(resultData2.driver) ?? RaceEventResults.GetVehicle(resultData2.driver, resultData1);
      racingVehicle.stats.qualifyingPosition = index + 1;
      standings.Add(racingVehicle);
    }
    TutorialSystem_v1 tutorialSystem = Game.instance.tutorialSystem;
    if (tutorialSystem.isTutorialActive && !tutorialSystem.IsTutorialSectionComplete(TutorialSystem_v1.TutorialSection.HasRunFirstRace))
      this.TutorialMovePlayersDriversInStandings();
    Game.instance.sessionManager.InformVehiclesOfTheirStandings();
    for (int inIndex = 0; inIndex < vehicleCount; ++inIndex)
      Game.instance.vehicleManager.GetVehicle(inIndex).pathState.ChangeState(PathStateManager.StateType.Grid);
  }

  private void TutorialMovePlayersDriversInStandings()
  {
    List<RacingVehicle> standings = Game.instance.sessionManager.standings;
    RacingVehicle vehicle1 = Game.instance.vehicleManager.GetVehicle(Game.instance.player.team.GetDriver(0));
    RacingVehicle vehicle2 = Game.instance.vehicleManager.GetVehicle(Game.instance.player.team.GetDriver(1));
    if (vehicle1 == null || vehicle2 == null)
      return;
    standings.Remove(vehicle1);
    standings.Remove(vehicle2);
    standings.Insert(1, vehicle2);
    standings.Insert(4, vehicle1);
  }
}
