// Decompiled with JetBrains decompiler
// Type: QualifyingPostSessionState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class QualifyingPostSessionState : PostSessionState
{
  public override GameState.Type type
  {
    get
    {
      return GameState.Type.QualifyingPostSession;
    }
  }

  public override void OnEnter(bool fromSave = false)
  {
    base.OnEnter(fromSave);
    Game.instance.sessionManager.FixConditionInBetwenSessions();
  }

  public override void GetFirstScreenForState(out string screenName, out UIManager.ScreenTransition screenTransition, out float transitionDuration, bool fromSave = false)
  {
    MovieScreen screen = UIManager.instance.GetScreen<MovieScreen>();
    screen.PlayMovie(SoundID.Video_ChampVideo, Game.instance.player.team.championship.GetIdentMovieString());
    SessionManager sessionManager = Game.instance.sessionManager;
    if (!sessionManager.eventDetails.hasSeveralQualifyingSessions || sessionManager.eventDetails.IsLastOfMultipleQualifyingSessions())
      screen.SetNextScreen("QualifyingResultsScreen", 1.5f, (Entity) null);
    else
      screen.SetNextScreen("QualifyingEliminationScreen", 1.5f, (Entity) null);
    screenName = "MovieScreen";
    screenTransition = UIManager.ScreenTransition.Fade;
    transitionDuration = 1.5f;
  }

  public override void OnContinueButton()
  {
    if (this.GetNextState() == GameState.Type.QualifyingPreSession)
      this.OnStartSessionButton();
    else
      base.OnContinueButton();
  }

  public override GameState.Type GetNextState()
  {
    return Game.instance.sessionManager.eventDetails.GetNextActiveSession().sessionType == SessionDetails.SessionType.Qualifying ? GameState.Type.QualifyingPreSession : GameState.Type.RacePreSession;
  }

  public void OnStartSessionButton()
  {
    if (UIManager.instance.dialogBoxManager.GetDialog<SkippableSessionDialog>().gameObject.activeSelf)
      return;
    QualifyingPostSessionState.ShowSkippableSessionDialog(new Action(this.OnTakeSessionConfirmed), new Action(this.OnSkipSessionConfirmed));
  }

  public static void ShowSkippableSessionDialog(Action inYesAction, Action inNoAction)
  {
    RaceEventDetails eventDetails = Game.instance.sessionManager.eventDetails;
    string empty = string.Empty;
    string inDescription = Localisation.LocaliseID("PSG_10009388", (GameObject) null);
    string inYesButtonText = Localisation.LocaliseID("PSG_10009387", (GameObject) null);
    Driver driver1 = Game.instance.player.team.GetDriver(0);
    Driver driver2 = Game.instance.player.team.GetDriver(1);
    if (Game.instance.sessionManager.eventDetails.results.IsDriverOutOfQualifying(driver1) && Game.instance.sessionManager.eventDetails.results.IsDriverOutOfQualifying(driver2))
    {
      inDescription = Localisation.LocaliseID("PSG_10011503", (GameObject) null);
      inYesButtonText = Localisation.LocaliseID("PSG_10011504", (GameObject) null);
    }
    SkippableSessionDialog.Show(eventDetails.GetNextQualifyingActiveSession().GetSessionName(), inDescription, inYesButtonText, Localisation.LocaliseID("PSG_10011849", (GameObject) null), inYesAction, inNoAction);
  }

  public void OnTakeSessionConfirmed()
  {
    Game.instance.sessionManager.championship.GoToNextSession();
    Game.instance.stateInfo.GoToNextState();
  }

  public void OnSkipSessionConfirmed()
  {
    Game.instance.sessionManager.championship.GoToNextSession();
    App.instance.gameStateManager.SetState(GameState.Type.SkipSession, GameStateManager.StateChangeType.CheckForFadedScreenChange, false);
  }
}
