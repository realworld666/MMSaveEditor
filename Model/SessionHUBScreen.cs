// Decompiled with JetBrains decompiler
// Type: SessionHUBScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class SessionHUBScreen : UIScreen
{
  public UIHUBSession sessionWidget;
  public UIHUBTweets tweetsWidget;
  public UIHUBSelection selectionWidget;
  public UIHUBSetup setupWidget;
  public UIHUBKnowledge knowledgeWidget;
  public SoundSFX[] cheeringSounds;
  public GameObject warningMessageContainer;
  public TextMeshProUGUI warningMessageText;
  private SessionDetails mSessionDetails;
  private bool mLastComplete;
  private bool mIsBonusesActive;
  private bool mShowDriverWarning;
  private SessionHUBScreen.WarningMessageState mCurrentWarningState;

  public bool isSetupComplete
  {
    get
    {
      return this.selectionWidget.isComplete();
    }
  }

  public SessionDetails.SessionType sessionType
  {
    get
    {
      return this.mSessionDetails.sessionType;
    }
  }

  public bool showDriverWarning
  {
    get
    {
      return this.mShowDriverWarning;
    }
    set
    {
      this.mShowDriverWarning = value;
    }
  }

  public override void OnStart()
  {
    base.OnStart();
    this.tweetsWidget.OnStart();
    this.selectionWidget.OnStart();
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.showNavigationBars = true;
    UIManager.instance.ClearNavigationStacks();
    this.mSessionDetails = Game.instance.sessionManager.eventDetails.currentSession;
    this.sessionWidget.Setup();
    this.tweetsWidget.Setup();
    this.selectionWidget.Setup();
    this.CacheActiveSelectionSteps();
    this.setupWidget.Setup();
    switch (this.mSessionDetails.sessionType)
    {
      case SessionDetails.SessionType.Practice:
        App.instance.cameraManager.ActivateMode(CameraManager.CameraMode.PracticeHUB);
        break;
      case SessionDetails.SessionType.Qualifying:
        App.instance.cameraManager.ActivateMode(CameraManager.CameraMode.QualifyingHUB);
        break;
      case SessionDetails.SessionType.Race:
        App.instance.cameraManager.ActivateMode(CameraManager.CameraMode.RaceHUB);
        break;
    }
    scSessionAmbience.Start(Game.instance.sessionManager.eventDetails.circuit.locationName, Game.instance.sessionManager.eventDetails.currentSession.sessionType, true);
    this.SetScreenName();
    this.UpdateWarningMessage();
    this.SetContinueButtonsState(this.selectionWidget.isComplete());
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionScreen, 0.0f);
  }

  private void SetScreenName()
  {
    if (App.instance.gameStateManager.currentState is PreSessionHUBState)
    {
      switch (this.mSessionDetails.sessionType)
      {
        case SessionDetails.SessionType.Practice:
          this.screenNameLabel = "Practice";
          break;
        case SessionDetails.SessionType.Qualifying:
          this.screenNameLabel = "Qualifying";
          break;
        case SessionDetails.SessionType.Race:
          this.screenNameLabel = "Race";
          break;
      }
    }
    else
    {
      switch (this.mSessionDetails.sessionType)
      {
        case SessionDetails.SessionType.Practice:
          this.screenNameLabel = "Post Practice";
          break;
        case SessionDetails.SessionType.Qualifying:
          this.screenNameLabel = "Post Qualifying";
          break;
        case SessionDetails.SessionType.Race:
          this.screenNameLabel = "Post Race";
          break;
      }
    }
  }

  private void Update()
  {
    bool isComplete = this.selectionWidget.isComplete();
    if (this.mLastComplete != isComplete)
    {
      this.SetContinueButtonsState(isComplete);
      this.mLastComplete = isComplete;
    }
    this.UpdateWarningMessage();
    scSessionAmbience.Update(GameTimer.deltaTime, 1f);
  }

  public override void OnExit()
  {
    base.OnExit();
    UIManager.instance.navigationBars.bottomBar.SetMode(UIBottomBar.Mode.Session);
    this.mSessionDetails = (SessionDetails) null;
    this.mLastComplete = false;
    scSessionAmbience.Stop();
  }

  private void SetContinueButtonsState(bool isComplete)
  {
    if (isComplete)
    {
      switch (Game.instance.sessionManager.eventDetails.currentSession.sessionType)
      {
        case SessionDetails.SessionType.Practice:
          this.continueButtonLabel = Localisation.LocaliseID("PSG_10002718", (GameObject) null);
          break;
        case SessionDetails.SessionType.Qualifying:
          this.continueButtonLabel = Localisation.LocaliseID("PSG_10002719", (GameObject) null);
          break;
        case SessionDetails.SessionType.Race:
          this.continueButtonLabel = Localisation.LocaliseID("PSG_10002720", (GameObject) null);
          break;
      }
    }
    else
      this.continueButtonLabel = Localisation.LocaliseID("PSG_10003119", (GameObject) null);
  }

  public override UIScreen.NavigationButtonEvent OnContinueButton()
  {
    if (this.selectionWidget.isComplete())
      return UIScreen.NavigationButtonEvent.LetGameStateHandle;
    this.selectionWidget.GoToNextStep();
    return UIScreen.NavigationButtonEvent.HandledByScreen;
  }

  private void UpdateWarningMessage()
  {
    if (!this.selectionWidget.selectBonuses.IsComplete() && this.mIsBonusesActive)
    {
      if (this.mCurrentWarningState == SessionHUBScreen.WarningMessageState.bonuses)
        return;
      this.warningMessageText.text = Localisation.LocaliseID("PSG_10011049", (GameObject) null);
      GameUtility.SetActive(this.warningMessageContainer, true);
      this.mCurrentWarningState = SessionHUBScreen.WarningMessageState.bonuses;
    }
    else if (this.showDriverWarning)
    {
      if (this.mCurrentWarningState == SessionHUBScreen.WarningMessageState.drivers)
        return;
      this.warningMessageText.text = Localisation.LocaliseID("PSG_10007949", (GameObject) null);
      GameUtility.SetActive(this.warningMessageContainer, true);
      this.mCurrentWarningState = SessionHUBScreen.WarningMessageState.drivers;
    }
    else
    {
      if (this.mCurrentWarningState != SessionHUBScreen.WarningMessageState.none)
        this.mCurrentWarningState = SessionHUBScreen.WarningMessageState.none;
      GameUtility.SetActive(this.warningMessageContainer, false);
    }
  }

  private void CacheActiveSelectionSteps()
  {
    this.mIsBonusesActive = false;
    for (int index = 0; index < this.selectionWidget.activeSteps.Count; ++index)
    {
      if (this.selectionWidget.activeSteps[index].step == UIHUBStep.Step.Knowledge)
      {
        this.mIsBonusesActive = true;
        break;
      }
    }
  }

  private enum WarningMessageState
  {
    none,
    bonuses,
    drivers,
  }
}
