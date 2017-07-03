// Decompiled with JetBrains decompiler
// Type: UITyreHistoryEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITyreHistoryEntry : MonoBehaviour
{
  public UITyreHistoryEntry.BarType barType;
  public Button button;
  public Image teamStripe;
  public TextMeshProUGUI driverPosition;
  public TextMeshProUGUI driverName;
  public TextMeshProUGUI stops;
  public Flag driverFlag;
  public UITyreHistoryStrategy strategy;
  public GameObject[] backing;
  private RacingVehicle mVehicle;

  public void OnStart()
  {
  }

  public void Setup(RacingVehicle inVehicle)
  {
    if (inVehicle == null)
      return;
    if (this.mVehicle != inVehicle)
    {
      this.mVehicle = inVehicle;
      Driver driver = this.mVehicle.driver;
      Team team = driver.contract.GetTeam();
      this.driverName.text = driver.shortName;
      this.driverFlag.SetNationality(driver.nationality);
      this.teamStripe.color = team.GetTeamColor().primaryUIColour.normal;
    }
    this.driverPosition.text = this.mVehicle.standingsPosition.ToString();
    this.stops.text = this.mVehicle.timer.stops.ToString();
    this.SetBarType();
    this.strategy.Setup(this.mVehicle);
  }

  private void SetBarType()
  {
    for (int index = 0; index < this.backing.Length; ++index)
      GameUtility.SetActive(this.backing[index].gameObject, (UITyreHistoryEntry.BarType) index == this.barType);
  }

  public enum BarType
  {
    Lighter,
    Darker,
    PlayerOwned,
  }
}
