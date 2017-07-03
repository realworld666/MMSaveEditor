// Decompiled with JetBrains decompiler
// Type: SponsorStatusChangeWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SponsorStatusChangeWidget : MonoBehaviour
{
  public TextMeshProUGUI targetPositionLabel;
  public TextMeshProUGUI targetTextLabel;
  public TextMeshProUGUI rewardLabel;
  public Image rewardBacking;
  public GameObject onTarget;
  public GameObject belowTarget;
  private int mSponsorTarget;
  private SponsorStatusChangeWidget.TargetStatus mTargetStatus;
  private float mDisplayTimer;

  private void Awake()
  {
  }

  public void OnSessionStart()
  {
    this.mTargetStatus = SponsorStatusChangeWidget.TargetStatus.Unset;
    SponsorController sponsorController = Game.instance.player.team.sponsorController;
    SponsorshipDeal weekendSponsorshipDeal = sponsorController.weekendSponsorshipDeal;
    int num = 0;
    if (weekendSponsorshipDeal != null)
    {
      switch (Game.instance.sessionManager.eventDetails.currentSession.sessionType)
      {
        case SessionDetails.SessionType.Qualifying:
          this.mSponsorTarget = sponsorController.qualifyingObjective.targetResult;
          num = sponsorController.qualifyingObjective.financialReward;
          break;
        case SessionDetails.SessionType.Race:
          this.mSponsorTarget = sponsorController.raceObjective.targetResult;
          num = sponsorController.raceObjective.financialReward;
          break;
      }
    }
    else
      this.mSponsorTarget = 0;
    this.rewardLabel.text = GameUtility.GetCurrencyString((long) num, 0);
    this.targetPositionLabel.text = GameUtility.FormatForPosition(this.mSponsorTarget, (string) null);
    this.targetTextLabel.text = Localisation.LocaliseID("PSG_10010399", (GameObject) null);
    GameUtility.SetActive(this.targetTextLabel.gameObject, this.mSponsorTarget != 1);
    GameUtility.SetActive(this.onTarget, false);
    GameUtility.SetActive(this.belowTarget, false);
  }

  public void OnEnter()
  {
    this.gameObject.SetActive(false);
  }

  public void OnExit()
  {
  }

  private void Update()
  {
    this.mDisplayTimer += GameTimer.deltaTime;
    if ((double) this.mDisplayTimer < 8.0)
      return;
    this.gameObject.SetActive(false);
  }

  public void Show()
  {
    this.mDisplayTimer = 0.0f;
    GameUtility.SetActive(this.gameObject, true);
  }

  public void CheckForStatusChange()
  {
    if (this.mSponsorTarget == 0)
      return;
    RacingVehicle placedPlayerVehicle = Game.instance.vehicleManager.GetHighestPlacedPlayerVehicle();
    bool flag = placedPlayerVehicle.standingsPosition <= this.mSponsorTarget;
    if (this.mTargetStatus != SponsorStatusChangeWidget.TargetStatus.Unset && (this.mTargetStatus != SponsorStatusChangeWidget.TargetStatus.Succeeding || flag) && (this.mTargetStatus != SponsorStatusChangeWidget.TargetStatus.Failing || !flag))
      return;
    this.mTargetStatus = !flag ? SponsorStatusChangeWidget.TargetStatus.Failing : SponsorStatusChangeWidget.TargetStatus.Succeeding;
    if (this.mTargetStatus == SponsorStatusChangeWidget.TargetStatus.Succeeding)
    {
      this.targetPositionLabel.color = UIConstants.positiveColor;
      this.rewardBacking.color = UIConstants.positiveColor;
      GameUtility.SetActive(this.onTarget, true);
      GameUtility.SetActive(this.belowTarget, false);
    }
    else
    {
      this.targetPositionLabel.color = UIConstants.negativeColor;
      this.rewardBacking.color = UIConstants.negativeColor;
      GameUtility.SetActive(this.onTarget, false);
      GameUtility.SetActive(this.belowTarget, true);
    }
    SessionDetails.SessionType sessionType = Game.instance.sessionManager.sessionType;
    if ((sessionType != SessionDetails.SessionType.Race || Game.instance.sessionManager.lap <= 1) && (sessionType != SessionDetails.SessionType.Qualifying || !placedPlayerVehicle.timer.HasSetLapTime()))
      return;
    this.Show();
  }

  public enum TargetStatus
  {
    Unset,
    Succeeding,
    Failing,
  }
}
