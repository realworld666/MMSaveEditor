// Decompiled with JetBrains decompiler
// Type: PreSessionState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class PreSessionState : GameState
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
      return GameState.Group.Event;
    }
  }

  public override void OnEnter(bool fromSave = false)
  {
    base.OnEnter(fromSave);
    this.SetAsSaveEntryPoint();
    this.SetGameTimeToSession();
    Game.instance.time.Pause(GameTimer.PauseType.Game);
    Time.timeScale = 1f;
    Game.instance.sessionManager.SetChampionship(Game.instance.player.team.championship);
    Game.instance.sessionManager.PrepareForSession();
    Game.instance.vehicleManager.CreateVehiclesForSession(Game.instance.player.team.championship, VehicleManager.Visuals.Create);
    Game.instance.sessionManager.PrepareTrackPaths();
    this.PrepareForSession();
    this.SetInitialStandings();
    App.instance.cameraManager.SetTarget((Vehicle) Game.instance.vehicleManager.GetVehicle(Game.instance.player.team.GetDriver(0)), CameraManager.Transition.Instant);
    if (Game.instance.gameType != Game.GameType.SingleEvent)
    {
      int inNumberOfTweets = Random.Range(GameStatsConstants.minTweetValue, GameStatsConstants.maxTweetValue);
      SessionDetails.SessionType sessionType = Game.instance.sessionManager.eventDetails.currentSession.sessionType;
      if (Game.instance.sessionManager.eventDetails.currentSession.sessionNumber == 0)
        Game.instance.mediaManager.SendTweets(inNumberOfTweets, 2f, MediaManager.TweetType.PreSessionTweet, sessionType);
    }
    if (!((Object) Simulation2D.instance != (Object) null))
      return;
    Simulation2D.instance.SetCircuit(Game.instance.sessionManager.eventDetails.circuit);
  }

  public override void OnReEnterFromSave()
  {
    Debug.Log((object) ("PreSessionState.OnReEnterFromSave: " + this.GetType().ToString()), (Object) null);
    Game.instance.time.SetSpeed(GameTimer.Speed.Slow);
    Game.instance.sessionManager.PrepareForSessionAfterLoad();
    App.instance.cameraManager.SetTarget((Vehicle) Game.instance.vehicleManager.GetVehicle(Game.instance.player.team.GetSelectedDriver(0)), CameraManager.Transition.Instant);
    if (!((Object) Simulation2D.instance != (Object) null))
      return;
    Simulation2D.instance.SetCircuit(Game.instance.sessionManager.eventDetails.circuit);
  }

  protected virtual void PrepareForSession()
  {
  }

  public override void GetFirstScreenForState(out string screenName, out UIManager.ScreenTransition screenTransition, out float transitionDuration, bool fromSave = false)
  {
    screenName = "DayTransitionScreen";
    transitionDuration = 2f;
    screenTransition = UIManager.ScreenTransition.Fade;
  }

  public override void Update()
  {
    base.Update();
    this.CheckForEscapeButton();
    Game instance = Game.instance;
    instance.time.UpdateInput();
    instance.time.Update();
    instance.calendar.Update();
    instance.entityManager.Update();
  }

  public override GameState.Type GetNextState()
  {
    return Game.instance.tutorialSystem.isTutorialActive && !Game.instance.tutorialSystem.IsTutorialSectionComplete(TutorialSystem_v1.TutorialSection.HasRunFirstRace) ? GameState.Type.RaceGrid : GameState.Type.PreSessionHUB;
  }

  protected void SetGameTimeToSession()
  {
    Game instance = Game.instance;
    instance.time.SetTime(instance.sessionManager.eventDetails.currentSession.sessionDateTime);
  }

  protected void SetInitialStandings()
  {
    List<RacingVehicle> standings = Game.instance.sessionManager.standings;
    standings.Clear();
    int vehicleCount = Game.instance.vehicleManager.vehicleCount;
    for (int inIndex = 0; inIndex < vehicleCount; ++inIndex)
    {
      bool flag = false;
      RacingVehicle vehicle = Game.instance.vehicleManager.GetVehicle(inIndex);
      for (int index = 0; index < standings.Count; ++index)
      {
        RacingVehicle racingVehicle = standings[index];
        switch (string.Compare(vehicle.driver.contract.GetTeam().name, racingVehicle.driver.contract.GetTeam().name))
        {
          case -1:
            standings.Insert(index, vehicle);
            flag = true;
            goto label_8;
          case 0:
            if (string.Compare(vehicle.driver.lastName, racingVehicle.driver.lastName) == -1)
            {
              standings.Insert(index, vehicle);
              flag = true;
              goto label_8;
            }
            else
              break;
        }
      }
label_8:
      if (!flag)
        standings.Add(vehicle);
    }
    Game.instance.sessionManager.InformVehiclesOfTheirStandings();
  }
}
