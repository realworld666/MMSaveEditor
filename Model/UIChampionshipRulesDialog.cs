// Decompiled with JetBrains decompiler
// Type: UIChampionshipRulesDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIChampionshipRulesDialog : UIDialogBox
{
  private List<PoliticalVote> mRules = new List<PoliticalVote>();
  private List<PoliticalVote> mRulesDiff = new List<PoliticalVote>();
  public Button currentRulesButton;
  public Button nextYearsRulesButton;
  public Button closeButton;
  public GameObject currentRulesHeader;
  public GameObject currentRulesDescription;
  public GameObject nextYearRulesHeader;
  public GameObject nextYearRulesDescription;
  public GameObject upcomingVotesHeader;
  public GameObject upcomingVotesDescription;
  public GameObject noNextYearRules;
  public TextMeshProUGUI rulesYear;
  public UIChampionshipLogo championshipLogo;
  public UIGridList grid;
  private Championship mChampionship;
  private ChampionshipRules mChampionshipRules;
  private UIChampionshipRulesDialog.Mode mMode;
  private bool mHideButtons;
  private bool mNextYearRules;
  private Action mAction;

  public static void ShowRollover(Championship inChampionship, UIChampionshipRulesDialog.Mode inMode, bool inHideButtons = false, Action inAction = null)
  {
    UIChampionshipRulesDialog dialog = UIManager.instance.dialogBoxManager.GetDialog<UIChampionshipRulesDialog>();
    dialog.Open(inChampionship, inMode, inHideButtons, inAction);
    UIManager.instance.dialogBoxManager.Show((UIDialogBox) dialog);
  }

  public static void HideRollover()
  {
    UIManager.instance.dialogBoxManager.GetDialog<UIChampionshipRulesDialog>().Close();
  }

  protected override void Awake()
  {
    base.Awake();
    this.closeButton.onClick.AddListener(new UnityAction(this.OnCloseButton));
    this.currentRulesButton.onClick.AddListener(new UnityAction(this.OnRulesButton));
    this.nextYearsRulesButton.onClick.AddListener(new UnityAction(this.OnRulesButton));
  }

  public void Open(Championship inChampionship, UIChampionshipRulesDialog.Mode inMode, bool inHideButtons = false, Action inAction = null)
  {
    this.mChampionship = inChampionship;
    this.mMode = inMode;
    this.mAction = inAction;
    this.mHideButtons = inHideButtons;
    this.mNextYearRules = this.mMode == UIChampionshipRulesDialog.Mode.NextYearRules;
    this.mChampionshipRules = !this.mNextYearRules ? this.mChampionship.rules : this.mChampionship.nextYearsRules;
    this.GetRulesDiff();
    this.mRules = !this.mNextYearRules ? this.mChampionshipRules.votedRules : this.mRulesDiff;
    this.championshipLogo.SetChampionship(this.mChampionship);
    GameUtility.SetActive(this.rulesYear.gameObject, this.mMode != UIChampionshipRulesDialog.Mode.UpcomingVotes);
    this.rulesYear.text = this.mNextYearRules ? Localisation.LocaliseID("PSG_10002267", (GameObject) null) : Localisation.LocaliseID("PSG_10009390", (GameObject) null);
    GameUtility.SetActive(this.noNextYearRules, this.mNextYearRules && this.mRulesDiff.Count <= 0);
    GameUtility.SetActive(this.currentRulesButton.gameObject, this.mNextYearRules && !inHideButtons);
    GameUtility.SetActive(this.nextYearsRulesButton.gameObject, !this.mNextYearRules && !inHideButtons);
    GameUtility.SetActive(this.currentRulesHeader.gameObject, this.mMode == UIChampionshipRulesDialog.Mode.CurrentRules);
    GameUtility.SetActive(this.currentRulesDescription.gameObject, this.mMode == UIChampionshipRulesDialog.Mode.CurrentRules);
    GameUtility.SetActive(this.nextYearRulesHeader.gameObject, this.mMode == UIChampionshipRulesDialog.Mode.NextYearRules);
    GameUtility.SetActive(this.nextYearRulesDescription.gameObject, this.mMode == UIChampionshipRulesDialog.Mode.NextYearRules);
    GameUtility.SetActive(this.upcomingVotesHeader.gameObject, this.mMode == UIChampionshipRulesDialog.Mode.UpcomingVotes);
    GameUtility.SetActive(this.upcomingVotesDescription.gameObject, this.mMode == UIChampionshipRulesDialog.Mode.UpcomingVotes);
    this.SetRulesList();
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionScreen, 0.0f);
  }

  private void OnCloseButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Back, 0.0f);
    this.Close();
  }

  private void Close()
  {
    this.Hide();
    if (this.mAction == null)
      return;
    this.mAction();
    this.mAction = (Action) null;
  }

  private void SetRulesList()
  {
    this.grid.HideListItems();
    int count1 = this.mRules.Count;
    int inIndex = 0;
    if (this.mMode != UIChampionshipRulesDialog.Mode.UpcomingVotes)
    {
      for (int index = 0; index < count1; ++index)
      {
        PoliticalVote mRule = this.mRules[index];
        if (mRule.CanBeDisplayed())
        {
          UIChampionshipRulesEntry championshipRulesEntry = this.grid.GetOrCreateItem<UIChampionshipRulesEntry>(inIndex);
          championshipRulesEntry.Setup(mRule);
          GameUtility.SetActive(championshipRulesEntry.gameObject, true);
          ++inIndex;
        }
      }
    }
    else
    {
      this.mRules = this.mChampionship.politicalSystem.votesForSeason;
      int count2 = this.mRules.Count;
      for (int index = 0; index < count2; ++index)
      {
        PoliticalVote mRule = this.mRules[index];
        if (this.mChampionship.politicalSystem.GetVoteResultsForVote(mRule) == null)
        {
          UIChampionshipRulesEntry championshipRulesEntry = this.grid.GetOrCreateItem<UIChampionshipRulesEntry>(inIndex);
          championshipRulesEntry.Setup(mRule);
          GameUtility.SetActive(championshipRulesEntry.gameObject, true);
          ++inIndex;
        }
      }
    }
  }

  private void GetRulesDiff()
  {
    this.mRulesDiff = this.mChampionship.rules.GetRuleChangesForNextSeason();
  }

  private void OnRulesButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.Open(this.mChampionship, this.mMode != UIChampionshipRulesDialog.Mode.CurrentRules ? UIChampionshipRulesDialog.Mode.CurrentRules : UIChampionshipRulesDialog.Mode.NextYearRules, this.mHideButtons, this.mAction);
  }

  public enum Mode
  {
    CurrentRules,
    NextYearRules,
    UpcomingVotes,
  }
}
