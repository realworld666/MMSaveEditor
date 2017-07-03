// Decompiled with JetBrains decompiler
// Type: PreSessionHUBState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class PreSessionHUBState : GameState
{
  private bool mHasConfirmedTyreChoice;

  public override GameState.Type type
  {
    get
    {
      return GameState.Type.PreSessionHUB;
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
    this.mHasConfirmedTyreChoice = false;
    UIManager.OnScreenChange += new Action(this.OnScreenChange);
  }

  public override void OnReEnterFromSave()
  {
  }

  public override void OnExit(GameState inNextState)
  {
    base.OnExit(inNextState);
    UIManager.OnScreenChange -= new Action(this.OnScreenChange);
    App.instance.cameraManager.gameCamera.SetBlurActive(false);
    Game.instance.sessionManager.circuit.ClearGridCrowds();
  }

  public override void GetFirstScreenForState(out string screenName, out UIManager.ScreenTransition screenTransition, out float transitionDuration, bool fromSave = false)
  {
    screenName = "SessionHUBScreen";
    screenTransition = UIManager.ScreenTransition.Fade;
    transitionDuration = 1.5f;
  }

  public override void Update()
  {
    base.Update();
    this.CheckForEscapeButton();
  }

  public void OnScreenChange()
  {
    bool inIsActive = !(UIManager.instance.currentScreen is SessionHUBScreen) && !(UIManager.instance.currentScreen is TrackDetailsScreen);
    App.instance.cameraManager.gameCamera.SetBlurActive(inIsActive);
  }

  public override GameState.Type GetNextState()
  {
    GameState.Type type = GameState.Type.None;
    switch (Game.instance.sessionManager.eventDetails.currentSession.sessionType)
    {
      case SessionDetails.SessionType.Practice:
        type = GameState.Type.Practice;
        break;
      case SessionDetails.SessionType.Qualifying:
        type = GameState.Type.Qualifying;
        break;
      case SessionDetails.SessionType.Race:
        type = GameState.Type.RaceGrid;
        break;
    }
    return type;
  }

  public override void OnContinueButton()
  {
    if (!this.mHasConfirmedTyreChoice)
    {
      this.CheckAndPromptForWrongTyresForWeather();
    }
    else
    {
      SessionHUBScreen screen = UIManager.instance.GetScreen<SessionHUBScreen>();
      if ((UnityEngine.Object) screen != (UnityEngine.Object) UIManager.instance.currentScreen)
        UIManager.instance.ChangeScreen("SessionHUBScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
      else if (screen.isSetupComplete)
      {
        if (screen.sessionType != SessionDetails.SessionType.Race)
          this.OnStartSessionButton();
        else
          this.OnTakeSessionConfirmed();
      }
      else
      {
        int num = (int) screen.OnContinueButton();
      }
    }
  }

  private void CheckAndPromptForWrongTyresForWeather()
  {
    bool flag = true;
    if (Game.instance.sessionManager.sessionType == SessionDetails.SessionType.Race)
    {
      RacingVehicle[] playerVehicles = Game.instance.vehicleManager.GetPlayerVehicles();
      for (int index = 0; index < playerVehicles.Length; ++index)
      {
        TyreSet.Tread withLeastTimeCost = playerVehicles[index].performance.tyrePerformance.GetTreadWithLeastTimeCost(5f);
        if (playerVehicles[index].setup.tyreSet.GetTread() != withLeastTimeCost)
        {
          flag = false;
          break;
        }
      }
    }
    if (flag)
    {
      this.mHasConfirmedTyreChoice = true;
      this.OnContinueButton();
    }
    else
    {
      Action inCancelAction = (Action) (() =>
      {
        this.mHasConfirmedTyreChoice = false;
        SessionHUBScreen screen = UIManager.instance.GetScreen<SessionHUBScreen>();
        if ((UnityEngine.Object) UIManager.instance.currentScreen == (UnityEngine.Object) screen)
          screen.selectionWidget.GoToStep(UIHUBStep.Step.Setup);
        else
          UIManager.instance.ChangeScreen("SessionHUBScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
      });
      Action inConfirmAction = (Action) (() =>
      {
        this.mHasConfirmedTyreChoice = true;
        this.OnContinueButton();
      });
      GenericConfirmation dialog = UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmation>();
      string inTitle = Localisation.LocaliseID("PSG_10009075", (GameObject) null);
      string inText = Localisation.LocaliseID("PSG_10009076", (GameObject) null);
      string inCancelString = Localisation.LocaliseID("PSG_10009077", (GameObject) null);
      string inConfirmString = Localisation.LocaliseID("PSG_10009078", (GameObject) null);
      dialog.Show(inCancelAction, inCancelString, inConfirmAction, inConfirmString, inText, inTitle);
    }
  }

  public void OnStartSessionButton()
  {
    if (UIManager.instance.dialogBoxManager.GetDialog<SkippableSessionDialog>().gameObject.activeSelf)
      return;
    switch (Game.instance.sessionManager.eventDetails.currentSession.sessionType)
    {
      case SessionDetails.SessionType.Practice:
        SkippableSessionDialog.Show(Localisation.LocaliseID("PSG_10009386", (GameObject) null), Localisation.LocaliseID("PSG_10009388", (GameObject) null), Localisation.LocaliseID("PSG_10009386", (GameObject) null), Localisation.LocaliseID("PSG_10011849", (GameObject) null), new Action(this.OnTakeSessionConfirmed), new Action(this.OnSkipSessionConfirmed));
        break;
      case SessionDetails.SessionType.Qualifying:
        if (Game.instance.sessionManager.eventDetails.hasSeveralQualifyingSessions)
        {
          QualifyingPostSessionState.ShowSkippableSessionDialog(new Action(this.OnTakeSessionConfirmed), new Action(this.OnSkipSessionConfirmed));
          break;
        }
        SkippableSessionDialog.Show(Localisation.LocaliseID("PSG_10009387", (GameObject) null), Localisation.LocaliseID("PSG_10009388", (GameObject) null), Localisation.LocaliseID("PSG_10009387", (GameObject) null), Localisation.LocaliseID("PSG_10011849", (GameObject) null), new Action(this.OnTakeSessionConfirmed), new Action(this.OnSkipSessionConfirmed));
        break;
      case SessionDetails.SessionType.Race:
        SkippableSessionDialog.Show("Go Racing", "Do you want to take this session?", "Go Race", "Instant Result", new Action(this.OnTakeSessionConfirmed), new Action(this.OnSkipSessionConfirmed));
        break;
    }
  }

  public void OnTakeSessionConfirmed()
  {
    Game.instance.stateInfo.GoToNextState();
  }

  public void OnSkipSessionConfirmed()
  {
    App.instance.gameStateManager.SetState(GameState.Type.SkipSession, GameStateManager.StateChangeType.CheckForFadedScreenChange, false);
  }
}
