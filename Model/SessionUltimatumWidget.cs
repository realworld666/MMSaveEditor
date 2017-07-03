// Decompiled with JetBrains decompiler
// Type: SessionUltimatumWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class SessionUltimatumWidget : UIBaseSessionHudDropdown
{
  public TextMeshProUGUI targetPositionLabel;
  public TextMeshProUGUI currentPositionLabel;
  public TextMeshProUGUI rewardLabel;
  public GameObject onTarget;
  public GameObject belowTarget;
  public UICharacterPortrait driverPortrait;
  public TextMeshProUGUI driverNameLabel;
  private int mChairmanTarget;
  private SessionUltimatumWidget.TargetStatus mTargetStatus;

  protected override void OnEnable()
  {
    base.OnEnable();
    if (!Game.IsActive() || !Game.instance.isCareer || Game.instance.vehicleManager.vehicleCount <= 0)
      return;
    this.mChairmanTarget = Game.instance.player.team.chairman.ultimatum.positionExpected;
    this.mTargetStatus = SessionUltimatumWidget.TargetStatus.Unset;
    this.UpdateWidget();
  }

  protected override void Update()
  {
    this.UpdateWidget();
    base.Update();
  }

  private void UpdateWidget()
  {
    if (!Game.IsActive() || !Game.instance.isCareer || this.mChairmanTarget == 0)
      return;
    RacingVehicle placedPlayerVehicle = Game.instance.vehicleManager.GetHighestPlacedPlayerVehicle();
    bool flag = placedPlayerVehicle.standingsPosition <= this.mChairmanTarget;
    if (this.mTargetStatus != SessionUltimatumWidget.TargetStatus.Unset && (this.mTargetStatus != SessionUltimatumWidget.TargetStatus.Succeeding || flag) && (this.mTargetStatus != SessionUltimatumWidget.TargetStatus.Failing || !flag))
      return;
    this.mTargetStatus = !flag ? SessionUltimatumWidget.TargetStatus.Failing : SessionUltimatumWidget.TargetStatus.Succeeding;
    Driver driver = placedPlayerVehicle.driver;
    this.driverPortrait.SetPortrait((Person) driver);
    this.driverNameLabel.text = driver.shortName;
    this.currentPositionLabel.text = GameUtility.FormatForPosition(placedPlayerVehicle.standingsPosition, (string) null);
    this.targetPositionLabel.text = GameUtility.FormatForPosition(this.mChairmanTarget, (string) null);
    this.rewardLabel.text = "Finish " + GameUtility.FormatForPosition(this.mChairmanTarget, (string) null) + " or better to keep job.";
    if (this.mTargetStatus == SessionUltimatumWidget.TargetStatus.Succeeding)
      this.currentPositionLabel.color = UIConstants.positiveColor;
    else
      this.currentPositionLabel.color = UIConstants.negativeColor;
  }

  public enum TargetStatus
  {
    Unset,
    Succeeding,
    Failing,
  }
}
