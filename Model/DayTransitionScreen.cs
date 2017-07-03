// Decompiled with JetBrains decompiler
// Type: DayTransitionScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayTransitionScreen : UIScreen
{
  public float duration = 5f;
  private Weather mWeather = new Weather();
  [SerializeField]
  private UIChampionshipLogo championshipLogo;
  public TextMeshProUGUI sessionLabel;
  public TextMeshProUGUI dayLabel;
  public TextMeshProUGUI circuitLabel;
  public UIWeatherIcon weatherIcon;
  public Flag circuitFlag;
  public UITrackLayout trackLayout;
  public UISessionColor sessionColor;
  public GameObject qualifyingObjective;
  public TextMeshProUGUI qualifyingObjectiveTargetPositionLabel;
  public GameObject objective;
  public TextMeshProUGUI objectiveTitleLabel;
  public TextMeshProUGUI objectiveTargetLabel;
  public TextMeshProUGUI objectiveValueLabel;
  public GameObject orAboveLabel;
  public Image sponsorTargetBackgroundImage;
  public UIHUBUltimatumWidget ultimatumWidget;
  private float mTimer;
  private bool mIsExiting;

  public override void OnStart()
  {
    base.OnStart();
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.canUseEscapeButton = false;
    this.championshipLogo.SetChampionship(Game.instance.sessionManager.championship);
    this.mTimer = 0.0f;
    this.mIsExiting = false;
    this.showNavigationBars = false;
    Game.instance.sessionManager.SetCircuitActive(true);
    SessionDetails.SessionType sessionType = Game.instance.sessionManager.eventDetails.currentSession.sessionType;
    this.sessionLabel.text = Game.instance.sessionManager.eventDetails.currentSession.GetSessionName();
    this.qualifyingObjective.SetActive(sessionType == SessionDetails.SessionType.Qualifying);
    switch (sessionType)
    {
      case SessionDetails.SessionType.Practice:
        this.objectiveTitleLabel.text = Localisation.LocaliseID("PSG_10010494", (GameObject) null);
        this.dayLabel.text = Localisation.LocaliseEnum((Enum) GameUtility.Day.Friday);
        App.instance.cameraManager.ActivateMode(CameraManager.CameraMode.PracticeIntro);
        break;
      case SessionDetails.SessionType.Qualifying:
        this.objectiveTitleLabel.text = Localisation.LocaliseID("PSG_10010495", (GameObject) null);
        this.dayLabel.text = Localisation.LocaliseEnum((Enum) GameUtility.Day.Saturday);
        this.qualifyingObjective.SetActive(Game.instance.sessionManager.eventDetails.hasSeveralQualifyingSessions && Game.instance.sessionManager.eventDetails.currentSession.sessionNumber != 2);
        if (this.qualifyingObjective.activeSelf)
          this.qualifyingObjectiveTargetPositionLabel.text = GameUtility.FormatForPosition(RaceEventResults.GetPositionThreshold(Game.instance.sessionManager.eventDetails.currentSession.sessionNumber), (string) null);
        App.instance.cameraManager.ActivateMode(CameraManager.CameraMode.QualifyingIntro);
        break;
      case SessionDetails.SessionType.Race:
        this.objectiveTitleLabel.text = Localisation.LocaliseID("PSG_10010496", (GameObject) null);
        this.dayLabel.text = Localisation.LocaliseEnum((Enum) GameUtility.Day.Sunday);
        App.instance.cameraManager.ActivateMode(CameraManager.CameraMode.RaceIntro);
        break;
    }
    scSoundManager.Instance.PlaySound(SoundID.Music_RaceIntro, 0.0f);
    this.SetCircuitDetails();
    this.SetSponsorDetails();
    this.ultimatumWidget.Setup();
    App.instance.cameraManager.gameCamera.SetBlurActive(false);
    Game.instance.sessionManager.UpdateWeather();
  }

  private void SetCircuitDetails()
  {
    RaceEventDetails eventDetails = Game.instance.sessionManager.eventDetails;
    SessionWeatherDetails currentSessionWeather = Game.instance.sessionManager.currentSessionWeather;
    currentSessionWeather.GetWeather(currentSessionWeather.normalizedTimeElapsed, ref this.mWeather);
    this.weatherIcon.SetIcon(this.mWeather);
    Circuit circuit = eventDetails.circuit;
    this.circuitLabel.text = Localisation.LocaliseID(circuit.locationNameID, (GameObject) null);
    this.circuitFlag.SetNationality(circuit.nationality);
    this.trackLayout.SetCircuitIcon(circuit);
    this.sessionColor.SetSessionColor(Game.instance.sessionManager.sessionType);
  }

  private void SetSponsorDetails()
  {
    SponsorshipDeal weekendSponsorshipDeal = Game.instance.player.team.sponsorController.weekendSponsorshipDeal;
    SessionObjective sessionObjective = (SessionObjective) null;
    SessionDetails.SessionType sessionType = Game.instance.sessionManager.sessionType;
    GameUtility.SetActive(this.objective, weekendSponsorshipDeal != null && sessionType != SessionDetails.SessionType.Practice);
    if (!this.objective.activeSelf)
      return;
    switch (Game.instance.sessionManager.sessionType)
    {
      case SessionDetails.SessionType.Qualifying:
        this.objectiveTitleLabel.text = Localisation.LocaliseID("PSG_10010495", (GameObject) null);
        sessionObjective = weekendSponsorshipDeal.qualifyingObjective;
        break;
      case SessionDetails.SessionType.Race:
        this.objectiveTitleLabel.text = Localisation.LocaliseID("PSG_10010496", (GameObject) null);
        sessionObjective = weekendSponsorshipDeal.raceObjective;
        break;
    }
    if (sessionObjective == null)
      return;
    this.objectiveTargetLabel.text = GameUtility.FormatForPosition(sessionObjective.targetResult, (string) null);
    this.objectiveValueLabel.text = GameUtility.GetCurrencyString((long) sessionObjective.financialReward, 0);
    GameUtility.SetActive(this.orAboveLabel, sessionObjective.targetResult != 1);
    bool flag = Game.instance.vehicleManager.GetHighestPlacedPlayerVehicle().standingsPosition <= sessionObjective.targetResult;
    if (Game.instance.sessionManager.sessionType == SessionDetails.SessionType.Qualifying)
      flag = true;
    if (flag)
    {
      this.objectiveTargetLabel.color = UIConstants.sponsorPositiveColor;
      this.sponsorTargetBackgroundImage.color = UIConstants.sponsorPositiveColor;
    }
    else
    {
      this.objectiveTargetLabel.color = UIConstants.sponsorNegativeColor;
      this.sponsorTargetBackgroundImage.color = UIConstants.sponsorNegativeColor;
    }
  }

  public override void OnExit()
  {
    base.OnExit();
  }

  private void Update()
  {
    this.mTimer += GameTimer.deltaTime;
    if (this.mIsExiting || (double) this.mTimer <= (double) this.duration && ((double) this.mTimer <= (double) this.duration * 0.25 || !InputManager.instance.GetKeyDown(KeyBinding.Name.Escape) && !InputManager.instance.GetKeyDown(KeyBinding.Name.MouseLeft) && !GamePad.GetButton(CButton.A, PlayerIndex.Any)))
      return;
    this.mIsExiting = true;
    Game.instance.stateInfo.GoToNextState();
  }
}
