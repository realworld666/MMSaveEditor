// Decompiled with JetBrains decompiler
// Type: DriverActionButtons
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DriverActionButtons : MonoBehaviour
{
  private bool mCancel_button_active = true;
  public RectTransform gridRectTransform;
  public GameObject actionButtonObject;
  public Button actionButton;
  public Button cancelActionButton;
  public Slider setupSlider;
  public TextMeshProUGUI buttonLabel;
  public StrategyButton strategyButton;
  public CarConditionButton carConditionButton;
  public GameObject standings;
  public OutLapOptimisationPanel outLapDetails;
  public ERSBatteryPanel ersBatteryPanel;
  public StrategyPanel strategyPanel;
  public CarConditionPanel carConditionPanel;
  public PitStopPanel pitStopPanel;
  public StintFeedbackPanel stintFeedbackPanel;
  public PenaltiesPanel penaltiesPanel;
  public CarSetupKnowledgePanel setupKnowledgePanel;
  public UISessionTeamRadioMessageWidget teamRadioMessageWidget;
  [HideInInspector]
  public DriverActionButtons.InfoPanel infoPanelState;
  private RacingVehicle mVehicle;
  private SessionDetails.SessionType mSessionType;
  private bool mSetupSliderActive;
  private bool mOnEnabledFailed;
  private UITutorial mUITutorial;

  private void Awake()
  {
    this.strategyPanel.SetToggle(this.strategyButton.toggle);
    this.carConditionPanel.SetToggle(this.carConditionButton.toggle);
    this.cancelActionButton.onClick.AddListener(new UnityAction(this.OnCancelButton));
    StrategyPanel strategyPanel = this.strategyPanel;
    Action action1 = strategyPanel.OnHidePanel + new Action(this.OnPanelClosed);
    strategyPanel.OnHidePanel = action1;
    CarConditionPanel carConditionPanel = this.carConditionPanel;
    Action action2 = carConditionPanel.OnHidePanel + new Action(this.OnPanelClosed);
    carConditionPanel.OnHidePanel = action2;
  }

  public void SetVehicle(RacingVehicle inVehicle, UITutorial inUITutorial)
  {
    this.mUITutorial = inUITutorial;
    this.mVehicle = inVehicle;
    if (this.mOnEnabledFailed)
      this.OnEnable();
    this.mSessionType = Game.instance.sessionManager.eventDetails.currentSession.sessionType;
    this.strategyButton.SetVehicle(inVehicle);
    this.carConditionButton.SetVehicle(inVehicle);
    this.cancelActionButton.gameObject.SetActive(false);
    this.mVehicle.pathState.OnPathStateGroupChanged += new Action(this.UpdateButtonStatuses);
    this.ersBatteryPanel.Setup(this.mVehicle);
    GameUtility.SetActive(this.ersBatteryPanel.gameObject, this.mSessionType == SessionDetails.SessionType.Race && this.mVehicle.driver.contract.GetTeam().championship.rules.isEnergySystemActive);
    this.strategyPanel.SetActionButtonPanel(this.gridRectTransform);
    this.strategyPanel.SetVehicle(this.mVehicle);
    this.carConditionPanel.SetActionButtonPanel(this.gridRectTransform);
    this.carConditionPanel.SetVehicle(this.mVehicle);
    this.outLapDetails.SetVehicle(this.mVehicle);
    this.pitStopPanel.SetVehicle(this.mVehicle);
    this.setupKnowledgePanel.SetVehicle(this.mVehicle);
    this.stintFeedbackPanel.SetVehicle(this.mVehicle);
    this.penaltiesPanel.SetVehicle(this.mVehicle);
    this.UpdateButtonStatuses();
    this.mVehicle.pathState.OnGarageEnter += new Action(this.OnGarageEnter);
    this.mVehicle.pathState.OnGarageExit += new Action(this.OnGarageExit);
    this.mVehicle.pathState.OnPitlaneEnter += new Action(this.OnPitlaneEnter);
    this.mVehicle.pathState.OnPitlaneExit += new Action(this.OnPitlaneExit);
    if (this.mSessionType == SessionDetails.SessionType.Practice && this.mVehicle.pathState.IsInState(PathStateManager.StateType.Garage))
      this.OpenPanel(DriverActionButtons.InfoPanel.StintFeedback);
    else
      this.OpenDefaultPanelForState();
    GameUtility.SetActive(this.carConditionButton.toggle.gameObject, this.mSessionType == SessionDetails.SessionType.Race);
    this.Update();
  }

  private void OnEnable()
  {
    if (this.mVehicle != null)
    {
      RacingVehicle mVehicle = this.mVehicle;
      Action action = mVehicle.OnLapEnd + new Action(this.OnLapEnd);
      mVehicle.OnLapEnd = action;
      this.mVehicle.behaviourManager.OnBehaviourChange += new Action(this.HideButtonIfOutOfRace);
      this.mOnEnabledFailed = false;
    }
    else
      this.mOnEnabledFailed = true;
  }

  private void OnDisable()
  {
    if (this.mVehicle == null)
      return;
    RacingVehicle mVehicle = this.mVehicle;
    Action action = mVehicle.OnLapEnd - new Action(this.OnLapEnd);
    mVehicle.OnLapEnd = action;
    this.mVehicle.behaviourManager.OnBehaviourChange -= new Action(this.HideButtonIfOutOfRace);
  }

  private void Update()
  {
    SessionManager sessionManager = Game.instance.sessionManager;
    SessionDetails.SessionType sessionType = sessionManager.sessionType;
    this.mCancel_button_active = true;
    this.mSetupSliderActive = this.mVehicle.setup.state == SessionSetup.State.ChangingSetup;
    GameUtility.SetActive(this.setupSlider.gameObject, this.mSetupSliderActive);
    switch (sessionType)
    {
      case SessionDetails.SessionType.Practice:
      case SessionDetails.SessionType.Qualifying:
        if (this.actionButtonObject.activeSelf)
        {
          if (Game.instance.sessionManager.eventDetails.results.IsDriverOutOfQualifying(this.mVehicle.driver))
            GameUtility.SetInteractable(this.actionButton, false);
          else if (this.mVehicle.pathState.pathStateGroup == PathStateManager.PathStateGroup.OnTrack)
          {
            GameUtility.SetInteractable(this.actionButton, !this.mVehicle.strategy.IsGoingToPit());
            this.mCancel_button_active = !this.actionButton.interactable;
            if (this.mCancel_button_active)
              this.mCancel_button_active = !this.mVehicle.strategy.HasCompletedOrderedLapCount();
          }
          else
          {
            GameUtility.SetInteractable(this.actionButton, this.mVehicle.setup.state == SessionSetup.State.Setup);
            this.mCancel_button_active = this.mVehicle.strategy.status == SessionStrategy.Status.WaitingForSetupCompletion;
            if (this.mSetupSliderActive)
              GameUtility.SetSliderAmountIfDifferent(this.setupSlider, this.mVehicle.setup.GetNormalizedProgress(), 1000f);
          }
        }
        GameUtility.SetActive(this.setupKnowledgePanel.gameObject, sessionType == SessionDetails.SessionType.Practice);
        break;
      case SessionDetails.SessionType.Race:
        bool flag = this.mVehicle.pathController.IsOnPitlanePath();
        if (this.actionButtonObject.activeSelf)
          GameUtility.SetInteractable(this.actionButton, !this.mVehicle.strategy.IsGoingToPit() && !this.mVehicle.strategy.HasQueuedOrderToPit() && !this.mVehicle.behaviourManager.isOutOfRace && !flag);
        this.mCancel_button_active = !this.actionButton.interactable && !this.mVehicle.behaviourManager.isOutOfRace && !flag;
        if (this.mSetupSliderActive)
          GameUtility.SetSliderAmountIfDifferent(this.setupSlider, this.mVehicle.setup.GetNormalizedProgress(), 1000f);
        GameUtility.SetActive(this.setupKnowledgePanel.gameObject, false);
        break;
    }
    this.penaltiesPanel.OnUpdate();
    if (sessionManager.flag == SessionManager.Flag.Chequered)
      GameUtility.SetActive(this.actionButtonObject, false);
    if (this.mVehicle.timer.hasSeenChequeredFlag)
    {
      GameUtility.SetActive(this.actionButtonObject, false);
      this.strategyButton.toggle.interactable = false;
    }
    if ((this.cancelActionButton.gameObject.activeSelf || this.mCancel_button_active) && (this.mVehicle.pathState.IsInPitlaneArea() || (this.mVehicle.performance.fuel.IsOutOfFuel() || MathsUtility.ApproximatelyZero(this.mVehicle.setup.tyreSet.GetCondition())) && sessionType != SessionDetails.SessionType.Race))
      this.mCancel_button_active = false;
    GameUtility.SetActive(this.cancelActionButton.gameObject, this.mCancel_button_active);
  }

  public void UpdateButtonStatuses()
  {
    SessionDetails.SessionType sessionType = Game.instance.sessionManager.eventDetails.currentSession.sessionType;
    GameUtility.SetActive(this.strategyButton.gameObject, !this.mVehicle.behaviourManager.isOutOfRace && this.mVehicle.pathState.pathStateGroup != PathStateManager.PathStateGroup.InGarage);
    switch (sessionType)
    {
      case SessionDetails.SessionType.Practice:
      case SessionDetails.SessionType.Qualifying:
        if (this.mVehicle.pathState.pathStateGroup == PathStateManager.PathStateGroup.InGarage)
        {
          GameUtility.SetActive(this.actionButtonObject, !this.GameObjectHiddenByTutorial(this.actionButtonObject));
          this.buttonLabel.text = Localisation.LocaliseID("PSG_10010772", (GameObject) null);
          this.actionButton.onClick.RemoveAllListeners();
          this.actionButton.onClick.AddListener(new UnityAction(this.OnSendOutButton));
          break;
        }
        if (this.mVehicle.pathState.pathStateGroup == PathStateManager.PathStateGroup.OnTrack)
        {
          GameUtility.SetActive(this.actionButtonObject, !this.GameObjectHiddenByTutorial(this.actionButtonObject));
          this.buttonLabel.text = Localisation.LocaliseID("PSG_10000389", (GameObject) null);
          this.actionButton.onClick.RemoveAllListeners();
          this.actionButton.onClick.AddListener(new UnityAction(this.OnBringInButton));
          break;
        }
        GameUtility.SetActive(this.actionButtonObject, false);
        break;
      case SessionDetails.SessionType.Race:
        if (this.mVehicle.pathState.pathStateGroup == PathStateManager.PathStateGroup.OnTrack)
        {
          GameUtility.SetActive(this.actionButtonObject, !this.GameObjectHiddenByTutorial(this.actionButtonObject));
          this.buttonLabel.text = Localisation.LocaliseID("PSG_10000420", (GameObject) null);
          this.actionButton.onClick.RemoveAllListeners();
          this.actionButton.onClick.AddListener(new UnityAction(this.OnPitButton));
          break;
        }
        GameUtility.SetActive(this.actionButtonObject, false);
        break;
    }
  }

  private void HideButtonIfOutOfRace()
  {
    if (!this.mVehicle.behaviourManager.isOutOfRace)
      return;
    GameUtility.SetActive(this.strategyButton.gameObject, false);
    GameUtility.SetActive(this.actionButton.gameObject, false);
    GameUtility.SetActive(this.cancelActionButton.gameObject, false);
  }

  public void OpenPanel(DriverActionButtons.InfoPanel inInfoPanel)
  {
    this.strategyPanel.Hide();
    this.carConditionPanel.Hide();
    this.pitStopPanel.Hide();
    this.stintFeedbackPanel.Hide();
    GameUtility.SetActive(this.outLapDetails.gameObject, false);
    GameUtility.SetActive(this.standings, false);
    switch (inInfoPanel)
    {
      case DriverActionButtons.InfoPanel.Standings:
        GameUtility.SetActive(this.standings, !this.GameObjectHiddenByTutorial(this.standings));
        break;
      case DriverActionButtons.InfoPanel.Strategy:
        this.strategyPanel.Show();
        break;
      case DriverActionButtons.InfoPanel.Condition:
        this.carConditionPanel.Show();
        break;
      case DriverActionButtons.InfoPanel.OutLap:
        GameUtility.SetActive(this.outLapDetails.gameObject, true);
        break;
      case DriverActionButtons.InfoPanel.PitStop:
        this.pitStopPanel.Show(this.mVehicle);
        break;
      case DriverActionButtons.InfoPanel.StintFeedback:
        this.stintFeedbackPanel.Show();
        break;
    }
    this.UpdateButtonStatuses();
    this.infoPanelState = inInfoPanel;
  }

  private bool GameObjectHiddenByTutorial(GameObject inGameObject)
  {
    return Game.instance.tutorialSystem.isTutorialActive && (UnityEngine.Object) this.mUITutorial != (UnityEngine.Object) null && this.mUITutorial.IsTutorialHidingGameObject(inGameObject);
  }

  public void ClosePanel(DriverActionButtons.InfoPanel inInfoPanel)
  {
    if (!this.IsPanelOpen(inInfoPanel))
      return;
    switch (inInfoPanel)
    {
      case DriverActionButtons.InfoPanel.Strategy:
        this.OpenDefaultPanelForState();
        break;
      case DriverActionButtons.InfoPanel.Condition:
        this.OpenDefaultPanelForState();
        break;
      case DriverActionButtons.InfoPanel.OutLap:
        this.OpenPanel(DriverActionButtons.InfoPanel.Standings);
        break;
      case DriverActionButtons.InfoPanel.PitStop:
        this.OpenDefaultPanelForState();
        break;
      case DriverActionButtons.InfoPanel.StintFeedback:
        this.OpenDefaultPanelForState();
        break;
    }
  }

  private bool IsPanelOpen(DriverActionButtons.InfoPanel inInfoPanel)
  {
    bool flag = false;
    switch (inInfoPanel)
    {
      case DriverActionButtons.InfoPanel.Standings:
        flag = this.standings.activeSelf;
        break;
      case DriverActionButtons.InfoPanel.Strategy:
        flag = this.strategyPanel.isVisible;
        break;
      case DriverActionButtons.InfoPanel.Condition:
        flag = this.carConditionPanel.isVisible;
        break;
      case DriverActionButtons.InfoPanel.OutLap:
        flag = this.outLapDetails.gameObject.activeSelf;
        break;
      case DriverActionButtons.InfoPanel.PitStop:
        flag = this.pitStopPanel.isVisible;
        break;
      case DriverActionButtons.InfoPanel.StintFeedback:
        flag = this.stintFeedbackPanel.isVisible;
        break;
    }
    return flag;
  }

  public void OnPitButton()
  {
    UIManager.instance.GetScreen<PitScreen>().Setup(this.mVehicle, PitScreen.Mode.Pitting);
    UIManager.instance.ClearNavigationStacks();
    UIManager.instance.ChangeScreen("PitScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }

  public void OnSendOutButton()
  {
    UIManager.instance.GetScreen<PitScreen>().Setup(this.mVehicle, PitScreen.Mode.SendOut);
    UIManager.instance.ClearNavigationStacks();
    UIManager.instance.ChangeScreen("PitScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }

  private void OnSendOutCancelButton()
  {
    this.mVehicle.strategy.SetStatus(SessionStrategy.Status.NoActionRequired);
  }

  public void OnBringInButton()
  {
    App.instance.cameraManager.SetTarget((Vehicle) this.mVehicle, CameraManager.Transition.Smooth);
    this.mVehicle.strategy.ReturnToGarage();
  }

  public void OnCancelButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.mVehicle.strategy.status == SessionStrategy.Status.Pitting)
      this.mVehicle.car.CancelFixCondition();
    if (this.mVehicle.strategy.status != SessionStrategy.Status.PitThruPenalty)
      this.mVehicle.strategy.SetToNoActionRequired();
    else
      this.mVehicle.strategy.RemoveQueuedOrder();
  }

  public void OnStrategyButton()
  {
    if (!this.strategyButton.toggle.interactable)
      return;
    if (!this.IsPanelOpen(DriverActionButtons.InfoPanel.Strategy))
      this.OpenPanel(DriverActionButtons.InfoPanel.Strategy);
    else
      this.ClosePanel(DriverActionButtons.InfoPanel.Strategy);
  }

  public void OnCarConditionButton()
  {
    if (!this.IsPanelOpen(DriverActionButtons.InfoPanel.Condition))
      this.OpenPanel(DriverActionButtons.InfoPanel.Condition);
    else
      this.ClosePanel(DriverActionButtons.InfoPanel.Condition);
  }

  public void OnGarageEnter()
  {
    if (this.mSessionType != SessionDetails.SessionType.Practice)
      return;
    this.OpenPanel(DriverActionButtons.InfoPanel.StintFeedback);
  }

  public void OnGarageExit()
  {
    if (this.mSessionType != SessionDetails.SessionType.Practice)
      return;
    this.OpenPanel(DriverActionButtons.InfoPanel.Standings);
  }

  public void OnPitlaneExit()
  {
    if (this.mSessionType == SessionDetails.SessionType.Race && this.IsPanelOpen(DriverActionButtons.InfoPanel.PitStop) || (this.IsPanelOpen(DriverActionButtons.InfoPanel.Strategy) || this.IsPanelOpen(DriverActionButtons.InfoPanel.Condition)))
      return;
    this.OpenPanel(this.mSessionType != SessionDetails.SessionType.Qualifying || !this.mVehicle.timer.currentLap.isOutLap || this.mVehicle.pathState.IsInPitlaneArea() ? DriverActionButtons.InfoPanel.Standings : DriverActionButtons.InfoPanel.OutLap);
  }

  public void OnPitlaneEnter()
  {
    if (this.mVehicle.behaviourManager.isOutOfRace || this.mVehicle.timer.hasSeenChequeredFlag || Game.instance.sessionManager.flag == SessionManager.Flag.Chequered)
      return;
    this.OpenPanel(this.mSessionType != SessionDetails.SessionType.Race ? DriverActionButtons.InfoPanel.Standings : DriverActionButtons.InfoPanel.PitStop);
  }

  public void OnLapEnd()
  {
    if (this.mVehicle.pathState.IsInPitlaneArea())
      return;
    this.ClosePanel(DriverActionButtons.InfoPanel.OutLap);
  }

  public void OnPanelClosed()
  {
    this.OpenDefaultPanelForState();
  }

  private void OpenDefaultPanelForState()
  {
    bool flag1 = this.mSessionType == SessionDetails.SessionType.Qualifying && this.mVehicle.timer.currentLap.isOutLap && !this.mVehicle.pathState.IsInPitlaneArea();
    bool flag2 = this.mSessionType == SessionDetails.SessionType.Practice && this.mVehicle.pathState.IsInState(PathStateManager.StateType.Garage);
    bool flag3 = !this.mVehicle.behaviourManager.isOutOfRace && !this.mVehicle.timer.hasSeenChequeredFlag && (Game.instance.sessionManager.flag != SessionManager.Flag.Chequered && this.mSessionType == SessionDetails.SessionType.Race) && this.mVehicle.pathState.IsInPitlaneArea();
    RaceEventDetails eventDetails = Game.instance.sessionManager.eventDetails;
    bool flag4 = (eventDetails.currentSession.sessionType != SessionDetails.SessionType.Qualifying || !eventDetails.hasSeveralQualifyingSessions ? 0 : (eventDetails.results.IsDriverOutOfQualifying(this.mVehicle.driver) ? 1 : 0)) == 0;
    if (flag1)
      this.OpenPanel(DriverActionButtons.InfoPanel.OutLap);
    else if (flag2)
      this.OpenPanel(DriverActionButtons.InfoPanel.StintFeedback);
    else if (flag3)
      this.OpenPanel(DriverActionButtons.InfoPanel.PitStop);
    else if (flag4)
    {
      this.OpenPanel(DriverActionButtons.InfoPanel.Standings);
    }
    else
    {
      this.OpenPanel(DriverActionButtons.InfoPanel.Standings);
      GameUtility.SetActive(this.standings, false);
    }
  }

  public enum InfoPanel
  {
    Standings,
    Strategy,
    Condition,
    OutLap,
    PitStop,
    StintFeedback,
  }
}
