// Decompiled with JetBrains decompiler
// Type: DriverTimerHUD
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DriverTimerHUD : MonoBehaviour
{
  public Image[] sectorDots;
  public TextMeshProUGUI timedLapLabel;
  public GameObject leaderDelta;
  public Image leaderDeltaBackingImage;
  public TextMeshProUGUI leaderDeltaPositionLabel;
  public TextMeshProUGUI leaderDeltaTimeLabel;
  public GameObject objectiveDelta;
  public Image objectiveDeltaBackingImage;
  public TextMeshProUGUI objectiveDeltaPositionLabel;
  public TextMeshProUGUI objectiveDeltaTimeLabel;
  public GameObject pace;
  public TextMeshProUGUI paceTimeLabel;
  private RacingVehicle mVehicle;
  private SessionDetails.SessionType mSessionType;
  private float mObjectivePositionGapUpdateTimer;
  private int mSponsorTarget;
  private float mPace;

  public RacingVehicle vehicle
  {
    get
    {
      return this.mVehicle;
    }
  }

  private void Awake()
  {
  }

  private void OnEnable()
  {
    this.mSessionType = Game.instance.sessionManager.eventDetails.currentSession.sessionType;
    SponsorController sponsorController = Game.instance.player.team.sponsorController;
    SponsorshipDeal weekendSponsorshipDeal = sponsorController.weekendSponsorshipDeal;
    this.mSponsorTarget = 1;
    if (weekendSponsorshipDeal != null)
    {
      switch (this.mSessionType)
      {
        case SessionDetails.SessionType.Qualifying:
          if (sponsorController.qualifyingObjective != null)
          {
            this.mSponsorTarget = sponsorController.qualifyingObjective.targetResult;
            break;
          }
          break;
        case SessionDetails.SessionType.Race:
          if (sponsorController.raceObjective != null)
          {
            this.mSponsorTarget = sponsorController.raceObjective.targetResult;
            break;
          }
          break;
      }
    }
    if ((Object) this.timedLapLabel != (Object) null)
      this.timedLapLabel.gameObject.SetActive(false);
    switch (this.mSessionType)
    {
      case SessionDetails.SessionType.Practice:
      case SessionDetails.SessionType.Qualifying:
        this.leaderDelta.SetActive(true);
        this.pace.SetActive(false);
        if ((Object) this.objectiveDelta != (Object) null)
        {
          this.objectiveDelta.SetActive(this.mSponsorTarget != 0);
          break;
        }
        break;
      case SessionDetails.SessionType.Race:
        this.leaderDelta.SetActive(false);
        this.pace.SetActive(true);
        if ((Object) this.objectiveDelta != (Object) null)
        {
          this.objectiveDelta.SetActive(this.mSponsorTarget != 0);
          break;
        }
        break;
    }
    this.mObjectivePositionGapUpdateTimer = 0.0f;
    this.leaderDeltaPositionLabel.text = GameUtility.FormatForPosition(1, (string) null);
  }

  private void Update()
  {
    if (this.mVehicle == null)
    {
      this.Hide();
    }
    else
    {
      this.UpdateSectorData();
      switch (this.mSessionType)
      {
        case SessionDetails.SessionType.Practice:
        case SessionDetails.SessionType.Qualifying:
          bool flag = (Object) this.timedLapLabel != (Object) null && !this.vehicle.timer.currentLap.isOutLap && !this.vehicle.timer.currentLap.isInLap;
          if ((Object) this.timedLapLabel != (Object) null)
          {
            this.timedLapLabel.gameObject.SetActive(flag);
            if (flag)
              this.timedLapLabel.text = GameUtility.GetLapTimeText(this.vehicle.timer.currentLap.time, true);
          }
          if ((Object) this.objectiveDelta != (Object) null)
            this.objectiveDelta.SetActive(!flag && this.mSponsorTarget != 0);
          this.UpdateLeaderDelta();
          this.UpdateObjectiveDelta();
          break;
        case SessionDetails.SessionType.Race:
          this.UpdatePace();
          this.UpdateGapToObjectivePosition();
          break;
      }
    }
  }

  private void UpdateSectorData()
  {
    int num = 1;
    for (int inSector = 0; inSector < SessionTimer.sectorCount; ++inSector)
    {
      SessionTimer.SectorStatus sectorStatus = this.mVehicle.timer.GetSectorStatus(inSector);
      if (sectorStatus != SessionTimer.SectorStatus.NoStatus)
      {
        num = Mathf.Min(num + 1, SessionTimer.sectorCount);
        switch (sectorStatus - 1)
        {
          case SessionTimer.SectorStatus.NoStatus:
            this.sectorDots[inSector].color = UIConstants.sectorSlowerColor;
            break;
          case SessionTimer.SectorStatus.Slower:
            this.sectorDots[inSector].color = UIConstants.sectorDriverFastestColor;
            break;
          case SessionTimer.SectorStatus.DriverFastest:
            this.sectorDots[inSector].color = UIConstants.sectorSessionFastestColor;
            break;
        }
      }
      this.sectorDots[inSector].gameObject.SetActive(sectorStatus != SessionTimer.SectorStatus.NoStatus);
    }
  }

  private void UpdateLeaderDelta()
  {
    float sectorDeltaToPosition = this.mVehicle.timer.GetSectorDeltaToPosition(1);
    DriverTimerHUD.SetLabelTime(sectorDeltaToPosition, this.leaderDeltaTimeLabel);
    if ((double) sectorDeltaToPosition < 0.0 || MathsUtility.ApproximatelyZero(sectorDeltaToPosition))
    {
      this.leaderDeltaBackingImage.color = UIConstants.positiveColor;
      this.leaderDeltaTimeLabel.color = UIConstants.positiveColor;
    }
    else
    {
      this.leaderDeltaBackingImage.color = UIConstants.negativeColor;
      this.leaderDeltaTimeLabel.color = UIConstants.negativeColor;
    }
  }

  private void UpdateObjectiveDelta()
  {
    if (this.mSponsorTarget == 0 || !((Object) this.objectiveDelta != (Object) null))
      return;
    float sectorDeltaToPosition = this.mVehicle.timer.GetSectorDeltaToPosition(this.mSponsorTarget);
    this.objectiveDeltaPositionLabel.text = GameUtility.FormatForPosition(this.mSponsorTarget, (string) null);
    DriverTimerHUD.SetLabelTime(sectorDeltaToPosition, this.objectiveDeltaPositionLabel);
    if ((double) sectorDeltaToPosition < 0.0 || MathsUtility.ApproximatelyZero(sectorDeltaToPosition))
    {
      this.objectiveDeltaBackingImage.color = UIConstants.positiveColor;
      this.objectiveDeltaTimeLabel.color = UIConstants.positiveColor;
    }
    else
    {
      this.objectiveDeltaBackingImage.color = UIConstants.negativeColor;
      this.objectiveDeltaTimeLabel.color = UIConstants.negativeColor;
    }
  }

  private void UpdatePace()
  {
    float pace = this.mVehicle.timer.pace;
    if ((double) Mathf.Abs(pace - this.mPace) > 1.0 / 1000.0)
    {
      this.mPace = pace;
      DriverTimerHUD.SetLabelTime(pace, this.paceTimeLabel);
    }
    if ((double) pace < 0.0)
    {
      if (!(this.paceTimeLabel.color != UIConstants.positiveColor))
        return;
      this.paceTimeLabel.color = UIConstants.positiveColor;
    }
    else
    {
      if (!(this.paceTimeLabel.color != UIConstants.negativeColor))
        return;
      this.paceTimeLabel.color = UIConstants.negativeColor;
    }
  }

  private void UpdateGapToObjectivePosition()
  {
    if (this.mSponsorTarget == 0 || !((Object) this.objectiveDelta != (Object) null))
      return;
    this.mObjectivePositionGapUpdateTimer -= GameTimer.deltaTime;
    if ((double) this.mObjectivePositionGapUpdateTimer >= 0.0)
      return;
    this.mObjectivePositionGapUpdateTimer = 3f;
    float gapToPosition = this.mVehicle.timer.GetGapToPosition(this.mSponsorTarget);
    this.objectiveDeltaPositionLabel.text = GameUtility.FormatForPosition(this.mSponsorTarget, (string) null);
    DriverTimerHUD.SetLabelTime(gapToPosition, this.objectiveDeltaTimeLabel);
    if ((double) gapToPosition < 0.0 || MathsUtility.ApproximatelyZero(gapToPosition))
    {
      this.objectiveDeltaBackingImage.color = UIConstants.positiveColor;
      this.objectiveDeltaTimeLabel.color = UIConstants.positiveColor;
    }
    else
    {
      this.objectiveDeltaBackingImage.color = UIConstants.negativeColor;
      this.objectiveDeltaTimeLabel.color = UIConstants.negativeColor;
    }
  }

  public void Show()
  {
    if (this.gameObject.activeSelf)
      return;
    this.gameObject.SetActive(true);
  }

  public void Hide()
  {
    if (!this.gameObject.activeSelf)
      return;
    this.gameObject.SetActive(false);
  }

  public void SetVehicle(RacingVehicle inVehicle)
  {
    this.mVehicle = inVehicle;
    this.mPace = float.MaxValue;
    this.Update();
  }

  private static void SetLabelTime(float inSeconds, TextMeshProUGUI inLabel)
  {
    using (GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe())
    {
      StringBuilder stringBuilder = builderSafe.stringBuilder;
      float num1 = Mathf.Abs(inSeconds);
      int num2 = (int) num1;
      int num3 = (int) ((double) (num1 - (float) num2) * 1000.0);
      if (Mathf.Approximately(inSeconds, 0.0f))
      {
        stringBuilder.Append("+");
        stringBuilder.Append(Localisation.GetLapTimeFormatting(true));
      }
      else
      {
        if ((double) inSeconds >= 0.0)
          stringBuilder.Append("+");
        else
          stringBuilder.Append("-");
        GameUtility.Format(stringBuilder, Localisation.GetLapTimeFormatting(true), num2, num3);
      }
      inLabel.text = stringBuilder.ToString();
    }
  }
}
