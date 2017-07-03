// Decompiled with JetBrains decompiler
// Type: DriverWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DriverWidget : MonoBehaviour
{
  public Color statusLabelColor = new Color();
  private DriverWidget.State mState = DriverWidget.State.Invalid;
  private int mPosition = -1;
  private float mFuelLapsRemaining = 100000f;
  public int selectedDriverID;
  public GameObject tyreInfo;
  public UICarSetupTyreIcon tyreIcon;
  public TextMeshProUGUI tyreWearLabel;
  public Image tyreTempPin;
  public RectTransform tyreTempTransform;
  public Gradient tyreTempGradient;
  public GameObject qualifyingTyreInfo;
  public UICarSetupTyreIcon qualifyingTyreIcon;
  public TextMeshProUGUI qualifyingTyreWearLabel;
  public GameObject fuelInfo;
  public TextMeshProUGUI fuelDeltaLabel;
  public TextMeshProUGUI fuelLapsLabel;
  public Transform engineModeFuelArrows;
  public Transform[] tyreStrategyArrows;
  public GameObject[] fuelUpArrows;
  public GameObject[] fuelDownArrows;
  public Slider fuelLevelSlider;
  public DriverTimerHUD timerHUD;
  public TextMeshProUGUI statusLabel;
  public TextMeshProUGUI qualifyingStatusLabel;
  public UICharacterPortrait driverPortrait;
  public TextMeshProUGUI driverNameLabel;
  public TextMeshProUGUI driverPositionLabel;
  public Button portraitButton;
  public GameObject pittingIcon;
  public UIBonusIcon bonusIcon;
  public GameObject crashedDataContainer;
  public GameObject retiredDataContainer;
  private SessionDetails.SessionType mSessionType;
  private RacingVehicle mVehicle;
  private float mTimeUntilNextRefresh;
  private bool mIsRefuellingAllowed;
  private float mFuelDeltaLaps;

  public Vehicle vehicle
  {
    get
    {
      return (Vehicle) this.mVehicle;
    }
  }

  private void Awake()
  {
    this.portraitButton.onClick.AddListener(new UnityAction(this.OnPortraitButton));
  }

  private void OnEnable()
  {
    if (!Game.IsActive() || !Game.instance.sessionManager.isCircuitLoaded)
      return;
    this.mSessionType = Game.instance.sessionManager.eventDetails.currentSession.sessionType;
    this.mFuelDeltaLaps = float.MaxValue;
    this.mIsRefuellingAllowed = Game.instance.player.team.championship.rules.isRefuelingOn;
    Driver selectedDriver = Game.instance.player.team.GetSelectedDriver(this.selectedDriverID);
    this.SetVehicle(Game.instance.vehicleManager.GetVehicle(selectedDriver));
  }

  private void SetVehicle(RacingVehicle inVehicle)
  {
    this.mVehicle = inVehicle;
    this.timerHUD.SetVehicle(this.mVehicle);
    this.SetState(DriverWidget.State.None);
    this.driverNameLabel.text = this.mVehicle.driver.lastName;
    this.driverPortrait.SetPortrait((Person) this.mVehicle.driver);
    this.mTimeUntilNextRefresh = 0.0f;
    this.bonusIcon.Setup(inVehicle);
    this.Update();
  }

  private void Update()
  {
    if (this.mPosition != this.mVehicle.standingsPosition)
    {
      this.mPosition = this.mVehicle.standingsPosition;
      if (Game.instance.sessionManager.sessionType == SessionDetails.SessionType.Race || this.mVehicle.timer.HasSetLapTime())
        this.driverPositionLabel.text = GameUtility.FormatForPosition(this.mPosition, (string) null);
      else if (Game.instance.sessionManager.sessionType == SessionDetails.SessionType.Qualifying && Game.instance.sessionManager.eventDetails.results.IsDriverOutOfQualifying(this.mVehicle.driver))
        this.driverPositionLabel.text = GameUtility.FormatForPosition(this.mPosition, (string) null);
      else
        this.driverPositionLabel.text = "-";
    }
    this.mTimeUntilNextRefresh -= GameTimer.deltaTime;
    if ((double) this.mTimeUntilNextRefresh < 0.0)
    {
      this.UpdateTyreWear();
      this.UpdateFuel();
      this.mTimeUntilNextRefresh = 1f;
    }
    this.UpdateTyreTemperature();
    this.UpdateFuelDeltaGauge();
    bool flag = (Object) App.instance.cameraManager.target == (Object) null || (Object) App.instance.cameraManager.target.transform != (Object) this.mVehicle.unityTransform;
    if (this.portraitButton.interactable != flag)
      this.portraitButton.interactable = flag;
    ColorBlock colors = this.portraitButton.colors;
    colors.disabledColor = colors.highlightedColor;
    this.portraitButton.colors = colors;
    GameUtility.SetActive(this.pittingIcon, !this.mVehicle.pathState.IsInPitlaneArea() && this.mVehicle.strategy.IsGoingToPit());
    this.UpdateState();
  }

  private void UpdateTyreWear()
  {
    this.tyreIcon.SetTyre(this.mVehicle.setup.tyreSet);
    this.qualifyingTyreIcon.SetTyre(this.mVehicle.setup.tyreSet);
    this.tyreWearLabel.text = this.mVehicle.setup.tyreSet.GetConditionText();
    this.qualifyingTyreWearLabel.text = this.tyreWearLabel.text;
    if ((double) this.mVehicle.setup.tyreSet.GetCondition() < 0.25)
      this.tyreWearLabel.color = UIConstants.negativeColor;
    else
      this.tyreWearLabel.color = UIConstants.positiveColor;
    for (int index1 = 0; index1 < this.tyreStrategyArrows.Length; ++index1)
    {
      if (this.tyreStrategyArrows[index1].gameObject.activeSelf)
      {
        for (int index2 = 0; index2 < this.tyreStrategyArrows[index1].childCount; ++index2)
          GameUtility.SetActive(this.tyreStrategyArrows[index1].GetChild(index2).gameObject, (DrivingStyle.Mode) index2 == this.mVehicle.performance.drivingStyleMode);
      }
    }
  }

  private void UpdateTyreTemperature()
  {
    float temperature = this.mVehicle.setup.tyreSet.GetTemperature();
    this.tyreTempPin.color = this.tyreTempGradient.Evaluate(temperature);
    this.tyreTempTransform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Lerp(-40f, 40f, temperature));
  }

  private void UpdateFuel()
  {
    using (GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe())
    {
      StringBuilder stringBuilder = builderSafe.stringBuilder;
      GameUtility.SetSliderAmountIfDifferent(this.fuelLevelSlider, this.mVehicle.performance.fuel.GetNormalisedFuelLevel(), 1000f);
      float targetFuelLapDelta = this.mVehicle.performance.fuel.GetTargetFuelLapDelta();
      float remainingDecimal = this.mVehicle.performance.fuel.GetFuelLapsRemainingDecimal();
      for (int index = 0; index < this.engineModeFuelArrows.childCount; ++index)
        GameUtility.SetActive(this.engineModeFuelArrows.GetChild(index).gameObject, (Fuel.EngineMode) index == this.mVehicle.performance.fuel.engineMode);
      if (this.mIsRefuellingAllowed)
      {
        if ((double) ((float) this.mVehicle.timer.lap + this.mVehicle.pathController.GetDistanceAlongPath01(PathController.PathType.Track) + remainingDecimal) >= (double) Game.instance.sessionManager.lapCount)
        {
          this.fuelDeltaLabel.color = UIConstants.positiveColor;
          this.fuelDeltaLabel.text = Localisation.LocaliseID("PSG_10011233", (GameObject) null);
        }
        else if ((double) remainingDecimal <= 1.0)
        {
          this.fuelDeltaLabel.color = UIConstants.negativeColor;
          this.fuelDeltaLabel.text = Localisation.LocaliseID("PSG_10011232", (GameObject) null);
        }
        else
          this.fuelDeltaLabel.text = string.Empty;
      }
      else if ((double) Mathf.Abs(targetFuelLapDelta - this.mFuelDeltaLaps) > 1.40129846432482E-45)
      {
        this.mFuelDeltaLaps = targetFuelLapDelta;
        float num1 = Mathf.Abs(this.mFuelDeltaLaps);
        int num2 = (int) num1;
        int num3 = (int) (((double) num1 - (double) num2) * 100.0);
        if ((double) this.mFuelDeltaLaps < 0.0)
        {
          if ((double) this.mFuelDeltaLaps <= -0.00999999977648258)
          {
            GameUtility.Format(stringBuilder, !Localisation.UseCommaDecimalSeperator ? "-0.00" : "-0,00", num2, num3);
            this.fuelDeltaLabel.color = UIConstants.negativeColor;
          }
          else
          {
            GameUtility.Format(stringBuilder, !Localisation.UseCommaDecimalSeperator ? " 0.00" : " 0,00", num2, num3);
            this.fuelDeltaLabel.color = UIConstants.positiveColor;
          }
        }
        else
        {
          GameUtility.Format(stringBuilder, !Localisation.UseCommaDecimalSeperator ? "+0.00" : "+0,00", num2, num3);
          this.fuelDeltaLabel.color = UIConstants.positiveColor;
        }
        this.fuelDeltaLabel.text = stringBuilder.ToString();
      }
      this.mFuelLapsRemaining = remainingDecimal;
      StringVariableParser.stringValue1 = Mathf.Max(0.0f, this.mFuelLapsRemaining).ToString("0.00");
      this.fuelLapsLabel.text = (double) remainingDecimal != 1.0 ? Localisation.LocaliseID("PSG_10011103", (GameObject) null) : Localisation.LocaliseID("PSG_10011102", (GameObject) null);
    }
  }

  private void UpdateFuelDeltaGauge()
  {
    switch (this.mVehicle.performance.fuel.engineMode)
    {
      case Fuel.EngineMode.SuperOvertake:
      case Fuel.EngineMode.Overtake:
        GameUtility.SetActive(this.fuelUpArrows[0], false);
        GameUtility.SetActive(this.fuelUpArrows[1], false);
        GameUtility.SetActive(this.fuelDownArrows[0], true);
        GameUtility.SetActive(this.fuelDownArrows[1], true);
        break;
      case Fuel.EngineMode.High:
        GameUtility.SetActive(this.fuelUpArrows[0], false);
        GameUtility.SetActive(this.fuelUpArrows[1], false);
        GameUtility.SetActive(this.fuelDownArrows[0], true);
        GameUtility.SetActive(this.fuelDownArrows[1], false);
        break;
      case Fuel.EngineMode.Medium:
        GameUtility.SetActive(this.fuelUpArrows[0], false);
        GameUtility.SetActive(this.fuelUpArrows[1], true);
        GameUtility.SetActive(this.fuelDownArrows[0], false);
        GameUtility.SetActive(this.fuelDownArrows[1], false);
        break;
      case Fuel.EngineMode.Low:
        GameUtility.SetActive(this.fuelUpArrows[0], true);
        GameUtility.SetActive(this.fuelUpArrows[1], true);
        GameUtility.SetActive(this.fuelDownArrows[0], false);
        GameUtility.SetActive(this.fuelDownArrows[1], false);
        break;
    }
  }

  public void OnPortraitButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    App.instance.cameraManager.SetTarget((Vehicle) this.mVehicle, CameraManager.Transition.Smooth);
  }

  private void UpdateState()
  {
    if (this.mVehicle.behaviourManager.isCrashed)
      this.SetState(DriverWidget.State.Crashed);
    else if (this.mVehicle.behaviourManager.isRetired)
      this.SetState(DriverWidget.State.Retired);
    else if (this.mVehicle.pathState.pathStateGroup == PathStateManager.PathStateGroup.InGarage)
      this.SetState(DriverWidget.State.Garage);
    else if (this.mVehicle.pathState.pathStateGroup == PathStateManager.PathStateGroup.OnGrid)
    {
      this.SetState(DriverWidget.State.OnGrid);
    }
    else
    {
      SessionDetails.SessionType sessionType = Game.instance.sessionManager.eventDetails.currentSession.sessionType;
      if (sessionType == SessionDetails.SessionType.Qualifying && this.mVehicle.timer.currentLap.isOutLap && !this.mVehicle.pathState.IsInPitlaneArea())
        this.SetState(DriverWidget.State.None);
      else if (sessionType == SessionDetails.SessionType.Race && this.mVehicle.pathState.IsInPitlaneArea())
        this.SetState(DriverWidget.State.RacePitlane);
      else if (sessionType != SessionDetails.SessionType.Race && this.mVehicle.timer.currentLap.isInLap)
        this.SetState(DriverWidget.State.OnTimedInLap);
      else if (sessionType != SessionDetails.SessionType.Race && this.mVehicle.timer.currentLap.isOutLap)
        this.SetState(DriverWidget.State.OnTimedOutLap);
      else
        this.SetState(DriverWidget.State.OnLap);
    }
  }

  private void SetState(DriverWidget.State inState)
  {
    if (this.mState == inState)
      return;
    this.mState = inState;
    this.statusLabel.color = this.statusLabelColor;
    switch (this.mState)
    {
      case DriverWidget.State.OnGrid:
        this.timerHUD.Hide();
        GameUtility.SetActive(this.tyreInfo, this.mSessionType != SessionDetails.SessionType.Qualifying);
        GameUtility.SetActive(this.fuelInfo, this.mSessionType != SessionDetails.SessionType.Qualifying);
        GameUtility.SetActive(this.qualifyingTyreInfo, this.mSessionType == SessionDetails.SessionType.Qualifying);
        this.statusLabel.gameObject.SetActive(true);
        this.statusLabel.text = Localisation.LocaliseID("PSG_10000459", (GameObject) null);
        GameUtility.SetActive(this.qualifyingStatusLabel.gameObject, false);
        GameUtility.SetActive(this.crashedDataContainer, false);
        GameUtility.SetActive(this.retiredDataContainer, false);
        break;
      case DriverWidget.State.OnLap:
        this.timerHUD.Show();
        GameUtility.SetActive(this.tyreInfo, this.mSessionType != SessionDetails.SessionType.Qualifying);
        GameUtility.SetActive(this.fuelInfo, this.mSessionType != SessionDetails.SessionType.Qualifying);
        GameUtility.SetActive(this.qualifyingTyreInfo, this.mSessionType == SessionDetails.SessionType.Qualifying);
        this.statusLabel.gameObject.SetActive(false);
        GameUtility.SetActive(this.qualifyingStatusLabel.gameObject, false);
        GameUtility.SetActive(this.crashedDataContainer, false);
        GameUtility.SetActive(this.retiredDataContainer, false);
        break;
      case DriverWidget.State.OnTimedOutLap:
        this.timerHUD.Hide();
        GameUtility.SetActive(this.tyreInfo, this.mSessionType != SessionDetails.SessionType.Qualifying);
        GameUtility.SetActive(this.fuelInfo, this.mSessionType != SessionDetails.SessionType.Qualifying);
        GameUtility.SetActive(this.qualifyingTyreInfo, this.mSessionType == SessionDetails.SessionType.Qualifying);
        this.statusLabel.gameObject.SetActive(true);
        this.statusLabel.text = Localisation.LocaliseID("PSG_10010426", (GameObject) null);
        GameUtility.SetActive(this.qualifyingStatusLabel.gameObject, false);
        GameUtility.SetActive(this.crashedDataContainer, false);
        GameUtility.SetActive(this.retiredDataContainer, false);
        break;
      case DriverWidget.State.OnTimedInLap:
        this.timerHUD.Hide();
        GameUtility.SetActive(this.tyreInfo, this.mSessionType != SessionDetails.SessionType.Qualifying);
        GameUtility.SetActive(this.fuelInfo, this.mSessionType != SessionDetails.SessionType.Qualifying);
        GameUtility.SetActive(this.qualifyingTyreInfo, this.mSessionType == SessionDetails.SessionType.Qualifying);
        this.statusLabel.gameObject.SetActive(true);
        this.statusLabel.text = Localisation.LocaliseID("PSG_10010425", (GameObject) null);
        GameUtility.SetActive(this.qualifyingStatusLabel.gameObject, false);
        GameUtility.SetActive(this.crashedDataContainer, false);
        GameUtility.SetActive(this.retiredDataContainer, false);
        break;
      case DriverWidget.State.RacePitlane:
        this.timerHUD.Hide();
        GameUtility.SetActive(this.tyreInfo, this.mSessionType != SessionDetails.SessionType.Qualifying);
        GameUtility.SetActive(this.fuelInfo, this.mSessionType != SessionDetails.SessionType.Qualifying);
        GameUtility.SetActive(this.qualifyingTyreInfo, this.mSessionType == SessionDetails.SessionType.Qualifying);
        this.statusLabel.gameObject.SetActive(false);
        GameUtility.SetActive(this.qualifyingStatusLabel.gameObject, false);
        GameUtility.SetActive(this.crashedDataContainer, false);
        GameUtility.SetActive(this.retiredDataContainer, false);
        break;
      case DriverWidget.State.Garage:
        this.timerHUD.Hide();
        GameUtility.SetActive(this.tyreInfo, false);
        GameUtility.SetActive(this.fuelInfo, false);
        GameUtility.SetActive(this.qualifyingTyreInfo, false);
        this.statusLabel.gameObject.SetActive(true);
        GameUtility.SetActive(this.crashedDataContainer, false);
        GameUtility.SetActive(this.retiredDataContainer, false);
        RaceEventDetails eventDetails = Game.instance.sessionManager.eventDetails;
        if (eventDetails.currentSession.sessionType == SessionDetails.SessionType.Qualifying && eventDetails.hasSeveralQualifyingSessions && Game.instance.sessionManager.eventDetails.results.IsDriverOutOfQualifying(this.mVehicle.driver))
        {
          GameUtility.SetActive(this.qualifyingStatusLabel.gameObject, true);
          StringVariableParser.stringValue1 = GameUtility.FormatForPosition(Game.instance.sessionManager.eventDetails.results.GetDriverQualifyingData(this.mVehicle.driver).position, (string) null);
          this.qualifyingStatusLabel.text = Localisation.LocaliseID("PSG_10011502", (GameObject) null);
          this.statusLabel.text = Localisation.LocaliseID("PSG_10011492", (GameObject) null);
          this.statusLabel.color = UIConstants.qualifyingEliminationColorForPositionLabel;
          break;
        }
        GameUtility.SetActive(this.qualifyingStatusLabel.gameObject, false);
        this.statusLabel.text = Localisation.LocaliseID("PSG_10000417", (GameObject) null);
        break;
      case DriverWidget.State.Crashed:
        this.timerHUD.Hide();
        GameUtility.SetActive(this.tyreInfo, false);
        GameUtility.SetActive(this.fuelInfo, false);
        GameUtility.SetActive(this.qualifyingTyreInfo, false);
        GameUtility.SetActive(this.qualifyingStatusLabel.gameObject, false);
        GameUtility.SetActive(this.crashedDataContainer, true);
        GameUtility.SetActive(this.retiredDataContainer, false);
        this.statusLabel.gameObject.SetActive(false);
        break;
      case DriverWidget.State.Retired:
        this.timerHUD.Hide();
        GameUtility.SetActive(this.tyreInfo, false);
        GameUtility.SetActive(this.fuelInfo, false);
        GameUtility.SetActive(this.qualifyingTyreInfo, false);
        GameUtility.SetActive(this.qualifyingStatusLabel.gameObject, false);
        GameUtility.SetActive(this.crashedDataContainer, false);
        GameUtility.SetActive(this.retiredDataContainer, true);
        this.statusLabel.gameObject.SetActive(false);
        break;
      default:
        this.timerHUD.Hide();
        GameUtility.SetActive(this.tyreInfo, false);
        GameUtility.SetActive(this.fuelInfo, false);
        GameUtility.SetActive(this.qualifyingTyreInfo, false);
        this.statusLabel.gameObject.SetActive(false);
        GameUtility.SetActive(this.qualifyingStatusLabel.gameObject, false);
        break;
    }
  }

  public void HideWidgets()
  {
    this.SetState(DriverWidget.State.None);
  }

  public enum State
  {
    None,
    OnGrid,
    OnLap,
    OnTimedOutLap,
    OnTimedInLap,
    RacePitlane,
    Garage,
    Crashed,
    Retired,
    Invalid,
  }
}
