// Decompiled with JetBrains decompiler
// Type: FastestLapWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FastestLapWidget : MonoBehaviour
{
  public TextMeshProUGUI driverName;
  public UICharacterPortrait driverPortrait;
  public TextMeshProUGUI lapTime;
  public Image teamColorImage;
  public UICarSetupTyreIcon tyre;
  public TextMeshProUGUI position;
  public Image[] sectorDots;
  private RacingVehicle mFastestLapVehicle;
  private Driver mDriver;
  private float mDisplayTimer;

  public void OnEnter()
  {
    Game.instance.sessionManager.OnFastestLapChanged += new Action(this.OnFastestLapChanged);
    this.gameObject.SetActive(false);
  }

  public void OnExit()
  {
    if (!Game.IsActive() || Game.instance.sessionManager == null)
      return;
    Game.instance.sessionManager.OnFastestLapChanged -= new Action(this.OnFastestLapChanged);
  }

  public void OnFastestLapChanged()
  {
    this.gameObject.SetActive(true);
    this.mFastestLapVehicle = Game.instance.sessionManager.GetVehicleWithFastestLap();
    this.mDriver = this.mFastestLapVehicle.driver;
    this.driverName.text = this.mDriver.shortName;
    this.driverPortrait.SetPortrait((Person) this.mDriver);
    this.lapTime.text = GameUtility.GetLapTimeText(this.mFastestLapVehicle.timer.GetFastestLapTime(), false);
    this.teamColorImage.color = this.mDriver.GetTeamColor().primaryUIColour.normal;
    this.position.text = this.mFastestLapVehicle.standingsPosition.ToString();
    this.tyre.SetTyre(this.mFastestLapVehicle.setup.tyreSet);
    this.SetSectorData(this.mFastestLapVehicle);
    this.mDisplayTimer = 0.0f;
  }

  private void SetSectorData(RacingVehicle inVehicle)
  {
    int num = 1;
    for (int inSector = 0; inSector < SessionTimer.sectorCount; ++inSector)
    {
      SessionTimer.SectorStatus sectorStatus = inVehicle.timer.GetSectorStatus(inSector);
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

  private void Update()
  {
    this.mDisplayTimer += GameTimer.deltaTime;
    if (this.mFastestLapVehicle != null)
      this.position.text = this.mFastestLapVehicle.standingsPosition.ToString();
    if ((double) this.mDisplayTimer < 4.5)
      return;
    this.gameObject.SetActive(false);
  }
}
