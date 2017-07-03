// Decompiled with JetBrains decompiler
// Type: SponsorObjectivesWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SponsorObjectivesWidget : UIBaseSessionHudDropdown
{
  public TextMeshProUGUI targetPositionLabel;
  public TextMeshProUGUI targetTextLabel;
  public TextMeshProUGUI rewardLabel;
  public Image rewardBacking;
  private int mSponsorTarget;
  private SponsorObjectivesWidget.TargetStatus mTargetStatus;

  protected override void OnEnable()
  {
    base.OnEnable();
    if (!Game.IsActive() || !Game.instance.isCareer || Game.instance.vehicleManager.vehicleCount <= 0)
      return;
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
    this.mTargetStatus = SponsorObjectivesWidget.TargetStatus.Unset;
    this.UpdateWidget();
  }

  protected override void Update()
  {
    this.UpdateWidget();
    base.Update();
  }

  private void UpdateWidget()
  {
    if (!Game.IsActive() || !Game.instance.isCareer || this.mSponsorTarget == 0)
      return;
    bool flag = Game.instance.vehicleManager.GetHighestPlacedPlayerVehicle().standingsPosition <= this.mSponsorTarget;
    if (this.mTargetStatus != SponsorObjectivesWidget.TargetStatus.Unset && (this.mTargetStatus != SponsorObjectivesWidget.TargetStatus.Succeeding || flag) && (this.mTargetStatus != SponsorObjectivesWidget.TargetStatus.Failing || !flag))
      return;
    this.mTargetStatus = !flag ? SponsorObjectivesWidget.TargetStatus.Failing : SponsorObjectivesWidget.TargetStatus.Succeeding;
    if (this.mTargetStatus == SponsorObjectivesWidget.TargetStatus.Succeeding)
    {
      this.targetPositionLabel.color = UIConstants.positiveColor;
      this.rewardBacking.color = UIConstants.positiveColor;
    }
    else
    {
      this.targetPositionLabel.color = UIConstants.negativeColor;
      this.rewardBacking.color = UIConstants.negativeColor;
    }
  }

  public enum TargetStatus
  {
    Unset,
    Succeeding,
    Failing,
  }
}
