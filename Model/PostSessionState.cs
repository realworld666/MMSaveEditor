// Decompiled with JetBrains decompiler
// Type: PostSessionState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PostSessionState : GameState
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
    Game.instance.sessionManager.SetChampionship(Game.instance.player.team.championship);
    Game.instance.time.SetSpeed(GameTimer.Speed.Slow);
    App.instance.cameraManager.gameCamera.SetBlurActive(true);
    if (Game.instance.gameType == Game.GameType.SingleEvent)
      return;
    SessionDetails.SessionType sessionType = Game.instance.sessionManager.eventDetails.currentSession.sessionType;
    if (sessionType == SessionDetails.SessionType.Race || !Game.instance.sessionManager.eventDetails.AreAllSessionsOfTypeDone(sessionType))
      return;
    PostSessionState.SendMediaMessages(sessionType);
  }

  public static void SendMediaMessages(SessionDetails.SessionType inSessionType)
  {
    int inNumberOfTweets = Random.Range(GameStatsConstants.minTweetValue, GameStatsConstants.maxTweetValue);
    Game.instance.mediaManager.SendTweets(inNumberOfTweets, 2f, MediaManager.TweetType.PostSessionTweet, inSessionType);
    Game.instance.mediaManager.CreateStoryForSession(Game.instance.sessionManager.eventDetails);
  }

  public override void OnExit(GameState inNextState)
  {
    base.OnExit(inNextState);
    if (inNextState.type == GameState.Type.PostSessionDataCenter)
      return;
    Game.instance.vehicleManager.RemoveVehicles();
  }

  public override void OnReEnterFromSave()
  {
    Debug.Log((object) ("PostSessionState.OnReEnterFromSave: " + this.GetType().ToString()), (Object) null);
    Game.instance.time.SetSpeed(GameTimer.Speed.Slow);
    App.instance.cameraManager.gameCamera.SetBlurActive(true);
    Game.instance.sessionManager.PrepareForSessionAfterLoad();
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

  public override void OnContinueButton()
  {
    if (Game.instance.sessionManager.eventDetails.currentSession.sessionType == SessionDetails.SessionType.Race)
    {
      Game.instance.stateInfo.GoToNextState();
    }
    else
    {
      Game.instance.sessionManager.championship.GoToNextSession();
      if (Game.instance.sessionManager.eventDetails.hasEventEnded)
      {
        Game.instance.sessionManager.championship.EndEvent();
        App.instance.gameStateManager.LoadToFrontend(GameStateManager.StateChangeType.CheckForFadedScreenChange);
      }
      else
        Game.instance.stateInfo.GoToNextState();
    }
  }
}
