// Decompiled with JetBrains decompiler
// Type: RaceSessionState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

public class RaceSessionState : SessionState
{
  public override GameState.Type type
  {
    get
    {
      return GameState.Type.Race;
    }
  }

  public override void OnEnter(bool fromSave = false)
  {
    base.OnEnter(fromSave);
    Game.instance.vehicleManager.OnSessionStart();
    int vehicleCount = Game.instance.vehicleManager.vehicleCount;
    for (int inIndex = 0; inIndex < vehicleCount; ++inIndex)
    {
      ((GridPathState) Game.instance.vehicleManager.GetVehicle(inIndex).pathState.GetState(PathStateManager.StateType.Grid)).OnLightsOut();
      UIManager.instance.GetScreen<SessionHUD>().remainingLapsWidget.Show();
    }
  }

  public override void OnExit(GameState inNextState)
  {
    base.OnExit(inNextState);
    this.StopCommentarySystem();
  }

  public override GameState.Type GetNextState()
  {
    return GameState.Type.SessionWinner;
  }

  public override void UpdateStandings()
  {
    if (Game.instance.sessionManager.hasSessionEnded)
      return;
    RaceSessionState.UpdateStandingsToTrackPosition();
  }

  public static void UpdateStandingsToTrackPosition()
  {
    List<RacingVehicle> standings = Game.instance.sessionManager.standings;
    standings.Clear();
    int vehicleCount = Game.instance.vehicleManager.vehicleCount;
    for (int inIndex = 0; inIndex < vehicleCount; ++inIndex)
    {
      bool flag1 = false;
      RacingVehicle vehicle = Game.instance.vehicleManager.GetVehicle(inIndex);
      SessionTimer timer1 = vehicle.timer;
      for (int index = 0; index < standings.Count; ++index)
      {
        RacingVehicle racingVehicle = standings[index];
        SessionTimer timer2 = racingVehicle.timer;
        if (timer1.lap >= timer2.lap)
        {
          bool flag2 = timer1.hasCrossedStartLine && !timer2.hasCrossedStartLine;
          bool flag3 = timer1.lap > timer2.lap;
          bool flag4 = timer1.hasSeenChequeredFlag && timer2.hasSeenChequeredFlag;
          bool flag5 = timer1.lap == timer2.lap;
          if (!flag2)
            flag2 = flag3;
          if (!vehicle.behaviourManager.isOutOfRace)
          {
            if (!flag2 && flag3 && (timer1.hasSeenChequeredFlag && !timer2.hasSeenChequeredFlag))
              flag2 = true;
            if (!flag2 && flag5 && flag4)
              flag2 = (double) timer1.sessionTime < (double) timer2.sessionTime;
          }
          if (!timer1.hasSeenChequeredFlag)
          {
            bool flag6 = timer1.hasCrossedStartLine && timer2.hasCrossedStartLine;
            bool flag7 = false;
            if (!flag2 && flag6)
            {
              flag2 = (double) timer1.sessionDistanceTraveled > (double) timer2.sessionDistanceTraveled;
              if (!flag2 && (double) timer1.sessionDistanceTraveled == (double) timer2.sessionDistanceTraveled)
                flag7 = true;
            }
            bool flag8 = !timer1.hasCrossedStartLine && !timer2.hasCrossedStartLine;
            if (!flag2 && (flag8 || flag7))
              flag2 = (double) vehicle.pathController.GetDistanceAlongPath01(PathController.PathType.Track) > (double) racingVehicle.pathController.GetDistanceAlongPath01(PathController.PathType.Track);
          }
          if (flag2)
          {
            standings.Insert(index, vehicle);
            flag1 = true;
            break;
          }
        }
      }
      if (!flag1)
        standings.Add(vehicle);
    }
  }
}
