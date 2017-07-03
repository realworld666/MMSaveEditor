// Decompiled with JetBrains decompiler
// Type: UISessionSponsorObjective
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UISessionSponsorObjective : UIBaseSessionTopBarWidget
{
  public GameObject onTarget;
  public GameObject belowTarget;
  private int mSponsorTarget;
  private SponsorObjectivesWidget.TargetStatus mTargetStatus;

  private void OnEnable()
  {
    if (!Game.IsActive() || !Game.instance.isCareer || (Game.instance.vehicleManager == null || Game.instance.vehicleManager.vehicleCount <= 0))
      return;
    SponsorController sponsorController = Game.instance.player.team.sponsorController;
    if (sponsorController.weekendSponsorshipDeal != null)
    {
      switch (Game.instance.sessionManager.eventDetails.currentSession.sessionType)
      {
        case SessionDetails.SessionType.Qualifying:
          this.mSponsorTarget = sponsorController.qualifyingObjective.targetResult;
          break;
        case SessionDetails.SessionType.Race:
          this.mSponsorTarget = sponsorController.raceObjective.targetResult;
          break;
      }
    }
    else
      this.mSponsorTarget = 0;
    this.mTargetStatus = SponsorObjectivesWidget.TargetStatus.Unset;
    this.UpdateWidget();
  }

  public override bool ShouldBeEnabled()
  {
    return Game.instance.sessionManager.eventDetails.currentSession.sessionType != SessionDetails.SessionType.Practice && Game.instance.sessionManager.isSessionActive && (Game.instance.player.team.sponsorController.weekendSponsorshipDeal != null && Game.instance.isCareer);
  }

  private void Update()
  {
    this.UpdateWidget();
  }

  private void UpdateWidget()
  {
    RacingVehicle placedPlayerVehicle = Game.instance.vehicleManager.GetHighestPlacedPlayerVehicle();
    if (placedPlayerVehicle == null)
      return;
    bool flag = placedPlayerVehicle.standingsPosition <= this.mSponsorTarget;
    if (this.mTargetStatus != SponsorObjectivesWidget.TargetStatus.Unset && (this.mTargetStatus != SponsorObjectivesWidget.TargetStatus.Succeeding || flag) && (this.mTargetStatus != SponsorObjectivesWidget.TargetStatus.Failing || !flag))
      return;
    this.mTargetStatus = !flag ? SponsorObjectivesWidget.TargetStatus.Failing : SponsorObjectivesWidget.TargetStatus.Succeeding;
    if (this.mTargetStatus == SponsorObjectivesWidget.TargetStatus.Succeeding)
    {
      this.onTarget.SetActive(true);
      this.belowTarget.SetActive(false);
    }
    else
    {
      this.onTarget.SetActive(false);
      this.belowTarget.SetActive(true);
    }
  }
}
