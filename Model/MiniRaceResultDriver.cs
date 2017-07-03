// Decompiled with JetBrains decompiler
// Type: MiniRaceResultDriver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniRaceResultDriver : MonoBehaviour
{
  public Image colorTint;
  public TextMeshProUGUI positionLabel;
  public Flag driverFlag;
  public TextMeshProUGUI driverNameLabel;
  public TextMeshProUGUI pointsLabel;
  public GameObject bonusPointsFastestLapContainer;
  public GameObject bonusPointsForPolePositionContainer;

  public void SetResultData(RaceEventResults.ResultData inResultData)
  {
    Driver driver = inResultData.driver;
    this.colorTint.color = driver.contract.GetTeam().GetTeamColor().primaryUIColour.normal;
    this.positionLabel.text = GameUtility.FormatForPosition(inResultData.position, (string) null);
    this.driverFlag.SetNationality(driver.nationality);
    this.driverNameLabel.text = driver.lastName;
    this.pointsLabel.text = inResultData.points.ToString();
    GameUtility.SetActive(this.bonusPointsFastestLapContainer, inResultData.gotExtraPointsForFastestLap);
    GameUtility.SetActive(this.bonusPointsForPolePositionContainer, inResultData.gotExtraPointsForPolePosition);
  }

  public void ShowToolTip(int inType)
  {
    switch (inType)
    {
      case 1:
        UIManager.instance.dialogBoxManager.GetDialog<GenericInfoRollover>().Open(Localisation.LocaliseID("PSG_10007150", (GameObject) null), Localisation.LocaliseID("PSG_10007151", (GameObject) null));
        break;
      case 2:
        UIManager.instance.dialogBoxManager.GetDialog<GenericInfoRollover>().Open(Localisation.LocaliseID("PSG_10007152", (GameObject) null), Localisation.LocaliseID("PSG_10007153", (GameObject) null));
        break;
    }
  }

  public void HideTooltip()
  {
    UIManager.instance.dialogBoxManager.GetDialog<GenericInfoRollover>().Hide();
  }
}
