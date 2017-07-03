// Decompiled with JetBrains decompiler
// Type: UITimedRaceEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITimedRaceEntry : MonoBehaviour
{
  public UITimedRaceEntry.BarType barType = UITimedRaceEntry.BarType.Darker;
  public Image[] backing = new Image[0];
  private bool[] mPopupOpen = new bool[5];
  private GameObject[] mObjectTargets = new GameObject[5];
  public TextMeshProUGUI positionLabel;
  public TextMeshProUGUI driverNameLabel;
  public TextMeshProUGUI teamNameLabel;
  public TextMeshProUGUI changeLabel;
  public TextMeshProUGUI stopsLabel;
  public TextMeshProUGUI timeLabel;
  public TextMeshProUGUI gapLabel;
  public TextMeshProUGUI pointsLabel;
  public GameObject bonusPointsFastestLapContainer;
  public GameObject bonusPointsForPolePositionContainer;
  public GameObject riskDemotionIconContainer;
  public Flag flag;
  public Image teamColorStrip;
  public Image greenArrow;
  public Image redArrow;
  public GameObject retiredContainer;
  public GameObject crashedContainer;
  public GameObject timePenaltyContainer;
  public GameObject pitPenaltyContainer;
  public Image sessionObjectiveLine;
  private RaceEventResults.ResultData mData;
  private int mPlacesLost;
  private int mPartsBanned;
  private float mTimePenaltyTotal;
  private int mPitlaneDrivesCount;

  private void Start()
  {
    this.greenArrow.color = UIConstants.positiveColor;
    this.redArrow.color = UIConstants.negativeColor;
    this.mObjectTargets[0] = this.riskDemotionIconContainer;
    this.mObjectTargets[1] = this.bonusPointsFastestLapContainer;
    this.mObjectTargets[2] = this.bonusPointsForPolePositionContainer;
    this.mObjectTargets[3] = this.timePenaltyContainer;
    this.mObjectTargets[4] = this.pitPenaltyContainer;
  }

  private void Update()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    UITimedRaceEntry.\u003CUpdate\u003Ec__AnonStoreyA2 updateCAnonStoreyA2 = new UITimedRaceEntry.\u003CUpdate\u003Ec__AnonStoreyA2();
    // ISSUE: reference to a compiler-generated field
    updateCAnonStoreyA2.\u003C\u003Ef__this = this;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    for (updateCAnonStoreyA2.index = UITimedRaceEntry.ToolTipType.PartRuleBreak; updateCAnonStoreyA2.index < UITimedRaceEntry.ToolTipType.Count; updateCAnonStoreyA2.index = updateCAnonStoreyA2.index + 1)
    {
      // ISSUE: reference to a compiler-generated field
      if (this.mObjectTargets[(int) updateCAnonStoreyA2.index].activeSelf)
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated method
        GameUtility.HandlePopup(ref this.mPopupOpen[(int) updateCAnonStoreyA2.index], this.mObjectTargets[(int) updateCAnonStoreyA2.index], new Action(updateCAnonStoreyA2.\u003C\u003Em__206), new Action(this.HideTooltip));
      }
    }
  }

  public void SetInfo(RaceEventResults.ResultData inResultData, RaceEventResults.ResultData inFirstPlaceEntry, int inPosition, int inPreviousPosition)
  {
    this.mData = inResultData;
    Driver driver = this.mData.driver;
    Team team = driver.contract.GetTeam();
    SessionObjective sessionObjective = Game.instance.player.team.sponsorController.GetCurrentSessionObjective();
    GameUtility.SetActiveAndCheckNull(this.retiredContainer, this.mData.carState == RaceEventResults.ResultData.CarState.Retired);
    GameUtility.SetActiveAndCheckNull(this.crashedContainer, this.mData.carState == RaceEventResults.ResultData.CarState.Crashed);
    GameUtility.SetActiveAndCheckNull(this.bonusPointsFastestLapContainer, this.mData.gotExtraPointsForFastestLap);
    GameUtility.SetActiveAndCheckNull(this.bonusPointsForPolePositionContainer, this.mData.gotExtraPointsForPolePosition);
    if (sessionObjective != null)
      this.ShowSessionObjective(inPosition == Mathf.RoundToInt((float) sessionObjective.targetResult), sessionObjective.lastObjectiveCompleted);
    else
      this.ShowSessionObjective(false, false);
    this.flag.SetNationality(driver.nationality);
    this.teamColorStrip.color = team.GetTeamColor().primaryUIColour.normal;
    this.driverNameLabel.text = driver.name;
    this.positionLabel.text = inPosition.ToString();
    this.changeLabel.text = Mathf.Abs(inPreviousPosition - inPosition).ToString();
    this.teamNameLabel.text = team.name;
    this.mData.lapsToLeader = inFirstPlaceEntry.laps - this.mData.laps;
    if (this.mData.position == 1)
      this.timeLabel.text = GameUtility.GetLapTimeText(this.mData.time, false);
    else
      GameUtility.SetGapTimeText(this.timeLabel, this.mData, true);
    this.pointsLabel.text = this.mData.points.ToString();
    this.stopsLabel.text = this.mData.stops.ToString();
    GameUtility.SetGapTimeText(this.gapLabel, this.mData, false);
    if (team.IsPlayersTeam())
      this.barType = UITimedRaceEntry.BarType.PlayerOwned;
    this.SetArrows(inPreviousPosition, inPosition);
    this.SetBarType();
    bool inIsActive = false;
    this.mTimePenaltyTotal = 0.0f;
    this.mPitlaneDrivesCount = 0;
    this.mPartsBanned = 0;
    this.mPlacesLost = 0;
    for (int index = 0; index < this.mData.penalties.Count; ++index)
    {
      Penalty penalty = this.mData.penalties[index];
      if (penalty is PenaltyTime)
        this.mTimePenaltyTotal += ((PenaltyTime) penalty).seconds;
      if (penalty is PenaltyPitlaneDriveThru)
        ++this.mPitlaneDrivesCount;
      if (penalty is PenaltyPartRulesBroken)
      {
        PenaltyPartRulesBroken penaltyPartRulesBroken = penalty as PenaltyPartRulesBroken;
        ++this.mPartsBanned;
        this.mPlacesLost += penaltyPartRulesBroken.placesLost;
        inIsActive = true;
      }
    }
    if (inIsActive)
    {
      this.timeLabel.text = "-";
      this.gapLabel.text = "-";
    }
    GameUtility.SetActiveAndCheckNull(this.timePenaltyContainer, (double) this.mTimePenaltyTotal != 0.0);
    GameUtility.SetActiveAndCheckNull(this.pitPenaltyContainer, this.mPitlaneDrivesCount != 0);
    GameUtility.SetActiveAndCheckNull(this.riskDemotionIconContainer, inIsActive);
  }

  public void ShowToolTip(UITimedRaceEntry.ToolTipType inType)
  {
    switch (inType)
    {
      case UITimedRaceEntry.ToolTipType.PartRuleBreak:
        string empty = string.Empty;
        StringVariableParser.intValue1 = Mathf.RoundToInt((float) this.mPlacesLost);
        StringVariableParser.intValue2 = Mathf.RoundToInt((float) this.mPartsBanned);
        UIManager.instance.dialogBoxManager.GetDialog<GenericInfoRollover>().Open(Localisation.LocaliseID("PSG_10007154", (GameObject) null), Localisation.LocaliseID(this.mPartsBanned != 1 ? (this.mPlacesLost != 1 ? "PSG_10011099" : "PSG_10011098") : (this.mPlacesLost != 1 ? "PSG_10007155" : "PSG_10011097"), (GameObject) null));
        break;
      case UITimedRaceEntry.ToolTipType.FastestLapBonus:
        UIManager.instance.dialogBoxManager.GetDialog<GenericInfoRollover>().Open(Localisation.LocaliseID("PSG_10007150", (GameObject) null), Localisation.LocaliseID("PSG_10007151", (GameObject) null));
        break;
      case UITimedRaceEntry.ToolTipType.QualifyingPolePositionBonus:
        UIManager.instance.dialogBoxManager.GetDialog<GenericInfoRollover>().Open(Localisation.LocaliseID("PSG_10007152", (GameObject) null), Localisation.LocaliseID("PSG_10007153", (GameObject) null));
        break;
      case UITimedRaceEntry.ToolTipType.TimePenalty:
        StringVariableParser.intValue1 = Mathf.RoundToInt(this.mTimePenaltyTotal);
        UIManager.instance.dialogBoxManager.GetDialog<GenericInfoRollover>().Open(Localisation.LocaliseID("PSG_10010042", (GameObject) null), Localisation.LocaliseID("PSG_10010043", (GameObject) null));
        break;
      case UITimedRaceEntry.ToolTipType.PitLaneDriveTrought:
        StringVariableParser.intValue1 = this.mPitlaneDrivesCount;
        if (this.mPitlaneDrivesCount == 1)
        {
          UIManager.instance.dialogBoxManager.GetDialog<GenericInfoRollover>().Open(Localisation.LocaliseID("PSG_10010039", (GameObject) null), Localisation.LocaliseID("PSG_10010041", (GameObject) null));
          break;
        }
        UIManager.instance.dialogBoxManager.GetDialog<GenericInfoRollover>().Open(Localisation.LocaliseID("PSG_10010039", (GameObject) null), Localisation.LocaliseID("PSG_10010040", (GameObject) null));
        break;
    }
  }

  public void HideTooltip()
  {
    UIManager.instance.dialogBoxManager.GetDialog<GenericInfoRollover>().Hide();
  }

  private void SetArrows(int inPreviousPosition, int inPosition)
  {
    if (inPreviousPosition > inPosition)
    {
      GameUtility.SetActive(this.greenArrow.gameObject, true);
      GameUtility.SetActive(this.redArrow.gameObject, false);
    }
    else if (inPreviousPosition < inPosition)
    {
      GameUtility.SetActive(this.redArrow.gameObject, true);
      GameUtility.SetActive(this.greenArrow.gameObject, false);
    }
    else
    {
      GameUtility.SetActive(this.redArrow.gameObject, false);
      GameUtility.SetActive(this.greenArrow.gameObject, false);
    }
  }

  private void SetBarType()
  {
    for (int index = 0; index < this.backing.Length; ++index)
      GameUtility.SetActive(this.backing[index].gameObject, (UITimedRaceEntry.BarType) index == this.barType);
  }

  public void ShowSessionObjective(bool inShow, bool inIsObjectiveBeingMet)
  {
    GameUtility.SetActive(this.sessionObjectiveLine.transform.parent.gameObject, inShow);
    if (inIsObjectiveBeingMet)
      this.sessionObjectiveLine.color = UIConstants.positiveColor;
    else
      this.sessionObjectiveLine.color = UIConstants.sponsorGreyColor;
  }

  public enum BarType
  {
    Lighter,
    Darker,
    PlayerOwned,
  }

  public enum ToolTipType
  {
    PartRuleBreak,
    FastestLapBonus,
    QualifyingPolePositionBonus,
    TimePenalty,
    PitLaneDriveTrought,
    Count,
  }
}
