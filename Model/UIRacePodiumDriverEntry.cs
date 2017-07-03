// Decompiled with JetBrains decompiler
// Type: UIRacePodiumDriverEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRacePodiumDriverEntry : MonoBehaviour
{
  public UICharacterPortrait portrait;
  public UITeamLogo teamLogo;
  public TextMeshProUGUI driverName;
  public TextMeshProUGUI driverTime;
  public TextMeshProUGUI points;
  public Flag driverFlag;
  public GameObject teamWinnerParent;
  public Image teamGradient;
  public Image teamColorTint;

  public void SetResultData(RaceEventResults.ResultData inResultData)
  {
    Driver driver = inResultData.driver;
    Team team = driver.contract.GetTeam();
    this.portrait.SetPortrait((Person) driver);
    this.driverFlag.SetNationality(driver.nationality);
    this.teamLogo.SetTeam(team);
    this.driverName.text = driver.name;
    if (inResultData.position == 1)
      this.driverTime.text = GameUtility.GetLapTimeText(inResultData.time, false);
    else
      GameUtility.SetGapTimeText(this.driverTime, inResultData, true);
    StringVariableParser.intValue1 = inResultData.points;
    this.points.text = "+" + Localisation.LocaliseID("PSG_10010222", (GameObject) null);
    GameUtility.SetActive(this.teamWinnerParent, team.IsPlayersTeam() || !team.championship.isPlayerChampionship);
    if (!this.teamWinnerParent.activeSelf)
      return;
    this.teamGradient.color = team.GetTeamColor().primaryUIColour.normal;
  }
}
