// Decompiled with JetBrains decompiler
// Type: UILiveTimingsSharedEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILiveTimingsSharedEntry : MonoBehaviour
{
  public UILiveTimingsSharedEntry.BarType barType;
  public TextMeshProUGUI driverName;
  public TextMeshProUGUI driverPosition;
  public TextMeshProUGUI teamName;
  public Button button;
  public Image teamStripe;
  public UICarSetupTyreIcon tyreIcon;
  public Flag driverFlag;
  public GameObject[] backing;
  private RacingVehicle mVehicle;

  public virtual void OnStart()
  {
  }

  public virtual void Setup(RacingVehicle inVehicle)
  {
    if (inVehicle != null)
    {
      this.mVehicle = inVehicle;
      Driver driver = this.mVehicle.driver;
      Team team = driver.contract.GetTeam();
      this.driverName.text = driver.shortName;
      this.driverFlag.SetNationality(driver.nationality);
      this.teamName.text = team.name;
      this.teamStripe.color = team.GetTeamColor().primaryUIColour.normal;
    }
    if (Game.instance.sessionManager.sessionType == SessionDetails.SessionType.Race || this.mVehicle.timer.HasSetLapTime())
      this.driverPosition.text = this.mVehicle.standingsPosition.ToString();
    else
      this.driverPosition.text = "-";
    this.tyreIcon.SetTyre(this.mVehicle.setup.tyreSet);
    this.SetBarType();
  }

  private void SetBarType()
  {
    for (int index = 0; index < this.backing.Length; ++index)
      GameUtility.SetActive(this.backing[index].gameObject, (UILiveTimingsSharedEntry.BarType) index == this.barType);
  }

  public enum BarType
  {
    Lighter,
    Darker,
    PlayerOwned,
  }
}
