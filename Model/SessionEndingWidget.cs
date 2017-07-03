// Decompiled with JetBrains decompiler
// Type: SessionEndingWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class SessionEndingWidget : FlagWidget
{
  public GameObject practiceHeader;
  public GameObject qualifyingHeader;
  public GameObject raceHeader;
  public TextMeshProUGUI infoLabel;
  private VehicleManager mVehicleManager;
  private float mUpdateTimer;

  public override void FlagChange()
  {
    base.FlagChange();
  }

  public override void Show()
  {
    base.Show();
    this.mUpdateTimer = 0.0f;
    this.mVehicleManager = Game.instance.vehicleManager;
    switch (Game.instance.sessionManager.eventDetails.currentSession.sessionType)
    {
      case SessionDetails.SessionType.Practice:
        GameUtility.SetActive(this.practiceHeader, true);
        GameUtility.SetActive(this.qualifyingHeader, false);
        GameUtility.SetActive(this.raceHeader, false);
        break;
      case SessionDetails.SessionType.Qualifying:
        GameUtility.SetActive(this.practiceHeader, false);
        GameUtility.SetActive(this.qualifyingHeader, true);
        GameUtility.SetActive(this.raceHeader, false);
        break;
      case SessionDetails.SessionType.Race:
        GameUtility.SetActive(this.practiceHeader, false);
        GameUtility.SetActive(this.qualifyingHeader, false);
        GameUtility.SetActive(this.raceHeader, true);
        break;
    }
    this.Update();
  }

  public override void Hide()
  {
    base.Hide();
  }

  public override void Update()
  {
    base.Update();
    this.mUpdateTimer -= GameTimer.deltaTime;
    if ((double) this.mUpdateTimer >= 0.0 || this.mVehicleManager.vehicleCount <= 0)
      return;
    this.mUpdateTimer = 2f;
    int num = 0;
    for (int inIndex = 0; inIndex < this.mVehicleManager.vehicleCount; ++inIndex)
    {
      if (!this.mVehicleManager.GetVehicle(inIndex).timer.hasSeenChequeredFlag)
        ++num;
    }
    if (num == 1)
    {
      this.infoLabel.text = Localisation.LocaliseID("PSG_10011076", (GameObject) null);
    }
    else
    {
      StringVariableParser.intValue1 = num;
      this.infoLabel.text = Localisation.LocaliseID("PSG_10010687", (GameObject) null);
    }
    if (num != 0)
      return;
    this.gameObject.SetActive(false);
  }
}
