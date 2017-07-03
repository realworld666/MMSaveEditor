// Decompiled with JetBrains decompiler
// Type: UITyreSelectionEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITyreSelectionEntry : MonoBehaviour
{
  public Image teamStrip;
  public TextMeshProUGUI teamName;
  public Flag driverOneFlag;
  public TextMeshProUGUI driverOneName;
  public UITyreSelectionTyreSet driverOneTyres;
  public Flag driverTwoFlag;
  public TextMeshProUGUI driverTwoName;
  public UITyreSelectionTyreSet driverTwoTyres;
  private Team mTeam;

  public void Setup(Team inTeam)
  {
    this.mTeam = inTeam;
    this.teamStrip.color = this.mTeam.GetTeamColor().primaryUIColour.normal;
    this.teamName.text = this.mTeam.name;
    RacingVehicle[] vehiclesByTeam = Game.instance.vehicleManager.GetVehiclesByTeam(this.mTeam);
    int slickTyresPerEvent = inTeam.championship.rules.maxSlickTyresPerEvent;
    int compoundsAvailable = inTeam.championship.rules.compoundsAvailable;
    Driver driver1 = vehiclesByTeam[0].driver;
    Driver driver2 = vehiclesByTeam[1].driver;
    this.driverOneFlag.SetNationality(driver1.nationality);
    this.driverOneName.text = driver1.name;
    this.driverOneTyres.Setup(vehiclesByTeam[0], compoundsAvailable, slickTyresPerEvent);
    this.driverTwoFlag.SetNationality(driver2.nationality);
    this.driverTwoName.text = driver2.name;
    this.driverTwoTyres.Setup(vehiclesByTeam[1], compoundsAvailable, slickTyresPerEvent);
  }
}
