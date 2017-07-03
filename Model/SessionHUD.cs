// Decompiled with JetBrains decompiler
// Type: SessionHUD
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SessionHUD : UIScreen
{
  private List<string> mSimulationTutorialIDS = new List<string>();
  public static Action OnModeChanged;
  public Camera sessionHUDCamera;
  public SprinklersWidget sprinklersWidget;
  public DriverActionButtons[] actionButtons;
  public StandingsWidget[] standingsWidget;
  public FastestLapWidget fastestLapWidget;
  public UISessionCommentaryWidget commentaryWidget;
  public SessionTutorialsWidget tutorialWidget;
  public MinimapWidget minimapWidget;
  public UIDriverInfoHUD driverInfoHUD;
  public FullStandingsDropdown fullStandingsDropdown;
  public UISessionTeamRadioWidget sessionTeamRadionWidget;
  public SessionFlagsWidget sessionFlagsWidget;
  public GameObject gridLights;
  public Animator gridLightsAnimator;
  public RemainingLapsWidget remainingLapsWidget;
  public SponsorStatusChangeWidget sponsorStatusChangeWidget;
  public SessionModeWidget sessionModeWidget;
  public RadialRaceMenu radialRaceMenuWidget;
  public Simulation2D simulation2D;
  [SerializeField]
  private UISessionPracticeKnowledgeUnlockedWidget mPracticeKnowledgeUnlocked;
  private scSoundContainer mSoundContainer;
  private float mTimer;

  public override void OnStart()
  {
    base.OnStart();
    this.commentaryWidget.OnStart();
    this.driverInfoHUD.OnStart();
    this.sessionTeamRadionWidget.OnStart();
    this.sessionModeWidget.OnStart();
  }

  public override void OnUnload()
  {
    base.OnUnload();
    this.driverInfoHUD.OnUnload();
    this.simulation2D.ClearSingletonInstance();
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.showNavigationBars = true;
    this.SetTopBarMode(UITopBar.Mode.Session);
    this.SetBottomBarMode(UIBottomBar.Mode.Session);
    this.fullStandingsDropdown.gameObject.SetActive(false);
    App.instance.cameraManager.ActivateMode(CameraManager.CameraMode.FreeRoam);
    App.instance.cameraManager.gameCamera.SetBlurActive(false);
    App.instance.cameraManager.SetRunningMode(false);
    Game.instance.sessionManager.SetCircuitActive(true);
    UITutorial componentInChildren = this.GetComponentInChildren<UITutorial>();
    if (Game.instance.vehicleManager.vehicleCount > 1)
    {
      Team team = Game.instance.player.team;
      for (int inIndex = 0; inIndex < 2; ++inIndex)
      {
        Driver selectedDriver = team.GetSelectedDriver(inIndex);
        RacingVehicle vehicle = Game.instance.vehicleManager.GetVehicle(selectedDriver);
        this.actionButtons[inIndex].SetVehicle(vehicle, componentInChildren);
        this.standingsWidget[inIndex].SetVehicle(vehicle);
      }
    }
    this.gridLights.SetActive(false);
    this.sessionTeamRadionWidget.OnEnter();
    this.sessionFlagsWidget.OnEnter();
    this.fastestLapWidget.OnEnter();
    this.sprinklersWidget.OnEnter();
    this.remainingLapsWidget.OnEnter();
    this.sponsorStatusChangeWidget.OnEnter();
    if ((UnityEngine.Object) this.radialRaceMenuWidget != (UnityEngine.Object) null)
      this.radialRaceMenuWidget.OnEnter();
    this.tutorialWidget.Setup();
    if ((UnityEngine.Object) this.minimapWidget != (UnityEngine.Object) null)
      this.minimapWidget.Setup();
    if (App.instance.gameStateManager.currentState is RaceGridState)
    {
      if (Game.instance.tutorialSystem.isTutorialActive)
      {
        if ((UnityEngine.Object) componentInChildren == (UnityEngine.Object) null || !componentInChildren.IsTutorialOnRaceGrid())
          this.StartGridLightAnimation();
      }
      else
        this.StartGridLightAnimation();
    }
    this.mSimulationTutorialIDS = new List<string>((IEnumerable<string>) new string[5]
    {
      "PSG_10003404",
      "PSG_10003404",
      "PSG_10003397",
      "PSG_10003389",
      "PSG_10003402"
    });
    this.SetupNavigationBarsCanvases();
    this.RegisterPracticeKnowledgeEvents();
    if (!Game.instance.tutorialSystem.isTutorialActive)
    {
      scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionScreen, 0.0f);
      scMusicController.Race();
    }
    else if (!Game.instance.tutorialSystem.isTutorialFirstRaceActive)
      scMusicController.Stop();
    this.sessionModeWidget.Setup();
    if (!App.instance.preferencesManager.videoPreferences.isSimulationRunning2DMode)
      this.simulation2D.Hide();
    else
      this.simulation2D.Show();
  }

  public void SetupNavigationBarsCanvases()
  {
    UIManager.instance.navigationBars.topBar.canvas.worldCamera = this.sessionHUDCamera;
    UIManager.instance.navigationBars.tutorialCanvas.worldCamera = this.sessionHUDCamera;
  }

  public void StartGridLightAnimation()
  {
    this.mSoundContainer = scSoundManager.Instance.PlaySound(SoundID.Sfx_RaceStartLights, 0.0f);
    this.gridLights.SetActive(true);
  }

  public override void OnExit()
  {
    base.OnExit();
    this.ExitWidgets();
    UIManager.instance.SetupCanvasCamera(UIManager.instance.navigationBars.topBar.canvas);
    UIManager.instance.SetupCanvasCamera(UIManager.instance.navigationBars.tutorialCanvas);
    App.instance.cameraManager.SetRunningMode(true);
    this.simulation2D.Hide();
  }

  public void ExitWidgets()
  {
    this.sessionTeamRadionWidget.OnExit();
    this.fastestLapWidget.OnExit();
    this.remainingLapsWidget.OnExit();
    this.sponsorStatusChangeWidget.OnExit();
    if (!((UnityEngine.Object) this.radialRaceMenuWidget != (UnityEngine.Object) null))
      return;
    this.radialRaceMenuWidget.OnExit();
  }

  public override void OnFocus()
  {
    base.OnFocus();
    this.SetupNavigationBarsCanvases();
  }

  private void Update()
  {
    if (this.mSimulationTutorialIDS.Count > 0)
    {
      this.mTimer -= GameTimer.deltaTime;
      if ((double) this.mTimer <= 0.0)
      {
        DialogRule ruleById = App.instance.dialogRulesManager.GetRuleByID(this.mSimulationTutorialIDS[0]);
        App.instance.tutorialSimulation.AddTutorial(ruleById);
        this.mSimulationTutorialIDS.RemoveAt(0);
        this.mTimer = UnityEngine.Random.Range(20f, 30f);
      }
    }
    if (!this.gridLights.activeSelf)
      this.sponsorStatusChangeWidget.CheckForStatusChange();
    if (Game.instance.sessionManager.flag != SessionManager.Flag.None)
      return;
    if (Game.instance.time.isPaused)
    {
      if ((UnityEngine.Object) this.mSoundContainer != (UnityEngine.Object) null)
        this.mSoundContainer.Pause();
      this.gridLightsAnimator.speed = 0.0f;
    }
    else
    {
      if ((UnityEngine.Object) this.mSoundContainer != (UnityEngine.Object) null)
        this.mSoundContainer.UnPause();
      this.gridLightsAnimator.speed = 1f;
    }
  }

  private void RegisterPracticeKnowledgeEvents()
  {
    SessionDetails.SessionType sessionType = Game.instance.sessionManager.sessionType;
    PracticeReportKnowledgeData[] knowledgeData = Game.instance.persistentEventData.GetPlayerPracticeReportData().GetKnowledgeData();
    if (sessionType == SessionDetails.SessionType.Practice)
    {
      for (int index = 0; index < knowledgeData.Length; ++index)
      {
        knowledgeData[index].onLevelUnlocked.RemoveAllListeners();
        knowledgeData[index].onLevelUnlocked.AddListener(new UnityAction<PracticeReportKnowledgeData>(this.OnKnowledgeLevelUnlocked));
      }
    }
    else
    {
      for (int index = 0; index < knowledgeData.Length; ++index)
        knowledgeData[index].onLevelUnlocked.RemoveAllListeners();
      GameUtility.SetActive(this.mPracticeKnowledgeUnlocked.gameObject, false);
    }
  }

  private void OnKnowledgeLevelUnlocked(PracticeReportKnowledgeData inPracticeKnowledge)
  {
    this.mPracticeKnowledgeUnlocked.Show(inPracticeKnowledge);
  }
}
