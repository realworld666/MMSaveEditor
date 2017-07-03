// Decompiled with JetBrains decompiler
// Type: UILiveTimingsRaceEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;

public class UILiveTimingsRaceEntry : UILiveTimingsSharedEntry
{
  public TextMeshProUGUI[] sectors;
  public TextMeshProUGUI lap;
  public TextMeshProUGUI leadGap;
  public TextMeshProUGUI gap;
  public TextMeshProUGUI pitStops;
  public TextMeshProUGUI bestLap;
  private RacingVehicle mVehicle;

  public override void OnStart()
  {
    base.OnStart();
  }

  public override void Setup(RacingVehicle inVehicle)
  {
    base.Setup(inVehicle);
    if (inVehicle == null)
      return;
    this.mVehicle = inVehicle;
    SessionTimer.LapData inLapData = this.mVehicle.timer.currentLap;
    if (this.mVehicle.timer.lastLap != null)
      inLapData = this.mVehicle.timer.lastLap;
    this.lap.text = !this.mVehicle.timer.HasSetLapTime() ? "-" : GameUtility.GetLapTimeText(inLapData.time, false);
    this.leadGap.text = this.mVehicle.standingsPosition == 1 ? "-" : GameUtility.GetGapTimeText(this.mVehicle.timer.gapToLeader, false);
    this.gap.text = this.mVehicle.standingsPosition == 1 ? "-" : GameUtility.GetGapTimeText(this.mVehicle.timer.gapToAhead, false);
    this.pitStops.text = this.mVehicle.timer.stops.ToString();
    this.bestLap.text = GameUtility.GetLapTimeText(this.mVehicle.timer.GetFastestLapTime(), false);
    this.SetSectors(inLapData);
  }

  public void ClearSectors()
  {
    for (int index = 0; index < this.sectors.Length; ++index)
    {
      this.sectors[index].text = "-";
      this.sectors[index].color = UIConstants.sectorNormalColor;
    }
  }

  public void SetSectors(SessionTimer.LapData inLapData)
  {
    if (inLapData != null)
    {
      for (int inIndex = 0; inIndex < this.sectors.Length; ++inIndex)
      {
        this.SetSector(inIndex, inLapData);
        this.SetSectorColor(inIndex);
      }
    }
    else
      this.ClearSectors();
  }

  public void SetSector(int inIndex, SessionTimer.LapData inLapData)
  {
    if (!MathsUtility.ApproximatelyZero(this.mVehicle.timer.currentLap.sector[inIndex]))
      this.sectors[inIndex].text = GameUtility.GetSectorTimeText(this.mVehicle.timer.currentLap.sector[inIndex]);
    else if (this.mVehicle.timer.lapData.Count >= 1)
      this.sectors[inIndex].text = GameUtility.GetSectorTimeText(this.mVehicle.timer.GetPreviousLapData().sector[inIndex]);
    else
      this.sectors[inIndex].text = "-";
  }

  public void SetSectorColor(int inIndex)
  {
    switch (this.mVehicle.timer.GetSectorStatus(inIndex))
    {
      case SessionTimer.SectorStatus.NoStatus:
        this.sectors[inIndex].color = UIConstants.sectorNormalColor;
        break;
      case SessionTimer.SectorStatus.Slower:
        this.sectors[inIndex].color = UIConstants.sectorSlowerColor;
        break;
      case SessionTimer.SectorStatus.DriverFastest:
        this.sectors[inIndex].color = UIConstants.sectorDriverFastestColor;
        break;
      case SessionTimer.SectorStatus.SessionFastest:
        this.sectors[inIndex].color = UIConstants.sectorSessionFastestColor;
        break;
    }
  }
}
