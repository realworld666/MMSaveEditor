// Decompiled with JetBrains decompiler
// Type: UIGridDriverWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGridDriverWidget : MonoBehaviour
{
  public UICharacterPortrait driverPortrait;
  public Flag driverFlag;
  public UICar uiCar;
  public Image highlight;
  public TextMeshProUGUI gridPosition;
  public TextMeshProUGUI driverName;
  public TextMeshProUGUI lapTime;
  public TextMeshProUGUI lapTimeGap;
  public TextMeshProUGUI teamName;
  public TextMeshProUGUI championshipCurrentPosition;
  public UITyreWearIcon tyre;
  public Driver driver;
  public GameObject singleSeaterIcon;
  public GameObject gtCarIcon;

  public void Setup(RaceEventResults.ResultData inQualifyingResult, RaceEventResults.ResultData inReferencePositionData, int inPosition)
  {
    this.driver = inQualifyingResult.driver;
    RacingVehicle vehicle = Game.instance.vehicleManager.GetVehicle(this.driver);
    Championship.Series series = Game.instance.sessionManager.championship.series;
    GameUtility.SetActive(this.gtCarIcon, series == Championship.Series.GTSeries);
    GameUtility.SetActive(this.singleSeaterIcon, series == Championship.Series.SingleSeaterSeries);
    if (vehicle == null)
    {
      vehicle = RaceEventResults.GetVehicle(this.driver, (List<RaceEventResults.ResultData>) null);
      this.driver = vehicle.driver;
    }
    this.SetDriverDetails();
    this.gridPosition.text = (inPosition + 1).ToString();
    bool flag = (inPosition == 0 || inPosition == GameStatsConstants.qualifyingThresholdForQ2 || inPosition == GameStatsConstants.qualifyingThresholdForQ3) | Game.instance.sessionManager.championship.rules.gridSetup != ChampionshipRules.GridSetup.QualifyingBased3Sessions;
    this.lapTime.text = GameUtility.GetLapTimeText(inQualifyingResult.bestLapTime, false);
    this.lapTime.gameObject.SetActive(flag);
    float inLapTime = inQualifyingResult.bestLapTime - inReferencePositionData.bestLapTime;
    if (inQualifyingResult.laps > 0)
    {
      GameUtility.SetActive(this.lapTimeGap.gameObject, true);
      if ((double) inLapTime == 0.0)
        this.lapTimeGap.text = "-";
      else
        this.lapTimeGap.text = "+" + GameUtility.GetLapTimeText(inLapTime, false);
    }
    else
      GameUtility.SetActive(this.lapTimeGap.gameObject, false);
    this.tyre.SetTyreSet(vehicle.setup.tyreSet, (List<SessionCarBonuses.DisplayBonusInfo>) null);
    this.tyre.UpdateTyreLocking(vehicle, true);
  }

  private void SetDriverDetails()
  {
    this.driverName.text = this.driver.name;
    this.driverPortrait.SetPortrait((Person) this.driver);
    this.driverFlag.SetNationality(this.driver.nationality);
    this.teamName.text = this.driver.contract.GetTeam().name;
    this.uiCar.SetTeamColor(this.driver.contract.GetTeam().GetTeamColor().carColor);
    if (this.driver.IsPlayersDriver())
      this.highlight.gameObject.SetActive(true);
    else
      this.highlight.gameObject.SetActive(false);
    StringVariableParser.subject = (Person) this.driver;
    this.championshipCurrentPosition.text = Localisation.LocaliseID("PSG_10010583", (GameObject) null);
    StringVariableParser.subject = (Person) null;
  }
}
