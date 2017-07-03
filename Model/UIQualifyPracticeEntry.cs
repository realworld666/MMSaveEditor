// Decompiled with JetBrains decompiler
// Type: UIQualifyPracticeEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIQualifyPracticeEntry : MonoBehaviour
{
  public UIQualifyPracticeEntry.BarType barType = UIQualifyPracticeEntry.BarType.Darker;
  public Color positionLabelColor = new Color();
  public Image[] backing = new Image[0];
  public TextMeshProUGUI positionLabel;
  public TextMeshProUGUI driverNameLabel;
  public TextMeshProUGUI teamNameLabel;
  public TextMeshProUGUI bestLabel;
  public TextMeshProUGUI gapLabel;
  public TextMeshProUGUI statusLabel;
  public Flag flag;
  public Image teamColorStrip;
  public UICarSetupTyreIcon tyre;
  public Image sessionObjectiveLine;
  public GameObject retiredContainer;
  public GameObject crashedContainer;

  public void SetInfo(RaceEventResults.ResultData inResultData, RaceEventResults.ResultData inFirstPlaceEntry, int inPosition, SessionDetails.SessionType inSessionType)
  {
    GameUtility.SetActive(this.gameObject, true);
    this.statusLabel.text = string.Empty;
    Team team = inResultData.driver.contract.GetTeam();
    SessionObjective sessionObjective = Game.instance.player.team.sponsorController.GetCurrentSessionObjective();
    GameUtility.SetActiveAndCheckNull(this.retiredContainer, inResultData.carState == RaceEventResults.ResultData.CarState.Retired);
    GameUtility.SetActiveAndCheckNull(this.crashedContainer, inResultData.carState == RaceEventResults.ResultData.CarState.Crashed);
    if (sessionObjective != null && inSessionType != SessionDetails.SessionType.Practice)
      this.ShowSessionObjective(inPosition + 1 == Mathf.RoundToInt((float) sessionObjective.targetResult), sessionObjective.lastObjectiveCompleted);
    else
      GameUtility.SetActive(this.sessionObjectiveLine.gameObject, false);
    this.flag.SetNationality(inResultData.driver.nationality);
    this.teamColorStrip.color = team.GetTeamColor().primaryUIColour.normal;
    this.driverNameLabel.text = inResultData.driver.name;
    this.bestLabel.text = GameUtility.GetLapTimeText(inResultData.bestLapTime, false);
    this.positionLabel.text = (inPosition + 1).ToString();
    this.teamNameLabel.text = team.name;
    float inLapTime = inResultData.bestLapTime - inFirstPlaceEntry.bestLapTime;
    if (inResultData.laps > 0)
    {
      if ((double) inLapTime == 0.0)
        this.gapLabel.text = "-";
      else
        this.gapLabel.text = "+" + GameUtility.GetLapTimeText(inLapTime, false);
    }
    else
      this.gapLabel.text = string.Empty;
    if (team == Game.instance.player.team)
      this.barType = UIQualifyPracticeEntry.BarType.PlayerOwned;
    if (inResultData.laps > 0)
    {
      GameUtility.SetActive(this.tyre.gameObject, true);
      this.tyre.SetTyre(inResultData.tyreCompound, 1f);
    }
    else
      GameUtility.SetActive(this.tyre.gameObject, false);
    this.SetBarType();
  }

  private void SetBarType()
  {
    for (int index = 0; index < this.backing.Length; ++index)
      GameUtility.SetActive(this.backing[index].gameObject, (UIQualifyPracticeEntry.BarType) index == this.barType);
  }

  public void ShowSessionObjective(bool inShow, bool inIsObjectiveBeingMet)
  {
    GameUtility.SetActive(this.sessionObjectiveLine.transform.parent.gameObject, inShow);
    this.sessionObjectiveLine.color = !inIsObjectiveBeingMet ? UIConstants.sponsorGreyColor : UIConstants.positiveColor;
  }

  public enum BarType
  {
    Lighter,
    Darker,
    PlayerOwned,
  }
}
