// Decompiled with JetBrains decompiler
// Type: UIStandingsOverviewWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIStandingsOverviewWidget : MonoBehaviour
{
  private bool mCurrentStandings = true;
  public UIChampionshipLogo championshipLogo;
  public Button leftButton;
  public Button rightButton;
  public Button historyLeftButton;
  public Button historyRightButton;
  public Button currentRulesButton;
  public Button upcomingRulesButton;
  public Button seasonCalendarButton;
  public TextMeshProUGUI championshipName;
  public TextMeshProUGUI championshipYear;
  public TextMeshProUGUI prizeFund;
  public TextMeshProUGUI tvAudience;
  public StandingsScreen screen;
  private ChampionshipStandings mStandings;
  private Championship mChampionship;
  private bool mHasHistory;
  private int mCurrentHistoryNum;

  public void OnStart()
  {
    this.leftButton.onClick.AddListener(new UnityAction(this.ChangeToChampionshipAbove));
    this.rightButton.onClick.AddListener(new UnityAction(this.ChangeToChampionshipBelow));
    this.historyLeftButton.onClick.AddListener(new UnityAction(this.OnHistoryLeft));
    this.historyRightButton.onClick.AddListener(new UnityAction(this.OnHistoryRight));
    this.currentRulesButton.onClick.AddListener((UnityAction) (() => this.OnRulesButton(true)));
    this.upcomingRulesButton.onClick.AddListener((UnityAction) (() => this.OnRulesButton(false)));
    this.seasonCalendarButton.onClick.AddListener(new UnityAction(this.OnSeasonCalendarButton));
  }

  public void Setup(ChampionshipStandings inStandings)
  {
    if (inStandings == null)
      return;
    this.mStandings = inStandings;
    this.mChampionship = this.mStandings.championship;
    this.mCurrentStandings = this.mChampionship.standings == this.mStandings;
    this.mHasHistory = this.mChampionship.standingsHistory.historyCount > 0;
    this.mCurrentHistoryNum = this.mCurrentStandings ? (!this.mHasHistory ? 0 : this.mChampionship.standingsHistory.historyCount) : this.mChampionship.standingsHistory.GetIndex(this.mStandings);
    this.SetDetails();
    this.UpdateButtonsState();
    this.UpdateHistoryButtonsState();
  }

  private void SetDetails()
  {
    this.championshipName.text = this.mChampionship.GetChampionshipName(false);
    this.championshipYear.text = this.mCurrentStandings || !this.mCurrentStandings && !this.mHasHistory ? (this.mChampionship.calendar.Count <= 0 ? Game.instance.time.now.Year.ToString() : this.mChampionship.calendar[0].eventDate.Year.ToString()) : this.mChampionship.standingsHistory.GetEntry(this.mCurrentHistoryNum).year.ToString();
    if (this.mCurrentStandings)
    {
      this.prizeFund.text = GameUtility.GetCurrencyString((long) this.mChampionship.prizeFund, 0);
      this.tvAudience.text = GameUtility.FormatNumberString(this.mChampionship.tvAudience);
    }
    else
    {
      ChampionshipStandingsHistoryEntry entry = this.mChampionship.standingsHistory.GetEntry(this.mCurrentHistoryNum);
      this.prizeFund.text = GameUtility.GetCurrencyString((long) entry.prizeFund, 0);
      this.tvAudience.text = GameUtility.FormatNumberString(entry.tvAudience);
    }
    this.championshipLogo.SetChampionship(this.mChampionship);
    this.currentRulesButton.interactable = this.mCurrentStandings;
    this.upcomingRulesButton.interactable = this.mCurrentStandings;
    this.seasonCalendarButton.interactable = this.mCurrentStandings;
  }

  private void ChangeToChampionshipAbove()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.ChangeToChampionshipIfValid(this.mChampionship.championshipAboveID);
  }

  private void ChangeToChampionshipBelow()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.ChangeToChampionshipIfValid(this.mChampionship.championshipBelowID);
  }

  private void ChangeToChampionshipIfValid(int championshipID)
  {
    if (championshipID != -1)
      this.screen.SetStandings(Game.instance.championshipManager.GetChampionshipByID(championshipID).standings);
    this.UpdateButtonsState();
  }

  private void UpdateButtonsState()
  {
    this.leftButton.interactable = this.mChampionship.championshipAboveID != -1;
    this.rightButton.interactable = this.mChampionship.championshipBelowID != -1;
  }

  private void UpdateHistoryButtonsState()
  {
    int historyCount = this.mChampionship.standingsHistory.historyCount;
    this.historyLeftButton.interactable = this.mHasHistory && this.mCurrentHistoryNum > 0;
    this.historyRightButton.interactable = this.mHasHistory && this.mCurrentHistoryNum < historyCount;
  }

  private void OnRulesButton(bool inCurrentRules)
  {
    if (inCurrentRules)
      UIChampionshipRulesDialog.ShowRollover(this.screen.championship, UIChampionshipRulesDialog.Mode.CurrentRules, false, (Action) null);
    else
      UIChampionshipRulesDialog.ShowRollover(this.screen.championship, UIChampionshipRulesDialog.Mode.NextYearRules, false, (Action) null);
  }

  private void OnHistoryLeft()
  {
    this.mCurrentHistoryNum = Mathf.Max(this.mCurrentHistoryNum - 1, 0);
    if (this.mCurrentHistoryNum == this.mChampionship.standingsHistory.historyCount)
      this.screen.SetStandings(this.mChampionship.standings);
    else
      this.screen.SetStandings(this.mChampionship.standingsHistory.GetEntry(this.mCurrentHistoryNum).standings);
    this.UpdateHistoryButtonsState();
  }

  private void OnHistoryRight()
  {
    this.mCurrentHistoryNum = Mathf.Min(this.mCurrentHistoryNum + 1, this.mChampionship.standingsHistory.historyCount);
    if (this.mCurrentHistoryNum == this.mChampionship.standingsHistory.historyCount)
      this.screen.SetStandings(this.mChampionship.standings);
    else
      this.screen.SetStandings(this.mChampionship.standingsHistory.GetEntry(this.mCurrentHistoryNum).standings);
    this.UpdateHistoryButtonsState();
  }

  private void OnSeasonCalendarButton()
  {
    UIManager.instance.ChangeScreen("EventCalendarScreen", (Entity) this.mChampionship, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
  }
}
