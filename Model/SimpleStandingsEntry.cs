// Decompiled with JetBrains decompiler
// Type: SimpleStandingsEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SimpleStandingsEntry : MonoBehaviour
{
  public Color positionLabelColor = new Color();
  public Image[] sectorDots = new Image[0];
  public Color currentLapInfoLabelColor = new Color();
  public float preferedWidthRace = 380f;
  public float preferedWidthDefault = 320f;
  private int mPosition = -1;
  private int mCachedPitStops = -1;
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
  public GameObject sectorsContainer;
  public TextMeshProUGUI vehicleStatusLabel;
  public GameObject chequeredFlagIcon;
  public GameObject ERSPowerIcon;
  public GameObject currentLapInfoContainer;
  public TextMeshProUGUI currentLapInfoLabel;
  public GameObject pitStopsContainer;
  public TextMeshProUGUI pitStopsLabel;
  public GameObject gapToLeaderContainer;
  public TextMeshProUGUI gapToLeaderLabel;
  public LayoutElement contentLayout;
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

  public void SetVehicle(int inEntryIndex, RacingVehicle inVehicle, bool inIsHighlighted, SessionDetails.SessionType inSessionType)
  {
    this.mVehicle = inVehicle;
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
      if (this.positionLabel.color != this.positionLabelColor)
        this.positionLabel.color = this.positionLabelColor;
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
      GameUtility.SetActiveAndCheckNull(this.ERSPowerIcon, this.mVehicle.ERSController.mode == ERSController.Mode.Power);
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
    this.Update();
  }

  private void SetCrashRetiredData()
  {
    this.canvasGroup.alpha = 0.6f;
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
        if (placedPlayerVehicle != null)
        {
          if (placedPlayerVehicle.standingsPosition <= this.mSponsorTarget)
            this.objectiveLineImage.color = UIConstants.positiveColor;
          else
            this.objectiveLineImage.color = UIConstants.sponsorGreyColor;
        }
      }
      else
        GameUtility.SetActive(this.objectiveLineObject, false);
    }
    else if ((Object) this.objectiveLineObject != (Object) null)
      GameUtility.SetActive(this.objectiveLineObject, false);
    this.UpdateSessionData();
  }

  private void UpdateSessionData()
  {
    switch (Game.instance.sessionManager.eventDetails.currentSession.sessionType)
    {
      case SessionDetails.SessionType.Practice:
        GameUtility.SetActiveAndCheckNull(this.pitStopsContainer, false);
        GameUtility.SetActiveAndCheckNull(this.gapToLeaderContainer, false);
        GameUtility.SetActiveAndCheckNull(this.currentLapInfoContainer, true);
        if ((double) this.contentLayout.preferredWidth != (double) this.preferedWidthDefault)
          this.contentLayout.preferredWidth = this.preferedWidthDefault;
        SessionTimer.LapData currentLap1 = this.mVehicle.timer.currentLap;
        SessionTimer.LapData previousLapData = this.mVehicle.timer.GetPreviousLapData();
        bool flag1 = previousLapData != null && !this.mVehicle.timer.wasLastLapAnOutLap;
        bool flag2 = this.mVehicle.pathController.currentPathType == PathController.PathType.Track;
        bool inIsActive1 = !currentLap1.isInLap && !currentLap1.isOutLap && flag2;
        bool flag3 = Game.instance.sessionManager.fastestLap == this.mVehicle.timer.fastestLap && this.mVehicle.timer.fastestLap != null;
        float num1 = 10f;
        bool flag4 = (double) this.mVehicle.timer.currentLap.time < (double) num1 && flag1;
        if (flag2 && flag4 && (flag3 || this.mVehicle.timer.fastestLap == this.mVehicle.timer.GetPreviousLapData()))
        {
          if (flag3)
            this.currentLapInfoLabel.color = Color.Lerp(UIConstants.sectorSessionFastestColor, this.currentLapInfoLabelColor, (float) (((double) this.mVehicle.timer.currentLap.time - (double) num1 / 2.0) / ((double) num1 / 2.0)));
          else
            this.currentLapInfoLabel.color = Color.Lerp(UIConstants.sectorDriverFastestColor, this.currentLapInfoLabelColor, (float) (((double) this.mVehicle.timer.currentLap.time - (double) num1 / 2.0) / ((double) num1 / 2.0)));
        }
        else
          this.currentLapInfoLabel.color = this.currentLapInfoLabelColor;
        GameUtility.SetActiveAndCheckNull(this.sectorsContainer, inIsActive1);
        if (inIsActive1 && !flag4)
        {
          if (flag3)
          {
            this.currentLapInfoLabel.text = GameUtility.GetLapTimeText(this.mVehicle.timer.fastestLap.time, false);
            break;
          }
          this.SetGapText();
          break;
        }
        if ((inIsActive1 || flag1) && flag4)
        {
          this.currentLapInfoLabel.text = GameUtility.GetLapTimeText(previousLapData.time, false);
          break;
        }
        if (flag3)
        {
          this.currentLapInfoLabel.text = GameUtility.GetLapTimeText(this.mVehicle.timer.fastestLap.time, false);
          break;
        }
        this.SetGapText();
        break;
      case SessionDetails.SessionType.Qualifying:
        GameUtility.SetActiveAndCheckNull(this.pitStopsContainer, false);
        GameUtility.SetActiveAndCheckNull(this.gapToLeaderContainer, false);
        GameUtility.SetActiveAndCheckNull(this.currentLapInfoContainer, true);
        if ((double) this.contentLayout.preferredWidth != (double) this.preferedWidthDefault)
          this.contentLayout.preferredWidth = this.preferedWidthDefault;
        SessionTimer.LapData currentLap2 = this.mVehicle.timer.currentLap;
        bool flag5 = this.mVehicle.pathController.currentPathType == PathController.PathType.Track;
        bool inIsActive2 = !currentLap2.isInLap && !currentLap2.isOutLap && flag5;
        bool flag6 = Game.instance.sessionManager.fastestLap == this.mVehicle.timer.fastestLap && this.mVehicle.timer.fastestLap != null;
        if (this.mVehicle.timer.fastestLap != null && !inIsActive2)
        {
          float num2 = 10f;
          bool flag7 = (double) this.mVehicle.timer.currentLap.time < (double) num2;
          if (flag5 && flag7 && flag6)
            this.currentLapInfoLabel.color = Color.Lerp(UIConstants.sectorSessionFastestColor, this.currentLapInfoLabelColor, (float) (((double) this.mVehicle.timer.currentLap.time - (double) num2 / 2.0) / ((double) num2 / 2.0)));
          else if (flag5 && flag7 && this.mVehicle.timer.fastestLap == this.mVehicle.timer.GetPreviousLapData())
            this.currentLapInfoLabel.color = Color.Lerp(UIConstants.sectorDriverFastestColor, this.currentLapInfoLabelColor, (float) (((double) this.mVehicle.timer.currentLap.time - (double) num2 / 2.0) / ((double) num2 / 2.0)));
          else
            this.currentLapInfoLabel.color = this.currentLapInfoLabelColor;
        }
        else
          this.currentLapInfoLabel.color = this.currentLapInfoLabelColor;
        GameUtility.SetActiveAndCheckNull(this.sectorsContainer, inIsActive2);
        if (inIsActive2)
        {
          this.SetGapText();
          break;
        }
        if (flag6)
        {
          this.currentLapInfoLabel.text = GameUtility.GetLapTimeText(this.mVehicle.timer.fastestLap.time, false);
          break;
        }
        this.SetGapText();
        break;
      case SessionDetails.SessionType.Race:
        if ((Object) this.contentLayout != (Object) null && (double) this.contentLayout.preferredWidth != (double) this.preferedWidthRace)
          this.contentLayout.preferredWidth = this.preferedWidthRace;
        GameUtility.SetActiveAndCheckNull(this.sectorsContainer, false);
        GameUtility.SetActiveAndCheckNull(this.currentLapInfoContainer, false);
        GameUtility.SetActiveAndCheckNull(this.pitStopsContainer, true);
        GameUtility.SetActiveAndCheckNull(this.gapToLeaderContainer, true);
        if (this.mCachedPitStops != this.mVehicle.timer.pitstopData.Count)
        {
          this.mCachedPitStops = this.mVehicle.timer.pitstopData.Count;
          this.pitStopsLabel.text = this.mCachedPitStops.ToString();
        }
        if ((double) this.mVehicle.timer.gapToLeader != 0.0)
        {
          this.gapToLeaderLabel.text = GameUtility.GetGapTimeText(this.mVehicle.timer.gapToLeader, true);
          break;
        }
        this.gapToLeaderLabel.text = "-";
        break;
    }
    if (!this.sectorsContainer.activeSelf)
      return;
    this.SetSectorColors();
  }

  private void SetGapText()
  {
    if ((double) this.mVehicle.timer.gapToLeader != 0.0)
      this.currentLapInfoLabel.text = GameUtility.GetGapTimeText(this.mVehicle.timer.gapToLeader, false);
    else
      this.currentLapInfoLabel.text = string.Empty;
  }

  private void SetSectorColors()
  {
    if (this.mVehicle == null)
      return;
    for (int inSector = 0; inSector < this.sectorDots.Length; ++inSector)
    {
      bool inIsActive = true;
      switch (this.mVehicle.timer.GetSectorStatus(inSector))
      {
        case SessionTimer.SectorStatus.NoStatus:
          inIsActive = false;
          break;
        case SessionTimer.SectorStatus.Slower:
          this.sectorDots[inSector].color = UIConstants.sectorSlowerColor;
          break;
        case SessionTimer.SectorStatus.DriverFastest:
          this.sectorDots[inSector].color = UIConstants.sectorDriverFastestColor;
          break;
        case SessionTimer.SectorStatus.SessionFastest:
          this.sectorDots[inSector].color = UIConstants.sectorSessionFastestColor;
          break;
      }
      GameUtility.SetActive(this.sectorDots[inSector].gameObject, inIsActive);
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
}
