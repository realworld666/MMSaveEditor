// Decompiled with JetBrains decompiler
// Type: UIMultipleQualifyingEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMultipleQualifyingEntry : MonoBehaviour
{
  public UIMultipleQualifyingEntry.BarType barType = UIMultipleQualifyingEntry.BarType.Darker;
  public Color positionLabelColor = new Color();
  public Image[] backing = new Image[0];
  public TextMeshProUGUI positionLabel;
  public TextMeshProUGUI driverNameLabel;
  public TextMeshProUGUI teamNameLabel;
  public TextMeshProUGUI[] bestLabelForSeveralSessions;
  public TextMeshProUGUI gapLabel;
  public TextMeshProUGUI statusLabel;
  public Flag flag;
  public Image teamColorStrip;
  public UICarSetupTyreIcon[] tyre;
  public Image sessionObjectiveLine;
  public TextMeshProUGUI qualifyingThresholdLabel;
  public GameObject qualifyingThreshold;
  public GameObject retiredContainer;
  public GameObject crashedContainer;

  public void SetInfo(RaceEventResults.ResultData inResultData, RaceEventResults.ResultData inFirstPlaceEntry, int inPosition, SessionDetails.SessionType inSessionType)
  {
    List<RaceEventResults.SessonResultData> resultsForSession = Game.instance.sessionManager.eventDetails.results.GetAllResultsForSession(SessionDetails.SessionType.Qualifying);
    GameUtility.SetActive(this.gameObject, true);
    bool flag1 = false;
    bool flag2 = false;
    this.statusLabel.text = string.Empty;
    int a = Game.instance.sessionManager.eventDetails.currentSession.sessionNumberForUI;
    if (Game.instance.sessionManager.eventDetails.currentSession.sessionType != SessionDetails.SessionType.Qualifying)
      a = 2;
    bool inIsActive = false;
    int inSessionNumber1 = 0;
    for (int inSessionNumber2 = 0; inSessionNumber2 < Mathf.Min(a, 2); ++inSessionNumber2)
    {
      inIsActive = inPosition == RaceEventResults.GetPositionThreshold(inSessionNumber2);
      inSessionNumber1 = inSessionNumber2;
      if (inIsActive)
        break;
    }
    GameUtility.SetActive(this.qualifyingThreshold, inIsActive);
    if (this.qualifyingThreshold.activeSelf)
      this.qualifyingThresholdLabel.text = UIMultipleQualifyingEntry.GetQualifyingThresholdText(inSessionNumber1);
    for (int index = 0; index < this.bestLabelForSeveralSessions.Length; ++index)
    {
      if (this.bestLabelForSeveralSessions[index].gameObject.activeSelf)
      {
        if (index < resultsForSession.Count && !flag1)
        {
          RaceEventResults.ResultData resultForDriver = resultsForSession[index].GetResultForDriver(inResultData.driver);
          if ((double) resultForDriver.gapToLeader > 0.0)
            this.bestLabelForSeveralSessions[index].color = this.gapLabel.color;
          else
            this.bestLabelForSeveralSessions[index].color = UIConstants.sectorDriverFastestColor;
          this.bestLabelForSeveralSessions[index].text = GameUtility.GetLapTimeText(resultForDriver.bestLapTime, false);
          if ((double) resultForDriver.bestLapTime == 0.0)
            this.bestLabelForSeveralSessions[index].text = "-";
          if (resultForDriver.qualifyingPositionRecorded)
          {
            flag1 = true;
            inResultData = resultForDriver;
            inFirstPlaceEntry = resultsForSession[index].resultData[0];
            if (resultForDriver.qualifyingSession != 3)
            {
              this.statusLabel.text = Localisation.LocaliseID("PSG_10011492", (GameObject) null);
              this.statusLabel.color = UIConstants.qualifyingEliminationColorForPositionLabel;
              this.positionLabel.color = UIConstants.qualifyingEliminationColorForPositionLabel;
            }
            else
            {
              this.statusLabel.color = this.positionLabelColor;
              this.positionLabel.color = this.positionLabelColor;
            }
          }
          this.tyre[index].SetTyre(resultForDriver.tyreCompound, 1f);
          GameUtility.SetActive(this.tyre[index].gameObject, resultForDriver.laps > 0);
        }
        else
        {
          this.bestLabelForSeveralSessions[index].text = string.Empty;
          flag2 = true;
          GameUtility.SetActive(this.tyre[index].gameObject, false);
        }
      }
      else
        GameUtility.SetActive(this.tyre[index].gameObject, false);
    }
    Team team = inResultData.driver.contract.GetTeam();
    SessionObjective sessionObjective = Game.instance.player.team.sponsorController.GetCurrentSessionObjective();
    GameUtility.SetActiveAndCheckNull(this.retiredContainer, inResultData.carState == RaceEventResults.ResultData.CarState.Retired);
    GameUtility.SetActiveAndCheckNull(this.crashedContainer, inResultData.carState == RaceEventResults.ResultData.CarState.Crashed);
    if (sessionObjective != null && Game.instance.sessionManager.sessionType != SessionDetails.SessionType.Practice)
      this.ShowSessionObjective(inPosition + 1 == Mathf.RoundToInt((float) sessionObjective.targetResult), sessionObjective.lastObjectiveCompleted);
    else
      GameUtility.SetActive(this.sessionObjectiveLine.gameObject, false);
    this.flag.SetNationality(inResultData.driver.nationality);
    this.teamColorStrip.color = team.GetTeamColor().primaryUIColour.normal;
    this.driverNameLabel.text = inResultData.driver.name;
    this.positionLabel.text = (inPosition + 1).ToString();
    this.teamNameLabel.text = team.name;
    float inLapTime = inResultData.bestLapTime - inFirstPlaceEntry.bestLapTime;
    if (inResultData.laps > 0 && !flag2)
    {
      if ((double) inLapTime == 0.0)
        this.gapLabel.text = "-";
      else
        this.gapLabel.text = "+" + GameUtility.GetLapTimeText(inLapTime, false);
    }
    else
      this.gapLabel.text = string.Empty;
    if (team == Game.instance.player.team)
      this.barType = UIMultipleQualifyingEntry.BarType.PlayerOwned;
    this.SetBarType();
  }

  private void SetBarType()
  {
    for (int index = 0; index < this.backing.Length; ++index)
      GameUtility.SetActive(this.backing[index].gameObject, (UIMultipleQualifyingEntry.BarType) index == this.barType);
  }

  public void ShowSessionObjective(bool inShow, bool inIsObjectiveBeingMet)
  {
    GameUtility.SetActive(this.sessionObjectiveLine.transform.parent.gameObject, inShow);
    this.sessionObjectiveLine.color = !inIsObjectiveBeingMet ? UIConstants.sponsorGreyColor : UIConstants.positiveColor;
  }

  public static string GetQualifyingThresholdText(int inSessionNumber)
  {
    StringVariableParser.intValue1 = inSessionNumber + 1;
    return Localisation.LocaliseID("PSG_10011495", (GameObject) null);
  }

  public enum BarType
  {
    Lighter,
    Darker,
    PlayerOwned,
  }
}
