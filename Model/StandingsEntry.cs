// Decompiled with JetBrains decompiler
// Type: StandingsEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StandingsEntry : MonoBehaviour
{
  public Color positionLabelColor = new Color();
  public Color currentLapInfoLabelColor = new Color();
  private int mPosition = -1;
  public Button button;
  public Image colourSlashImage;
  public TextMeshProUGUI driverNameLabel;
  public TextMeshProUGUI positionLabel;
  public GameObject positionUp;
  public GameObject positionDown;
  public UICarSetupTyreIcon tyreIcon;
  public TextMeshProUGUI qualifyingThresholdLabel;
  public GameObject qualifyingLineObject;
  public GameObject objectiveLineObject;
  public Image objectiveLineImage;
  public CanvasGroup canvasGroup;
  public GameObject crashedIcon;
  public GameObject retiredIcon;
  public GameObject vehicleStatusIcon;
  public TextMeshProUGUI vehicleStatusLabel;
  public GameObject chequeredFlagIcon;
  public GameObject ERSPowerIcon;
  public GameObject race;
  public GameObject practiceQualifying;
  public TextMeshProUGUI stopsLabel;
  public TextMeshProUGUI raceLastLapLabel;
  public GameObject currentLapInfoContainer;
  public TextMeshProUGUI currentLapInfoLabel;
  public Image[] qualifyingSectorDot;
  public Image[] raceSectorDot;
  public TextMeshProUGUI qualifyingLastLapLabel;
  public TextMeshProUGUI fastestLapLabel;
  public GameObject[] engineModes;
  public GameObject[] drivingStyles;
  public TextMeshProUGUI timedLeaderLabel;
  public TextMeshProUGUI timedGapLabel;
  public TextMeshProUGUI raceLeaderLabel;
  public TextMeshProUGUI raceGapLabel;
  public TextMeshProUGUI batteryChargeLabel;
  public float regularLayoutSize;
  public float withBatteryInfoLayoutSize;
  public LayoutElement layout;
  private StandingsEntry.Mode mMode;
  private RacingVehicle mVehicle;
  private bool mShowObjective;
  private int mSponsorTarget;
  private int mEntryIndex;
  private Transform mChangeUpTransform;
  private Transform mChangeDownTransform;
  private float mChangeUpDefaultX;
  private float mChangeDownDefaultX;

  private void Awake()
  {
    this.button.onClick.AddListener(new UnityAction(this.OnButtonPressed));
    this.canvasGroup.alpha = 1f;
    this.SetupImageTransforms();
    this.HideImage(this.mChangeUpTransform, this.mChangeUpDefaultX);
    this.HideImage(this.mChangeDownTransform, this.mChangeDownDefaultX);
  }

  private void Start()
  {
  }

  public void ShowObjective(bool inShow)
  {
    this.mShowObjective = inShow;
  }

  public void SetVehicle(int inEntryIndex, RacingVehicle inVehicle, bool inIsHighlighted, StandingsEntry.Mode inMode, SessionDetails.SessionType inSessionType)
  {
    this.mVehicle = inVehicle;
    this.mMode = inMode;
    this.positionLabel.color = this.positionLabelColor;
    SponsorController sponsorController = Game.instance.player.team.sponsorController;
    SponsorshipDeal weekendSponsorshipDeal = sponsorController.weekendSponsorshipDeal;
    this.mEntryIndex = inEntryIndex + 1;
    this.mSponsorTarget = -1;
    if (inSessionType == SessionDetails.SessionType.Race || this.mVehicle.timer.HasSetLapTime())
    {
      if (this.mVehicle.standingsPosition != this.mPosition)
      {
        this.mPosition = this.mVehicle.standingsPosition;
        this.positionLabel.text = this.mPosition.ToString();
      }
    }
    else if (this.mPosition != 0)
    {
      this.mPosition = 0;
      this.positionLabel.text = "-";
    }
    this.colourSlashImage.color = this.mVehicle.driver.GetTeamColor().primaryUIColour.normal;
    this.driverNameLabel.text = this.mVehicle.driver.lastName;
    if (this.mVehicle.behaviourManager.isCrashed)
    {
      this.SetCrashRetiredData();
      GameUtility.SetActive(this.crashedIcon, true);
    }
    else if (this.mVehicle.behaviourManager.isRetired)
    {
      this.SetCrashRetiredData();
      GameUtility.SetActive(this.retiredIcon, true);
    }
    else
    {
      this.canvasGroup.alpha = 1f;
      GameUtility.SetActive(this.crashedIcon, false);
      GameUtility.SetActive(this.retiredIcon, false);
      StandingsEntry.SetVehicleStatusLabel(this.mVehicle, this.vehicleStatusIcon, this.vehicleStatusLabel);
      GameUtility.SetActive(this.chequeredFlagIcon, this.mVehicle.timer.hasSeenChequeredFlag);
      GameUtility.SetActiveAndCheckNull(this.ERSPowerIcon, this.mVehicle.ERSController != null && this.mVehicle.ERSController.mode == ERSController.Mode.Power);
    }
    this.SetButtonColor(inIsHighlighted);
    if (this.mVehicle.standingsPosition < this.mVehicle.previousStandingsPosition)
    {
      this.ShowImage(this.mChangeUpTransform, this.mChangeUpDefaultX);
      this.HideImage(this.mChangeDownTransform, this.mChangeDownDefaultX);
    }
    else if (this.mVehicle.standingsPosition > this.mVehicle.previousStandingsPosition)
    {
      this.HideImage(this.mChangeUpTransform, this.mChangeUpDefaultX);
      this.ShowImage(this.mChangeDownTransform, this.mChangeDownDefaultX);
    }
    else
    {
      this.HideImage(this.mChangeUpTransform, this.mChangeUpDefaultX);
      this.HideImage(this.mChangeDownTransform, this.mChangeDownDefaultX);
    }
    switch (inSessionType)
    {
      case SessionDetails.SessionType.Qualifying:
        if (weekendSponsorshipDeal != null && sponsorController.qualifyingObjective != null)
        {
          this.mSponsorTarget = sponsorController.qualifyingObjective.targetResult;
          break;
        }
        break;
      case SessionDetails.SessionType.Race:
        if (weekendSponsorshipDeal != null && sponsorController.raceObjective != null)
        {
          this.mSponsorTarget = sponsorController.raceObjective.targetResult;
          break;
        }
        break;
    }
    if (this.mMode == StandingsEntry.Mode.Detailed)
    {
      switch (inSessionType)
      {
        case SessionDetails.SessionType.Practice:
          GameUtility.SetActiveAndCheckNull(this.practiceQualifying, true);
          GameUtility.SetActiveAndCheckNull(this.race, false);
          break;
        case SessionDetails.SessionType.Qualifying:
          GameUtility.SetActiveAndCheckNull(this.practiceQualifying, true);
          GameUtility.SetActiveAndCheckNull(this.race, false);
          break;
        case SessionDetails.SessionType.Race:
          GameUtility.SetActiveAndCheckNull(this.practiceQualifying, false);
          GameUtility.SetActiveAndCheckNull(this.race, true);
          if (this.engineModes != null && this.engineModes.Length > 0)
          {
            int engineMode = (int) this.mVehicle.performance.fuel.engineMode;
            for (int index = 0; index < this.engineModes.Length; ++index)
              this.engineModes[index].SetActive(index == engineMode);
          }
          if (this.drivingStyles != null && this.drivingStyles.Length > 0)
          {
            int drivingStyleMode = (int) this.mVehicle.performance.drivingStyleMode;
            for (int index = 0; index < this.drivingStyles.Length; ++index)
              this.drivingStyles[index].SetActive(index == drivingStyleMode);
            break;
          }
          break;
      }
      if ((Object) this.stopsLabel != (Object) null)
        this.stopsLabel.text = this.mVehicle.timer.stops.ToString();
      if (this.mVehicle.timer.HasSetLapTime())
      {
        this.raceLastLapLabel.text = GameUtility.GetLapTimeText(this.mVehicle.timer.GetPreviousLapData().time, false);
        this.qualifyingLastLapLabel.text = GameUtility.GetLapTimeText(this.mVehicle.timer.GetPreviousLapData().time, false);
      }
      else
      {
        this.raceLastLapLabel.text = GameUtility.GetLapTimeText(0.0f, false);
        this.qualifyingLastLapLabel.text = GameUtility.GetLapTimeText(0.0f, false);
      }
      this.fastestLapLabel.text = GameUtility.GetLapTimeText(this.mVehicle.timer.GetFastestLapTime(), false);
      if ((Object) this.batteryChargeLabel != (Object) null)
      {
        GameUtility.SetActive(this.batteryChargeLabel.gameObject, Game.instance.sessionManager.championship.rules.isEnergySystemActive);
        if (this.mVehicle.ERSController != null && this.batteryChargeLabel.gameObject.activeSelf && this.mVehicle.ERSController.IsSetup())
          this.batteryChargeLabel.text = string.Format("{0}% ", (object) Mathf.RoundToInt(this.mVehicle.ERSController.normalizedCharge * 100f));
      }
      if ((Object) this.layout != (Object) null)
      {
        if (Game.instance.sessionManager.championship.rules.isEnergySystemActive && (double) this.layout.preferredWidth != (double) this.withBatteryInfoLayoutSize)
          this.layout.preferredWidth = this.withBatteryInfoLayoutSize;
        else if (!Game.instance.sessionManager.championship.rules.isEnergySystemActive && (double) this.layout.preferredWidth != (double) this.regularLayoutSize)
          this.layout.preferredWidth = this.regularLayoutSize;
      }
      if (inSessionType == SessionDetails.SessionType.Race)
      {
        if (this.mVehicle.standingsPosition == 1)
        {
          if ((Object) this.raceGapLabel != (Object) null)
            this.raceGapLabel.text = string.Empty;
          if ((Object) this.raceLeaderLabel != (Object) null)
            this.raceLeaderLabel.text = string.Empty;
        }
        else
        {
          if ((Object) this.raceGapLabel != (Object) null)
            this.raceGapLabel.text = GameUtility.GetGapTimeText(this.mVehicle.timer.gapToAhead, false);
          if ((Object) this.raceLeaderLabel != (Object) null)
            this.raceLeaderLabel.text = GameUtility.GetGapTimeText(this.mVehicle.timer.gapToLeader, false);
        }
      }
      else if (Mathf.Approximately(this.mVehicle.timer.GetFastestLapTime(), 0.0f) || this.mVehicle.standingsPosition == 1)
      {
        if ((Object) this.timedGapLabel != (Object) null)
          this.timedGapLabel.text = string.Empty;
        if ((Object) this.timedLeaderLabel != (Object) null)
          this.timedLeaderLabel.text = string.Empty;
      }
      else
      {
        if ((Object) this.timedGapLabel != (Object) null)
          this.timedGapLabel.text = GameUtility.GetGapTimeText(this.mVehicle.timer.gapToAhead, false);
        if ((Object) this.timedLeaderLabel != (Object) null)
          this.timedLeaderLabel.text = GameUtility.GetGapTimeText(this.mVehicle.timer.gapToLeader, false);
      }
      this.SetSectorColors();
    }
    this.Update();
  }

  public static void SetVehicleStatusLabel(RacingVehicle inVehicle, GameObject inContainer, TextMeshProUGUI inLabel)
  {
    if (inVehicle.timer.hasSeenChequeredFlag)
      GameUtility.SetActive(inContainer, false);
    else if (inVehicle.pathState.IsInPitlaneArea())
    {
      GameUtility.SetActive(inContainer, true);
      inLabel.text = Localisation.LocaliseID("PSG_10000420", (GameObject) null);
    }
    else if (inVehicle.timer.currentLap.isInLap)
    {
      GameUtility.SetActive(inContainer, true);
      inLabel.text = Localisation.LocaliseID("PSG_10010425", (GameObject) null);
    }
    else if (inVehicle.timer.currentLap.isOutLap)
    {
      GameUtility.SetActive(inContainer, true);
      inLabel.text = Localisation.LocaliseID("PSG_10010426", (GameObject) null);
    }
    else
      GameUtility.SetActive(inContainer, false);
  }

  private void SetCrashRetiredData()
  {
    this.canvasGroup.alpha = 0.6f;
    if (this.mMode == StandingsEntry.Mode.Detailed)
    {
      if ((Object) this.raceGapLabel != (Object) null)
        this.raceGapLabel.text = string.Empty;
      if ((Object) this.raceLeaderLabel != (Object) null)
        this.raceLeaderLabel.text = string.Empty;
    }
    GameUtility.SetActive(this.crashedIcon, false);
    GameUtility.SetActive(this.retiredIcon, false);
    GameUtility.SetActive(this.vehicleStatusIcon, false);
    GameUtility.SetActive(this.chequeredFlagIcon, false);
    GameUtility.SetActiveAndCheckNull(this.ERSPowerIcon, false);
  }

  private void Update()
  {
    if (this.mVehicle == null)
      return;
    this.tyreIcon.SetTyre(this.mVehicle.setup.tyreSet);
    if ((Object) this.qualifyingLineObject != (Object) null)
    {
      RaceEventDetails eventDetails = Game.instance.sessionManager.eventDetails;
      bool flag = eventDetails.currentSession.sessionType == SessionDetails.SessionType.Qualifying && eventDetails.hasSeveralQualifyingSessions;
      int positionThreshold = RaceEventResults.GetPositionThreshold(Game.instance.sessionManager.eventDetails);
      GameUtility.SetActive(this.qualifyingLineObject, flag && this.mEntryIndex == positionThreshold);
      if (flag)
      {
        if (this.mEntryIndex > positionThreshold && eventDetails.currentSession.sessionNumberForUI != 3)
          this.positionLabel.color = UIConstants.qualifyingEliminationColorForPositionLabel;
        else
          this.positionLabel.color = this.positionLabelColor;
      }
      if (this.qualifyingLineObject.activeSelf && (Object) this.qualifyingThresholdLabel != (Object) null)
        this.qualifyingThresholdLabel.text = UIMultipleQualifyingEntry.GetQualifyingThresholdText(Game.instance.sessionManager.eventDetails.currentSession.sessionNumberForUI - 1);
    }
    if (this.mShowObjective)
    {
      GameUtility.SetActive(this.objectiveLineObject, this.mEntryIndex == this.mSponsorTarget && Game.instance.sessionManager.sessionType != SessionDetails.SessionType.Practice);
      if (this.objectiveLineObject.activeSelf)
      {
        RacingVehicle placedPlayerVehicle = Game.instance.vehicleManager.GetHighestPlacedPlayerVehicle();
        if (placedPlayerVehicle == null)
          return;
        if (placedPlayerVehicle.standingsPosition <= this.mSponsorTarget)
          this.objectiveLineImage.color = UIConstants.positiveColor;
        else
          this.objectiveLineImage.color = UIConstants.sponsorGreyColor;
      }
      else
        GameUtility.SetActive(this.objectiveLineObject, false);
    }
    else
    {
      if (!((Object) this.objectiveLineObject != (Object) null))
        return;
      GameUtility.SetActive(this.objectiveLineObject, false);
    }
  }

  private void SetButtonColor(bool inIsHighlighted)
  {
    ColorBlock colors = this.button.colors;
    Color normalColor = colors.normalColor;
    Color highlightedColor = colors.highlightedColor;
    Color color;
    if (inIsHighlighted)
    {
      color = Color.white;
      color.a = 0.2f;
    }
    else
    {
      color = Color.black;
      color.a = 0.0f;
    }
    highlightedColor.a = 0.4f;
    colors.normalColor = color;
    colors.highlightedColor = highlightedColor;
    GameUtility.SetColorBlock((Selectable) this.button, colors);
  }

  private void SetSectorColors()
  {
    if (this.mVehicle == null)
      return;
    for (int inSector = 0; inSector < this.qualifyingSectorDot.Length; ++inSector)
    {
      bool inIsActive = true;
      switch (this.mVehicle.timer.GetSectorStatus(inSector))
      {
        case SessionTimer.SectorStatus.NoStatus:
          inIsActive = false;
          break;
        case SessionTimer.SectorStatus.Slower:
          this.qualifyingSectorDot[inSector].color = UIConstants.sectorSlowerColor;
          this.raceSectorDot[inSector].color = UIConstants.sectorSlowerColor;
          break;
        case SessionTimer.SectorStatus.DriverFastest:
          this.qualifyingSectorDot[inSector].color = UIConstants.sectorDriverFastestColor;
          this.raceSectorDot[inSector].color = UIConstants.sectorDriverFastestColor;
          break;
        case SessionTimer.SectorStatus.SessionFastest:
          this.qualifyingSectorDot[inSector].color = UIConstants.sectorSessionFastestColor;
          this.raceSectorDot[inSector].color = UIConstants.sectorSessionFastestColor;
          break;
      }
      GameUtility.SetActive(this.qualifyingSectorDot[inSector].gameObject, inIsActive);
      GameUtility.SetActive(this.raceSectorDot[inSector].gameObject, inIsActive);
    }
  }

  public void OnButtonPressed()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    App.instance.cameraManager.SetTarget((Vehicle) this.mVehicle, CameraManager.Transition.Smooth);
  }

  private void SetupImageTransforms()
  {
    if ((Object) this.positionUp != (Object) null)
    {
      this.mChangeUpTransform = this.positionUp.GetComponent<Transform>();
      this.mChangeUpDefaultX = this.mChangeUpTransform.localPosition.x;
    }
    if (!((Object) this.positionDown != (Object) null))
      return;
    this.mChangeDownTransform = this.positionDown.GetComponent<Transform>();
    this.mChangeDownDefaultX = this.mChangeDownTransform.localPosition.x;
  }

  private void HideImage(Transform image_transform, float default_x)
  {
    if (!((Object) image_transform != (Object) null))
      return;
    float num = default_x - 5000f;
    if ((double) Mathf.Abs(num - image_transform.localPosition.x) <= (double) Mathf.Epsilon)
      return;
    Vector3 localPosition = image_transform.localPosition;
    localPosition.x = num;
    image_transform.localPosition = localPosition;
  }

  private void ShowImage(Transform image_transform, float default_x)
  {
    if (!((Object) image_transform != (Object) null) || (double) Mathf.Abs(default_x - image_transform.localPosition.x) <= (double) Mathf.Epsilon)
      return;
    Vector3 localPosition = image_transform.localPosition;
    localPosition.x = default_x;
    image_transform.localPosition = localPosition;
  }

  public enum Mode
  {
    Simple,
    Detailed,
  }
}
