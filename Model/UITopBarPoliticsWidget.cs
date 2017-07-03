// Decompiled with JetBrains decompiler
// Type: UITopBarPoliticsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UITopBarPoliticsWidget : MonoBehaviour
{
  public GameObject dropdownContainer;
  public GameObject[] dropdownMouseContainers;
  public Button leftButton;
  public Button rightButton;
  public Button trackButton;
  public TextMeshProUGUI selectedVoteHeader;
  public TextMeshProUGUI selectedVoteTimeLeft;
  public TextMeshProUGUI selectedVoteDescription;
  public GameObject trackButtonContainer;
  public GameObject voteCompleteContainer;
  public TextMeshProUGUI selectedVoteWinner;
  public TextMeshProUGUI selectedVoteAbstained;
  public Slider selectedVoteYesSlider;
  public Slider selectedVoteNoSlider;
  public GameObject additionalVotingPowerContainer;
  public UIGridList additionalVotingPowerList;
  private int mSelectedVoteIndex;
  private PoliticalVote mSelectedVote;
  private PoliticalSystem mPoliticalSystem;

  private void Start()
  {
    this.leftButton.onClick.AddListener(new UnityAction(this.OnLeftButton));
    this.rightButton.onClick.AddListener(new UnityAction(this.OnRightButton));
    this.trackButton.onClick.AddListener(new UnityAction(this.OnTrackButton));
  }

  private void OnLeftButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    --this.mSelectedVoteIndex;
    this.ClampSelectedVoteIndex();
    this.Refresh();
  }

  private void OnRightButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    ++this.mSelectedVoteIndex;
    this.ClampSelectedVoteIndex();
    this.Refresh();
  }

  private void OnTrackButton()
  {
    GameUtility.SetActive(this.dropdownContainer, false);
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    UIEventCalendarVariationsPopup.ShowTrackLayoutPolitics(this.mSelectedVote, (Action) null);
  }

  private void ClampSelectedVoteIndex()
  {
    int max = this.mPoliticalSystem.votesForSeason.Count - 1;
    this.mSelectedVoteIndex = Mathf.Clamp(this.mSelectedVoteIndex, 0, max);
    this.leftButton.interactable = this.mSelectedVoteIndex > 0;
    this.rightButton.interactable = this.mSelectedVoteIndex < max;
  }

  public void OnOpen()
  {
    if (!Game.IsActive() || Game.instance.player.IsUnemployed())
      return;
    this.mPoliticalSystem = Game.instance.player.team.championship.politicalSystem;
    this.mSelectedVoteIndex = this.mPoliticalSystem.votesForSeason.Count - this.mPoliticalSystem.votesRemaining;
    this.ClampSelectedVoteIndex();
    this.Refresh();
  }

  private void Refresh()
  {
    Championship championship = Game.instance.player.team.championship;
    this.mSelectedVote = this.mPoliticalSystem.votesForSeason[this.mSelectedVoteIndex];
    this.selectedVoteHeader.text = this.mSelectedVote.GetName();
    this.selectedVoteDescription.text = this.mSelectedVote.GetDescription();
    GameUtility.SetActive(this.trackButtonContainer, this.mSelectedVote.HasImpactOfType<PoliticalImpactChangeTrack>());
    GameUtility.SetActive(this.voteCompleteContainer, this.mSelectedVoteIndex < this.mPoliticalSystem.voteResultsForSeason.Count);
    if (this.voteCompleteContainer.activeSelf)
    {
      PoliticalSystem.VoteResults voteResults = this.mPoliticalSystem.voteResultsForSeason[this.mSelectedVoteIndex];
      int num = voteResults.yesVotesCount + voteResults.noVotesCount;
      this.selectedVoteTimeLeft.text = string.Empty;
      this.selectedVoteNoSlider.maxValue = (float) num;
      this.selectedVoteYesSlider.maxValue = (float) num;
      this.selectedVoteNoSlider.value = (float) voteResults.noVotesCount;
      this.selectedVoteYesSlider.value = (float) voteResults.yesVotesCount;
      StringVariableParser.intValue1 = voteResults.abstainedVotesCount;
      this.selectedVoteAbstained.text = Localisation.LocaliseID("PSG_10010402", (GameObject) null);
      this.selectedVoteWinner.text = Localisation.LocaliseEnum((Enum) voteResults.voteResult);
    }
    else
    {
      int days = (this.mPoliticalSystem.voteCalendarEventsForSeason[this.mSelectedVoteIndex].triggerDate - Game.instance.time.now).Days;
      int num = Mathf.RoundToInt((float) days / 7f);
      if (num > 1)
      {
        StringVariableParser.intValue1 = num;
        this.selectedVoteTimeLeft.text = Localisation.LocaliseID("PSG_10010401", (GameObject) null);
      }
      else if (num > 0)
      {
        StringVariableParser.intValue1 = num;
        this.selectedVoteTimeLeft.text = Localisation.LocaliseID("PSG_10010404", (GameObject) null);
      }
      else
      {
        StringVariableParser.intValue1 = days;
        this.selectedVoteTimeLeft.text = Localisation.LocaliseID("PSG_10010403", (GameObject) null);
      }
    }
    this.additionalVotingPowerList.HideListItems();
    int inIndex1 = 0;
    for (int inIndex2 = 0; inIndex2 < championship.standings.teamEntryCount; ++inIndex2)
    {
      Team entity = championship.standings.GetTeamEntry(inIndex2).GetEntity<Team>();
      if (entity.votingPower > 0)
      {
        this.additionalVotingPowerList.GetOrCreateItem<UIPoliticalVotingPowerEntry>(inIndex1).Setup(entity);
        ++inIndex1;
      }
    }
    GameUtility.SetActive(this.additionalVotingPowerContainer, inIndex1 > 0 && this.mSelectedVote == this.mPoliticalSystem.nextVote);
  }

  public void Update()
  {
    GameUtility.DisableIfMouseExit(this.dropdownContainer, this.dropdownMouseContainers);
  }
}
