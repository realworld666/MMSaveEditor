// Decompiled with JetBrains decompiler
// Type: SessionState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class SessionState : GameState
{
  public override GameState.Type type
  {
    get
    {
      return GameState.Type.None;
    }
  }

  public override UIManager.ScreenSet screenSet
  {
    get
    {
      return UIManager.ScreenSet.RaceEvent;
    }
  }

  public override GameState.Group group
  {
    get
    {
      return GameState.Group.Simulation;
    }
  }

  public override void OnEnter(bool fromSave = false)
  {
    Game.instance.sessionManager.SetCircuitActive(true);
    base.OnEnter(fromSave);
    this.SetAsSaveEntryPoint();
    this.SetStartingStandings();
    Game.instance.sessionManager.SessionStarting();
    if (Game.instance.sessionManager.sessionType != SessionDetails.SessionType.Race)
    {
      App.instance.cameraManager.SetTarget((Vehicle) Game.instance.vehicleManager.GetVehicle(Game.instance.player.team.GetSelectedDriver(0)), CameraManager.Transition.Instant);
      UIManager.instance.GetScreen<SessionHUD>().sponsorStatusChangeWidget.OnSessionStart();
    }
    Game.instance.time.SetSpeed(GameTimer.Speed.Slow);
  }

  public override void OnLoad()
  {
    base.OnLoad();
  }

  public override void OnReEnterFromSave()
  {
    Debug.Log((object) ("SessionState.OnReEnterFromSave: " + this.GetType().ToString()), (UnityEngine.Object) null);
    Game.instance.sessionManager.SetCircuitActive(true);
    Game.instance.time.SetSpeed(GameTimer.Speed.Slow);
    if (this.type != GameState.Type.RaceGrid)
      Game.instance.time.Pause(GameTimer.PauseType.Game);
    Game.instance.sessionManager.PrepareForSessionAfterLoad();
    App.instance.cameraManager.SetTarget((Vehicle) Game.instance.vehicleManager.GetVehicle(Game.instance.player.team.GetSelectedDriver(0)), CameraManager.Transition.Instant);
    UIManager.instance.GetScreen<SessionHUD>().sponsorStatusChangeWidget.OnSessionStart();
  }

  public override void OnExit(GameState inNextState)
  {
    base.OnExit(inNextState);
    Time.timeScale = 1f;
  }

  public override void GetFirstScreenForState(out string screenName, out UIManager.ScreenTransition screenTransition, out float transitionDuration, bool fromSave = false)
  {
    screenName = "SessionHUD";
    screenTransition = !fromSave ? UIManager.ScreenTransition.None : UIManager.ScreenTransition.Fade;
    transitionDuration = !fromSave ? 0.0f : 1.5f;
  }

  public override void SimulationUpdate()
  {
    base.SimulationUpdate();
    Game instance = Game.instance;
    instance.time.Update();
    instance.calendar.Update();
    Game.instance.vehicleManager.SimulationUpdate();
    if (!Game.instance.sessionManager.hasSessionEnded)
      this.UpdateStandings();
    Game.instance.sessionManager.SimulationUpdate();
  }

  public override void Update()
  {
    base.Update();
    this.CheckForEscapeButton();
    Game.instance.vehicleManager.Update();
  }

  public virtual void SetStartingStandings()
  {
    this.UpdateStandings();
    Game.instance.sessionManager.InformVehiclesOfTheirStandings();
  }

  protected void OrderStandingsByFastestLap()
  {
    List<RacingVehicle> standings = Game.instance.sessionManager.standings;
    RaceEventResults results = Game.instance.sessionManager.eventDetails.results;
    standings.Clear();
    int vehicleCount = Game.instance.vehicleManager.vehicleCount;
    for (int inIndex = 0; inIndex < vehicleCount; ++inIndex)
    {
      bool flag = false;
      RacingVehicle vehicle = Game.instance.vehicleManager.GetVehicle(inIndex);
      SessionTimer timer1 = vehicle.timer;
      if (timer1.lap > 0)
      {
        for (int index = 0; index < standings.Count; ++index)
        {
          RacingVehicle racingVehicle = standings[index];
          SessionTimer timer2 = racingVehicle.timer;
          if (results.GetDriverQualifyingData(racingVehicle.driver) != null || (double) timer1.GetFastestLapTime() < (double) timer2.GetFastestLapTime() || Mathf.Approximately(timer2.GetFastestLapTime(), 0.0f))
          {
            standings.Insert(index, vehicle);
            flag = true;
            break;
          }
        }
      }
      else if (results.IsDriverOutOfQualifying(vehicle.driver))
      {
        RaceEventResults.ResultData driverQualifyingData1 = results.GetDriverQualifyingData(vehicle.driver);
        for (int index = 0; index < standings.Count; ++index)
        {
          RacingVehicle racingVehicle = standings[index];
          RaceEventResults.ResultData driverQualifyingData2 = results.GetDriverQualifyingData(racingVehicle.driver);
          if (driverQualifyingData2 != null && driverQualifyingData1.gridPosition < driverQualifyingData2.gridPosition)
          {
            standings.Insert(index, vehicle);
            flag = true;
            break;
          }
        }
      }
      else
      {
        for (int index = 0; index < standings.Count; ++index)
        {
          RacingVehicle racingVehicle = standings[index];
          SessionTimer timer2 = racingVehicle.timer;
          if (results.GetDriverQualifyingData(racingVehicle.driver) != null)
          {
            standings.Insert(index, vehicle);
            flag = true;
            break;
          }
          if (Mathf.Approximately(timer2.GetFastestLapTime(), 0.0f))
          {
            switch (string.Compare(vehicle.driver.contract.GetTeam().name, racingVehicle.driver.contract.GetTeam().name, StringComparison.Ordinal))
            {
              case -1:
                standings.Insert(index, vehicle);
                flag = true;
                goto label_25;
              case 0:
                if (string.Compare(vehicle.driver.lastName, racingVehicle.driver.lastName, StringComparison.Ordinal) == -1)
                {
                  standings.Insert(index, vehicle);
                  flag = true;
                  goto label_25;
                }
                else
                  continue;
              default:
                continue;
            }
          }
        }
      }
label_25:
      if (!flag)
        standings.Add(vehicle);
    }
    if (standings.Count <= 0)
      return;
    RacingVehicle racingVehicle1 = standings[0];
    SessionTimer timer3 = racingVehicle1.timer;
    racingVehicle1.timer.SetGapToLeader(0.0f);
    racingVehicle1.timer.SetGapToAhead(0.0f);
    for (int index = 1; index < standings.Count; ++index)
    {
      SessionTimer timer1 = standings[index].timer;
      SessionTimer timer2 = standings[index - 1].timer;
      if (!Mathf.Approximately(timer1.GetFastestLapTime(), 0.0f))
      {
        timer1.SetGapToLeader(timer1.GetFastestLapTime() - timer3.GetFastestLapTime());
        timer1.SetGapToAhead(timer1.GetFastestLapTime() - timer2.GetFastestLapTime());
      }
      else
      {
        timer1.SetGapToLeader(0.0f);
        timer1.SetGapToAhead(0.0f);
      }
    }
  }

  public virtual void UpdateStandings()
  {
    this.OrderStandingsByFastestLap();
  }

  public virtual void OnSessionStarting()
  {
  }

  public virtual void OnSessionStart()
  {
  }

  public virtual void OnSessionEnding()
  {
  }

  public virtual void OnSessionEnd()
  {
  }

  protected void StartCommentarySystem()
  {
    Game.instance.sessionManager.commentaryManager.OnSessionStart();
  }

  protected void StopCommentarySystem()
  {
    Game.instance.sessionManager.commentaryManager.OnSessionEnd();
  }

  public override void OnContinueButton()
  {
    if (!(UIManager.instance.currentScreen is SessionHUD))
      Game.instance.time.UnPause(GameTimer.PauseType.UI);
    UIManager.instance.ChangeScreen("SessionHUD", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }
}
