// Decompiled with JetBrains decompiler
// Type: UIPitConditionWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UIPitConditionWidget : UIBaseTimerWidget
{
  public UIPartConditionEntry[] singleSeaterPartEntries = new UIPartConditionEntry[0];
  public UIPartConditionEntry[] gtCarPartEntries = new UIPartConditionEntry[0];
  public GameObject gtCarContainer;
  public GameObject singleSeaterContainer;
  private RacingVehicle mVehicle;

  public void SetVehicle(RacingVehicle inVehicle)
  {
    this.mVehicle = inVehicle;
    this.SetGraphic();
    if (Game.instance.sessionManager.championship.series == Championship.Series.GTSeries)
      this.SetParts(ref this.gtCarPartEntries);
    else
      this.SetParts(ref this.singleSeaterPartEntries);
    this.UpdateTimeEstimate();
  }

  public void UpdateTimeEstimate()
  {
    this.RefreshTime();
    if (Game.instance.sessionManager.championship.series == Championship.Series.GTSeries)
      this.NotifyPitScreenPartsRepairing(ref this.gtCarPartEntries);
    else
      this.NotifyPitScreenPartsRepairing(ref this.singleSeaterPartEntries);
  }

  private void SetGraphic()
  {
    GameUtility.SetActive(this.singleSeaterContainer, Game.instance.sessionManager.championship.series == Championship.Series.SingleSeaterSeries);
    GameUtility.SetActive(this.gtCarContainer, Game.instance.sessionManager.championship.series == Championship.Series.GTSeries);
  }

  private void SetParts(ref UIPartConditionEntry[] entries)
  {
    Car car = this.mVehicle.car;
    CarPart.PartType[] partType = CarPart.GetPartType(Game.instance.sessionManager.championship.series, false);
    for (int index = 0; index < entries.Length; ++index)
      entries[index].SetPart(car.GetPart(partType[index]), this.mVehicle);
    this.mVehicle.setup.SetRepair();
  }

  private void NotifyPitScreenPartsRepairing(ref UIPartConditionEntry[] entries)
  {
    for (int index = 0; index < entries.Length; ++index)
    {
      if (entries[index].fixPartToggle.isOn)
      {
        UIManager.instance.GetScreen<PitScreen>().OnPartRepaired(true);
        return;
      }
    }
    UIManager.instance.GetScreen<PitScreen>().OnPartRepaired(false);
  }

  public override void RefreshTime()
  {
    if (this.mVehicle == null)
      return;
    this.SetTimeEstimate(this.mVehicle.setup.GetConditionReplairTimeImpact(), this.mVehicle.setup.IsOnTheCriticalPath(SessionSetup.PitCrewSizeDependentSteps.Condition));
  }
}
