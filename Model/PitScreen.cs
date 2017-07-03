// Decompiled with JetBrains decompiler
// Type: PitScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PitScreen : UIScreen
{
  private PitScreen.Mode mMode = PitScreen.Mode.SendOut;
  public UICharacterPortrait portrait;
  public TextMeshProUGUI driverNameLabel;
  public TextMeshProUGUI positionLabel;
  public DriverTimerHUD timerHUD;
  public GameObject qualifyingTyres;
  public TextMeshProUGUI qualifyingTyresConditionLabel;
  public Canvas onQualTyresChangedCanvas;
  public GameObject tyres;
  public TextMeshProUGUI tyreConditionLabel;
  public Canvas onRaceTyresChangedCanvas;
  public Image tyreTempPin;
  public RectTransform tyreTempTransform;
  public Gradient tyreTempGradient;
  public GameObject fuel;
  public TextMeshProUGUI fuelLapsLabel;
  public TextMeshProUGUI newFuelLabel;
  public Slider fuelLevelslider;
  public Canvas onFuelChangedCanvas;
  public GameObject battery;
  public TextMeshProUGUI currentCharge;
  public Image batteryFill;
  public Image sessionSlider;
  public TextMeshProUGUI sessionTimeLabel;
  public TextMeshProUGUI lapsRemainingLabel;
  public Canvas driverFeedbackCanvas;
  public TextMeshProUGUI driverFeedbackLabel;
  public UIPitOptionsSelectionWidget optionsSelectionWidget;
  public GameObject time;
  public TextMeshProUGUI totalTimeLabel;
  public GameObject timeBreakdownIcon;
  public UISetupBalanceWidget setupBalanceWidget;
  public UIStintListWidget stintListWidget;
  public UIStintListWidget raceStintListWidget;
  public GameObject minimapInfo;
  public MinimapWidget minimapWidget;
  public Canvas minimapToggleBarCanvas;
  public Toggle minimapShowAllToggle;
  public Canvas minimapEstimatedPositionCanvas;
  public TextMeshProUGUI estimatedPitExitPosition;
  public TextMeshProUGUI driverBehindInfoText;
  public TextMeshProUGUI driverBehindPositionText;
  public TextMeshProUGUI driverInFrontInfoText;
  public TextMeshProUGUI driverInFrontPositionText;
  public GameObject driverBehindGameObject;
  public GameObject driverInFrontGameObject;
  public GameObject warningMessageContainer;
  public TextMeshProUGUI warningMessageText;
  private RacingVehicle mVehicle;
  private bool mBatteryRecharged;
  private bool mFuelChanged;
  private bool mTyresChanged;
  private bool mPartsRepaired;
  private SessionManager mSessionManager;
  private SessionDetails.SessionType mSessionType;
  private bool mTrackPlayerChoices;
  private bool mDisplayTyreWarning;
  private bool mDisplayFuelWarning;
  private List<RacingVehicle> mCurrentStandings;
  private float mTimeImpactCached;
  private float mPitLaneExtraTravelDurationCached;
  private bool pitLaneExtraTimeSet;
  private scSoundContainer soundDataCenter;
  private bool mAudioWasPausedOnEnter;

  public RacingVehicle vehicle
  {
    get
    {
      return this.mVehicle;
    }
  }

  public PitScreen.Mode mode
  {
    get
    {
      return this.mMode;
    }
  }

  public override void OnStart()
  {
    this.optionsSelectionWidget.OnStart();
    this.setupBalanceWidget.OnStart();
    base.OnStart();
    this.dontAddToBackStack = true;
  }

  public void Setup(RacingVehicle inVehicle, PitScreen.Mode inMode)
  {
    this.mSessionManager = Game.instance.sessionManager;
    this.mSessionType = this.mSessionManager.sessionType;
    this.mCurrentStandings = this.mSessionManager.standings;
    this.mVehicle = inVehicle;
    this.mMode = inMode;
    this.mVehicle.performance.setupPerformance.UpdateVisualKnowledgeRange();
    this.portrait.SetPortrait((Person) this.mVehicle.driver);
    this.driverNameLabel.text = this.mVehicle.driver.lastName;
    if (this.mSessionType != SessionDetails.SessionType.Practice && (this.mSessionType != SessionDetails.SessionType.Qualifying || this.mMode != PitScreen.Mode.PreSession))
    {
      this.driverNameLabel.fontSizeMin = 8f;
      this.driverNameLabel.fontSizeMax = 14f;
      this.positionLabel.text = GameUtility.FormatForPosition(this.mVehicle.standingsPosition, (string) null);
    }
    else
    {
      this.driverNameLabel.fontSizeMin = 16f;
      this.driverNameLabel.fontSizeMax = 20f;
      this.positionLabel.text = string.Empty;
    }
    this.timerHUD.SetVehicle(this.mVehicle);
    GameUtility.SetSliderAmountIfDifferent(this.fuelLevelslider, this.mVehicle.performance.fuel.GetNormalisedFuelLevel(), 1000f);
    this.tyres.GetComponentsInChildren<UICarSetupTyreIcon>()[0].SetTyre(this.mVehicle.setup.tyreSet);
    this.tyreConditionLabel.text = this.mVehicle.setup.tyreSet.GetConditionText();
    this.qualifyingTyres.GetComponentsInChildren<UICarSetupTyreIcon>()[0].SetTyre(this.mVehicle.setup.tyreSet);
    this.qualifyingTyresConditionLabel.text = this.mVehicle.setup.tyreSet.GetConditionText();
    GameUtility.SetCanvasEnabled(this.onRaceTyresChangedCanvas, false);
    GameUtility.SetCanvasEnabled(this.onQualTyresChangedCanvas, false);
    this.UpdateTyreTemperatureWidget(this.mVehicle.setup.tyreSet);
    this.mVehicle.setup.ResetChanges();
    this.mVehicle.car.CancelFixCondition();
    this.mVehicle.setup.SetRepair();
    this.optionsSelectionWidget.Setup(this.mVehicle, this.mMode);
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.showCoreNavigation = false;
    this.showNavigationBars = true;
    this.ShowCancelButton(true);
    this.SetTopBarMode(UITopBar.Mode.Session);
    this.SetBottomBarMode(UIBottomBar.Mode.Core);
    Game.instance.time.Pause(GameTimer.PauseType.UI);
    if (App.instance.gameStateManager.currentState is SessionState)
      App.instance.cameraManager.ActivateMode(CameraManager.CameraMode.PostSession);
    App.instance.cameraManager.gameCamera.SetBlurActive(true);
    GameUtility.SetImageFillAmountIfDifferent(this.sessionSlider, this.mSessionManager.GetNormalizedSessionTime(), 1f / 512f);
    if (this.mMode == PitScreen.Mode.Pitting && this.mSessionType == SessionDetails.SessionType.Race && !this.pitLaneExtraTimeSet)
    {
      this.mPitLaneExtraTravelDurationCached = (float) SimulationLapTimeModel.EstimateDriveThroughPenaltyTime(this.mSessionManager.circuit, this.mSessionManager.championship.rules.pitlaneSpeed, this.mVehicle.performance.GetRacingPerformance(), (CarStats) null).TotalSeconds;
      Debug.LogFormat("Drive through penalty estimated at {0}s", (object) this.mPitLaneExtraTravelDurationCached);
      this.pitLaneExtraTimeSet = true;
    }
    if (this.mSessionType == SessionDetails.SessionType.Race)
    {
      if (this.mMode == PitScreen.Mode.PreSession)
      {
        this.lapsRemainingLabel.text = Localisation.LocaliseID("PSG_10010182", (GameObject) null);
        this.sessionTimeLabel.text = this.mSessionManager.lapCount.ToString() + " " + Localisation.LocaliseID("PSG_10002291", (GameObject) null);
      }
      else
      {
        this.sessionTimeLabel.text = Localisation.LocaliseID("PSG_10000434", (GameObject) null) + " " + (object) this.mSessionManager.lap + "/" + (object) this.mSessionManager.lapCount;
        this.lapsRemainingLabel.text = Mathf.Clamp(this.mSessionManager.GetLapsRemaining() + 1, 0, this.mSessionManager.lapCount).ToString() + " " + Localisation.LocaliseID("PSG_10010183", (GameObject) null);
      }
    }
    else
    {
      if (this.mMode == PitScreen.Mode.PreSession)
        this.lapsRemainingLabel.text = Localisation.LocaliseID("PSG_10010182", (GameObject) null);
      else
        this.lapsRemainingLabel.text = Localisation.LocaliseID("PSG_10002125", (GameObject) null) + ":";
      this.sessionTimeLabel.text = GameUtility.GetSessionTimeText(Game.instance.sessionManager.time);
    }
    GameUtility.SetActive(this.qualifyingTyres, this.mSessionType == SessionDetails.SessionType.Qualifying);
    GameUtility.SetCanvasEnabled(this.onQualTyresChangedCanvas, this.mSessionType == SessionDetails.SessionType.Qualifying);
    GameUtility.SetActive(this.tyres, this.mSessionType != SessionDetails.SessionType.Qualifying);
    GameUtility.SetCanvasEnabled(this.onRaceTyresChangedCanvas, this.mSessionType != SessionDetails.SessionType.Qualifying);
    GameUtility.SetActive(this.fuel, this.mSessionType == SessionDetails.SessionType.Race);
    GameUtility.SetCanvasEnabled(this.onFuelChangedCanvas, this.mSessionType == SessionDetails.SessionType.Race && this.mMode == PitScreen.Mode.Pitting);
    GameUtility.SetActive(this.battery, this.mVehicle.ERSController.isActive);
    GameUtility.SetActive(this.time, this.mMode != PitScreen.Mode.PreSession);
    GameUtility.SetActive(this.timerHUD.gameObject, this.mMode == PitScreen.Mode.Pitting);
    switch (this.mMode)
    {
      case PitScreen.Mode.PreSession:
        this.screenName = "Setup";
        break;
      case PitScreen.Mode.SendOut:
      case PitScreen.Mode.Pitting:
        this.screenName = this.mMode != PitScreen.Mode.Pitting ? "Send Out" : "Pit";
        StringVariableParser.intValue1 = (int) this.mTimeImpactCached;
        this.totalTimeLabel.text = Localisation.LocaliseID("PSG_10010554", (GameObject) null);
        break;
    }
    if (this.battery.activeSelf)
    {
      StringVariableParser.intValue1 = Mathf.RoundToInt(this.mVehicle.ERSController.normalizedCharge * 100f);
      this.currentCharge.text = Localisation.LocaliseID("PSG_10011508", (GameObject) null);
      this.batteryFill.fillAmount = this.mVehicle.ERSController.normalizedCharge;
    }
    UIManager.instance.dialogBoxManager.GetDialog<TyreInfoRollover>().Hide();
    UIManager.instance.dialogBoxManager.GetDialog<SetupInfoRollover>().Hide();
    this.mTrackPlayerChoices = this.mMode == PitScreen.Mode.Pitting && this.mSessionType == SessionDetails.SessionType.Race;
    this.UpdateWarningMessages();
    this.UpdateContinueButtonActive();
    if (this.mMode == PitScreen.Mode.PreSession)
      this.OnFuelSelectionChanged(this.mVehicle.performance.fuel.GetFuelLapsRemainingDecimal(), false);
    else
      this.OnFuelSelectionChanged(this.mVehicle.performance.fuel.GetFuelLapsRemainingAtPit(), false);
    this.OnTyreSelectionChanged(this.mVehicle.setup.tyreSet);
    this.OnPartRepaired(false);
    this.setupBalanceWidget.SetupVehicle(this.mVehicle, this.mMode == PitScreen.Mode.SendOut && this.mSessionType == SessionDetails.SessionType.Practice, this.mMode);
    this.SetupDriverFeedback();
    this.SetupInfoPane();
    this.mAudioWasPausedOnEnter = scSessionAmbience.IsPaused;
    if (!this.mAudioWasPausedOnEnter)
      scSoundManager.Instance.Pause(false, false, true);
    scSoundManager.Instance.StartTutorialPaused(true);
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionDrivers, 0.0f);
    if (!scSoundManager.Instance.SessionActive)
      return;
    scSoundManager.CheckPlaySound(SoundID.Ambience_DataCentre, ref this.soundDataCenter, 0.0f);
  }

  public override void OnExit()
  {
    this.setupBalanceWidget.OnExit();
    base.OnExit();
    scSoundManager.CheckStopSound(ref this.soundDataCenter);
    UIManager.instance.dialogBoxManager.GetDialog<TimeBreakdownRollover>().Hide();
    Game.instance.time.UnPause(GameTimer.PauseType.UI);
    if (!this.mAudioWasPausedOnEnter)
      scSoundManager.Instance.UnPause(true);
    scSoundManager.Instance.EndTutorialPaused();
    if (App.instance.gameStateManager.currentState is SessionState)
      App.instance.cameraManager.ActivateMode(CameraManager.CameraMode.FreeRoam);
    App.instance.cameraManager.gameCamera.SetBlurActive(false);
    UIManager.instance.dialogBoxManager.HideAll();
  }

  private void Update()
  {
    if (!this.time.activeSelf || (double) this.mTimeImpactCached == (double) this.mVehicle.setup.GetTotalTimeImpact())
      return;
    this.mTimeImpactCached = this.mVehicle.setup.GetTotalTimeImpact();
    StringVariableParser.intValue1 = Mathf.RoundToInt(this.mTimeImpactCached);
    this.totalTimeLabel.text = Localisation.LocaliseID("PSG_10010554", (GameObject) null);
    this.UpdateEstimatedPitExitPosition();
  }

  private void UpdateEstimatedPitExitPosition()
  {
    if (this.mSessionType != SessionDetails.SessionType.Race)
      return;
    float num1 = Mathf.Max(0.0f, this.mPitLaneExtraTravelDurationCached + this.mTimeImpactCached);
    if ((double) num1 < 0.0)
      Debug.LogWarning((object) "Warning time was gained during the pitstop!", (UnityEngine.Object) null);
    else
      Debug.LogFormat("Estimated lost time to other drivers: {0} seconds", (object) num1);
    int index1 = -1;
    bool flag = false;
    for (int index2 = 0; index2 < this.mCurrentStandings.Count; ++index2)
    {
      if ((double) this.mCurrentStandings[index2].timer.gapToAhead == 0.0 && (double) this.mCurrentStandings[index2].timer.gapToLeader == 0.0 && (double) this.mCurrentStandings[index2].timer.gapToBehind == 0.0)
        flag = true;
      if (this.mCurrentStandings[index2].id == this.mVehicle.id)
      {
        index1 = index2;
        break;
      }
    }
    Debug.Assert(index1 >= 0, "Could not locate our car in the standings!");
    int num2 = this.mCurrentStandings[index1].standingsPosition;
    int num3 = 0;
    int num4 = 0;
    float f1 = 10f;
    float f2 = -10f;
    if (flag)
    {
      num2 = this.mCurrentStandings.Count;
      num3 = this.mCurrentStandings.Count;
    }
    else
    {
      for (int index2 = 0; index2 < this.mCurrentStandings.Count; ++index2)
      {
        if (index2 != index1 && !this.mCurrentStandings[index2].behaviourManager.isOutOfRace)
        {
          int standingsPosition = this.mCurrentStandings[index2].standingsPosition;
          float num5 = num1 + this.mCurrentStandings[index1].timer.GetGapToPosition(standingsPosition);
          if ((double) num5 > 0.0)
          {
            if ((double) num5 < (double) f1 || num3 == 0)
            {
              f1 = num5;
              num3 = standingsPosition;
              if (num3 > num2)
                num2 = num3;
            }
          }
          else if ((double) num5 > (double) f2 || num4 == 0)
          {
            f2 = num5;
            num4 = standingsPosition;
          }
        }
      }
    }
    int num6 = 0;
    for (int index2 = 0; index2 < num2 - 1; ++index2)
    {
      if (this.mCurrentStandings[index2].behaviourManager.isOutOfRace && this.mCurrentStandings[index2].timer.lap == this.mCurrentStandings[index1].timer.lap)
        ++num6;
    }
    int inNumber = num2 - num6;
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    if (num3 > 0)
    {
      if ((double) f1 < 0.0)
        Debug.LogErrorFormat("The driver in front of us is actually in behind!\nEstimatedPosition{0}, driverinFrontPos{1}, driverbehindPos{2}, estimatedGap{3}", (object) inNumber, (object) num3, (object) num4, (object) f1);
      StringVariableParser.subject = (Person) this.mCurrentStandings[num3 - 1].driver;
      string str = (double) Mathf.Abs(f1) <= 8.0 ? ((double) Mathf.Abs(f1) <= 4.0 ? Localisation.LocaliseID("PSG_10010585", (GameObject) null) : Localisation.LocaliseID("PSG_10010587", (GameObject) null)) : Localisation.LocaliseID("PSG_10010586", (GameObject) null);
      GameUtility.SetActive(this.driverInFrontGameObject, true);
      this.driverInFrontPositionText.text = Localisation.LocaliseID(GameUtility.OrdinalLocalisationID(inNumber - 1), (GameObject) null);
      this.driverInFrontInfoText.text = str;
    }
    else
      GameUtility.SetActive(this.driverInFrontGameObject, false);
    if (num4 > 0)
    {
      if ((double) f2 > 0.0)
        Debug.LogErrorFormat("The driver behind is actually in front!!\nEstimatedPosition{0}, driverinFrontPos{1}, driverbehindPos{2}, estimatedGap{3}", (object) inNumber, (object) num3, (object) num4, (object) f1);
      StringVariableParser.subject = (Person) this.mCurrentStandings[num4 - 1].driver;
      string str = (double) Mathf.Abs(f2) <= 8.0 ? ((double) Mathf.Abs(f2) <= 4.0 ? Localisation.LocaliseID("PSG_10010585", (GameObject) null) : Localisation.LocaliseID("PSG_10010589", (GameObject) null)) : Localisation.LocaliseID("PSG_10010588", (GameObject) null);
      GameUtility.SetActive(this.driverBehindGameObject, true);
      this.driverBehindPositionText.text = Localisation.LocaliseID(GameUtility.OrdinalLocalisationID(inNumber + 1), (GameObject) null);
      this.driverBehindInfoText.text = str;
    }
    else
      GameUtility.SetActive(this.driverBehindGameObject, false);
    StringVariableParser.subject = (Person) null;
    this.estimatedPitExitPosition.text = Localisation.LocaliseID(GameUtility.OrdinalLocalisationID(inNumber), (GameObject) null);
  }

  public void OnFuelSelectionChanged(float lapsInTank, bool inIsChanged)
  {
    UIFuelWidget.UpdateTextFieldNLaps(this.fuelLapsLabel, lapsInTank, true);
    GameUtility.SetSliderAmountIfDifferent(this.fuelLevelslider, Mathf.Clamp01(lapsInTank / (float) this.mVehicle.performance.fuel.fuelTankLapCountCapacity), 1000f);
    this.mFuelChanged = inIsChanged;
    if (inIsChanged)
      this.newFuelLabel.text = Localisation.LocaliseID("PSG_10010650", (GameObject) null);
    else
      this.newFuelLabel.text = Localisation.LocaliseID("PSG_10011135", (GameObject) null);
    this.optionsSelectionWidget.fuelOptions.SetChanged(inIsChanged);
    this.mDisplayFuelWarning = (double) lapsInTank < (double) Game.instance.sessionManager.GetLapsRemaining() && this.mSessionManager.championship.rules.isRefuelingOn;
    this.UpdateWarningMessages();
    this.UpdateContinueButtonActive();
  }

  public void OnTyreSelectionChanged(TyreSet inSelectedTyreSet)
  {
    this.mTyresChanged = inSelectedTyreSet != this.mVehicle.setup.tyreSet;
    if (this.mSessionType == SessionDetails.SessionType.Qualifying)
    {
      this.qualifyingTyres.GetComponentsInChildren<UICarSetupTyreIcon>()[0].SetTyre(inSelectedTyreSet);
      this.qualifyingTyresConditionLabel.text = inSelectedTyreSet.GetConditionText();
      GameUtility.SetCanvasEnabled(this.onQualTyresChangedCanvas, this.mTyresChanged);
    }
    else
    {
      this.tyres.GetComponentsInChildren<UICarSetupTyreIcon>()[0].SetTyre(inSelectedTyreSet);
      this.tyreConditionLabel.text = inSelectedTyreSet.GetConditionText();
      this.UpdateTyreTemperatureWidget(inSelectedTyreSet);
      GameUtility.SetCanvasEnabled(this.onRaceTyresChangedCanvas, this.mTyresChanged);
    }
    this.optionsSelectionWidget.tyreChoice.SetChanged(this.mTyresChanged);
    this.mDisplayTyreWarning = !this.mTyresChanged;
    this.UpdateWarningMessages();
    this.UpdateContinueButtonActive();
  }

  public void OnPartRepaired(bool inIsAnyRepairs)
  {
    this.mPartsRepaired = inIsAnyRepairs;
    this.optionsSelectionWidget.partCondition.SetChanged(this.mPartsRepaired);
    this.UpdateContinueButtonActive();
    this.UpdateWarningMessages();
  }

  public void OnSetupChanged(bool inIsChanged)
  {
    this.optionsSelectionWidget.carSetup.SetChanged(inIsChanged);
  }

  public void OnStintChanged(bool inIsChanged)
  {
    this.optionsSelectionWidget.stintType.SetChanged(inIsChanged);
  }

  public void OnStrategyChanged(bool inIsChanged)
  {
    this.optionsSelectionWidget.pitStrategy.SetChanged(inIsChanged);
  }

  public void OnBatteryChargeChanged(bool inIsChanged)
  {
    this.mBatteryRecharged = inIsChanged;
    this.optionsSelectionWidget.batteryCharge.SetChanged(inIsChanged);
    this.UpdateContinueButtonActive();
  }

  private void UpdateContinueButtonActive()
  {
    this.continueButtonInteractable = !this.mTrackPlayerChoices || this.IsAnyChangesQueued();
  }

  private void UpdateWarningMessages()
  {
    if (!this.mTrackPlayerChoices)
      GameUtility.SetActive(this.warningMessageContainer, false);
    else if (!this.IsAnyChangesQueued())
    {
      GameUtility.SetActive(this.warningMessageContainer, true);
      this.warningMessageText.text = Localisation.LocaliseID("PSG_10010097", (GameObject) null);
    }
    else if (this.mDisplayTyreWarning)
    {
      GameUtility.SetActive(this.warningMessageContainer, true);
      this.warningMessageText.text = Localisation.LocaliseID("PSG_10010098", (GameObject) null);
    }
    else if (this.mDisplayFuelWarning)
    {
      GameUtility.SetActive(this.warningMessageContainer, true);
      this.warningMessageText.text = Localisation.LocaliseID("PSG_10010099", (GameObject) null);
    }
    else
      GameUtility.SetActive(this.warningMessageContainer, false);
  }

  private bool IsAnyChangesQueued()
  {
    if (!this.mTyresChanged && !this.mFuelChanged && !this.mPartsRepaired)
      return this.mBatteryRecharged;
    return true;
  }

  private void UpdateTyreTemperatureWidget(TyreSet inTyreSet)
  {
    float temperature = inTyreSet.GetTemperature();
    this.tyreTempPin.color = this.tyreTempGradient.Evaluate(temperature);
    this.tyreTempTransform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Lerp(-40f, 40f, temperature));
  }

  public void UpdateContinuebuttonText()
  {
    if (this.optionsSelectionWidget.IsComplete())
    {
      switch (this.mMode)
      {
        case PitScreen.Mode.PreSession:
          this.continueButtonLabel = Localisation.LocaliseID("PSG_10003949", (GameObject) null);
          break;
        case PitScreen.Mode.SendOut:
          this.continueButtonLabel = Localisation.LocaliseID("PSG_10008841", (GameObject) null);
          break;
        case PitScreen.Mode.Pitting:
          this.continueButtonLabel = Localisation.LocaliseID("PSG_10000420", (GameObject) null);
          break;
      }
    }
    else
      this.continueButtonLabel = Localisation.LocaliseID("PSG_10002713", (GameObject) null);
  }

  public override UIScreen.NavigationButtonEvent OnContinueButton()
  {
    if (!this.optionsSelectionWidget.IsComplete())
    {
      this.optionsSelectionWidget.GoToNextStep();
    }
    else
    {
      switch (this.mMode)
      {
        case PitScreen.Mode.PreSession:
          this.OnPreSessionButton();
          break;
        case PitScreen.Mode.SendOut:
          this.OnSendOutButton();
          break;
        case PitScreen.Mode.Pitting:
          this.OnPitButton();
          break;
      }
    }
    return UIScreen.NavigationButtonEvent.HandledByScreen;
  }

  public override UIScreen.NavigationButtonEvent OnCancelButton()
  {
    this.mVehicle.setup.ResetChanges();
    return this.OnBackButton();
  }

  private void SetupDriverFeedback()
  {
    GameUtility.SetCanvasEnabled(this.driverFeedbackCanvas, false);
  }

  private void OnPreSessionButton()
  {
    if (this.mVehicle.setup.isChanging())
    {
      this.mVehicle.setup.MakeInstantChanges();
      FeedbackPopup.Open(Localisation.LocaliseID("PSG_10009944", (GameObject) null), string.Empty);
    }
    UIManager.instance.ChangeScreen("SessionHUBScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }

  private void OnSendOutButton()
  {
    int num = 2;
    if (this.mSessionType == SessionDetails.SessionType.Practice)
    {
      int inLapCount = this.optionsSelectionWidget.practiceStintTypeWidget.GetSelectedDropdownValue() + 1;
      switch (this.optionsSelectionWidget.practiceStintTypeWidget.GetSelectedStint())
      {
        case UIPracticeStintTypeWidget.StintSelection.QualifyingTrim:
          inLapCount *= this.optionsSelectionWidget.practiceStintTypeWidget.qualTrimMultiplier;
          this.mVehicle.setup.SetTargetFuelLevel(inLapCount + num);
          break;
        case UIPracticeStintTypeWidget.StintSelection.RaceTrim:
          inLapCount *= this.optionsSelectionWidget.practiceStintTypeWidget.raceTrimMultiplier;
          this.mVehicle.setup.SetTargetFuelLevel(inLapCount + num);
          break;
      }
      this.mVehicle.strategy.SetOrderedLapCount(inLapCount);
    }
    else if (this.mSessionType == SessionDetails.SessionType.Qualifying)
    {
      this.mVehicle.setup.SetTargetFuelLevel(1 + num);
      this.mVehicle.strategy.SetOrderedLapCount(1);
    }
    if (this.mVehicle.pathState.pathStateGroup == PathStateManager.PathStateGroup.InPitbox || this.mVehicle.pathState.pathStateGroup == PathStateManager.PathStateGroup.InGarage)
      this.mVehicle.setup.MakeSetupChanges();
    else
      this.mVehicle.strategy.Pit();
    App.instance.cameraManager.SetTarget((Vehicle) this.mVehicle, CameraManager.Transition.Instant);
    UIManager.instance.ChangeScreen("SessionHUD", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }

  private void OnPitButton()
  {
    this.mVehicle.strategy.Pit();
    App.instance.cameraManager.SetTarget((Vehicle) this.mVehicle, CameraManager.Transition.Instant);
    UIManager.instance.ChangeScreen("SessionHUD", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }

  private void SetupInfoPane()
  {
    if (this.mMode == PitScreen.Mode.Pitting)
    {
      GameUtility.SetActive(this.setupBalanceWidget.gameObject, false);
      GameUtility.SetActive(this.stintListWidget.gameObject, false);
      GameUtility.SetActive(this.raceStintListWidget.gameObject, true);
      this.raceStintListWidget.gridList.DestroyListItems();
      this.raceStintListWidget.SetVehicle(this.mVehicle, this.mMode);
      GameUtility.SetActive(this.minimapInfo, true);
      this.StartMinimap();
    }
    else
    {
      GameUtility.SetActive(this.raceStintListWidget.gameObject, false);
      GameUtility.SetActive(this.setupBalanceWidget.gameObject, true);
      this.stintListWidget.gridList.DestroyListItems();
      this.stintListWidget.SetVehicle(this.mVehicle, this.mMode);
      if (this.stintListWidget.IsEmpty())
      {
        GameUtility.SetActive(this.stintListWidget.gameObject, false);
        GameUtility.SetActive(this.minimapInfo, true);
        this.StartMinimap();
      }
      else
      {
        GameUtility.SetActive(this.minimapInfo, false);
        GameUtility.SetCanvasEnabled(this.minimapToggleBarCanvas, false);
        GameUtility.SetCanvasEnabled(this.minimapEstimatedPositionCanvas, false);
        GameUtility.SetActive(this.stintListWidget.gameObject, true);
      }
    }
  }

  private void StartMinimap()
  {
    this.minimapWidget.options.displayGameCamera = false;
    this.minimapWidget.options.displayFinishLine = true;
    this.minimapWidget.options.displaySectors = true;
    this.minimapWidget.Setup();
    if (this.mMode == PitScreen.Mode.PreSession)
    {
      this.minimapWidget.HideAllVehicles();
      GameUtility.SetCanvasEnabled(this.minimapToggleBarCanvas, false);
    }
    else
    {
      this.SetMinimapShowAll();
      this.minimapShowAllToggle.isOn = true;
      GameUtility.SetCanvasEnabled(this.minimapToggleBarCanvas, true);
    }
    if (this.mMode == PitScreen.Mode.Pitting)
    {
      GameUtility.SetCanvasEnabled(this.minimapEstimatedPositionCanvas, true);
      this.UpdateEstimatedPitExitPosition();
    }
    else
      GameUtility.SetCanvasEnabled(this.minimapEstimatedPositionCanvas, false);
  }

  public void SetMinimapShowAll()
  {
    this.minimapWidget.ShowAllVehicles();
  }

  public void SetMinimapShowPlayerDrivers()
  {
    this.minimapWidget.ShowOnlyPlayerVehicles();
  }

  public void SetMinimapShowRivals()
  {
    this.minimapWidget.ShowPlayerAndRivalVehicles();
  }

  public void ShowTimeBreakDown()
  {
    UIManager.instance.dialogBoxManager.GetDialog<TimeBreakdownRollover>().Show(this.timeBreakdownIcon.GetComponent<RectTransform>(), this.mVehicle);
  }

  public void HideTimeBreakDown()
  {
    UIManager.instance.dialogBoxManager.GetDialog<TimeBreakdownRollover>().Hide();
  }

  public float GetLostPitStopTimeToVehicleSeconds(RacingVehicle inRivalVehicle)
  {
    return (float) SimulationLapTimeModel.EstimateDriveThroughPenaltyTime(this.mSessionManager.circuit, this.mSessionManager.championship.rules.pitlaneSpeed, inRivalVehicle.performance.currentPerformance, (CarStats) null).TotalSeconds + this.mTimeImpactCached;
  }

  public enum Mode
  {
    PreSession,
    SendOut,
    Pitting,
  }
}
