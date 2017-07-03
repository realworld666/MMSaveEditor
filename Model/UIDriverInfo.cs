// Decompiled with JetBrains decompiler
// Type: UIDriverInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDriverInfo : MonoBehaviour
{
  private Vector3 mVehicleWorldPosition = Vector3.zero;
  private int mDriverPosition = -1;
  public RectTransform canvasRectTransform;
  public GameObject playerTeamBacking;
  public Image teamColorSlashImage;
  public Flag flag;
  public UICharacterPortrait portrait;
  public TextMeshProUGUI positionLabel;
  public TextMeshProUGUI nameLabel;
  public UICarSetupTyreIcon tyreIcon;
  public TextMeshProUGUI tyreWearLabel;
  public GameObject[] drivingStyleIcons;
  public GameObject[] engineModeIcons;
  public GameObject damageIcon;
  public GameObject punctureIcon;
  public GameObject blueFlagIcon;
  public GameObject pitIcon;
  public GameObject conditionWarning;
  public GameObject powerMode;
  public GameObject rivalIcon;
  public GameObject currentLapContainer;
  public TextMeshProUGUI currentLapLabel;
  public GameObject speedTrapInfo;
  public TextMeshProUGUI speedTrapLabel;
  public DriverTimerHUD timerHUD;
  public TextMeshProUGUI statusLabel;
  public GameObject finished;
  public UIDriverDebugInfo driverDebugInfo;
  private RectTransform mRectTransform;
  private float mNextUpdateTime;
  private RacingVehicle mVehicle;
  private float mSpeedTrapDisplayTime;

  public Vehicle vehicle
  {
    get
    {
      return (Vehicle) this.mVehicle;
    }
  }

  public bool isShowing
  {
    get
    {
      return this.gameObject.activeSelf;
    }
  }

  private void Awake()
  {
    this.driverDebugInfo.ActivateDebug(false);
    this.Hide();
  }

  public void Show(RacingVehicle inVehicle)
  {
    this.driverDebugInfo.Setup(inVehicle);
    this.mVehicle = inVehicle;
    if ((Object) this.speedTrapInfo != (Object) null)
      GameUtility.SetActive(this.speedTrapInfo, false);
    this.Show();
  }

  public void Show()
  {
    if (this.mVehicle == null)
      return;
    if ((Object) this.mRectTransform == (Object) null)
      this.mRectTransform = this.GetComponent<RectTransform>();
    if ((Object) this.nameLabel != (Object) null)
      this.nameLabel.text = this.mVehicle.driver.lastName;
    if ((Object) this.flag != (Object) null)
      this.flag.SetNationality(this.mVehicle.driver.nationality);
    if ((Object) this.portrait != (Object) null)
      this.portrait.SetPortrait((Person) this.mVehicle.driver);
    if ((Object) this.timerHUD != (Object) null)
      this.timerHUD.SetVehicle(this.mVehicle);
    if ((Object) this.playerTeamBacking != (Object) null)
      this.playerTeamBacking.SetActive(this.mVehicle.driver.IsPlayersDriver());
    if ((Object) this.teamColorSlashImage != (Object) null)
      this.teamColorSlashImage.color = this.mVehicle.driver.GetTeamColor().primaryUIColour.normal;
    if ((Object) this.rivalIcon != (Object) null)
      GameUtility.SetActive(this.rivalIcon, !this.mVehicle.driver.IsPlayersDriver());
    if ((Object) this.currentLapContainer != (Object) null)
      GameUtility.SetActive(this.currentLapContainer, Game.instance.sessionManager.eventDetails.currentSession.sessionType == SessionDetails.SessionType.Race);
    this.mNextUpdateTime = 0.0f;
    GameUtility.SetActive(this.gameObject, true);
    this.Update();
  }

  public void HideAndClearVehicle()
  {
    this.mVehicle = (RacingVehicle) null;
    this.Hide();
  }

  public void Hide()
  {
    GameUtility.SetActive(this.gameObject, false);
  }

  private void Update()
  {
    if (this.mVehicle == null)
    {
      this.Hide();
    }
    else
    {
      if ((double) this.mNextUpdateTime < (double) Time.time)
      {
        this.mNextUpdateTime = Time.time + 1f;
        if ((Object) this.positionLabel != (Object) null)
        {
          if (Game.instance.sessionManager.sessionType == SessionDetails.SessionType.Race || this.mVehicle.timer.HasSetLapTime())
          {
            if (this.mDriverPosition != this.mVehicle.standingsPosition)
            {
              this.mDriverPosition = this.mVehicle.standingsPosition;
              this.positionLabel.text = GameUtility.FormatForPosition(this.mDriverPosition, (string) null);
            }
          }
          else
            this.positionLabel.text = "-";
        }
        if ((Object) this.currentLapLabel != (Object) null && Game.instance.sessionManager.sessionType == SessionDetails.SessionType.Race)
        {
          GameUtility.SetActiveAndCheckNull(this.currentLapContainer, !this.mVehicle.timer.hasSeenChequeredFlag);
          int num = this.mVehicle.timer.lap + 1;
          if (num >= Game.instance.sessionManager.lapCount || Game.instance.sessionManager.IsSessionEnding())
          {
            this.currentLapLabel.text = Localisation.LocaliseID("PSG_10011024", (GameObject) null);
          }
          else
          {
            StringVariableParser.intValue1 = num;
            this.currentLapLabel.text = Localisation.LocaliseID("PSG_10010418", (GameObject) null);
          }
        }
        GameUtility.SetActive(this.powerMode, this.mVehicle.ERSController != null && this.mVehicle.ERSController.mode == ERSController.Mode.Power);
        GameUtility.SetActive(this.damageIcon, false);
        GameUtility.SetActive(this.punctureIcon, this.mVehicle.setup.tyreSet.isPunctured);
        GameUtility.SetActive(this.conditionWarning, Game.instance.sessionManager.sessionType == SessionDetails.SessionType.Race && this.mVehicle.car.AreAnyPartsOnRedCondition());
        GameUtility.SetActive(this.pitIcon, !this.mVehicle.pathState.IsInPitlaneArea() && this.mVehicle.strategy.IsGoingToPit());
        if (this.mVehicle.timer.hasSeenChequeredFlag)
        {
          GameUtility.SetActive(this.finished, true);
          GameUtility.SetActive(this.statusLabel.gameObject, false);
          if ((Object) this.timerHUD != (Object) null)
            GameUtility.SetActive(this.timerHUD.gameObject, false);
        }
        else
        {
          GameUtility.SetActive(this.finished, false);
          if (this.mVehicle.pathState.pathStateGroup == PathStateManager.PathStateGroup.InGarage)
          {
            GameUtility.SetActive(this.statusLabel.gameObject, true);
            if ((Object) this.timerHUD != (Object) null)
              GameUtility.SetActive(this.timerHUD.gameObject, false);
            this.statusLabel.text = Localisation.LocaliseID("PSG_10000417", (GameObject) null);
          }
          else if (this.mVehicle.pathState.pathStateGroup == PathStateManager.PathStateGroup.OnGrid)
          {
            GameUtility.SetActive(this.statusLabel.gameObject, true);
            if ((Object) this.timerHUD != (Object) null)
              GameUtility.SetActive(this.timerHUD.gameObject, false);
            this.statusLabel.text = Localisation.LocaliseID("PSG_10000459", (GameObject) null);
          }
          else
          {
            SessionDetails.SessionType sessionType = Game.instance.sessionManager.eventDetails.currentSession.sessionType;
            if (sessionType == SessionDetails.SessionType.Race && this.mVehicle.pathState.IsInPitlaneArea())
            {
              GameUtility.SetActive(this.statusLabel.gameObject, true);
              if ((Object) this.timerHUD != (Object) null)
                GameUtility.SetActive(this.timerHUD.gameObject, false);
              this.statusLabel.text = Localisation.LocaliseID("PSG_10000418", (GameObject) null);
            }
            else if (sessionType != SessionDetails.SessionType.Race && this.mVehicle.timer.currentLap.isInLap)
            {
              GameUtility.SetActive(this.statusLabel.gameObject, true);
              if ((Object) this.timerHUD != (Object) null)
                GameUtility.SetActive(this.timerHUD.gameObject, false);
              this.statusLabel.text = Localisation.LocaliseID("PSG_10010425", (GameObject) null);
            }
            else if (sessionType != SessionDetails.SessionType.Race && this.mVehicle.timer.currentLap.isOutLap)
            {
              GameUtility.SetActive(this.statusLabel.gameObject, true);
              if ((Object) this.timerHUD != (Object) null)
                GameUtility.SetActive(this.timerHUD.gameObject, false);
              this.statusLabel.text = Localisation.LocaliseID("PSG_10010426", (GameObject) null);
            }
            else
            {
              GameUtility.SetActive(this.statusLabel.gameObject, false);
              if ((Object) this.timerHUD != (Object) null)
                GameUtility.SetActive(this.timerHUD.gameObject, true);
            }
          }
        }
        GameUtility.SetActive(this.blueFlagIcon, this.mVehicle.behaviourManager.currentBehaviour is AIBlueFlagBehaviour);
        if ((Object) this.tyreIcon != (Object) null)
          this.tyreIcon.SetTyre(this.mVehicle.setup.tyreSet);
        if ((Object) this.tyreWearLabel != (Object) null)
        {
          this.tyreWearLabel.text = this.mVehicle.setup.tyreSet.GetConditionText();
          if ((double) this.mVehicle.setup.tyreSet.GetCondition() < 0.25)
            this.tyreWearLabel.color = UIConstants.negativeColor;
          else
            this.tyreWearLabel.color = UIConstants.positiveColor;
        }
        this.UpdateDrivingStyleIcon();
        this.UpdateEngineModeIcon();
      }
      this.UpdateSpeedTrap();
      this.mVehicleWorldPosition = this.mVehicle.unityTransform.position;
    }
  }

  private void UpdateSpeedTrap()
  {
    if (!((Object) this.speedTrapInfo != (Object) null))
      return;
    if (this.speedTrapInfo.activeSelf)
    {
      this.mSpeedTrapDisplayTime -= GameTimer.deltaTime;
      if ((double) this.mSpeedTrapDisplayTime >= 0.0)
        return;
      GameUtility.SetActive(this.speedTrapInfo, false);
    }
    else
    {
      if (Game.instance.sessionManager.sessionType != SessionDetails.SessionType.Race || this.mVehicle.pathController.currentPathType != PathController.PathType.Track || (!this.mVehicle.pathController.GetPreviousGate().isSpeedTrap || Game.instance.sessionManager.lap <= 1))
        return;
      CarStats.RelevantToCircuit relevancy = Game.instance.sessionManager.eventDetails.circuit.GetRelevancy(CarStats.StatType.TopSpeed);
      float num = 1f;
      switch (relevancy)
      {
        case CarStats.RelevantToCircuit.No:
        case CarStats.RelevantToCircuit.Useful:
          num = 0.92f;
          break;
        case CarStats.RelevantToCircuit.VeryUseful:
          num = 0.96f;
          break;
        case CarStats.RelevantToCircuit.VeryImportant:
          num = 1f;
          break;
      }
      this.speedTrapLabel.text = GameUtility.GetSpeedText(this.mVehicle.speedTrapSpeed * num, 1f);
      this.mSpeedTrapDisplayTime = 5f;
      GameUtility.SetActive(this.speedTrapInfo, true);
    }
  }

  public void UpdatePosition()
  {
    if (this.mVehicle == null)
      return;
    bool flag = !App.instance.preferencesManager.videoPreferences.isSimulationRunning2DMode;
    Vector2 zero = Vector2.zero;
    Vector2 vector2 = !flag ? (Vector2) Simulation2D.instance.camera.WorldToViewportPoint(Simulation2D.instance.miniMapWidget.GetWorldPositionOfEntry((Vehicle) this.mVehicle)) : (Vector2) App.instance.cameraManager.GetCamera().WorldToViewportPoint(this.mVehicleWorldPosition);
    vector2.x = (float) ((double) vector2.x * (double) this.canvasRectTransform.sizeDelta.x - (double) this.canvasRectTransform.sizeDelta.x * 0.5);
    vector2.y = (float) ((double) vector2.y * (double) this.canvasRectTransform.sizeDelta.y - (double) this.canvasRectTransform.sizeDelta.y * 0.5);
    this.mRectTransform.anchoredPosition = vector2;
  }

  private void UpdateDrivingStyleIcon()
  {
    if (this.drivingStyleIcons == null || this.drivingStyleIcons.Length <= 0)
      return;
    int drivingStyleMode = (int) this.mVehicle.performance.drivingStyleMode;
    for (int index = 0; index < this.drivingStyleIcons.Length; ++index)
      this.drivingStyleIcons[index].SetActive(index == drivingStyleMode);
  }

  private void UpdateEngineModeIcon()
  {
    if (this.engineModeIcons == null || this.engineModeIcons.Length <= 0)
      return;
    int engineMode = (int) this.mVehicle.performance.fuel.engineMode;
    for (int index = 0; index < this.engineModeIcons.Length; ++index)
      this.engineModeIcons[index].SetActive(index == engineMode);
  }
}
