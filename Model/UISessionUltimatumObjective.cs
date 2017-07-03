// Decompiled with JetBrains decompiler
// Type: UISessionUltimatumObjective
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UISessionUltimatumObjective : UIBaseSessionTopBarWidget
{
  public GameObject onTarget;
  public GameObject belowTarget;
  private int mChairmanTarget;
  private SponsorObjectivesWidget.TargetStatus mTargetStatus;

  private void OnEnable()
  {
    if (!Game.IsActive() || !Game.instance.isCareer || Game.instance.vehicleManager.vehicleCount <= 0)
      return;
    this.mChairmanTarget = Game.instance.player.team.chairman.ultimatum.positionExpected;
    this.mTargetStatus = SponsorObjectivesWidget.TargetStatus.Unset;
    this.UpdateWidget();
  }

  public override bool ShouldBeEnabled()
  {
    return Game.instance.sessionManager.eventDetails.currentSession.sessionType == SessionDetails.SessionType.Race && Game.instance.isCareer && Game.instance.player.team.chairman.hasMadeUltimatum;
  }

  private void Update()
  {
    this.UpdateWidget();
  }

  private void UpdateWidget()
  {
    bool flag = Game.instance.vehicleManager.GetHighestPlacedPlayerVehicle().standingsPosition <= this.mChairmanTarget;
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
